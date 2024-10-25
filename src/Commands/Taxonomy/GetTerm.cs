using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq.Expressions;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Get, "PnPTerm")]
    public class GetTerm : PnPRetrievalsCmdlet<Term>
    {
        private const string ParameterSet_TERMID = "By Term Id";
        private const string ParameterSet_TERMNAME = "By Term Name";
        private Term term;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_TERMID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TERMNAME)]
        public TaxonomyTermPipeBind Identity;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMNAME)]
        public TaxonomyTermSetPipeBind TermSet;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_TERMNAME)]
        public TaxonomyTermGroupPipeBind TermGroup;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TERMID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TERMNAME)]
        public TaxonomyTermStorePipeBind TermStore;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TERMNAME)]
        public SwitchParameter Recursive;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter IncludeChildTerms;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TERMNAME)]
        public TaxonomyTermPipeBind ParentTerm;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TERMNAME)]
        public SwitchParameter IncludeDeprecated;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<Term, object>>[] { g => g.Name, g => g.TermsCount, g => g.Id };
            var taxonomySession = TaxonomySession.GetTaxonomySession(ClientContext);
            // Get Term Store
            TermStore termStore = null;
            if (TermStore == null)
            {
                termStore = taxonomySession.GetDefaultSiteCollectionTermStore();
            }
            else
            {
                termStore = TermStore.GetTermStore(taxonomySession);
            }

            if (ParameterSetName == ParameterSet_TERMID)
            {
                if (Identity.Id != Guid.Empty)
                {
                    term = termStore.GetTerm(Identity.Id);
                    ClientContext.Load(term, RetrievalExpressions);
                    ClientContext.ExecuteQueryRetry();
                    if (IncludeChildTerms.IsPresent && term.TermsCount > 0)
                    {
                        LoadChildTerms(term);
                    }
                    WriteObject(term);
                }
                else
                {
                    throw new PSArgumentException("Insufficient Parameters specified to determine the term to retrieve");
                }
            }
            else
            {
                TermGroup termGroup = TermGroup.GetGroup(termStore);
                TermSet termSet = TermSet.GetTermSet(termGroup);

                if (Identity != null && ParentTerm == null)
                {
                    var term = Identity.GetTerm(ClientContext, termStore, termSet, Recursive, RetrievalExpressions, IncludeDeprecated);

                    if (IncludeChildTerms.IsPresent && term.TermsCount > 0)
                    {
                        LoadChildTerms(term);
                    }
                    WriteObject(term);
                }
                else if (Identity != null && ParentTerm != null)
                {
                    var parentTerm = ParentTerm.GetTerm(ClientContext, termStore, termSet, Recursive, RetrievalExpressions, IncludeDeprecated);
                    LoadChildTerms(parentTerm);

                    Term term = null;
                    if (Identity.Id != Guid.Empty)
                    {
                        term = parentTerm.Terms.GetById(Identity.Id);
                    }
                    else
                    {
                        term = parentTerm.Terms.GetByName(Identity.Title);
                    }

                    ClientContext.Load(term, RetrievalExpressions);
                    ClientContext.ExecuteQueryRetry();

                    if (IncludeChildTerms.IsPresent && term.TermsCount > 0)
                    {
                        LoadChildTerms(term);
                    }
                    WriteObject(term);
                }
                else
                {
                    var query = termSet.Terms.IncludeWithDefaultProperties(RetrievalExpressions);
                    var terms = ClientContext.LoadQuery(query);
                    ClientContext.ExecuteQueryRetry();
                    if (IncludeChildTerms.IsPresent)
                    {
                        foreach (var collectionTerm in terms)
                        {
                            if (collectionTerm.TermsCount > 0)
                            {
                                LoadChildTerms(collectionTerm);
                            }
                        }
                    }
                    WriteObject(terms, true);
                }
            }
        }

        private void LoadChildTerms(Term incomingTerm)
        {
            ClientContext.Load(incomingTerm.Terms, ts => ts.IncludeWithDefaultProperties(RetrievalExpressions));
            ClientContext.ExecuteQueryRetry();
            foreach (var childTerm in incomingTerm.Terms)
            {
                if (childTerm.TermsCount > 0)
                {
                    LoadChildTerms(childTerm);
                }
            }
        }
    }
}