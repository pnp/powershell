import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';
import { FieldResolver } from '../../resolvers/field-resolver.js';
import { ListResolver } from '../../resolvers/list-resolver.js';

export class GetFieldCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-field')
      .description('Returns one or more fields/columns from a web or list')
      .option('--identity <nameOrId>', 'Field internal name, title, or GUID')
      .option('--list <nameOrId>', 'List title, URL, or GUID to get fields from')
      .option('--group <group>', 'Filter by field group name')
      .option('--in-site-hierarchy', 'Search in site hierarchy (available fields)')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    let baseEndpoint: string;

    if (this.parameterSpecified(options, 'list')) {
      const listResolver = new ListResolver(options.list as string);
      const list = await listResolver.resolve(this.sharePointRequestHelper as any, webUrl);
      const listId = (list as any).Id;
      baseEndpoint = `${webUrl}/_api/web/lists(guid'${listId}')/fields`;
    } else if (options.inSiteHierarchy) {
      baseEndpoint = `${webUrl}/_api/web/availablefields`;
    } else {
      baseEndpoint = `${webUrl}/_api/web/fields`;
    }

    if (this.parameterSpecified(options, 'identity')) {
      const identity = options.identity as string;
      const guidPattern = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;

      let url: string;
      if (guidPattern.test(identity)) {
        url = `${baseEndpoint}(guid'${identity}')`;
      } else {
        url = `${baseEndpoint}/getByInternalNameOrTitle('${encodeURIComponent(identity)}')`;
      }

      const field = await this.sharePointRequestHelper.getTyped<any>(url);
      this.writeOutput(field);
    } else {
      let url = baseEndpoint;
      if (this.parameterSpecified(options, 'group')) {
        url += `?$filter=Group eq '${encodeURIComponent(options.group as string)}'&$orderby=Title`;
      } else {
        url += '?$orderby=Title';
      }
      const result = await this.sharePointRequestHelper.getTyped<{ value: unknown[] }>(url);
      this.writeOutput(result?.value);
    }
  }
}
