import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';
import { ListResolver } from '../../resolvers/list-resolver.js';

export class SetListCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('set-list')
      .description('Updates list settings')
      .requiredOption('--identity <nameOrId>', 'List title, URL, or GUID')
      .option('--title <title>', 'New title')
      .option('--description <description>', 'New description')
      .option('--hidden <boolean>', 'Show or hide the list')
      .option('--enable-versioning <boolean>', 'Enable or disable versioning')
      .option('--enable-minor-versions <boolean>', 'Enable or disable minor versions')
      .option('--enable-content-types <boolean>', 'Enable or disable content types')
      .option('--on-quick-launch <boolean>', 'Show or hide on quick launch')
      .option('--enable-folder-creation <boolean>', 'Enable or disable folder creation')
      .option('--force-checkout <boolean>', 'Enable or disable force checkout')
      .option('--iri-enabled <boolean>', 'Enable or disable IRM')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const resolver = new ListResolver(options.identity as string);
    const listInfo = await resolver.resolve(this.sharePointRequestHelper as any, webUrl) as any;

    if (!listInfo) {
      throw new Error(`List '${options.identity}' not found.`);
    }

    const listId = listInfo?.d?.Id || listInfo?.Id;

    const updates: Record<string, unknown> = { __metadata: { type: 'SP.List' } };
    if (this.parameterSpecified(options, 'title')) updates.Title = options.title;
    if (this.parameterSpecified(options, 'description')) updates.Description = options.description;
    if (this.parameterSpecified(options, 'hidden')) updates.Hidden = options.hidden === 'true';
    if (this.parameterSpecified(options, 'enableVersioning')) updates.EnableVersioning = options.enableVersioning === 'true';
    if (this.parameterSpecified(options, 'enableMinorVersions')) updates.EnableMinorVersions = options.enableMinorVersions === 'true';
    if (this.parameterSpecified(options, 'enableContentTypes')) updates.ContentTypesEnabled = options.enableContentTypes === 'true';
    if (this.parameterSpecified(options, 'onQuickLaunch')) updates.OnQuickLaunch = options.onQuickLaunch === 'true';
    if (this.parameterSpecified(options, 'enableFolderCreation')) updates.EnableFolderCreation = options.enableFolderCreation === 'true';
    if (this.parameterSpecified(options, 'forceCheckout')) updates.ForceCheckout = options.forceCheckout === 'true';

    const accessToken = await this.accessToken;
    const digestResponse = await fetch(`${webUrl}/_api/contextinfo`, {
      method: 'POST',
      headers: { Accept: 'application/json;odata=verbose', Authorization: `Bearer ${accessToken}`, 'Content-Length': '0' },
    });
    const digestData = await digestResponse.json() as any;
    const digest = digestData.d.GetContextWebInformation.FormDigestValue;

    const response = await fetch(`${webUrl}/_api/web/lists(guid'${listId}')`, {
      method: 'POST',
      headers: {
        Accept: 'application/json;odata=verbose',
        'Content-Type': 'application/json;odata=verbose',
        Authorization: `Bearer ${accessToken}`,
        'X-RequestDigest': digest,
        'X-HTTP-Method': 'MERGE',
        'IF-MATCH': '*',
      },
      body: JSON.stringify(updates),
    });

    if (!response.ok) {
      const err = await response.text();
      throw new Error(`Failed to update list: ${err}`);
    }

    console.log('List updated successfully.');
  }
}
