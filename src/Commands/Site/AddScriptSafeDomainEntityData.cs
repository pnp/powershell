using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Add, "PnPScriptSafeDomain")]
    [OutputType(typeof(ScriptSafeDomain))]
    public class AddScriptSafeDomain : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string DomainName;
        protected override void ExecuteCmdlet()
        {
            // Validate user inputs
            ScriptSafeDomain safeDomain = null;
            try
            {
                safeDomain = ClientContext.Site.CustomScriptSafeDomains.GetByDomainName(DomainName);
                ClientContext.Load(safeDomain);
                ClientContext.ExecuteQueryRetry();
            }
            catch { }
            if (safeDomain.ServerObjectIsNull == null)
            {
                var spSafeDomain = new ScriptSafeDomainEntityData();
                spSafeDomain.DomainName = DomainName;

                safeDomain = ClientContext.Site.CustomScriptSafeDomains.Create(spSafeDomain);
                ClientContext.Load(safeDomain);
                ClientContext.ExecuteQueryRetry();
                WriteObject(safeDomain);
            }
            else
            {
                WriteWarning($"Unable to add Domain Name as there is an existing domain name with the same name. Will be skipped.");
            }
        }
    }
}
