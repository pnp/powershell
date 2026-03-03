import { PnPConnection } from '../connection.js';
import { MsalWrapper } from '../msal-wrapper.js';
import { AzureEnvironment } from '../../enums/azure-environment.js';

export async function connectDeviceCode(
  url: string | undefined,
  clientId: string,
  tenant?: string,
  tenantAdminUrl?: string,
  azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
): Promise<PnPConnection> {
  const msal = new MsalWrapper(clientId, tenant, azureEnvironment);

  const scopes = url ? [`${new URL(url).origin}/.default`] : ['https://graph.microsoft.com/.default'];
  await msal.acquireTokenByDeviceCode(scopes);

  const connection = PnPConnection.createWithDeviceCode(url, clientId, tenantAdminUrl, azureEnvironment);

  connection.setTokenAcquirer(async (audience: string) => {
    const silent = await msal.acquireTokenSilent([audience]);
    if (silent) return silent.accessToken;

    const result = await msal.acquireTokenByDeviceCode([audience]);
    return result.accessToken;
  });

  return connection;
}
