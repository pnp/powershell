using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using Microsoft.SharePoint.Client;
using System.Text.Json.Serialization;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Compression;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Admin
{
    [Obsolete("This usage of this cmdlet is not supported anymore and will be removed in the next version.")]
    [Cmdlet(VerbsData.Publish, "PnPCompanyApp")]
    public class BuildCompanyApp : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string PortalUrl;

        [Parameter(Mandatory = true)]
        [ValidateLength(1, 30)]
        public string AppName;

        [Parameter(Mandatory = false)]
        [ValidateLength(1, 80)]
        public string Description;

        [Parameter(Mandatory = false)]
        [ValidateLength(1, 4000)]
        public string LongDescription;

        [Parameter(Mandatory = false)]
        public string PrivacyPolicyUrl = "https://privacy.microsoft.com/en-us/privacystatement";

        [Parameter(Mandatory = false)]
        public string TermsAndUsagePolicyUrl = "https://go.microsoft.com/fwlink/?linkid=2039674";

        [Parameter(Mandatory = true)]
        public string CompanyName;

        [Parameter(Mandatory = true)]
        public string CompanyWebSiteUrl;

        [Parameter(Mandatory = true)]
        public string ColoredIconPath;

        [Parameter(Mandatory = true)]
        public string OutlineIconPath;

        [Parameter(Mandatory = false)]
        public string AccentColor = "#40497E";

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public SwitchParameter NoUpload;

        protected override void ExecuteCmdlet()
        {

            var isValidCommSite = Tenant.IsValidCommSite(PortalUrl);
            AdminContext.ExecuteQueryRetry();
            if (!isValidCommSite.Value)
            {
                throw new PSInvalidOperationException("The site specified is not a Communication Site");
            }
            var packageName = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, $"{AppName}.zip");
            if (System.IO.File.Exists(packageName) && (Force || ShouldContinue($"File {packageName} exists. Overwrite?", string.Empty)))
            {
                System.IO.File.Delete(packageName);

            }
            var uri = new Uri(PortalUrl);
            var host = uri.Host;
            var searchUrlPath = host;
            if (uri.LocalPath.Contains("/teams") || uri.LocalPath.Contains("/sites"))
            {
                var match = Regex.Match(uri.LocalPath, "^\\/[^\\/]+\\/[^\\/]+");
                if (match.Success)
                {
                    searchUrlPath = host + match.Value;
                }
            }
            if (!System.IO.Path.IsPathRooted(ColoredIconPath))
            {
                ColoredIconPath = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, ColoredIconPath);
            }
            if (!System.IO.File.Exists(ColoredIconPath))
            {
                throw new PSArgumentException($"File {ColoredIconPath} does not exist.");
            }
            if (!System.IO.Path.IsPathRooted(OutlineIconPath))
            {
                OutlineIconPath = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, OutlineIconPath);
            }
            if (!System.IO.File.Exists(OutlineIconPath))
            {
                throw new PSArgumentException($"File {OutlineIconPath} does not exist.");
            }
            var modifiedPortalUrl = string.Empty;
            if (PortalUrl.Contains("?"))
            {
                modifiedPortalUrl = PortalUrl + "&app=portals";
            }
            else
            {
                modifiedPortalUrl = PortalUrl + "?app=portals";
            }

            if (!ParameterSpecified(nameof(PrivacyPolicyUrl)))
            {
                WriteWarning("Privacy Policy link not provided, adding Microsoft privacy link");
            }
            if (!ParameterSpecified(nameof(TermsAndUsagePolicyUrl)))
            {
                WriteWarning("Terms and Usage policy link not provided. Adding Microsoft Terms Of Use link.");
            }
            var guid = Guid.NewGuid();

            var coloredIconFileName = new System.IO.FileInfo(ColoredIconPath).Name;
            var outlineIconFileName = new System.IO.FileInfo(OutlineIconPath).Name;
            var appManifest = new AppManifest();
            appManifest.Id = guid;
            appManifest.PackageName = $"com.microsoft.teams.{AppName}";
            appManifest.Developer.Name = CompanyName;
            appManifest.Developer.WebSiteUrl = CompanyWebSiteUrl;
            appManifest.Developer.PrivacyUrl = PrivacyPolicyUrl;
            appManifest.Developer.TermsOfUseUrl = TermsAndUsagePolicyUrl;

            appManifest.Icons.Color = coloredIconFileName;
            appManifest.Icons.Outline = outlineIconFileName;

            appManifest.Name.Short = AppName;
            appManifest.Name.Long = AppName;

            appManifest.Description.Short = Description;
            appManifest.Description.Long = LongDescription;

            appManifest.AccentColor = AccentColor;
            appManifest.IsFullScreen = true;

            appManifest.StaticTabs.Add(new AppManifest_Tab()
            {
                EntityId = $"sharepointportal_{guid}",
                Name = $"Portals-{AppName}",
                ContentUrl = $"https://{host}/_layouts/15/teamslogon.aspx?spfx=true&dest={modifiedPortalUrl}",
                WebSiteUrl = $"https://{host}/_layouts/15/teamslogon.aspx?spfx=true&dest={PortalUrl}",
                SearchUrl = $"https://{searchUrlPath}/_layouts/15/search.aspx?q={{searchQuery}}"
            });

            appManifest.ValidDomains.Add(host);
            appManifest.ValidDomains.Add("*.login.microsoftonline.com");
            appManifest.ValidDomains.Add("*.sharepoint.com");
            appManifest.ValidDomains.Add("*.sharepoint-df.com");
            appManifest.ValidDomains.Add("spoppe-a.akamaihd.net");
            appManifest.ValidDomains.Add("spoprod-a.akamaihd.net");
            appManifest.ValidDomains.Add("resourceeng.blob.core.windows.net");
            appManifest.ValidDomains.Add("msft.spoppe.com");

            appManifest.WebApplicationInfo.Add("id", "00000003-0000-0ff1-ce00-000000000000");
            appManifest.WebApplicationInfo.Add("resource", $"https://{host}");
            var output = JsonSerializer.Serialize(appManifest);


            if (!System.IO.File.Exists(packageName))
            {
                var tempFolder = Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempFolder);
                System.IO.File.WriteAllText(System.IO.Path.Combine(tempFolder, "manifest.json"), output);
                System.IO.File.Copy(ColoredIconPath, System.IO.Path.Combine(tempFolder, coloredIconFileName));
                System.IO.File.Copy(OutlineIconPath, System.IO.Path.Combine(tempFolder, outlineIconFileName));
                ZipFile.CreateFromDirectory(tempFolder, System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, $"{AppName}.zip"));
                Directory.Delete(tempFolder, true);
            }
            else
            {
                throw new PSInvalidOperationException("Cannot create package");
            }

            WriteObject($"Teams app created: {System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, $"{AppName}.zip")}");

            if (!NoUpload)
            {
                var bytes = System.IO.File.ReadAllBytes(System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, $"{AppName}.zip"));
                TeamsUtility.AddApp(this, Connection, GraphAccessToken, bytes);
                WriteObject($"Teams app uploaded to teams app Store.");
            }
        }

        internal class AppManifest
        {
            [JsonPropertyName("$schema")]
            public string Schema { get; set; } = "https://developer.microsoft.com/en-us/json-schemas/teams/v1.9/MicrosoftTeams.schema.json";

            [JsonPropertyName("manifestVersion")]
            public string ManifestVersion { get; set; } = "1.9";

            [JsonPropertyName("version")]
            public string Version { get; set; } = "1.0";

            [JsonPropertyName("id")]
            public Guid Id { get; set; }

            [JsonPropertyName("packageName")]
            public string PackageName { get; set; }

            [JsonPropertyName("developer")]
            public AppManifest_Developer Developer { get; set; } = new AppManifest_Developer();

            [JsonPropertyName("icons")]
            public AppManifest_Icons Icons { get; set; } = new AppManifest_Icons();

            [JsonPropertyName("name")]
            public AppManifest_Name Name { get; set; } = new AppManifest_Name();

            [JsonPropertyName("description")]
            public AppManifest_Description Description { get; set; } = new AppManifest_Description();

            [JsonPropertyName("accentColor")]
            public string AccentColor { get; set; }

            [JsonPropertyName("isFullScreen")]
            public bool IsFullScreen { get; set; }

            [JsonPropertyName("staticTabs")]
            public List<AppManifest_Tab> StaticTabs { get; set; } = new List<AppManifest_Tab>();

            [JsonPropertyName("permissions")]
            public List<string> Permissions { get; set; } = new List<string> { "identity", "messageTeamMembers" };

            [JsonPropertyName("validDomains")]
            public List<string> ValidDomains { get; set; } = new List<string>();

            [JsonPropertyName("webApplicationInfo")]
            public Dictionary<string, string> WebApplicationInfo { get; set; } = new Dictionary<string, string>();
        }

        internal class AppManifest_Developer
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("websiteUrl")]
            public string WebSiteUrl { get; set; }
            [JsonPropertyName("privacyUrl")]
            public string PrivacyUrl { get; set; }
            [JsonPropertyName("termsOfUseUrl")]
            public string TermsOfUseUrl { get; set; }
        }

        internal class AppManifest_Icons
        {
            [JsonPropertyName("color")]
            public string Color { get; set; }

            [JsonPropertyName("outline")]
            public string Outline { get; set; }
        }

        internal class AppManifest_Name
        {
            [JsonPropertyName("short")]
            public string Short { get; set; }

            [JsonPropertyName("full")]
            public string Long { get; set; }
        }

        internal class AppManifest_Description
        {
            [JsonPropertyName("short")]
            public string Short { get; set; }

            [JsonPropertyName("full")]
            public string Long { get; set; }
        }

        internal class AppManifest_Tab
        {
            [JsonPropertyName("entityId")]
            public string EntityId { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("contentUrl")]
            public string ContentUrl { get; set; }

            [JsonPropertyName("websiteUrl")]
            public string WebSiteUrl { get; set; }

            [JsonPropertyName("searchUrl")]
            public string SearchUrl { get; set; }

            [JsonPropertyName("scopes")]
            public string[] Scopes { get; set; } = new string[] { "personal" };

            [JsonPropertyName("supportedPlatform")]
            public string[] SupportedPlatform { get; set; } = new string[] { "desktop" };
        }

    }
}
