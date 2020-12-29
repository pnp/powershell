using System.Net;

namespace PnP.PowerShell.Commands.Model
{
    public class BatchRequest
    {
        public HttpStatusCode HttpStatusCode {get;set;}
        public string ResponseJson {get;set;}
    }
}