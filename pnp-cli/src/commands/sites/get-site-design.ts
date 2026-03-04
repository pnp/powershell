import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

/**
 * Gets site designs from the tenant.
 * Equivalent to Get-PnPSiteDesign in PnP PowerShell.
 * Uses the SiteScriptUtility REST endpoint.
 */
export class GetSiteDesignCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-site-design')
      .description('Returns available site designs, optionally filtered by identity')
      .option('--identity <id>', 'Site design ID (GUID) to retrieve a specific design')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();

    if (this.parameterSpecified(options, 'identity')) {
      const designId = options.identity as string;

      // Get a specific site design by ID
      const result = await this.sharePointRequestHelper.post<Record<string, unknown>>(
        `${webUrl}/_api/Microsoft.SharePoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesignMetadata`,
        { id: designId },
      );

      if (!result) {
        this.writeWarning(`No site design found with ID '${designId}'.`);
        return;
      }

      this.writeOutput(result);
    } else {
      // Get all site designs
      const result = await this.sharePointRequestHelper.post<{ value: unknown[] }>(
        `${webUrl}/_api/Microsoft.SharePoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesigns`,
        {},
      );

      this.writeOutput(result?.value ?? []);
    }
  }
}
