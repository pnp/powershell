using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment.Solution
{
    public class PowerPlatformSolutionPublisher
    {
        /// <summary>
        /// Etag for the publisher
        /// </summary>
        [JsonPropertyName("@odata.etag")]
        public string ODataEtag { get; set; }

        /// <summary>
        /// Address line 1 (null in this case)
        /// </summary>
        [JsonPropertyName("address2_line1")]
        public string Address2Line1 { get; set; }

        /// <summary>
        /// Pinpoint? publisher default locale (null in this case)
        /// </summary>
        [JsonPropertyName("pinpoint?publisherdefaultlocale")]
        public string PinpointPublisherDefaultLocale { get; set; }

        /// <summary>
        /// County (null in this case)
        /// </summary>
        [JsonPropertyName("address1_county")]
        public string Address1County { get; set; }

        /// <summary>
        /// Address 2 UTC offset (null in this case)
        /// </summary>
        [JsonPropertyName("address2_utcoffset")]
        public string Address2UtcOffset { get; set; }

        /// <summary>
        /// Fax number (null in this case)
        /// </summary>
        [JsonPropertyName("address2_fax")]
        public string Address2Fax { get; set; }

        /// <summary>
        /// Date and time when modified
        /// </summary>
        [JsonPropertyName("modifiedon")]
        public string ModifiedOn { get; set; }

        /// <summary>
        /// Entity image URL (null in this case)
        /// </summary>
        [JsonPropertyName("entityimage_url")]
        public string EntityImageUrl { get; set; }

        /// <summary>
        /// Name (null in this case)
        /// </summary>
        [JsonPropertyName("address1_name")]
        public string Address1Name { get; set; }

        /// <summary>
        /// Address line 1 (null in this case)
        /// </summary>
        [JsonPropertyName("address1_line1")]
        public string Address1Line1 { get; set; }

        /// <summary>
        /// Unique name
        /// </summary>
        [JsonPropertyName("uniquename")]
        public string UniqueName { get; set; }

        /// <summary>
        /// Postal code (null in this case)
        /// </summary>
        [JsonPropertyName("address1_postalcode")]
        public string Address1PostalCode { get; set; }

        /// <summary>
        /// Address 2 line 3 (null in this case)
        /// </summary>
        [JsonPropertyName("address2_line3")]
        public string Address2Line3 { get; set; }

        /// <summary>
        /// Address 1 address ID
        /// </summary>
        [JsonPropertyName("address1_addressid")]
        public string Address1AddressId { get; set; }

        /// <summary>
        /// Publisher ID
        /// </summary>
        [JsonPropertyName("publisherid")]
        public string PublisherId { get; set; }

        /// <summary>
        /// Address 1 line 3 (null in this case)
        /// </summary>
        [JsonPropertyName("address1_line3")]
        public string Address1Line3 { get; set; }

        /// <summary>
        /// Address 2 name (null in this case)
        /// </summary>
        [JsonPropertyName("address2_name")]
        public string Address2Name { get; set; }

        /// <summary>
        /// Address 2 city (null in this case)
        /// </summary>
        [JsonPropertyName("address2_city")]
        public string Address2City { get; set; }

        /// <summary>
        /// Address 1 UTC offset (null in this case)
        /// </summary>
        [JsonPropertyName("address1_utcoffset")]
        public string Address1UtcOffset { get; set; }

        /// <summary>
        /// Pinpoint? publisher ID (null in this case)
        /// </summary>
        [JsonPropertyName("pinpoint?publisherid")]
        public string PinpointPublisherId { get; set; }

        /// <summary>
        /// Address 2 county (null in this case)
        /// </summary>
        [JsonPropertyName("address2_county")]
        public string Address2County { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        [JsonPropertyName("emailaddress")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Address 2 post office box (null in this case)
        /// </summary>
        [JsonPropertyName("address2_postofficebox")]
        public string Address2PostOfficeBox { get; set; }

        /// <summary>
        /// State or province (null in this case)
        /// </summary>
        [JsonPropertyName("address1_stateorprovince")]
        public string Address1StateOrProvince { get; set; }

        /// <summary>
        /// Address 2 telephone 3 (null in this case)
        /// </summary>
        [JsonPropertyName("address2_telephone3")]
        public string Address2Telephone3 { get; set; }

        /// <summary>
        /// Address 2 telephone 2 (null in this case)
        /// </summary>
        [JsonPropertyName("address2_telephone2")]
        public string Address2Telephone2 { get; set; }

        /// <summary>
        /// Address 2 telephone 1 (null in this case)
        /// </summary>
        [JsonPropertyName("address2_telephone1")]
        public string Address2Telephone1 { get; set; }

        /// <summary>
        /// Address 2 shipping method code
        /// </summary>
        [JsonPropertyName("address2_shippingmethodcode")]
        public int? Address2ShippingMethodCode { get; set; }

        /// <summary>
        /// Modified on behalf by value (null in this case)
        /// </summary>
        [JsonPropertyName("_modifiedonbehalfby_value")]
        public string ModifiedOnBehalfByValue { get; set; }

        /// <summary>
        /// Indicates if it is read-only
        /// </summary>
        [JsonPropertyName("isreadonly")]
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Address 2 state or province (null in this case)
        /// </summary>
        [JsonPropertyName("address2_stateorprovince")]
        public string Address2StateOrProvince { get; set; }

        /// <summary>
        /// Entity image timestamp (null in this case)
        /// </summary>
        [JsonPropertyName("entityimage_timestamp")]
        public string EntityImageTimestamp { get; set; }

        /// <summary>
        /// Address 1 latitude (null in this case)
        /// </summary>
        [JsonPropertyName("address1_latitude")]
        public string Address1Latitude { get; set; }

        /// <summary>
        /// Customization option value prefix
        /// </summary>
        [JsonPropertyName("customizationoptionvalueprefix")]
        public int? CustomizationOptionValuePrefix { get; set; }

        /// <summary>
        /// Address 2 latitude (null in this case)
        /// </summary>
        [JsonPropertyName("address2_latitude")]
        public string Address2Latitude { get; set; }

        /// <summary>
        /// Address 1 longitude (null in this case)
        /// </summary>
        [JsonPropertyName("address1_longitude")]
        public string Address1Longitude { get; set; }

        /// <summary>
        /// Address 1 line 2 (null in this case)
        /// </summary>
        [JsonPropertyName("address1_line2")]
        public string Address1Line2 { get; set; }

        /// <summary>
        /// Friendly name
        /// </summary>
        [JsonPropertyName("friendlyname")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Supporting website URL
        /// </summary>
        [JsonPropertyName("supportingwebsiteurl")]
        public string SupportingWebsiteUrl { get; set; }

        /// <summary>
        /// Address 2 line 2 (null in this case)
        /// </summary>
        [JsonPropertyName("address2_line2")]
        public string Address2Line2 { get; set; }

        /// <summary>
        /// Address 2 postal code (null in this case)
        /// </summary>
        [JsonPropertyName("address2_postalcode")]
        public string Address2PostalCode { get; set; }

        /// <summary>
        /// Organization ID value
        /// </summary>
        [JsonPropertyName("_organizationid_value")]
        public string OrganizationIdValue { get; set; }

        /// <summary>
        /// Version number
        /// </summary>
        [JsonPropertyName("versionnumber")]
        public int? VersionNumber { get; set; }

        /// <summary>
        /// Address 2 UPS zone (null in this case)
        /// </summary>
        [JsonPropertyName("address2_upszone")]
        public string Address2UpsZone { get; set; }

        /// <summary>
        /// Address 2 longitude (null in this case)
        /// </summary>
        [JsonPropertyName("address2_longitude")]
        public string Address2Longitude { get; set; }

        /// <summary>
        /// Address 1 fax (null in this case)
        /// </summary>
        [JsonPropertyName("address1_fax")]
        public string Address1Fax { get; set; }

        /// <summary>
        /// Customization prefix
        /// </summary>
        [JsonPropertyName("customizationprefix")]
        public string CustomizationPrefix { get; set; }

        /// <summary>
        /// Created on behalf by value (null in this case)
        /// </summary>
        [JsonPropertyName("_createdonbehalfby_value")]
        public string CreatedOnBehalfByValue { get; set; }

        /// <summary>
        /// Modified by value
        /// </summary>
        [JsonPropertyName("_modifiedby_value")]
        public string ModifiedByValue { get; set; }

        /// <summary>
        /// Date and time when created
        /// </summary>
        [JsonPropertyName("createdon")]
        public string CreatedOn { get; set; }

        /// <summary>
        /// Address 2 country (null in this case)
        /// </summary>
        [JsonPropertyName("address2_country")]
        public string Address2Country { get; set; }

        /// <summary>
        /// Description (null in this case)
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Address 2 address ID
        /// </summary>
        [JsonPropertyName("address2_addressid")]
        public string Address2AddressId { get; set; }

        /// <summary>
        /// Address 1 shipping method code
        /// </summary>
        [JsonPropertyName("address1_shippingmethodcode")]
        public int? Address1ShippingMethodCode { get; set; }

        /// <summary>
        /// Address 1 post office box (null in this case)
        /// </summary>
        [JsonPropertyName("address1_postofficebox")]
        public string Address1PostOfficeBox { get; set; }

        /// <summary>
        /// Address 1 UPS zone (null in this case)
        /// </summary>
        [JsonPropertyName("address1_upszone")]
        public string Address1UpsZone { get; set; }

        /// <summary>
        /// Address 1 address type code
        /// </summary>
        [JsonPropertyName("address1_addresstypecode")]
        public int? Address1AddressTypeCode { get; set; }

        /// <summary>
        /// Address 1 country (null in this case)
        /// </summary>
        [JsonPropertyName("address1_country")]
        public string Address1Country { get; set; }

        /// <summary>
        /// Entity image ID (null in this case)
        /// </summary>
        [JsonPropertyName("entityimageid")]
        public string EntityImageId { get; set; }

        /// <summary>
        /// Entity image (null in this case)
        /// </summary>
        [JsonPropertyName("entityimage")]
        public string EntityImage { get; set; }

        /// <summary>
        /// Created by value
        /// </summary>
        [JsonPropertyName("_createdby_value")]
        public string CreatedByValue { get; set; }

        /// <summary>
        /// Address 1 telephone 3 (null in this case)
        /// </summary>
        [JsonPropertyName("address1_telephone3")]
        public string Address1Telephone3 { get; set; }

        /// <summary>
        /// Address 1 city (null in this case)
        /// </summary>
        [JsonPropertyName("address1_city")]
        public string Address1City { get; set; }

        /// <summary>
        /// Address 1 telephone 2 (null in this case)
        /// </summary>
        [JsonPropertyName("address1_telephone2")]
        public string Address1Telephone2 { get; set; }

        /// <summary>
        /// Address 1 telephone 1 (null in this case)
        /// </summary>
        [JsonPropertyName("address1_telephone1")]
        public string Address1Telephone1 { get; set; }
    }
}

