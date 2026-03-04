import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

/**
 * Updates site collection properties.
 * Equivalent to Set-PnPSite in PnP PowerShell.
 *
 * For site-level properties (Classification, SensitivityLabel), uses /_api/site directly.
 * For tenant-level properties (SharingCapability, LockState, StorageMaximumLevel, etc.),
 * a tenant admin context would be required. This implementation covers the REST-accessible
 * site-level properties.
 */
export class SetSiteCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('set-site')
      .description('Sets site collection properties')
      .option('--identity <url>', 'URL of the site collection to update (defaults to connected site)')
      .option('--classification <value>', 'The classification to set on the site')
      .option('--disable-flows', 'Disable Power Automate flows on the site')
      .option('--enable-flows', 'Enable Power Automate flows on the site')
      .option('--no-script-site', 'Set the site to NoScript mode (DenyAddAndCustomizePages)')
      .option('--allow-script-site', 'Allow custom scripts on the site')
      .option('--comments-on-site-pages-disabled', 'Disable comments on site pages')
      .option('--enable-comments-on-site-pages', 'Enable comments on site pages')
      .option('--sharing-capability <value>', 'Sharing capability: Disabled, ExternalUserSharingOnly, ExternalUserAndGuestSharing, ExistingExternalUserSharingOnly')
      .option('--storage-maximum-level <bytes>', 'Maximum storage level in megabytes')
      .option('--storage-warning-level <bytes>', 'Warning storage level in megabytes')
      .option('--locale-id <lcid>', 'Locale ID for the site')
      .option('--lock-state <state>', 'Lock state: Unlock, NoAdditions, ReadOnly, NoAccess')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const siteUrl = (options.identity as string) || this.getWebUrl();

    // Build the update payload for REST-accessible site properties
    const sitePayload: Record<string, unknown> = {};
    let hasSiteUpdates = false;

    if (this.parameterSpecified(options, 'classification')) {
      sitePayload['Classification'] = options.classification as string;
      hasSiteUpdates = true;
    }

    if (hasSiteUpdates) {
      // Use PATCH on /_api/site to update site-level properties
      const accessToken = await this.accessToken;
      const digestResponse = await fetch(`${siteUrl}/_api/contextinfo`, {
        method: 'POST',
        headers: {
          Accept: 'application/json;odata=verbose',
          Authorization: `Bearer ${accessToken}`,
          'Content-Length': '0',
        },
      });
      const digestData = (await digestResponse.json()) as any;
      const digest = digestData.d.GetContextWebInformation.FormDigestValue;

      await this.sharePointRequestHelper.patch(
        `${siteUrl}/_api/site`,
        sitePayload,
        {
          'X-RequestDigest': digest,
          'IF-MATCH': '*',
        },
      );
    }

    // Tenant-level properties require the admin endpoint
    const tenantProps: Record<string, unknown> = {};
    let hasTenantUpdates = false;

    if (this.parameterSpecified(options, 'sharingCapability')) {
      tenantProps['SharingCapability'] = options.sharingCapability;
      hasTenantUpdates = true;
    }
    if (this.parameterSpecified(options, 'storageMaximumLevel')) {
      tenantProps['StorageMaximumLevel'] = Number(options.storageMaximumLevel);
      hasTenantUpdates = true;
    }
    if (this.parameterSpecified(options, 'storageWarningLevel')) {
      tenantProps['StorageWarningLevel'] = Number(options.storageWarningLevel);
      hasTenantUpdates = true;
    }
    if (this.parameterSpecified(options, 'localeId')) {
      tenantProps['Lcid'] = Number(options.localeId);
      hasTenantUpdates = true;
    }
    if (options.noScriptSite) {
      tenantProps['DenyAddAndCustomizePages'] = 'Enabled';
      hasTenantUpdates = true;
    }
    if (options.allowScriptSite) {
      tenantProps['DenyAddAndCustomizePages'] = 'Disabled';
      hasTenantUpdates = true;
    }
    if (options.disableFlows) {
      tenantProps['DisableFlows'] = 'Disabled';
      hasTenantUpdates = true;
    }
    if (options.enableFlows) {
      tenantProps['DisableFlows'] = 'NotDisabled';
      hasTenantUpdates = true;
    }
    if (options.commentsOnSitePagesDisabled) {
      tenantProps['CommentsOnSitePagesDisabled'] = true;
      hasTenantUpdates = true;
    }
    if (options.enableCommentsOnSitePages) {
      tenantProps['CommentsOnSitePagesDisabled'] = false;
      hasTenantUpdates = true;
    }
    if (this.parameterSpecified(options, 'lockState')) {
      tenantProps['LockState'] = options.lockState;
      hasTenantUpdates = true;
    }

    if (hasTenantUpdates) {
      // Tenant admin site properties update via REST
      const adminUrl = this.connection.tenantAdminUrl || this.deriveTenantAdminUrl(siteUrl);
      const resourceUri = new URL(adminUrl);
      const adminResource = `${resourceUri.protocol}//${resourceUri.host}/.default`;
      const adminToken = await this.connection.getAccessToken(adminResource);

      const adminDigestResponse = await fetch(`${adminUrl}/_api/contextinfo`, {
        method: 'POST',
        headers: {
          Accept: 'application/json;odata=verbose',
          Authorization: `Bearer ${adminToken}`,
          'Content-Length': '0',
        },
      });
      const adminDigestData = (await adminDigestResponse.json()) as any;
      const adminDigest = adminDigestData.d.GetContextWebInformation.FormDigestValue;

      const updatePayload = {
        '__metadata': { 'type': 'Microsoft.Online.SharePoint.TenantAdministration.SiteProperties' },
        ...tenantProps,
      };

      const response = await fetch(
        `${adminUrl}/_api/SPOSitePropertiesEnumerable/GetByUrl('${encodeURIComponent(siteUrl)}')`,
        {
          method: 'PATCH',
          headers: {
            Accept: 'application/json;odata=verbose',
            'Content-Type': 'application/json;odata=verbose',
            Authorization: `Bearer ${adminToken}`,
            'X-RequestDigest': adminDigest,
            'IF-MATCH': '*',
          },
          body: JSON.stringify(updatePayload),
        },
      );

      if (!response.ok) {
        const err = await response.text();
        throw new Error(`Failed to update tenant site properties: ${err}`);
      }
    }

    if (!hasSiteUpdates && !hasTenantUpdates) {
      this.writeWarning('No properties specified to update.');
      return;
    }

    this.writeVerbose('Site properties updated successfully.');
  }

  private deriveTenantAdminUrl(siteUrl: string): string {
    const url = new URL(siteUrl);
    const adminHostname = url.hostname.replace('.sharepoint.', '-admin.sharepoint.');
    return `${url.protocol}//${adminHostname}`;
  }
}
