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
            /// <summary>
            /// Internal name of the field to update. A field can be referenced by its display name through -Values, so this is
            /// resolved from the field rather than taken from the name that was provided, both because the item is keyed on the
            /// internal name and because the Author and Editor handling below needs to recognize those fields either way round.
            /// </summary>
            public string Key { get; set; }
            public object Value { get; set; }
            public string FieldTypeString { get; set; }

            /// <summary>
            /// The resolved field, carried along for the field types that are set through the field rather than through the item.
            /// </summary>
            public Field Field { get; set; }

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

            Hashtable values = valuesToSet ?? new Hashtable();

            var fields = LoadFields(context, list, values.Keys);

            foreach (var key in values.Keys)
            {
                fields.TryGetValue(key as string ?? string.Empty, out Field field);
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
                                        var userValue = UnwrapValue(arrayItem);
                                        int userId;
                                        if (!int.TryParse(userValue, out userId))
                                        {
                                            var user = web.EnsureUser(userValue);
                                            clonedContext.Load(user);
                                            clonedContext.ExecuteQueryRetry();
                                            userValues.Add(new FieldUserValue() { LookupId = user.Id });
                                        }
                                        else
                                        {
                                            userValues.Add(new FieldUserValue() { LookupId = userId });
                                        }
                                    }
                                    itemValues.Add(new FieldUpdateValue(field.InternalName, userValues.ToArray(), null));
                                }
                                else
                                {
                                    var userValue = UnwrapValue(value);
                                    int userId;
                                    if (!int.TryParse(userValue, out userId))
                                    {
                                        var user = web.EnsureUser(userValue);
                                        clonedContext.Load(user);
                                        clonedContext.ExecuteQueryRetry();
                                        itemValues.Add(new FieldUpdateValue(field.InternalName, new FieldUserValue() { LookupId = user.Id }));
                                    }
                                    else
                                    {
                                        itemValues.Add(new FieldUpdateValue(field.InternalName, new FieldUserValue() { LookupId = userId }));
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
                                            itemValues.Add(new FieldUpdateValue(field.InternalName, newTaxFieldValue, field.TypeAsString) { Field = field });
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
                                        itemValues.Add(new FieldUpdateValue(field.InternalName, taxValue, field.TypeAsString) { Field = field });
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
                                itemValues.Add(new FieldUpdateValue(field.InternalName, newVals));
                                break;
                            }
                        default:
                            {
                                itemValues.Add(new FieldUpdateValue(field.InternalName, values[key]));
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
                                var taxField = context.CastTo<TaxonomyField>(itemValue.Field);
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
                                var taxField = context.CastTo<TaxonomyField>(itemValue.Field);
                                taxField.SetFieldValueByValue(item, itemValue.Value as TaxonomyFieldValue);
                                break;
                            }
                    }
                }
            }
        }

        /// <summary>
        /// Returns the string representation of a value provided through -Values, unwrapping a PSObject where PowerShell
        /// wrapped one. A user field accepts both a login name and a user id, and the id can reach us as a number rather
        /// than as a string, so casting to string instead of converting would drop it.
        /// </summary>
        private static string UnwrapValue(object value)
        {
            var unwrapped = value is PSObject psObject ? psObject.BaseObject : value;
            return unwrapped?.ToString();
        }

        /// <summary>
        /// Retrieves only the fields that are referenced by the provided keys, instead of the entire field collection of the list,
        /// which is a sizeable response to pay for on every call on a list carrying a hundred or more columns.
        /// See https://github.com/pnp/powershell/issues/4311
        /// </summary>
        private static Dictionary<string, Field> LoadFields(ClientContext context, List list, ICollection keys)
        {
            var fields = new Dictionary<string, Field>();

            foreach (var key in keys)
            {
                if (!(key is string name) || string.IsNullOrWhiteSpace(name) || fields.ContainsKey(name)) continue;

                var field = list.Fields.GetByInternalNameOrTitle(name);
                context.Load(field, f => f.InternalName, f => f.Title, f => f.TypeAsString);
                fields.Add(name, field);
            }

            if (fields.Count == 0) return fields;

            try
            {
                context.ExecuteQueryRetry();
            }
            catch (ServerException)
            {
                // At least one of the provided keys does not refer to an existing field, which fails the request as a whole.
                // Fall back to retrieving the full field collection and matching against it, so that the field which cannot
                // be found is reported as such rather than as a server error, at the cost of one request on this path only.
                var allFields = context.LoadQuery(list.Fields.Include(f => f.InternalName, f => f.Title, f => f.TypeAsString));
                context.ExecuteQueryRetry();

                var resolvedFields = new Dictionary<string, Field>();
                foreach (var name in fields.Keys)
                {
                    var field = allFields.FirstOrDefault(f => f.InternalName == name || f.Title == name);
                    if (field != null) resolvedFields.Add(name, field);
                }
                return resolvedFields;
            }

            return fields;
        }

        private static Core.Model.SharePoint.IFieldValue GetTaxonomyFieldValue(object value, Core.Model.SharePoint.IField field , TaxonomySession taxSession, ClientContext context, int defaultLanguage, PnPBatch batch)
        {
            Term taxonomyItem;
            Guid termGuid;
            var label = string.Empty;
            var itemId = Guid.Empty;

            if (!Guid.TryParse(value.ToString(), out termGuid))
            {
                var batchedTerm = batch.GetCachedTerm(value.ToString());
                if (batchedTerm.key == null)
                {
                    // Assume it's a TermPath
                    taxonomyItem = context.Site.GetTaxonomyItemByPath(value.ToString()) as Term;
                    if (taxonomyItem == null)
                    {
                        throw new PSInvalidOperationException($"Cannot find term '{value}'");
                    }
                    var labelResult = taxonomyItem.GetDefaultLabel(defaultLanguage);
                    context.ExecuteQueryRetry();
                    itemId = taxonomyItem.Id;
                    label = labelResult.Value;
                    batch.CacheTerm(value.ToString(), itemId, label);
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
                        throw new PSInvalidOperationException($"Cannot find term {value}");
                    }
                    var labelResult = taxonomyItem.GetDefaultLabel(defaultLanguage);
                    context.Load(taxonomyItem);
                    context.ExecuteQueryRetry();
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

            return field.NewFieldTaxonomyValue(itemId, label);
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
                    // A field can be referenced by its display name, which is not what the item is keyed on, so assign to the
                    // internal name of the field that was resolved rather than to the name that was provided
                    var fieldName = field.InternalName;

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
                                        if (!int.TryParse(UnwrapValue(arrayItem), out userId))
                                        {
                                            var user = list.PnPContext.Web.EnsureUser(UnwrapValue(arrayItem));
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
                                    item[fieldName] = userValueCollection;
                                }
                                else
                                {
                                    int userId;
                                    if (!int.TryParse(UnwrapValue(value), out userId))
                                    {
                                        var user = list.PnPContext.Web.EnsureUser(UnwrapValue(value));
                                        item[fieldName] = field.NewFieldUserValue(user);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            var fieldUserValue = list.PnPContext.Web.GetUserById(userId);
                                            item[fieldName] = field.NewFieldUserValue(fieldUserValue);
                                        }
                                        catch
                                        {
                                            // It is SharePoint Group
                                            list.PnPContext.Web.LoadAsync(p => p.SiteGroups).GetAwaiter().GetResult();
                                            var groupItem = list.PnPContext.Web.SiteGroups.AsRequested().Where(g => g.Id == userId).FirstOrDefault();
                                            if (groupItem != null)
                                            {
                                                item[fieldName] = field.NewFieldUserValue(groupItem);
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
                                        fieldValueCollection.Values.Add(GetTaxonomyFieldValue(arrayItem, field, taxSession, clientContext, defaultLanguage, batch));
                                    }

                                    item[fieldName] = fieldValueCollection;
                                }
                                else
                                {
                                    if (value == null)
                                    {
                                        item[fieldName] = null;
                                    }
                                    else
                                    {
                                        item[fieldName] = GetTaxonomyFieldValue(value, field, taxSession, clientContext, defaultLanguage, batch);
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
                                    var fieldValueCollection = field.NewFieldValueCollection();
                                    var arr = (object[])values[key];
                                    for (int i = 0; i < arr.Length; i++)
                                    {
                                        var arrayValue = arr[i].ToString();
                                        fieldValueCollection.Values.Add(field.NewFieldLookupValue(int.Parse(arrayValue)));
                                    }
                                    item[fieldName] = fieldValueCollection;
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
                                        item[fieldName] = fieldValueCollection;
                                    }
                                    else
                                    {
                                        item[fieldName] = field.NewFieldLookupValue(multiValue[0]);
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

                                item[fieldName] = itemValue;
                                break;
                            }

                        default:
                            {
                                object itemValue = values[key] is PSObject ? ((PSObject)values[key]).BaseObject : values[key];
                                item[fieldName] = itemValue;
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
