import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class AddGroupMemberCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('add-group-member')
      .description('Add a user to a SharePoint group')
      .requiredOption('--group <nameOrId>', 'Group name or numeric ID')
      .requiredOption('--login-name <loginName>', 'User login name to add')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const group = options.group as string;
    const numId = parseInt(group, 10);

    let groupUsersUrl: string;
    if (!isNaN(numId)) {
      groupUsersUrl = `${webUrl}/_api/web/sitegroups/getbyid(${numId})/users`;
    } else {
      groupUsersUrl = `${webUrl}/_api/web/sitegroups/getbyname('${encodeURIComponent(group)}')/users`;
    }

    const body = {
      __metadata: { type: 'SP.User' },
      LoginName: options.loginName,
    };

    const result = await this.sharePointRequestHelper.post<any>(groupUsersUrl, body);
    this.writeOutput(result);
  }
}
