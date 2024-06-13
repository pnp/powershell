using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.ServiceHealth;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class ServiceHealthUtility
    {
        #region Service Update Message

        /// <summary>
        /// Retrieves all Service Update Messages
        /// </summary>
        /// <param name="connection">Connection to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>List with <see cref="ServiceUpdateMessage"> objects</returns>
        public static async Task<IEnumerable<ServiceUpdateMessage>> GetServiceUpdateMessagesAsync(Cmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            var collection = await GraphHelper.GetResultCollectionAsync<ServiceUpdateMessage>(cmdlet, connection, $"v1.0/admin/serviceAnnouncement/messages", accessToken);            
            return collection;
        }

        /// <summary>
        /// Retrieves a specific Service Update Message
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="connection">Connection to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns><see cref="ServiceUpdateMessage"> containing the requested information</returns>
        public static async Task<ServiceUpdateMessage> GetServiceUpdateMessageByIdAsync(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            var item = await GraphHelper.GetAsync<ServiceUpdateMessage>(cmdlet, connection, $"v1.0/admin/serviceAnnouncement/messages/{id}", accessToken);
            return item;
        }

        /// <summary>
        /// Sets a specific Service Update Message as read
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static async Task<bool> SetServiceUpdateMessageAsReadByIdAsync(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            return await SetServiceUpdateMessageAsReadByIdAsync(cmdlet, new [] { id }, connection, accessToken);
        }

        /// <summary>
        /// Sets specific Service Update Messages as read
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static async Task<bool> SetServiceUpdateMessageAsReadByIdAsync(Cmdlet cmdlet, string[] id, PnPConnection connection, string accessToken)
        {
            var postBody = new PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = await GraphHelper.PostAsync<PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody>(cmdlet, connection, "v1.0/admin/serviceAnnouncement/messages/markRead", postBody, accessToken);
            return true;
        }

        /// <summary>
        /// Sets a specific Service Update Message as unread
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static async Task<bool> SetServiceUpdateMessageAsUnreadByIdAsync(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            return await SetServiceUpdateMessageAsUnreadByIdAsync(cmdlet, new [] { id }, connection, accessToken);
        }

        /// <summary>
        /// Sets specific Service Update Messages as unread
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static async Task<bool> SetServiceUpdateMessageAsUnreadByIdAsync(Cmdlet cmdlet, string[] id, PnPConnection connection, string accessToken)
        {
            var postBody = new PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = await GraphHelper.PostAsync<PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody>(cmdlet, connection, "v1.0/admin/serviceAnnouncement/messages/markUnread", postBody, accessToken);
            return true;
        }       

        /// <summary>
        /// Sets a specific Service Update Message as archived
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static async Task<bool> SetServiceUpdateMessageAsArchivedByIdAsync(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            return await SetServiceUpdateMessageAsArchivedByIdAsync(cmdlet, new [] { id }, connection, accessToken);
        }

        /// <summary>
        /// Sets specific Service Update Messages as archived
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static async Task<bool> SetServiceUpdateMessageAsArchivedByIdAsync(Cmdlet cmdlet, string[] id, PnPConnection connection, string accessToken)
        {
            var postBody = new ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = await GraphHelper.PostAsync(cmdlet, connection, "v1.0/admin/serviceAnnouncement/messages/archive", postBody, accessToken);
            return true;
        }

        /// <summary>
        /// Sets a specific Service Update Message as unarchived
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static async Task<bool> SetServiceUpdateMessageAsUnarchivedByIdAsync(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            return await SetServiceUpdateMessageAsUnarchivedByIdAsync(cmdlet, new [] { id }, connection, accessToken);
        }

        /// <summary>
        /// Sets specific Service Update Messages as unarchived
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static async Task<bool> SetServiceUpdateMessageAsUnarchivedByIdAsync(Cmdlet cmdlet, string[] id, PnPConnection connection, string accessToken)
        {
            var postBody = new ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = await GraphHelper.PostAsync(cmdlet, connection, "v1.0/admin/serviceAnnouncement/messages/unarchive", postBody, accessToken);
            return true;
        }

        /// <summary>
        /// Sets a specific Service Update Message as being a favorite
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static async Task<bool> SetServiceUpdateMessageAsFavoriteByIdAsync(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            return await SetServiceUpdateMessageAsFavoriteByIdAsync(cmdlet, new [] { id }, connection, accessToken);
        }

        /// <summary>
        /// Sets specific Service Update Messages as being favorites
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static async Task<bool> SetServiceUpdateMessageAsFavoriteByIdAsync(Cmdlet cmdlet, string[] id, PnPConnection connection, string accessToken)
        {
            var postBody = new PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = await GraphHelper.PostAsync<PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody>(cmdlet, connection, "v1.0/admin/serviceAnnouncement/messages/favorite", postBody, accessToken);
            return true;
        }    

        /// <summary>
        /// Removes a specific Service Update Message as being a favorite
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static async Task<bool> SetServiceUpdateMessageAsNotfavoriteByIdAsync(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            return await SetServiceUpdateMessageAsNotfavoriteByIdAsync(cmdlet, new [] { id }, connection, accessToken);
        }

        /// <summary>
        /// Removes specific Service Update Messages from being favorites
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static async Task<bool> SetServiceUpdateMessageAsNotfavoriteByIdAsync(Cmdlet cmdlet, string[] id, PnPConnection connection, string accessToken)
        {
            var postBody = new PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = await GraphHelper.PostAsync<PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody>(cmdlet, connection, "v1.0/admin/serviceAnnouncement/messages/unfavorite", postBody, accessToken);
            return true;
        }

        #endregion       

        #region Service Health Issue

        /// <summary>
        /// Retrieves all Service Health Issues
        /// </summary>
        /// <param name="connection">Connection to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>List with <see cref="ServiceHealthIssue"> objects</returns>
        public static async Task<IEnumerable<ServiceHealthIssue>> GetServiceHealthIssuesAsync(Cmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            var collection = await GraphHelper.GetResultCollectionAsync<ServiceHealthIssue>(cmdlet, connection, $"v1.0/admin/serviceAnnouncement/issues", accessToken);            
            return collection;
        }

        /// <summary>
        /// Retrieves a specific Service Health Issue
        /// </summary>
        /// <param name="id">Identifier of the service health issue</param>
        /// <param name="connection">Connection to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns><see cref="ServiceHealthIssue"> containing the requested information</returns>
        public static async Task<ServiceHealthIssue> GetServiceHealthIssueByIdAsync(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            var item = await GraphHelper.GetAsync<ServiceHealthIssue>(cmdlet, connection, $"v1.0/admin/serviceAnnouncement/issues/{id}", accessToken);
            return item;
        }

        #endregion

        #region Service Current Health

        /// <summary>
        /// Retrieves the current health of all services
        /// </summary>
        /// <param name="connection">Connection to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>List with <see cref="ServiceHealthCurrent"> objects</returns>
        public static async Task<IEnumerable<ServiceHealthCurrent>> GetServiceCurrentHealthAsync(Cmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            var collection = await GraphHelper.GetResultCollectionAsync<ServiceHealthCurrent>(cmdlet, connection, $"v1.0/admin/serviceAnnouncement/healthOverviews", accessToken);            
            return collection;
        }

        /// <summary>
        /// Retrieves the current health of a specific service
        /// </summary>
        /// <param name="id">Full name of the service, i.e. Microsoft Forms</param>
        /// <param name="connection">Connection to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns><see cref="ServiceHealthIssue"> containing the requested information</returns>
        public static async Task<ServiceHealthCurrent> GetServiceCurrentHealthByIdAsync(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            var item = await GraphHelper.GetAsync<ServiceHealthCurrent>(cmdlet, connection, $"v1.0/admin/serviceAnnouncement/healthOverviews/{id}", accessToken);
            return item;
        }        

        #endregion                 
    }
}
