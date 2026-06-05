using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq.Expressions;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Lists
{
	public abstract class ListVersionPolicyCmdletBase : PnPSharePointOnlineAdminCmdlet
	{
		[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
		[ValidateNotNull]
		[ArgumentCompleter(typeof(ListNameCompleter))]
		public ListPipeBind Identity;

		[Parameter(Mandatory = false)]
		public SitePipeBind Site;

		protected List GetListOrWarn(params Expression<Func<List, object>>[] retrievals)
		{
			var targetWebUrl = GetTargetWebUrl();
			if (string.Equals(targetWebUrl, Connection.Url.TrimEnd('/'), StringComparison.OrdinalIgnoreCase))
			{
				return Identity.GetListOrWarn(this, ClientContext.Web, retrievals);
			}

			using var targetWebContext = Connection.CloneContext(targetWebUrl);
			return Identity.GetListOrWarn(this, targetWebContext.Web, retrievals);
		}

		protected string GetTargetSiteUrl()
		{
			if (!ParameterSpecified(nameof(Site)))
			{
				return ClientContext.Site.EnsureProperty(s => s.Url).TrimEnd('/');
			}

			if (Site.Site != null)
			{
				Site.Site.EnsureProperty(s => s.Url);
				return Site.Site.Url.TrimEnd('/');
			}

			if (!string.IsNullOrEmpty(Site.Url))
			{
				using var targetWebContext = Connection.CloneContext(Site.Url.TrimEnd('/'));
				targetWebContext.Load(targetWebContext.Site, s => s.Url);
				targetWebContext.ExecuteQueryRetry();
				return targetWebContext.Site.Url.TrimEnd('/');
			}

			if (Site.Id != Guid.Empty)
			{
				var siteProperties = Tenant.GetSitePropertiesById(Site.Id, false, Connection.TenantAdminUrl);
				if (siteProperties == null || string.IsNullOrEmpty(siteProperties.Url))
				{
					throw new PSArgumentException("The specified site could not be resolved.", nameof(Site));
				}

				return siteProperties.Url.TrimEnd('/');
			}

			throw new PSArgumentException("The Site parameter must be a valid site object, site id, or absolute site url.", nameof(Site));
		}

		private string GetTargetWebUrl()
		{
			if (!ParameterSpecified(nameof(Site)))
			{
				return Connection.Url.TrimEnd('/');
			}

			if (Site.Site != null)
			{
				Site.Site.EnsureProperty(s => s.Url);
				return Site.Site.Url.TrimEnd('/');
			}

			if (!string.IsNullOrEmpty(Site.Url))
			{
				return Site.Url.TrimEnd('/');
			}

			if (Site.Id != Guid.Empty)
			{
				var siteProperties = Tenant.GetSitePropertiesById(Site.Id, false, Connection.TenantAdminUrl);
				if (siteProperties == null || string.IsNullOrEmpty(siteProperties.Url))
				{
					throw new PSArgumentException("The specified site could not be resolved.", nameof(Site));
				}

				return siteProperties.Url.TrimEnd('/');
			}

			throw new PSArgumentException("The Site parameter must be a valid site object, site id, or absolute site url.", nameof(Site));
		}
	}
}