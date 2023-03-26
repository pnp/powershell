using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Branding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class NavigationNodePipeBind
    {
        private int _id;
        private NavigationNode _node;

        public NavigationNodePipeBind(int id)
        {
            _id = id;
        }

        public NavigationNodePipeBind(NavigationNode node)
        {
            _node = node;
        }

        internal NavigationNode GetNavigationNode(Web web)
        {
            NavigationNode node = null;
            if (_node != null)
            {
                node = _node;
            } else {
                node = web.Navigation.GetNodeById(_id);
            }

            if (node != null)
            {
                web.Context.Load(node);
                web.Context.ExecuteQueryRetry();
            }

            return node;
        }
    }
}
