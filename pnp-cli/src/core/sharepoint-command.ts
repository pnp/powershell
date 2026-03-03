import { ConnectedCommand } from './connected-command.js';
import { ApiRequestHelper } from '../http/api-request-helper.js';

/**
 * Base class for all SharePoint-related commands.
 * Equivalent to PnPSharePointCmdlet in the C# codebase.
 */
export abstract class SharePointCommand extends ConnectedCommand {
  protected sharePointRequestHelper!: ApiRequestHelper;
  protected graphRequestHelper!: ApiRequestHelper;

  protected get accessToken(): Promise<string> {
    const resourceUri = new URL(this.connection.url!);
    const defaultResource = `${resourceUri.protocol}//${resourceUri.host}/.default`;
    return this.connection.getAccessToken(defaultResource);
  }

  protected get graphAccessToken(): Promise<string> {
    return this.connection.getAccessToken(`https://${this.connection.graphEndPoint}/.default`);
  }

  protected override async beforeExecute(options: Record<string, unknown>): Promise<void> {
    await super.beforeExecute(options);

    if (!this.connection.url) {
      throw new Error(
        'No SharePoint connection URL available. Connect with a URL: pnp connect --url https://contoso.sharepoint.com',
      );
    }

    const resourceUri = new URL(this.connection.url);
    const defaultResource = `${resourceUri.protocol}//${resourceUri.host}/.default`;
    this.sharePointRequestHelper = new ApiRequestHelper(this.connection, defaultResource);
    this.graphRequestHelper = new ApiRequestHelper(
      this.connection,
      `https://${this.connection.graphEndPoint}/.default`,
    );
  }
}
