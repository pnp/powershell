export enum AzureEnvironment {
  Production = 'Production',
  PPE = 'PPE',
  China = 'China',
  Germany = 'Germany',
  USGovernment = 'USGovernment',
  USGovernmentHigh = 'USGovernmentHigh',
  USGovernmentDoD = 'USGovernmentDoD',
  Custom = 'Custom',
}

export function getGraphEndpoint(environment: AzureEnvironment): string {
  switch (environment) {
    case AzureEnvironment.China:
      return 'microsoftgraph.chinacloudapi.cn';
    case AzureEnvironment.USGovernment:
    case AzureEnvironment.USGovernmentHigh:
      return 'graph.microsoft.us';
    case AzureEnvironment.USGovernmentDoD:
      return 'dod-graph.microsoft.us';
    case AzureEnvironment.Germany:
      return 'graph.microsoft.de';
    default:
      return 'graph.microsoft.com';
  }
}

export function getLoginEndpoint(environment: AzureEnvironment): string {
  switch (environment) {
    case AzureEnvironment.China:
      return 'login.chinacloudapi.cn';
    case AzureEnvironment.USGovernment:
    case AzureEnvironment.USGovernmentHigh:
    case AzureEnvironment.USGovernmentDoD:
      return 'login.microsoftonline.us';
    case AzureEnvironment.Germany:
      return 'login.microsoftonline.de';
    default:
      return 'login.microsoftonline.com';
  }
}
