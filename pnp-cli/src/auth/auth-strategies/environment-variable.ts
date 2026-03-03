import { EnvironmentCredential } from '@azure/identity';
import { PnPConnection } from '../connection.js';
import { AzureEnvironment } from '../../enums/azure-environment.js';

export async function connectWithEnvironmentVariable(
  url: string | undefined,
  clientId: string,
  tenantAdminUrl?: string,
  azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
): Promise<PnPConnection> {
  const credential = new EnvironmentCredential();

  // Validate the credential works
  const testScope = url ? `${new URL(url).origin}/.default` : 'https://graph.microsoft.com/.default';
  await credential.getToken(testScope);

  const connection = PnPConnection.createWithEnvironmentVariable(url, clientId, tenantAdminUrl, azureEnvironment);

  connection.setTokenAcquirer(async (audience: string) => {
    const result = await credential.getToken(audience);
    return result.token;
  });

  return connection;
}
