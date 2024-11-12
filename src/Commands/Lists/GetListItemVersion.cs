using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PnP.Core.QueryModel;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPListItemVersion")]
    [OutputType(typeof(List<ListItemVersion>))]
    public class GetListItemVersion : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(Connection.PnPContext);

            if (list is null)
            {
                throw new PSArgumentException($"Cannot find the list provided through -{nameof(List)}", nameof(List));
            }

            var item = Identity.GetListItem(list);

            if (item is null)
            {
                throw new PSArgumentException($"Cannot find the list item provided through -{nameof(Identity)}", nameof(Identity));
            }

            item.EnsureProperties(i => i.All, i => i.Versions);
            var itemVersionCollection = item.Versions.AsRequested();
            if (itemVersionCollection is not null && itemVersionCollection.Count() > 0)
            {
                List<ListItemVersion> listItemVersionCollection = new List<ListItemVersion>();
                foreach (var version in itemVersionCollection)
                {
                    var listItemVersion = new ListItemVersion();
                    listItemVersion.Id = version.Id;
                    listItemVersion.IsCurrentVersion = version.IsCurrentVersion;
                    listItemVersion.Created = version.Created;
                    listItemVersion.CreatedBy = version.CreatedBy;
                    listItemVersion.Fields = version.Fields.AsRequested();                    
                    listItemVersion.Values = version.Values;
                    listItemVersion.VersionLabel = version.VersionLabel;
                    listItemVersionCollection.Add(listItemVersion);
                }

                WriteObject(listItemVersionCollection, true);
            }
        }
    }
}