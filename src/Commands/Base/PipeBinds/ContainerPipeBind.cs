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
        /// Matches a url carrying the id of a container: a SharePoint api url such as https://contoso-admin.sharepoint.com/_api/v2.1/storageContainers/{containerId},
        /// or a Microsoft Graph url such as https://graph.microsoft.com/v1.0/storage/fileStorage/containers/{containerId}. The id ends at the next path segment, query or fragment.
        /// </summary>
        private static readonly Regex ContainerIdUrl = new(@"/(?:_api/v2\.1/storageContainers|storage/fileStorage/(?:containers|deletedContainers))/(?<id>[^/?#]*)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

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
            if (string.IsNullOrWhiteSpace(idOrUrl))
                throw new ArgumentException("Url or ID null or empty.", nameof(idOrUrl));

            idOrUrl = idOrUrl.Trim();
            if (Uri.TryCreate(idOrUrl, UriKind.Absolute, out _))
            {
                var idUrl = ContainerIdUrl.Match(idOrUrl);
                if (idUrl.Success)
                {
                    _id = Uri.UnescapeDataString(idUrl.Groups["id"].Value);
                    if (string.IsNullOrWhiteSpace(_id))
                        throw new ArgumentException($"The container url '{idOrUrl}' does not contain a container id.", nameof(idOrUrl));
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

        public ContainerPipeBind(Model.Graph.FileStorage.FileStorageContainer container)
        {
            _id = container.Id;
        }

        public ContainerPipeBind(SPContainerUser user)
        {
            _id = user.ContainerId;
        }

        /// <summary>
        /// Gets the id of the container in this pipebind, looking it up by its site url when the pipebind was created from one
        /// </summary>
        /// <param name="tenant">Tenant instance to use to look up the container by its site url</param>
        /// <returns>Id of the container, or null when no container exists at the site url</returns>
        public string GetContainerId(Tenant tenant)
        {
            return _id ?? GetContainer(tenant)?.ContainerId;
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
