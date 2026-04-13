using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Files
{
	[Cmdlet(VerbsCommon.Set, "PnPFolderArchiveState")]
	[OutputType(typeof(FolderArchiveStateResult))]
	[ApiNotAvailableUnderApplicationPermissions]
	[RequiredApiDelegatedPermissions("graph/Files.Read")]
	[RequiredApiDelegatedPermissions("graph/Files.Read.All")]
	[RequiredApiDelegatedPermissions("graph/Files.ReadWrite")]
	[RequiredApiDelegatedPermissions("graph/Files.ReadWrite.All")]
	public class SetFolderArchiveState : PnPGraphCmdlet
	{
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
		public FolderPipeBind Identity;

		[Parameter(Mandatory = true, Position = 1)]
		public FolderArchiveState ArchiveState;

		[Parameter(Mandatory = false)]
		public SwitchParameter Force;

		protected override void ExecuteCmdlet()
		{
			var folder = Identity.GetFolder(PnPContext, f => f.Name, f => f.ServerRelativeUrl, f => f.ListItemAllFields);
			if (folder == null)
			{
				throw new PSInvalidOperationException("The provided identity does not resolve to a folder.");
			}

			folder.EnsureProperties(f => f.Name, f => f.ServerRelativeUrl, f => f.ListItemAllFields);

			if (string.IsNullOrWhiteSpace(folder.ServerRelativeUrl))
			{
				throw new PSInvalidOperationException($"Unable to resolve the server relative URL for folder '{folder.Name}'.");
			}

			ValidateSupportedFolder(folder);

			switch (ArchiveState)
			{
				case FolderArchiveState.Archived:
					EnsureRequiredFolderArchivePermissions(FolderArchiveState.Archived);
					WriteVerbose("Archiving a folder is an asynchronous operation. Microsoft Graph returns a monitor URL that can be used to track completion.");
					if (Force || ShouldContinue($"Change the archive state of folder '{folder.Name}' to Archived?", Properties.Resources.Confirm))
					{
						WriteObject(ArchiveFolder(folder));
					}
					break;
				case FolderArchiveState.Active:
					EnsureRequiredFolderArchivePermissions(FolderArchiveState.Active);
					WriteVerbose("Reactivating a folder is an asynchronous operation. Microsoft Graph returns a monitor URL that can be used to track completion.");
					if (Force || ShouldContinue($"Change the archive state of folder '{folder.Name}' to Active?", Properties.Resources.Confirm))
					{
						WriteObject(UnarchiveFolder(folder));
					}
					break;
				default:
					throw new InvalidOperationException("OperationAborted");
			}
		}

		private void ValidateSupportedFolder(IFolder folder)
		{
			folder.EnsureProperties(f => f.ListItemAllFields);

			var listItem = folder.ListItemAllFields;
			if (listItem == null)
			{
				throw new PSInvalidOperationException($"Folder '{folder.Name}' must be located in a document library in the current web.");
			}

			listItem.EnsureProperties(item => item.ParentList);
			if (listItem.ParentList == null)
			{
				throw new PSInvalidOperationException($"Folder '{folder.Name}' must be located in a document library in the current web.");
			}

			listItem.ParentList.EnsureProperties(list => list.TemplateType, list => list.RootFolder);
			if (listItem.ParentList.TemplateType != ListTemplateType.DocumentLibrary)
			{
				throw new PSInvalidOperationException($"Folder '{folder.Name}' must be located in a document library in the current web.");
			}

			if (listItem.ParentList.RootFolder == null)
			{
				throw new PSInvalidOperationException($"Unable to resolve the document library root folder for folder '{folder.Name}'.");
			}

			PnPContext.Web.EnsureProperties(web => web.ServerRelativeUrl);
			listItem.ParentList.RootFolder.EnsureProperties(rootFolder => rootFolder.ServerRelativeUrl);

			if (!IsDocumentLibraryInCurrentWeb(listItem.ParentList.RootFolder.ServerRelativeUrl, PnPContext.Web.ServerRelativeUrl))
			{
				throw new PSInvalidOperationException($"Folder '{folder.Name}' must be located in a document library in the current web.");
			}
		}

		private bool IsDocumentLibraryInCurrentWeb(string listRootFolderServerRelativeUrl, string currentWebServerRelativeUrl)
		{
			if (string.IsNullOrWhiteSpace(listRootFolderServerRelativeUrl) || string.IsNullOrWhiteSpace(currentWebServerRelativeUrl))
			{
				return false;
			}

			var normalizedCurrentWebServerRelativeUrl = currentWebServerRelativeUrl.Length > 1
				? currentWebServerRelativeUrl.TrimEnd('/')
				: currentWebServerRelativeUrl;

			if (!listRootFolderServerRelativeUrl.StartsWith(normalizedCurrentWebServerRelativeUrl, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}

			var relativeListRootFolderUrl = listRootFolderServerRelativeUrl.Substring(normalizedCurrentWebServerRelativeUrl.Length).Trim('/');
			return !string.IsNullOrWhiteSpace(relativeListRootFolderUrl) && !relativeListRootFolderUrl.Contains('/');
		}

		private sealed class GraphDriveItemReference
		{
			public string DriveId { get; init; }

			public string ItemId { get; init; }
		}

		private void EnsureRequiredFolderArchivePermissions(FolderArchiveState requestedState)
		{
			var graphAccessToken = AccessToken;
			if (string.IsNullOrWhiteSpace(graphAccessToken))
			{
				throw new PSInvalidOperationException("Unable to acquire a Microsoft Graph access token required to validate folder archive state permissions.");
			}

			if (TokenHandler.RetrieveTokenType(graphAccessToken) != IdType.Delegate)
			{
				throw new PSInvalidOperationException("Changing the folder archive state is only supported with a delegated Microsoft Graph access token.");
			}

			var availableScopes = TokenHandler.ReturnScopes(graphAccessToken)
				.Where(scope => scope.ResourceType == ResourceTypeName.Graph)
				.Select(scope => scope.Scope)
				.ToArray();

			var requiredScopes = requestedState == FolderArchiveState.Archived
				? new[] { "Files.ReadWrite", "Files.Read", "Files.Read.All", "Files.ReadWrite.All" }
				: new[] { "Files.Read", "Files.Read.All" };

			if (!availableScopes.Any(scope => requiredScopes.Contains(scope, StringComparer.InvariantCultureIgnoreCase)))
			{
				throw new PSInvalidOperationException($"Current access token lacks one of the required delegated Microsoft Graph permission scopes for changing the folder archive state to {requestedState}: {string.Join(", ", requiredScopes)}.");
			}
		}

		private FolderArchiveStateResult ArchiveFolder(IFolder folder)
		{
			var graphDriveItemReference = ResolveGraphDriveItemReference(folder);
			var requestUrl = $"beta/drives/{graphDriveItemReference.DriveId}/items/{graphDriveItemReference.ItemId}/archive";

			LogDebug($"Sending folder archive request to '{requestUrl}'");

			using var response = GraphRequestHelper.PostHttpContent(requestUrl, null, GetAsyncHeaders());
			if (response.StatusCode != HttpStatusCode.Accepted)
			{
				throw new PSInvalidOperationException($"Archiving folder '{folder.Name}' returned unexpected status code {(int)response.StatusCode} {response.StatusCode}. Expected 202 Accepted for a folder.");
			}

			return CreateResult(folder, FolderArchiveState.Archived, response.Headers.Location);
		}

		private FolderArchiveStateResult UnarchiveFolder(IFolder folder)
		{
			var graphDriveItemReference = ResolveGraphDriveItemReference(folder);
			var requestUrl = $"beta/drives/{graphDriveItemReference.DriveId}/items/{graphDriveItemReference.ItemId}/unarchive";

			LogDebug($"Sending folder unarchive request to '{requestUrl}'");

			using var response = GraphRequestHelper.PostHttpContent(requestUrl, null, GetAsyncHeaders());
			if (response.StatusCode != HttpStatusCode.Accepted)
			{
				throw new PSInvalidOperationException($"Unarchiving folder '{folder.Name}' returned unexpected status code {(int)response.StatusCode} {response.StatusCode}. Expected 202 Accepted for a folder.");
			}

			return CreateResult(folder, FolderArchiveState.Active, response.Headers.Location);
		}

		private GraphDriveItemReference ResolveGraphDriveItemReference(IFolder folder)
		{
			folder.EnsureProperties(f => f.ListItemAllFields);

			var listItem = folder.ListItemAllFields;
			if (listItem == null)
			{
				throw new PSInvalidOperationException($"Unable to resolve the SharePoint list item metadata for folder '{folder.Name}'.");
			}

			listItem.EnsureProperties(item => item.Id, item => item.ParentList);

			if (listItem.Id <= 0)
			{
				throw new PSInvalidOperationException($"Unable to resolve the SharePoint list item metadata for folder '{folder.Name}'.");
			}

			if (listItem.ParentList == null)
			{
				throw new PSInvalidOperationException($"Unable to resolve the SharePoint parent list for folder '{folder.Name}'.");
			}

			listItem.ParentList.EnsureProperties(list => list.Id);

			if (listItem.ParentList.Id == Guid.Empty)
			{
				throw new PSInvalidOperationException($"Unable to resolve the SharePoint list identifier for folder '{folder.Name}'.");
			}

			PnPContext.Site.EnsureProperties(site => site.Id);
			PnPContext.Web.EnsureProperties(web => web.Id);

			var graphSiteId = $"{PnPContext.Uri.DnsSafeHost},{PnPContext.Site.Id},{PnPContext.Web.Id}";
			var requestUrl = $"v1.0/sites/{graphSiteId}/lists/{listItem.ParentList.Id}/items/{listItem.Id}/driveItem?$select=id,parentReference";

			LogDebug($"Resolving Graph drive item identifiers for folder '{folder.Name}' using '{requestUrl}'");

			var responseContent = GraphRequestHelper.Get(requestUrl);
			if (string.IsNullOrWhiteSpace(responseContent))
			{
				throw new PSInvalidOperationException($"Resolving Microsoft Graph identifiers for folder '{folder.Name}' returned an empty response.");
			}

			using var jsonDocument = JsonDocument.Parse(responseContent);
			if (!jsonDocument.RootElement.TryGetProperty("id", out var itemIdElement) || itemIdElement.ValueKind != JsonValueKind.String)
			{
				throw new PSInvalidOperationException($"Resolving Microsoft Graph identifiers for folder '{folder.Name}' returned a response without a driveItem id.");
			}

			if (!jsonDocument.RootElement.TryGetProperty("parentReference", out var parentReferenceElement))
			{
				throw new PSInvalidOperationException($"Resolving Microsoft Graph identifiers for folder '{folder.Name}' returned a response without a parentReference object.");
			}

			if (!parentReferenceElement.TryGetProperty("driveId", out var driveIdElement) || driveIdElement.ValueKind != JsonValueKind.String)
			{
				throw new PSInvalidOperationException($"Resolving Microsoft Graph identifiers for folder '{folder.Name}' returned a response without a driveId.");
			}

			var itemId = itemIdElement.GetString();
			var driveId = driveIdElement.GetString();

			if (string.IsNullOrWhiteSpace(itemId) || string.IsNullOrWhiteSpace(driveId))
			{
				throw new PSInvalidOperationException($"Resolving Microsoft Graph identifiers for folder '{folder.Name}' returned empty driveItem identifiers.");
			}

			return new GraphDriveItemReference
			{
				DriveId = driveId,
				ItemId = itemId
			};
		}

		private FolderArchiveStateResult CreateResult(IFolder folder, FolderArchiveState requestedState, Uri monitorUrl)
		{
			if (monitorUrl == null)
			{
				throw new PSInvalidOperationException($"Changing the archive state of folder '{folder.Name}' succeeded but Microsoft Graph did not return the expected monitor URL in the Location header.");
			}

			var operationName = requestedState == FolderArchiveState.Archived ? "archive" : "reactivation";
			WriteVerbose($"Folder '{folder.Name}' {operationName} request accepted. Monitor URL returned.");

			return new FolderArchiveStateResult
			{
				FolderName = folder.Name,
				ServerRelativeUrl = folder.ServerRelativeUrl,
				RequestedState = requestedState,
				MonitorUrl = monitorUrl.AbsoluteUri
			};
		}

		private Dictionary<string, string> GetAsyncHeaders()
		{
			return new Dictionary<string, string>
			{
				{ "Prefer", "respond-async" }
			};
		}
	}
}