import { ConfidentialClientApplication } from '@azure/msal-node';
import { PnPConnection } from '../connection.js';
import { AzureEnvironment } from '../../enums/azure-environment.js';
import * as fs from 'fs';

/**
 * Connects using Azure AD Workload Identity.
 * Equivalent to GetAzureADWorkloadIdentityTokenAsync in the C# codebase.
 */
export async function connectWithWorkloadIdentity(
  url?: string,
  tenantAdminUrl?: string,
  azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
): Promise<PnPConnection> {
  const clientId = process.env.AZURE_CLIENT_ID;
  const tokenPath = process.env.AZURE_FEDERATED_TOKEN_FILE;
  const tenantId = process.env.AZURE_TENANT_ID;
  const host = process.env.AZURE_AUTHORITY_HOST || 'https://login.microsoftonline.com';

  if (!clientId || !tokenPath || !tenantId) {
    throw new Error(
      'Azure AD Workload Identity environment variables not found. ' +
        'Ensure AZURE_CLIENT_ID, AZURE_FEDERATED_TOKEN_FILE, and AZURE_TENANT_ID are set.',
    );
  }

  const connection = PnPConnection.createWithWorkloadIdentity(url, tenantAdminUrl, azureEnvironment);

  connection.setTokenAcquirer(async (audience: string) => {
    const app = new ConfidentialClientApplication({
      auth: {
        clientId: clientId!,
        authority: `${host}/${tenantId}`,
        clientAssertion: async () => fs.readFileSync(tokenPath!, 'utf-8'),
      },
    });

    const result = await app.acquireTokenByClientCredential({ scopes: [audience] });
    if (!result) throw new Error('Workload identity token acquisition failed.');
    return result.accessToken;
  });

  return connection;
}
