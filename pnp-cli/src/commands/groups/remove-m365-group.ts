import { Command } from 'commander';
import { GraphCommand } from '../../core/graph-command.js';

/**
 * Removes (deletes) a Microsoft 365 Group.
 * Equivalent to Remove-PnPMicrosoft365Group in the C# codebase.
 *
 * Permissions: Group.ReadWrite.All
 */
export class RemoveM365GroupCommand extends GraphCommand {
  register(program: Command): void {
    program
      .command('remove-m365-group')
      .description('Removes (deletes) a Microsoft 365 Group')
      .requiredOption('--identity <groupId>', 'Group ID of the Microsoft 365 Group to remove')
      .option('--force', 'Skip confirmation prompt')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const groupId = options.identity as string;

    const response = await this.graphRequestHelper.delete(`v1.0/groups/${groupId}`);

    if (response.ok || response.status === 204) {
      this.writeVerbose(`Microsoft 365 Group '${groupId}' has been successfully deleted.`);
    } else {
      const errorText = await response.text();
      throw new Error(`Failed to delete group: ${errorText}`);
    }
  }
}
