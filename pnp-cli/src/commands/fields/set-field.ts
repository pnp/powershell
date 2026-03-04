import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';
import { ListResolver } from '../../resolvers/list-resolver.js';

export class SetFieldCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('set-field')
      .description('Update a field on a web or list')
      .requiredOption('--identity <nameOrId>', 'Field internal name, title, or GUID')
      .option('--list <nameOrId>', 'List title, URL, or GUID')
      .option('--values <json>', 'JSON object with field properties to update')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const identity = options.identity as string;
    const guidPattern = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;

    let baseEndpoint: string;

    if (this.parameterSpecified(options, 'list')) {
      const listResolver = new ListResolver(options.list as string);
      const list = await listResolver.resolve(this.sharePointRequestHelper as any, webUrl);
      const listId = (list as any).Id;
      baseEndpoint = `${webUrl}/_api/web/lists(guid'${listId}')/fields`;
    } else {
      baseEndpoint = `${webUrl}/_api/web/fields`;
    }

    let fieldUrl: string;
    if (guidPattern.test(identity)) {
      fieldUrl = `${baseEndpoint}(guid'${identity}')`;
    } else {
      fieldUrl = `${baseEndpoint}/getByInternalNameOrTitle('${encodeURIComponent(identity)}')`;
    }

    const values = options.values ? JSON.parse(options.values as string) : {};
    const body = {
      __metadata: { type: 'SP.Field' },
      ...values,
    };

    await this.sharePointRequestHelper.patch<void>(fieldUrl, body);
    this.writeOutput({ message: `Field '${identity}' updated successfully` });
  }
}
