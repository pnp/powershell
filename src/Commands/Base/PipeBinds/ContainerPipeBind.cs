using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model.SharePoint;
using System;
using System.Text.RegularExpressions;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class ContainerPipeBind
    {
        /// <summary>
        /// Matches the path of a container api url, such as https://contoso.sharepoint.com/_api/v2.1/storageContainers/{containerId}
        /// </summary>
        private static readonly Regex ContainerApiUrlPath = new(@"/_api/v2\.1/storageContainers/", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        /// <summary>
        /// Id of the container
        /// </summary>
        private readonly string _id;

        /// <summary>
        /// Url of the container
        /// </summary>
        private readonly string _url;

        /// <summary>
        /// ContainerProperties of the container
        /// </summary>
        private SPContainerProperties _ContainerProperties;

        /// <summary>
        /// Id of the container, when the pipebind was created from an id, a container api url or a container object
        /// </summary>
        public string Id => _id;

        /// <summary>
        /// Site url of the container, when the pipebind was created from one
        /// </summary>
        public string Url => _url;

        /// <summary>
        /// Creates a new ContainerPipeBind based on the site url, the api url or the id of a container
        /// </summary>
        /// <param name="idOrUrl">Site url, api url or id of a container</param>
        public ContainerPipeBind(string idOrUrl)
        {
            if (string.IsNullOrEmpty(idOrUrl))
                throw new ArgumentException("Url or ID null or empty.", nameof(idOrUrl));

            if (Uri.TryCreate(idOrUrl, UriKind.Absolute, out _))
            {
                var apiUrlParts = ContainerApiUrlPath.Split(idOrUrl);
                if (apiUrlParts.Length == 2)
                {
                    _id = apiUrlParts[1].TrimEnd('/');
                    if (string.IsNullOrWhiteSpace(_id))
                        throw new ArgumentException($"The container api url '{idOrUrl}' does not contain a container id.", nameof(idOrUrl));
                }
                else
                {
                    _url = idOrUrl;
                }
            }
            else
            {
                _id = idOrUrl;
            }
        }

        public ContainerPipeBind(SPContainerProperties properties)
        {
            _id = properties.ContainerId;
            _ContainerProperties = properties;
        }

        public ContainerPipeBind(SPConsumingTenantContainerByIdentity container)
        {
            _id = container.ContainerId;
        }

        /// <summary>
        /// Gets the ContainerProperties of the container in this pipebind
        /// </summary>
        /// <param name="tenant">Tenant instance to use to retrieve the ContainerProperties in this pipe bind</param>
        /// <param name="refresh">Retrieves the ContainerProperties from the server even when the pipebind was created from them, so changes are not made to stale or previously modified properties</param>
        /// <exception cref="Exception">Thrown when the ContainerProperties cannot be retrieved</exception>
        /// <returns>ContainerProperties of the container in this pipebind</returns>
        public SPContainerProperties GetContainer(Tenant tenant, bool refresh = false)
        {
            if(_ContainerProperties != null && !refresh)
            {
                return _ContainerProperties;
            }
            else if(_id != null)
            {
                var containerProperties = tenant.GetSPOContainerByContainerId(_id);
                 (tenant.Context as ClientContext).ExecuteQueryRetry();
                return containerProperties.Value;
            }
            else if(_url != null)
            {
                var containerProperties = tenant.GetSPOContainerByContainerSiteUrl(_url);
                 (tenant.Context as ClientContext).ExecuteQueryRetry();
                return containerProperties.Value;
            }
            throw new Exception(Resources.ContainerNotFound);
        }
    }
}
