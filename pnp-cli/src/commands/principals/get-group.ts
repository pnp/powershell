import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class GetSPGroupCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-group')
      .description('Returns a SharePoint group by name or ID')
      .option('--identity <nameOrId>', 'Group name or numeric ID')
      .option('--associated-owner-group', 'Return the associated owner group')
      .option('--associated-member-group', 'Return the associated member group')
      .option('--associated-visitor-group', 'Return the associated visitor group')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();

    if (options.associatedOwnerGroup) {
      const result = await this.sharePointRequestHelper.getTyped<any>(
        `${webUrl}/_api/web/associatedownergroup`,
      );
      this.writeOutput(result);
    } else if (options.associatedMemberGroup) {
      const result = await this.sharePointRequestHelper.getTyped<any>(
        `${webUrl}/_api/web/associatedmembergroup`,
      );
      this.writeOutput(result);
    } else if (options.associatedVisitorGroup) {
      const result = await this.sharePointRequestHelper.getTyped<any>(
        `${webUrl}/_api/web/associatedvisitorgroup`,
      );
      this.writeOutput(result);
    } else if (this.parameterSpecified(options, 'identity')) {
      const identity = options.identity as string;
      const numId = parseInt(identity, 10);

      if (!isNaN(numId)) {
        const result = await this.sharePointRequestHelper.getTyped<any>(
          `${webUrl}/_api/web/sitegroups/getbyid(${numId})`,
        );
        this.writeOutput(result);
      } else {
        const result = await this.sharePointRequestHelper.getTyped<any>(
          `${webUrl}/_api/web/sitegroups/getbyname('${encodeURIComponent(identity)}')`,
        );
        this.writeOutput(result);
      }
    } else {
      const result = await this.sharePointRequestHelper.getTyped<{ value: unknown[] }>(
        `${webUrl}/_api/web/sitegroups`,
      );
      this.writeOutput(result?.value);
    }
  }
}
