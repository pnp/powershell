import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class RemoveFolderCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('remove-folder')
      .description('Removes a folder from the current web')
      .requiredOption('--name <name>', 'Name of the folder to remove')
      .requiredOption('--folder <folder>', 'Site relative URL of the parent folder')
      .option('--recycle', 'Send the folder to the recycle bin instead of permanently deleting it')
      .option('--force', 'Skip confirmation prompt')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const webPath = new URL(webUrl).pathname.replace(/\/$/, '');
    const parentFolderUrl = options.folder as string;
    const folderName = options.name as string;

    // Build the server-relative URL for the folder to remove
    const parentServerRelativeUrl = parentFolderUrl.startsWith('/')
      ? parentFolderUrl
      : `${webPath}/${parentFolderUrl}`;

    const folderServerRelativeUrl = `${parentServerRelativeUrl}/${folderName}`;

    if (options.recycle) {
      // Recycle the folder
      const result = await this.sharePointRequestHelper.post<Record<string, unknown>>(
        `${webUrl}/_api/web/GetFolderByServerRelativeUrl('${encodeURIComponent(folderServerRelativeUrl)}')/recycle()`,
      );
      this.writeOutput(result);
    } else {
      // Permanently delete the folder
      const response = await this.sharePointRequestHelper.delete(
        `${webUrl}/_api/web/GetFolderByServerRelativeUrl('${encodeURIComponent(folderServerRelativeUrl)}')`,
        {
          'IF-MATCH': '*',
        },
      );

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(`Failed to delete folder: HTTP ${response.status} - ${errorText}`);
      }

      this.writeVerbose(`Folder '${folderServerRelativeUrl}' deleted successfully.`);
    }
  }
}
