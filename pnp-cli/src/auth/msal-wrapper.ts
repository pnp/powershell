import * as msal from '@azure/msal-node';
import { AzureEnvironment, getLoginEndpoint } from '../enums/azure-environment.js';
import { ConnectionStore } from './connection-store.js';

/** Certificate configuration for confidential client */
export interface CertificateCredential {
  thumbprint?: string;
  thumbprintSha256?: string;
  privateKey: string;
  x5c?: string;
}

/**
 * Wrapper around MSAL Node for token acquisition with persistent caching.
 */
export class MsalWrapper {
  private publicApp?: msal.PublicClientApplication;
  private confidentialApp?: msal.ConfidentialClientApplication;

  constructor(
    private clientId: string,
    private tenant?: string,
    private azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
  ) {}

  private get authority(): string {
    const loginEndpoint = getLoginEndpoint(this.azureEnvironment);
    const tenantId = this.tenant || 'organizations';
    return `https://${loginEndpoint}/${tenantId}`;
  }

  private createCachePlugin(): msal.ICachePlugin {
    return {
      beforeCacheAccess: async (context: msal.TokenCacheContext) => {
        const cache = ConnectionStore.readTokenCache();
        if (cache) {
          context.tokenCache.deserialize(cache);
        }
      },
      afterCacheAccess: async (context: msal.TokenCacheContext) => {
        if (context.cacheHasChanged) {
          ConnectionStore.writeTokenCache(context.tokenCache.serialize());
        }
      },
    };
  }

  /**
   * Get or create a PublicClientApplication for interactive/device-code flows.
   */
  getPublicApp(): msal.PublicClientApplication {
    if (!this.publicApp) {
      this.publicApp = new msal.PublicClientApplication({
        auth: {
          clientId: this.clientId,
          authority: this.authority,
        },
        cache: {
          cachePlugin: this.createCachePlugin(),
        },
      });
    }
    return this.publicApp;
  }

  /**
   * Get or create a ConfidentialClientApplication for certificate/secret flows.
   */
  getConfidentialApp(
    clientCredential: CertificateCredential | string,
  ): msal.ConfidentialClientApplication {
    if (!this.confidentialApp) {
      const config: msal.Configuration = {
        auth: {
          clientId: this.clientId,
          authority: this.authority,
          ...(typeof clientCredential === 'string'
            ? { clientSecret: clientCredential }
            : { clientCertificate: clientCredential }),
        },
        cache: {
          cachePlugin: this.createCachePlugin(),
        },
      };
      this.confidentialApp = new msal.ConfidentialClientApplication(config);
    }
    return this.confidentialApp;
  }

  /**
   * Acquire token interactively using the system browser.
   */
  async acquireTokenInteractive(scopes: string[]): Promise<msal.AuthenticationResult> {
    const app = this.getPublicApp();

    // Try silent first (from cache)
    const accounts = await app.getTokenCache().getAllAccounts();
    if (accounts.length > 0) {
      try {
        const result = await app.acquireTokenSilent({ scopes, account: accounts[0] });
        if (result) return result;
      } catch {
        // Fall through to interactive
      }
    }

    const result = await app.acquireTokenInteractive({
      scopes,
      openBrowser: async (url: string) => {
        const open = (await import('open')).default;
        await open(url);
      },
      successTemplate: '<h1>Authentication successful!</h1><p>You can close this window.</p>',
      errorTemplate: '<h1>Authentication failed!</h1><p>Error: {{error}}</p>',
    });

    return result;
  }

  /**
   * Acquire token using device code flow.
   */
  async acquireTokenByDeviceCode(scopes: string[]): Promise<msal.AuthenticationResult> {
    const app = this.getPublicApp();

    const result = await app.acquireTokenByDeviceCode({
      scopes,
      deviceCodeCallback: (response) => {
        console.log(response.message);
      },
    });

    if (!result) {
      throw new Error('Device code authentication failed.');
    }

    return result;
  }

  /**
   * Acquire token using client certificate.
   */
  async acquireTokenByCertificate(
    scopes: string[],
    certificate: CertificateCredential,
  ): Promise<msal.AuthenticationResult> {
    const app = this.getConfidentialApp(certificate);

    const result = await app.acquireTokenByClientCredential({ scopes });

    if (!result) {
      throw new Error('Certificate authentication failed.');
    }

    return result;
  }

  /**
   * Acquire token using client secret.
   */
  async acquireTokenByClientSecret(scopes: string[], clientSecret: string): Promise<msal.AuthenticationResult> {
    const app = this.getConfidentialApp(clientSecret);

    const result = await app.acquireTokenByClientCredential({ scopes });

    if (!result) {
      throw new Error('Client secret authentication failed.');
    }

    return result;
  }

  /**
   * Acquire token using username/password (ROPC).
   */
  async acquireTokenByUsernamePassword(
    scopes: string[],
    username: string,
    password: string,
  ): Promise<msal.AuthenticationResult> {
    const app = this.getPublicApp();

    const result = await app.acquireTokenByUsernamePassword({ scopes, username, password });

    if (!result) {
      throw new Error('Username/password authentication failed.');
    }

    return result;
  }

  /**
   * Try to acquire a token silently from the cache.
   */
  async acquireTokenSilent(scopes: string[]): Promise<msal.AuthenticationResult | null> {
    const app = this.getPublicApp();
    const accounts = await app.getTokenCache().getAllAccounts();

    if (accounts.length === 0) {
      return null;
    }

    try {
      return await app.acquireTokenSilent({ scopes, account: accounts[0] });
    } catch {
      return null;
    }
  }
}
