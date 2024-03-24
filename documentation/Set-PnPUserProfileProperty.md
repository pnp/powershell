---
Module Name: PnP.PowerShell
title: Set-PnPUserProfileProperty
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPUserProfileProperty.html
---
 
# Set-PnPUserProfileProperty

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Office365 only: Uses the tenant API to retrieve site information. You must connect to the tenant admin website (https://\<tenant\>-admin.sharepoint.com) with Connect-PnPOnline in order to use this command.

## SYNTAX

### Single
```powershell
Set-PnPUserProfileProperty -Account <String> -PropertyName <String> -Value <String>
 [-Connection <PnPConnection>] 
```

### Multi
```powershell
Set-PnPUserProfileProperty -Account <String> -PropertyName <String> -Values <String[]>
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Updates the value of a specific user profile property for a single user profile in the SharePoint Online environment. Requires a connection to the SharePoint Tenant Admin site.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPUserProfileProperty -Account 'john@domain.com' -Property 'SPS-Location' -Value 'Stockholm'
```

Sets the SPS-Location property to 'Stockholm' for the user john@domain.com.

### EXAMPLE 2
```powershell
Set-PnPUserProfileProperty -Account 'john@domain.com' -Property 'MyProperty' -Values 'Value 1','Value 2'
```

Sets the MyProperty multi value property for the user john@domain.com.

## PARAMETERS

### -Account
The account of the user, formatted either as a login name, or as a claims identity, e.g. i:0#.f|membership|user@domain.com

```yaml
Type: String
Parameter Sets: (All)

Required: True
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

### -PropertyName
The property to set, for instance SPS-Skills or SPS-Location.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Value
The value to set in the case of a single value property.

```yaml
Type: String
Parameter Sets: Single

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Values
The values set in the case of a multi value property, e.g. "Value 1","Value 2"

```yaml
Type: String[]
Parameter Sets: Multi

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

