import { isGuid } from '../utils/url-utilities.js';
import { SharePointRestClient } from '../http/sharepoint-rest-client.js';

/**
 * Resolves a list from a string identifier (title, URL, or GUID).
 * Equivalent to ListPipeBind in the C# codebase.
 */
export class ListResolver {
  private id?: string;
  private title?: string;

  constructor(input: string) {
    if (isGuid(input)) {
      this.id = input;
    } else {
      this.title = input;
    }
  }

  async resolve(spClient: SharePointRestClient, webUrl: string): Promise<unknown> {
    if (this.id) {
      return spClient.get(`${webUrl}/_api/web/lists(guid'${this.id}')`);
    }

    // Try by URL first, then by title (matches C# ListPipeBind.GetList logic)
    try {
      return await spClient.get(`${webUrl}/_api/web/GetList('${encodeURIComponent(this.title!)}')`);
    } catch {
      return spClient.get(`${webUrl}/_api/web/lists/GetByTitle('${encodeURIComponent(this.title!)}')`);
    }
  }

  getIdOrTitle(): string {
    return this.id || this.title || '';
  }
}
