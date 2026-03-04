import { Command } from 'commander';
import { GraphCommand } from '../../core/graph-command.js';

/**
 * Retrieves Entra ID (Azure AD) application registrations.
 * Equivalent to Get-PnPEntraIDApp in the C# codebase.
 *
 * Permissions: Application.Read.All
 */
export class GetEntraAppCommand extends GraphCommand {
  register(program: Command): void {
    program
      .command('get-entra-app')
      .description('Returns one or more Entra ID application registrations')
      .option('--identity <appIdOrName>', 'Application (client) ID, object ID, or display name')
      .option('--filter <filter>', 'OData filter to apply when listing applications')
      .option('--all', 'Retrieve all pages of applications')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    if (this.parameterSpecified(options, 'identity')) {
      const identity = options.identity as string;
      const isGuid = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i.test(identity);

      if (isGuid) {
        // Try by object ID first, fall back to appId filter
        try {
          const app = await this.graphRequestHelper.getTyped<unknown>(
            `v1.0/applications/${identity}`,
          );
          this.writeOutput(app);
          return;
        } catch {
          // Object ID lookup failed, try by appId
        }

        const result = await this.graphRequestHelper.getTyped<{ value: unknown[] }>(
          `v1.0/applications?$filter=appId eq '${identity}'`,
          { ConsistencyLevel: 'eventual' },
        );
        if (result?.value?.length > 0) {
          this.writeOutput(result.value.length === 1 ? result.value[0] : result.value);
        } else {
          throw new Error(`Application not found: ${identity}`);
        }
      } else {
        // Search by display name
        const encodedName = encodeURIComponent(identity);
        const result = await this.graphRequestHelper.getTyped<{ value: unknown[] }>(
          `v1.0/applications?$filter=displayName eq '${encodedName}'`,
          { ConsistencyLevel: 'eventual' },
        );
        if (result?.value?.length > 0) {
          this.writeOutput(result.value.length === 1 ? result.value[0] : result.value);
        } else {
          throw new Error(`Application not found: ${identity}`);
        }
      }
    } else {
      let url = 'v1.0/applications';
      const additionalHeaders: Record<string, string> = {};

      if (this.parameterSpecified(options, 'filter')) {
        url += `?$filter=(${options.filter as string})`;
        additionalHeaders.ConsistencyLevel = 'eventual';
      }

      if (options.all) {
        const apps = await this.graphRequestHelper.getResultCollection<unknown>(url, additionalHeaders);
        this.writeOutput(apps);
      } else {
        const result = await this.graphRequestHelper.getTyped<{ value: unknown[] }>(url, additionalHeaders);
        this.writeOutput(result?.value);
      }
    }
  }
}
