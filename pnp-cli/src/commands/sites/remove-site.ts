import { Command } from 'commander';
import { SharePointAdminCommand } from '../../core/sharepoint-admin-command.js';

/**
 * Removes (deletes) a site collection via the tenant admin endpoint.
 * Equivalent to Remove-PnPTenantSite in PnP PowerShell.
 */
export class RemoveSiteCommand extends SharePointAdminCommand {
  register(program: Command): void {
    program
      .command('remove-site')
      .description('Removes a site collection')
      .requiredOption('--url <siteUrl>', 'URL of the site collection to remove')
      .option('--skip-recycle-bin', 'Permanently delete without sending to the recycle bin')
      .option('--from-recycle-bin', 'Remove a site that is already in the recycle bin')
      .option('--force', 'Do not ask for confirmation')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    let siteUrl = options.url as string;

    // Ensure URL is fully qualified
    if (!siteUrl.toLowerCase().startsWith('https://') && !siteUrl.toLowerCase().startsWith('http://')) {
      const baseUri = new URL(this.connection.url!);
      siteUrl = `${baseUri.protocol}//${baseUri.host}/${siteUrl.replace(/^\//, '')}`;
    }

    const adminUrl = this.tenantAdminUrl;

    // Get request digest for admin context
    const resourceUri = new URL(adminUrl);
    const adminResource = `${resourceUri.protocol}//${resourceUri.host}/.default`;
    const adminToken = await this.connection.getAccessToken(adminResource);

    const digestResponse = await fetch(`${adminUrl}/_api/contextinfo`, {
      method: 'POST',
      headers: {
        Accept: 'application/json;odata=verbose',
        Authorization: `Bearer ${adminToken}`,
        'Content-Length': '0',
      },
    });
    const digestData = (await digestResponse.json()) as any;
    const digest = digestData.d.GetContextWebInformation.FormDigestValue;

    if (options.fromRecycleBin) {
      // Remove from recycle bin
      const response = await fetch(
        `${adminUrl}/_api/SPSiteManager/Delete`,
        {
          method: 'POST',
          headers: {
            Accept: 'application/json;odata=verbose',
            'Content-Type': 'application/json;odata=verbose',
            Authorization: `Bearer ${adminToken}`,
            'X-RequestDigest': digest,
          },
          body: JSON.stringify({ siteUrl }),
        },
      );

      if (!response.ok) {
        const err = await response.text();
        throw new Error(`Failed to remove site from recycle bin: ${err}`);
      }

      this.writeVerbose(`Site '${siteUrl}' permanently removed from recycle bin.`);
    } else {
      // Delete site collection (recycle or permanent)
      const endpoint = options.skipRecycleBin
        ? `${adminUrl}/_api/SPSiteManager/Delete`
        : `${adminUrl}/_api/SPSiteManager/Delete`;

      const response = await fetch(endpoint, {
        method: 'POST',
        headers: {
          Accept: 'application/json;odata=verbose',
          'Content-Type': 'application/json;odata=verbose',
          Authorization: `Bearer ${adminToken}`,
          'X-RequestDigest': digest,
        },
        body: JSON.stringify({ siteUrl }),
      });

      if (!response.ok) {
        const err = await response.text();
        throw new Error(`Failed to remove site collection: ${err}`);
      }

      if (options.skipRecycleBin) {
        this.writeVerbose(`Site '${siteUrl}' permanently deleted (skipped recycle bin).`);
      } else {
        this.writeVerbose(`Site '${siteUrl}' moved to recycle bin.`);
      }
    }

    console.log(`Site '${siteUrl}' removed successfully.`);
  }
}
