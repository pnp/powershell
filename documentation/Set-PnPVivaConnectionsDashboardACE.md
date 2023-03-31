---
Module Name: PnP.PowerShell
title: Update-PnPVivaConnectionsDashboardACE
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Update-PnPVivaConnectionsDashboardACE.html
---
 
# Set-PnPVivaConnectionsDashboardACE

## SYNOPSIS
Update the Adaptive card extension in the Viva connections dashboard page. This requires that you connect to a SharePoint Home site and have configured the Viva connections page.

## SYNTAX

### Update using typed properties (Default)

```powershell
Update-PnPVivaConnectionsDashboardACE -Identity <VivaACEPipeBind> [-Title <string>] [-Properties <object>] [-Description <string>] [-IconProperty <string>] [-Order <Int>][-CardSize <CardSize>] [-Connection <PnPConnection>]
```

### Update using JSON properties

```powershell
Update-PnPVivaConnectionsDashboardACE -Identity <VivaACEPipeBind> [-Title <string>] [-PropertiesJSON <string>] [-Description <string>] [-IconProperty <string>] [-Order <Int>][-CardSize <CardSize>] [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to update the Adaptive card extension in the Viva connections dashboard page.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPVivaConnectionsDashboardACE -Identity "58108715-185e-4214-8786-01218e7ab9ef" -Title "Update title" -Description "Update Description" -IconProperty "https://cdn.hubblecontent.osi.office.net/m365content/publish/002f8bf9-b8ee-4689-ae97-e411b756099d/691108002.jpg" -Order 4 -CardSize Large -PropertiesJSON $myProperties
```

Update the adaptive card extensions with Instance Id `58108715-185e-4214-8786-01218e7ab9ef` in the Viva connections dashboard page. It will update the Title, Description, IconProperty, Order , CardSize and PropertiesJSON of the ACE.

### EXAMPLE 2
```powershell
Set-PnPVivaConnectionsDashboardACE -Identity "58108715-185e-4214-8786-01218e7ab9ef" -Title "Update title" -Description "Update Description"
```

Update the adaptive card extensions with Instance Id `58108715-185e-4214-8786-01218e7ab9ef` in the Viva connections dashboard page. It will update the Title and Description of the ACE.

### EXAMPLE 3
```powershell
Set-PnPVivaConnectionsDashboardACE -Identity "58108715-185e-4214-8786-01218e7ab9ef" -IconProperty "https://cdn.hubblecontent.osi.office.net/m365content/publish/002f8bf9-b8ee-4689-ae97-e411b756099d/691108002.jpg" -Order 4
```

Update the adaptive card extensions with Instance Id `58108715-185e-4214-8786-01218e7ab9ef` in the Viva connections dashboard page. It will update the IconProperty and Order of the ACE.

### EXAMPLE 4
```powershell
Set-PnPVivaConnectionsDashboardACE -Identity "58108715-185e-4214-8786-01218e7ab9ef" -CardSize Large
```

Update the adaptive card extensions with Instance Id `58108715-185e-4214-8786-01218e7ab9ef` in the Viva connections dashboard page. It will update the CardSize to large.

### EXAMPLE 5
```powershell
$ace = Get-PnPVivaConnectionsDashboardACE -Identity 58108715-185e-4214-8786-01218e7ab9ef
$ace.Properties.QuickViews[0].Data = '{ 
        "items": [ 
            { "title": "Sample 1", "image": "https://contoso.sharepoint.com/SiteAssets/image1.png" }, 
            { "title": "Sample 2", "image": "https://contoso.sharepoint.com/SiteAssets/image2.png" } 
        ]}'
Update-PnPVivaConnectionsDashboardACE -Identity $ace.InstanceId -Properties $ace.Properties
```

Update the default quickview data of the adaptive card extension with Instance Id `58108715-185e-4214-8786-01218e7ab9ef` in the Viva connections dashboard page to the provided JSON structure.

### EXAMPLE 6
```powershell
$ace = Get-PnPVivaConnectionsDashboardACE -Identity 58108715-185e-4214-8786-01218e7ab9ef
$ace.Properties.QuickViews[0].Template = '{
    "type": "AdaptiveCard",
    "$schema": "http://adaptivecards.io/schemas/adaptive-card.json",
    "version": "1.3",
    "body": [
        ...
    ]}'
Set-PnPVivaConnectionsDashboardACE -Identity $ace.InstanceId -Properties $ace.Properties
```

Update the default quickview Adaptive Cards template of the adaptive card extension with Instance Id `58108715-185e-4214-8786-01218e7ab9ef` in the Viva connections dashboard page to the provided JSON structure.

## PARAMETERS

### -Identity
The instance Id of the Adaptive Card extension present on the Viva connections dashboard page. You can retrieve the value for this parameter by executing `Get-PnPVivaConnectionsDashboardACE` cmdlet. This parameter takes either the Instance Id, the Id or the Title property. But as the latter two are not necessarily unique within the dashboard, the preferred value is to use the Instance Id of the ACE.

```yaml
Type: VivaACEPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The Title of the Adaptive Card extension present on the Viva connections dashboard page.

```yaml
Type: string
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
The Description of the Adaptive Card extension present on the Viva connections dashboard page.

```yaml
Type: string
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IconProperty
The Icon used by Adaptive Card extension present on the Viva connections dashboard page.

```yaml
Type: string
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PropertiesJSON
The properties of the Adaptive Card extension present on the Viva connections dashboard page in JSON format.

```yaml
Type: string
Parameter Sets: Update using JSON properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Properties
The typed properties of the Adaptive Card extension present on the Viva connections dashboard page. These can be retrieved and changed through the `Get-PnPVivaConnectionsDashboardACE` cmdlet and using its Properties property.

```yaml
Type: string
Parameter Sets: Update using typed properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Order
The Order of appearance of the Adaptive Card extension present on the Viva connections dashboard page.

```yaml
Type: Int
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CardSize
The size of the Adaptive Card extension present on the Viva connections dashboard page. The available values are `Large` or `Medium`.

```yaml
Type: CardSize
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

