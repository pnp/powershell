import { isGuid } from '../utils/url-utilities.js';
import { ApiRequestHelper } from '../http/api-request-helper.js';

/**
 * Resolves a site from a URL or GUID.
 * Equivalent to SitePipeBind in the C# codebase.
 */
export class SiteResolver {
  private id?: string;
  private url?: string;

  constructor(input: string) {
    if (isGuid(input)) {
      this.id = input;
    } else {
      this.url = input;
    }
  }

  async resolve(graphHelper: ApiRequestHelper): Promise<unknown> {
    if (this.id) {
      return graphHelper.getTyped(`sites/${this.id}`);
    }
    if (this.url) {
      const url = new URL(this.url);
      const siteRelativePath = url.pathname === '/' ? '' : `:${url.pathname}:`;
      return graphHelper.getTyped(`sites/${url.hostname}${siteRelativePath}`);
    }
    throw new Error('No site identifier provided.');
  }
}
