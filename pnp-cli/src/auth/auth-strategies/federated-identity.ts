import { PnPConnection } from '../connection.js';
import { AzureEnvironment } from '../../enums/azure-environment.js';

/**
 * Connects using Federated Identity (GitHub Actions or Azure DevOps).
 * Equivalent to GetFederatedIdentityTokenAsync in the C# codebase.
 */
export async function connectWithFederatedIdentity(
  url: string | undefined,
  clientId: string,
  tenant: string,
  tenantAdminUrl?: string,
  azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
): Promise<PnPConnection> {
  if (!clientId || !tenant) {
    throw new Error('--client-id and --tenant are required for federated identity authentication.');
  }

  const connection = PnPConnection.createWithFederatedIdentity(url, clientId, tenant, tenantAdminUrl, azureEnvironment);

  connection.setTokenAcquirer(async (audience: string) => {
    const federationToken = await getFederationToken();
    return await getAccessTokenWithFederatedToken(clientId, tenant, audience, federationToken);
  });

  return connection;
}

async function getFederationToken(): Promise<string> {
  const actionsIdTokenRequestUrl = process.env.ACTIONS_ID_TOKEN_REQUEST_URL;
  const actionsIdTokenRequestToken = process.env.ACTIONS_ID_TOKEN_REQUEST_TOKEN;
  const systemOidcRequestUri = process.env.SYSTEM_OIDCREQUESTURI;

  if (actionsIdTokenRequestUrl && actionsIdTokenRequestToken) {
    // GitHub Actions
    const requestUrl = `${actionsIdTokenRequestUrl}&audience=${encodeURIComponent('api://AzureADTokenExchange')}`;
    const response = await fetch(requestUrl, {
      headers: {
        Authorization: `Bearer ${actionsIdTokenRequestToken}`,
        Accept: 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error(`Failed to retrieve GitHub federation token: HTTP ${response.status}`);
    }

    const data = (await response.json()) as Record<string, string>;
    return data.value;
  } else if (systemOidcRequestUri) {
    // Azure DevOps
    const systemAccessToken = process.env.SYSTEM_ACCESSTOKEN;
    if (!systemAccessToken) {
      throw new Error('SYSTEM_ACCESSTOKEN environment variable is not available.');
    }

    const serviceConnectionId = process.env.AZURESUBSCRIPTION_SERVICE_CONNECTION_ID;
    if (!serviceConnectionId) {
      throw new Error('AZURESUBSCRIPTION_SERVICE_CONNECTION_ID not set.');
    }

    const requestUrl = `${systemOidcRequestUri}?api-version=7.1&serviceConnectionId=${serviceConnectionId}`;
    const response = await fetch(requestUrl, {
      method: 'POST',
      headers: {
        Authorization: `Bearer ${systemAccessToken}`,
        'Content-Type': 'application/json',
        Accept: 'application/json',
      },
      body: '',
    });

    if (!response.ok) {
      throw new Error(`Failed to retrieve Azure DevOps federation token: HTTP ${response.status}`);
    }

    const data = (await response.json()) as Record<string, string>;
    return data.oidcToken;
  } else {
    throw new Error('Federated identity is currently only supported in GitHub Actions and Azure DevOps.');
  }
}

async function getAccessTokenWithFederatedToken(
  clientId: string,
  tenant: string,
  resource: string,
  federatedToken: string,
): Promise<string> {
  const params = new URLSearchParams({
    grant_type: 'client_credentials',
    scope: resource,
    client_id: clientId,
    client_assertion_type: 'urn:ietf:params:oauth:client-assertion-type:jwt-bearer',
    client_assertion: federatedToken,
  });

  const response = await fetch(`https://login.microsoftonline.com/${tenant}/oauth2/v2.0/token`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/x-www-form-urlencoded',
      Accept: 'application/json',
    },
    body: params.toString(),
  });

  if (!response.ok) {
    throw new Error(`Failed to retrieve federated access token: HTTP ${response.status}`);
  }

  const data = (await response.json()) as Record<string, string>;
  return data.access_token;
}
