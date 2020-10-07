---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpupabulkimportstatus
schema: 2.0.0
title: Get-PnPUPABulkImportStatus
---

# Get-PnPUPABulkImportStatus

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Get user profile bulk import status.

## SYNTAX

```
Get-PnPUPABulkImportStatus [-JobId <GuidPipeBind>] [-IncludeErrorDetails] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION
Retrieve information about the status of submitted user profile bulk upload jobs.

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
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeErrorDetails
Include error log details

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -JobId
The instance id of the job

```yaml
Type: GuidPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)