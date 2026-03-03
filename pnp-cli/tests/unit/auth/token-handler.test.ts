import { TokenHandler } from '../../../src/auth/token-handler';
import { IdType } from '../../../src/enums/id-type';
import { ResourceTypeName } from '../../../src/enums/resource-type-name';

// Helper to create a fake JWT token
function createFakeJwt(payload: Record<string, unknown>): string {
  const header = Buffer.from(JSON.stringify({ alg: 'none', typ: 'JWT' })).toString('base64url');
  const body = Buffer.from(JSON.stringify(payload)).toString('base64url');
  return `${header}.${body}.fakesignature`;
}

describe('TokenHandler', () => {
  describe('decodeToken', () => {
    it('should decode a JWT payload', () => {
      const token = createFakeJwt({ sub: 'user123', aud: 'https://graph.microsoft.com' });
      const payload = TokenHandler.decodeToken(token);
      expect(payload.sub).toBe('user123');
      expect(payload.aud).toBe('https://graph.microsoft.com');
    });

    it('should throw on invalid JWT format', () => {
      expect(() => TokenHandler.decodeToken('not-a-jwt')).toThrow('Invalid JWT token format');
    });
  });

  describe('retrieveTokenType', () => {
    it('should return Delegate for user tokens', () => {
      const token = createFakeJwt({ idtyp: 'user' });
      expect(TokenHandler.retrieveTokenType(token)).toBe(IdType.Delegate);
    });

    it('should return Application for app tokens', () => {
      const token = createFakeJwt({ idtyp: 'app' });
      expect(TokenHandler.retrieveTokenType(token)).toBe(IdType.Application);
    });

    it('should return Unknown for tokens without idtyp', () => {
      const token = createFakeJwt({ sub: 'test' });
      expect(TokenHandler.retrieveTokenType(token)).toBe(IdType.Unknown);
    });
  });

  describe('retrieveTokenUser', () => {
    it('should return the oid claim', () => {
      const token = createFakeJwt({ oid: 'abc-123-def' });
      expect(TokenHandler.retrieveTokenUser(token)).toBe('abc-123-def');
    });

    it('should return undefined if no oid', () => {
      const token = createFakeJwt({ sub: 'test' });
      expect(TokenHandler.retrieveTokenUser(token)).toBeUndefined();
    });
  });

  describe('retrieveScopes', () => {
    it('should return roles from app tokens', () => {
      const token = createFakeJwt({ roles: ['Sites.Read.All', 'User.Read.All'] });
      expect(TokenHandler.retrieveScopes(token)).toEqual(['Sites.Read.All', 'User.Read.All']);
    });

    it('should return scp from delegated tokens', () => {
      const token = createFakeJwt({ scp: 'Sites.Read.All User.Read' });
      expect(TokenHandler.retrieveScopes(token)).toEqual(['Sites.Read.All', 'User.Read']);
    });
  });

  describe('defineResourceTypeFromAudience', () => {
    it('should identify Graph audiences', () => {
      expect(TokenHandler.defineResourceTypeFromAudience('https://graph.microsoft.com')).toBe(ResourceTypeName.Graph);
      expect(TokenHandler.defineResourceTypeFromAudience('https://graph.microsoft.us/')).toBe(ResourceTypeName.Graph);
    });

    it('should identify SharePoint as default', () => {
      expect(TokenHandler.defineResourceTypeFromAudience('https://contoso.sharepoint.com')).toBe(ResourceTypeName.SharePoint);
    });

    it('should identify Azure Management', () => {
      expect(TokenHandler.defineResourceTypeFromAudience('https://management.azure.com')).toBe(ResourceTypeName.AzureManagementApi);
    });
  });
});
