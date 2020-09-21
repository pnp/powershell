using System.Management.Automation;
using PnP.Framework.Provisioning.Model;
using PnP.PowerShell.CmdletHelpAttributes;

namespace PnP.PowerShell.Commands.Extensibility
{
    [Cmdlet(VerbsCommon.New, "PnPExtensibilityHandlerObject")]
    public class NewExtensibilityHandlerObject : PSCmdlet
    {
        [Parameter(Mandatory = true, Position=0, ValueFromPipeline=true)]
        public string Assembly;

        [Parameter(Mandatory = true)]
        public string Type;

        [Parameter(Mandatory = false)]
        public string Configuration;

        [Parameter(Mandatory = false)]
        public SwitchParameter Disabled;


        protected override void ProcessRecord()
        {
            var handler = new ExtensibilityHandler
            {
                Assembly = Assembly,
                Type = Type,
                Configuration = Configuration,
                Enabled = !Disabled
            };
            WriteObject(handler);
        }

    }
}
