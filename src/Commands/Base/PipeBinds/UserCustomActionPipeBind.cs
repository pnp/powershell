using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    /// <summary>
    /// Allows passing UserCustomAction objects in pipelines
    /// </summary>
    public sealed class UserCustomActionPipeBind
    {
        private Microsoft.SharePoint.Client.UserCustomAction _userCustomAction;
        private IUserCustomAction _coreUserCustomAction;
        private Guid? _id;
        private string _name;

        public UserCustomActionPipeBind(Guid guid)
        {
            _id = guid;
        }

        public UserCustomActionPipeBind(Microsoft.SharePoint.Client.UserCustomAction userCustomAction)
        {
            _userCustomAction = userCustomAction;
        }

        public UserCustomActionPipeBind(IUserCustomAction userCustomAction)
        {
            _coreUserCustomAction = userCustomAction;
        }

        /// <summary>
        /// Accepts a name or id to be passed in
        /// </summary>
        /// <param name="id">Name or id of the UserCustomAction</param>
        public UserCustomActionPipeBind(string id)
        {
            // Added Guid checking first for backwards compatibility
            if (Guid.TryParse(id, out Guid result))
            {
                _id = result;
            }
            else
            {
                _name = id;
            }
        }

                public IEnumerable<IUserCustomAction> GetCustomActions(PnPContext context, CustomActionScope scope)
        {
            if (_coreUserCustomAction != null)
            {
                return new List<IUserCustomAction> { _coreUserCustomAction };
            }
            if (_userCustomAction != null)
            {
                switch (_userCustomAction.Scope)
                {
                    case Microsoft.SharePoint.Client.UserCustomActionScope.Web:
                        {
                            return new List<IUserCustomAction> { context.Web.UserCustomActions.Where(ca => ca.Id == _userCustomAction.Id).FirstOrDefault() };
                        }
                        case Microsoft.SharePoint.Client.UserCustomActionScope.Site:
                        {
                            return new List<IUserCustomAction> { context.Site.UserCustomActions.Where(ca => ca.Id == _userCustomAction.Id).FirstOrDefault() };
                        }

                }
            }
            var customActions = new List<IUserCustomAction>();

            if (scope == CustomActionScope.Web || scope == CustomActionScope.All)
            {
                customActions.AddRange(context.Web.UserCustomActions.ToList());
            }
            if (scope == CustomActionScope.Site || scope == CustomActionScope.All)
            {
                customActions.AddRange(context.Site.UserCustomActions.ToList());
            }
            if (_id != null)
            {
                return customActions.Where(ca => ca.Id == _id.Value);
            }
            if (!string.IsNullOrEmpty(_name))
            {
                return customActions.Where(ca => ca.Name == _name);
            }
            return null;
        }
    }
}