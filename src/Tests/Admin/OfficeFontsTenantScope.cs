using Microsoft.VisualStudio.TestTools.UnitTesting;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;

namespace PnP.PowerShell.Tests.Admin
{
    /// <summary>
    /// PowerShell runspace for the Office font tenant tests. They CHANGE THE TENANT'S OFFICE FONTS, so
    /// they only run when these environment variables are set (on a test tenant):
    ///   PnPTests_OfficeFonts       "1" to run them
    ///   PnPTests_TenantUrl         https://[tenant].sharepoint.com
    ///   PnPTests_ClientId          Entra app (client) id; sign-in is interactive (-PersistLogin)
    ///   PnPTests_OfficeFontFolder  folder with at least 3 .ttf/.otf fonts you may publish
    /// Otherwise the tests are inconclusive. The Office font library and the public CDN must be set up.
    /// At the end, the library offers exactly the fonts of PnPTests_OfficeFontFolder.
    /// </summary>
    internal sealed class OfficeFontsTenantScope : IDisposable
    {
        private readonly Runspace _runspace;

        public string FontFolder { get; }
        public string[] FontNames { get; }

        private OfficeFontsTenantScope(string tenantUrl, string clientId, string fontFolder)
        {
            FontFolder = fontFolder;
            FontNames = Directory.EnumerateFiles(fontFolder)
                .Where(f => f.EndsWith(".ttf", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".otf", StringComparison.OrdinalIgnoreCase))
                .Select(Path.GetFileName)
                .OrderBy(n => n, StringComparer.OrdinalIgnoreCase)
                .ToArray();
            if (FontNames.Length < 3) Assert.Inconclusive($"PnPTests_OfficeFontFolder needs at least 3 .ttf/.otf fonts; {fontFolder} has {FontNames.Length}.");

            // Every PnP cmdlet, without importing the module.
            var state = InitialSessionState.CreateDefault();
            foreach (var type in typeof(PnPConnectedCmdlet).Assembly.GetTypes().Where(t => !t.IsAbstract && typeof(Cmdlet).IsAssignableFrom(t)))
            {
                var attribute = type.GetCustomAttribute<CmdletAttribute>();
                if (attribute != null) state.Commands.Add(new SessionStateCmdletEntry($"{attribute.VerbName}-{attribute.NounName}", type, null));
            }
            _runspace = RunspaceFactory.CreateRunspace(state);
            _runspace.Open();
            Run("Connect-PnPOnline -Url $args[0] -ClientId $args[1] -PersistLogin", tenantUrl, clientId);
        }

        /// <summary>The scope, or an inconclusive test when the tenant tests are not enabled.</summary>
        public static OfficeFontsTenantScope Open()
        {
            if (Environment.GetEnvironmentVariable("PnPTests_OfficeFonts") != "1")
            {
                Assert.Inconclusive("Office font tenant tests are off: they change the tenant's Office fonts. Set PnPTests_OfficeFonts=1 on a test tenant.");
            }
            string Required(string name) => Environment.GetEnvironmentVariable(name) is { Length: > 0 } value
                ? value
                : throw new AssertInconclusiveException($"Set {name} to run the Office font tenant tests.");
            return new OfficeFontsTenantScope(Required("PnPTests_TenantUrl"), Required("PnPTests_ClientId"), Required("PnPTests_OfficeFontFolder"));
        }

        /// <summary>Runs a script; positional values are available as $args. Fails the test on errors.</summary>
        public Collection<PSObject> Run(string script, params object[] values)
        {
            using var shell = System.Management.Automation.PowerShell.Create();
            shell.Runspace = _runspace;
            shell.AddScript(script);
            foreach (var value in values) shell.AddArgument(value);
            var output = shell.Invoke();
            if (shell.HadErrors)
            {
                Assert.Fail(string.Join(Environment.NewLine, shell.Streams.Error.Select(e => e.ToString())));
            }
            return output;
        }

        /// <summary>Text output of a cmdlet, joined.</summary>
        public string RunText(string script, params object[] values) =>
            string.Join(" ", Run(script, values).Select(o => o?.ToString()));

        /// <summary>Server-relative URL of the Office font library.</summary>
        public string LibraryPath => (string)Run(@"
            $library = Get-PnPOrgAssetsLibrary |
                ForEach-Object { if ($_.PSObject.Properties['OrgAssetsLibraries']) { $_.OrgAssetsLibraries } else { $_ } } |
                Where-Object { ""$($_.OrgAssetType)"" -match 'OfficeFontLibrary' } | Select-Object -First 1
            if (-not $library) { throw 'No Office font library is configured.' }
            $url = if ($library.LibraryUrl.DecodedUrl) { $library.LibraryUrl.DecodedUrl } else { ""$($library.LibraryUrl)"" }
            '/' + [Uri]::UnescapeDataString(($url -replace '^https?://[^/]+', '')).Trim('/')").Single().BaseObject;

        /// <summary>Font file names in the Office font library (all folders).</summary>
        public string[] LibraryFonts() => Run(LibrarySite + @"
            $list = Get-PnPList -Includes RootFolder -Connection $site | Where-Object { $_.RootFolder.ServerRelativeUrl -ieq $path } | Select-Object -First 1
            Get-PnPListItem -List $list.Id -PageSize 500 -Fields FileLeafRef -Connection $site |
                Where-Object { $_['FileLeafRef'] -match '\.(ttf|otf)$' } | ForEach-Object { $_['FileLeafRef'] }", LibraryPath)
            .Select(o => o.ToString()).OrderBy(n => n, StringComparer.OrdinalIgnoreCase).ToArray();

        /// <summary>Face ids (file names without extension) listed in catalog version <paramref name="major"/>.</summary>
        public string[] CatalogFonts(int major) => Run(LibrarySite + @"
            $json = Get-PnPFile -Url ""$path/$($args[1])/fontcatalog.fontconfig"" -AsString -Connection $site | ConvertFrom-Json
            $json.Fonts.sf.id", LibraryPath, major)
            .Select(o => o.ToString()).OrderBy(n => n, StringComparer.OrdinalIgnoreCase).ToArray();

        /// <summary>Sets $path (library, from $args[0]) and $site (a connection to the library's site).</summary>
        private const string LibrarySite = @"
            $path = $args[0]
            $siteUrl = $env:PnPTests_TenantUrl.TrimEnd('/') + ($path -replace '/[^/]+$', '')
            $site = Connect-PnPOnline -Url $siteUrl -ClientId $env:PnPTests_ClientId -PersistLogin -ReturnConnection";

        /// <summary>A new temporary folder with the first <paramref name="count"/> test fonts.</summary>
        public string Subset(int count)
        {
            var folder = Path.Combine(Path.GetTempPath(), "pnp-officefonts-" + Guid.NewGuid().ToString("N")[..8]);
            Directory.CreateDirectory(folder);
            foreach (var name in FontNames.Take(count)) System.IO.File.Copy(Path.Combine(FontFolder, name), Path.Combine(folder, name));
            return folder;
        }

        public static string[] Ids(params string[] fileNames) =>
            fileNames.Select(Path.GetFileNameWithoutExtension).OrderBy(n => n, StringComparer.OrdinalIgnoreCase).ToArray();

        public void Dispose() => _runspace?.Dispose();
    }
}
