import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';
import { ListResolver } from '../../resolvers/list-resolver.js';

export class SetListItemCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('set-list-item')
      .description('Updates a list item')
      .requiredOption('--list <nameOrId>', 'List title, URL, or GUID')
      .requiredOption('--id <id>', 'Item ID')
      .option('--values <json>', 'JSON object with field values')
      .option('--content-type <nameOrId>', 'Content type name or ID')
      .option('--system-update', 'Use SystemUpdate to avoid triggering workflows')
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
    const itemId = options.id as string;

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
      headers: { Accept: 'application/json;odata=verbose', Authorization: `Bearer ${accessToken}`, 'Content-Length': '0' },
    });
    const digestData = await digestResponse.json() as any;
    const digest = digestData.d.GetContextWebInformation.FormDigestValue;

    const method = options.systemUpdate ? 'ValidateUpdateListItem' : 'MERGE';

    if (options.systemUpdate) {
      const formValues = Object.entries(fieldValues)
        .filter(([key]) => key !== '__metadata')
        .map(([key, value]) => ({ FieldName: key, FieldValue: String(value) }));

      const response = await fetch(`${webUrl}/_api/web/lists(guid'${listId}')/items(${itemId})/ValidateUpdateListItem`, {
        method: 'POST',
        headers: {
          Accept: 'application/json;odata=verbose',
          'Content-Type': 'application/json;odata=verbose',
          Authorization: `Bearer ${accessToken}`,
          'X-RequestDigest': digest,
        },
        body: JSON.stringify({ formValues, bNewDocumentUpdate: false }),
      });

      if (!response.ok) {
        const err = await response.text();
        throw new Error(`Failed to update list item: ${err}`);
      }
      const result = await response.json();
      this.writeOutput(result);
    } else {
      const response = await fetch(`${webUrl}/_api/web/lists(guid'${listId}')/items(${itemId})`, {
        method: 'POST',
        headers: {
          Accept: 'application/json;odata=verbose',
          'Content-Type': 'application/json;odata=verbose',
          Authorization: `Bearer ${accessToken}`,
          'X-RequestDigest': digest,
          'IF-MATCH': '*',
          'X-HTTP-Method': 'MERGE',
        },
        body: JSON.stringify(fieldValues),
      });

      if (!response.ok) {
        const err = await response.text();
        throw new Error(`Failed to update list item: ${err}`);
      }
      console.log(`List item ${itemId} updated successfully.`);
    }
  }
}
