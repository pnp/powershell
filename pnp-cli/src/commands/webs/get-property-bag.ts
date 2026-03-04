import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

/**
 * Gets web property bag values via /_api/web/AllProperties.
 * Equivalent to Get-PnPPropertyBag in PnP PowerShell.
 */
export class GetPropertyBagCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-property-bag')
      .description('Returns the property bag values of the current web')
      .option('--key <key>', 'Return only the value of a specific key')
      .option('--folder <folderPath>', 'Server-relative folder path to get properties from')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();

    if (this.parameterSpecified(options, 'folder')) {
      // Folder-level property bag
      const folder = options.folder as string;
      const folderRelativeUrl = folder.startsWith('/') ? folder : `/${folder}`;

      const result = await this.sharePointRequestHelper.getTyped<Record<string, unknown>>(
        `${webUrl}/_api/web/GetFolderByServerRelativeUrl('${encodeURIComponent(folderRelativeUrl)}')/Properties`,
      );

      if (!result) {
        throw new Error(`Could not retrieve properties for folder: ${folder}`);
      }

      if (this.parameterSpecified(options, 'key')) {
        const key = options.key as string;
        const value = result[key];
        if (value === undefined) {
          this.writeWarning(`Property '${key}' not found in folder property bag.`);
          return;
        }
        this.writeOutput(value);
      } else {
        // Transform to key-value array for readability
        const entries = Object.entries(result)
          .filter(([k]) => !k.startsWith('odata') && !k.startsWith('__'))
          .map(([k, v]) => ({ Key: k, Value: v }));
        this.writeOutput(entries);
      }
    } else {
      // Web-level property bag
      const result = await this.sharePointRequestHelper.getTyped<Record<string, unknown>>(
        `${webUrl}/_api/web/AllProperties`,
      );

      if (!result) {
        throw new Error('Could not retrieve web properties.');
      }

      if (this.parameterSpecified(options, 'key')) {
        const key = options.key as string;
        const value = result[key];
        if (value === undefined) {
          this.writeWarning(`Property '${key}' not found in property bag.`);
          return;
        }
        this.writeOutput(value);
      } else {
        // Transform to key-value array for readability
        const entries = Object.entries(result)
          .filter(([k]) => !k.startsWith('odata') && !k.startsWith('__'))
          .map(([k, v]) => ({ Key: k, Value: v }));
        this.writeOutput(entries);
      }
    }
  }
}
