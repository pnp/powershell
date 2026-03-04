import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';
import { ListResolver } from '../../resolvers/list-resolver.js';

export class GetViewCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-view')
      .description('Returns one or more list views')
      .requiredOption('--list <nameOrId>', 'List title, URL, or GUID')
      .option('--identity <nameOrId>', 'View title or GUID')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const resolver = new ListResolver(options.list as string);
    const listInfo = await resolver.resolve(this.sharePointRequestHelper as any, webUrl) as any;
    if (!listInfo) throw new Error(`List '${options.list}' not found.`);
    const listId = listInfo?.d?.Id || listInfo?.Id;

    if (this.parameterSpecified(options, 'identity')) {
      const viewId = options.identity as string;
      const isGuid = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i.test(viewId);
      if (isGuid) {
        const view = await this.sharePointRequestHelper.getTyped(`${webUrl}/_api/web/lists(guid'${listId}')/views(guid'${viewId}')`);
        this.writeOutput(view);
      } else {
        const view = await this.sharePointRequestHelper.getTyped(`${webUrl}/_api/web/lists(guid'${listId}')/views/GetByTitle('${encodeURIComponent(viewId)}')`);
        this.writeOutput(view);
      }
    } else {
      const result = await this.sharePointRequestHelper.getTyped<{ value: unknown[] }>(`${webUrl}/_api/web/lists(guid'${listId}')/views`);
      this.writeOutput(result?.value);
    }
  }
}
