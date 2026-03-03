import { isGuid } from '../utils/url-utilities.js';
import { ApiRequestHelper } from '../http/api-request-helper.js';

/**
 * Resolves a user from a UPN, display name, or object ID.
 * Equivalent to UserPipeBind in the C# codebase.
 */
export class UserResolver {
  private id?: string;
  private upnOrName?: string;

  constructor(input: string) {
    if (isGuid(input)) {
      this.id = input;
    } else {
      this.upnOrName = input;
    }
  }

  async resolve(graphHelper: ApiRequestHelper): Promise<{ id: string; displayName: string; userPrincipalName: string }> {
    if (this.id) {
      return graphHelper.getTyped(`users/${this.id}?$select=id,displayName,userPrincipalName`);
    }

    // Try as UPN first (contains @)
    if (this.upnOrName!.includes('@')) {
      return graphHelper.getTyped(`users/${encodeURIComponent(this.upnOrName!)}?$select=id,displayName,userPrincipalName`);
    }

    // Search by display name
    const users = await graphHelper.getResultCollection<{ id: string; displayName: string; userPrincipalName: string }>(
      `users?$filter=displayName eq '${encodeURIComponent(this.upnOrName!)}'&$select=id,displayName,userPrincipalName`,
    );

    if (users.length === 0) {
      throw new Error(`User '${this.upnOrName}' not found.`);
    }
    if (users.length > 1) {
      throw new Error(`Multiple users found matching '${this.upnOrName}'. Please use the User ID or UPN instead.`);
    }

    return users[0];
  }
}
