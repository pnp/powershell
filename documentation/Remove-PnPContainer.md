---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPContainer.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPContainer
---

# Remove-PnPContainer

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

The Remove-PnPContainer cmdlet removes a container from the SharePoint tenant. The container to remove is specified by the Identity parameter, which accepts a ContainerPipeBind object.

When admins delete a Container, it is moved into the Recycle Bin. A deleted Container can be restored from the Recycle Bin within 93 days. If a Container is deleted from the Recycle Bin, or it exceeds the 93-day retention period, it is permanently deleted. Deleting a Container deletes everything within it, including all documents and files. You can view all deleted Containers in the Recycle Bin with the Get-PnPDeletedContainer cmdlet.

## SYNTAX

### Default (Default)

```
Remove-PnPContainer [-Identity] <ContainerPipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPContainer -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1"
```

Removes the specified container by using the container id.

### EXAMPLE 2

```powershell
Remove-PnPContainer -Identity  "https://contoso.sharepoint.com/contentstorage/CSP_4bd71a68-8312-4275-99b1-a2b70e3ba0e8"
```

Removes the the specified container by using the container url

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPContainer -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1"
```

Removes the specified container by using the container id.

### EXAMPLE 2

```powershell
Remove-PnPContainer -Identity  "https://contoso.sharepoint.com/contentstorage/CSP_4bd71a68-8312-4275-99b1-a2b70e3ba0e8"
```

Removes the the specified container by using the container url

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Identity

Specify container site url or container id.

```yaml
Type: ContainerPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
