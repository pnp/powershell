using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections;
using PnP.PowerShell.Commands.Base.Completers;

namespace PnP.PowerShell.Commands.Fields
{
    [Cmdlet(VerbsCommon.Set, "PnPView")]
    [OutputType(typeof(View))]
    public class SetView : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ViewPipeBind Identity;

        [Parameter(Mandatory = false)]
        public Hashtable Values;

        [Parameter(Mandatory = false)]
        public string[] Fields;

        [Parameter(Mandatory = false)]
        public string Aggregations;

        protected override void ExecuteCmdlet()
        {
            List list;
            View view = null;
            if (List != null)
            {
                list = List.GetList(CurrentWeb);
                if (list == null)
                {
                    throw new PSArgumentException("List provided in the List argument could not be found", "List");
                }

                if (Identity.Id != Guid.Empty)
                {
                    WriteVerbose($"Retrieving view by Id '{Identity.Id}'");
                    view = list.GetViewById(Identity.Id);
                }
                else if (!string.IsNullOrEmpty(Identity.Title))
                {
                    WriteVerbose($"Retrieving view by Title '{Identity.Title}'");
                    view = list.GetViewByName(Identity.Title);
                }
            }
            else if (Identity.View != null)
            {
                WriteVerbose("Using view passed through the pipeline");
                view = Identity.View;
            }
            else
            {
                throw new PSArgumentException("List must be provided through the List argument if not passing in a view instance", "List");
            }

            if (view == null)
            {
                throw new PSArgumentException("View provided in the Identity argument could not be found", "Identity");
            }

            if (ParameterSpecified(nameof(Values)))
            {
                bool atLeastOnePropertyChanged = false;
                foreach (string key in Values.Keys)
                {
                    var value = Values[key];

                    var property = view.GetType().GetProperty(key);
                    if (property == null)
                    {
                        WriteWarning($"No property '{key}' found on this view. Value will be ignored.");
                    }
                    else
                    {
                        try
                        {
                            property.SetValue(view, value);
                            atLeastOnePropertyChanged = true;
                        }
                        catch (Exception e)
                        {
                            WriteWarning($"Setting property '{key}' to '{value}' failed with exception '{e.Message}'. Value will be ignored.");
                        }
                    }
                }

                if (atLeastOnePropertyChanged)
                {
                    view.Update();
                    ClientContext.ExecuteQueryRetry();
                }
            }
            if(ParameterSpecified(nameof(Fields)))
            {
                view.ViewFields.RemoveAll();
                foreach(var viewField in Fields)
                {
                    view.ViewFields.Add(viewField);
                }
                view.Update();
                ClientContext.ExecuteQueryRetry();
            }
            if(ParameterSpecified(nameof(Aggregations)))
            {
                view.Aggregations = Aggregations;
                view.Update();
                ClientContext.Load(view);
                ClientContext.ExecuteQueryRetry();
            }
            WriteObject(view);
        }
    }
}
