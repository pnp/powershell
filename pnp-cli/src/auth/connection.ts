import { ConnectionMethod } from '../enums/connection-method.js';
import { ConnectionType } from '../enums/connection-type.js';
import { AzureEnvironment, getGraphEndpoint } from '../enums/azure-environment.js';

/**
 * Serializable connection data that persists to disk.
 */
export interface PnPConnectionData {
  url?: string;
  tenantAdminUrl?: string;
  clientId: string;
  clientSecret?: string;
  tenant?: string;
  connectionMethod: ConnectionMethod;
  connectionType: ConnectionType;
  azureEnvironment: AzureEnvironment;
  graphEndPoint: string;
  scopes?: string[];
  certificatePath?: string;
  certificateBase64?: string;
  thumbprint?: string;
  userAssignedManagedIdentityObjectId?: string;
  userAssignedManagedIdentityClientId?: string;
  userAssignedManagedIdentityAzureResourceId?: string;
  connectedAt: string;
}

/**
 * Token cache entry.
 */
interface CachedToken {
  token: string;
  expiresAt: number;
}

/**
 * Represents an active connection to Microsoft 365.
 * Equivalent to PnPConnection in the C# codebase.
 *
 * Key difference from C#: since each CLI invocation is a new process,
 * this class + ConnectionStore replaces the C# static PnPConnection.Current singleton.
 */
export class PnPConnection {
  /** Default PnP Management Shell client ID */
  static readonly PNP_MANAGEMENT_SHELL_CLIENT_ID = '31359c7f-bd7e-475c-86db-fdb8c937548e';

  private data: PnPConnectionData;
  private tokenCache: Map<string, CachedToken> = new Map();

  /** Token acquisition function injected by auth strategies */
  private tokenAcquirer?: (audience: string) => Promise<string>;

  constructor(data: PnPConnectionData, tokenAcquirer?: (audience: string) => Promise<string>) {
    this.data = data;
    this.tokenAcquirer = tokenAcquirer;
  }

  // --- Property accessors ---

  get url(): string | undefined {
    return this.data.url;
  }

  get tenantAdminUrl(): string | undefined {
    return this.data.tenantAdminUrl;
  }

  get clientId(): string {
    return this.data.clientId;
  }

  get clientSecret(): string | undefined {
    return this.data.clientSecret;
  }

  get tenant(): string | undefined {
    return this.data.tenant;
  }

  get connectionMethod(): ConnectionMethod {
    return this.data.connectionMethod;
  }

  get connectionType(): ConnectionType {
    return this.data.connectionType;
  }

  get azureEnvironment(): AzureEnvironment {
    return this.data.azureEnvironment;
  }

  get graphEndPoint(): string {
    return this.data.graphEndPoint;
  }

  get scopes(): string[] | undefined {
    return this.data.scopes;
  }

  get connectedAt(): string {
    return this.data.connectedAt;
  }

  // --- Token management ---

  /**
   * Get an access token for the specified audience/resource.
   * Uses cache if token is still valid (with 5-minute buffer).
   */
  async getAccessToken(audience: string): Promise<string> {
    // Check cache first
    const cached = this.tokenCache.get(audience);
    if (cached && cached.expiresAt > Date.now() + 5 * 60 * 1000) {
      return cached.token;
    }

    if (!this.tokenAcquirer) {
      throw new Error('No token acquirer configured. This connection may need to be re-established.');
    }

    const token = await this.tokenAcquirer(audience);

    // Cache the token (try to parse expiry from JWT)
    try {
      const payload = JSON.parse(Buffer.from(token.split('.')[1], 'base64').toString());
      const expiresAt = (payload.exp as number) * 1000;
      this.tokenCache.set(audience, { token, expiresAt });
    } catch {
      // If we can't parse the JWT, cache with 1-hour expiry
      this.tokenCache.set(audience, { token, expiresAt: Date.now() + 60 * 60 * 1000 });
    }

    return token;
  }

  async getGraphAccessToken(): Promise<string> {
    return this.getAccessToken(`https://${this.graphEndPoint}/.default`);
  }

  async getSharePointAccessToken(): Promise<string> {
    if (!this.url) {
      throw new Error('No SharePoint URL configured on this connection.');
    }
    const resourceUri = new URL(this.url);
    return this.getAccessToken(`${resourceUri.protocol}//${resourceUri.host}/.default`);
  }

  /**
   * Set a token acquirer function (used when restoring from disk with MSAL cache).
   */
  setTokenAcquirer(acquirer: (audience: string) => Promise<string>): void {
    this.tokenAcquirer = acquirer;
  }

  // --- Serialization ---

  toJSON(): PnPConnectionData {
    return { ...this.data };
  }

  static fromJSON(data: PnPConnectionData): PnPConnection {
    return new PnPConnection(data);
  }

  // --- Factory methods ---

  static createWithAccessToken(
    url: string | undefined,
    accessToken: string,
    tenantAdminUrl?: string,
    azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
  ): PnPConnection {
    const data: PnPConnectionData = {
      url,
      tenantAdminUrl,
      clientId: PnPConnection.PNP_MANAGEMENT_SHELL_CLIENT_ID,
      connectionMethod: ConnectionMethod.AccessToken,
      connectionType: url?.includes('-admin.sharepoint.') ? ConnectionType.TenantAdmin : ConnectionType.O365,
      azureEnvironment,
      graphEndPoint: getGraphEndpoint(azureEnvironment),
      connectedAt: new Date().toISOString(),
    };

    return new PnPConnection(data, async () => accessToken);
  }

  static createWithClientSecret(
    url: string | undefined,
    clientId: string,
    clientSecret: string,
    tenant: string,
    tenantAdminUrl?: string,
    azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
  ): PnPConnection {
    const data: PnPConnectionData = {
      url,
      tenantAdminUrl,
      clientId,
      clientSecret,
      tenant,
      connectionMethod: ConnectionMethod.ACS,
      connectionType: url?.includes('-admin.sharepoint.') ? ConnectionType.TenantAdmin : ConnectionType.O365,
      azureEnvironment,
      graphEndPoint: getGraphEndpoint(azureEnvironment),
      connectedAt: new Date().toISOString(),
    };

    // Token acquirer will be set by the auth strategy
    return new PnPConnection(data);
  }

  static createWithInteractive(
    url: string | undefined,
    clientId: string,
    tenantAdminUrl?: string,
    azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
    scopes?: string[],
  ): PnPConnection {
    const data: PnPConnectionData = {
      url,
      tenantAdminUrl,
      clientId,
      connectionMethod: ConnectionMethod.Interactive,
      connectionType: url?.includes('-admin.sharepoint.') ? ConnectionType.TenantAdmin : ConnectionType.O365,
      azureEnvironment,
      graphEndPoint: getGraphEndpoint(azureEnvironment),
      scopes,
      connectedAt: new Date().toISOString(),
    };

    return new PnPConnection(data);
  }

  static createWithDeviceCode(
    url: string | undefined,
    clientId: string,
    tenantAdminUrl?: string,
    azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
  ): PnPConnection {
    const data: PnPConnectionData = {
      url,
      tenantAdminUrl,
      clientId,
      connectionMethod: ConnectionMethod.DeviceCode,
      connectionType: url?.includes('-admin.sharepoint.') ? ConnectionType.TenantAdmin : ConnectionType.O365,
      azureEnvironment,
      graphEndPoint: getGraphEndpoint(azureEnvironment),
      connectedAt: new Date().toISOString(),
    };

    return new PnPConnection(data);
  }

  static createWithCertificate(
    url: string | undefined,
    clientId: string,
    tenant: string,
    certificatePath?: string,
    certificateBase64?: string,
    thumbprint?: string,
    tenantAdminUrl?: string,
    azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
  ): PnPConnection {
    const data: PnPConnectionData = {
      url,
      tenantAdminUrl,
      clientId,
      tenant,
      certificatePath,
      certificateBase64,
      thumbprint,
      connectionMethod: ConnectionMethod.ClientCertificate,
      connectionType: url?.includes('-admin.sharepoint.') ? ConnectionType.TenantAdmin : ConnectionType.O365,
      azureEnvironment,
      graphEndPoint: getGraphEndpoint(azureEnvironment),
      connectedAt: new Date().toISOString(),
    };

    return new PnPConnection(data);
  }

  static createWithCredentials(
    url: string | undefined,
    clientId: string,
    tenantAdminUrl?: string,
    azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
  ): PnPConnection {
    const data: PnPConnectionData = {
      url,
      tenantAdminUrl,
      clientId,
      connectionMethod: ConnectionMethod.Credentials,
      connectionType: url?.includes('-admin.sharepoint.') ? ConnectionType.TenantAdmin : ConnectionType.O365,
      azureEnvironment,
      graphEndPoint: getGraphEndpoint(azureEnvironment),
      connectedAt: new Date().toISOString(),
    };

    return new PnPConnection(data);
  }

  static createWithManagedIdentity(
    url?: string,
    objectId?: string,
    clientId?: string,
    azureResourceId?: string,
    tenantAdminUrl?: string,
    azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
  ): PnPConnection {
    const data: PnPConnectionData = {
      url,
      tenantAdminUrl,
      clientId: clientId || PnPConnection.PNP_MANAGEMENT_SHELL_CLIENT_ID,
      connectionMethod: ConnectionMethod.ManagedIdentity,
      connectionType: url?.includes('-admin.sharepoint.') ? ConnectionType.TenantAdmin : ConnectionType.O365,
      azureEnvironment,
      graphEndPoint: getGraphEndpoint(azureEnvironment),
      userAssignedManagedIdentityObjectId: objectId,
      userAssignedManagedIdentityClientId: clientId,
      userAssignedManagedIdentityAzureResourceId: azureResourceId,
      connectedAt: new Date().toISOString(),
    };

    return new PnPConnection(data);
  }

  static createWithEnvironmentVariable(
    url: string | undefined,
    clientId: string,
    tenantAdminUrl?: string,
    azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
  ): PnPConnection {
    const data: PnPConnectionData = {
      url,
      tenantAdminUrl,
      clientId,
      connectionMethod: ConnectionMethod.EnvironmentVariable,
      connectionType: url?.includes('-admin.sharepoint.') ? ConnectionType.TenantAdmin : ConnectionType.O365,
      azureEnvironment,
      graphEndPoint: getGraphEndpoint(azureEnvironment),
      connectedAt: new Date().toISOString(),
    };

    return new PnPConnection(data);
  }

  static createWithWorkloadIdentity(
    url?: string,
    tenantAdminUrl?: string,
    azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
  ): PnPConnection {
    const clientId = process.env.AZURE_CLIENT_ID || PnPConnection.PNP_MANAGEMENT_SHELL_CLIENT_ID;
    const data: PnPConnectionData = {
      url,
      tenantAdminUrl,
      clientId,
      tenant: process.env.AZURE_TENANT_ID,
      connectionMethod: ConnectionMethod.AzureADWorkloadIdentity,
      connectionType: url?.includes('-admin.sharepoint.') ? ConnectionType.TenantAdmin : ConnectionType.O365,
      azureEnvironment,
      graphEndPoint: getGraphEndpoint(azureEnvironment),
      connectedAt: new Date().toISOString(),
    };

    return new PnPConnection(data);
  }

  static createWithFederatedIdentity(
    url: string | undefined,
    clientId: string,
    tenant: string,
    tenantAdminUrl?: string,
    azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
  ): PnPConnection {
    const data: PnPConnectionData = {
      url,
      tenantAdminUrl,
      clientId,
      tenant,
      connectionMethod: ConnectionMethod.FederatedIdentity,
      connectionType: url?.includes('-admin.sharepoint.') ? ConnectionType.TenantAdmin : ConnectionType.O365,
      azureEnvironment,
      graphEndPoint: getGraphEndpoint(azureEnvironment),
      connectedAt: new Date().toISOString(),
    };

    return new PnPConnection(data);
  }
}
