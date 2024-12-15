using PnP.PowerShell.Commands.Model.ServiceHealth;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Collections.Generic;

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
        public static IEnumerable<ServiceUpdateMessage> GetServiceUpdateMessages(ApiRequestHelper requestHelper)
        {
            var collection = requestHelper.GetResultCollection<ServiceUpdateMessage>($"v1.0/admin/serviceAnnouncement/messages");
            return collection;
        }

        /// <summary>
        /// Retrieves a specific Service Update Message
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="connection">Connection to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns><see cref="ServiceUpdateMessage"> containing the requested information</returns>
        public static ServiceUpdateMessage GetServiceUpdateMessageById(ApiRequestHelper requestHelper, string id)
        {
            var item = requestHelper.Get<ServiceUpdateMessage>($"v1.0/admin/serviceAnnouncement/messages/{id}");
            return item;
        }

        /// <summary>
        /// Sets a specific Service Update Message as read
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsReadById(ApiRequestHelper requestHelper, string id)
        {
            return SetServiceUpdateMessageAsReadById(requestHelper, new[] { id });
        }

        /// <summary>
        /// Sets specific Service Update Messages as read
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsReadById(ApiRequestHelper requestHelper, string[] id)
        {
            var postBody = new PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = requestHelper.Post<PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody>("v1.0/admin/serviceAnnouncement/messages/markRead", postBody);
            return true;
        }

        /// <summary>
        /// Sets a specific Service Update Message as unread
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsUnreadById(ApiRequestHelper requestHelper, string id)
        {
            return SetServiceUpdateMessageAsUnreadById(requestHelper, new[] { id });
        }

        /// <summary>
        /// Sets specific Service Update Messages as unread
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsUnreadById(ApiRequestHelper requestHelper, string[] id)
        {
            var postBody = new PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = requestHelper.Post<PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody>("v1.0/admin/serviceAnnouncement/messages/markUnread", postBody);
            return true;
        }

        /// <summary>
        /// Sets a specific Service Update Message as archived
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsArchivedById(ApiRequestHelper requestHelper, string id)
        {
            return SetServiceUpdateMessageAsArchivedById(requestHelper, new[] { id });
        }

        /// <summary>
        /// Sets specific Service Update Messages as archived
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsArchivedById(ApiRequestHelper requestHelper, string[] id)
        {
            var postBody = new ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = requestHelper.Post("v1.0/admin/serviceAnnouncement/messages/archive", postBody);
            return true;
        }

        /// <summary>
        /// Sets a specific Service Update Message as unarchived
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsUnarchivedById(ApiRequestHelper requestHelper, string id)
        {
            return SetServiceUpdateMessageAsUnarchivedById(requestHelper, new[] { id });
        }

        /// <summary>
        /// Sets specific Service Update Messages as unarchived
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsUnarchivedById(ApiRequestHelper requestHelper, string[] id)
        {
            var postBody = new ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = requestHelper.Post("v1.0/admin/serviceAnnouncement/messages/unarchive", postBody);
            return true;
        }

        /// <summary>
        /// Sets a specific Service Update Message as being a favorite
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsFavoriteById(ApiRequestHelper requestHelper, string id)
        {
            return SetServiceUpdateMessageAsFavoriteById(requestHelper, new[] { id });
        }

        /// <summary>
        /// Sets specific Service Update Messages as being favorites
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsFavoriteById(ApiRequestHelper requestHelper, string[] id)
        {
            var postBody = new PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = requestHelper.Post<PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody>("v1.0/admin/serviceAnnouncement/messages/favorite", postBody);
            return true;
        }

        /// <summary>
        /// Removes a specific Service Update Message as being a favorite
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsNotfavoriteById(ApiRequestHelper requestHelper, string id)
        {
            return SetServiceUpdateMessageAsNotfavoriteById(requestHelper, new[] { id });
        }

        /// <summary>
        /// Removes specific Service Update Messages from being favorites
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsNotfavoriteById(ApiRequestHelper requestHelper, string[] id)
        {
            var postBody = new PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = requestHelper.Post<PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody>("v1.0/admin/serviceAnnouncement/messages/unfavorite", postBody);
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
        public static IEnumerable<ServiceHealthIssue> GetServiceHealthIssues(ApiRequestHelper requestHelper)
        {
            var collection = requestHelper.GetResultCollection<ServiceHealthIssue>($"v1.0/admin/serviceAnnouncement/issues");
            return collection;
        }

        /// <summary>
        /// Retrieves a specific Service Health Issue
        /// </summary>
        /// <param name="id">Identifier of the service health issue</param>
        /// <param name="connection">Connection to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns><see cref="ServiceHealthIssue"> containing the requested information</returns>
        public static ServiceHealthIssue GetServiceHealthIssueById(ApiRequestHelper requestHelper, string id)
        {
            var item = requestHelper.Get<ServiceHealthIssue>($"v1.0/admin/serviceAnnouncement/issues/{id}");
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
        public static IEnumerable<ServiceHealthCurrent> GetServiceCurrentHealth(ApiRequestHelper requestHelper)
        {
            var collection = requestHelper.GetResultCollection<ServiceHealthCurrent>($"v1.0/admin/serviceAnnouncement/healthOverviews");
            return collection;
        }

        /// <summary>
        /// Retrieves the current health of a specific service
        /// </summary>
        /// <param name="id">Full name of the service, i.e. Microsoft Forms</param>
        /// <param name="connection">Connection to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns><see cref="ServiceHealthIssue"> containing the requested information</returns>
        public static ServiceHealthCurrent GetServiceCurrentHealthById(ApiRequestHelper requestHelper, string id)
        {
            var item = requestHelper.Get<ServiceHealthCurrent>($"v1.0/admin/serviceAnnouncement/healthOverviews/{id}");
            return item;
        }

        #endregion
    }
}
