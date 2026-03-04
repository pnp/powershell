import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';
import { ContentTypeResolver } from '../../resolvers/content-type-resolver.js';
import { ListResolver } from '../../resolvers/list-resolver.js';

export class GetContentTypeCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-content-type')
      .description('Returns one or more content types from a web or list')
      .option('--identity <nameOrId>', 'Content type name or ID (e.g. 0x0101)')
      .option('--list <nameOrId>', 'List title, URL, or GUID')
      .option('--in-site-hierarchy', 'Search available content types in site hierarchy')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();

    if (this.parameterSpecified(options, 'list')) {
      const listResolver = new ListResolver(options.list as string);
      const list = await listResolver.resolve(this.sharePointRequestHelper as any, webUrl);
      const listId = (list as any).Id;
      const baseUrl = `${webUrl}/_api/web/lists(guid'${listId}')/contenttypes`;

      if (this.parameterSpecified(options, 'identity')) {
        const ctResolver = new ContentTypeResolver(options.identity as string);
        const ct = await ctResolver.resolve(this.sharePointRequestHelper as any, webUrl, listId);
        this.writeOutput(ct);
      } else {
        const result = await this.sharePointRequestHelper.getTyped<{ value: unknown[] }>(baseUrl);
        this.writeOutput(result?.value);
      }
    } else {
      const endpoint = options.inSiteHierarchy
        ? `${webUrl}/_api/web/availablecontenttypes`
        : `${webUrl}/_api/web/contenttypes`;

      if (this.parameterSpecified(options, 'identity')) {
        const identity = options.identity as string;
        // Content type IDs start with 0x
        if (identity.startsWith('0x')) {
          const url = `${endpoint}('${identity}')`;
          const ct = await this.sharePointRequestHelper.getTyped<any>(url);
          this.writeOutput(ct);
        } else {
          const url = `${endpoint}?$filter=Name eq '${encodeURIComponent(identity)}'`;
          const result = await this.sharePointRequestHelper.getTyped<{ value: unknown[] }>(url);
          this.writeOutput(result?.value?.[0] ?? null);
        }
      } else {
        const result = await this.sharePointRequestHelper.getTyped<{ value: unknown[] }>(endpoint);
        this.writeOutput(result?.value);
      }
    }
  }
}
