import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class GetFolderCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-folder')
      .description('Returns a folder or all folders in the current web')
      .option('--url <url>', 'Site relative or server relative URL of the folder')
      .option('--list <list>', 'Title, URL or GUID of the list to get the root folder of')
      .option('--current-web-root-folder', 'Return the root folder of the current web')
      .option('--as-list-item', 'Return the folder as a list item with all field values')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();

    if (this.parameterSpecified(options, 'url')) {
      // Get a specific folder by URL
      let folderUrl = options.url as string;
      folderUrl = this.ensureServerRelativeUrl(webUrl, folderUrl);

      if (options.asListItem) {
        const result = await this.sharePointRequestHelper.getTyped<Record<string, unknown>>(
          `${webUrl}/_api/web/GetFolderByServerRelativeUrl('${encodeURIComponent(folderUrl)}')/ListItemAllFields`,
        );
        this.writeOutput(result);
      } else {
        const result = await this.sharePointRequestHelper.getTyped<Record<string, unknown>>(
          `${webUrl}/_api/web/GetFolderByServerRelativeUrl('${encodeURIComponent(folderUrl)}')`,
        );
        this.writeOutput(result);
      }
    } else if (this.parameterSpecified(options, 'list')) {
      // Get root folder of a list
      const listIdentity = options.list as string;
      let listUrl: string;

      // Determine if it is a GUID
      const guidRegex = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;
      if (guidRegex.test(listIdentity)) {
        listUrl = `${webUrl}/_api/web/lists(guid'${listIdentity}')/RootFolder`;
      } else {
        listUrl = `${webUrl}/_api/web/lists/GetByTitle('${encodeURIComponent(listIdentity)}')/RootFolder`;
      }

      if (options.asListItem) {
        const result = await this.sharePointRequestHelper.getTyped<Record<string, unknown>>(
          `${listUrl}/ListItemAllFields`,
        );
        this.writeOutput(result);
      } else {
        const result = await this.sharePointRequestHelper.getTyped<Record<string, unknown>>(listUrl);
        this.writeOutput(result);
      }
    } else if (options.currentWebRootFolder) {
      // Get root folder of the current web
      const result = await this.sharePointRequestHelper.getTyped<Record<string, unknown>>(
        `${webUrl}/_api/web/RootFolder`,
      );
      this.writeOutput(result);
    } else {
      // Get all folders in the root of the current web
      const result = await this.sharePointRequestHelper.getTyped<{ value: unknown[] }>(
        `${webUrl}/_api/web/Folders`,
      );
      this.writeOutput(result?.value);
    }
  }

  private ensureServerRelativeUrl(webUrl: string, folderUrl: string): string {
    if (folderUrl.startsWith('https://') || folderUrl.startsWith('http://')) {
      try {
        const url = new URL(folderUrl);
        folderUrl = url.pathname;
      } catch {
        // leave as-is
      }
    }

    if (!folderUrl.startsWith('/')) {
      const webPath = new URL(webUrl).pathname;
      folderUrl = `${webPath.replace(/\/$/, '')}/${folderUrl}`;
    }

    return folderUrl;
  }
}
