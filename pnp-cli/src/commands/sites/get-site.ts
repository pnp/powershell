import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

/**
 * Gets site collection properties via the /_api/site endpoint (SPSite level).
 * Equivalent to Get-PnPSite in PnP PowerShell.
 */
export class GetSiteCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-site')
      .description('Returns the current site collection from the connected web')
      .option('--select <fields>', 'Comma-separated list of fields to select (e.g. Url,Id,CompatibilityLevel)')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();

    let endpoint = `${webUrl}/_api/site`;

    if (this.parameterSpecified(options, 'select')) {
      const selectFields = (options.select as string).split(',').map((f) => f.trim()).join(',');
      endpoint += `?$select=${selectFields}`;
    }

    const result = await this.sharePointRequestHelper.getTyped<Record<string, unknown>>(endpoint);
    this.writeOutput(result);
  }
}
