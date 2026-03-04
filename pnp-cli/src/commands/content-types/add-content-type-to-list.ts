import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';
import { ListResolver } from '../../resolvers/list-resolver.js';

export class AddContentTypeToListCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('add-content-type-to-list')
      .description('Add an existing content type to a list')
      .requiredOption('--list <nameOrId>', 'List title, URL, or GUID')
      .requiredOption('--content-type <nameOrId>', 'Content type name or ID to add')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();

    const listResolver = new ListResolver(options.list as string);
    const list = await listResolver.resolve(this.sharePointRequestHelper as any, webUrl);
    const listId = (list as any).Id;

    // Resolve content type ID
    const ctIdentity = options.contentType as string;
    let contentTypeId: string;

    if (ctIdentity.startsWith('0x')) {
      contentTypeId = ctIdentity;
    } else {
      const searchUrl = `${webUrl}/_api/web/contenttypes?$filter=Name eq '${encodeURIComponent(ctIdentity)}'`;
      const result = await this.sharePointRequestHelper.getTyped<{ value: any[] }>(searchUrl);
      if (!result?.value?.length) {
        throw new Error(`Content type '${ctIdentity}' not found`);
      }
      contentTypeId = result.value[0].StringId || result.value[0].Id?.StringValue;
    }

    const url = `${webUrl}/_api/web/lists(guid'${listId}')/contenttypes/addAvailableContentType`;
    const body = { contentTypeId };
    const ct = await this.sharePointRequestHelper.post<any>(url, body);
    this.writeOutput(ct);
  }
}
