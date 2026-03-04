import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class GetGroupMemberCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-group-member')
      .description('Returns members of a SharePoint group')
      .requiredOption('--group <nameOrId>', 'Group name or numeric ID')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const group = options.group as string;
    const numId = parseInt(group, 10);

    let groupUrl: string;
    if (!isNaN(numId)) {
      groupUrl = `${webUrl}/_api/web/sitegroups/getbyid(${numId})/users`;
    } else {
      groupUrl = `${webUrl}/_api/web/sitegroups/getbyname('${encodeURIComponent(group)}')/users`;
    }

    const result = await this.sharePointRequestHelper.getTyped<{ value: unknown[] }>(groupUrl);
    this.writeOutput(result?.value);
  }
}
