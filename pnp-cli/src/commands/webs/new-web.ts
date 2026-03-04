import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

/**
 * Creates a new subweb via POST /_api/web/webs/add.
 * Equivalent to New-PnPWeb in PnP PowerShell.
 */
export class NewWebCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('new-web')
      .description('Creates a new subweb under the current web')
      .requiredOption('--title <title>', 'The title of the new web')
      .requiredOption('--url <url>', 'The leaf URL name of the new web')
      .requiredOption('--template <template>', 'The web template to use (e.g. STS#3, GROUP#0)')
      .option('--description <description>', 'The description of the new web')
      .option('--locale <lcid>', 'The locale ID for the new web (default: 1033)', '1033')
      .option('--break-inheritance', 'Break role inheritance from the parent web')
      .option('--no-inherit-navigation', 'Do not inherit the navigation from the parent web')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const locale = Number(options.locale) || 1033;
    const useUniquePermissions = options.breakInheritance === true;
    // Commander's --no-inherit-navigation sets inheritNavigation to false
    const inheritNavigation = options.inheritNavigation !== false;

    // Get request digest
    const accessToken = await this.accessToken;
    const digestResponse = await fetch(`${webUrl}/_api/contextinfo`, {
      method: 'POST',
      headers: {
        Accept: 'application/json;odata=verbose',
        Authorization: `Bearer ${accessToken}`,
        'Content-Length': '0',
      },
    });
    const digestData = (await digestResponse.json()) as any;
    const digest = digestData.d.GetContextWebInformation.FormDigestValue;

    const payload = {
      parameters: {
        __metadata: { type: 'SP.WebCreationInformation' },
        Title: options.title as string,
        Url: options.url as string,
        Description: (options.description as string) || '',
        Language: locale,
        WebTemplate: options.template as string,
        UseUniquePermissions: useUniquePermissions,
      },
    };

    const result = await this.sharePointRequestHelper.post<Record<string, unknown>>(
      `${webUrl}/_api/web/webs/add`,
      payload,
      {
        'X-RequestDigest': digest,
      },
    );

    if (!result) {
      throw new Error('Failed to create the new web. No response returned.');
    }

    this.writeOutput(result);
  }
}
