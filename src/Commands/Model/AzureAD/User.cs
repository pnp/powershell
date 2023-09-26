using System;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
    /// <summary>
    /// User in Entra ID
    /// </summary>
    public class User
    {
        /// <summary>
        /// The User Principal Name of the user
        /// </summary>
        public string UserPrincipalName { get; set; }

        /// <summary>
        /// The friendly display name of the user
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The GUID of the user object
        /// </summary>
        public System.Guid? Id { get; set; }

        /// <summary>
        /// Business phone numbers for the user
        /// </summary>
        public IEnumerable<string> BusinessPhones { get; set; }

        /// <summary>
        /// Given name of the user
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// Job title of the user
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// Primary e-mail address of the user
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// Mobile phone number of the user
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// Office location of the user
        /// </summary>
        public string OfficeLocation { get; set; }

        /// <summary>
        /// Preferred language of the user
        /// </summary>
        public string PreferredLanguage { get; set; }

        /// <summary>
        /// Surname of the user
        /// </summary>
        public string Surname { get; set; }

         /// <summary>
        /// Indicates if the account is currently enabled
        /// </summary>
        public bool? AccountEnabled { get; set; }

        /// <summary>
        /// Additional properties requested regarding the user and included in the response
        /// </summary>
        public IDictionary<string, object> AdditionalProperties { get; set; }       

        /// <summary>
        /// Converts a PnP Framework User to a PnP PowerShell Entra ID User
        /// </summary>
        /// <param name="entity">PnP Framework user object</param>
        /// <returns>PnP PowerShell Entra ID User object</returns>
        internal static User CreateFrom(PnP.Framework.Graph.Model.User entity)
        {
            if(entity == null) return null;
            
            var user = new User
            {
                UserPrincipalName = entity.UserPrincipalName,
                DisplayName = entity.DisplayName,
                Id = entity.Id,
                BusinessPhones = entity.BusinessPhones,
                GivenName = entity.GivenName,
                JobTitle = entity.JobTitle,
                Mail = entity.Mail,
                MobilePhone = entity.MobilePhone,
                OfficeLocation = entity.OfficeLocation,
                PreferredLanguage = entity.PreferredLanguage,
                Surname = entity.Surname,
                AccountEnabled = entity.AccountEnabled,
                AdditionalProperties = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase)
            };
            
            if (entity.AdditionalProperties != null)
            {
                // Copy over the AdditionalProperties. We have to do it like this instead of directly assigning the Dictionary so we can instruct the dictionary to ignore casing in the dictionary constructor.
                foreach (var additionalProperty in entity.AdditionalProperties)
                {
                    user.AdditionalProperties.Add(additionalProperty.Key, additionalProperty.Value);
                }
            }
            
            return user;
        }
    }
}