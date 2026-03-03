export enum ConnectionMethod {
  Interactive = 'Interactive',
  DeviceCode = 'DeviceCode',
  ClientCertificate = 'ClientCertificate',
  ClientSecret = 'ClientSecret',
  Credentials = 'Credentials',
  AccessToken = 'AccessToken',
  ManagedIdentity = 'ManagedIdentity',
  EnvironmentVariable = 'EnvironmentVariable',
  AzureADWorkloadIdentity = 'AzureADWorkloadIdentity',
  FederatedIdentity = 'FederatedIdentity',
  ACS = 'ACS',
}
