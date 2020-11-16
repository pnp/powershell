using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
    internal class AzureApp
    {
        public string Id { get; set; }
        public string AppId { get; set; }
        public string DisplayName { get; set; }
        public string SignInAudience { get; set; }
    }

    public class PermissionScope
    {
        [JsonIgnore]
        public string resourceAppId { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; } = "Role";
        [JsonIgnore]
        public string Identifier { get; set; }
    }

    public class PermissionScopes
    {
        private const string ResourceAppId_Graph = "00000003-0000-0000-c000-000000000000";
        private const string ResourceAppID_SPO = "00000003-0000-0ff1-ce00-000000000000";
        private const string ResourceAppID_O365Management = "c5393580-f805-4401-95e8-94b7a6ef2fc2";
        private List<PermissionScope> scopes = new List<PermissionScope>();
        public PermissionScopes()
        {
            #region GRAPH
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "bf7b1a76-6e77-406b-b258-bf5c7720e98f",
                Identifier = "MSGraph.Group.Create"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "5b567255-7703-4780-807c-7be8301ae99b",
                Identifier = "MSGraph.Group.Read.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "62a82d76-70ea-41e2-9197-370581804d09",
                Identifier = "MSGraph.Group.ReadWrite.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "5ef47bde-23a3-4cfb-be03-6ab63044aec6",
                Identifier = "MSGraph.Group.Select"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "98830695-27a2-44f7-8c18-0c3ebc9698f6",
                Identifier = "MSGraph.GroupMember.Read.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "dbaae8cf-10b5-4b86-a4a1-f871c94c6695",
                Identifier = "MSGraph.GroupMember.ReadWrite.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "01d4889c-1287-42c6-ac1f-5d1e02578ef6",
                Identifier = "MSGraph.Files.Read.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "75359482-378d-4052-8f01-80520e7db3cd",
                Identifier = "MSGraph.Files.ReadWrite.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "b0afded3-3588-46d8-8b3d-9842eff778da",
                Identifier = "MSGraph.AuditLog.Read.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "798ee544-9d2d-430c-a058-570e29e34338",
                Identifier = "MSGraph.Calendars.Read"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "ef54d2bf-783f-4e0f-bca1-3210c0444d99",
                Identifier = "MSGraph.Calendars.ReadWrite"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "df021288-bdef-4463-88db-98f22de89214",
                Identifier = "MSGraph.User.Read.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "741f803b-c850-494e-b5df-cde7c675a1ca",
                Identifier = "MSGraph.User.ReadWrite.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "88e58d74-d3df-44f3-ad47-e89edf4472e4",
                Identifier = "MSGraph.AppCatalog.Read.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "1ca167d5-1655-44a1-8adf-1414072e1ef9",
                Identifier = "MSGraph.AppCatalog.ReadWrite.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "3db89e36-7fa6-4012-b281-85f3d9d9fd2e",
                Identifier = "MSGraph.AppCatalog.Submit"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "19dbc75e-c2e2-444c-a770-ec69d8559fc7",
                Identifier = "MSGraph.Directory.ReadWrite.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "0e263e50-5827-48a4-b97c-d940288653c7",
                Identifier = "MSGraph.Directory.AccessAsUser.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "0c3e411a-ce45-4cd1-8f30-f99a3efa7b11",
                Identifier = "MSGraph.ChannelMember.ReadWrite.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "ebf0f66e-9fb1-49e4-a278-222f76911cf4",
                Identifier = "MSGraph.ChannelMessage.Send"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "d649fb7c-72b4-4eec-b2b4-b15acf79e378",
                Identifier = "MSGraph.ChannelSettings.ReadWrite.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "f13ce604-1677-429f-90bd-8a10b9f01325",
                Identifier = "MSGraph.IdentityProvider.ReadWrite.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "024d486e-b451-40bb-833d-3e66d98c5c73",
                Identifier = "MSGraph.Mail.ReadWrite"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "e383f46e-2787-4529-855e-0e479a3ffac0",
                Identifier = "MSGraph.Mail.Send"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "02e97553-ed7b-43d0-ab3c-f8bace0d040c",
                Identifier = "MSGraph.Reports.Read.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "2219042f-cab5-40cc-b0d2-16b1540b4c5f",
                Identifier = "MSGraph.Tasks.ReadWrite"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "7825d5d6-6049-4ce7-bdf6-3b8d53f4bcd0",
                Identifier = "MSGraph.Team.Create"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "7825d5d6-6049-4ce7-bdf6-3b8d53f4bcd0",
                Identifier = "MSGraph.TeamMember.ReadWrite.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "d3f0af02-b22d-4778-a433-14f7e3f2e1e2",
                Identifier = "MSGraph.TeamsApp.ReadWrite.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "093f8818-d05f-49b8-95bc-9d2a73e9a43c",
                Identifier = "MSGraph.TeamsAppInstallation.ReadWriteForUser"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "39d65650-9d3e-4223-80db-a335590d027e",
                Identifier = "MSGraph.TeamSettings.ReadWrite.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "b98bfd41-87c6-45cc-b104-e2de4f0dafb9",
                Identifier = "MSGraph.TeamsTab.ReadWrite.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "63dd7cd9-b489-4adf-a28c-ac38b9a0f962",
                Identifier = "MSGraph.User.Invite.All"
            });
            #endregion
            #region SPO
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppID_SPO,
                Id = "678536fe-1083-478a-9c59-b99265e6b0d3",
                Identifier = "SPO.Sites.FullControl.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppID_SPO,
                Id = "fbcd29d2-fcca-4405-aded-518d457caae4",
                Identifier = "SPO.Sites.ReadWrite.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppID_SPO,
                Id = "9bff6588-13f2-4c48-bbf2-ddab62256b36",
                Identifier = "SPO.Sites.Manage.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppID_SPO,
                Id = "d13f72ca-a275-4b96-b789-48ebcc4da984",
                Identifier = "SPO.Sites.Read.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppID_SPO,
                Id = "2a8d57a5-4090-4a41-bf1c-3c621d2ccad3",
                Identifier = "SPO.TermStore.Read.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppID_SPO,
                Id = "c8e3537c-ec53-43b9-bed3-b2bd3617ae97",
                Identifier = "SPO.TermStore.ReadWrite.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppID_SPO,
                Id = "df021288-bdef-4463-88db-98f22de89214",
                Identifier = "SPO.User.Read.All"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppID_SPO,
                Id = "741f803b-c850-494e-b5df-cde7c675a1ca",
                Identifier = "SPO.User.ReadWrite.All"
            });
            #endregion
            #region Office 365 Management
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppID_O365Management,
                Id = "741f803b-c850-494e-b5df-cde7c675a1ca",
                Identifier = "O365.ActivityFeed.Read"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppID_O365Management,
                Id = "e2cea78f-e743-4d8f-a16a-75b629a038ae",
                Identifier = "O365.ServiceHealth.Read"
            });
            #endregion
        }

        public string[] GetIdentifiers()
        {
            return this.scopes.Select(s => s.Identifier).ToArray();
        }

        public PermissionScope GetScope(string identifier)
        {
            return this.scopes.FirstOrDefault(s => s.Identifier == identifier);
        }
    }

    public class AppResource
    {
        [JsonPropertyName("resourceAppId")]
        public string Id { get; set; }
        [JsonPropertyName("resourceAccess")]
        public List<PermissionScope> ResourceAccess { get; set; } = new List<PermissionScope>();
    }
}