import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';
import { ListResolver } from '../../resolvers/list-resolver.js';

export class RemoveListItemCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('remove-list-item')
      .description('Deletes an item from a list')
      .requiredOption('--list <nameOrId>', 'List title, URL, or GUID')
      .requiredOption('--id <id>', 'Item ID')
      .option('--recycle', 'Move to recycle bin')
      .option('--force', 'Do not ask for confirmation')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const resolver = new ListResolver(options.list as string);
    const listInfo = await resolver.resolve(this.sharePointRequestHelper as any, webUrl) as any;
    if (!listInfo) throw new Error(`List '${options.list}' not found.`);

    const listId = listInfo?.d?.Id || listInfo?.Id;
    const itemId = options.id as string;

    const accessToken = await this.accessToken;
    const digestResponse = await fetch(`${webUrl}/_api/contextinfo`, {
      method: 'POST',
      headers: { Accept: 'application/json;odata=verbose', Authorization: `Bearer ${accessToken}`, 'Content-Length': '0' },
    });
    const digestData = await digestResponse.json() as any;
    const digest = digestData.d.GetContextWebInformation.FormDigestValue;

    if (options.recycle) {
      const response = await fetch(`${webUrl}/_api/web/lists(guid'${listId}')/items(${itemId})/recycle`, {
        method: 'POST',
        headers: { Accept: 'application/json;odata=verbose', Authorization: `Bearer ${accessToken}`, 'X-RequestDigest': digest },
      });
      if (!response.ok) throw new Error(`Failed to recycle list item: ${await response.text()}`);
      const result = await response.json() as any;
      this.writeOutput({ recycleBinItemId: result?.d?.Recycle || result?.value });
    } else {
      const response = await fetch(`${webUrl}/_api/web/lists(guid'${listId}')/items(${itemId})`, {
        method: 'POST',
        headers: {
          Accept: 'application/json;odata=verbose', Authorization: `Bearer ${accessToken}`,
          'X-RequestDigest': digest, 'X-HTTP-Method': 'DELETE', 'IF-MATCH': '*',
        },
      });
      if (!response.ok) throw new Error(`Failed to delete list item: ${await response.text()}`);
      console.log(`List item ${itemId} deleted successfully.`);
    }
  }
}
