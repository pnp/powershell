import { Command } from 'commander';
import { GraphCommand } from '../../core/graph-command.js';

/**
 * Retrieves Entra ID (Azure AD) groups.
 * Equivalent to Get-PnPEntraIDGroup in the C# codebase.
 *
 * Permissions: Group.Read.All or Group.ReadWrite.All
 */
export class GetEntraGroupCommand extends GraphCommand {
  register(program: Command): void {
    program
      .command('get-entra-group')
      .description('Returns one or more Entra ID groups')
      .option('--identity <groupIdOrName>', 'Group ID (GUID) or display name')
      .option('--all', 'Retrieve all pages of groups')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    if (this.parameterSpecified(options, 'identity')) {
      const identity = options.identity as string;
      const isGuid = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i.test(identity);

      if (isGuid) {
        const group = await this.graphRequestHelper.getTyped<unknown>(
          `v1.0/groups/${identity}`,
        );
        this.writeOutput(group);
      } else {
        // Search by display name
        const encodedName = encodeURIComponent(identity);
        const result = await this.graphRequestHelper.getTyped<{ value: unknown[] }>(
          `v1.0/groups?$filter=displayName eq '${encodedName}'`,
        );
        if (result?.value?.length === 1) {
          this.writeOutput(result.value[0]);
        } else if (result?.value?.length > 1) {
          this.writeOutput(result.value);
        } else {
          throw new Error(`Entra ID group not found: ${identity}`);
        }
      }
    } else {
      const url = 'v1.0/groups?$orderby=displayName';

      if (options.all) {
        const groups = await this.graphRequestHelper.getResultCollection<unknown>(url);
        this.writeOutput(groups);
      } else {
        const result = await this.graphRequestHelper.getTyped<{ value: unknown[] }>(url);
        this.writeOutput(result?.value);
      }
    }
  }
}
