import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';
import { ListResolver } from '../../resolvers/list-resolver.js';

export class AddListItemCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('add-list-item')
      .description('Adds an item to a list')
      .requiredOption('--list <nameOrId>', 'List title, URL, or GUID')
      .option('--values <json>', 'JSON object with field values')
      .option('--content-type <nameOrId>', 'Content type name or ID')
      .option('--folder <path>', 'Folder path within the list')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const resolver = new ListResolver(options.list as string);
    const listInfo = await resolver.resolve(this.sharePointRequestHelper as any, webUrl) as any;

    if (!listInfo) {
      throw new Error(`List '${options.list}' not found.`);
    }

    const listId = listInfo?.d?.Id || listInfo?.Id;
    const listItemEntityTypeFullName = listInfo?.d?.ListItemEntityTypeFullName || listInfo?.ListItemEntityTypeFullName || 'SP.Data.ListItem';

    const fieldValues: Record<string, unknown> = {};
    if (options.values) {
      const parsed = typeof options.values === 'string' ? JSON.parse(options.values as string) : options.values;
      Object.assign(fieldValues, parsed);
    }

    if (options.contentType) {
      fieldValues['ContentTypeId'] = options.contentType;
    }

    fieldValues['__metadata'] = { type: listItemEntityTypeFullName };

    const accessToken = await this.accessToken;
    const digestResponse = await fetch(`${webUrl}/_api/contextinfo`, {
      method: 'POST',
      headers: {
        Accept: 'application/json;odata=verbose',
        Authorization: `Bearer ${accessToken}`,
        'Content-Length': '0',
      },
    });
    const digestData = await digestResponse.json() as any;
    const digest = digestData.d.GetContextWebInformation.FormDigestValue;

    const response = await fetch(`${webUrl}/_api/web/lists(guid'${listId}')/items`, {
      method: 'POST',
      headers: {
        Accept: 'application/json;odata=verbose',
        'Content-Type': 'application/json;odata=verbose',
        Authorization: `Bearer ${accessToken}`,
        'X-RequestDigest': digest,
      },
      body: JSON.stringify(fieldValues),
    });

    if (!response.ok) {
      const err = await response.text();
      throw new Error(`Failed to add list item: ${err}`);
    }

    const result = await response.json();
    this.writeOutput(result);
  }
}
