using System;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class SPOSitePipeBind
    {
        private string _url;
        private Guid? _id;

        public string Url => _url;

        public Guid? Id => _id;

        public SPOSitePipeBind(string identity)
        {
            if(Guid.TryParse(identity, out Guid id))
            {
                _id = id;
            }
            else
            {
                _url = identity?.TrimEnd('/');
            }
        }   

        public SPOSitePipeBind(Uri uri)
        {
            _url = uri.AbsoluteUri?.TrimEnd('/');
        }

        public SPOSitePipeBind(SPOSite site)
        {
            if(string.IsNullOrEmpty(site.Url))
            {
                throw new PSArgumentException("Site Url must be specified");
            }
            _url = site.Url?.TrimEnd('/');
        }

        public SPOSitePipeBind(Model.SPODeletedSite site)
        {
            if(string.IsNullOrEmpty(site.Url))
            {
                throw new PSArgumentException("Site Url must be specified");
            }
            _url = site.Url?.TrimEnd('/');
        }
    }
}