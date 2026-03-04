import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class RemoveContentTypeCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('remove-content-type')
      .description('Remove a content type from the web')
      .requiredOption('--identity <nameOrId>', 'Content type name or ID (e.g. 0x0101)')
      .option('--force', 'Skip confirmation')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const identity = options.identity as string;

    let ctUrl: string;
    if (identity.startsWith('0x')) {
      ctUrl = `${webUrl}/_api/web/contenttypes('${identity}')`;
    } else {
      // Resolve by name first
      const searchUrl = `${webUrl}/_api/web/contenttypes?$filter=Name eq '${encodeURIComponent(identity)}'`;
      const result = await this.sharePointRequestHelper.getTyped<{ value: any[] }>(searchUrl);
      if (!result?.value?.length) {
        throw new Error(`Content type '${identity}' not found`);
      }
      const ctId = result.value[0].StringId || result.value[0].Id?.StringValue;
      ctUrl = `${webUrl}/_api/web/contenttypes('${ctId}')`;
    }

    await this.sharePointRequestHelper.delete(ctUrl);
    this.writeOutput({ message: `Content type '${identity}' removed successfully` });
  }
}
