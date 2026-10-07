---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPList.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPList
---
  
# Get-PnPList

## SYNOPSIS
Returns lists from SharePoint

## SYNTAX

```powershell
Get-PnPList [[-Identity] <ListPipeBind>] [-ThrowExceptionIfListNotFound] 
 [-Connection <PnPConnection>] [-Includes <String[]>] 
```

## DESCRIPTION
This cmdlet returns lists in the current web.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPList
```

Returns all lists in the current web

### EXAMPLE 2
```powershell
Get-PnPList -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe
```

Returns a list with the given id

### EXAMPLE 3
```powershell
Get-PnPList -Identity Lists/Announcements
```

Returns a list with the given url

### EXAMPLE 4
```powershell
Get-PnPList | Where-Object {$_.RootFolder.ServerRelativeUrl -like "/lists/*"}
```

This examples shows how to do wildcard searches on the list URL. It returns all lists whose URL starts with "/lists/" This could also be used to search for strings inside of the URL.

### EXAMPLE 5
```powershell
Get-PnPList -Includes HasUniqueRoleAssignments
```

This examples shows how to retrieve additional properties of the list. 

## PARAMETERS

### -Identity
The ID, name or Url (Lists/MyList) of the list

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ThrowExceptionIfListNotFound
Switch parameter if an exception should be thrown if the requested list does not exist (true) or if omitted, nothing will be returned in case the list does not exist

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Includes
List of properties

```yaml
Type: String[]
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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
