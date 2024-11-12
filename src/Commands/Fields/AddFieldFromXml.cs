using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Fields
{
    [Cmdlet(VerbsCommon.Add, "PnPFieldFromXml")]
    [OutputType(typeof(Field))]
    public class AddFieldFromXml : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true, Position = 0)]
        public string FieldXml;

        protected override void ExecuteCmdlet()
        {
            Field f;

            if (List != null)
            {
                List list = List.GetList(CurrentWeb);
                f = list.CreateField(FieldXml);
            }
            else
            {
                f = CurrentWeb.CreateField(FieldXml);
            }
            ClientContext.Load(f);
            ClientContext.ExecuteQueryRetry();

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
                        WriteObject(ClientContext.CastTo<FieldCalculated>(f));
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

}
