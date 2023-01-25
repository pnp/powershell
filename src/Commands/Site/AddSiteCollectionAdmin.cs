using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Add, "PnPSiteCollectionAdmin")]
    [OutputType(typeof(void))]
    public class AddSiteCollectionAdmin : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public List<UserPipeBind> Owners;

        [Parameter(Mandatory = false)]
        public SwitchParameter MakePrimarySiteCollectionAdmin;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(MakePrimarySiteCollectionAdmin)) && MakePrimarySiteCollectionAdmin.ToBool())
            {
                if (Owners?.Count > 1)
                {
                    throw new PSArgumentException("Only one owner can be set as primary site collection admin", nameof(Owners));
                }
                else
                {
                    AddAsPrimarySiteCollectionAdmin();
                }
            }
            else
            {
                AddAsSecondarySiteCollectionAdmin();
            }
        }

        /// <summary>
        /// Adds the first owner as a primary site collection admin
        /// </summary>
        private void AddAsPrimarySiteCollectionAdmin()
        {
            var owner = Owners[0];

            WriteVerbose("Retrieving details of user so it can set as the primary site collection admin");
            User user = owner.GetUser(ClientContext, true);

            if (user != null)
            {
                WriteVerbose("User has been found, setting it as the primary site collection admin");

                try
                {
                    ClientContext.Site.Owner = user;
                    ClientContext.ExecuteQueryRetry();

                    WriteVerbose("User has been set as the primary site collection admin");
                }
                catch (ServerException e)
                {
                    WriteWarning($"Exception occurred while trying to set the user as the primary site collection admin: \"{e.Message}\"");
                }
            }
            else
            {
                WriteWarning("Unable to set user as the primary site collection admin as it wasn't found");
            }
        }

        /// <summary>
        /// Adds all the owners as secondary site collection admins
        /// </summary>
        private void AddAsSecondarySiteCollectionAdmin()
        {
            WriteVerbose($"Adding {Owners.Count} users as secondary site collection admins");

            foreach (var owner in Owners)
            {
                WriteVerbose("Retrieving details of user so it can be added as a secondary site collection admin");
                User user = owner.GetUser(ClientContext, true);

                if (user != null)
                {
                    WriteVerbose("User has been found, adding it as a secondary site collection admin");

                    user.IsSiteAdmin = true;
                    user.Update();

                    try
                    {
                        ClientContext.ExecuteQueryRetry();

                        WriteVerbose("User has been added as a secondary site collection admin");
                    }
                    catch (ServerException e)
                    {
                        WriteWarning($"Exception occurred while trying to add the user as a secondary site collection admin: \"{e.Message}\". User will be skipped.");
                    }
                }
                else
                {
                    WriteWarning("Unable to add as a secondary site collectin admin as it wasn't found. User will be skipped.");
                }
            }
        }
    }
}