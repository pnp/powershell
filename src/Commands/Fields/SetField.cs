﻿using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Fields
{
    [Cmdlet(VerbsCommon.Set, "PnPField", DefaultParameterSetName = "Field")]
    [OutputType(typeof(Field))]
    public class SetField : PnPWebCmdlet, IDynamicParameters
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public ListPipeBind List;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public FieldPipeBind Identity = new FieldPipeBind();

        [Parameter(Mandatory = false)]
        public Hashtable Values;

        [Parameter(Mandatory = false)]
        public SwitchParameter UpdateExistingLists;

        object IDynamicParameters.GetDynamicParameters() => DynamicParameters;

        static readonly RuntimeDefinedParameterDictionary DynamicParameters = PropertyDynamicParameters.GetDynamicParametersForSettablePropertiesOfType<Field>();

        protected override void ExecuteCmdlet()
        {
            Field field = null;
            if (List != null)
            {
                var list = List.GetList(CurrentWeb);

                if (list == null)
                {
                    throw new ArgumentException("Unable to retrieve the list specified using the List parameter", "List");
                }

                if (Identity.Id != Guid.Empty)
                {
                    field = list.Fields.GetById(Identity.Id);
                }
                else if (!string.IsNullOrEmpty(Identity.Name))
                {
                    field = list.Fields.GetByInternalNameOrTitle(Identity.Name);
                }
                if (field == null)
                {
                    throw new ArgumentException("Unable to retrieve field with id, name or the field instance provided through Identity on the specified List", "Identity");
                }
            }
            else
            {
                if (Identity.Id != Guid.Empty)
                {
                    field = ClientContext.Web.Fields.GetById(Identity.Id);
                }
                else if (!string.IsNullOrEmpty(Identity.Name))
                {
                    field = ClientContext.Web.Fields.GetByInternalNameOrTitle(Identity.Name);
                }
                else if (Identity.Field != null)
                {
                    field = Identity.Field;
                }

                if (field == null)
                {
                    throw new ArgumentException("Unable to retrieve field with id, name or the field instance provided through Identity on the current web", "Identity");
                }
            }

            DynamicParameters.SetValuesFromParams(field, this);
            SetValuesFromHashtable(field);

            field.UpdateAndPushChanges(UpdateExistingLists);
            ClientContext.Load(field);
            ClientContext.ExecuteQueryRetry();
            WriteObject(field);
        }

        private void SetValuesFromHashtable(Field field)
        {
            const string allowDeletionPropertyKey = "AllowDeletion";
            if (Values.ContainsKey(allowDeletionPropertyKey))
            {
                ClientContext.Load(field, f => f.SchemaXmlWithResourceTokens);
            }
            else
            {
                ClientContext.Load(field);
            }
            ClientContext.ExecuteQueryRetry();
            // Get a reference to the type-specific object to allow setting type-specific properties, i.e. LookupList and LookupField for Microsoft.SharePoint.Client.FieldLookup
            var typeSpecificField = field.TypedObject;

            foreach (string key in Values.Keys)
            {
                var value = Values[key];

                var property = typeSpecificField.GetType().GetProperty(key);

                bool isAllowDeletionProperty = string.Equals(key, allowDeletionPropertyKey, StringComparison.Ordinal);

                if (property == null && !isAllowDeletionProperty)
                {
                    WriteWarning($"No property '{key}' found on this field. Value will be ignored.");
                }
                else
                {
                    try
                    {
                        if (isAllowDeletionProperty)
                        {
                            field.SetAllowDeletion(value as bool?);
                        }
                        else
                        {
                            property.SetValue(typeSpecificField, value);
                        }
                    }
                    catch (Exception e)
                    {
                        WriteWarning($"Setting property '{key}' to '{value}' failed with exception '{e.Message}'. Value will be ignored.");
                    }
                }
            }
        }
    }
}