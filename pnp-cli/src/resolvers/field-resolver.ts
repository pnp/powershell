import { isGuid } from '../utils/url-utilities.js';
import { SharePointRestClient } from '../http/sharepoint-rest-client.js';

/**
 * Resolves a field from a title, internal name, or GUID.
 * Equivalent to FieldPipeBind in the C# codebase.
 */
export class FieldResolver {
  private id?: string;
  private nameOrTitle?: string;

  constructor(input: string) {
    if (isGuid(input)) {
      this.id = input;
    } else {
      this.nameOrTitle = input;
    }
  }

  async resolve(spClient: SharePointRestClient, webUrl: string, listId?: string): Promise<unknown> {
    const base = listId
      ? `${webUrl}/_api/web/lists(guid'${listId}')/fields`
      : `${webUrl}/_api/web/fields`;

    if (this.id) {
      return spClient.get(`${base}(guid'${this.id}')`);
    }

    // Try by internal name first, then by title
    try {
      return await spClient.get(`${base}/GetByInternalNameOrTitle('${encodeURIComponent(this.nameOrTitle!)}')`);
    } catch {
      return spClient.get(
        `${base}?$filter=Title eq '${encodeURIComponent(this.nameOrTitle!)}'`,
      );
    }
  }
}
