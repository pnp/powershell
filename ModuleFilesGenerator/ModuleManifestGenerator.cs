using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.ModuleFilesGenerator
{
    internal class ModuleManifestGenerator
    {
        private string _assemblyPath;
        private string _configurationName;
        private List<Model.CmdletInfo> _cmdlets;
        private Version _assemblyVersion;

        public ModuleManifestGenerator(List<Model.CmdletInfo> cmdlets, string assemblyPath, string configurationName, Version assemblyVersion)
        {
            _cmdlets = cmdlets;
            _assemblyPath = assemblyPath;
            _configurationName = configurationName;
            _assemblyVersion = assemblyVersion;
        }
        internal void Generate()
        {

            // Generate PSM1 file
            var aliasesToExport = new List<string>();
            foreach (var cmdlet in _cmdlets.Where(c => c.Aliases.Any()))
            {
                foreach (var alias in cmdlet.Aliases)
                {
                    aliasesToExport.Add(alias);
                }
            }

            // Create Module Manifest
            var manifestFolder = $"{new FileInfo(_assemblyPath).Directory}\\..\\ModuleFiles\\";
            if (!Directory.Exists(manifestFolder))
            {
                Directory.CreateDirectory(manifestFolder);
            }
            var psd1Path = $"{manifestFolder}\\PnP.PowerShell.psd1";

            var cmdletsToExportString = string.Join(",", _cmdlets.Select(c => "\"" + c.FullCommand + "\""));
            string aliasesToExportString = null;
            if (aliasesToExport.Any())
            {
                aliasesToExportString = string.Join(",", aliasesToExport.Select(x => "\"" + x + "\""));
            }
            WriteModuleManifest(psd1Path, cmdletsToExportString, aliasesToExportString);
        }

        private void WriteModuleManifest(string path, string cmdletsToExport, string aliasesToExport)
        {
            var manifest = $@"@{{
    NestedModules =  if ($PSEdition -eq 'Core')
    {{
        'Core/PnP.PowerShell.dll'
    }}
    else
    {{
        'Framework/PnP.PowerShell.dll'
    }}
    ModuleVersion = '{_assemblyVersion.Major}.{_assemblyVersion.Minor}.{_assemblyVersion.Revision}'
    Description = 'Microsoft 365 Patterns and Practices PowerShell Cmdlets'
    GUID = '0b0430ce-d799-4f3b-a565-f0dca1f31e17'
    Author = 'Microsoft 365 Patterns and Practices'
    CompanyName = 'Microsoft 365 Patterns and Practices'
    CompatiblePSEditions = @(""Core"",""Desktop"")
    PowerShellVersion = '5.1'
    DotNetFrameworkVersion = '4.6.1'
    ProcessorArchitecture = 'None'
    FunctionsToExport = '*'
    CmdletsToExport = @({cmdletsToExport})
    VariablesToExport = '*'
    AliasesToExport = '*'
    FormatsToProcess = 'PnP.PowerShell.Format.ps1xml' 
    PrivateData = @{{
        PSData = @{{
            ProjectUri = 'https://aka.ms/sppnp'
            IconUri = 'https://github.com/pnp/media/raw/e62d267575c81bda81485111ec52714033141e62/parker/pnp/300w/parker.png'
        }}
    }}
}}";
            File.WriteAllText(path, manifest, Encoding.UTF8);
        }
    }
}
