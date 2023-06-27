using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Fields
{
    [Cmdlet(VerbsCommon.Get, "PnPField")]
    [OutputType(typeof(Field))]
    public class GetField : PnPWebRetrievalsCmdlet<Field>
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public ListPipeBind List;

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public FieldPipeBind Identity = new FieldPipeBind();

        [Parameter(Mandatory = false)]
        public string Group;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public SwitchParameter InSiteHierarchy;

        protected override void ExecuteCmdlet()
        {
            if (List != null)
            {
                var list = List.GetList(CurrentWeb);

                Field field = null;
                FieldCollection fieldCollection = null;
                if (list != null)
                {
                    if (Identity.Id != Guid.Empty)
                    {
                        field = list.Fields.GetById(Identity.Id);
                    }
                    else if (!string.IsNullOrEmpty(Identity.Name))
                    {
                        field = list.Fields.GetByInternalNameOrTitle(Identity.Name);
                    }
                    else
                    {
                        fieldCollection = list.Fields;
                        ClientContext.Load(fieldCollection, fc => fc.IncludeWithDefaultProperties(RetrievalExpressions));
                        ClientContext.ExecuteQueryRetry();
                    }
                }
                if (field != null)
                {
                    ClientContext.Load(field, RetrievalExpressions);
                    ClientContext.ExecuteQueryRetry();

                    field.EnsureProperty(f => f.FieldTypeKind);

                    switch (field.FieldTypeKind)
                    {
                        case FieldType.DateTime:
                            {
                                WriteObject(ClientContext.CastTo<FieldDateTime>(field));
                                break;
                            }
                        case FieldType.Choice:
                            {
                                WriteObject(ClientContext.CastTo<FieldChoice>(field));
                                break;
                            }
                        case FieldType.Calculated:
                            {
                                WriteObject(ClientContext.CastTo<FieldCalculated>(field));
                                break;
                            }
                        case FieldType.Computed:
                            {
                                WriteObject(ClientContext.CastTo<FieldComputed>(field));
                                break;
                            }
                        case FieldType.Geolocation:
                            {
                                WriteObject(ClientContext.CastTo<FieldGeolocation>(field));
                                break;
                            }
                        case FieldType.User:
                            {
                                WriteObject(ClientContext.CastTo<FieldUser>(field));
                                break;
                            }
                        case FieldType.Currency:
                            {
                                WriteObject(ClientContext.CastTo<FieldCurrency>(field));
                                break;
                            }
                        case FieldType.Guid:
                            {
                                WriteObject(ClientContext.CastTo<FieldGuid>(field));
                                break;
                            }
                        case FieldType.URL:
                            {
                                WriteObject(ClientContext.CastTo<FieldUrl>(field));
                                break;
                            }
                        case FieldType.Lookup:
                            {
                                WriteObject(ClientContext.CastTo<FieldLookup>(field));
                                break;
                            }
                        case FieldType.MultiChoice:
                            {
                                WriteObject(ClientContext.CastTo<FieldMultiChoice>(field));
                                break;
                            }
                        case FieldType.Number:
                            {
                                WriteObject(ClientContext.CastTo<FieldNumber>(field));
                                break;
                            }
                        case FieldType.Invalid:
                            {
                                if (field.TypeAsString.StartsWith("TaxonomyFieldType"))
                                {
                                    WriteObject(ClientContext.CastTo<TaxonomyField>(field));
                                    break;
                                }
                                goto default;
                            }
                        default:
                            {
                                WriteObject(field);
                                break;
                            }
                    }

                }
                else if (fieldCollection != null)
                {
                    if (!string.IsNullOrEmpty(Group))
                    {
                        WriteObject(fieldCollection.Where(f => f.Group.Equals(Group, StringComparison.InvariantCultureIgnoreCase)), true);
                    }
                    else
                    {
                        WriteObject(fieldCollection, true);
                    }
                }
                else
                {
                    WriteObject(null);
                }
            }
            else
            {
                if (Identity.Id == Guid.Empty && string.IsNullOrEmpty(Identity.Name))
                {
                    if (InSiteHierarchy.IsPresent)
                    {
                        ClientContext.Load(CurrentWeb.AvailableFields, fc => fc.IncludeWithDefaultProperties(RetrievalExpressions));
                    }
                    else
                    {
                        ClientContext.Load(CurrentWeb.Fields, fc => fc.IncludeWithDefaultProperties(RetrievalExpressions));
                    }
                    ClientContext.ExecuteQueryRetry();
                    if (!string.IsNullOrEmpty(Group))
                    {
                        if (InSiteHierarchy.IsPresent)
                        {
                            WriteObject(CurrentWeb.AvailableFields.Where(f => f.Group.Equals(Group, StringComparison.InvariantCultureIgnoreCase)).OrderBy(f => f.Title), true);
                        }
                        else
                        {
                            WriteObject(CurrentWeb.Fields.Where(f => f.Group.Equals(Group, StringComparison.InvariantCultureIgnoreCase)).OrderBy(f => f.Title), true);
                        }
                    }
                    else
                    {
                        if (InSiteHierarchy.IsPresent)
                        {
                            WriteObject(CurrentWeb.AvailableFields.OrderBy(f => f.Title), true);
                        }
                        else
                        {
                            WriteObject(CurrentWeb.Fields.OrderBy(f => f.Title), true);
                        }
                    }
                }
                else
                {
                    Field field = null;
                    if (Identity.Id != Guid.Empty)
                    {
                        if (InSiteHierarchy.IsPresent)
                        {
                            field = CurrentWeb.AvailableFields.GetById(Identity.Id);
                        }
                        else
                        {
                            field = CurrentWeb.Fields.GetById(Identity.Id);
                        }
                    }
                    else if (!string.IsNullOrEmpty(Identity.Name))
                    {
                        if (InSiteHierarchy.IsPresent)
                        {
                            field = CurrentWeb.AvailableFields.GetByInternalNameOrTitle(Identity.Name);
                        }
                        else
                        {
                            field = CurrentWeb.Fields.GetByInternalNameOrTitle(Identity.Name);
                        }
                    }
                    ClientContext.Load(field, RetrievalExpressions);
                    ClientContext.ExecuteQueryRetry();

                    field.EnsureProperty(f => f.FieldTypeKind);

                    switch (field.FieldTypeKind)
                    {
                        case FieldType.DateTime:
                            {
                                WriteObject(ClientContext.CastTo<FieldDateTime>(field));
                                break;
                            }
                        case FieldType.Choice:
                            {
                                WriteObject(ClientContext.CastTo<FieldChoice>(field));
                                break;
                            }
                        case FieldType.Calculated:
                            {
                                WriteObject(ClientContext.CastTo<FieldCalculated>(field));
                                break;
                            }
                        case FieldType.Computed:
                            {
                                WriteObject(ClientContext.CastTo<FieldComputed>(field));
                                break;
                            }
                        case FieldType.Geolocation:
                            {
                                WriteObject(ClientContext.CastTo<FieldGeolocation>(field));
                                break;

                            }
                        case FieldType.User:
                            {
                                WriteObject(ClientContext.CastTo<FieldUser>(field));
                                break;
                            }
                        case FieldType.Currency:
                            {
                                WriteObject(ClientContext.CastTo<FieldCurrency>(field));
                                break;
                            }
                        case FieldType.Guid:
                            {
                                WriteObject(ClientContext.CastTo<FieldGuid>(field));
                                break;
                            }
                        case FieldType.URL:
                            {
                                WriteObject(ClientContext.CastTo<FieldUrl>(field));
                                break;
                            }
                        case FieldType.Lookup:
                            {
                                WriteObject(ClientContext.CastTo<FieldLookup>(field));
                                break;
                            }
                        case FieldType.MultiChoice:
                            {
                                WriteObject(ClientContext.CastTo<FieldMultiChoice>(field));
                                break;
                            }
                        case FieldType.Number:
                            {
                                WriteObject(ClientContext.CastTo<FieldNumber>(field));
                                break;
                            }
                        case FieldType.Invalid:
                            {
                                if (field.TypeAsString.StartsWith("TaxonomyFieldType"))
                                {
                                    WriteObject(ClientContext.CastTo<TaxonomyField>(field));
                                    break;
                                }
                                goto default;
                            }
                        default:
                            {
                                WriteObject(field);
                                break;
                            }
                    }
                }
            }
        }
    }
}
