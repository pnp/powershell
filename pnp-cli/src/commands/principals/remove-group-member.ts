import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class RemoveGroupMemberCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('remove-group-member')
      .description('Remove a user from a SharePoint group')
      .requiredOption('--group <nameOrId>', 'Group name or numeric ID')
      .requiredOption('--login-name <loginName>', 'User login name to remove')
      .option('--force', 'Skip confirmation')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const group = options.group as string;
    const numId = parseInt(group, 10);

    let removeUrl: string;
    if (!isNaN(numId)) {
      removeUrl = `${webUrl}/_api/web/sitegroups/getbyid(${numId})/users/removebyloginname('${encodeURIComponent(options.loginName as string)}')`;
    } else {
      removeUrl = `${webUrl}/_api/web/sitegroups/getbyname('${encodeURIComponent(group)}')/users/removebyloginname('${encodeURIComponent(options.loginName as string)}')`;
    }

    await this.sharePointRequestHelper.post<void>(removeUrl, {});
    this.writeOutput({ message: `User '${options.loginName}' removed from group '${group}'` });
  }
}
