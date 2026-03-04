import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';
import { ListResolver } from '../../resolvers/list-resolver.js';

export class GetListCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-list')
      .description('Returns one or more lists from the current web')
      .option('--identity <nameOrId>', 'List title, URL, or GUID')
      .option('--throw-exception-if-list-not-found', 'Throw an error if the list is not found')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();

    if (this.parameterSpecified(options, 'identity')) {
      const resolver = new ListResolver(options.identity as string);
      try {
        const list = await resolver.resolve(
          this.sharePointRequestHelper as any, // We use the raw REST approach
          webUrl,
        );
        this.writeOutput(list);
      } catch (err) {
        if (options.throwExceptionIfListNotFound) {
          throw new Error(`List not found: ${options.identity}`);
        }
        this.writeOutput(null);
      }
    } else {
      const result = await this.sharePointRequestHelper.getTyped<{ value: unknown[] }>(
        `${webUrl}/_api/web/lists?$filter=Hidden eq false`,
      );
      this.writeOutput(result?.value);
    }
  }
}
