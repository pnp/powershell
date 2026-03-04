import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

/**
 * Gets web properties via /_api/web.
 * Equivalent to Get-PnPWeb in PnP PowerShell.
 */
export class GetWebCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-web')
      .description('Returns the current web')
      .option('--identity <urlOrId>', 'Server-relative URL or GUID of a specific web to retrieve')
      .option('--select <fields>', 'Comma-separated list of fields to select (e.g. Id,Url,Title,ServerRelativeUrl)')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    let endpoint: string;

    if (this.parameterSpecified(options, 'identity')) {
      const identity = options.identity as string;
      // If it looks like a GUID, use the web by ID
      if (/^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i.test(identity)) {
        endpoint = `${webUrl}/_api/site/openWebById('${identity}')`;
      } else {
        // Treat as a server-relative URL
        const relativeUrl = identity.startsWith('/') ? identity : `/${identity}`;
        endpoint = `${webUrl}/_api/web/webs?$filter=ServerRelativeUrl eq '${relativeUrl}'`;
        const result = await this.sharePointRequestHelper.getTyped<{ value: unknown[] }>(endpoint);
        if (result?.value && result.value.length > 0) {
          this.writeOutput(result.value[0]);
        } else {
          throw new Error(`Web not found with URL: ${identity}`);
        }
        return;
      }
    } else {
      endpoint = `${webUrl}/_api/web`;
    }

    if (this.parameterSpecified(options, 'select')) {
      const selectFields = (options.select as string).split(',').map((f) => f.trim()).join(',');
      const separator = endpoint.includes('?') ? '&' : '?';
      endpoint += `${separator}$select=${selectFields}`;
    }

    const result = await this.sharePointRequestHelper.getTyped<Record<string, unknown>>(endpoint);
    this.writeOutput(result);
  }
}
