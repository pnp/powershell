---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPViewsFromXML.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPViewsFromXML
---
  
# Add-PnPViewsFromXML

## SYNOPSIS
Adds a view(s) to a list from an XML string.

## SYNTAX

```powershell
Add-PnPViewsFromXML [-List] <ListPipeBind> [-ViewsXML <String>] 
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
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
</ListViews>
"@

Add-PnPViewsFromXML -List "Demo List" -ViewsXML $viewsXML
```

Adds a view named "Demo view" to the "Demo List" list from the XML string.

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
           <FieldRef Name='Created'  Ascending='FALSE'/>
         </OrderBy>
       </ViewQuery>
     </View>
    </List>
</ListViews>
"@

Add-PnPViewsFromXML -List "Demo List" -ViewsXML $viewsXML
```

Adds a view named "Demo view" and "Created By Me" to the "Demo List" list from the XML string.

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

### -List
The ID or Url of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ViewsXML
The XML string of the Views that you want to create in a list.

```yaml
Type: string
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


