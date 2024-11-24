---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPContainer.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPContainer
---

# Get-PnPContainer

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns one or more Containers in a SharePoint repository services application.

## SYNTAX

### Default (Default)

```
Get-PnPContainer [-Identity <ContainerPipeBind>] [-OwningApplicationId <Guid>]
 [-Paged <switchparameter>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPContainer -OwningApplicationId a187e399-0c36-4b98-8f04-1edc167a0996
```

Returns a tabular list of Containers created under the specified SharePoint repository services application.

### EXAMPLE 2
```powershell
Get-PnPContainer -OwningApplicationId a187e399-0c36-4b98-8f04-1edc167a0996 -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1"
```

Returns the properties of the specified container by using the container id

### EXAMPLE 3
```powershell
Get-PnPContainer -Identity "bc07d4b8-1c2f-4184-8cc2-a52dfd6fe0c4" -Identity  "https://contoso.sharepoint.com/contentstorage/CSP_4bd71a68-8312-4275-99b1-a2b70e3ba0e8"
```

Returns the properties of the specified container by using the container url

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPContainer -OwningApplicationId a187e399-0c36-4b98-8f04-1edc167a0996
```

Returns a tabular list of Containers created under the specified SharePoint repository services application.

### EXAMPLE 2

```powershell
Get-PnPContainer -OwningApplicationId a187e399-0c36-4b98-8f04-1edc167a0996 -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1"
```

Returns the properties of the specified container by using the container id

### EXAMPLE 3

```powershell
Get-PnPContainer -Identity "bc07d4b8-1c2f-4184-8cc2-a52dfd6fe0c4" -Identity  "https://contoso.sharepoint.com/contentstorage/CSP_4bd71a68-8312-4275-99b1-a2b70e3ba0e8"
```

Returns the properties of the specified container by using the container url

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
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -OwningApplicationId

This parameter specifies the ID of the SharePoint repository services application.

To retrieve Containers for the Microsoft Loop app, use OwningApplicationId: a187e399-0c36-4b98-8f04-1edc167a0996.
To retrieve Containers for the Microsoft Designer app, use OwningApplicationId: 5e2795e3-ce8c-4cfb-b302-35fe5cd01597

```yaml
Type: Guid
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

### -Paged

This parameter can be used when there are more than 5,000 Containers in a given SharePoint repository services application. Using -Paged will provide a <Paging Token> that will display the next 5,000 Containers.

```yaml
Type: SwitchParameter
DefaultValue: False
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

### -PagingToken

Use this parameter to provide the <Paging Token> provided to view the remaining Containers as shown in Example 5. If there are no more Containers to display, the commandlet output will return the message End of Containers view. Otherwise, use the given <Paging Token> to retrieve the next batch of up to 5,000 ontainers.

```yaml
Type: String
DefaultValue: False
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

### -SortOrder

Use this parameter to specify the sort order. The sorting will be done based on Storage used in ascending or descending order.

```yaml
Type: SortOrder
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
