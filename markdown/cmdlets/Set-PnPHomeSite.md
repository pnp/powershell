---
Module Name: PnP.PowerShell
title: Set-PnPHomeSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPHomeSite.html
---
 
# Set-PnPHomeSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Sets the home site for your tenant. The home site needs to be a communication site.

## SYNTAX

```powershell
Set-PnPHomeSite -HomeSiteUrl <String> [VivaConnectionsDefaultStart <SwitchParameter>] [-Force <SwitchParameter>] [-DraftMode <SwitchParameter>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to set the home site of the current tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPHomeSite -HomeSiteUrl "https://yourtenant.sharepoint.com/sites/myhome"
```

Sets the home site to the provided site collection url.

### EXAMPLE 2
```powershell
Set-PnPHomeSite -HomeSiteUrl "https://yourtenant.sharepoint.com/sites/myhome" -VivaConnectionsDefaultStart:$true
```

Sets the home site to the provided site collection url and keeps the Viva Connections landing experience to the SharePoint home site.

### EXAMPLE 3
```powershell
Set-PnPHomeSite -HomeSiteUrl "https://yourtenant.sharepoint.com/sites/myhome" -VivaConnectionsDefaultStart:$true -DraftMode:$true
```

Sets the home site to the provided site collection url and keeps the Viva Connections landing experience to the SharePoint home site but it will be in draft mode.

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

### -HomeSiteUrl
The url of the site to set as the home site.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -VivaConnectionsDefaultStart
When set to $true, the VivaConnectionsDefaultStart parameter will keep the Viva Connections landing experience to the SharePoint home site. If set to $false the Viva Connections home experience will be used. 

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: true
Accept pipeline input: False
Accept wildcard characters: False
```

### -DraftMode
When set to $true, the DraftMode parameter will keep the Viva Connections landing experience to the SharePoint home site in draft mode.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: true
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Use the -Force flag to bypass the confirmation question.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: true
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Set up a home site for your organization](https://learn.microsoft.com/sharepoint/home-site)
[Customize and edit the Viva Connections home experience](https://learn.microsoft.com/en-us/viva/connections/edit-viva-home)
