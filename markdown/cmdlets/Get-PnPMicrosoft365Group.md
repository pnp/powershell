---
Module Name: PnP.PowerShell
schema: 2.0.0
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPMicrosoft365Group
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPMicrosoft365Group.html
---
   
# Get-PnPMicrosoft365Group

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All, Group.Read.All, Group.ReadWrite.All, GroupMember.Read.All, GroupMember.ReadWrite.All

Gets one Microsoft 365 Group or a list of Microsoft 365 Groups

## SYNTAX

```powershell
Get-PnPMicrosoft365Group [-Identity <Microsoft365GroupPipeBind>] [-IncludeSiteUrl] [-IncludeOwners] [-Detailed] [-Filter <string>] [-IncludeSensitivityLabels] [-IncludeExtensionAttributes]
```

## DESCRIPTION

Allows to retrieve Microsoft 365 Groups. By using `Identity` option you may specify the exact group that will be retrieved.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPMicrosoft365Group
```

Retrieves all the Microsoft 365 Groups

### EXAMPLE 2
```powershell
Get-PnPMicrosoft365Group -Identity $groupId
```

Retrieves a specific Microsoft 365 Group based on its ID

### EXAMPLE 3
```powershell
Get-PnPMicrosoft365Group -Identity $groupDisplayName
```

Retrieves a specific or list of Microsoft 365 Groups that start with the given DisplayName

### EXAMPLE 4
```powershell
Get-PnPMicrosoft365Group -Identity $groupSiteMailNickName
```

Retrieves a specific or list of Microsoft 365 Groups for which the email starts with the provided mail nickName

### EXAMPLE 5
```powershell
Get-PnPMicrosoft365Group -Identity $group
```

Retrieves a specific Microsoft 365 Group based on its object instance

### EXAMPLE 6
```powershell
Get-PnPMicrosoft365Group -IncludeSiteUrl
```

Retrieves all Microsoft 365 Groups in this tenant and includes the URL property for the underlying SharePoint site.

### EXAMPLE 7
```powershell
$groups = Get-PnPMicrosoft365Group -IncludeOwners
$g[0].Owners
```

Retrieves all Microsoft 365 Groups in this tenant and retrieves the owners for each group. The owners are available in the "Owners" property of the returned objects.

### EXAMPLE 8
```powershell
$groups = Get-PnPMicrosoft365Group -Filter "startswith(description, 'contoso')"
```

Retrieves all Microsoft 365 Groups in this tenant with description starting with Contoso. This example demonstrates using Advanced Query capabilities (see: https://learn.microsoft.com/graph/aad-advanced-queries?tabs=http#group-properties).

### EXAMPLE 9
```powershell
Get-PnPMicrosoft365Group -Identity $groupId | Select-Object DisplayName, PreferredDataLocation
```

Retrieves a specific Microsoft 365 Group and returns its preferred data location.

### EXAMPLE 10
```powershell
$group = Get-PnPMicrosoft365Group -Identity $groupId -IncludeExtensionAttributes
$group.OnPremisesExtensionAttributes.ExtensionAttribute1
```

Retrieves a specific Microsoft 365 Group including its extension attributes and returns the first one. Note that Microsoft Graph only populates these for groups that are synchronized from an on-premises Active Directory, see the `-IncludeExtensionAttributes` parameter.

## PARAMETERS

### -Detailed
When provided, the following properties originating from Exchange Online, will also be loaded into the returned group. Without providing this flag, they will not be populated. Providing this flag causes an extra call to be made to Microsoft Graph, so only add it when you need one of the properties below.

- AutoSubscribeNewMembers
- RequireSenderAuthenticationEnabled
- IsSubscribedByMail

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeSiteUrl
Include fetching the site URL for Microsoft 365 Groups. This slows down large listings.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeOwners
Include fetching the group owners. This slows down large listings.

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

### -Filter
Specify the query to pass to Graph API in $filter.

```yaml
Type: String
Parameter Sets: Filter

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeSensitivityLabels

Include fetching the sensitivity labels. This slows down large listings.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeExtensionAttributes

Include fetching the extension attributes 1-15 of the group. They are returned in the `OnPremisesExtensionAttributes` property of the returned group. Microsoft Graph only returns these attributes when they are explicitly requested, so providing this flag causes an extra call to be made to Microsoft Graph. This slows down large listings.

Microsoft Graph only populates these attributes for groups that are synchronized from an on-premises Active Directory. For a cloud only group they are always empty, also when the equivalent `CustomAttribute1-15` properties do hold a value in Exchange Online, as Microsoft Graph does not expose the values that Exchange Online stores. Use `Get-UnifiedGroup` from the Exchange Online PowerShell module to read those.

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


