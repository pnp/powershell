import { IdType } from '../enums/id-type.js';
import { ResourceTypeName } from '../enums/resource-type-name.js';

/**
 * Helper for oAuth JWT token evaluation.
 * Equivalent to TokenHandler in the C# codebase.
 */
export class TokenHandler {
  /**
   * Decode a JWT token payload without verification.
   */
  static decodeToken(accessToken: string): Record<string, unknown> {
    const parts = accessToken.split('.');
    if (parts.length !== 3) {
      throw new Error('Invalid JWT token format.');
    }
    return JSON.parse(Buffer.from(parts[1], 'base64').toString('utf-8'));
  }

  /**
   * Returns the type of oAuth JWT token (Delegate/Application).
   */
  static retrieveTokenType(accessToken: string): IdType {
    try {
      const payload = TokenHandler.decodeToken(accessToken);
      const idType = payload['idtyp'] as string | undefined;

      if (!idType) return IdType.Unknown;

      switch (idType.toLowerCase()) {
        case 'user':
          return IdType.Delegate;
        case 'app':
          return IdType.Application;
        default:
          return IdType.Unknown;
      }
    } catch {
      return IdType.Unknown;
    }
  }

  /**
   * Returns the user ID (oid claim) from a JWT token.
   */
  static retrieveTokenUser(accessToken: string): string | undefined {
    try {
      const payload = TokenHandler.decodeToken(accessToken);
      return payload['oid'] as string | undefined;
    } catch {
      return undefined;
    }
  }

  /**
   * Returns the scopes/roles from a JWT token.
   */
  static retrieveScopes(accessToken: string): string[] {
    try {
      const payload = TokenHandler.decodeToken(accessToken);
      const scopes: string[] = [];

      // Scopes can be in 'roles' (application) or 'scp' (delegated) claims
      const roles = payload['roles'] as string[] | undefined;
      if (roles) {
        scopes.push(...roles);
      }

      const scp = payload['scp'] as string | undefined;
      if (scp) {
        scopes.push(...scp.split(' '));
      }

      return scopes;
    } catch {
      return [];
    }
  }

  /**
   * Returns the audience from a JWT token.
   */
  static retrieveAudience(accessToken: string): string | undefined {
    try {
      const payload = TokenHandler.decodeToken(accessToken);
      const aud = payload['aud'] as string | string[] | undefined;
      return Array.isArray(aud) ? aud[0] : aud;
    } catch {
      return undefined;
    }
  }

  /**
   * Returns the expiration timestamp from a JWT token.
   */
  static retrieveExpiration(accessToken: string): Date | undefined {
    try {
      const payload = TokenHandler.decodeToken(accessToken);
      const exp = payload['exp'] as number | undefined;
      return exp ? new Date(exp * 1000) : undefined;
    } catch {
      return undefined;
    }
  }

  /**
   * Determines the resource type from an audience string.
   */
  static defineResourceTypeFromAudience(audience: string | undefined): ResourceTypeName {
    if (!audience) return ResourceTypeName.SharePoint;

    let sanitized = audience.replace(/\/+$/, '').toLowerCase();
    if (sanitized.startsWith('http://')) sanitized = sanitized.substring(7);
    if (sanitized.startsWith('https://')) sanitized = sanitized.substring(8);

    switch (sanitized) {
      case 'graph':
      case 'graph.microsoft.com':
      case 'graph.microsoft.us':
      case 'graph.microsoft.de':
      case 'microsoftgraph.chinacloudapi.cn':
      case 'dod-graph.microsoft.us':
      case '00000003-0000-0000-c000-000000000000':
        return ResourceTypeName.Graph;
      case 'azure':
      case 'management.azure.com':
      case 'management.chinacloudapi.cn':
      case 'management.usgovcloudapi.net':
        return ResourceTypeName.AzureManagementApi;
      case 'exchangeonline':
      case 'outlook.office.com':
      case 'outlook.office365.com':
        return ResourceTypeName.ExchangeOnline;
      case 'flow':
      case 'service.flow.microsoft.com':
        return ResourceTypeName.PowerAutomate;
      case 'powerapps':
      case 'api.powerapps.com':
        return ResourceTypeName.PowerApps;
      case 'dynamics':
      case 'admin.services.crm.dynamics.com':
      case 'api.crm.dynamics.com':
        return ResourceTypeName.DynamicsCRM;
      case 'gcs':
      case 'gcs.office.com':
        return ResourceTypeName.Gcs;
      default:
        return ResourceTypeName.SharePoint;
    }
  }
}
