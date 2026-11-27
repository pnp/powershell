using PnP.Core.Provisioning.Providers.Xml;
using PnP.PowerShell.Commands.Utilities;
using System.IO;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Provisioning
{
    public partial class ConvertSiteTemplate
    {
        private void BeginProcessingExperimental()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }
            if (ParameterSpecified(nameof(Out)) && !System.IO.Path.IsPathRooted(Out))
            {
                Out = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Out);
            }

            var fileInfo = new FileInfo(Path);
            var template = new XMLFileSystemTemplateProvider(fileInfo.DirectoryName, string.Empty).GetTemplate(fileInfo.Name);
            if (template == null)
            {
                return;
            }

            var xml = template.ToXML(CoreProvisioningHelper.GetFormatter(ToSchema));

            if (string.IsNullOrEmpty(Out))
            {
                WriteObject(xml);
                return;
            }

            if (File.Exists(Out) && !Force && !ShouldContinue(string.Format(Resources.File0ExistsOverwrite, Out), Resources.Confirm))
            {
                return;
            }

            File.WriteAllText(Out, xml, Encoding);
        }
    }
}
