---
Module Name: PnP.PowerShell
title: Get-PnPPage
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPage.html
---
 
# Get-PnPPage

## SYNOPSIS
Returns a modern page

## SYNTAX

```powershell
Get-PnPPage -Identity <PagePipeBind> [-Connection <PnPConnection>]
```

## DESCRIPTION
This command allows the retrieval of a modern sitepage along with its properties and contents on it. Note that for a newly created modern site, the Columns and Sections of the Home.aspx page will not be filled according to the actual site page contents. This is because the underlying CanvasContent1 will not be populated until the homepage has been edited and published. The reason for this behavior is to allow for the default homepage to be able to be updated by Microsoft as long as it hasn't been modified. For any other site page or after editing and publishing the homepage, this command will return the correct columns and sections as they are positioned on the site page.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPPage -Identity "MyPage.aspx"
```

Gets the page named 'MyPage.aspx' in the current SharePoint site

### EXAMPLE 2
```powershell
Get-PnPPage "MyPage"
```

Gets the page named 'MyPage.aspx' in the current SharePoint site

### EXAMPLE 3
```powershell
Get-PnPPage "Templates/MyPageTemplate"
```

Gets the page named 'MyPageTemplate.aspx' from the templates folder of the Page Library in the current SharePoint site

### EXAMPLE 4
```powershell
Get-PnPPage -Identity "MyPage.aspx" -Web (Get-PnPWeb -Identity "Subsite1")
```

Gets the page named 'MyPage.aspx' from the subsite named 'Subsite1'

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

