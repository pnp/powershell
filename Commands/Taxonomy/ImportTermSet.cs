using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;


namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsData.Import, "PnPTermSet")]
    public class ImportTermSet : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true)]
        public string GroupName;

        [Parameter(Mandatory = true)]
        public string Path;

        [Parameter(Mandatory = false)]
        public Guid TermSetId = default(Guid);

        [Parameter(Mandatory = false)]
        public SwitchParameter SynchronizeDeletions;

        [Parameter(Mandatory = false)]
        public bool? IsOpen;

        [Parameter(Mandatory = false)]
        public string Contact;

        [Parameter(Mandatory = false)]
        public string Owner;

        [Parameter(Mandatory = false)]
        public string TermStoreName;

        protected override void ExecuteCmdlet()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }

            var taxonomySession = TaxonomySession.GetTaxonomySession(ClientContext);
            // Get Term Store
            var termStore = default(TermStore);
            if (string.IsNullOrEmpty(TermStoreName))
            {
                termStore = taxonomySession.GetDefaultSiteCollectionTermStore();
            }
            else
            {
                termStore = taxonomySession.TermStores.GetByName(TermStoreName);
            }
            // Get Group
            var group = termStore.GetTermGroupByName(GroupName);
            // Import
            var termSet = default(TermSet);
            if (group != null) {
                termSet = group.ImportTermSet(Path, TermSetId, SynchronizeDeletions, IsOpen, Contact, Owner);
            }
            else
            {
                throw new Exception("Group does not exist.");
            }

            WriteObject(termSet);
        }
    }
}
