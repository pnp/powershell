using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPCommandPermission")]
    [OutputType(typeof(CommandPermission))]
    public class GetCommandPermission : BasePSCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Identity", "Name")]
        [ArgumentCompleter(typeof(PnPCommandNameCompleter))]
        public string CommandName { get; set; }

        [Parameter(Mandatory = false)]
        public Enums.ResourceTypeName? ResourceTypeName { get; set; }

        [Parameter(Mandatory = false)]
        public Enums.CommandPermissionSource? Source { get; set; }

        protected override void ExecuteCmdlet()
        {
            IEnumerable<CommandPermission> results;

            if (string.IsNullOrWhiteSpace(CommandName))
            {
                results = CommandPermissionHelper.GetAll();
            }
            else if (WildcardPattern.ContainsWildcardCharacters(CommandName))
            {
                var pattern = new WildcardPattern(CommandName, WildcardOptions.IgnoreCase);
                results = CommandPermissionHelper.GetAll().Where(permission => pattern.IsMatch(permission.CommandName) || permission.Aliases.Any(pattern.IsMatch));
            }
            else
            {
                var permission = CommandPermissionHelper.Get(CommandName);
                if (permission == null)
                {
                    // Non terminating so that piping a list of cmdlet names reports the ones that could not be found without discarding the rest
                    WriteError(new ErrorRecord(
                        new PSArgumentException($"The PnP PowerShell cmdlet '{CommandName}' was not found."),
                        "CommandNotFound",
                        ErrorCategory.ObjectNotFound,
                        CommandName));
                    return;
                }

                results = [permission];
            }

            if (ResourceTypeName.HasValue)
            {
                results = results.Where(permission => RequiresResource(permission, ResourceTypeName.Value));
            }

            if (Source.HasValue)
            {
                results = results.Where(permission => permission.PermissionSource == Source.Value);
            }

            WriteObject(results, true);
        }

        private static bool RequiresResource(CommandPermission permission, Enums.ResourceTypeName resourceType)
        {
            return permission.ResourceTypes.Contains(resourceType);
        }
    }
}
