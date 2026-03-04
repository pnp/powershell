import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';
import { ListResolver } from '../../resolvers/list-resolver.js';

export class GetListItemCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-list-item')
      .description('Retrieves list items from a list')
      .requiredOption('--list <nameOrId>', 'List title, URL, or GUID')
      .option('--id <id>', 'Item ID')
      .option('--unique-id <guid>', 'Item unique ID (GUID)')
      .option('--query <caml>', 'CAML query')
      .option('--fields <fields...>', 'Fields to return')
      .option('--page-size <size>', 'Number of items per page', '100')
      .option('--folder-server-relative-url <url>', 'Folder server-relative URL')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const listIdentity = options.list as string;
    const resolver = new ListResolver(listIdentity);
    const listInfo = await resolver.resolve(this.sharePointRequestHelper as any, webUrl) as any;

    if (!listInfo) {
      throw new Error(`List '${listIdentity}' not found.`);
    }

    const listId = listInfo?.d?.Id || listInfo?.Id;

    if (this.parameterSpecified(options, 'id')) {
      const item = await this.sharePointRequestHelper.getTyped(
        `${webUrl}/_api/web/lists(guid'${listId}')/items(${options.id})`,
      );
      this.writeOutput(item);
    } else if (this.parameterSpecified(options, 'uniqueId')) {
      const item = await this.sharePointRequestHelper.getTyped(
        `${webUrl}/_api/web/lists(guid'${listId}')/GetItemByUniqueId('${options.uniqueId}')`,
      );
      this.writeOutput(item);
    } else if (this.parameterSpecified(options, 'query')) {
      const camlQuery = options.query as string;
      const result = await this.sharePointRequestHelper.post(
        `${webUrl}/_api/web/lists(guid'${listId}')/GetItems`,
        { query: { __metadata: { type: 'SP.CamlQuery' }, ViewXml: camlQuery } },
      );
      this.writeOutput(result);
    } else {
      const pageSize = parseInt(options.pageSize as string, 10) || 100;
      let selectFields = '';
      if (options.fields) {
        selectFields = `&$select=${(options.fields as string[]).join(',')}`;
      }
      let folderFilter = '';
      if (options.folderServerRelativeUrl) {
        folderFilter = `&$filter=FileDirRef eq '${options.folderServerRelativeUrl}'`;
      }

      const items = await this.sharePointRequestHelper.getResultCollection(
        `${webUrl}/_api/web/lists(guid'${listId}')/items?$top=${pageSize}${selectFields}${folderFilter}`,
      );
      this.writeOutput(items);
    }
  }
}
