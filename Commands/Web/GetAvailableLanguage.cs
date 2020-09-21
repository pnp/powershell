using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq.Expressions;
using System.Management.Automation;
using PnP.PowerShell.CmdletHelpAttributes;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPAvailableLanguage")]
    public class GetAvailableLanguage : PnPRetrievalsCmdlet<Web>
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public WebPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<Web, object>>[] { w => w.RegionalSettings.InstalledLanguages };
            if (Identity == null)
            {
                ClientContext.Web.EnsureProperties(RetrievalExpressions);
                WriteObject(ClientContext.Web.RegionalSettings.InstalledLanguages, true);
            }
            else
            {
                if (Identity.Id != Guid.Empty)
                {
                    WriteObject(ClientContext.Web.RegionalSettings.InstalledLanguages, true);
                }
                else if (Identity.Web != null)
                {
                    WriteObject(ClientContext.Web.RegionalSettings.InstalledLanguages, true);
                }
                else if (Identity.Url != null)
                {
                    WriteObject(ClientContext.Web.RegionalSettings.InstalledLanguages, true);
                }
            }
        }
    }
}