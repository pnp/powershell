import { Command } from 'commander';
import { GraphCommand } from '../../core/graph-command.js';

/**
 * Retrieves members of a Microsoft 365 Group.
 * Equivalent to Get-PnPMicrosoft365GroupMember in the C# codebase.
 *
 * Permissions: Group.Read.All or Group.ReadWrite.All
 */
export class GetM365GroupMemberCommand extends GraphCommand {
  register(program: Command): void {
    program
      .command('get-m365-group-member')
      .description('Returns members of a Microsoft 365 Group')
      .requiredOption('--identity <groupId>', 'Group ID of the Microsoft 365 Group')
      .option('--all', 'Retrieve all pages of members')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const groupId = options.identity as string;
    const url = `v1.0/groups/${groupId}/members`;

    if (options.all) {
      const members = await this.graphRequestHelper.getResultCollection<Record<string, unknown>>(url);
      members.sort((a, b) => {
        const nameA = (a.displayName as string) || '';
        const nameB = (b.displayName as string) || '';
        return nameA.localeCompare(nameB);
      });
      this.writeOutput(members);
    } else {
      const result = await this.graphRequestHelper.getTyped<{ value: Record<string, unknown>[] }>(url);
      const members = result?.value || [];
      members.sort((a, b) => {
        const nameA = (a.displayName as string) || '';
        const nameB = (b.displayName as string) || '';
        return nameA.localeCompare(nameB);
      });
      this.writeOutput(members);
    }
  }
}
