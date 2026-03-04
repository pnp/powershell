import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

/**
 * Updates web properties via PATCH /_api/web.
 * Equivalent to Set-PnPWeb in PnP PowerShell.
 */
export class SetWebCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('set-web')
      .description('Sets web properties')
      .option('--title <title>', 'The title of the web')
      .option('--description <description>', 'The description of the web')
      .option('--site-logo-url <url>', 'The URL of the site logo')
      .option('--alternate-css-url <url>', 'The alternate CSS URL')
      .option('--master-url <url>', 'The URL of the master page')
      .option('--custom-master-url <url>', 'The URL of the custom master page')
      .option('--quick-launch-enabled', 'Enable the quick launch navigation')
      .option('--no-quick-launch', 'Disable the quick launch navigation')
      .option('--members-can-share', 'Allow members to share the site')
      .option('--no-members-share', 'Prevent members from sharing the site')
      .option('--no-crawl', 'Disable search crawling for this web')
      .option('--allow-crawl', 'Enable search crawling for this web')
      .option('--mega-menu-enabled', 'Enable mega menu navigation')
      .option('--no-mega-menu', 'Disable mega menu navigation')
      .option('--comments-on-site-pages-disabled', 'Disable comments on site pages')
      .option('--enable-comments-on-site-pages', 'Enable comments on site pages')
      .option('--disable-power-automate', 'Disable Power Automate flows')
      .option('--enable-power-automate', 'Enable Power Automate flows')
      .option('--hide-title-in-header', 'Hide the title in the header')
      .option('--show-title-in-header', 'Show the title in the header')
      .option('--horizontal-quick-launch', 'Enable horizontal quick launch')
      .option('--vertical-quick-launch', 'Use vertical quick launch')
      .option('--nav-audience-targeting-enabled', 'Enable audience targeting for navigation')
      .option('--no-nav-audience-targeting', 'Disable audience targeting for navigation')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();

    const payload: Record<string, unknown> = {};
    let dirty = false;

    if (this.parameterSpecified(options, 'title')) {
      payload['Title'] = options.title;
      dirty = true;
    }
    if (this.parameterSpecified(options, 'description')) {
      payload['Description'] = options.description;
      dirty = true;
    }
    if (this.parameterSpecified(options, 'siteLogoUrl')) {
      payload['SiteLogoUrl'] = options.siteLogoUrl;
      dirty = true;
    }
    if (this.parameterSpecified(options, 'alternateCssUrl')) {
      payload['AlternateCssUrl'] = options.alternateCssUrl;
      dirty = true;
    }
    if (this.parameterSpecified(options, 'masterUrl')) {
      payload['MasterUrl'] = options.masterUrl;
      dirty = true;
    }
    if (this.parameterSpecified(options, 'customMasterUrl')) {
      payload['CustomMasterUrl'] = options.customMasterUrl;
      dirty = true;
    }
    if (options.quickLaunchEnabled) {
      payload['QuickLaunchEnabled'] = true;
      dirty = true;
    }
    if (options.noQuickLaunch) {
      payload['QuickLaunchEnabled'] = false;
      dirty = true;
    }
    if (options.membersCanShare) {
      payload['MembersCanShare'] = true;
      dirty = true;
    }
    if (options.noMembersShare) {
      payload['MembersCanShare'] = false;
      dirty = true;
    }
    if (options.noCrawl) {
      payload['NoCrawl'] = true;
      dirty = true;
    }
    if (options.allowCrawl) {
      payload['NoCrawl'] = false;
      dirty = true;
    }
    if (options.megaMenuEnabled) {
      payload['MegaMenuEnabled'] = true;
      dirty = true;
    }
    if (options.noMegaMenu) {
      payload['MegaMenuEnabled'] = false;
      dirty = true;
    }
    if (options.commentsOnSitePagesDisabled) {
      payload['CommentsOnSitePagesDisabled'] = true;
      dirty = true;
    }
    if (options.enableCommentsOnSitePages) {
      payload['CommentsOnSitePagesDisabled'] = false;
      dirty = true;
    }
    if (options.disablePowerAutomate) {
      payload['DisableFlows'] = true;
      dirty = true;
    }
    if (options.enablePowerAutomate) {
      payload['DisableFlows'] = false;
      dirty = true;
    }
    if (options.hideTitleInHeader) {
      payload['HideTitleInHeader'] = true;
      dirty = true;
    }
    if (options.showTitleInHeader) {
      payload['HideTitleInHeader'] = false;
      dirty = true;
    }
    if (options.horizontalQuickLaunch) {
      payload['HorizontalQuickLaunch'] = true;
      dirty = true;
    }
    if (options.verticalQuickLaunch) {
      payload['HorizontalQuickLaunch'] = false;
      dirty = true;
    }
    if (options.navAudienceTargetingEnabled) {
      payload['NavAudienceTargetingEnabled'] = true;
      dirty = true;
    }
    if (options.noNavAudienceTargeting) {
      payload['NavAudienceTargetingEnabled'] = false;
      dirty = true;
    }

    if (!dirty) {
      this.writeWarning('No properties specified to update.');
      return;
    }

    // Get request digest for the PATCH operation
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

    await this.sharePointRequestHelper.patch(
      `${webUrl}/_api/web`,
      payload,
      {
        'X-RequestDigest': digest,
        'IF-MATCH': '*',
      },
    );

    this.writeVerbose('Web properties updated successfully.');
  }
}
