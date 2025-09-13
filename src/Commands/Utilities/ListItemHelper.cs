using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.Core.QueryModel;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Utilities
{
    public static class ListItemHelper
    {
        private class FieldUpdateValue
        {
            public string Key { get; set; }
            public object Value { get; set; }
            public string FieldTypeString { get; set; }

            public FieldUpdateValue(string key, object value)
            {
                Key = key;
                Value = value;
            }
            public FieldUpdateValue(string key, object value, string fieldTypeString)
            {
                Key = key;
                Value = value;
                FieldTypeString = fieldTypeString;
            }
        }

        public static void SetFieldValues(this ListItem item, Hashtable valuesToSet, BasePSCmdlet cmdlet)
        {
            var itemValues = new List<FieldUpdateValue>();

            var context = item.Context as ClientContext;
            var list = item.ParentList;
            context.Web.EnsureProperty(w => w.Url);

            var clonedContext = context.Clone(context.Web.Url);
            var web = clonedContext.Web;

            var fields =
                     context.LoadQuery(list.Fields.Include(f => f.InternalName, f => f.Title,
                         f => f.TypeAsString));
            context.ExecuteQueryRetry();

            Hashtable values = valuesToSet ?? new Hashtable();

            foreach (var key in values.Keys)
            {
                var field = fields.FirstOrDefault(f => f.InternalName == key as string || f.Title == key as string);
                if (field != null)
                {
                    switch (field.TypeAsString)
                    {
                        case "User":
                        case "UserMulti":
                            {
                                List<FieldUserValue> userValues = new List<FieldUserValue>();

                                var value = values[key];
                                if (value == null) goto default;
                                if (value is string && string.IsNullOrWhiteSpace(value + "")) goto default;
                                if (value.GetType().IsArray)
                                {
                                    foreach (var arrayItem in value as IEnumerable)
                                    {
                                        int userId;
                                        if (!int.TryParse(arrayItem.ToString(), out userId))
                                        {
                                            var user = web.EnsureUser(arrayItem as string);
                                            clonedContext.Load(user);
                                            clonedContext.ExecuteQueryRetry();
                                            userValues.Add(new FieldUserValue() { LookupId = user.Id });
                                        }
                                        else
                                        {
                                            userValues.Add(new FieldUserValue() { LookupId = userId });
                                        }
                                    }
                                    itemValues.Add(new FieldUpdateValue(key as string, userValues.ToArray(), null));
                                }
                                else
                                {
                                    int userId;
                                    if (!int.TryParse(value as string, out userId))
                                    {
                                        var user = web.EnsureUser(value as string);
                                        clonedContext.Load(user);
                                        clonedContext.ExecuteQueryRetry();
                                        itemValues.Add(new FieldUpdateValue(key as string, new FieldUserValue() { LookupId = user.Id }));
                                    }
                                    else
                                    {
                                        itemValues.Add(new FieldUpdateValue(key as string, new FieldUserValue() { LookupId = userId }));
                                    }
                                }
                                break;
                            }
                        case "TaxonomyFieldType":
                        case "TaxonomyFieldTypeMulti":
                            {
                                var value = values[key];
                                if (value != null && value.GetType().IsArray)
                                {
                                    var taxSession = clonedContext.Site.GetTaxonomySession();
                                    var terms = new List<KeyValuePair<Guid, string>>();
                                    foreach (var arrayItem in value as object[])
                                    {
                                        TaxonomyItem taxonomyItem;
                                        Guid termGuid;
                                        if (!Guid.TryParse(arrayItem?.ToString(), out termGuid))
                                        {
                                            // Assume it's a TermPath
                                            taxonomyItem = clonedContext.Site.GetTaxonomyItemByPath(arrayItem?.ToString());
                                        }
                                        else
                                        {
                                            taxonomyItem = taxSession.GetTerm(termGuid);
                                            clonedContext.Load(taxonomyItem);
                                            clonedContext.ExecuteQueryRetry();
                                        }
                                        if (taxonomyItem != null)
                                        {
                                            terms.Add(new KeyValuePair<Guid, string>(taxonomyItem.Id, taxonomyItem.Name));
                                        }
                                        else
                                        {
                                            cmdlet.LogWarning("Unable to find the specified term. Skipping values for field '" + field.InternalName + "'.");
                                        }
                                    }

                                    TaxonomyField taxField = context.CastTo<TaxonomyField>(field);
                                    taxField.EnsureProperty(tf => tf.AllowMultipleValues);
                                    if (taxField.AllowMultipleValues)
                                    {
                                        var termValuesString = String.Empty;
                                        foreach (var term in terms)
                                        {
                                            termValuesString += "-1;#" + term.Value + "|" + term.Key.ToString("D") + ";#";
                                        }

                                        if (!string.IsNullOrEmpty(termValuesString))
                                        {
                                            termValuesString = termValuesString.Substring(0, termValuesString.Length - 2);

                                            var newTaxFieldValue = new TaxonomyFieldValueCollection(context, termValuesString, taxField);
                                            itemValues.Add(new FieldUpdateValue(key as string, newTaxFieldValue, field.TypeAsString));
                                        }
                                    }
                                    else
                                    {
                                        cmdlet.LogWarning("You are trying to set multiple values in a single value field. Skipping values for field '" + field.InternalName + "'.");
                                    }
                                }
                                else
                                {
                                    Guid termGuid = Guid.Empty;

                                    var taxSession = clonedContext.Site.GetTaxonomySession();
                                    TaxonomyItem taxonomyItem = null;
                                    bool updateTaxItemValue = true;
                                    if (value != null && !Guid.TryParse(value?.ToString(), out termGuid))
                                    {
                                        // Assume it's a TermPath
                                        taxonomyItem = clonedContext.Site.GetTaxonomyItemByPath(value as string);
                                        if (taxonomyItem == null)
                                        {
                                            updateTaxItemValue = false;
                                            cmdlet.LogWarning("Unable to find the specified term. Skipping values for field '" + field.InternalName + "'.");
                                        }
                                    }
                                    else
                                    {
                                        if (value != null)
                                        {
                                            taxonomyItem = taxSession.GetTerm(termGuid);
                                            clonedContext.Load(taxonomyItem);
                                            clonedContext.ExecuteQueryRetry();
                                        }
                                    }

                                    TaxonomyField taxField = context.CastTo<TaxonomyField>(field);
                                    TaxonomyFieldValue taxValue = new TaxonomyFieldValue();
                                    if (taxonomyItem != null)
                                    {
                                        taxValue.TermGuid = taxonomyItem.Id.ToString();
                                        taxValue.Label = taxonomyItem.Name;
                                        itemValues.Add(new FieldUpdateValue(key as string, taxValue, field.TypeAsString));
                                    }
                                    else
                                    {
                                        if (updateTaxItemValue)
                                        {
                                            taxField.ValidateSetValue(item, null);
                                        }
                                    }
                                }
                                break;
                            }
                        case "Lookup":
                        case "LookupMulti":
                            {
                                var value = values[key];
                                if (value == null) goto default;
                                int[] multiValue;
                                if (value is Array)
                                {
                                    var arr = (object[])values[key];
                                    multiValue = new int[arr.Length];
                                    for (int i = 0; i < arr.Length; i++)
                                    {
                                        multiValue[i] = int.Parse(arr[i].ToString());
                                    }
                                }
                                else
                                {
                                    string valStr = values[key].ToString();
                                    multiValue = valStr.Split(',', ';').Select(int.Parse).ToArray();
                                }

                                var newVals = multiValue.Select(id => new FieldLookupValue { LookupId = id }).ToArray();

                                FieldLookup lookupField = context.CastTo<FieldLookup>(field);
                                lookupField.EnsureProperty(lf => lf.AllowMultipleValues);
                                if (!lookupField.AllowMultipleValues && newVals.Length > 1)
                                {
                                    throw new Exception("Field " + field.InternalName + " does not support multiple values");
                                }
                                itemValues.Add(new FieldUpdateValue(key as string, newVals));
                                break;
                            }
                        default:
                            {
                                itemValues.Add(new FieldUpdateValue(key as string, values[key]));
                                break;
                            }
                    }
                }
                else
                {
                    throw new PSInvalidOperationException($"Field {key} not present in list.");
                }
            }
            if (item != null && !item.ServerObjectIsNull.Value)
            {
                var specialFields = new[] { "Author", "Editor", "Created", "Modified" };
                // check if we are setting editor or author fields  
                if (itemValues.Any(i => specialFields.Contains(i.Key)))
                {
                    foreach (var field in specialFields)
                    {
                        if (itemValues.FirstOrDefault(i => i.Key == field) == null)
                        {
                            if (item.FieldValues.TryGetValue(field, out object fieldValue))
                            {
                                itemValues.Add(new FieldUpdateValue(field, fieldValue));
                            }
                        }
                    }
                }
            }

            foreach (var itemValue in itemValues)
            {
                if (string.IsNullOrEmpty(itemValue.FieldTypeString))
                {
                    item[itemValue.Key] = itemValue.Value;
                }
                else
                {
                    switch (itemValue.FieldTypeString)
                    {
                        case "TaxonomyFieldTypeMulti":
                            {
                                var field = fields.FirstOrDefault(f => f.InternalName == itemValue.Key as string || f.Title == itemValue.Key as string);
                                var taxField = context.CastTo<TaxonomyField>(field);
                                if (itemValue.Value is TaxonomyFieldValueCollection)
                                {
                                    taxField.SetFieldValueByValueCollection(item, itemValue.Value as TaxonomyFieldValueCollection);
                                }
                                else
                                {
                                    taxField.SetFieldValueByValue(item, itemValue.Value as TaxonomyFieldValue);
                                }
                                break;
                            }
                        case "TaxonomyFieldType":
                            {
                                var field = fields.FirstOrDefault(f => f.InternalName == itemValue.Key as string || f.Title == itemValue.Key as string);
                                var taxField = context.CastTo<TaxonomyField>(field);
                                taxField.SetFieldValueByValue(item, itemValue.Value as TaxonomyFieldValue);
                                break;
                            }
                    }
                }
            }
        }

        public static Dictionary<string, object> GetFieldValues(PnP.Core.Model.SharePoint.IList list, PnP.Core.Model.SharePoint.IListItem existingItem, Hashtable valuesToSet, ClientContext clientContext, PnPBatch batch)
        {

            TermStore store = null;
            TaxonomySession taxSession = null;
            int defaultLanguage = CultureInfo.CurrentCulture.LCID;
            var item = new Dictionary<string, object>();

            // xxx: return early if hashtable is empty to save getting fields?

            var fields = list.Fields;

            Hashtable values = valuesToSet ?? new Hashtable();

            foreach (var key in values.Keys)
            {
                var field = fields.AsRequested().FirstOrDefault(f => f.InternalName == key as string || f.Title == key as string);
                if (field != null)
                {
                    switch (field.TypeAsString)
                    {
                        case "User":
                        case "UserMulti":
                            {
                                var userValueCollection = field.NewFieldValueCollection();

                                var value = values[key];
                                if (value == null) goto default;
                                if (value is string && string.IsNullOrWhiteSpace(value + "")) goto default;
                                if (value.GetType().IsArray)
                                {
                                    foreach (var arrayItem in (value as IEnumerable))
                                    {
                                        int userId;
                                        if (!int.TryParse(arrayItem.ToString(), out userId))
                                        {
                                            var user = list.PnPContext.Web.EnsureUser(arrayItem as string);
                                            userValueCollection.Values.Add(field.NewFieldUserValue(user));
                                        }
                                        else
                                        {
                                            try
                                            {
                                                var fieldUserValue = list.PnPContext.Web.GetUserById(userId);
                                                userValueCollection.Values.Add(field.NewFieldUserValue(fieldUserValue));
                                            }
                                            catch
                                            {
                                                // It is SharePoint Group
                                                list.PnPContext.Web.LoadAsync(p => p.SiteGroups).GetAwaiter().GetResult();
                                                var groupItem = list.PnPContext.Web.SiteGroups.AsRequested().Where(g => g.Id == userId).FirstOrDefault();
                                                if (groupItem != null)
                                                {
                                                    userValueCollection.Values.Add(field.NewFieldUserValue(groupItem));
                                                }
                                            }

                                        }
                                    }
                                    item[key as string] = userValueCollection;
                                }
                                else
                                {
                                    int userId;
                                    if (!int.TryParse(value as string, out userId))
                                    {
                                        var user = list.PnPContext.Web.EnsureUser(value as string);
                                        item[key as string] = field.NewFieldUserValue(user);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            var fieldUserValue = list.PnPContext.Web.GetUserById(userId);
                                            item[key as string] = field.NewFieldUserValue(fieldUserValue);
                                        }
                                        catch
                                        {
                                            // It is SharePoint Group
                                            list.PnPContext.Web.LoadAsync(p => p.SiteGroups).GetAwaiter().GetResult();
                                            var groupItem = list.PnPContext.Web.SiteGroups.AsRequested().Where(g => g.Id == userId).FirstOrDefault();
                                            if (groupItem != null)
                                            {
                                                item[key as string] = field.NewFieldUserValue(groupItem);
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case "TaxonomyFieldType":
                        case "TaxonomyFieldTypeMulti":
                            {
                                var value = values[key];
                                if (batch.TermStore == null)
                                {
                                    taxSession = clientContext.Site.GetTaxonomySession();
                                    store = taxSession.GetDefaultSiteCollectionTermStore();
                                    clientContext.Load(store, s => s.DefaultLanguage);
                                    clientContext.ExecuteQueryRetry();
                                    defaultLanguage = store.DefaultLanguage;
                                    batch.TermStore = store;
                                    batch.TaxonomySession = taxSession;
                                    batch.DefaultTermStoreLanguage = defaultLanguage;
                                }
                                else
                                {
                                    taxSession = batch.TaxonomySession;
                                    store = batch.TermStore;
                                    defaultLanguage = batch.DefaultTermStoreLanguage.Value;
                                }
                                if (value != null && value.GetType().IsArray)
                                {
                                    var fieldValueCollection = field.NewFieldValueCollection();
                                    foreach (var arrayItem in value as object[])
                                    {
                                        Term taxonomyItem;
                                        Guid termGuid;
                                        var label = string.Empty;
                                        var itemId = Guid.Empty;

                                        if (!Guid.TryParse(arrayItem?.ToString(), out termGuid))
                                        {
                                            var batchedTerm = batch.GetCachedTerm(arrayItem?.ToString());
                                            if (batchedTerm.key == null)
                                            {
                                                taxonomyItem = clientContext.Site.GetTaxonomyItemByPath(arrayItem?.ToString()) as Term;
                                                if (taxonomyItem == null)
                                                {
                                                    throw new PSInvalidOperationException($"Cannot find term '{arrayItem}'");
                                                }
                                                var labelResult = taxonomyItem.GetDefaultLabel(defaultLanguage);
                                                clientContext.ExecuteQueryRetry();
                                                label = labelResult.Value;
                                                itemId = taxonomyItem.Id;
                                                batch.CacheTerm(arrayItem?.ToString(), itemId, label);
                                                batch.CacheTerm(itemId.ToString(), itemId, label);
                                            }
                                            else
                                            {
                                                itemId = batchedTerm.id;
                                                label = batchedTerm.label;
                                            }
                                        }
                                        else
                                        {
                                            var batchedTerm = batch.GetCachedTerm(termGuid.ToString());
                                            if (batchedTerm.key == null)
                                            {
                                                taxonomyItem = taxSession.GetTerm(termGuid);
                                                if (taxonomyItem == null)
                                                {
                                                    throw new PSInvalidOperationException($"Cannot find term {arrayItem}");
                                                }
                                                var labelResult = taxonomyItem.GetDefaultLabel(defaultLanguage);
                                                clientContext.Load(taxonomyItem);
                                                clientContext.ExecuteQueryRetry();
                                                itemId = taxonomyItem.Id;
                                                label = labelResult.Value;
                                                batch.CacheTerm(termGuid.ToString(), termGuid, label);
                                            }
                                            else
                                            {
                                                itemId = batchedTerm.id;
                                                label = batchedTerm.label;
                                            }
                                        }

                                        fieldValueCollection.Values.Add(field.NewFieldTaxonomyValue(itemId, label));
                                    }

                                    item[key as string] = fieldValueCollection;
                                }
                                else
                                {
                                    Guid termGuid = Guid.Empty;

                                    Term taxonomyItem = null;
                                    var label = string.Empty;
                                    var itemId = Guid.Empty;
                                    if (value != null && !Guid.TryParse(value as string, out termGuid))
                                    {
                                        var batchedTerm = batch.GetCachedTerm(termGuid.ToString());
                                        if (batchedTerm.key == null)
                                        {
                                            // Assume it's a TermPath
                                            taxonomyItem = clientContext.Site.GetTaxonomyItemByPath(value as string) as Term;
                                            var labelResult = taxonomyItem.GetDefaultLabel(defaultLanguage);
                                            clientContext.ExecuteQueryRetry();
                                            itemId = taxonomyItem.Id;
                                            label = labelResult.Value;
                                        }
                                        else
                                        {
                                            itemId = batchedTerm.id;
                                            label = batchedTerm.label;
                                        }
                                    }
                                    else
                                    {
                                        if (value != null)
                                        {
                                            var batchedTerm = batch.GetCachedTerm(termGuid.ToString());
                                            if (batchedTerm.key == null)
                                            {
                                                taxonomyItem = taxSession.GetTerm(termGuid);
                                                var labelResult = taxonomyItem.GetDefaultLabel(defaultLanguage);
                                                clientContext.Load(taxonomyItem);
                                                clientContext.ExecuteQueryRetry();
                                                label = labelResult.Value;
                                            }
                                            else
                                            {
                                                itemId = batchedTerm.id;
                                                label = batchedTerm.label;
                                            }
                                        }
                                    }

                                    item[key as string] = field.NewFieldTaxonomyValue(taxonomyItem.Id, label);
                                }
                                break;
                            }
                        case "Lookup":
                        case "LookupMulti":
                            {
                                var value = values[key];
                                if (value == null) goto default;
                                int[] multiValue;
                                if (value is Array)
                                {
                                    var fieldValueCollection = field.NewFieldValueCollection();
                                    var arr = (object[])values[key];
                                    for (int i = 0; i < arr.Length; i++)
                                    {
                                        var arrayValue = arr[i].ToString();
                                        fieldValueCollection.Values.Add(field.NewFieldLookupValue(int.Parse(arrayValue)));
                                    }
                                    item[key as string] = fieldValueCollection;
                                }
                                else
                                {
                                    var fieldValueCollection = field.NewFieldValueCollection();
                                    string valStr = values[key].ToString();
                                    multiValue = valStr.Split(',', ';').Select(int.Parse).ToArray();
                                    if (multiValue.Length > 1)
                                    {
                                        for (int i = 0; i < multiValue.Length; i++)
                                        {
                                            fieldValueCollection.Values.Add(field.NewFieldLookupValue(multiValue[i]));
                                        }
                                        item[key as string] = fieldValueCollection;
                                    }
                                    else
                                    {
                                        item[key as string] = field.NewFieldLookupValue(multiValue[0]);
                                    }
                                }
                                break;
                            }
                        case "MultiChoice":
                            {
                                string itemValue = string.Empty;
                                var choices = values[key];

                                if (choices is string)
                                {
                                    // Handle comma or semicolon separated string
                                    itemValue = string.Join(";#", ((string)choices).Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim()));
                                }
                                else if (choices is Array)
                                {
                                    // Handle array of values (string[], object[], etc.)
                                    foreach (var choice in (Array)choices)
                                    {
                                        itemValue += choice?.ToString() + ";#";
                                    }
                                    if (!string.IsNullOrEmpty(itemValue))
                                    {
                                        itemValue = itemValue.Substring(0, itemValue.Length - 2);
                                    }
                                }
                                else if (choices is IEnumerable)
                                {
                                    // Handle other enumerable types
                                    foreach (var choice in (IEnumerable)choices)
                                    {
                                        itemValue += choice?.ToString() + ";#";
                                    }
                                    if (!string.IsNullOrEmpty(itemValue))
                                    {
                                        itemValue = itemValue.Substring(0, itemValue.Length - 2);
                                    }
                                }
                                else
                                {
                                    // Handle a single value
                                    itemValue = choices?.ToString();
                                }

                                item[key as string] = itemValue;
                                break;
                            }

                        default:
                            {
                                object itemValue = values[key] is PSObject ? ((PSObject)values[key]).BaseObject : values[key];
                                item[key as string] = itemValue;
                                break;
                            }
                    }
                }
                else
                {
                    throw new PSInvalidOperationException($"Field {key} not present in list.");
                }
            }
            if (existingItem != null && existingItem.Requested)
            {
                var specialFields = new[] { "Author", "Editor", "Created", "Modified" };
                // check if we are setting editor or author fields  
                if (item.Any(i => specialFields.Contains(i.Key)))
                {
                    foreach (var field in specialFields)
                    {
                        if (!item.ContainsKey(field) && existingItem.Values.ContainsKey(field))
                        {
                            item[field] = existingItem[field];
                        }
                    }
                }
            }
            return item;
        }

        public static void UpdateListItem(this ListItem item, ListItemUpdateType updateType)
        {
            switch (updateType)
            {
                default:
                case ListItemUpdateType.Update:
                    {
                        item.Update();
                        break;
                    }
                case ListItemUpdateType.SystemUpdate:
                    {
                        item.SystemUpdate();
                        break;
                    }
                case ListItemUpdateType.UpdateOverwriteVersion:
                    {
                        item.UpdateOverwriteVersion();
                        break;
                    }
            }
        }
    }
}
