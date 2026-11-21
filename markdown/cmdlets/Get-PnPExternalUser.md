---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPExternalUser.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPExternalUser
---
  
# Get-PnPExternalUser

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns external users in the tenant.

## SYNTAX

```powershell
Get-PnPExternalUser
   [-Position <Integer>]
   [-PageSize <Integer>]
   [-Filter <String>]
   [-SortOrder <SortOrder>]
   [-SiteUrl <String>]
   [-ShowOnlyUsersWithAcceptingAccountNotMatchInvitedAccount <Boolean>]
```

## DESCRIPTION

The Get-PnPExternalUser cmdlet returns external users that are located in the tenant based on specified criteria.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPExternalUser -Position 0 -PageSize 2
```

Returns the first two external users in the collection.

### EXAMPLE 2
```powershell
Get-PnPExternalUser -Position 2 -PageSize 2
```

Returns two external users from the third page of the collection.


## PARAMETERS

### -Filter
Prompts you for confirmation before running the cmdlet.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PageSize
Specifies the maximum number of users to be returned in the collection.

The value must be less than or equal to 50.
```yaml
Type: Integer
Parameter Sets: (All)

Required: False
Position: Named
Default value: 1
Accept pipeline input: False
Accept wildcard characters: False
```

### -Position
Use to specify the zero-based index of the position in the sorted collection of the first result to be returned.

```yaml
Type: Integer
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShowOnlyUsersWithAcceptingAccountNotMatchInvitedAccount
Shows users who have accepted an invite but not using the account the invite was sent to.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteUrl
Specifies the site to retrieve external users for.

If no site is specified, the external users for all sites are returned.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SortOrder
Specifies the sort results in Ascending or Descending order on the Email property should occur.

```yaml
Type: SortOrder
Parameter Sets: (All)

Required: False
Position: Named
Default value: Ascending
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


