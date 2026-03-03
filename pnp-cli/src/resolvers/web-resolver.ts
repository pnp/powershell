import { isGuid } from '../utils/url-utilities.js';
import { SharePointRestClient } from '../http/sharepoint-rest-client.js';

/**
 * Resolves a web from a URL or GUID.
 * Equivalent to WebPipeBind in the C# codebase.
 */
export class WebResolver {
  private id?: string;
  private url?: string;

  constructor(input: string) {
    if (isGuid(input)) {
      this.id = input;
    } else {
      this.url = input;
    }
  }

  async resolve(spClient: SharePointRestClient, webUrl: string): Promise<unknown> {
    if (this.id) {
      return spClient.get(`${webUrl}/_api/web/webs?$filter=Id eq guid'${this.id}'`);
    }
    if (this.url) {
      return spClient.get(`${this.url}/_api/web`);
    }
    return spClient.get(`${webUrl}/_api/web`);
  }
}
