import * as fs from 'fs';
import * as crypto from 'crypto';
import { PnPConnection } from '../connection.js';
import { MsalWrapper } from '../msal-wrapper.js';
import { AzureEnvironment } from '../../enums/azure-environment.js';

export async function connectWithCertificate(
  url: string | undefined,
  clientId: string,
  tenant: string,
  certificatePath?: string,
  certificateBase64?: string,
  thumbprint?: string,
  tenantAdminUrl?: string,
  azureEnvironment: AzureEnvironment = AzureEnvironment.Production,
): Promise<PnPConnection> {
  if (!certificatePath && !certificateBase64) {
    throw new Error('Either --certificate-path or --certificate-base64 must be provided.');
  }

  if (!tenant) {
    throw new Error('--tenant is required for certificate authentication.');
  }

  // Read the certificate
  let certBuffer: Buffer;
  if (certificatePath) {
    certBuffer = fs.readFileSync(certificatePath);
  } else {
    certBuffer = Buffer.from(certificateBase64!, 'base64');
  }

  // Compute thumbprint if not provided
  const computedThumbprint = thumbprint || crypto.createHash('sha1').update(certBuffer).digest('hex').toUpperCase();

  const msalWrapper = new MsalWrapper(clientId, tenant, azureEnvironment);

  const clientCertificate = {
    thumbprint: computedThumbprint,
    privateKey: certBuffer.toString('utf-8'),
  };

  const scopes = url ? [`${new URL(url).origin}/.default`] : ['https://graph.microsoft.com/.default'];
  await msalWrapper.acquireTokenByCertificate(scopes, clientCertificate);

  const connection = PnPConnection.createWithCertificate(
    url,
    clientId,
    tenant,
    certificatePath,
    certificateBase64,
    computedThumbprint,
    tenantAdminUrl,
    azureEnvironment,
  );

  connection.setTokenAcquirer(async (audience: string) => {
    const result = await msalWrapper.acquireTokenByCertificate([audience], clientCertificate);
    return result.accessToken;
  });

  return connection;
}
