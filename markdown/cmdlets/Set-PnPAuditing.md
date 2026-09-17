---
Module Name: PnP.PowerShell
title: Set-PnPAuditing
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPAuditing.html
---
 
# Set-PnPAuditing

## SYNOPSIS
Set Auditing setting for a site

## SYNTAX

### Enable all
```powershell
Set-PnPAuditing [-EnableAll] [-RetentionTime <Int32>] [-TrimAuditLog] [-Connection <PnPConnection>]
 
```

### Disable All
```powershell
Set-PnPAuditing [-DisableAll] [-Connection <PnPConnection>] 
```

### Specific flags
```powershell
Set-PnPAuditing [-RetentionTime <Int32>] [-TrimAuditLog] [-EditItems] [-CheckOutCheckInItems] [-MoveCopyItems]
 [-DeleteRestoreItems] [-EditContentTypesColumns] [-SearchContent] [-EditUsersPermissions]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to modify Auditing setting for a site.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPAuditing -EnableAll
```

Enables all auditing settings for the current site

### EXAMPLE 2
```powershell
Set-PnPAuditing -DisableAll
```

Disables all auditing settings for the current site

### EXAMPLE 3
```powershell
Set-PnPAuditing -RetentionTime 7
```

Sets the audit log trimming to 7 days, this also enables the automatic trimming of the audit log

### EXAMPLE 4
```powershell
Set-PnPAuditing -TrimAuditLog
```

Enables the automatic trimming of the audit log

### EXAMPLE 5
```powershell
Set-PnPAuditing -RetentionTime 7 -CheckOutCheckInItems -MoveCopyItems -SearchContent
```

Sets the audit log trimming to 7 days, this also enables the automatic trimming of the audit log.

Do auditing for:
- Checking out or checking in items
- Moving or copying items to another location in the site
- Searching site content

## PARAMETERS

### -CheckOutCheckInItems
Audit checking out or checking in items

```yaml
Type: SwitchParameter
Parameter Sets: Specific flags

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

### -DeleteRestoreItems
Audit deleting or restoring items

```yaml
Type: SwitchParameter
Parameter Sets: Specific flags

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableAll
Disable all audit flags

```yaml
Type: SwitchParameter
Parameter Sets: Disable All

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EditContentTypesColumns
Audit editing content types and columns

```yaml
Type: SwitchParameter
Parameter Sets: Specific flags

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EditItems
Audit editing items

```yaml
Type: SwitchParameter
Parameter Sets: Specific flags

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EditUsersPermissions
Audit editing users and permissions

```yaml
Type: SwitchParameter
Parameter Sets: Specific flags

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableAll
Enable all audit flags

```yaml
Type: SwitchParameter
Parameter Sets: Enable all

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MoveCopyItems
Audit moving or copying items to another location in the site.

```yaml
Type: SwitchParameter
Parameter Sets: Specific flags

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RetentionTime
Set the retention time

```yaml
Type: Int32
Parameter Sets: Enable all, Specific flags

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SearchContent
Audit searching site content

```yaml
Type: SwitchParameter
Parameter Sets: Specific flags

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TrimAuditLog
Trim the audit log

```yaml
Type: SwitchParameter
Parameter Sets: Enable all, Specific flags

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

