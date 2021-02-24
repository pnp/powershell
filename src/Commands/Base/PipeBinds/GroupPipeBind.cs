using System.Linq;
using Microsoft.SharePoint.Client;
using PnP.Core.Model.Security;
using PnP.Core.Services;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class GroupPipeBind
    {
        private readonly int _id = -1;
        private readonly Group _group;
        private readonly ISharePointGroup _sharePointGroup;
        private readonly string _name;
        public int Id => _id;

        public Group Group => _group;

        public string Name => _name;

        internal GroupPipeBind()
        {
        }

        public GroupPipeBind(int id)
        {
            _id = id;
        }

        public GroupPipeBind(Group group)
        {
            _group = group;
        }

        public GroupPipeBind(string name)
        {
            _name = name;
        }

        public GroupPipeBind(ISharePointGroup group)
        {
            _sharePointGroup = group;
        }

        internal ISharePointGroup GetGroup(PnPContext context)
        {
            ISharePointGroup group = null;
            if (Group != null)
            {
                Group.EnsureProperty(g => g.Id);
                group = context.Web.SiteGroups.Where(g => g.Id == Group.Id).FirstOrDefault();
            }
            if(_sharePointGroup != null)
            {
                group = _sharePointGroup;
            }
            else if (Id != -1)
            {
                group = context.Web.SiteGroups.Where(g => g.Id == Id).FirstOrDefault();
            }
            else if (!string.IsNullOrEmpty(Name))
            {
                group = context.Web.SiteGroups.Where(g => g.Title == Name && g.LoginName == Name).FirstOrDefault();
            }
            return group;
        }
        
        internal Group GetGroup(Web web)
        {
            var clientContext = web.Context;

            Group group = null;
            if (Id != -1)
            {
                group = web.SiteGroups.GetById(Id);
            }
            else if (!string.IsNullOrEmpty(Name))
            {
                group = web.SiteGroups.GetByName(Name);
            }
            else if (Group != null)
            {
                group = Group;
                clientContext = group.Context;
            }

            clientContext.Load(group);
            clientContext.Load(group.Users);
            clientContext.ExecuteQueryRetry();
            return group;
        }
    }
}
