using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Enums;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Add, "PnPCustomAction")]
    public class AddCustomAction : PnPWebCmdlet
    {
        private const string ParameterSet_DEFAULT = "Default";
        private const string ParameterSet_CLIENTSIDECOMPONENTID = "Client Side Component Id";
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public string Name = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public string Title = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DEFAULT)]
        public string Description = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DEFAULT)]
        public string Group = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public string Location = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public int Sequence = 0;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        public string Url = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        public string ImageUrl = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        public string CommandUIExtension = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public string RegistrationId = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        public PermissionKind[] Rights;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public UserCustomActionRegistrationType RegistrationType;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public CustomActionScope Scope = CustomActionScope.Web;
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public Guid ClientSideComponentId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public string ClientSideComponentProperties;
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public string ClientSideHostProperties;

        protected override void ExecuteCmdlet()
        {
            AddUserCustomActionOptions ca;
            if (ParameterSetName == ParameterSet_DEFAULT)
            {

                ca = new AddUserCustomActionOptions
                {
                    Name = Name,
                    ImageUrl = ImageUrl,
                    CommandUIExtension = CommandUIExtension,
                    RegistrationId = RegistrationId,
                    RegistrationType = RegistrationType,
                    Description = Description,
                    Location = Location,
                    Group = Group,
                    Sequence = Sequence,
                    Title = Title,
                    Url = Url,
                };
                if (ParameterSpecified(nameof(Rights)))
                {
                    foreach (var kind in Rights)
                    {
                        ca.Rights.Set(kind);
                    }
                }
            }
            else
            {
                ca = new AddUserCustomActionOptions()
                {
                    Name = Name,
                    Title = Title,
                    Location = Location,
                    Sequence = Sequence,
                    ClientSideComponentId = ClientSideComponentId,
                    ClientSideComponentProperties = ClientSideComponentProperties,
                    HostProperties = ClientSideHostProperties
                };

                if (ParameterSpecified(nameof(RegistrationId)))
                {
                    ca.RegistrationId = RegistrationId;
                }

                if (ParameterSpecified(nameof(RegistrationType)))
                {
                    ca.RegistrationType = RegistrationType;
                }
            }

            switch (Scope)
            {
                case CustomActionScope.Web:
                    {
                        Connection.PnPContext.Web.UserCustomActions.Add(ca);
                        break;
                    }
                case CustomActionScope.Site:
                    {
                        Connection.PnPContext.Site.UserCustomActions.Add(ca);
                        break;
                    }
                case CustomActionScope.All:
                    WriteWarning("CustomActionScope 'All' is not supported for adding CustomActions");
                    break;
            }
        }
    }
}
