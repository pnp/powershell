using PnP.Framework;
using PnP.PowerShell.Commands.Model.AzureAD;
using System;
using System.Net;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    /// <summary>
    /// Allows passing an Azure Active Directory User from one cmdlet to another
    /// </summary>
    public class AzureADUserPipeBind
    {
        private readonly User _user;
        private readonly Guid? _userId;
        private readonly string _upn;

        public AzureADUserPipeBind()
        {
        }

        public AzureADUserPipeBind(User user)
        {
            _user = user;
        }

        public AzureADUserPipeBind(string input)
        {
            Guid idValue;
            if (Guid.TryParse(input, out idValue))
            {
                _userId = idValue;
            }
            else
            {
                _upn = input;
            }
        }

        /// <summary>
        /// Instance of an Azure Active Directory User
        /// </summary>
        public User User => _user;

        /// <summary>
        /// User Principal Name (UPN) of the user
        /// </summary>
        public string Upn => _upn;

        /// <summary>
        /// GUID of the user account
        /// </summary>
        public Guid? UserId => _userId;

        /// <summary>
        /// Tries to return the User instace based on the information this pipe has available
        /// </summary>
        /// <param name="accessToken">Access Token for Microsoft Graph that can be used to fetch User data</param>
        /// <param name="azureEnvironment">Azure environment cloud</param>
        /// <returns>User instance or NULL if unable to define user instance based on the available information</returns>
        public User GetUser(string accessToken, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        {
            if (_user != null)
            {
                return _user;
            }
            if (_userId.HasValue)
            {
                return User.CreateFrom(Framework.Graph.UsersUtility.GetUser(accessToken, _userId.Value, azureEnvironment: azureEnvironment));
            }
            if (_upn != null)
            {                
                return User.CreateFrom(Framework.Graph.UsersUtility.GetUser(accessToken, WebUtility.UrlEncode(_upn), azureEnvironment: azureEnvironment));
            }
            return null;
        }
    }
}
