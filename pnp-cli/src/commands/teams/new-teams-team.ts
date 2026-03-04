import { Command } from 'commander';
import { GraphCommand } from '../../core/graph-command.js';

/**
 * Creates a new Microsoft Teams team.
 * Equivalent to New-PnPTeamsTeam in the C# codebase.
 *
 * Permissions: Group.ReadWrite.All
 */
export class NewTeamsTeamCommand extends GraphCommand {
  register(program: Command): void {
    program
      .command('new-teams-team')
      .description('Creates a new Microsoft Teams team')
      .option('--display-name <displayName>', 'Display name of the team')
      .option('--description <description>', 'Description of the team')
      .option('--mail-nickname <mailNickname>', 'Mail nickname for the underlying group')
      .option('--visibility <visibility>', 'Team visibility: Private or Public', 'Public')
      .option('--group-id <groupId>', 'Create a team from an existing Microsoft 365 group')
      .option('--template <template>', 'Team template type (e.g., standard)')
      .option('--owners <owners>', 'Comma-separated list of owner UPNs or IDs')
      .option('--members <members>', 'Comma-separated list of member UPNs or IDs')
      .option('--allow-add-remove-apps <value>', 'Allow members to add/remove apps')
      .option('--allow-create-update-channels <value>', 'Allow members to create/update channels')
      .option('--allow-delete-channels <value>', 'Allow members to delete channels')
      .option('--allow-giphy <value>', 'Allow Giphy in the team')
      .option('--allow-stickers-and-memes <value>', 'Allow stickers and memes')
      .option('--allow-channel-mentions <value>', 'Allow @channel mentions')
      .option('--allow-team-mentions <value>', 'Allow @team mentions')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    if (this.parameterSpecified(options, 'groupId')) {
      // Create team from existing group
      const groupId = options.groupId as string;
      const teamBody: Record<string, unknown> = {};

      this.applyTeamSettings(teamBody, options);

      const result = await this.graphRequestHelper.put<unknown>(
        `v1.0/groups/${groupId}/team`,
        teamBody,
      );
      this.writeOutput(result);
    } else {
      // Create a new team (and underlying group)
      if (!this.parameterSpecified(options, 'displayName')) {
        throw new Error('--display-name is required when creating a new team');
      }

      const displayName = options.displayName as string;
      const description = (options.description as string) || '';
      const visibility = (options.visibility as string) || 'Public';
      const mailNickname = (options.mailNickname as string) || displayName.replace(/[^a-zA-Z0-9]/g, '');

      const teamBody: Record<string, unknown> = {
        'template@odata.bind': `https://graph.microsoft.com/v1.0/teamsTemplates('standard')`,
        displayName,
        description,
        visibility: visibility.charAt(0).toUpperCase() + visibility.slice(1).toLowerCase(),
      };

      if (this.parameterSpecified(options, 'template') && options.template !== 'standard') {
        teamBody['template@odata.bind'] = `https://graph.microsoft.com/v1.0/teamsTemplates('${options.template as string}')`;
      }

      this.applyTeamSettings(teamBody, options);

      // Add owners and members
      const members: Record<string, unknown>[] = [];
      if (this.parameterSpecified(options, 'owners')) {
        const ownerList = (options.owners as string).split(',').map(o => o.trim());
        for (const owner of ownerList) {
          members.push({
            '@odata.type': '#microsoft.graph.aadUserConversationMember',
            roles: ['owner'],
            'user@odata.bind': `https://graph.microsoft.com/v1.0/users('${owner}')`,
          });
        }
      }
      if (this.parameterSpecified(options, 'members')) {
        const memberList = (options.members as string).split(',').map(m => m.trim());
        for (const member of memberList) {
          members.push({
            '@odata.type': '#microsoft.graph.aadUserConversationMember',
            roles: [],
            'user@odata.bind': `https://graph.microsoft.com/v1.0/users('${member}')`,
          });
        }
      }
      if (members.length > 0) {
        teamBody.members = members;
      }

      const result = await this.graphRequestHelper.post<unknown>(
        'v1.0/teams',
        teamBody,
      );
      this.writeOutput(result);
    }
  }

  private applyTeamSettings(teamBody: Record<string, unknown>, options: Record<string, unknown>): void {
    const memberSettings: Record<string, boolean> = {};
    const messagingSettings: Record<string, unknown> = {};
    const funSettings: Record<string, unknown> = {};

    if (this.parameterSpecified(options, 'allowAddRemoveApps')) {
      memberSettings.allowAddRemoveApps = options.allowAddRemoveApps === 'true';
    }
    if (this.parameterSpecified(options, 'allowCreateUpdateChannels')) {
      memberSettings.allowCreateUpdateChannels = options.allowCreateUpdateChannels === 'true';
    }
    if (this.parameterSpecified(options, 'allowDeleteChannels')) {
      memberSettings.allowDeleteChannels = options.allowDeleteChannels === 'true';
    }

    if (this.parameterSpecified(options, 'allowChannelMentions')) {
      messagingSettings.allowChannelMentions = options.allowChannelMentions === 'true';
    }
    if (this.parameterSpecified(options, 'allowTeamMentions')) {
      messagingSettings.allowTeamMentions = options.allowTeamMentions === 'true';
    }

    if (this.parameterSpecified(options, 'allowGiphy')) {
      funSettings.allowGiphy = options.allowGiphy === 'true';
    }
    if (this.parameterSpecified(options, 'allowStickersAndMemes')) {
      funSettings.allowStickersAndMemes = options.allowStickersAndMemes === 'true';
    }

    if (Object.keys(memberSettings).length > 0) {
      teamBody.memberSettings = memberSettings;
    }
    if (Object.keys(messagingSettings).length > 0) {
      teamBody.messagingSettings = messagingSettings;
    }
    if (Object.keys(funSettings).length > 0) {
      teamBody.funSettings = funSettings;
    }
  }
}
