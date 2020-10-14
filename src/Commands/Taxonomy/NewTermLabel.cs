using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.New, "TermLabel")]
    public class NewTermLabel : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public TaxonomyTermPipeBind Term;

        [Parameter(Mandatory = true)]
        public string Name;

        [Parameter(Mandatory = true)]
        public int Lcid;

        [Parameter(Mandatory = false)]
        public SwitchParameter IsDefault = true;

        [Parameter(Mandatory = false)]
        public TaxonomyTermStorePipeBind TermStore;

        protected override void ExecuteCmdlet()
        {
            var taxonomySession = TaxonomySession.GetTaxonomySession(ClientContext);

            TermStore termStore = null;
            if (TermStore == null)
            {
                termStore = taxonomySession.GetDefaultSiteCollectionTermStore();
            }
            else
            {
                termStore = TermStore.GetTermStore(taxonomySession);
            }

            termStore.EnsureProperty(ts => ts.DefaultLanguage);

            Term term = Term.GetTerm(ClientContext, termStore, null, false, null);

            var label = Term.Item.CreateLabel(Name, Lcid, IsDefault.IsPresent ? IsDefault.ToBool() : true);
            ClientContext.ExecuteQueryRetry();
            WriteObject(label);
        }
    }
}
