using System;
using System.Collections.Generic;
using System.Linq;
using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Utilities.EntraID
{
    /// <summary>
    /// Contains functionality towards users in Entra ID
    /// </summary>
    internal static class UsersUtility
    {
        #region Users

        /// <summary>
        /// Returns users from Entra ID with a Delta Token which can be used to retrieve only those users who have had changes done to them since the delta token was given out
        /// </summary>
        /// <param name="accessToken">Access Token which allows requests to be made</param>
        /// <param name="deltaToken">A delta token to query for changes done since the delta token was given out</param>
        /// <param name="filter">Optional filters to query for only specific users</param>
        /// <param name="orderby">Optional sort order instructions</param>
        /// <param name="selectProperties">Optional additional properties to fetch for the users</param>
        /// <param name="startIndex">Optional start index indicating starting from which result to start returning users</param>
        /// <param name="endIndex">Optional end index indicating up to which result to return users. By default all users will be returned.</param>
        /// <param name="useBetaEndPoint">Indicates if the v1.0 (false) or beta (true) endpoint should be used at Microsoft Graph to query for the data</param>
        /// <returns>UserDelta instance</returns>
        public static UserDelta ListUserDelta(string accessToken, string deltaToken, string filter, string orderby, string[] selectProperties = null, int startIndex = 0, int? endIndex = null, bool useBetaEndPoint = false)
        {
            var userDelta = Framework.Graph.UsersUtility.ListUserDelta(accessToken, deltaToken, filter, orderby, selectProperties, startIndex, endIndex, useBetaEndPoint: useBetaEndPoint);

            var result = new UserDelta
            {
                DeltaToken = userDelta.DeltaToken,
                Users = userDelta.Users.Select(User.CreateFrom).ToList()
            };
            return result;
        }

        /// <summary>
        /// Returns all the Users in the current domain filtered out with a custom OData filter
        /// </summary>
        /// <param name="accessToken">The OAuth 2.0 Access Token to use for invoking the Microsoft Graph</param>
        /// <param name="filter">OData filter to apply to retrieval of the users from the Microsoft Graph</param>
        /// <param name="orderby">OData orderby instruction</param>
        /// <param name="selectProperties">Allows providing the names of properties to return regarding the users. If not provided, the standard properties will be returned.</param>
        /// <param name="ignoreDefaultProperties">If set to true, only the properties provided through selectProperties will be loaded. The default properties will not be. Optional. Default is that the default properties will always be retrieved.</param>
        /// <param name="startIndex">First item in the results returned by Microsoft Graph to return</param>
        /// <param name="endIndex">Last item in the results returned by Microsoft Graph to return. Provide NULL to return all results that exist.</param>
        /// <param name="useBetaEndPoint">Indicates if the v1.0 (false) or beta (true) endpoint should be used at Microsoft Graph to query for the data</param>
        /// <returns>List with User objects</returns>
        public static List<User> ListUsers(string accessToken, string filter, string orderby, string[] selectProperties = null, bool ignoreDefaultProperties = false, int startIndex = 0, int? endIndex = 999, bool useBetaEndPoint = false)
        {
            return Framework.Graph.UsersUtility.ListUsers(accessToken, filter, orderby, selectProperties, startIndex, endIndex, ignoreDefaultProperties: ignoreDefaultProperties, useBetaEndPoint: useBetaEndPoint).Select(User.CreateFrom).ToList();
        }

        /// <summary>
        /// Returns the user with the provided <paramref name="userId"/> from Entra ID
        /// </summary>
        /// <param name="accessToken">The OAuth 2.0 Access Token to use for invoking the Microsoft Graph</param>
        /// <param name="userId">The unique identifier of the user in Entra ID to return</param>    
        /// <param name="selectProperties">Allows providing the names of properties to return regarding the users. If not provided, the standard properties will be returned.</param>
        /// <param name="ignoreDefaultProperties">If set to true, only the properties provided through selectProperties will be loaded. The default properties will not be. Optional. Default is that the default properties will always be retrieved.</param>
        /// <param name="startIndex">First item in the results returned by Microsoft Graph to return</param>
        /// <param name="endIndex">Last item in the results returned by Microsoft Graph to return. Provide NULL to return all results that exist.</param>
        /// <param name="useBetaEndPoint">Indicates if the v1.0 (false) or beta (true) endpoint should be used at Microsoft Graph to query for the data</param>
        /// <returns>List with User objects</returns>
        public static User GetUser(string accessToken, Guid userId, string[] selectProperties = null, bool ignoreDefaultProperties = false, int startIndex = 0, int? endIndex = 999, bool useBetaEndPoint = false)
        {
            return Framework.Graph.UsersUtility.ListUsers(accessToken, $"id eq '{userId}'", null, selectProperties, startIndex, endIndex, ignoreDefaultProperties: ignoreDefaultProperties, useBetaEndPoint: useBetaEndPoint).Select(User.CreateFrom).FirstOrDefault();
        }

        /// <summary>
        /// Returns the user with the provided <paramref name="userPrincipalName"/> from Entra ID
        /// </summary>
        /// <param name="accessToken">The OAuth 2.0 Access Token to use for invoking the Microsoft Graph</param>
        /// <param name="userPrincipalName">The User Principal Name of the user in Entra ID to return</param>
        /// <param name="selectProperties">Allows providing the names of properties to return regarding the users. If not provided, the standard properties will be returned.</param>
        /// <param name="ignoreDefaultProperties">If set to true, only the properties provided through selectProperties will be loaded. The default properties will not be. Optional. Default is that the default properties will always be retrieved.</param>
        /// <param name="startIndex">First item in the results returned by Microsoft Graph to return</param>
        /// <param name="endIndex">Last item in the results returned by Microsoft Graph to return. Provide NULL to return all results that exist.</param>
        /// <param name="useBetaEndPoint">Indicates if the v1.0 (false) or beta (true) endpoint should be used at Microsoft Graph to query for the data</param>
        /// <returns>User object</returns>
        public static User GetUser(string accessToken, string userPrincipalName, string[] selectProperties = null, bool ignoreDefaultProperties = false, int startIndex = 0, int? endIndex = 999, bool useBetaEndPoint = false)
        {
            return Framework.Graph.UsersUtility.ListUsers(accessToken, $"userPrincipalName eq '{userPrincipalName}'", null, selectProperties, startIndex, endIndex, ignoreDefaultProperties: ignoreDefaultProperties, useBetaEndPoint: useBetaEndPoint).Select(User.CreateFrom).FirstOrDefault();
        }

        #endregion
    }
}