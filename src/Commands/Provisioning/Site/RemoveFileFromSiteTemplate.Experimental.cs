using PnP.Core.Provisioning.Connectors;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    public partial class RemoveFileFromSiteTemplate
    {
        private void ProcessRecordExperimental()
        {
            if (ParameterSpecified(nameof(TemplateProviderExtensions)))
            {
                LogWarning("-TemplateProviderExtensions has no effect with -Experimental: template provider extensions are not run by the PnP.Core.Provisioning engine.");
            }

            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }

            var template = CoreProvisioningHelper.LoadSiteTemplateFromFile(Path, LogError);
            if (template == null)
            {
                throw new ApplicationException("Invalid template file!");
            }

            var fileToRemove = template.Files.FirstOrDefault(f => f.Src == FilePath);
            if (fileToRemove == null)
            {
                return;
            }

            template.Files.Remove(fileToRemove);
            template.Connector.DeleteFile(FilePath);

            if (template.Connector is ICommitableFileConnector commitableConnector)
            {
                commitableConnector.Commit();
            }

            CoreProvisioningHelper.SaveSiteTemplateToFile(template, Path);
        }
    }
}
