import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class MoveFileCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('move-file')
      .description('Moves a file to another location within the same site collection or across site collections')
      .requiredOption('--source-url <sourceUrl>', 'Server relative URL of the source file')
      .requiredOption('--target-url <targetUrl>', 'Server relative URL or absolute URL of the target location')
      .option('--overwrite', 'Overwrite the target file if it already exists')
      .option('--ignore-version-history', 'Do not preserve version history')
      .option('--allow-schema-mismatch', 'Allow schema mismatch between source and target')
      .option('--allow-smaller-version-limit-on-destination', 'Allow a smaller version limit at the destination')
      .option('--no-wait', 'Do not wait for the move job to complete')
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

    // Build absolute URLs for the move operation
    const siteUrl = new URL(webUrl);
    const absoluteSourceUrl = `${siteUrl.origin}${sourceUrl}`;
    const absoluteTargetUrl = targetUrl.startsWith('https://')
      ? targetUrl
      : `${siteUrl.origin}${targetUrl}`;

    // Use SP.MoveCopyUtil.MoveFileByPath for the move operation
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
        `${webUrl}/_api/SP.MoveCopyUtil.MoveFileByPath()`,
        requestBody,
      );

      if (result) {
        this.writeOutput(result);
      } else {
        this.writeVerbose(`File moved from '${sourceUrl}' to '${targetUrl}' successfully.`);
      }
    } catch (err) {
      // Fallback to simpler MoveTo endpoint for same-web scenarios
      this.writeVerbose('Falling back to MoveTo endpoint');
      const moveFlags = overwrite ? 1 : 0; // MoveOperations.Overwrite = 1, None = 0
      await this.sharePointRequestHelper.post(
        `${webUrl}/_api/web/GetFileByServerRelativeUrl('${encodeURIComponent(sourceUrl)}')/MoveTo(newUrl='${encodeURIComponent(targetUrl)}',flags=${moveFlags})`,
      );
      this.writeVerbose(`File moved from '${sourceUrl}' to '${targetUrl}' successfully.`);
    }
  }
}
