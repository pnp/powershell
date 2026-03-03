import { ConnectedCommand } from './connected-command.js';
import { ApiRequestHelper } from '../http/api-request-helper.js';

/**
 * Base class for all Microsoft Graph-related commands.
 * Equivalent to PnPGraphCmdlet in the C# codebase.
 */
export abstract class GraphCommand extends ConnectedCommand {
  protected graphRequestHelper!: ApiRequestHelper;

  protected get microsoftGraphDefaultAudience(): string {
    return `https://${this.connection.graphEndPoint}/.default`;
  }

  protected get accessToken(): Promise<string> {
    return this.connection.getAccessToken(this.microsoftGraphDefaultAudience);
  }

  protected override async beforeExecute(options: Record<string, unknown>): Promise<void> {
    await super.beforeExecute(options);

    this.graphRequestHelper = new ApiRequestHelper(this.connection, this.microsoftGraphDefaultAudience);
  }
}
