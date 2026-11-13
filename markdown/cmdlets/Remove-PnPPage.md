---
Module Name: PnP.PowerShell
title: Remove-PnPPage
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPPage.html
---
 
# Remove-PnPPage

## SYNOPSIS
Removes a page

## SYNTAX

```powershell
Remove-PnPPage [-Identity] <PagePipeBind> [-Force] [-Recycle]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to remove a page.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPPage -Identity "MyPage"
```

Removes the page named 'MyPage.aspx'

### EXAMPLE 2
```powershell
Remove-PnPPage -Identity "Templates/MyPageTemplate"
```

Removes the specified page which is located in the Templates folder of the Site Pages library.

### EXAMPLE 3
```powershell
Remove-PnPPage $page
```

Removes the specified page which is contained in the $page variable.

### EXAMPLE 4
```powershell
Remove-PnPPage -Identity "MyPage" -Recycle
```

Removes the page named 'MyPage.aspx' and sends it to the recycle bin.

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

### -Recycle
Specifying the Recycle parameter will delete the page and send it to recycle bin.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

