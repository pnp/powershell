import { Command } from 'commander';
import { GraphCommand } from '../../core/graph-command.js';

/**
 * Retrieves Microsoft Teams teams.
 * Equivalent to Get-PnPTeamsTeam in the C# codebase.
 *
 * Permissions: Group.Read.All or Group.ReadWrite.All
 */
export class GetTeamsTeamCommand extends GraphCommand {
  register(program: Command): void {
    program
      .command('get-teams-team')
      .description('Returns one or more Microsoft Teams teams')
      .option('--identity <nameOrId>', 'Team display name or group ID')
      .option('--filter <filter>', 'OData filter to apply when listing teams')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    if (this.parameterSpecified(options, 'identity')) {
      const identity = options.identity as string;
      const isGuid = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i.test(identity);

      if (isGuid) {
        const team = await this.graphRequestHelper.getTyped<unknown>(
          `v1.0/teams/${identity}`,
        );
        this.writeOutput(team);
      } else {
        // Search by display name
        const encodedName = encodeURIComponent(identity);
        const result = await this.graphRequestHelper.getTyped<{ value: unknown[] }>(
          `v1.0/groups?$filter=resourceProvisioningOptions/any(x:x eq 'Team') and displayName eq '${encodedName}'`,
          { ConsistencyLevel: 'eventual' },
        );
        if (result?.value?.length === 1) {
          this.writeOutput(result.value[0]);
        } else if (result?.value?.length > 1) {
          this.writeOutput(result.value);
        } else {
          throw new Error(`Team not found: ${identity}`);
        }
      }
    } else {
      let url = `v1.0/groups?$filter=resourceProvisioningOptions/any(x:x eq 'Team')`;
      if (this.parameterSpecified(options, 'filter')) {
        url += ` and (${options.filter as string})`;
      }

      const teams = await this.graphRequestHelper.getResultCollection<unknown>(
        url,
        { ConsistencyLevel: 'eventual' },
      );
      this.writeOutput(teams);
    }
  }
}
