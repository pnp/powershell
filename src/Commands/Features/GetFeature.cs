using Microsoft.SharePoint.Client;
using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq;
using System.Linq.Expressions;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Features
{
    [Cmdlet(VerbsCommon.Get, "PnPFeature")]
    [OutputType(typeof(Feature))]
    public class GetFeature : PnPWebRetrievalsCmdlet<Feature>
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public FeaturePipeBind Identity;

        [Parameter(Mandatory = false)]
        public FeatureScope Scope = FeatureScope.Web;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<Feature, object>>[] { f => f.DisplayName };
            FeatureCollection featureCollection;
            if (Scope == FeatureScope.Site)
            {
                featureCollection = ClientContext.Site.Features;
            }
            else
            {
                featureCollection = CurrentWeb.Features;
            }
            IEnumerable<Feature> query = ClientContext.LoadQuery(featureCollection.IncludeWithDefaultProperties(RetrievalExpressions));
            ClientContext.ExecuteQueryRetry();
            if (Identity == null)
            {
                WriteObject(query, true);
            }
            else
            {
                if (Identity.Id != Guid.Empty)
                {
                    WriteObject(query.Where(f => f.DefinitionId == Identity.Id));
                }
                else if (!string.IsNullOrEmpty(Identity.Name))
                {
                    WriteObject(query.Where(f => f.DisplayName.Equals(Identity.Name, StringComparison.OrdinalIgnoreCase)));
                }
            }
        }

    }
}
