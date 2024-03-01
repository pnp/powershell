using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Model
{
    public class PageLikedByInformation
    {
        public string Name { get; set; }
        public string Mail {  get; set; }
        public int Id { get; set; }
        public string LoginName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
