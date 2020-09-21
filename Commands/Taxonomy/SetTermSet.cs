using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.Framework.Extensions;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Set, "PnPTermSet", SupportsShouldProcess = false)]
    public class SetTermSet : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false)]
        public GenericObjectNameIdPipeBind<TermSet> Identity;

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public TermGroupPipeBind TermGroup;

        [Parameter(Mandatory = false)]
        public GenericObjectNameIdPipeBind<TermStore> TermStore;

        [Parameter(Mandatory = false)]
        public string Name;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public string Owner;

        [Parameter(Mandatory = false)]
        public string Contact;

        [Parameter(Mandatory = false)]
        public Hashtable CustomProperties;

        [Parameter(Mandatory = false)]
        public string StakeholderToAdd;

        [Parameter(Mandatory = false)]
        public string StakeholderToDelete;

        [Parameter(Mandatory = false)]
        public bool IsAvailableForTagging;

        [Parameter(Mandatory = false)]
        public bool IsOpenForTermCreation;

        [Parameter(Mandatory = false)]
        public bool UseForSiteNavigation;

        [Parameter(Mandatory = false)]
        public bool UseForFacetedNavigation;

        [Parameter(Mandatory = false)]
        public string SetTargetPageForTerms;

        [Parameter(Mandatory = false)]
        public SwitchParameter RemoveTargetPageforTerms;

        [Parameter(Mandatory = false)]
        public string SetCatalogItemPageForCategories;

        [Parameter(Mandatory = false)]
        public SwitchParameter RemoveCatalogItemPageForCategories;
        protected override void ExecuteCmdlet()
        {
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

            TermGroup termGroup = null;

            if (TermGroup.Id != Guid.Empty)
            {
                termGroup = termStore.Groups.GetById(TermGroup.Id);
            }
            else if (!string.IsNullOrEmpty(TermGroup.Name))
            {
                termGroup = termStore.Groups.GetByName(TermGroup.Name);
            }
            if (Identity != null)
            {
                var termSet = default(TermSet);
                if (Identity.IdValue != Guid.Empty)
                {
                    termSet = termGroup.TermSets.GetById(Identity.IdValue);
                }
                else
                {
                    termSet = termGroup.TermSets.GetByName(Identity.StringValue);
                }

                ClientContext.Load(termSet, t => t.CustomProperties);
                ClientContext.ExecuteQueryRetry();
                if (termSet.ServerObjectIsNull.Value == false)
                {
                    bool updateRequired = false;
                    if (ParameterSpecified(nameof(Name)))
                    {
                        termSet.Name = Name;
                        updateRequired = true;
                    }
                    if (ParameterSpecified(nameof(Description)))
                    {
                        termSet.Description = Description;
                        updateRequired = true;
                    }
                    if (ParameterSpecified(nameof(Owner)))
                    {
                        termSet.Owner = Owner;
                        updateRequired = true;
                    }
                    if (ParameterSpecified(nameof(Contact)))
                    {
                        termSet.Contact = Contact;
                        updateRequired = true;
                    }
                    if (ParameterSpecified(nameof(CustomProperties)))
                    {
                        var enumerator = termSet.CustomProperties.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            var prop = enumerator.Current;
                            if (!prop.Key.StartsWith("_Sys_"))
                            {
                                termSet.DeleteCustomProperty(prop.Key);
                            }
                        }

                        foreach (var entry in CustomProperties.Cast<DictionaryEntry>().ToDictionary(kvp => (string)kvp.Key, kvp => (string)kvp.Value))
                        {
                            termSet.SetCustomProperty(entry.Key, entry.Value);
                        }
                        updateRequired = true;
                    }
                    if (ParameterSpecified(nameof(StakeholderToAdd)))
                    {
                        termSet.AddStakeholder(StakeholderToAdd);
                        updateRequired = true;
                    }
                    if (ParameterSpecified(nameof(StakeholderToDelete)))
                    {
                        termSet.DeleteStakeholder(StakeholderToDelete);
                        updateRequired = true;
                    }
                    if (ParameterSpecified(nameof(IsAvailableForTagging)))
                    {
                        termSet.IsAvailableForTagging = IsAvailableForTagging;
                        updateRequired = true;
                    }
                    if (ParameterSpecified(nameof(IsOpenForTermCreation)))
                    {
                        termSet.IsOpenForTermCreation = IsOpenForTermCreation;
                        updateRequired = true;
                    }
                    if (ParameterSpecified(nameof(UseForSiteNavigation)))
                    {
                        if (UseForSiteNavigation)
                        {
                            if (!termSet.CustomProperties.ContainsKey("_Sys_Nav_IsNavigationTermSet"))
                            {
                                termSet.SetCustomProperty("_Sys_Nav_IsNavigationTermSet", "True");
                                updateRequired = true;
                            }
                        }
                        else
                        {
                            if (termSet.CustomProperties.ContainsKey("_Sys_Nav_IsNavigationTermSet"))
                            {
                                termSet.DeleteCustomProperty("_Sys_Nav_IsNavigationTermSet");
                                updateRequired = true;
                            }
                        }
                    }
                    if (ParameterSpecified(nameof(UseForFacetedNavigation)))
                    {
                        if (UseForFacetedNavigation)
                        {
                            if (!termSet.CustomProperties.ContainsKey("_Sys_Facet_IsFacetedTermSet"))
                            {
                                termSet.SetCustomProperty("_Sys_Facet_IsFacetedTermSet", "True");
                                updateRequired = true;
                            }
                        }
                        else
                        {
                            if (termSet.CustomProperties.ContainsKey("_Sys_Facet_IsFacetedTermSet"))
                            {
                                termSet.DeleteCustomProperty("_Sys_Facet_IsFacetedTermSet");
                                updateRequired = true;
                            }
                        }
                    }
                    if (ParameterSpecified(nameof(SetTargetPageForTerms)) && ParameterSpecified(nameof(RemoveTargetPageforTerms)))
                    {
                        throw new PSArgumentException("Cannot both set and remove the target page for this termset. Either use SetTargetPageForTerms or RemoveTargetPageForTerms");
                    }
                    else
                    {
                        if (ParameterSpecified(nameof(SetTargetPageForTerms)))
                        {
                            termSet.SetCustomProperty("_Sys_Nav_TargetUrlForChildTerms", SetTargetPageForTerms);
                            updateRequired = true;
                        }
                        if(ParameterSpecified(nameof(RemoveTargetPageforTerms)))
                        {
                            termSet.DeleteCustomProperty("_Sys_Nav_TargetUrlForChildTerms");
                            updateRequired = true;
                        }
                    }

                    if (ParameterSpecified(nameof(SetCatalogItemPageForCategories)) && ParameterSpecified(nameof(RemoveCatalogItemPageForCategories)))
                    {
                        throw new PSArgumentException("Cannot both set and remove the catalog page for this termset. Either use SetCatalogItemPageForCategories or RemoveCatalogItemPageForCategories");
                    }
                    else
                    {
                        if (ParameterSpecified(nameof(SetCatalogItemPageForCategories)))
                        {
                            termSet.SetCustomProperty("_Sys_Nav_CatalogTargetUrlForChildTerms", SetTargetPageForTerms);
                            updateRequired = true;
                        }
                        if (ParameterSpecified(nameof(RemoveCatalogItemPageForCategories)))
                        {
                            termSet.DeleteCustomProperty("_Sys_Nav_CatalogTargetUrlForChildTerms");
                            updateRequired = true;
                        }
                    }

                    if (updateRequired)
                    {
                        termStore.CommitAll();
                        ClientContext.ExecuteQuery();
                    }
                }
                else
                {
                    throw new PSArgumentException("Cannot find termset");
                }
            }
        }

    }
}
