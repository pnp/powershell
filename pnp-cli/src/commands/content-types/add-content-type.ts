import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class AddContentTypeCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('add-content-type')
      .description('Add a new content type to the web')
      .requiredOption('--name <name>', 'Content type name')
      .option('--content-type-id <id>', 'Content type ID (e.g. 0x0101)')
      .option('--description <description>', 'Content type description')
      .option('--group <group>', 'Content type group')
      .option('--parent-content-type <nameOrId>', 'Parent content type name or ID')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const url = `${webUrl}/_api/web/contenttypes/add`;

    const body: any = {
      parameters: {
        __metadata: { type: 'SP.ContentTypeCreationInformation' },
        Name: options.name,
        Description: options.description || '',
        Group: options.group || 'Custom Content Types',
      },
    };

    if (options.contentTypeId) {
      body.parameters.Id = { StringValue: options.contentTypeId };
    } else if (options.parentContentType) {
      body.parameters.ParentContentType = { StringValue: options.parentContentType };
    }

    const result = await this.sharePointRequestHelper.post<any>(url, body);
    this.writeOutput(result);
  }
}
