import { Command } from 'commander';
import { GraphCommand } from '../../core/graph-command.js';

/**
 * Retrieves channels for a Microsoft Teams team.
 * Equivalent to Get-PnPTeamsChannel in the C# codebase.
 *
 * Permissions: Group.Read.All or Group.ReadWrite.All
 */
export class GetTeamsChannelCommand extends GraphCommand {
  register(program: Command): void {
    program
      .command('get-teams-channel')
      .description('Returns one or more channels for a Microsoft Teams team')
      .requiredOption('--team <teamId>', 'Team group ID')
      .option('--identity <channelId>', 'Specific channel ID to retrieve')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const teamId = options.team as string;

    if (this.parameterSpecified(options, 'identity')) {
      const channelId = options.identity as string;
      const channel = await this.graphRequestHelper.getTyped<unknown>(
        `v1.0/teams/${teamId}/channels/${channelId}`,
      );
      this.writeOutput(channel);
    } else {
      const result = await this.graphRequestHelper.getTyped<{ value: unknown[] }>(
        `v1.0/teams/${teamId}/channels`,
      );
      this.writeOutput(result?.value);
    }
  }
}
