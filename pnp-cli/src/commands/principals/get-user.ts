import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class GetSPUserCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-user')
      .description('Returns a SharePoint site user by identity')
      .option('--identity <loginOrId>', 'User login name or numeric ID')
      .option('--current', 'Get the current user')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();

    if (options.current) {
      const result = await this.sharePointRequestHelper.getTyped<any>(
        `${webUrl}/_api/web/currentuser`,
      );
      this.writeOutput(result);
    } else if (this.parameterSpecified(options, 'identity')) {
      const identity = options.identity as string;
      const numId = parseInt(identity, 10);

      if (!isNaN(numId)) {
        const result = await this.sharePointRequestHelper.getTyped<any>(
          `${webUrl}/_api/web/siteusers/getbyid(${numId})`,
        );
        this.writeOutput(result);
      } else {
        const result = await this.sharePointRequestHelper.getTyped<any>(
          `${webUrl}/_api/web/siteusers/getbyloginname('${encodeURIComponent(identity)}')`,
        );
        this.writeOutput(result);
      }
    } else {
      const result = await this.sharePointRequestHelper.getTyped<{ value: unknown[] }>(
        `${webUrl}/_api/web/siteusers`,
      );
      this.writeOutput(result?.value);
    }
  }
}
