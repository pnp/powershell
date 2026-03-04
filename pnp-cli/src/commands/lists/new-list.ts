import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class NewListCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('new-list')
      .description('Creates a new list')
      .requiredOption('--title <title>', 'Title of the new list')
      .requiredOption('--template <template>', 'List template type (e.g., GenericList, DocumentLibrary, Events, Tasks)')
      .option('--url <url>', 'Custom URL for the list')
      .option('--hidden', 'Create as hidden list')
      .option('--enable-versioning', 'Enable versioning')
      .option('--enable-content-types', 'Enable content types')
      .option('--on-quick-launch', 'Show on quick launch')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const template = this.resolveTemplate(options.template as string);

    const listCreationInfo = {
      __metadata: { type: 'SP.List' },
      Title: options.title,
      BaseTemplate: template,
      Hidden: options.hidden || false,
      EnableVersioning: options.enableVersioning || false,
      ContentTypesEnabled: options.enableContentTypes || false,
      AllowContentTypes: options.enableContentTypes || true,
    };

    if (options.url) {
      (listCreationInfo as any).Url = options.url;
    }

    const accessToken = await this.accessToken;
    const digestResponse = await fetch(`${webUrl}/_api/contextinfo`, {
      method: 'POST',
      headers: { Accept: 'application/json;odata=verbose', Authorization: `Bearer ${accessToken}`, 'Content-Length': '0' },
    });
    const digestData = await digestResponse.json() as any;
    const digest = digestData.d.GetContextWebInformation.FormDigestValue;

    const response = await fetch(`${webUrl}/_api/web/lists`, {
      method: 'POST',
      headers: {
        Accept: 'application/json;odata=verbose',
        'Content-Type': 'application/json;odata=verbose',
        Authorization: `Bearer ${accessToken}`,
        'X-RequestDigest': digest,
      },
      body: JSON.stringify(listCreationInfo),
    });

    if (!response.ok) {
      const err = await response.text();
      throw new Error(`Failed to create list: ${err}`);
    }

    const result = await response.json();

    if (options.onQuickLaunch) {
      // Update list to show on quick launch
      await fetch(`${webUrl}/_api/web/lists/GetByTitle('${encodeURIComponent(options.title as string)}')`, {
        method: 'POST',
        headers: {
          Accept: 'application/json;odata=verbose',
          'Content-Type': 'application/json;odata=verbose',
          Authorization: `Bearer ${accessToken}`,
          'X-RequestDigest': digest,
          'X-HTTP-Method': 'MERGE',
          'IF-MATCH': '*',
        },
        body: JSON.stringify({ __metadata: { type: 'SP.List' }, OnQuickLaunch: true }),
      });
    }

    this.writeOutput(result);
  }

  private resolveTemplate(template: string): number {
    const templates: Record<string, number> = {
      genericlist: 100,
      documentlibrary: 101,
      survey: 102,
      links: 103,
      announcements: 104,
      contacts: 105,
      events: 106,
      tasks: 107,
      discussionboard: 108,
      picturelibrary: 109,
      datasources: 110,
      webtemplatecatalog: 111,
      userinformation: 112,
      webpartcatalog: 113,
      listtemplatecatalog: 114,
      xmlform: 115,
      masterpagecatalog: 116,
      nocodeworkflows: 117,
      workflowprocess: 118,
      webpagecatalog: 119,
      custommodifiedpages: 120,
      solutioncatalog: 121,
      issues: 1100,
    };

    const numericValue = parseInt(template, 10);
    if (!isNaN(numericValue)) return numericValue;

    return templates[template.toLowerCase()] || 100;
  }
}
