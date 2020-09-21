using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using PnP.PowerShell.Commands.Extensions;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPSubWebs")]
    
    public class GetSubWebs : PnPWebRetrievalsCmdlet<Web>
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public WebPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Recurse;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<Web, object>>[] { w => w.Id, w => w.Url, w => w.Title, w => w.ServerRelativeUrl };

            Web parentWeb = SelectedWeb;
            if (Identity != null)
            {
                if (Identity.Id != Guid.Empty)
                {
                    parentWeb = parentWeb.GetWebById(Identity.Id);
                }
                else if (Identity.Web != null)
                {
                    parentWeb = Identity.Web;
                }
                else if (Identity.Url != null)
                {
                    parentWeb = parentWeb.GetWebByUrl(Identity.Url);
                }
            }

            var allWebs = GetSubWebsInternal(parentWeb.Webs, Recurse);
            WriteObject(allWebs, true);
        }

        private List<Web> GetSubWebsInternal(WebCollection subsites, bool recurse)
        {
            var subwebs = new List<Web>();

            // Retrieve the subsites in the provided webs collection
            subsites.EnsureProperties(new Expression<Func<WebCollection, object>>[] { wc => wc.Include(w => w.Id) });

            foreach (var subsite in subsites)
            {
                // Retrieve all the properties for this particular subsite
                subsite.EnsureProperties(RetrievalExpressions);
                subwebs.Add(subsite);

                if (recurse)
                {
                    // As the Recurse flag has been set, recurse this method for it's child web collection
                    subwebs.AddRange(GetSubWebsInternal(subsite.Webs, recurse));
                }
            }

            return subwebs;
        }
    }
}
