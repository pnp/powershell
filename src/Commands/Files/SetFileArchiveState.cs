using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.SharePoint;
using System;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Files
{
	[Cmdlet(VerbsCommon.Set, "PnPFileArchiveState")]
	[OutputType(typeof(FileArchiveStateResult))]
	[ApiNotAvailableUnderApplicationPermissions]
	[RequiredApiDelegatedPermissions("graph/Files.Read")]
	[RequiredApiDelegatedPermissions("graph/Files.Read.All")]
	[RequiredApiDelegatedPermissions("graph/Files.ReadWrite")]
	[RequiredApiDelegatedPermissions("graph/Files.ReadWrite.All")]
	public class SetFileArchiveState : PnPGraphCmdlet
	{
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
		public FilePipeBind Identity;

		[Parameter(Mandatory = true, Position = 1)]
		public FileArchiveState ArchiveState;

		[Parameter(Mandatory = false)]
		public SwitchParameter Force;

		protected override void ExecuteCmdlet()
		{
			IFile file = Identity.GetCoreFile(Connection.PnPContext, this);
			if (file == null)
			{
				throw new PSInvalidOperationException("The provided identity does not resolve to a file.");
			}
			file.EnsureProperties(f => f.Name, f => f.ServerRelativeUrl, f => f.VroomDriveID, f => f.VroomItemID);

			if (string.IsNullOrEmpty(file.VroomDriveID) || string.IsNullOrEmpty(file.VroomItemID))
			{
				throw new PSInvalidOperationException($"Unable to resolve Microsoft Graph identifiers for file '{file.Name}'.");
			}

			switch (ArchiveState)
			{
				case FileArchiveState.Archived:
					EnsureRequiredFileArchivePermissions(FileArchiveState.Archived);
					WriteVerbose("Archiving a file makes its contents unavailable until the file is reactivated. Recently archived files can typically be reactivated immediately, while older archived files may take up to 24 hours to reactivate.");
					if (Force || ShouldContinue($"Change the archive state of file '{file.Name}' to Archived?", Properties.Resources.Confirm))
					{
						WriteObject(ArchiveFile(file));
					}
					break;
				case FileArchiveState.Active:
					EnsureRequiredFileArchivePermissions(FileArchiveState.Active);
					WriteVerbose("Reactivating an archived file can complete immediately for recently archived files, or it can transition into a reactivation period that may take up to 24 hours.");
					if (Force || ShouldContinue($"Change the archive state of file '{file.Name}' to Active?", Properties.Resources.Confirm))
					{
						WriteObject(UnarchiveFile(file));
					}
					break;
				default:
					throw new InvalidOperationException("OperationAborted");
			}
		}

		private void EnsureRequiredFileArchivePermissions(FileArchiveState requestedState)
		{
			var graphAccessToken = AccessToken;
			if (string.IsNullOrWhiteSpace(graphAccessToken))
			{
				throw new PSInvalidOperationException("Unable to acquire a Microsoft Graph access token required to validate file archive state permissions.");
			}

			var availableScopes = TokenHandler.ReturnScopes(graphAccessToken)
				.Where(scope => scope.ResourceType == ResourceTypeName.Graph)
				.Select(scope => scope.Scope)
				.ToArray();

			var requiredScopes = requestedState == FileArchiveState.Archived
				? new[] { "Files.ReadWrite", "Files.Read", "Files.Read.All", "Files.ReadWrite.All" }
				: new[] { "Files.Read", "Files.Read.All" };

			if (!availableScopes.Any(scope => requiredScopes.Contains(scope, StringComparer.InvariantCultureIgnoreCase)))
			{
				throw new PSInvalidOperationException($"Current access token lacks one of the required delegated Microsoft Graph permission scopes for changing the file archive state to {requestedState}: {string.Join(", ", requiredScopes)}.");
			}
		}

		private FileArchiveStateResult ArchiveFile(IFile file)
		{
			var requestUrl = $"beta/drives/{file.VroomDriveID}/items/{file.VroomItemID}/archive";

			LogDebug($"Sending file archive request to '{requestUrl}'");

			using var response = GraphRequestHelper.PostHttpContent(requestUrl, null);
			if (response.StatusCode != HttpStatusCode.NoContent)
			{
				throw new PSInvalidOperationException($"Archiving file '{file.Name}' returned unexpected status code {(int)response.StatusCode} {response.StatusCode}. Expected 204 NoContent for a file.");
			}

			WriteVerbose($"File '{file.Name}' archived.");

			return new FileArchiveStateResult
			{
				FileName = file.Name,
				ServerRelativeUrl = file.ServerRelativeUrl,
				RequestedState = FileArchiveState.Archived,
				ArchiveStatus = FileArchiveState.Archived.ToString().ToLowerInvariant()
			};
		}

		private FileArchiveStateResult UnarchiveFile(IFile file)
		{
			var requestUrl = $"beta/drives/{file.VroomDriveID}/items/{file.VroomItemID}/unarchive";

			LogDebug($"Sending file unarchive request to '{requestUrl}'");

			using var response = GraphRequestHelper.PostHttpContent(requestUrl, null);
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new PSInvalidOperationException($"Unarchiving file '{file.Name}' returned unexpected status code {(int)response.StatusCode} {response.StatusCode}. Expected 200 OK for a file.");
			}

			var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
			if (string.IsNullOrWhiteSpace(responseContent))
			{
				throw new PSInvalidOperationException($"Unarchiving file '{file.Name}' returned an empty response body. Expected driveItem metadata for a file.");
			}

			using var jsonDocument = JsonDocument.Parse(responseContent);
			if (!jsonDocument.RootElement.TryGetProperty("file", out var fileElement))
			{
				throw new PSInvalidOperationException($"Unarchiving file '{file.Name}' returned a response body without the expected file facet.");
			}

			var result = new FileArchiveStateResult
			{
				FileName = file.Name,
				ServerRelativeUrl = file.ServerRelativeUrl,
				RequestedState = FileArchiveState.Active,
				ArchiveStatus = FileArchiveState.Active.ToString().ToLowerInvariant()
			};

			if (fileElement.TryGetProperty("archiveStatus", out var archiveStatusElement) && archiveStatusElement.ValueKind == JsonValueKind.String)
			{
				var archiveStatus = archiveStatusElement.GetString();
				if (!string.IsNullOrWhiteSpace(archiveStatus) && archiveStatus.Equals("reactivating", StringComparison.OrdinalIgnoreCase))
				{
					result.ArchiveStatus = archiveStatus;
					WriteVerbose($"File '{file.Name}' reactivation in progress. It may take up to 24 hours for reactivation to complete.");
					return result;
				}

				if (!string.IsNullOrWhiteSpace(archiveStatus))
				{
					result.ArchiveStatus = archiveStatus;
					WriteVerbose($"File '{file.Name}' unarchive request accepted with archive status '{archiveStatus}'.");
					return result;
				}
			}

			WriteVerbose($"File '{file.Name}' reactivated.");
			return result;
		}
	}
}