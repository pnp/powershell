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
        public static IEnumerable<ServiceUpdateMessage> GetServiceUpdateMessages(Cmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            var collection = GraphHelper.GetResultCollection<ServiceUpdateMessage>(cmdlet, connection, $"v1.0/admin/serviceAnnouncement/messages", accessToken);            
            return collection;
        }

        /// <summary>
        /// Retrieves a specific Service Update Message
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="connection">Connection to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns><see cref="ServiceUpdateMessage"> containing the requested information</returns>
        public static ServiceUpdateMessage GetServiceUpdateMessageById(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            var item = GraphHelper.Get<ServiceUpdateMessage>(cmdlet, connection, $"v1.0/admin/serviceAnnouncement/messages/{id}", accessToken);
            return item;
        }

        /// <summary>
        /// Sets a specific Service Update Message as read
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsReadById(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            return SetServiceUpdateMessageAsReadById(cmdlet, new [] { id }, connection, accessToken);
        }

        /// <summary>
        /// Sets specific Service Update Messages as read
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsReadById(Cmdlet cmdlet, string[] id, PnPConnection connection, string accessToken)
        {
            var postBody = new PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = GraphHelper.Post<PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody>(cmdlet, connection, "v1.0/admin/serviceAnnouncement/messages/markRead", postBody, accessToken);
            return true;
        }

        /// <summary>
        /// Sets a specific Service Update Message as unread
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsUnreadById(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            return SetServiceUpdateMessageAsUnreadById(cmdlet, new [] { id }, connection, accessToken);
        }

        /// <summary>
        /// Sets specific Service Update Messages as unread
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsUnreadById(Cmdlet cmdlet, string[] id, PnPConnection connection, string accessToken)
        {
            var postBody = new PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = GraphHelper.Post<PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody>(cmdlet, connection, "v1.0/admin/serviceAnnouncement/messages/markUnread", postBody, accessToken);
            return true;
        }       

        /// <summary>
        /// Sets a specific Service Update Message as archived
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsArchivedById(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            return SetServiceUpdateMessageAsArchivedById(cmdlet, new [] { id }, connection, accessToken);
        }

        /// <summary>
        /// Sets specific Service Update Messages as archived
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsArchivedById(Cmdlet cmdlet, string[] id, PnPConnection connection, string accessToken)
        {
            var postBody = new ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = GraphHelper.Post(cmdlet, connection, "v1.0/admin/serviceAnnouncement/messages/archive", postBody, accessToken);
            return true;
        }

        /// <summary>
        /// Sets a specific Service Update Message as unarchived
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsUnarchivedById(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            return SetServiceUpdateMessageAsUnarchivedById(cmdlet, new [] { id }, connection, accessToken);
        }

        /// <summary>
        /// Sets specific Service Update Messages as unarchived
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsUnarchivedById(Cmdlet cmdlet, string[] id, PnPConnection connection, string accessToken)
        {
            var postBody = new ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = GraphHelper.Post(cmdlet, connection, "v1.0/admin/serviceAnnouncement/messages/unarchive", postBody, accessToken);
            return true;
        }

        /// <summary>
        /// Sets a specific Service Update Message as being a favorite
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsFavoriteById(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            return SetServiceUpdateMessageAsFavoriteById(cmdlet, new [] { id }, connection, accessToken);
        }

        /// <summary>
        /// Sets specific Service Update Messages as being favorites
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsFavoriteById(Cmdlet cmdlet, string[] id, PnPConnection connection, string accessToken)
        {
            var postBody = new PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = GraphHelper.Post<PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody>(cmdlet, connection, "v1.0/admin/serviceAnnouncement/messages/favorite", postBody, accessToken);
            return true;
        }    

        /// <summary>
        /// Removes a specific Service Update Message as being a favorite
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsNotfavoriteById(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            return SetServiceUpdateMessageAsNotfavoriteById(cmdlet, new [] { id }, connection, accessToken);
        }

        /// <summary>
        /// Removes specific Service Update Messages from being favorites
        /// </summary>
        /// <param name="id">List with identifiers of the service update messages</param>
        /// <param name="httpClient">HttpClient to use for updating the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>Boolean indicating whether the request succeeded</returns>
        public static bool SetServiceUpdateMessageAsNotfavoriteById(Cmdlet cmdlet, string[] id, PnPConnection connection, string accessToken)
        {
            var postBody = new PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody { MessageIds = id };
            var item = GraphHelper.Post<PnP.PowerShell.Commands.Model.ServiceHealth.ServiceUpdateMessageReadStatusBody>(cmdlet, connection, "v1.0/admin/serviceAnnouncement/messages/unfavorite", postBody, accessToken);
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
        public static IEnumerable<ServiceHealthIssue> GetServiceHealthIssues(Cmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            var collection = GraphHelper.GetResultCollection<ServiceHealthIssue>(cmdlet, connection, $"v1.0/admin/serviceAnnouncement/issues", accessToken);            
            return collection;
        }

        /// <summary>
        /// Retrieves a specific Service Health Issue
        /// </summary>
        /// <param name="id">Identifier of the service health issue</param>
        /// <param name="connection">Connection to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns><see cref="ServiceHealthIssue"> containing the requested information</returns>
        public static ServiceHealthIssue GetServiceHealthIssueById(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            var item = GraphHelper.Get<ServiceHealthIssue>(cmdlet, connection, $"v1.0/admin/serviceAnnouncement/issues/{id}", accessToken);
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
        public static IEnumerable<ServiceHealthCurrent> GetServiceCurrentHealth(Cmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            var collection = GraphHelper.GetResultCollection<ServiceHealthCurrent>(cmdlet, connection, $"v1.0/admin/serviceAnnouncement/healthOverviews", accessToken);            
            return collection;
        }

        /// <summary>
        /// Retrieves the current health of a specific service
        /// </summary>
        /// <param name="id">Full name of the service, i.e. Microsoft Forms</param>
        /// <param name="connection">Connection to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns><see cref="ServiceHealthIssue"> containing the requested information</returns>
        public static ServiceHealthCurrent GetServiceCurrentHealthById(Cmdlet cmdlet, string id, PnPConnection connection, string accessToken)
        {
            var item = GraphHelper.Get<ServiceHealthCurrent>(cmdlet, connection, $"v1.0/admin/serviceAnnouncement/healthOverviews/{id}", accessToken);
            return item;
        }        

        #endregion                 
    }
}
