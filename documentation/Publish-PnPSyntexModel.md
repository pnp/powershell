---
Module Name: PnP.PowerShell
title: Publish-PnPSyntexModel
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPage.html
---
 
# Publish-PnPSyntexModel

## SYNOPSIS
Publishes a SharePoint Syntex models to a list.

This cmdlet only works when you've connected to a SharePoint Syntex Content Center site.

## SYNTAX

```powershell
Publish-PnPSyntexModel -Model <SyntexModelPipeBind> -ListWebUrl <string> -List <ListPipeBind> [-PublicationViewOption <MachineLearningPublicationViewOption>]  [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION
This command publishes a SharePoint Syntex content understanding models to a list.

## EXAMPLES

### EXAMPLE 1
```powershell
Publish-PnPSyntexModel -Model "Invoice model" -ListWebUrl "https://contoso.sharepoint.com/sites/finance" -List "Documents"
```

Publishes the content understanding model named "Invoice model" to the list named "Documents" in the /sites/finance web.

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

### -Model
The name or id of the SharePoint Syntex model.

```yaml
Type: SyntexModelPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ListWebUrl
Url of the web hosting the list to publish the model to.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -List
The name or id of the list to publish the model to.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -PublicationViewOption
The view options to apply when publishing the model to the list.

```yaml
Type: MachineLearningPublicationViewOption
Parameter Sets: (All)
Accepted values: NewView, NewViewAsDefault, NoNewView

Required: False
Position: Named
Default value: NewViewAsDefault
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

