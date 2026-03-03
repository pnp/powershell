import { PnPConnection } from '../connection.js';
import { AzureEnvironment } from '../../enums/azure-environment.js';

export function connectWithAccessToken(
  url: string | undefined,
  accessToken: string,
  tenantAdminUrl?: string,
  azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
): PnPConnection {
  if (!accessToken) {
    throw new Error('--access-token must be provided.');
  }

  return PnPConnection.createWithAccessToken(url, accessToken, tenantAdminUrl, azureEnvironment);
}
