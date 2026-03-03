import { PnPConnection } from '../connection.js';
import { MsalWrapper } from '../msal-wrapper.js';
import { AzureEnvironment } from '../../enums/azure-environment.js';

export async function connectInteractive(
  url: string | undefined,
  clientId: string,
  tenant?: string,
  tenantAdminUrl?: string,
  azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
  scopes?: string[],
): Promise<PnPConnection> {
  const msal = new MsalWrapper(clientId, tenant, azureEnvironment);

  // Determine scopes to request
  const requestScopes = scopes || (url ? [`${new URL(url).origin}/.default`] : ['https://graph.microsoft.com/.default']);

  const result = await msal.acquireTokenInteractive(requestScopes);

  const connection = PnPConnection.createWithInteractive(url, clientId, tenantAdminUrl, azureEnvironment, scopes);

  connection.setTokenAcquirer(async (audience: string) => {
    const silent = await msal.acquireTokenSilent([audience]);
    if (silent) return silent.accessToken;

    const interactive = await msal.acquireTokenInteractive([audience]);
    return interactive.accessToken;
  });

  return connection;
}
