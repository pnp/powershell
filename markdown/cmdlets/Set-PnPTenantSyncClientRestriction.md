---
Module Name: PnP.PowerShell
title: Set-PnPTenantSyncClientRestriction
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTenantSyncClientRestriction.html
---
 
# Set-PnPTenantSyncClientRestriction

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Sets organization-level sync client restriction properties

## SYNTAX

```powershell
Set-PnPTenantSyncClientRestriction [-BlockMacSync] [-DisableReportProblemDialog]
 [-DomainGuids <System.Collections.Generic.List`1[System.Guid]>] [-Enable]
 [-ExcludedFileExtensions <System.Collections.Generic.List`1[System.String]>]
 [-GrooveBlockOption <GrooveBlockOption>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Sets organization-level sync client restriction properties such as BlockMacSync, OptOutOfGroveBlock, and DisableReportProblemDialog.

You must have the SharePoint Online admin or Global admin role to run the cmdlet.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTenantSyncClientRestriction -BlockMacSync:$false
```

This example blocks access to Mac sync clients for OneDrive file synchronization

### EXAMPLE 2
```powershell
Set-PnPTenantSyncClientRestriction  -ExcludedFileExtensions "pptx;docx;xlsx"
```

This example blocks syncing of PowerPoint, Word, and Excel file types using the new sync client (OneDrive.exe).

## PARAMETERS

### -BlockMacSync
Block Mac sync clients-- the Beta version and the new sync client (OneDrive.exe). The values for this parameter are $true and $false. The default value is $false.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableReportProblemDialog
Specifies if the Report Problem Dialog is disabled or not.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DomainGuids
Sets the domain GUID to add to the safe recipient list. Requires a minimum of 1 domain GUID. The maximum number of domain GUIDs allowed are 125. I.e. 634c71f6-fa83-429c-b77b-0dba3cb70b93,4fbc735f-0ac2-48ba-b035-b1ae3a480887.

```yaml
Type: System.Collections.Generic.List`1[System.Guid]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Enable
Enables the feature to block sync originating from domains that are not present in the safe recipients list.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludedFileExtensions
Blocks certain file types from syncing with the new sync client (OneDrive.exe). Provide as one string separating the extensions using a semicolon (;). I.e. "docx;pptx"

```yaml
Type: System.Collections.Generic.List`1[System.String]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -GrooveBlockOption
Controls whether or not a tenant's users can sync OneDrive for Business libraries with the old OneDrive for Business sync client. The valid values are OptOut, HardOptin, and SoftOptin. GrooveBlockOption is planned to be deprecated. Please refrain from using the parameter.

```yaml
Type: GrooveBlockOption
Parameter Sets: (All)
Accepted values: OptOut, HardOptin, SoftOptin

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

