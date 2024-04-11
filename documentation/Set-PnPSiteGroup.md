---
Module Name: PnP.PowerShell
title: Set-PnPSiteGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteGroup.html
---
 
# Set-PnPSiteGroup

## SYNOPSIS

Updates the SharePoint Online owner and permission levels on a group inside a site collection.

## SYNTAX

```powershell
Set-PnPSiteGroup -Identity <String> [-Name <String>] [-Owner <String>] [-PermissionLevelsToAdd <String[]>] 
    [-PermissionLevelsToRemove <String[]>] [-Site <PipeBind>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

For permissions and the most current information about Windows PowerShell for SharePoint Online, see the online documentation at [Intro to SharePoint Online Management Shell](https://learn.microsoft.com/powershell/sharepoint/sharepoint-online/introduction-sharepoint-online-management-shell?view=sharepoint-ps).

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPSiteGroup -Site "https://contoso.sharepoint.com/sites/siteA" -Identity "ProjectViewers" -PermissionLevelsToRemove "Full Control" -PermissionLevelsToAdd "View Only"
```

Example 1 changes permission level of the ProjectViewers group inside site collection https://contoso.sharepoint.com/sites/siteA from Full Control to View Only.

### EXAMPLE 2

```powershell
Set-PnPSiteGroup -Site "https://contoso.sharepoint.com" -Identity "ProjectViewers" -Owner user@domain.com
```

Example 2 sets user@domain.com as the owner of the ProjectViewers group.

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

Specifies the name of the group.

```yaml
Type: String
Parameter Sets: (All)
Aliases:
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name

Specifies the new name of the group.

```yaml
Type: String
Parameter Sets: (All)
Aliases:
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Owner

Specifies the owner (individual or a security group) of the group to be created.

```yaml
Type: String
Parameter Sets: (All)
Aliases:
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PermissionLevelsToAdd

Specifies the permission levels to grant to the group.

> [!NOTE]
> Permission levels are defined by SharePoint Online administrators from SharePoint Online Administration Center.  

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PermissionLevelsToRemove

Specifies the permission levels to remove from the group.

> [!NOTE]
> Permission levels are defined by SharePoint Online administrators from SharePoint Online Administration Center.  

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site

Specifies the site collection the group belongs to. If not defined, the currently connected site will be used.

```yaml
Type: SitePipeBind
Parameter Sets: (All)
Aliases:
Applicable: SharePoint Online
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

