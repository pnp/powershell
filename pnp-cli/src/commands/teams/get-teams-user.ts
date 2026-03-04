import { Command } from 'commander';
import { GraphCommand } from '../../core/graph-command.js';

/**
 * Retrieves members (users) of a Microsoft Teams team.
 * Equivalent to Get-PnPTeamsUser in the C# codebase.
 *
 * Permissions: Group.Read.All or Group.ReadWrite.All
 */
export class GetTeamsUserCommand extends GraphCommand {
  register(program: Command): void {
    program
      .command('get-teams-user')
      .description('Returns members of a Microsoft Teams team')
      .requiredOption('--team <teamId>', 'Team group ID')
      .option('--role <role>', 'Filter by role: owner, member, or guest')
      .option('--channel <channelId>', 'Channel ID (for private channel members)')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const teamId = options.team as string;

    let members: unknown[];

    if (this.parameterSpecified(options, 'channel')) {
      const channelId = options.channel as string;
      const result = await this.graphRequestHelper.getTyped<{ value: unknown[] }>(
        `v1.0/teams/${teamId}/channels/${channelId}/members`,
      );
      members = result?.value || [];
    } else {
      const result = await this.graphRequestHelper.getTyped<{ value: unknown[] }>(
        `v1.0/teams/${teamId}/members`,
      );
      members = result?.value || [];
    }

    if (this.parameterSpecified(options, 'role')) {
      const role = (options.role as string).toLowerCase();
      members = members.filter((m: any) => {
        const memberRoles: string[] = m.roles || [];
        if (role === 'owner') {
          return memberRoles.some((r: string) => r.toLowerCase() === 'owner');
        } else if (role === 'guest') {
          return memberRoles.some((r: string) => r.toLowerCase() === 'guest');
        } else {
          // member role = no special roles
          return memberRoles.length === 0;
        }
      });
    }

    this.writeOutput(members);
  }
}
