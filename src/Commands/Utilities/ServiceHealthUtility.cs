using PnP.PowerShell.Commands.Model.ServiceHealth;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class ServiceHealthUtility
    {
        #region Service Update Message

        /// <summary>
        /// Retrieves all Service Update Messages
        /// </summary>
        /// <param name="httpClient">HttpClient to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>List with <see cref="ServiceUpdateMessage"> objects</returns>
        public static async Task<IEnumerable<ServiceUpdateMessage>> GetServiceUpdateMessagesAsync(HttpClient httpClient, string accessToken)
        {
            var collection = await GraphHelper.GetResultCollectionAsync<ServiceUpdateMessage>(httpClient, $"v1.0/admin/serviceAnnouncement/messages", accessToken);
            return collection;
        }

        /// <summary>
        /// Retrieves a specific Service Update Message
        /// </summary>
        /// <param name="id">Identifier of the service update message</param>
        /// <param name="httpClient">HttpClient to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns><see cref="ServiceUpdateMessage"> containing the requested information</returns>
        public static async Task<ServiceUpdateMessage> GetServiceUpdateMessageByIdAsync(string id, HttpClient httpClient, string accessToken)
        {
            var item = await GraphHelper.GetAsync<ServiceUpdateMessage>(httpClient, $"v1.0/admin/serviceAnnouncement/messages/{id}", accessToken);
            return item;
        }               

        #endregion       

        #region Service Health Issue

        /// <summary>
        /// Retrieves all Service Health Issues
        /// </summary>
        /// <param name="httpClient">HttpClient to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>List with <see cref="ServiceHealthIssue"> objects</returns>
        public static async Task<IEnumerable<ServiceHealthIssue>> GetServiceHealthIssuesAsync(HttpClient httpClient, string accessToken)
        {
            var collection = await GraphHelper.GetResultCollectionAsync<ServiceHealthIssue>(httpClient, $"v1.0/admin/serviceAnnouncement/issues", accessToken);           
            return collection;
        }

        /// <summary>
        /// Retrieves a specific Service Health Issue
        /// </summary>
        /// <param name="id">Identifier of the service health issue</param>
        /// <param name="httpClient">HttpClient to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns><see cref="ServiceHealthIssue"> containing the requested information</returns>
        public static async Task<ServiceHealthIssue> GetServiceHealthIssueByIdAsync(string id, HttpClient httpClient, string accessToken)
        {
            var item = await GraphHelper.GetAsync<ServiceHealthIssue>(httpClient, $"v1.0/admin/serviceAnnouncement/issues/{id}", accessToken);
            return item;
        }

        #endregion

        #region Service Current Health

        /// <summary>
        /// Retrieves the current health of all services
        /// </summary>
        /// <param name="httpClient">HttpClient to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns>List with <see cref="ServiceHealthCurrent"> objects</returns>
        public static async Task<IEnumerable<ServiceHealthCurrent>> GetServiceCurrentHealthAsync(HttpClient httpClient, string accessToken)
        {
            var collection = await GraphHelper.GetResultCollectionAsync<ServiceHealthCurrent>(httpClient, $"v1.0/admin/serviceAnnouncement/healthOverviews", accessToken);            
            return collection;
        }

        /// <summary>
        /// Retrieves the current health of a specific service
        /// </summary>
        /// <param name="id">Full name of the service, i.e. Microsoft Forms</param>
        /// <param name="httpClient">HttpClient to use for retrieval of the data</param>
        /// <param name="accessToken">AccessToken to use for authentication of the request</param>
        /// <returns><see cref="ServiceHealthIssue"> containing the requested information</returns>
        public static async Task<ServiceHealthCurrent> GetServiceCurrentHealthByIdAsync(string id, HttpClient httpClient, string accessToken)
        {
            var item = await GraphHelper.GetAsync<ServiceHealthCurrent>(httpClient, $"v1.0/admin/serviceAnnouncement/healthOverviews/{id}", accessToken);
            return item;
        }        

        #endregion                 
    }
}
