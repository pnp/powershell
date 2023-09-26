using PnP.PowerShell.Commands.Model.AzureAD;
using System;
using System.Net;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    /// <summary>
    /// Allows passing an Entra ID User from one cmdlet to another
    /// </summary>
    public class EntraIDUserPipeBind
    {
        private readonly User _user;
        private readonly string _userId;
        private readonly string _upn;

        public EntraIDUserPipeBind()
        {
        }

        public EntraIDUserPipeBind(User user)
        {
            _user = user;
        }

        public EntraIDUserPipeBind(string input)
        {
            Guid idValue;
            if (Guid.TryParse(input, out idValue))
            {
                _userId = input;
            }
            else
            {
                _upn = input;
            }
        }

        /// <summary>
        /// Instance of an Entra ID User
        /// </summary>
        public User User => _user;

        /// <summary>
        /// User Principal Name (UPN) of the user
        /// </summary>
        public string Upn => _upn;

        /// <summary>
        /// GUID of the user account
        /// </summary>
        public string UserId => _userId;

        /// <summary>
        /// Tries to return the User instace based on the information this pipe has available
        /// </summary>
        /// <param name="accessToken">Access Token for Microsoft Graph that can be used to fetch User data</param>
        /// <returns>User instance or NULL if unable to define user instance based on the available information</returns>
        public User GetUser(string accessToken)
        {
            if (_user != null)
            {
                return _user;
            }
            if (_userId != null)
            {
                return User.CreateFrom(PnP.Framework.Graph.UsersUtility.GetUser(accessToken, _userId));
            }
            if (_upn != null)
            {                
                return User.CreateFrom(PnP.Framework.Graph.UsersUtility.GetUser(accessToken, WebUtility.UrlEncode(_upn)));
            }
            return null;
        }
    }
}
