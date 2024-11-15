
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Move, "PnPPageComponent")]
    [Alias("Move-PnPClientSideComponent")]
    [OutputType(typeof(void))]
    public class MovePageComponent : PnPWebCmdlet
    {
        const string ParameterSet_SECTION = "Move to other section";
        const string ParameterSet_COLUMN = "Move to other column";
        const string ParameterSet_SECTIONCOLUMN = "Move to other section and column";
        const string ParameterSet_POSITION = "Move within a column";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(PageCompleter))]
        public PagePipeBind Page;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Guid InstanceId;

        [Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = ParameterSet_SECTION)]
        [Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = ParameterSet_SECTIONCOLUMN)]
        public int Section;

        [Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = ParameterSet_COLUMN)]
        [Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = ParameterSet_SECTIONCOLUMN)]
        public int Column;

        [Parameter(Mandatory = false, ValueFromPipeline = false, ParameterSetName = ParameterSet_COLUMN)]
        [Parameter(Mandatory = false, ValueFromPipeline = false, ParameterSetName = ParameterSet_SECTION)]
        [Parameter(Mandatory = false, ValueFromPipeline = false, ParameterSetName = ParameterSet_SECTIONCOLUMN)]
        [Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = ParameterSet_POSITION)]
        public int Position;

        protected override void ExecuteCmdlet()
        {
            var clientSidePage = Page.GetPage(Connection);

            if (clientSidePage == null)
            {
                throw new Exception($"Page '{Page?.Name}' does not exist");
            }

            var control = clientSidePage.Controls.FirstOrDefault(c => c.InstanceId == InstanceId);
            if (control != null)
            {
                bool updated = false;

                switch (ParameterSetName)
                {
                    case ParameterSet_COLUMN:
                        {
                            var column = control.Section.Columns[Column - 1];
                            if (column != control.Column)
                            {
                                if (ParameterSpecified(nameof(Position)))
                                {
                                    control.MovePosition(column, Position);
                                }
                                else
                                {
                                    control.Move(column);
                                }
                                updated = true;
                            }
                            break;
                        }
                    case ParameterSet_SECTION:
                        {
                            var section = clientSidePage.Sections[Section - 1];
                            if (section != control.Section)
                            {
                                if (ParameterSpecified(nameof(Position)))
                                {
                                    control.MovePosition(section, Position);
                                }
                                else
                                {
                                    control.Move(section);
                                }
                                updated = true;
                            }
                            break;
                        }
                    case ParameterSet_SECTIONCOLUMN:
                        {
                            var section = clientSidePage.Sections[Section - 1];
                            if (section != control.Section)
                            {
                                control.Move(section);
                                updated = true;
                            }
                            var column = section.Columns[Column - 1];
                            if (column != control.Column)
                            {
                                if (ParameterSpecified(nameof(Position)))
                                {
                                    control.MovePosition(column, Position);
                                }
                                else
                                {
                                    control.Move(column);
                                }
                                updated = true;
                            }
                            break;
                        }
                    case ParameterSet_POSITION:
                        {
                            control.MovePosition(control.Column, Position);
                            updated = true;
                            break;
                        }
                }


                if (updated)
                {
                    clientSidePage.Save();
                }
            }
            else
            {
                throw new Exception($"Webpart does not exist");
            }
        }
    }
}