using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Get, "PnPTerm")]
    public class GetTerm : PnPRetrievalsCmdlet<Term>
    {
        private const string ParameterSet_TERM = "By Term Id";
        private const string ParameterSet_TERMSET = "By Termset";
        private Term term;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_TERM)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TERMSET)]
        public GenericObjectNameIdPipeBind<TermSet> Identity;

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_TERMSET)]
        public TaxonomyItemPipeBind<TermSet> TermSet;

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_TERMSET)]
        public TermGroupPipeBind TermGroup;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TERM)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TERMSET)]
        public GenericObjectNameIdPipeBind<TermStore> TermStore;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TERMSET)]
        public SwitchParameter Recursive;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter IncludeChildTerms;

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
                if (TermStore.StringValue != null)
                {
                    termStore = taxonomySession.TermStores.GetByName(TermStore.StringValue);
                }
                else if (TermStore.IdValue != Guid.Empty)
                {
                    termStore = taxonomySession.TermStores.GetById(TermStore.IdValue);
                }
                else
                {
                    if (TermStore.Item != null)
                    {
                        termStore = TermStore.Item;
                    }
                }
            }

            if (ParameterSetName == ParameterSet_TERM)
            {
                if (Identity.IdValue != Guid.Empty)
                {
                    term = termStore.GetTerm(Identity.IdValue);
                    ClientContext.Load(term, RetrievalExpressions);
                    ClientContext.ExecuteQueryRetry();
                    if (IncludeChildTerms.IsPresent && term.TermsCount > 0)
                    {
                        LoadChildTerms(term);
                    }
                    WriteObject(term);
                } else
                {
                    WriteError(new ErrorRecord(new Exception("Insufficient parameters"), "INSUFFICIENTPARAMETERS", ErrorCategory.SyntaxError, this));
                }
            }
            else
            {
                TermGroup termGroup = null;

                if (TermGroup != null && TermGroup.Id != Guid.Empty)
                {
                    termGroup = termStore.Groups.GetById(TermGroup.Id);
                }
                else if (TermGroup != null && !string.IsNullOrEmpty(TermGroup.Name))
                {
                    termGroup = termStore.Groups.GetByName(TermGroup.Name);
                }

                TermSet termSet = null;
                if (TermSet != null)
                {
                    if (TermSet.Id != Guid.Empty)
                    {
                        termSet = termGroup.TermSets.GetById(TermSet.Id);
                    }
                    else if (!string.IsNullOrEmpty(TermSet.Title))
                    {
                        termSet = termGroup.TermSets.GetByName(TermSet.Title);
                    }
                    else
                    {
                        termSet = TermSet.Item;
                    }
                }
                if (Identity != null)
                {
                    term = null;
                    if (Identity.IdValue != Guid.Empty)
                    {
                        term = termStore.GetTerm(Identity.IdValue);
                    }
                    else
                    {
                        var termName = TaxonomyExtensions.NormalizeName(Identity.StringValue);
                        if (!Recursive)
                        {
                            term = termSet.Terms.GetByName(termName);
                        }
                        else
                        {
                            var lmi = new LabelMatchInformation(ClientContext)
                            {
                                TrimUnavailable = true,
                                TermLabel = termName
                            };

                            var termMatches = termSet.GetTerms(lmi);
                            ClientContext.Load(termMatches);
                            ClientContext.ExecuteQueryRetry();

                            if (termMatches.AreItemsAvailable)
                            {
                                term = termMatches.FirstOrDefault();
                            }
                        }
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