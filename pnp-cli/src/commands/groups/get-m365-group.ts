import { Command } from 'commander';
import { GraphCommand } from '../../core/graph-command.js';

/**
 * Retrieves Microsoft 365 Groups.
 * Equivalent to Get-PnPMicrosoft365Group in the C# codebase.
 *
 * Permissions: Group.Read.All or Group.ReadWrite.All
 */
export class GetM365GroupCommand extends GraphCommand {
  register(program: Command): void {
    program
      .command('get-m365-group')
      .description('Returns one or more Microsoft 365 Groups')
      .option('--identity <nameOrId>', 'Group display name or ID')
      .option('--filter <filter>', 'OData filter to apply when listing groups')
      .option('--include-site-url', 'Include the SharePoint site URL in the output')
      .option('--include-owners', 'Include the group owners in the output')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    if (this.parameterSpecified(options, 'identity')) {
      const identity = options.identity as string;
      const isGuid = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i.test(identity);

      if (isGuid) {
        let selectFields = '';
        if (options.includeSiteUrl) {
          // Need to explicitly select the sites URL via a separate call
          selectFields = '?$select=id,displayName,description,mail,mailNickname,visibility,groupTypes,createdDateTime,renewedDateTime';
        }
        const group = await this.graphRequestHelper.getTyped<Record<string, unknown>>(
          `v1.0/groups/${identity}${selectFields}`,
        );

        if (options.includeSiteUrl && group) {
          try {
            const site = await this.graphRequestHelper.getTyped<{ webUrl: string }>(
              `v1.0/groups/${identity}/sites/root?$select=webUrl`,
            );
            if (site?.webUrl) {
              group.siteUrl = site.webUrl;
            }
          } catch {
            // Site URL may not be available for all groups
          }
        }

        if (options.includeOwners && group) {
          try {
            const owners = await this.graphRequestHelper.getTyped<{ value: unknown[] }>(
              `v1.0/groups/${identity}/owners`,
            );
            group.owners = owners?.value || [];
          } catch {
            // Owners may not be accessible
          }
        }

        this.writeOutput(group);
      } else {
        // Search by display name
        const encodedName = encodeURIComponent(identity);
        const result = await this.graphRequestHelper.getTyped<{ value: unknown[] }>(
          `v1.0/groups?$filter=displayName eq '${encodedName}' and groupTypes/any(c:c eq 'Unified')`,
        );
        if (result?.value?.length === 1) {
          this.writeOutput(result.value[0]);
        } else if (result?.value?.length > 1) {
          this.writeOutput(result.value);
        } else {
          throw new Error(`Microsoft 365 Group not found: ${identity}`);
        }
      }
    } else {
      let url = `v1.0/groups?$filter=groupTypes/any(c:c eq 'Unified')`;
      if (this.parameterSpecified(options, 'filter')) {
        url += ` and (${options.filter as string})`;
      }
      url += '&$orderby=displayName';

      const groups = await this.graphRequestHelper.getResultCollection<unknown>(url);
      this.writeOutput(groups);
    }
  }
}
