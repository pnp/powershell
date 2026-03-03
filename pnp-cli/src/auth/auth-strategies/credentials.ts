import { PnPConnection } from '../connection.js';
import { MsalWrapper } from '../msal-wrapper.js';
import { AzureEnvironment } from '../../enums/azure-environment.js';

export async function connectWithCredentials(
  url: string | undefined,
  clientId: string,
  username: string,
  password: string,
  tenant?: string,
  tenantAdminUrl?: string,
  azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
): Promise<PnPConnection> {
  const msalWrapper = new MsalWrapper(clientId, tenant, azureEnvironment);

  const scopes = url ? [`${new URL(url).origin}/.default`] : ['https://graph.microsoft.com/.default'];
  await msalWrapper.acquireTokenByUsernamePassword(scopes, username, password);

  const connection = PnPConnection.createWithCredentials(url, clientId, tenantAdminUrl, azureEnvironment);

  connection.setTokenAcquirer(async (audience: string) => {
    const result = await msalWrapper.acquireTokenByUsernamePassword([audience], username, password);
    return result.accessToken;
  });

  return connection;
}
