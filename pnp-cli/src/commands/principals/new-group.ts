import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class NewSPGroupCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('new-group')
      .description('Create a new SharePoint group')
      .requiredOption('--title <title>', 'Group title')
      .option('--description <description>', 'Group description')
      .option('--owner <loginName>', 'Owner login name')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const url = `${webUrl}/_api/web/sitegroups`;

    const body: any = {
      __metadata: { type: 'SP.Group' },
      Title: options.title,
    };

    if (options.description) {
      body.Description = options.description;
    }

    const result = await this.sharePointRequestHelper.post<any>(url, body);

    if (options.owner && result?.Id) {
      const ownerUrl = `${webUrl}/_api/web/sitegroups/getbyid(${result.Id})/owner`;
      await this.sharePointRequestHelper.post<void>(ownerUrl, {
        __metadata: { type: 'SP.User' },
        LoginName: options.owner,
      });
    }

    this.writeOutput(result);
  }
}
