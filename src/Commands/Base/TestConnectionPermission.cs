using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.Auth;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsDiagnostic.Test, "PnPConnectionPermission")]
    [OutputType(typeof(bool))]
    [ApiPermissionsNotRequired(Remarks = "The cmdlet only inspects access tokens acquired through the current connection and does not call an API.")]
    public class TestConnectionPermission : PnPConnectedCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Identity", "Name")]
        [ArgumentCompleter(typeof(PnPCommandNameCompleter))]
        [ValidateNotNullOrEmpty]
        public string CommandName { get; set; }

        // Tokens are acquired per resource type and reused for every cmdlet passed in through the pipeline. Acquiring them again per pipeline item
        // would issue hundreds of token requests for a script of any size and can provoke transient failures from the token endpoint.
        private Dictionary<ResourceTypeName, RequiredApiPermission[]> _grantedPermissions;
        private Dictionary<ResourceTypeName, string> _acquisitionErrors;
        private IdType _tokenType;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            _grantedPermissions = [];
            _acquisitionErrors = [];
            _tokenType = IdType.Unknown;
        }

        protected override void ExecuteCmdlet()
        {
            var commandPermission = CommandPermissionHelper.Get(CommandName);
            if (commandPermission == null)
            {
                WriteError(new ErrorRecord(
                    new PSArgumentException($"The PnP PowerShell cmdlet '{CommandName}' was not found."),
                    "CommandNotFound",
                    ErrorCategory.ObjectNotFound,
                    CommandName));
                return;
            }

            if (commandPermission.PermissionSource == CommandPermissionSource.NotApplicable)
            {
                WriteObject(true);
                return;
            }

            if (commandPermission.PermissionSource is CommandPermissionSource.Unknown or CommandPermissionSource.ResourceDependent)
            {
                WriteIndeterminate(commandPermission);
                return;
            }

            var fixedResourceTypes = commandPermission.DelegatedPermissions
                .Concat(commandPermission.ApplicationPermissions)
                .SelectMany(set => set.Permissions)
                .Select(permission => permission.ResourceType)
                .Distinct()
                .ToArray();
            if (fixedResourceTypes.Length == 0 || commandPermission.ResourceTypes.Except(fixedResourceTypes).Any())
            {
                WriteIndeterminate(commandPermission);
                return;
            }

            // An ACS token cannot be exchanged for a token on any other endpoint, not even for the SharePoint endpoint it was issued for,
            // so there is no token to read the permissions from
            if (Connection.Context?.GetContextSettings()?.Type == Framework.Utilities.Context.ClientContextType.SharePointACSAppOnly)
            {
                WriteIndeterminate(commandPermission, "The permissions of a connection made with an ACS app only token cannot be determined, as such a token carries no permission scopes. Connect using an Entra ID application registration to test permissions.");
                return;
            }

            if (_tokenType == IdType.Unknown)
            {
                _tokenType = DetermineTokenType(fixedResourceTypes);
            }

            if (_tokenType == IdType.Unknown)
            {
                WriteError(new ErrorRecord(
                    new PSInvalidOperationException($"Unable to determine the token type of the current connection. {DescribeAcquisitionErrors()}"),
                    "UnknownAccessTokenType",
                    ErrorCategory.AuthenticationError,
                    Connection));
                return;
            }

            var available = _tokenType == IdType.Application ? commandPermission.ApplicationAvailable : commandPermission.DelegatedAvailable;
            if (available == false)
            {
                WriteError(new ErrorRecord(
                    new PSInvalidOperationException($"{commandPermission.CommandName} is not available under {_tokenType.GetDescription()} permissions."),
                    "PermissionTypeNotSupported",
                    ErrorCategory.PermissionDenied,
                    commandPermission.CommandName));
                WriteObject(false);
                return;
            }

            var permissionSets = _tokenType == IdType.Application ? commandPermission.ApplicationPermissions : commandPermission.DelegatedPermissions;
            if (permissionSets.Length == 0)
            {
                WriteIndeterminate(commandPermission, $"No {_tokenType.GetDescription()} permissions are declared for this cmdlet.");
                return;
            }

            var missingAlternatives = new List<string>();
            var unverifiableResourceTypes = new HashSet<ResourceTypeName>();

            foreach (var permissionSet in permissionSets)
            {
                var missingPermissions = new List<RequiredApiPermission>();
                var alternativeIsUnverifiable = false;

                foreach (var resourceGroup in permissionSet.Permissions.GroupBy(permission => permission.ResourceType))
                {
                    var grantedPermissions = GetGrantedPermissions(resourceGroup.Key);
                    if (grantedPermissions == null)
                    {
                        // Without a token there is nothing to compare against. Reporting the permissions of this alternative as missing would
                        // present a check that could not be performed as a permission which is not held.
                        alternativeIsUnverifiable = true;
                        unverifiableResourceTypes.Add(resourceGroup.Key);
                        continue;
                    }

                    missingPermissions.AddRange(resourceGroup.Where(permission => !ApiPermissionEvaluator.IsSatisfiedBy(permission, grantedPermissions)));
                }

                if (alternativeIsUnverifiable)
                {
                    continue;
                }

                if (missingPermissions.Count == 0)
                {
                    WriteObject(true);
                    return;
                }

                missingAlternatives.Add(string.Join(" and ", missingPermissions.Select(permission => permission.ToString())));
            }

            // At least one alternative could not be evaluated, so it cannot be ruled out that the connection does hold a complete set
            if (unverifiableResourceTypes.Count > 0)
            {
                WriteIndeterminate(commandPermission, $"The permissions on {string.Join(" and ", unverifiableResourceTypes.Select(resourceType => resourceType.GetDescription()))} could not be read. {DescribeAcquisitionErrors(unverifiableResourceTypes)}");
                return;
            }

            WriteError(new ErrorRecord(
                new PSInvalidOperationException($"The current connection lacks one of the following required {_tokenType.GetDescription()} permission sets for {commandPermission.CommandName}: {string.Join(" or ", missingAlternatives)}."),
                "RequiredPermissionMissing",
                ErrorCategory.PermissionDenied,
                commandPermission.CommandName));
            WriteObject(false);
        }

        /// <summary>
        /// Reports that no reliable answer can be given for this cmdlet. Deliberately not returning FALSE: that would state that the connection
        /// does not hold the permissions, while the check could not be performed at all.
        /// </summary>
        private void WriteIndeterminate(CommandPermission commandPermission, string reason = null)
        {
            var details = string.IsNullOrWhiteSpace(reason) ? commandPermission.Guidance : reason;

            WriteError(new ErrorRecord(
                new PSInvalidOperationException($"The required permissions for {commandPermission.CommandName} cannot be determined. {details}"),
                "PermissionRequirementsIndeterminate",
                ErrorCategory.MetadataError,
                commandPermission.CommandName));
        }

        /// <summary>
        /// Reads the permissions present in the access token for a resource type, acquiring the token once and reusing it for later calls
        /// </summary>
        /// <returns>The permissions in the token, or NULL when no token could be acquired</returns>
        private RequiredApiPermission[] GetGrantedPermissions(ResourceTypeName resourceType)
        {
            if (_grantedPermissions.TryGetValue(resourceType, out var cachedPermissions))
            {
                return cachedPermissions;
            }

            if (_acquisitionErrors.ContainsKey(resourceType))
            {
                return null;
            }

            try
            {
                var token = ReadTokenPermissions(resourceType, _tokenType);
                _grantedPermissions[resourceType] = token.Permissions;
                return token.Permissions;
            }
            catch (Exception exception)
            {
                RecordAcquisitionError(resourceType, exception);
                return null;
            }
        }

        /// <summary>
        /// Establishes whether the connection represents delegated or application permissions by reading the idtyp claim of the first access token
        /// which can be acquired. Deliberately not derived from the connection itself: PnPConnection.InitializationType is never assigned, and
        /// ConnectionMethod defaults to Credentials, so neither can tell an app only connection apart from a delegated one. The token can.
        /// </summary>
        private IdType DetermineTokenType(ResourceTypeName[] resourceTypes)
        {
            var candidateResourceTypes = new List<ResourceTypeName>();
            if (Uri.TryCreate(Connection.Context?.Url, UriKind.Absolute, out _))
            {
                candidateResourceTypes.Add(ResourceTypeName.SharePoint);
            }
            candidateResourceTypes.Add(ResourceTypeName.Graph);
            candidateResourceTypes.AddRange(resourceTypes);

            foreach (var resourceType in candidateResourceTypes.Distinct())
            {
                if (_acquisitionErrors.ContainsKey(resourceType))
                {
                    continue;
                }

                try
                {
                    var token = ReadTokenPermissions(resourceType, IdType.Unknown);
                    _grantedPermissions[resourceType] = token.Permissions;
                    return token.TokenType;
                }
                catch (Exception exception)
                {
                    RecordAcquisitionError(resourceType, exception);
                }
            }

            return IdType.Unknown;
        }

        private (IdType TokenType, RequiredApiPermission[] Permissions) ReadTokenPermissions(ResourceTypeName resourceType, IdType expectedTokenType)
        {
            var accessToken = GetAccessToken(resourceType);
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new PSInvalidOperationException($"Unable to acquire an access token for {resourceType.GetDescription()} from the current connection.");
            }

            JsonWebToken decodedToken;
            try
            {
                decodedToken = new JsonWebToken(accessToken);
            }
            catch (ArgumentException exception)
            {
                throw new PSInvalidOperationException($"The access token for {resourceType.GetDescription()} is not a valid JWT.", exception);
            }

            var tokenType = TokenHandler.RetrieveTokenType(accessToken);
            if (tokenType == IdType.Unknown && decodedToken.Claims.Any(claim => claim.Type.Equals("scp", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(claim.Value)))
            {
                tokenType = IdType.Delegate;
            }
            else if (tokenType == IdType.Unknown && decodedToken.Claims.Any(claim => claim.Type.Equals("roles", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(claim.Value)))
            {
                tokenType = IdType.Application;
            }

            if (tokenType == IdType.Unknown)
            {
                throw new PSInvalidOperationException($"The access token for {resourceType.GetDescription()} does not identify whether it represents delegated or application permissions.");
            }

            if (expectedTokenType != IdType.Unknown && expectedTokenType != tokenType)
            {
                throw new PSInvalidOperationException($"The access token for {resourceType.GetDescription()} represents {tokenType.GetDescription()} permissions while the connection represents {expectedTokenType.GetDescription()} permissions.");
            }

            // A connection made with -AccessToken hands out the very same token whatever resource is asked for, so what comes back is not
            // necessarily a token for the resource that was requested. Trusting the request and labelling the scopes with the requested resource
            // type would present, i.e., SharePoint roles as Microsoft Graph roles and report a permission as held which the target API will reject.
            // The audience claim is what decides which API the token is actually for.
            var tokenResourceType = TokenHandler.DefineResourceTypeFromAudience(decodedToken.Audiences.FirstOrDefault());
            if (tokenResourceType != resourceType)
            {
                throw new PSInvalidOperationException($"The access token returned for {resourceType.GetDescription()} was issued for {tokenResourceType.GetDescription()}, so the permissions of this connection on {resourceType.GetDescription()} cannot be established from it.");
            }

            var permissions = TokenHandler.ReturnScopes(decodedToken);

            WriteVerbose($"Access token for {resourceType.GetDescription()} contains {permissions.Length} {tokenType.GetDescription()} permission scope(s): {string.Join(", ", permissions.Select(permission => permission.Scope))}");

            return (tokenType, permissions);
        }

        private string GetAccessToken(ResourceTypeName resourceType)
        {
            if (resourceType == ResourceTypeName.SharePoint && !Uri.TryCreate(Connection.Context?.Url, UriKind.Absolute, out _))
            {
                throw new PSInvalidOperationException("Unable to acquire a SharePoint access token because the current connection does not have a valid SharePoint site URL. Connect using -Url or provide a connection that includes a SharePoint context.");
            }

            var audience = resourceType switch
            {
                ResourceTypeName.Graph => $"https://{Connection.GraphEndPoint}/.default",
                ResourceTypeName.SharePoint when Uri.TryCreate(Connection.Context?.Url, UriKind.Absolute, out var siteUrl) => $"{siteUrl.GetLeftPart(UriPartial.Authority)}/.default",
                ResourceTypeName.AzureManagementApi => $"{Endpoints.GetArmEndpoint(Connection)}/.default",
                ResourceTypeName.PowerApps => $"{PowerPlatformUtility.GetPowerAppsServiceEndpoint(Connection.AzureEnvironment)}/.default",
                ResourceTypeName.Gcs => "https://gcs.office.com/.default",
                _ => null
            };

            if (audience == null)
            {
                throw new PSNotSupportedException($"Permission validation for {resourceType.GetDescription()} access tokens is not supported because the token audience cannot be determined from the cmdlet name alone.");
            }

            return TokenHandler.GetAccessToken(audience, Connection);
        }

        /// <summary>
        /// Records why no token could be acquired for a resource type. The full message goes to the verbose stream, as a token endpoint can reply
        /// with several hundred characters of diagnostics which would drown out the permission which could not be checked.
        /// </summary>
        private void RecordAcquisitionError(ResourceTypeName resourceType, Exception exception)
        {
            var message = exception.Message ?? "no details available";

            WriteVerbose($"Unable to acquire an access token for {resourceType.GetDescription()}: {message}");

            var summary = message.Split('\n')[0].Trim();
            _acquisitionErrors[resourceType] = summary.Length <= 200 ? summary : $"{summary[..200]}... Run with -Verbose for the full message.";
        }

        private string DescribeAcquisitionErrors(ICollection<ResourceTypeName> resourceTypes = null)
        {
            var errors = _acquisitionErrors.Where(error => resourceTypes == null || resourceTypes.Contains(error.Key)).ToArray();

            return errors.Length == 0
                ? string.Empty
                : string.Join(" ", errors.Select(error => $"{error.Key.GetDescription()}: {error.Value}"));
        }
    }
}
