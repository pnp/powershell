---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Clear-PnPAadGroupOwner.html
external help file: PnP.PowerShell.dll-Help.xml
title: Clear-PnPAadGroupOwner
---
  
# Clear-PnPAadGroupOwner

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All

Removes all current owners from a particular Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

```powershell
Clear-PnPAadGroupOwner -Identity <AadGroupPipeBind> [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Clear-PnPAadGroupOwner -Identity "Project Team"
```

Removes all the current owners from the Azure Active Directory group named "Project Team"

## PARAMETERS

### -Identity
The Identity of the Azure Active Directory group to remove all owners from

```yaml
Type: AadGroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)[Documentation](https://docs.microsoft.com/graph/api/group-delete-owners)


