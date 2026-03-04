import { Command } from 'commander';
import { GraphCommand } from '../../core/graph-command.js';

/**
 * Retrieves Entra ID (Azure AD) users.
 * Equivalent to Get-PnPEntraIDUser in the C# codebase.
 *
 * Permissions: User.Read.All
 */
export class GetEntraUserCommand extends GraphCommand {
  register(program: Command): void {
    program
      .command('get-entra-user')
      .description('Returns one or more Entra ID users')
      .option('--identity <userIdOrUpn>', 'User ID (GUID) or User Principal Name')
      .option('--filter <filter>', 'OData filter to apply when listing users')
      .option('--order-by <orderBy>', 'OData orderby clause')
      .option('--select <properties>', 'Comma-separated list of properties to select')
      .option('--all', 'Retrieve all pages of users')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    if (this.parameterSpecified(options, 'identity')) {
      const identity = options.identity as string;
      let url = `v1.0/users/${encodeURIComponent(identity)}`;

      if (this.parameterSpecified(options, 'select')) {
        url += `?$select=${options.select as string}`;
      }

      const user = await this.graphRequestHelper.getTyped<unknown>(url);
      this.writeOutput(user);
    } else {
      let url = 'v1.0/users';
      const queryParams: string[] = [];

      if (this.parameterSpecified(options, 'filter')) {
        queryParams.push(`$filter=${options.filter as string}`);
      }
      if (this.parameterSpecified(options, 'orderBy')) {
        queryParams.push(`$orderby=${options.orderBy as string}`);
      }
      if (this.parameterSpecified(options, 'select')) {
        queryParams.push(`$select=${options.select as string}`);
      }

      if (queryParams.length > 0) {
        url += '?' + queryParams.join('&');
      }

      const additionalHeaders: Record<string, string> = {};
      // ConsistencyLevel header may be needed for advanced queries
      if (this.parameterSpecified(options, 'filter') || this.parameterSpecified(options, 'orderBy')) {
        additionalHeaders.ConsistencyLevel = 'eventual';
      }

      if (options.all) {
        const users = await this.graphRequestHelper.getResultCollection<unknown>(url, additionalHeaders);
        this.writeOutput(users);
      } else {
        const result = await this.graphRequestHelper.getTyped<{ value: unknown[] }>(url, additionalHeaders);
        this.writeOutput(result?.value);
      }
    }
  }
}
