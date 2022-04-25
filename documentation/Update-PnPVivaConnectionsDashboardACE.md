---
Module Name: PnP.PowerShell
title: Update-PnPVivaConnectionsDashboardACE
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Update-PnPVivaConnectionsDashboardACE.html
---
 
# Update-PnPVivaConnectionsDashboardACE

## SYNOPSIS
Update the Adaptive card extension in the Viva connections dashboard page. This requires that you connect to a SharePoint Home site and have configured the Viva connections page.

## SYNTAX

```powershell
Update-PnPVivaConnectionsDashboardACE [-Identity <GUID>] [-Title <string>] [-PropertiesJSON <string>] [-Description <string>] [-IconProperty <string>] [-Order <Int>][-CardSize <CardSize>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Update-PnPVivaConnectionsDashboardACE -Identity "58108715-185e-4214-8786-01218e7ab9ef" -Title "Update title" -Description "Update Description" -IconProperty "https://cdn.hubblecontent.osi.office.net/m365content/publish/002f8bf9-b8ee-4689-ae97-e411b756099d/691108002.jpg" -Order 4 -CardSize Large -PropertiesJSON $myProperties
```

Update the adaptive card extensions with Instance Id `58108715-185e-4214-8786-01218e7ab9ef` in the Viva connections dashboard page. It will update the Title, Description, IconProperty, Order , CardSize and PropertiesJSON of the ACE.

### EXAMPLE 2
```powershell
Update-PnPVivaConnectionsDashboardACE -Identity "58108715-185e-4214-8786-01218e7ab9ef" -Title "Update title" -Description "Update Description"
```

Update the adaptive card extensions with Instance Id `58108715-185e-4214-8786-01218e7ab9ef` in the Viva connections dashboard page. It will update the Title and Description of the ACE.

### EXAMPLE 3
```powershell
Update-PnPVivaConnectionsDashboardACE -Identity "58108715-185e-4214-8786-01218e7ab9ef" -IconProperty "https://cdn.hubblecontent.osi.office.net/m365content/publish/002f8bf9-b8ee-4689-ae97-e411b756099d/691108002.jpg" -Order 4
```

Update the adaptive card extensions with Instance Id `58108715-185e-4214-8786-01218e7ab9ef` in the Viva connections dashboard page. It will update the IconProperty and Order of the ACE.

### EXAMPLE 4
```powershell
Update-PnPVivaConnectionsDashboardACE -Identity "58108715-185e-4214-8786-01218e7ab9ef" -CardSize Large
```

Update the adaptive card extensions with Instance Id `58108715-185e-4214-8786-01218e7ab9ef` in the Viva connections dashboard page. It will update the CardSize to large.


## PARAMETERS

### -Identity
The instance Id of the Adaptive Card extension present on the Viva connections dashboard page. You can retrieve the value for this parameter by executing `Get-PnPVivaConnectionsDashboardACE` cmdlet

```yaml
Type: GUID
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The Tite of the Adaptive Card extension present on the Viva connections dashboard page.

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
The properties of the Adaptive Card extension present on the Viva connections dashboard page.

```yaml
Type: string
Parameter Sets: (All)

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

