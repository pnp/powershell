---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPContainer.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPContainer
---
  
# Remove-PnPContainer

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

The Remove-PnPContainer cmdlet removes a container from the SharePoint tenant. The container to remove is specified by the Identity parameter, which accepts a ContainerPipeBind object.

When admins delete a Container, it is moved into the Recycle Bin. A deleted Container can be restored from the Recycle Bin within 93 days. If a Container is deleted from the Recycle Bin, or it exceeds the 93-day retention period, it is permanently deleted. Deleting a Container deletes everything within it, including all documents and files. You can view all deleted Containers in the Recycle Bin with the Get-PnPDeletedContainer cmdlet.

## SYNTAX

```powershell
Remove-PnPContainer [-Identity] <ContainerPipeBind> [-Connection <PnPConnection>] 
```

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

Specify container site url or container id.

```yaml
Type: ContainerPipeBind
Parameter Sets: (All)

Required: true 
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)