import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class CopyFileCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('copy-file')
      .description('Copies a file to another location within the same site collection or across site collections')
      .requiredOption('--source-url <sourceUrl>', 'Server relative URL of the source file')
      .requiredOption('--target-url <targetUrl>', 'Server relative URL or absolute URL of the target location')
      .option('--overwrite', 'Overwrite the target file if it already exists')
      .option('--ignore-version-history', 'Do not copy version history')
      .option('--allow-schema-mismatch', 'Allow schema mismatch between source and target')
      .option('--no-wait', 'Do not wait for the copy job to complete')
      .option('--force', 'Skip confirmation prompt')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const webPath = new URL(webUrl).pathname.replace(/\/$/, '');
    let sourceUrl = options.sourceUrl as string;
    let targetUrl = options.targetUrl as string;

    // Make URLs server-relative if they are not already
    if (!sourceUrl.startsWith('/')) {
      sourceUrl = `${webPath}/${sourceUrl}`;
    }
    if (!targetUrl.startsWith('https://') && !targetUrl.startsWith('/')) {
      targetUrl = `${webPath}/${targetUrl}`;
    }

    const overwrite = options.overwrite === true;

    // Build the absolute URLs for the copy operation
    const siteUrl = new URL(webUrl);
    const absoluteSourceUrl = `${siteUrl.origin}${sourceUrl}`;
    const absoluteTargetUrl = targetUrl.startsWith('https://')
      ? targetUrl
      : `${siteUrl.origin}${targetUrl}`;

    // Use SP.MoveCopyUtil.CopyFileByPath for the copy operation
    const requestBody = {
      srcPath: {
        __metadata: { type: 'SP.ResourcePath' },
        DecodedUrl: absoluteSourceUrl,
      },
      destPath: {
        __metadata: { type: 'SP.ResourcePath' },
        DecodedUrl: absoluteTargetUrl,
      },
      overwrite: overwrite,
      options: {
        __metadata: { type: 'SP.MoveCopyOptions' },
        KeepBoth: false,
        ResetAuthorAndCreatedOnCopy: false,
        ShouldBypassSharedLocks: true,
      },
    };

    try {
      const result = await this.sharePointRequestHelper.post<Record<string, unknown>>(
        `${webUrl}/_api/SP.MoveCopyUtil.CopyFileByPath()`,
        requestBody,
      );

      if (result) {
        this.writeOutput(result);
      } else {
        this.writeVerbose(`File copied from '${sourceUrl}' to '${targetUrl}' successfully.`);
      }
    } catch (err) {
      // Fallback to simpler copy endpoint for same-web scenarios
      this.writeVerbose('Falling back to CopyTo endpoint');
      await this.sharePointRequestHelper.post(
        `${webUrl}/_api/web/GetFileByServerRelativeUrl('${encodeURIComponent(sourceUrl)}')/CopyTo(strNewUrl='${encodeURIComponent(targetUrl)}',bOverWrite=${overwrite})`,
      );
      this.writeVerbose(`File copied from '${sourceUrl}' to '${targetUrl}' successfully.`);
    }
  }
}
