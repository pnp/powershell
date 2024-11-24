---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPVivaConnectionsDashboardACE.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPVivaConnectionsDashboardACE
---

# Add-PnPVivaConnectionsDashboardACE

## SYNOPSIS

Add an Adaptive card extension in the Viva connections dashboard page. This requires that you connect to a SharePoint Home site and have configured the Viva connections page.

## SYNTAX

### Default (Default)

```
Add-PnPVivaConnectionsDashboardACE [-Identity <DefaultACE>] [-Title <string>]
 [-PropertiesJSON <string>] [-Description <string>] [-IconProperty <string>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add a new an Adaptive card extension in the Viva Home dashboard page. Before running the command it is required you are connect to a SharePoint Home site and have configured the Viva connections page.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPVivaConnectionsDashboardACE -Identity CardDesigner -Order 3 -Title "Hello there" -PropertiesJSON $myProperties -CardSize Large -Description "ACE description" -Iconproperty "https://cdn.hubblecontent.osi.office.net/m365content/publish/002f8bf9-b8ee-4689-ae97-e411b756099d/691108002.jpg"
```

Add an Adaptive card extension of type Card Designer in the Viva connections dashboard page with Title, Description, IconProperty, Order , CardSize and PropertiesJSON of the ACE.

### EXAMPLE 2

```powershell
Add-PnPVivaConnectionsDashboardACE -Identity ThirdPartyApp -Order 1 -Title "Hello there" -PropertiesJSON $myProperties -CardSize Medium -Description "ACE with description" -Iconproperty "https://cdn.hubblecontent.osi.office.net/m365content/publish/002f8bf9-b8ee-4689-ae97-e411b756099d/691108002.jpg"
```

Add an Adaptive card extension of type Third party(custom adaptive card) in the Viva connections dashboard page with Title, Description, IconProperty, Order , CardSize and PropertiesJSON of the ACE.

### EXAMPLE 3

```powershell
Add-PnPVivaConnectionsDashboardACE -Identity AssignedTasks -Order 2 -Title "Tasks" -PropertiesJSON $myProperties -CardSize Medium -Description "My Assigned tasks" -Iconproperty "https://cdn.hubblecontent.osi.office.net/m365content/publish/002f8bf9-b8ee-4689-ae97-e411b756099d/691108002.jpg"
```

Add an Adaptive card extension of type AssignedTasks in the Viva connections dashboard page with Title, Description, IconProperty, Order , CardSize and PropertiesJSON of the ACE.

## PARAMETERS

### -CardSize

The size of the Adaptive Card extension. The available values are `Large` or `Medium`. Default card size is `Medium`

```yaml
Type: CardSize
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

### -Description

The Description of the Adaptive Card extension.

```yaml
Type: string
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

### -IconProperty

The Icon used by Adaptive Card extension.

```yaml
Type: string
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

The Id of the Adaptive Card on the Viva connections dashboard page. Supported values are:

- Approvals
- AssignedTasks
- CardDesigner
- Shifts
- TeamsApp
- ThirdParty
- WebLink

```yaml
Type: DefaultACE
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Order

The Order of appearance of the Adaptive Card extension on the Viva connections dashboard page. The default value is 0.

```yaml
Type: Int
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

### -PropertiesJSON

The properties of the Adaptive Card extension. You can get the properties by executing `Get-PnPVivaConnectionsDashboardACE` and then use the `JSONProperties`.

```yaml
Type: string
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

### -Title

The Title of the Adaptive Card extension.

```yaml
Type: string
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
