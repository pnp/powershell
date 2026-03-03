import { SharePointRestClient } from '../http/sharepoint-rest-client.js';

/**
 * Resolves a content type from a name or ID.
 * Equivalent to ContentTypePipeBind in the C# codebase.
 */
export class ContentTypeResolver {
  private id?: string;
  private name?: string;

  constructor(input: string) {
    // Content type IDs are typically hex strings starting with 0x
    if (input.startsWith('0x')) {
      this.id = input;
    } else {
      this.name = input;
    }
  }

  async resolve(spClient: SharePointRestClient, webUrl: string, listId?: string): Promise<unknown> {
    const base = listId
      ? `${webUrl}/_api/web/lists(guid'${listId}')/contenttypes`
      : `${webUrl}/_api/web/contenttypes`;

    if (this.id) {
      return spClient.get(`${base}('${this.id}')`);
    }

    // Filter by name
    const result = await spClient.get<{ d: { results: unknown[] } }>(
      `${base}?$filter=Name eq '${encodeURIComponent(this.name!)}'`,
    );
    const items = result?.d?.results;
    if (items && items.length > 0) {
      return items[0];
    }
    throw new Error(`Content type '${this.name}' not found.`);
  }
}
