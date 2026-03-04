import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';
import * as fs from 'fs';
import * as path from 'path';

export class GetFileCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-file')
      .description('Returns a file from the current web')
      .requiredOption('--url <url>', 'Server relative URL or site relative URL of the file')
      .option('--as-file', 'Download the file to disk')
      .option('--as-string', 'Return the file contents as a string')
      .option('--as-list-item', 'Return the file as a list item with all field values')
      .option('--path <path>', 'Local folder path to save the file to (used with --as-file)')
      .option('--filename <filename>', 'Name for the local file (used with --as-file)')
      .option('--force', 'Overwrite the file if it already exists locally')
      .option('--throw-exception-if-file-not-found', 'Throw an error if the file is not found')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    let fileUrl = options.url as string;

    // Make the URL server-relative if it is not already
    fileUrl = this.ensureServerRelativeUrl(webUrl, fileUrl);

    if (options.asFile) {
      // Download file to local disk
      const localPath = (options.path as string) || process.cwd();
      const response = await this.sharePointRequestHelper.getResponse(
        `${webUrl}/_api/web/GetFileByServerRelativeUrl('${encodeURIComponent(fileUrl)}')/$value`,
      );

      if (!response.ok) {
        throw new Error(`Failed to download file: HTTP ${response.status}`);
      }

      const buffer = Buffer.from(await response.arrayBuffer());
      const defaultFilename = fileUrl.substring(fileUrl.lastIndexOf('/') + 1);
      const filename = (options.filename as string) || defaultFilename;
      const filePath = path.join(localPath, filename);

      if (fs.existsSync(filePath) && !options.force) {
        this.writeWarning(
          `File '${filename}' already exists. Use --force to overwrite.`,
        );
        return;
      }

      fs.writeFileSync(filePath, buffer);
      this.writeVerbose(`File saved to ${filePath}`);
      this.writeOutput({ FilePath: filePath, FileName: filename, Size: buffer.length });
    } else if (options.asString) {
      // Return file contents as a string
      const content = await this.sharePointRequestHelper.get(
        `${webUrl}/_api/web/GetFileByServerRelativeUrl('${encodeURIComponent(fileUrl)}')/$value`,
      );
      this.writeOutput(content);
    } else if (options.asListItem) {
      // Return the file as a list item
      try {
        const result = await this.sharePointRequestHelper.getTyped<Record<string, unknown>>(
          `${webUrl}/_api/web/GetFileByServerRelativeUrl('${encodeURIComponent(fileUrl)}')/ListItemAllFields`,
        );
        this.writeOutput(result);
      } catch (err) {
        if (options.throwExceptionIfFileNotFound) {
          throw new Error(`File not found: ${fileUrl}`);
        }
        this.writeOutput(null);
      }
    } else {
      // Default: return file metadata
      try {
        const result = await this.sharePointRequestHelper.getTyped<Record<string, unknown>>(
          `${webUrl}/_api/web/GetFileByServerRelativeUrl('${encodeURIComponent(fileUrl)}')`,
        );
        this.writeOutput(result);
      } catch (err) {
        if (options.throwExceptionIfFileNotFound) {
          throw new Error(`File not found: ${fileUrl}`);
        }
        this.writeOutput(null);
      }
    }
  }

  private ensureServerRelativeUrl(webUrl: string, fileUrl: string): string {
    // If it is an absolute URL, extract the path
    if (fileUrl.startsWith('https://') || fileUrl.startsWith('http://')) {
      try {
        const url = new URL(fileUrl);
        fileUrl = url.pathname;
      } catch {
        // leave as-is
      }
    }

    // If it does not start with /, treat it as site-relative
    if (!fileUrl.startsWith('/')) {
      const webPath = new URL(webUrl).pathname;
      fileUrl = `${webPath.replace(/\/$/, '')}/${fileUrl}`;
    }

    return fileUrl;
  }
}
