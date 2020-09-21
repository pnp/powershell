using PnP.Framework.ALM;
using PnP.Framework.Enums;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Enums;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Add, "PnPApp")]
    public class AddApp : PnPSharePointCmdlet
    {
        private const string ParameterSet_ADD = "Add only";
        private const string ParameterSet_PUBLISH = "Add and Publish";

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_ADD, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_PUBLISH, ValueFromPipeline = true)]
        public string Path;

        [Parameter(Mandatory = false)]
        public AppCatalogScope Scope = AppCatalogScope.Tenant;

        [Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = ParameterSet_PUBLISH)]
        public SwitchParameter Publish;

        [Parameter(Mandatory = false, ValueFromPipeline = false, ParameterSetName = ParameterSet_PUBLISH)]
        public SwitchParameter SkipFeatureDeployment;

        [Parameter(Mandatory = false)]
        public SwitchParameter Overwrite;

        [Parameter(Mandatory = false)]
        public int Timeout = 200;

        protected override void ExecuteCmdlet()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }

            var fileInfo = new System.IO.FileInfo(Path);

            var bytes = System.IO.File.ReadAllBytes(Path);

            var manager = new AppManager(ClientContext);

            var result = manager.Add(bytes, fileInfo.Name, Overwrite, Scope, timeoutSeconds: Timeout);

            try
            {

                if (Publish)
                {
                    if (manager.Deploy(result, SkipFeatureDeployment, Scope))
                    {
                        result = manager.GetAvailable(result.Id, Scope);
                    }

                }
                WriteObject(result);
            }
            catch
            {
                // Exception occurred rolling back
                manager.Remove(result, Scope);
                throw;
            }
        }
    }
}