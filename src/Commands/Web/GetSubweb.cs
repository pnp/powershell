using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPSubWeb")]
    public class GetSubWebs : PnPWebRetrievalsCmdlet<Web>
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public WebPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Recurse;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeRootWeb;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<Web, object>>[] { w => w.Id, w => w.Url, w => w.Title, w => w.ServerRelativeUrl };

            Web parentWeb = CurrentWeb;
            List<Web> results = new List<Web>();
            if(IncludeRootWeb)
            {
                parentWeb.EnsureProperties(RetrievalExpressions);
                results.Add(parentWeb);
            }

            if (Identity != null)
            {
                parentWeb = Identity.GetWeb(ClientContext, RetrievalExpressions);
                
                if (parentWeb != null)
                {
                    if (Recurse)
                    {
                        results.Add(parentWeb);
                        results.AddRange(GetSubWebsInternal(parentWeb.Webs, Recurse));
                    }
                    else
                    {
                        results.Add(parentWeb);
                    }
                }
                else
                {
                    throw new PSArgumentException($"No subweb found with the provided id or url", nameof(Identity));
                }
            }
            else
            {
                ClientContext.Load(parentWeb.Webs);
                ClientContext.ExecuteQueryRetry();

                results.AddRange(GetSubWebsInternal(parentWeb.Webs, Recurse));
            }

            WriteObject(results, true);
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