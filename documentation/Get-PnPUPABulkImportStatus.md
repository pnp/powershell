---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPUPABulkImportStatus.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPUPABulkImportStatus
---

# Get-PnPUPABulkImportStatus

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Get user profile bulk import status.

## SYNTAX

### Default (Default)

```
Get-PnPUPABulkImportStatus [-JobId <Guid>] [-IncludeErrorDetails] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Retrieve information about the status of submitted user profile bulk upload jobs.

Possible statuses are defined in the [ImportProfilePropertiesJobState enumeration](https://learn.microsoft.com/previous-versions/office/sharepoint-csom/mt643017(v=office.15)):

- Unknown
- Submitted
- Processing
- Queued
- Succeeded
- Error

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPUPABulkImportStatus
```

This will list the status of all submitted user profile bulk import jobs.

### EXAMPLE 2

```powershell
Get-PnPUPABulkImportStatus -IncludeErrorDetails
```

This will list the status of all submitted user profile bulk import jobs, and if it contains an error it will include the error log messages if present.

### EXAMPLE 3

```powershell
Get-PnPUPABulkImportStatus -JobId <guid>
```

This will list the status for the specified import job.

### EXAMPLE 4

```powershell
Get-PnPUPABulkImportStatus -JobId <guid> -IncludeErrorDetails
```

This will list the status for the specified import job, and if it contains an error it will include the error log messages if present.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
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

### -IncludeErrorDetails

Include error log details

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -JobId

The instance id of the job

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
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
