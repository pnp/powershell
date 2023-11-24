---
Module Name: PnP.PowerShell
title: Set-PnPComplianceTagOnBulkItems
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPComplianceTagOnBulkItems.html
---
 
# Set-PnPComplianceTagOnBulkItems

## SYNOPSIS

Sets a compliance tag (retention label) on one or many ListItems. 

## SYNTAX

```powershell
Set-PnPComplianceTagOnBulkItems -List <ListPipeBind> -ItemIds <List<Int32>> -ComplianceTag <String> [-BatchSize <Int32>] [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
This cmdlet allows to set a compliance tag (retention label) on one or many ListItems. Cmdlet allows passing of unlimited number of ListItems - items will be split and processed in batches (CSOM method SetComplianceTagOnBulkItems has a hard count limit on number of processed items in one go). If needed, batch size may be adjusted with BatchSize parameter.

Providing empty ComplianceTag clears the compliance tag (retention label) from items.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPComplianceTagOnBulkItems -List "Demo List" -ItemIds @(1,2,3) -ComplianceTag "My demo compliance tag"
```

Sets "My demo compliance tag" for items with ids 1, 2 and 3 on a list "Demo List"

### EXAMPLE 2
```powershell
Set-PnPComplianceTagOnBulkItems -List "Demo List" -ItemIds @(1,2,3) -ComplianceTag ""
```

Clears a compliant tag from items with ids 1, 2 and 3 from a list "Demo List"

## PARAMETERS

### -List
The ID, Title or Url of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ItemIds
List of iist item IDs. 

```yaml
Type: List<Int32>
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ComplianceTag
Name of compliance tag (retention label) to be set or empty value to clear existing tag.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BatchSize
Optional batch size.

```yaml
Type: Int32
Parameter Sets: (All)

Required: True
Position: Named
Default value: 25
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

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Learn article on applying retention labels](https://learn.microsoft.com/en-us/sharepoint/dev/apis/csom-methods-for-applying-retention-labels)