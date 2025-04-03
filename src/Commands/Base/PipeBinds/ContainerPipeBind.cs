using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using System;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class ContainerPipeBind
    {
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
        /// Creates a new ContainerPipeBind based on the Url of a container or the Id of a container
        /// </summary>
        /// <param name="identity">Url or Id of a container</param>
        public string Id => _id;
        
        public ContainerPipeBind(string idOrUrl)
        {
            if (string.IsNullOrEmpty(idOrUrl))
                throw new ArgumentException("Url or ID null or empty.", nameof(idOrUrl));

            Uri uriResult;
            if (Uri.TryCreate(idOrUrl, UriKind.Absolute, out uriResult))
            {
                _url = idOrUrl;
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

        /// <summary>
        /// Gets the ContainerProperties of the container in this pipebind
        /// </summary>
        /// <param name="tenant">Tenant instance to use to retrieve the ContainerProperties in this pipe bind</param>
        /// <exception cref="Exception">Thrown when the ContainerProperties cannot be retrieved</exception>
        /// <returns>ContainerProperties of the container in this pipebind</returns>
        public SPContainerProperties GetContainer(Tenant tenant)
        {
            if(_ContainerProperties != null)
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