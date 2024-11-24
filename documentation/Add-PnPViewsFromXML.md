---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPViewsFromXML.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPViewsFromXML
---

# Add-PnPViewsFromXML

## SYNOPSIS

Adds one or more views to a list from an XML string.

## SYNTAX

### Default (Default)

```
Add-PnPViewsFromXML [-List] <ListPipeBind> [-ViewsXML <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows the creation of one or more views on a SharePoint Online list based on passing in an XML definition with the view details.

## EXAMPLES

### EXAMPLE 1

```powershell

$viewsXML = @"
<ListViews>
   <List Type='GenericList'>
     <View Name='Demo View' ViewTypeKind='Html' OrderedView='TRUE' ViewFields='Author,Created,Editor,Modified' RowLimit='30' DefaultView='TRUE'>
       <ViewQuery>
         <OrderBy>
           <FieldRef Name='ID' Ascending='FALSE'/>
         </OrderBy>
       </ViewQuery>
     </View>
    </List>
</ListViews>
"@

Add-PnPViewsFromXML -List "Demo List" -ViewsXML $viewsXML
```

Adds one view named "Demo view" to the "Demo List" list from the XML string.

### EXAMPLE 2

```powershell

$viewsXML = @"
<ListViews>
   <List Type='GenericList'>
     <View Name='Demo View' ViewTypeKind='Html' OrderedView='TRUE' ViewFields='Author,Created,Editor,Modified' RowLimit='30' DefaultView='TRUE'>
       <ViewQuery>
         <OrderBy>
           <FieldRef Name='ID'  Ascending='FALSE'/>
         </OrderBy>
       </ViewQuery>
     </View>
    </List>
    <List Type='GenericList'>
     <View Name='Created By Me' ViewTypeKind='Html' OrderedView='TRUE' ViewFields='Author,Created,Editor,Modified' RowLimit='30' DefaultView='FALSE'>
       <ViewQuery>
         <Where>
           <Eq>
             <FieldRef Name='Author' />
             <Value Type='Integer'>
               <UserID Type='Integer' />
             </Value>
           </Eq>
         </Where>
         <OrderBy>
           <FieldRef Name='Created' Ascending='FALSE'/>
         </OrderBy>
       </ViewQuery>
     </View>
    </List>
</ListViews>
"@

Add-PnPViewsFromXML -List "Demo List" -ViewsXML $viewsXML
```

Adds two views named "Demo view" and "Created By Me" to the "Demo List" list from the XML string.

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

### -List

The ID or Url of the list to add the view to.

```yaml
Type: ListPipeBind
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

### -ViewsXML

The XML string of the view(s) that you want to add to the list.

```yaml
Type: string
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
