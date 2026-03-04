import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

/**
 * Gets subwebs of the current web via /_api/web/webs.
 * Equivalent to Get-PnPSubWeb in PnP PowerShell.
 */
export class GetSubwebsCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-subwebs')
      .description('Returns the subwebs of the current web')
      .option('--identity <urlOrId>', 'Server-relative URL or GUID of a specific subweb to retrieve')
      .option('--recurse', 'Recursively retrieve all subwebs')
      .option('--include-root-web', 'Include the root web in the output')
      .option('--select <fields>', 'Comma-separated list of fields to select')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const results: unknown[] = [];

    // Build $select parameter
    let selectParam = '';
    if (this.parameterSpecified(options, 'select')) {
      selectParam = `?$select=${(options.select as string).split(',').map((f) => f.trim()).join(',')}`;
    }

    // Include root web if requested
    if (options.includeRootWeb) {
      const rootWeb = await this.sharePointRequestHelper.getTyped<Record<string, unknown>>(
        `${webUrl}/_api/web${selectParam}`,
      );
      if (rootWeb) {
        results.push(rootWeb);
      }
    }

    if (this.parameterSpecified(options, 'identity')) {
      const identity = options.identity as string;
      let targetWebUrl: string;

      if (/^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i.test(identity)) {
        // Resolve by GUID
        const webInfo = await this.sharePointRequestHelper.getTyped<any>(
          `${webUrl}/_api/site/openWebById('${identity}')`,
        );
        if (!webInfo) {
          throw new Error(`No subweb found with ID: ${identity}`);
        }
        targetWebUrl = webInfo.d?.Url || webInfo.Url;
      } else {
        // Server-relative URL
        const baseUrl = new URL(webUrl);
        const relativePath = identity.startsWith('/') ? identity : `/${identity}`;
        targetWebUrl = `${baseUrl.protocol}//${baseUrl.host}${relativePath}`;
      }

      // Get the specific web
      const specificWeb = await this.sharePointRequestHelper.getTyped<Record<string, unknown>>(
        `${targetWebUrl}/_api/web${selectParam}`,
      );
      if (specificWeb) {
        results.push(specificWeb);
      }

      if (options.recurse) {
        const childWebs = await this.getSubwebsRecursive(targetWebUrl, selectParam);
        results.push(...childWebs);
      }
    } else {
      // Get all subwebs of the current web
      const subwebs = await this.sharePointRequestHelper.getTyped<{ value: Record<string, unknown>[] }>(
        `${webUrl}/_api/web/webs${selectParam}`,
      );

      if (subwebs?.value) {
        results.push(...subwebs.value);

        if (options.recurse) {
          for (const subweb of subwebs.value) {
            const subwebUrl = (subweb.Url || subweb.url) as string;
            if (subwebUrl) {
              const childWebs = await this.getSubwebsRecursive(subwebUrl, selectParam);
              results.push(...childWebs);
            }
          }
        }
      }
    }

    this.writeOutput(results);
  }

  private async getSubwebsRecursive(
    parentUrl: string,
    selectParam: string,
  ): Promise<Record<string, unknown>[]> {
    const results: Record<string, unknown>[] = [];

    const subwebs = await this.sharePointRequestHelper.getTyped<{ value: Record<string, unknown>[] }>(
      `${parentUrl}/_api/web/webs${selectParam}`,
    );

    if (subwebs?.value) {
      for (const subweb of subwebs.value) {
        results.push(subweb);
        const subwebUrl = (subweb.Url || subweb.url) as string;
        if (subwebUrl) {
          const childWebs = await this.getSubwebsRecursive(subwebUrl, selectParam);
          results.push(...childWebs);
        }
      }
    }

    return results;
  }
}
