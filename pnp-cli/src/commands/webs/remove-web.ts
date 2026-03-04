import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

/**
 * Deletes a web (subweb).
 * Equivalent to Remove-PnPWeb in PnP PowerShell.
 */
export class RemoveWebCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('remove-web')
      .description('Removes a subweb')
      .requiredOption('--identity <urlOrId>', 'Server-relative URL or GUID of the web to remove')
      .option('--force', 'Do not ask for confirmation')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const identity = options.identity as string;

    // Resolve the web URL
    let targetWebUrl: string;

    if (/^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i.test(identity)) {
      // Identity is a GUID - resolve it via the site endpoint
      const webInfo = await this.sharePointRequestHelper.getTyped<any>(
        `${webUrl}/_api/site/openWebById('${identity}')`,
      );
      if (!webInfo) {
        throw new Error(`Web not found with ID: ${identity}`);
      }
      targetWebUrl = webInfo.d?.Url || webInfo.Url;
    } else {
      // Identity is a URL
      if (identity.startsWith('https://') || identity.startsWith('http://')) {
        targetWebUrl = identity;
      } else {
        // Server-relative URL - construct the full URL
        const baseUrl = new URL(webUrl);
        const relativePath = identity.startsWith('/') ? identity : `/${identity}`;
        targetWebUrl = `${baseUrl.protocol}//${baseUrl.host}${relativePath}`;
      }
    }

    // Get request digest
    const accessToken = await this.accessToken;
    const digestResponse = await fetch(`${targetWebUrl}/_api/contextinfo`, {
      method: 'POST',
      headers: {
        Accept: 'application/json;odata=verbose',
        Authorization: `Bearer ${accessToken}`,
        'Content-Length': '0',
      },
    });
    const digestData = (await digestResponse.json()) as any;
    const digest = digestData.d.GetContextWebInformation.FormDigestValue;

    // Delete the web using POST with X-HTTP-Method DELETE
    const response = await fetch(`${targetWebUrl}/_api/web`, {
      method: 'POST',
      headers: {
        Accept: 'application/json;odata=verbose',
        Authorization: `Bearer ${accessToken}`,
        'X-RequestDigest': digest,
        'X-HTTP-Method': 'DELETE',
        'IF-MATCH': '*',
      },
    });

    if (!response.ok) {
      const err = await response.text();
      throw new Error(`Failed to delete web: ${err}`);
    }

    console.log(`Web '${targetWebUrl}' deleted successfully.`);
  }
}
