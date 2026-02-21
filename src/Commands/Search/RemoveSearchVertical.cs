using System.Management.Automation;
using System.Net;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

namespace PnP.PowerShell.Commands.Search
{
	[Cmdlet(VerbsCommon.Remove, "PnPSearchVertical", SupportsShouldProcess = true)]
	[RequiredApiDelegatedPermissions("https://gcs.office.com/ExternalConnection.ReadWrite.All")]
	public class RemoveSearchVertical : PnPGcsCmdlet
	{
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[Alias("LogicalId")]
		public string Identity;

		[Parameter(Mandatory = false)]
		public SearchVerticalScope Scope = SearchVerticalScope.Site;

		[Parameter(Mandatory = false)]
		public SwitchParameter Force;

		protected override void ExecuteCmdlet()
		{
			if (!ShouldProcess($"Search vertical '{Identity}' at {Scope} scope", "Remove"))
				return;

			if (!Force && !ShouldContinue($"Remove search vertical '{Identity}' at {Scope} scope?", "Confirm"))
				return;

			var headers = GetGcsHeaders();

			string url;
			if (Scope == SearchVerticalScope.Organization)
			{
				url = GetGcsOrgVerticalUrl($"/{Identity}");
			}
			else
			{
				var siteId = GetCurrentSiteId();
				url = GetGcsVerticalUrl(siteId, $"/{Identity}");
			}

			LogDebug($"Removing search vertical with Identity '{Identity}' at {Scope} scope");

			DeleteWithRetry(url, headers, verifySuccess: () =>
			{
				try
				{
					GcsRequestHelper.Get<SearchVertical>(url, additionalHeaders: headers);
					return false; // Still exists
				}
				catch (GraphException ex) when (ex.HttpResponse?.StatusCode == HttpStatusCode.NotFound)
				{
					return true; // 404 = Gone
				}
			});
		}
	}
}
