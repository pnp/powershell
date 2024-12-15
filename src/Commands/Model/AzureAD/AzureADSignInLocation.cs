using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class AzureADSignInLocation
    {
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("countryOrRegion")]
        public string CountryOrRegion { get; set; }

        [JsonPropertyName("geoCoordinates")]
        public AzureADSignInGeoCoordinates GeoCoordinates { get; set; }
    }
}
