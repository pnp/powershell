using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class UserPipeBind
    {
        private readonly int _id;
        private readonly string _loginOrName;
        private readonly User _user;

        public UserPipeBind()
        {
            _id = 0;
            _loginOrName = null;
            _user = null;
        }

        public UserPipeBind(int id)
        {
            _id = id;
        }

        public UserPipeBind(string id)
        {
            if (!int.TryParse(id, out _id))
            {
                _loginOrName = id;
            }
        }

        public UserPipeBind(User user)
        {
            _user = user;
        }

        public User GetUser(ClientContext context, bool ensure = false, Expression<Func<User, object>>[] retrievalOptions = null)
        {
            // note: the following code to get the user is copied from Remove-PnPUser - it could be put into a utility class
            var additionalRetrievalOptions = new List<Expression<Func<User, object>>>()
            {
                u => u.Id,
                u => u.LoginName,
                u => u.Email,
                u => u.Title
            };

            if(retrievalOptions != null)
            {
                additionalRetrievalOptions.AddRange(retrievalOptions);
            }

            User user = null;
            if (_user != null)
            {
                user = _user;
                context.Load(user, additionalRetrievalOptions.ToArray());
                context.ExecuteQueryRetry();
            }
            else if (_id > 0)
            {
                user = context.Web.GetUserById(_id);
                context.Load(user, additionalRetrievalOptions.ToArray());
                context.ExecuteQueryRetry();
            }
            else if (!string.IsNullOrWhiteSpace(_loginOrName))
            {
                try
                {
                    user = context.Web.SiteUsers.GetByLoginName(_loginOrName);
                    context.Load(user, additionalRetrievalOptions.ToArray());
                    context.ExecuteQueryRetry();
                }
                catch
                {
                    var userQuery = context.LoadQuery(context.Web.SiteUsers.Where(u => u.Title == _loginOrName).IncludeWithDefaultProperties(additionalRetrievalOptions.ToArray()));
                    context.ExecuteQueryRetry();
                    user = userQuery.FirstOrDefault();
                    if(user == null && ensure)
                    {
                        user = context.Web.EnsureUser(_loginOrName);
                        context.ExecuteQueryRetry();
                    }
                }
            }
            return user;
        }
    }
}
