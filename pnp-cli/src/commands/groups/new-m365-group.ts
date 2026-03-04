import { Command } from 'commander';
import { GraphCommand } from '../../core/graph-command.js';

/**
 * Creates a new Microsoft 365 Group.
 * Equivalent to New-PnPMicrosoft365Group in the C# codebase.
 *
 * Permissions: Group.ReadWrite.All
 */
export class NewM365GroupCommand extends GraphCommand {
  register(program: Command): void {
    program
      .command('new-m365-group')
      .description('Creates a new Microsoft 365 Group')
      .requiredOption('--display-name <displayName>', 'Display name of the group')
      .requiredOption('--description <description>', 'Description of the group')
      .requiredOption('--mail-nickname <mailNickname>', 'Mail nickname for the group (no spaces)')
      .option('--is-private', 'Create a private group (default is public)')
      .option('--mail-enabled <value>', 'Enable mail for the group', 'true')
      .option('--security-enabled', 'Enable security for the group')
      .option('--owners <owners>', 'Comma-separated list of owner UPNs or IDs')
      .option('--members <members>', 'Comma-separated list of member UPNs or IDs')
      .option('--create-team', 'Also create a Microsoft Teams team for the group')
      .option('--preferred-data-location <location>', 'Preferred data location for the group')
      .option('--preferred-language <language>', 'Preferred language for the group')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const mailNickname = options.mailNickname as string;
    if (mailNickname.includes(' ')) {
      throw new Error('Mail nickname cannot contain spaces.');
    }

    const groupBody: Record<string, unknown> = {
      displayName: options.displayName as string,
      description: options.description as string,
      mailNickname,
      mailEnabled: options.mailEnabled !== 'false',
      securityEnabled: !!options.securityEnabled,
      groupTypes: ['Unified'],
      visibility: options.isPrivate ? 'Private' : 'Public',
    };

    if (this.parameterSpecified(options, 'preferredDataLocation')) {
      groupBody.preferredDataLocation = options.preferredDataLocation as string;
    }

    if (this.parameterSpecified(options, 'preferredLanguage')) {
      groupBody.preferredLanguage = options.preferredLanguage as string;
    }

    // Add owners as additional data bindings
    if (this.parameterSpecified(options, 'owners')) {
      const ownerList = (options.owners as string).split(',').map(o => o.trim());
      groupBody['owners@odata.bind'] = ownerList.map(
        owner => `https://graph.microsoft.com/v1.0/users/${owner}`,
      );
    }

    // Add members as additional data bindings
    if (this.parameterSpecified(options, 'members')) {
      const memberList = (options.members as string).split(',').map(m => m.trim());
      groupBody['members@odata.bind'] = memberList.map(
        member => `https://graph.microsoft.com/v1.0/users/${member}`,
      );
    }

    const group = await this.graphRequestHelper.post<Record<string, unknown>>(
      'v1.0/groups',
      groupBody,
    );

    // Optionally create a team for the group
    if (options.createTeam && group?.id) {
      this.writeVerbose('Creating a team for the new group...');

      // Wait briefly for group provisioning
      await new Promise(resolve => setTimeout(resolve, 5000));

      try {
        await this.graphRequestHelper.put<unknown>(
          `v1.0/groups/${group.id}/team`,
          {},
        );
        this.writeVerbose('Team created successfully for the group.');
      } catch (err) {
        this.writeWarning(`Group created but team creation failed: ${(err as Error).message}`);
      }
    }

    this.writeOutput(group);
  }
}
