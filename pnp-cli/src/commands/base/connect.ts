import { Command } from 'commander';
import { BaseCommand } from '../../core/base-command.js';
import { ConnectionStore } from '../../auth/connection-store.js';
import { PnPConnection } from '../../auth/connection.js';
import { AzureEnvironment } from '../../enums/azure-environment.js';
import { connectInteractive } from '../../auth/auth-strategies/interactive.js';
import { connectDeviceCode } from '../../auth/auth-strategies/device-code.js';
import { connectWithCertificate } from '../../auth/auth-strategies/client-certificate.js';
import { connectWithClientSecret } from '../../auth/auth-strategies/client-secret.js';
import { connectWithAccessToken } from '../../auth/auth-strategies/access-token.js';
import { connectWithCredentials } from '../../auth/auth-strategies/credentials.js';
import { connectWithManagedIdentity } from '../../auth/auth-strategies/managed-identity.js';
import { connectWithEnvironmentVariable } from '../../auth/auth-strategies/environment-variable.js';
import { connectWithWorkloadIdentity } from '../../auth/auth-strategies/workload-identity.js';
import { connectWithFederatedIdentity } from '../../auth/auth-strategies/federated-identity.js';

/**
 * Connect to a Microsoft 365 environment.
 * Equivalent to Connect-PnPOnline in the C# codebase.
 */
export class ConnectCommand extends BaseCommand {
  register(program: Command): void {
    program
      .command('connect')
      .description('Connect to a Microsoft 365 environment')
      .option('--url <url>', 'SharePoint site URL to connect to')
      .option('--interactive', 'Use interactive browser login')
      .option('--device-code', 'Use device code flow')
      .option('--client-id <clientId>', 'Azure AD Application Client ID')
      .option('--client-secret <clientSecret>', 'Azure AD Application Client Secret')
      .option('--certificate-path <path>', 'Path to PFX certificate file')
      .option('--certificate-base64 <base64>', 'Base64 encoded certificate')
      .option('--thumbprint <thumbprint>', 'Certificate thumbprint')
      .option('--tenant <tenant>', 'Azure AD tenant ID or domain')
      .option('--tenant-admin-url <url>', 'SharePoint tenant admin URL')
      .option('--access-token <token>', 'Pre-acquired access token')
      .option('--username <username>', 'Username for credentials auth')
      .option('--password <password>', 'Password for credentials auth')
      .option('--managed-identity', 'Use managed identity')
      .option('--user-assigned-managed-identity-object-id <id>', 'User-assigned managed identity object ID')
      .option('--user-assigned-managed-identity-client-id <id>', 'User-assigned managed identity client ID')
      .option('--user-assigned-managed-identity-azure-resource-id <id>', 'User-assigned managed identity Azure resource ID')
      .option('--environment-variable', 'Use environment variable auth')
      .option('--azure-ad-workload-identity', 'Use Azure AD Workload Identity')
      .option('--federated-identity', 'Use federated identity (GitHub Actions/Azure DevOps)')
      .option('--azure-environment <env>', 'Azure environment (Production, China, USGovernment, USGovernmentHigh, USGovernmentDoD, Germany)', 'Production')
      .option('--scopes <scopes...>', 'Permission scopes to request')
      .option('--return-connection', 'Output the connection object')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const clientId = (options.clientId as string) || PnPConnection.PNP_MANAGEMENT_SHELL_CLIENT_ID;
    const url = options.url as string | undefined;
    const tenant = options.tenant as string | undefined;
    const tenantAdminUrl = options.tenantAdminUrl as string | undefined;
    const azureEnvironment = (options.azureEnvironment as AzureEnvironment) || AzureEnvironment.Production;

    let connection: PnPConnection;

    if (options.accessToken) {
      connection = connectWithAccessToken(url, options.accessToken as string, tenantAdminUrl, azureEnvironment);
    } else if (options.clientSecret) {
      connection = await connectWithClientSecret(
        url, clientId, options.clientSecret as string, tenant!, tenantAdminUrl, azureEnvironment,
      );
    } else if (options.certificatePath || options.certificateBase64) {
      connection = await connectWithCertificate(
        url, clientId, tenant!,
        options.certificatePath as string | undefined,
        options.certificateBase64 as string | undefined,
        options.thumbprint as string | undefined,
        tenantAdminUrl, azureEnvironment,
      );
    } else if (options.username && options.password) {
      connection = await connectWithCredentials(
        url, clientId, options.username as string, options.password as string,
        tenant, tenantAdminUrl, azureEnvironment,
      );
    } else if (options.managedIdentity) {
      connection = await connectWithManagedIdentity(
        url,
        options.userAssignedManagedIdentityObjectId as string | undefined,
        options.userAssignedManagedIdentityClientId as string | undefined,
        options.userAssignedManagedIdentityAzureResourceId as string | undefined,
        tenantAdminUrl, azureEnvironment,
      );
    } else if (options.environmentVariable) {
      connection = await connectWithEnvironmentVariable(url, clientId, tenantAdminUrl, azureEnvironment);
    } else if (options.azureAdWorkloadIdentity) {
      connection = await connectWithWorkloadIdentity(url, tenantAdminUrl, azureEnvironment);
    } else if (options.federatedIdentity) {
      connection = await connectWithFederatedIdentity(url, clientId, tenant!, tenantAdminUrl, azureEnvironment);
    } else if (options.deviceCode) {
      connection = await connectDeviceCode(url, clientId, tenant, tenantAdminUrl, azureEnvironment);
    } else {
      // Default to interactive
      connection = await connectInteractive(
        url, clientId, tenant, tenantAdminUrl, azureEnvironment, options.scopes as string[] | undefined,
      );
    }

    await ConnectionStore.save(connection);

    if (options.returnConnection) {
      this.writeOutput(connection.toJSON());
    } else {
      console.log(`Connected to ${url || 'Microsoft 365'}`);
    }
  }
}
