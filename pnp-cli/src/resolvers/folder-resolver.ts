import { SharePointRestClient } from '../http/sharepoint-rest-client.js';

/**
 * Resolves a folder from a server-relative URL.
 * Equivalent to FolderPipeBind in the C# codebase.
 */
export class FolderResolver {
  constructor(private serverRelativeUrl: string) {}

  async resolve(spClient: SharePointRestClient, webUrl: string): Promise<unknown> {
    return spClient.get(
      `${webUrl}/_api/web/GetFolderByServerRelativeUrl('${encodeURIComponent(this.serverRelativeUrl)}')`,
    );
  }
}
