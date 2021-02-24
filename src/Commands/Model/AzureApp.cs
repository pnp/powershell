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
        public static string ResourceAppId_Graph = "00000003-0000-0000-c000-000000000000";
        public static string ResourceAppId_SPO = "00000003-0000-0ff1-ce00-000000000000";
        public static string ResourceAppID_O365Management = "c5393580-f805-4401-95e8-94b7a6ef2fc2";
        private List<PermissionScope> scopes = new List<PermissionScope>();
        public PermissionScopes()
        {
            #region GRAPH
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,

                Id = "bf7b1a76-6e77-406b-b258-bf5c7720e98f",
                Identifier = "Group.Create"
            });

            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "5b567255-7703-4780-807c-7be8301ae99b",
                Identifier = "Group.Read.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "5f8c59db-677d-491f-a6b8-5f174b11ec1d",
                Identifier = "Group.Read.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "62a82d76-70ea-41e2-9197-370581804d09",
                Identifier = "Group.ReadWrite.All"
            });
            scopes.Add(new PermissionScope  // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "4e46008b-f24c-477d-8fff-7bb4ec7aafe0",
                Type = "Scope",
                Identifier = "Group.ReadWrite.All"
            });

            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "5ef47bde-23a3-4cfb-be03-6ab63044aec6",
                Identifier = "Group.Select"
            });

            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "98830695-27a2-44f7-8c18-0c3ebc9698f6",
                Identifier = "GroupMember.Read.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "bc024368-1153-4739-b217-4326f2e966d0",
                Identifier = "GroupMember.Read.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "dbaae8cf-10b5-4b86-a4a1-f871c94c6695",
                Identifier = "GroupMember.ReadWrite.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "f81125ac-d3b7-4573-a3b2-7099cc39df9e",
                Identifier = "GroupMember.ReadWrite.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "01d4889c-1287-42c6-ac1f-5d1e02578ef6",
                Identifier = "Files.Read.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "df85f4d6-205c-4ac5-a5ea-6bf408dba283",
                Identifier = "Files.Read.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "75359482-378d-4052-8f01-80520e7db3cd",
                Identifier = "Files.ReadWrite.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "863451e7-0667-486c-a5d6-d135439485f0",
                Identifier = "Files.ReadWrite.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "b0afded3-3588-46d8-8b3d-9842eff778da",
                Identifier = "AuditLog.Read.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "e4c9e354-4dc5-45b8-9e7c-e1393b0b1a20",
                Identifier = "AuditLog.Read.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "798ee544-9d2d-430c-a058-570e29e34338",
                Identifier = "Calendars.Read"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "465a38f9-76ea-45b9-9f34-9e8b0d4b0b42",
                Identifier = "Calendars.Read",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "ef54d2bf-783f-4e0f-bca1-3210c0444d99",
                Identifier = "Calendars.ReadWrite"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "1ec239c2-d7c9-4623-a91a-a9775856bb36",
                Identifier = "Calendars.ReadWrite",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "df021288-bdef-4463-88db-98f22de89214",
                Identifier = "User.Read.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "a154be20-db9c-4678-8ab7-66f6cc099a59",
                Identifier = "User.Read.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "741f803b-c850-494e-b5df-cde7c675a1ca",
                Identifier = "User.ReadWrite.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "204e0828-b5ca-4ad8-b9f3-f32a958e7cc4",
                Identifier = "User.ReadWrite.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "88e58d74-d3df-44f3-ad47-e89edf4472e4",
                Identifier = "AppCatalog.Read.All",
                Type = "Scope"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "1ca167d5-1655-44a1-8adf-1414072e1ef9",
                Identifier = "AppCatalog.ReadWrite.All",
                Type = "Scope"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "3db89e36-7fa6-4012-b281-85f3d9d9fd2e",
                Identifier = "AppCatalog.Submit",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "19dbc75e-c2e2-444c-a770-ec69d8559fc7",
                Identifier = "Directory.ReadWrite.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "c5366453-9fb0-48a5-a156-24f0c49a4b84",
                Identifier = "Directory.ReadWrite.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "0e263e50-5827-48a4-b97c-d940288653c7",
                Identifier = "Directory.AccessAsUser.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "3b55498e-47ec-484f-8136-9013221c06a9",
                Identifier = "ChannelMember.Read.All",
                Type = "Scope"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "2eadaff8-0bce-4198-a6b9-2cfc35a30075",
                Identifier = "ChannelMember.Read.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "0c3e411a-ce45-4cd1-8f30-f99a3efa7b11",
                Identifier = "ChannelMember.ReadWrite.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "0c3e411a-ce45-4cd1-8f30-f99a3efa7b11",
                Identifier = "ChannelMember.ReadWrite.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "ebf0f66e-9fb1-49e4-a278-222f76911cf4",
                Identifier = "ChannelMessage.Send",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "243cded2-bd16-4fd6-a953-ff8177894c3d",
                Identifier = "ChannelSettings.ReadWrite.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "d649fb7c-72b4-4eec-b2b4-b15acf79e378",
                Identifier = "ChannelSettings.ReadWrite.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // appplication
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "90db2b9a-d928-4d33-a4dd-8442ae3d41e4",
                Identifier = "IdentityProvider.ReadWrite.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "f13ce604-1677-429f-90bd-8a10b9f01325",
                Identifier = "IdentityProvider.ReadWrite.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "e2a3a72e-5f79-4c64-b1b1-878b674786c9",
                Identifier = "Mail.ReadWrite"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "024d486e-b451-40bb-833d-3e66d98c5c73",
                Identifier = "Mail.ReadWrite",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "b633e1c5-b582-4048-a93e-9f11b44c7e96",
                Identifier = "Mail.Send"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "e383f46e-2787-4529-855e-0e479a3ffac0",
                Identifier = "Mail.Send",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "230c1aed-a721-4c5d-9cb4-a90514e508ef",
                Identifier = "Reports.Read.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "02e97553-ed7b-43d0-ab3c-f8bace0d040c",
                Identifier = "Reports.Read.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "2219042f-cab5-40cc-b0d2-16b1540b4c5f",
                Identifier = "Tasks.ReadWrite",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "23fc2474-f741-46ce-8465-674744c5c361",
                Identifier = "Team.Create"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "7825d5d6-6049-4ce7-bdf6-3b8d53f4bcd0",
                Identifier = "Team.Create",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "0121dc95-1b9f-4aed-8bac-58c5ac466691",
                Identifier = "TeamMember.ReadWrite.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "7825d5d6-6049-4ce7-bdf6-3b8d53f4bcd0",
                Identifier = "TeamMember.ReadWrite.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "eb6b3d76-ed75-4be6-ac36-158d04c0a555",
                Identifier = "TeamsApp.ReadWrite.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "d3f0af02-b22d-4778-a433-14f7e3f2e1e2",
                Identifier = "TeamsApp.ReadWrite.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "74ef0291-ca83-4d02-8c7e-d2391e6a444f",
                Identifier = "TeamsAppInstallation.ReadWriteForUser.All",
            });

            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "093f8818-d05f-49b8-95bc-9d2a73e9a43c",
                Identifier = "TeamsAppInstallation.ReadWriteForUser",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "bdd80a03-d9bc-451d-b7c4-ce7c63fe3c8f",
                Identifier = "TeamSettings.ReadWrite.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "39d65650-9d3e-4223-80db-a335590d027e",
                Identifier = "TeamSettings.ReadWrite.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "a96d855f-016b-47d7-b51c-1218a98d791c",
                Identifier = "TeamsTab.ReadWrite.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "b98bfd41-87c6-45cc-b104-e2de4f0dafb9",
                Identifier = "TeamsTab.ReadWrite.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "09850681-111b-4a89-9bed-3f2cae46d706",
                Identifier = "User.Invite.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "63dd7cd9-b489-4adf-a28c-ac38b9a0f962",
                Identifier = "User.Invite.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "883ea226-0bf2-4a8f-9f9d-92c9162a727d",
                Identifier = "Sites.Selected"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "5a54b8b3-347c-476d-8f8e-42d5c7424d29",
                Identifier = "Sites.FullControl.All",
                Type = "Scope"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_Graph,
                Id = "a82116e5-55eb-4c41-a434-62fe8a61c773",
                Identifier = "Sites.FullControl.All"
            });

            #endregion
            #region SPO
            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_SPO,
                Id = "678536fe-1083-478a-9c59-b99265e6b0d3",
                Identifier = "Sites.FullControl.All"
            });
            scopes.Add(new PermissionScope() // delegate
            {
                resourceAppId = ResourceAppId_SPO,
                Id = "56680e0d-d2a3-4ae1-80d8-3c4f2100e3d0",
                Identifier = "AllSites.FullControl",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope() // application
            {
                resourceAppId = ResourceAppId_SPO,
                Id = "741f803b-c850-494e-b5df-cde7c675a1ca",
                Identifier = "User.ReadWrite.All"
            });
            scopes.Add(new PermissionScope() // delegate
            {
                resourceAppId = ResourceAppId_SPO,
                Id = "82866913-39a9-4be7-8091-f4fa781088ae",
                Identifier = "User.ReadWrite.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_SPO,
                Id = "fbcd29d2-fcca-4405-aded-518d457caae4",
                Identifier = "Sites.ReadWrite.All"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_SPO,
                Id = "9bff6588-13f2-4c48-bbf2-ddab62256b36",
                Identifier = "Sites.Manage.All"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_SPO,
                Id = "d13f72ca-a275-4b96-b789-48ebcc4da984",
                Identifier = "Sites.Read.All"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_SPO,
                Id = "2a8d57a5-4090-4a41-bf1c-3c621d2ccad3",
                Identifier = "TermStore.Read.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_SPO,
                Id = "a468ea40-458c-4cc2-80c4-51781af71e41",
                Identifier = "TermStore.Read.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_SPO,
                Id = "c8e3537c-ec53-43b9-bed3-b2bd3617ae97",
                Identifier = "TermStore.ReadWrite.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_SPO,
                Id = "59a198b5-0420-45a8-ae59-6da1cb640505",
                Identifier = "TermStore.ReadWrite.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_SPO,
                Id = "df021288-bdef-4463-88db-98f22de89214",
                Identifier = "User.Read.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_SPO,
                Id = "0cea5a30-f6f8-42b5-87a0-84cc26822e02",
                Identifier = "User.Read.All",
                Type = "Scope"
            });

            scopes.Add(new PermissionScope // application
            {
                resourceAppId = ResourceAppId_SPO,
                Id = "741f803b-c850-494e-b5df-cde7c675a1ca",
                Identifier = "User.ReadWrite.All"
            });
            scopes.Add(new PermissionScope // delegate
            {
                resourceAppId = ResourceAppId_SPO,
                Id = "82866913-39a9-4be7-8091-f4fa781088ae",
                Identifier = "User.ReadWrite.All",
                Type = "Scope"
            });
            #endregion
            #region Office 365 Management
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppID_O365Management,
                Id = "741f803b-c850-494e-b5df-cde7c675a1ca",
                Identifier = "ActivityFeed.Read"
            });
            scopes.Add(new PermissionScope()
            {
                resourceAppId = ResourceAppID_O365Management,
                Id = "e2cea78f-e743-4d8f-a16a-75b629a038ae",
                Identifier = "ServiceHealth.Read"
            });
            #endregion
        }

        public string[] GetIdentifiers()
        {

            return this.scopes.Select(s => (s.resourceAppId == ResourceAppId_SPO ? "SPO." : s.resourceAppId == ResourceAppId_Graph ? "MSGraph." : "O365.") + s.Identifier).ToArray();
        }

        public string[] GetIdentifiers(string resourceAppId, string type)
        {
            return this.scopes.Where(s => s.resourceAppId == resourceAppId && s.Type == type).Select(s => s.Identifier).ToArray();
        }

        public PermissionScope GetScope(string identifier)
        {
            return this.scopes.FirstOrDefault(s => s.Identifier == identifier);
        }

        public PermissionScope GetScope(string resourceAppId, string identifier, string type)
        {
            return this.scopes.FirstOrDefault(s => s.resourceAppId == resourceAppId && s.Identifier == identifier && s.Type == type);
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