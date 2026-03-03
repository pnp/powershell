import { ManagedIdentityCredential } from '@azure/identity';
import { PnPConnection } from '../connection.js';
import { AzureEnvironment } from '../../enums/azure-environment.js';

export async function connectWithManagedIdentity(
  url?: string,
  objectId?: string,
  clientId?: string,
  azureResourceId?: string,
  tenantAdminUrl?: string,
  azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
): Promise<PnPConnection> {
  // Create the appropriate managed identity credential
  let credential: ManagedIdentityCredential;
  if (clientId) {
    credential = new ManagedIdentityCredential(clientId);
  } else if (azureResourceId) {
    credential = new ManagedIdentityCredential({ resourceId: azureResourceId });
  } else {
    // System-assigned
    credential = new ManagedIdentityCredential();
  }

  // Validate the credential works
  const testScope = url ? `${new URL(url).origin}/.default` : 'https://graph.microsoft.com/.default';
  await credential.getToken(testScope);

  const connection = PnPConnection.createWithManagedIdentity(
    url,
    objectId,
    clientId,
    azureResourceId,
    tenantAdminUrl,
    azureEnvironment,
  );

  connection.setTokenAcquirer(async (audience: string) => {
    const result = await credential.getToken(audience);
    return result.token;
  });

  return connection;
}
