import { Command } from 'commander';
import { GraphCommand } from '../../core/graph-command.js';

/**
 * Removes (deletes) a Microsoft Teams team.
 * Equivalent to Remove-PnPTeamsTeam in the C# codebase.
 *
 * Permissions: Group.ReadWrite.All
 */
export class RemoveTeamsTeamCommand extends GraphCommand {
  register(program: Command): void {
    program
      .command('remove-teams-team')
      .description('Removes (deletes) a Microsoft Teams team')
      .requiredOption('--identity <nameOrId>', 'Team display name or group ID')
      .option('--force', 'Skip confirmation prompt')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const identity = options.identity as string;
    const groupId = await this.resolveTeamGroupId(identity);

    if (!groupId) {
      throw new Error(`Team not found: ${identity}`);
    }

    const response = await this.graphRequestHelper.delete(`v1.0/groups/${groupId}`);

    if (response.ok || response.status === 204) {
      this.writeVerbose(`Team '${identity}' has been successfully deleted.`);
    } else {
      const errorText = await response.text();
      throw new Error(`Failed to delete team: ${errorText}`);
    }
  }

  private async resolveTeamGroupId(identity: string): Promise<string | null> {
    const isGuid = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i.test(identity);

    if (isGuid) {
      return identity;
    }

    // Resolve by display name
    const encodedName = encodeURIComponent(identity);
    const result = await this.graphRequestHelper.getTyped<{ value: Array<{ id: string }> }>(
      `v1.0/groups?$filter=resourceProvisioningOptions/any(x:x eq 'Team') and displayName eq '${encodedName}'`,
      { ConsistencyLevel: 'eventual' },
    );

    if (result?.value?.length === 1) {
      return result.value[0].id;
    }

    return null;
  }
}
