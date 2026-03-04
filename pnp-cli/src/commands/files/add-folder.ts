import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class AddFolderCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('add-folder')
      .description('Creates a new folder in the current web')
      .requiredOption('--name <name>', 'Name of the new folder')
      .requiredOption('--folder <folder>', 'Site relative URL of the parent folder')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const webPath = new URL(webUrl).pathname.replace(/\/$/, '');
    const parentFolderUrl = options.folder as string;
    const folderName = options.name as string;

    // Build the server-relative URL for the new folder
    const parentServerRelativeUrl = parentFolderUrl.startsWith('/')
      ? parentFolderUrl
      : `${webPath}/${parentFolderUrl}`;

    const newFolderUrl = `${parentServerRelativeUrl}/${folderName}`;

    const result = await this.sharePointRequestHelper.post<Record<string, unknown>>(
      `${webUrl}/_api/web/folders/add('${encodeURIComponent(newFolderUrl)}')`,
    );

    this.writeOutput(result);
  }
}
