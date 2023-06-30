---
Module Name: PnP.PowerShell
title: Set-PnPTenantCdnPolicy
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTenantCdnPolicy.html
---
 
# Set-PnPTenantCdnPolicy

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Sets the CDN Policies for the specified CDN (Public | Private).

## SYNTAX

```powershell
Set-PnPTenantCdnPolicy -CdnType <SPOTenantCdnType> -PolicyType <SPOTenantCdnPolicyType> -PolicyValue <String>
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Sets the CDN Policies for the specified CDN (Public | Private).

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTenantCdnPolicy -CdnType Public -PolicyType IncludeFileExtensions -PolicyValue "CSS,EOT,GIF,ICO,JPEG,JPG,JS,MAP,PNG,SVG,TTF,WOFF"
```

This example sets the IncludeFileExtensions policy to the specified value.

### EXAMPLE 2
```powershell
Set-PnPTenantCdnPolicy -CdnType Public -PolicyType ExcludeRestrictedSiteClassifications -PolicyValue "Confidential,Restricted"
```

This example sets the ExcludeRestrictedSiteClassifications policy for the selected CdnType to a policy value of listed excluded site classifications.

## PARAMETERS

### -CdnType
The type of cdn to retrieve the policies from

```yaml
Type: SPOTenantCdnType
Parameter Sets: (All)
Accepted values: Public, Private

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

### -PolicyType
The type of the policy to set

```yaml
Type: SPOTenantCdnPolicyType
Parameter Sets: (All)
Accepted values: IncludeFileExtensions, ExcludeRestrictedSiteClassifications, ExcludeIfNoScriptDisabled

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PolicyValue
The value of the policy to set

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

