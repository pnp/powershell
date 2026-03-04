import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';
import { ListResolver } from '../../resolvers/list-resolver.js';

export class RemoveFieldCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('remove-field')
      .description('Remove a field from a web or list')
      .requiredOption('--identity <nameOrId>', 'Field internal name, title, or GUID')
      .option('--list <nameOrId>', 'List title, URL, or GUID')
      .option('--force', 'Skip confirmation')
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

    await this.sharePointRequestHelper.delete(fieldUrl);
    this.writeOutput({ message: `Field '${identity}' removed successfully` });
  }
}
