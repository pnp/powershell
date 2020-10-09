---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpmicrosoft365group
schema: 2.0.0
title: Get-PnPMicrosoft365Group
---

# Get-PnPMicrosoft365Group

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All, Group.Read.All, Group.ReadWrite.All, GroupMember.Read.All, GroupMember.ReadWrite.All

Gets one Microsoft 365 Group or a list of Microsoft 365 Groups

## SYNTAX

```powershell
Get-PnPMicrosoft365Group [-Identity <Microsoft365GroupPipeBind>] [-ExcludeSiteUrl] [-IncludeClassification]
 [-IncludeHasTeam] [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-Microsoft365Group
```

Retrieves all the Microsoft 365 Groups

### EXAMPLE 2
```powershell
Get-Microsoft365Group -Identity $groupId
```

Retrieves a specific Microsoft 365 Group based on its ID

### EXAMPLE 3
```powershell
Get-Microsoft365Group -Identity $groupDisplayName
```

Retrieves a specific or list of Microsoft 365 Groups that start with the given DisplayName

### EXAMPLE 4
```powershell
Get-Microsoft365Group -Identity $groupSiteMailNickName
```

Retrieves a specific or list of Microsoft 365 Groups for which the email starts with the provided mail nickName

### EXAMPLE 5
```powershell
Get-Microsoft365Group -Identity $group
```

Retrieves a specific Microsoft 365 Group based on its object instance

### EXAMPLE 6
```powershell
Get-Microsoft365Group -IncludeIfHasTeam
```

Retrieves all the Microsoft 365 Groups and checks for each of them if it has a Microsoft Team provisioned for it

## PARAMETERS

### -ByPassPermissionCheck
Allows the check for required permissions in the access token to be bypassed when set to $true

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludeSiteUrl
Exclude fetching the site URL for Microsoft 365 Groups. This speeds up large listings.

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
The Identity of the Microsoft 365 Group

```yaml
Type: Microsoft365GroupPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeClassification
Include Classification value of Microsoft 365 Groups

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeHasTeam
Include a flag for every Microsoft 365 Group if it has a Microsoft Team provisioned for it. This will slow down the retrieval of Microsoft 365 Groups so only use it if you need it.

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

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)