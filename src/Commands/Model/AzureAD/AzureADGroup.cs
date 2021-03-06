namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class AzureADGroup
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string MailNickname { get; set; }

        public string Mail { get; set; }

        public bool? MailEnabled { get; set; }

        public bool? SecurityEnabled { get; set; }

        public string[] GroupTypes { get; set; }


        internal static AzureADGroup CreateFrom(PnP.Framework.Entities.GroupEntity groupEntity)
        {
            var o = new AzureADGroup()
            {
                Id = groupEntity.GroupId,
                DisplayName = groupEntity.DisplayName,
                Description = groupEntity.Description,
                MailNickname = groupEntity.MailNickname,
                Mail = groupEntity.Mail,
                MailEnabled = groupEntity.MailEnabled,
                SecurityEnabled = groupEntity.SecurityEnabled,
                GroupTypes = groupEntity.GroupTypes
            };
            return o;
        }

        internal PnP.Framework.Entities.GroupEntity Convert()
        {
            var entity = new PnP.Framework.Entities.GroupEntity()
            {
                GroupId = Id,
                DisplayName = DisplayName,
                Description = Description,
                MailNickname = MailNickname,
                Mail = Mail,
                MailEnabled = MailEnabled,
                SecurityEnabled = SecurityEnabled,
                GroupTypes = GroupTypes
            };
            return entity;

        }
    }
}
