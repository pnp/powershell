---
Module Name: PnP.PowerShell
title: Remove-PnPList
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPList.html
---
 
# Remove-PnPList

## SYNOPSIS
Deletes a list.

## SYNTAX

```powershell
Remove-PnPList [-Identity] <ListPipeBind> [-Recycle] [-LargeList] [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to remove a list.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPList -Identity Announcements
```

Removes the list named 'Announcements'. Asks for confirmation.

### EXAMPLE 2
```powershell
Remove-PnPList -Identity Announcements -Force
```

Removes the list named 'Announcements' without asking for confirmation.

### EXAMPLE 3
```powershell
Remove-PnPList -Identity Announcements -Recycle
```

Removes the list named 'Announcements' and moves it to the Recycle Bin.

### EXAMPLE 4
```powershell
Remove-PnPList -Identity Announcements -Recycle -LargeList
```

Removes the large list named 'Announcements' and moves it to the Recycle Bin.
Run Get-PnPLargeListOperationStatus -ListId <ListId> -OperationId <OperationId> to check the status of the operation.

## PARAMETERS

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

### -Force
Specifying the Force parameter will skip the confirmation question.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The ID or Title of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Recycle
When provided, the list will be moved to recycle bin. If omitted, the list will directly be deleted.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -LargeList
When provided, the large list will be moved to recycle bin through a timer job. It must be paired with the Recycle Parameter.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)