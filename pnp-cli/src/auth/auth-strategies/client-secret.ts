import { PnPConnection } from '../connection.js';
import { MsalWrapper } from '../msal-wrapper.js';
import { AzureEnvironment } from '../../enums/azure-environment.js';

export async function connectWithClientSecret(
  url: string | undefined,
  clientId: string,
  clientSecret: string,
  tenant: string,
  tenantAdminUrl?: string,
  azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
): Promise<PnPConnection> {
  if (!tenant) {
    throw new Error('--tenant is required for client secret authentication.');
  }

  const msalWrapper = new MsalWrapper(clientId, tenant, azureEnvironment);

  const scopes = url ? [`${new URL(url).origin}/.default`] : ['https://graph.microsoft.com/.default'];
  await msalWrapper.acquireTokenByClientSecret(scopes, clientSecret);

  const connection = PnPConnection.createWithClientSecret(
    url,
    clientId,
    clientSecret,
    tenant,
    tenantAdminUrl,
    azureEnvironment,
  );

  connection.setTokenAcquirer(async (audience: string) => {
    const result = await msalWrapper.acquireTokenByClientSecret([audience], clientSecret);
    return result.accessToken;
  });

  return connection;
}
