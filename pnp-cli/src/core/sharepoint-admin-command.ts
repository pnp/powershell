import { SharePointCommand } from './sharepoint-command.js';
import { ApiRequestHelper } from '../http/api-request-helper.js';
import { getTenantAdminUrl } from '../utils/url-utilities.js';

/**
 * Base class for SharePoint Online Admin commands.
 * Equivalent to PnPSharePointOnlineAdminCmdlet in the C# codebase.
 */
export abstract class SharePointAdminCommand extends SharePointCommand {
  protected tenantAdminUrl!: string;
  protected adminRequestHelper!: ApiRequestHelper;

  protected override async beforeExecute(options: Record<string, unknown>): Promise<void> {
    await super.beforeExecute(options);

    // Use explicitly provided tenant admin URL or auto-derive it
    this.tenantAdminUrl = this.connection.tenantAdminUrl || getTenantAdminUrl(this.connection.url!);

    const resourceUri = new URL(this.tenantAdminUrl);
    const adminResource = `${resourceUri.protocol}//${resourceUri.host}/.default`;
    this.adminRequestHelper = new ApiRequestHelper(this.connection, adminResource);
  }
}
