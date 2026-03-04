import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class RemoveFileCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('remove-file')
      .description('Removes a file from the current web')
      .requiredOption('--url <url>', 'Server relative URL or site relative URL of the file')
      .option('--recycle', 'Send the file to the recycle bin instead of permanently deleting it')
      .option('--force', 'Skip confirmation prompt')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    let fileUrl = options.url as string;

    // Ensure server-relative URL
    fileUrl = this.ensureServerRelativeUrl(webUrl, fileUrl);

    if (options.recycle) {
      // Recycle the file
      const result = await this.sharePointRequestHelper.post<Record<string, unknown>>(
        `${webUrl}/_api/web/GetFileByServerRelativeUrl('${encodeURIComponent(fileUrl)}')/recycle()`,
      );
      this.writeOutput(result);
    } else {
      // Permanently delete the file
      const response = await this.sharePointRequestHelper.delete(
        `${webUrl}/_api/web/GetFileByServerRelativeUrl('${encodeURIComponent(fileUrl)}')`,
        {
          'IF-MATCH': '*',
        },
      );

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(`Failed to delete file: HTTP ${response.status} - ${errorText}`);
      }

      this.writeVerbose(`File '${fileUrl}' deleted successfully.`);
    }
  }

  private ensureServerRelativeUrl(webUrl: string, fileUrl: string): string {
    if (fileUrl.startsWith('https://') || fileUrl.startsWith('http://')) {
      try {
        const url = new URL(fileUrl);
        fileUrl = url.pathname;
      } catch {
        // leave as-is
      }
    }

    if (!fileUrl.startsWith('/')) {
      const webPath = new URL(webUrl).pathname;
      fileUrl = `${webPath.replace(/\/$/, '')}/${fileUrl}`;
    }

    return fileUrl;
  }
}
