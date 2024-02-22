---
Module Name: PnP.PowerShell
title: Get-PnPPageLikedByInformation
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPageLikedByInformation.html
---
 
# Get-PnPPageLikedByInformation

## SYNOPSIS
Returns liked-by Information of a modern page 

## SYNTAX

```powershell
Get-PnPPageLikedByInformation -Identity <PagePipeBind> [-Connection <PnPConnection>]
```

## DESCRIPTION
This command retrieves the LikedBy Information of a modern page. 


## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPPageLikedByInformation -Identity "MyPage.aspx"
```

Gets the LikedBy Information of page named 'MyPage.aspx' in the current SharePoint site

### EXAMPLE 2
```powershell
Get-PnPPageLikedByInformation "MyPage"
```

Gets the LikedBy Information of page named 'MyPage.aspx' in the current SharePoint site


### EXAMPLE 3
```powershell
Get-PnPPageLikedByInformation -Identity "MyPage.aspx" -Web (Get-PnPWeb -Identity "Subsite1")
```

Gets the LikedBy Information of page named 'MyPage.aspx' from the subsite named 'Subsite1'

### Sample Output

```powershell
Name         : Johnny Bravo
Mail         :
Id           : 14
LoginName    : i:0#.f|membership|johnny.bravo@contosso.onmicrosoft.com
CreationDate : 2024-02-16 14:49:55

Name         : Nishkalank Bezawada
Mail         : SuperAdmin@contosso.onmicrosoft.com
Id           : 6
LoginName    : i:0#.f|membership|superadmin@contosso.onmicrosoft.com
CreationDate : 2024-02-22 19:47:24
```

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

### -Identity
The name of the page

```yaml
Type: PagePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

