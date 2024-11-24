---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPUnifiedAuditLog.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPUnifiedAuditLog
---

# Get-PnPUnifiedAuditLog

## SYNOPSIS

**Required Permissions**

  * Microsoft Office 365 Management API: ActivityFeed.Read, Microsoft Office 365 Management API: ActivityFeed.ReadDlp, Microsoft Office 365 Management API: ActivityReports.Read, Microsoft Office 365 Management API: ServiceHealth.Read and Microsoft Office 365 Management API:ThreatIntelligence.Read

Gets unified audit logs from the Office 365 Management API. Requires the Azure Entra application permission 'ActivityFeed.Read', 'ActivityFeed.ReadDlp', 'ActivityReports.Read', 'ServiceHealth.Read' and 'ThreatIntelligence.Read'.

Before you can access audit log data, you must enable unified audit logging for your Microsoft 365 tenant. For instructions, check out the page [Turn auditing on or off](https://learn.microsoft.com/microsoft-365/compliance/audit-log-enable-disable).

When running this command for the first time for a certain content type, a subscription for this content type is created. It can take up to 12 hours for the first content blobs to become available for that subscription.

Retrieving audit logs is an intensive process, especially for large or active tenants. In this case it may take some time to retrieve all audit logs.

## SYNTAX

### Default (Default)

```
Get-PnPUnifiedAuditLog [-ContentType <AuditContentType>] [-StartTime <DateTime>]
 [-EndTime <DateTime>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve unified audit logs from the Office 365 Management API.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPUnifiedAuditLog -ContentType SharePoint -StartTime (Get-Date -asUtc).AddDays(-2) -EndTime (Get-Date -asUtc).AddDays(-1)
```

Retrieves the audit logs of SharePoint happening between the current time yesterday and the current time the day before yesterday

## PARAMETERS

### -ContentType

Content type of logs to be retrieved, should be one of the following: AzureActiveDirectory, Exchange, SharePoint, General, DLP.

```yaml
Type: AuditContentType
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- AzureActiveDirectory
- Exchange
- SharePoint
- General
- DLP
HelpMessage: ''
```

### -EndTime

UTC end time of logs to be retrieved. Start time and end time must both be specified (or both omitted) and must be less than or equal to 24 hours apart. If passed as a string this should be defined as a valid ISO 8601 string (2024-01-16T18:28:48.6964197Z). If you don't include a timestamp in the value, the default timestamp is 12:00 AM (midnight) on the specified date.

```yaml
Type: DateTime
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -StartTime

UTC start time of logs to be retrieved. Start time and end time must both be specified (or both omitted) and must be less than or equal to 24 hours apart, with the start time prior to end time and start time no more than 7 days in the past. If passed as a string this should be defined as a valid ISO 8601 string (2024-01-16T18:28:48.6964197Z). If you don't include a timestamp in the value, the default timestamp is 12:00 AM (midnight) on the specified date.

```yaml
Type: DateTime
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
