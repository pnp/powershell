using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq.Expressions;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPWeb")]
    [OutputType(typeof(Web))]
    public class GetWeb : PnPRetrievalsCmdlet<Web>
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public WebPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<Web, object>>[] { w => w.Id, w => w.Url, w => w.Title, w => w.ServerRelativeUrl };
            if (Identity == null)
            {
                ClientContext.Web.EnsureProperties(RetrievalExpressions);
                WriteObject(ClientContext.Web);
            }
            else
            {
                WriteObject(Identity.GetWeb(ClientContext, RetrievalExpressions));
            }
        }
    }
}
