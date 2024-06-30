using Microsoft.SharePoint.Client;
using System;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class SitePipeBind
    {
        private readonly Guid _id;
        private readonly string _url;
        private readonly Microsoft.SharePoint.Client.Site _site;

        public SitePipeBind()
        {
            _id = Guid.Empty;
            _url = string.Empty;
            _site = null;
        }

        public SitePipeBind(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException(nameof(url));
            }
            if (Guid.TryParse(url, out Guid siteId))
            {
                _id = siteId;
            }
            else
            {
                Uri uri;
                try
                {
                    uri = new Uri(url);
                }
                catch (UriFormatException)
                {
                    throw new ArgumentException(nameof(url));
                }
                _url = url;
            }
        }

        public SitePipeBind(Microsoft.SharePoint.Client.Site site)
        {
            site.EnsureProperties(s => s.Url, s => s.Id);
            _url = site.Url;
            _site = site;
            _id = site.Id;
        }

        public string Url => _url;

        public Microsoft.SharePoint.Client.Site Site => _site;

        public Guid Id => _id;

        public Guid GetSiteIdThroughGraph(PnPConnection connection, string accesstoken)
        {
            if (_site != null)
            {
                return _site.EnsureProperty(s => s.Id);
            }
            if (_id != Guid.Empty)
            {
                return _id;
            }
            if (!string.IsNullOrEmpty(_url))
            {
                var uri = new Uri(_url);

                var result = Utilities.REST.RestHelper.Get(connection.HttpClient, $"https://{connection.GraphEndPoint}/v1.0/sites/{uri.Host}:{uri.LocalPath}", accesstoken);
                if (!string.IsNullOrEmpty(result))
                {
                    var resultElement = JsonSerializer.Deserialize<JsonElement>(result);
                    if (resultElement.TryGetProperty("id", out JsonElement idProperty))
                    {
                        var idValue = idProperty.GetString();
                        return Guid.Parse(idValue.Split(',')[1]);
                    }
                }
            }
            return Guid.Empty;
        }
    }
}
