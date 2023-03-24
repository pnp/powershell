using PnP.Framework.ALM;
using PnP.Framework.Enums;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Add, "PnPApp")]
    public class AddApp : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Path;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public AppCatalogScope Scope = AppCatalogScope.Tenant;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public SwitchParameter Publish;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
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