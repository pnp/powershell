import { Command } from 'commander';
import { GraphCommand } from '../../core/graph-command.js';

/**
 * Creates a new channel in a Microsoft Teams team.
 * Equivalent to Add-PnPTeamsChannel in the C# codebase.
 *
 * Permissions: Group.ReadWrite.All
 */
export class NewTeamsChannelCommand extends GraphCommand {
  register(program: Command): void {
    program
      .command('new-teams-channel')
      .description('Creates a new channel in a Microsoft Teams team')
      .requiredOption('--team <teamId>', 'Team group ID')
      .requiredOption('--display-name <displayName>', 'Display name of the channel')
      .option('--description <description>', 'Description of the channel')
      .option('--channel-type <channelType>', 'Channel type: standard, private, or shared', 'standard')
      .option('--owner-upn <ownerUpn>', 'Owner UPN (required for private and shared channels)')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const teamId = options.team as string;
    const displayName = options.displayName as string;
    const description = (options.description as string) || '';
    const channelType = ((options.channelType as string) || 'standard').toLowerCase();

    if (channelType !== 'standard' && !this.parameterSpecified(options, 'ownerUpn')) {
      throw new Error('--owner-upn is required when using a non-standard channel type');
    }

    const channelBody: Record<string, unknown> = {
      displayName,
      description,
      membershipType: channelType,
    };

    if (channelType !== 'standard' && this.parameterSpecified(options, 'ownerUpn')) {
      const ownerUpn = options.ownerUpn as string;
      channelBody.members = [
        {
          '@odata.type': '#microsoft.graph.aadUserConversationMember',
          'user@odata.bind': `https://graph.microsoft.com/v1.0/users('${ownerUpn}')`,
          roles: ['owner'],
        },
      ];
    }

    const result = await this.graphRequestHelper.post<unknown>(
      `v1.0/teams/${teamId}/channels`,
      channelBody,
    );
    this.writeOutput(result);
  }
}
