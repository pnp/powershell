using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class AzureADSignInGeoCoordinates
    {
        [JsonPropertyName("altitude")]
        public object Altitude { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
}
