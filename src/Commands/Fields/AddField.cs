using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Entities;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using Microsoft.SharePoint.Client.Taxonomy;

namespace PnP.PowerShell.Commands.Fields
{
    [Cmdlet(VerbsCommon.Add, "PnPField", DefaultParameterSetName = "Add field to list")]
    [OutputType(typeof(Field))]
    public class AddField : PnPWebCmdlet, IDynamicParameters
    {
        const string ParameterSet_ADDFIELDTOLIST = "Add field to list";
        const string ParameterSet_ADDFIELDREFERENCETOLIST = "Add field reference to list";
        const string ParameterSet_ADDFIELDTOWEB = "Add field to web";
        const string ParameterSet_ADDFIELDBYXMLTOLIST = "Add field by XML to list";

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_ADDFIELDTOLIST)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_ADDFIELDREFERENCETOLIST)]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ADDFIELDREFERENCETOLIST)]
        public FieldPipeBind Field;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ADDFIELDTOLIST)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ADDFIELDTOWEB)]
        public string DisplayName;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ADDFIELDTOLIST)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ADDFIELDTOWEB)]
        public string InternalName;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ADDFIELDTOLIST)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ADDFIELDTOWEB)]
        public FieldType Type;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ADDFIELDTOLIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ADDFIELDTOWEB)]
        public Guid Id;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ADDFIELDTOLIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ADDFIELDBYXMLTOLIST)]
        public SwitchParameter AddToDefaultView;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ADDFIELDTOLIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ADDFIELDBYXMLTOLIST)]
        public SwitchParameter Required;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ADDFIELDTOLIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ADDFIELDBYXMLTOLIST)]
        public string Group;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ADDFIELDTOLIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ADDFIELDTOWEB)]
        public Guid ClientSideComponentId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ADDFIELDTOLIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ADDFIELDTOWEB)]
        public string ClientSideComponentProperties;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ADDFIELDTOLIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ADDFIELDBYXMLTOLIST)]
        public SwitchParameter AddToAllContentTypes;

        public object GetDynamicParameters()
        {
            if (Type == FieldType.Choice || Type == FieldType.MultiChoice)
            {
                choiceFieldParameters = new ChoiceFieldDynamicParameters();
                return choiceFieldParameters;
            }
            if (Type == FieldType.Calculated)
            {
                calculatedFieldParameters = new CalculatedFieldDynamicParameters();
                return calculatedFieldParameters;
            }
            return null;
        }

        private ChoiceFieldDynamicParameters choiceFieldParameters;
        private CalculatedFieldDynamicParameters calculatedFieldParameters;

        protected override void ExecuteCmdlet()
        {

            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }

            if (List != null)
            {
                var list = List.GetList(CurrentWeb);
                if (list == null)
                    throw new PSArgumentException($"No list found with id, title or url '{List}'", "List");
                Field f;
                if (ParameterSetName != ParameterSet_ADDFIELDREFERENCETOLIST)
                {
                    var fieldCI = new FieldCreationInformation(Type)
                    {
                        Id = Id,
                        InternalName = InternalName,
                        DisplayName = DisplayName,
                        Group = Group,
                        AddToDefaultView = AddToDefaultView
                    };

                    if (AddToAllContentTypes)
                    {
                        fieldCI.FieldOptions |= AddFieldOptions.AddToAllContentTypes;
                    }

                    if (ClientSideComponentId != Guid.Empty)
                    {
                        fieldCI.ClientSideComponentId = ClientSideComponentId;
                    }
                    if (!string.IsNullOrEmpty(ClientSideComponentProperties))
                    {
                        fieldCI.ClientSideComponentProperties = ClientSideComponentProperties;
                    }
                    if (Type == FieldType.Choice || Type == FieldType.MultiChoice)
                    {
                        EnsureDynamicParameters(choiceFieldParameters);
                        f = list.CreateField<FieldChoice>(fieldCI);
                        ((FieldChoice)f).Choices = choiceFieldParameters.Choices;
                        f.Update();
                        ClientContext.ExecuteQueryRetry();
                    }
                    else if (Type == FieldType.Calculated)
                    {
                        EnsureDynamicParameters(calculatedFieldParameters);

                        // Either set the ResultType as input parameter or set it to the default Text
                        if (!string.IsNullOrEmpty(calculatedFieldParameters.ResultType))
                        {
                            fieldCI.AdditionalAttributes = new List<KeyValuePair<string, string>>()
                            {
                                new KeyValuePair<string, string>("ResultType", calculatedFieldParameters.ResultType)
                            };
                        }
                        else
                        {
                            fieldCI.AdditionalAttributes = new List<KeyValuePair<string, string>>()
                            {
                                new KeyValuePair<string, string>("ResultType", "Text")
                            };
                        }

                        fieldCI.AdditionalChildNodes = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("Formula", calculatedFieldParameters.Formula)
                        };

                        f = list.CreateField<FieldCalculated>(fieldCI);
                    }
                    else
                    {
                        f = list.CreateField(fieldCI);

                    }
                    if (Required)
                    {
                        f.Required = true;
                        f.Update();
                        ClientContext.Load(f);
                        ClientContext.ExecuteQueryRetry();
                    }
                    WriteObject(f);
                }
                else
                {
                    Field field = Field.Field;
                    if (field == null)
                    {
                        if (Field.Id != Guid.Empty)
                        {
                            field = CurrentWeb.Fields.GetById(Field.Id);
                            ClientContext.Load(field);
                            ClientContext.ExecuteQueryRetry();
                        }
                        else if (!string.IsNullOrEmpty(Field.Name))
                        {
                            try
                            {
                                field = CurrentWeb.Fields.GetByInternalNameOrTitle(Field.Name);
                                ClientContext.Load(field);
                                ClientContext.ExecuteQueryRetry();
                            }
                            catch
                            {
                                // Field might be sitecolumn, swallow exception
                            }
                            if (field != null)
                            {
                                var rootWeb = ClientContext.Site.RootWeb;
                                field = rootWeb.Fields.GetByInternalNameOrTitle(Field.Name);
                                ClientContext.Load(field);
                                ClientContext.ExecuteQueryRetry();
                            }
                        }
                    }
                    if (field != null)
                    {
                        list.Fields.Add(field);
                        list.Update();
                        ClientContext.ExecuteQueryRetry();
                    }
                    WriteObject(field);
                }
            }
            else
            {
                Field f;

                var fieldCI = new FieldCreationInformation(Type)
                {
                    Id = Id,
                    InternalName = InternalName,
                    DisplayName = DisplayName,
                    Group = Group,
                    AddToDefaultView = AddToDefaultView
                };

                if (ClientSideComponentId != Guid.Empty)
                {
                    fieldCI.ClientSideComponentId = ClientSideComponentId;
                }
                if (!string.IsNullOrEmpty(ClientSideComponentProperties))
                {
                    fieldCI.ClientSideComponentProperties = ClientSideComponentProperties;
                }

                if (Type == FieldType.Choice || Type == FieldType.MultiChoice)
                {
                    EnsureDynamicParameters(choiceFieldParameters);
                    f = CurrentWeb.CreateField<FieldChoice>(fieldCI);
                    ((FieldChoice)f).Choices = choiceFieldParameters.Choices;
                    f.Update();
                    ClientContext.ExecuteQueryRetry();
                }
                else if (Type == FieldType.Calculated)
                {
                    EnsureDynamicParameters(calculatedFieldParameters);
                    f = CurrentWeb.CreateField<FieldCalculated>(fieldCI);
                    ((FieldCalculated)f).Formula = calculatedFieldParameters.Formula;
                    f.Update();
                    ClientContext.ExecuteQueryRetry();
                }
                else
                {
                    f = CurrentWeb.CreateField(fieldCI);
                }

                if (Required)
                {
                    f.Required = true;
                    f.Update();
                    ClientContext.Load(f);
                    ClientContext.ExecuteQueryRetry();
                }
                switch (f.FieldTypeKind)
                {
                    case FieldType.DateTime:
                        {
                            WriteObject(ClientContext.CastTo<FieldDateTime>(f));
                            break;
                        }
                    case FieldType.Choice:
                        {
                            WriteObject(ClientContext.CastTo<FieldChoice>(f));
                            break;
                        }
                    case FieldType.Calculated:
                        {
                            var calculatedField = ClientContext.CastTo<FieldCalculated>(f);
                            calculatedField.EnsureProperty(fc => fc.Formula);
                            WriteObject(calculatedField);
                            break;
                        }
                    case FieldType.Computed:
                        {
                            WriteObject(ClientContext.CastTo<FieldComputed>(f));
                            break;
                        }
                    case FieldType.Geolocation:
                        {
                            WriteObject(ClientContext.CastTo<FieldGeolocation>(f));
                            break;

                        }
                    case FieldType.User:
                        {
                            WriteObject(ClientContext.CastTo<FieldUser>(f));
                            break;
                        }
                    case FieldType.Currency:
                        {
                            WriteObject(ClientContext.CastTo<FieldCurrency>(f));
                            break;
                        }
                    case FieldType.Guid:
                        {
                            WriteObject(ClientContext.CastTo<FieldGuid>(f));
                            break;
                        }
                    case FieldType.URL:
                        {
                            WriteObject(ClientContext.CastTo<FieldUrl>(f));
                            break;
                        }
                    case FieldType.Lookup:
                        {
                            WriteObject(ClientContext.CastTo<FieldLookup>(f));
                            break;
                        }
                    case FieldType.MultiChoice:
                        {
                            WriteObject(ClientContext.CastTo<FieldMultiChoice>(f));
                            break;
                        }
                    case FieldType.Number:
                        {
                            WriteObject(ClientContext.CastTo<FieldNumber>(f));
                            break;
                        }
                    case FieldType.Invalid:
                        {
                            if (f.TypeAsString.StartsWith("TaxonomyFieldType"))
                            {
                                WriteObject(ClientContext.CastTo<TaxonomyField>(f));
                                break;
                            }
                            goto default;
                        }
                    default:
                        {
                            WriteObject(f);
                            break;
                        }
                }
            }
        }

        private void EnsureDynamicParameters(object dynamicParameters)
        {
            if (dynamicParameters == null)
            {
                throw new PSArgumentException($"Please specify the parameter -{nameof(Type)} when invoking this cmdlet", nameof(Type));
            }
        }

        public class ChoiceFieldDynamicParameters
        {
            [Parameter(Mandatory = false)]
            public string[] Choices
            {
                get { return _choices; }
                set { _choices = value; }
            }
            private string[] _choices;
        }

        public class CalculatedFieldDynamicParameters
        {
            [Parameter(Mandatory = true)]
            public string Formula;

            [Parameter(Mandatory = false)]
            public string ResultType;
        }
    }
}
