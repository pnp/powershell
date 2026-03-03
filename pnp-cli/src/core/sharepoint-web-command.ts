import { SharePointCommand } from './sharepoint-command.js';

/**
 * Base class for SharePoint commands that operate in the context of a specific web.
 * Equivalent to PnPWebCmdlet in the C# codebase.
 */
export abstract class SharePointWebCommand extends SharePointCommand {
  /**
   * Returns the base URL for the current web context.
   */
  protected getWebUrl(): string {
    return this.connection.url!;
  }
}
