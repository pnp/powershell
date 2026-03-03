import { isGuid } from '../utils/url-utilities.js';
import { ApiRequestHelper } from '../http/api-request-helper.js';

/**
 * Resolves a Microsoft Teams team from a display name, mail nickname, or group ID.
 * Equivalent to TeamsTeamPipeBind in the C# codebase.
 */
export class TeamResolver {
  private id?: string;
  private name?: string;

  constructor(input: string) {
    if (isGuid(input)) {
      this.id = input;
    } else {
      this.name = input;
    }
  }

  async resolveGroupId(graphHelper: ApiRequestHelper): Promise<string | undefined> {
    if (this.id) {
      return this.id;
    }

    // Search by display name
    const groups = await graphHelper.getResultCollection<{ id: string; displayName: string }>(
      `groups?$filter=displayName eq '${encodeURIComponent(this.name!)}' and resourceProvisioningOptions/Any(x:x eq 'Team')&$select=id,displayName`,
    );

    if (groups.length === 0) {
      return undefined;
    }
    if (groups.length > 1) {
      throw new Error(`Multiple teams found matching '${this.name}'. Please use the team's Group ID instead.`);
    }

    return groups[0].id;
  }
}
