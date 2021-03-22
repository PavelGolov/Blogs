using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Core.Models
{
    public class BackslashEscape
    {
        public string MdBackslashEscape { get; set; }
        public string HtmlBackslashEscape { get; set; }
        public BackslashEscape(string mdBackslashEscape,string htmlBackslashEscape) 
        {
            MdBackslashEscape = mdBackslashEscape;
            HtmlBackslashEscape = htmlBackslashEscape;
        }
    }
}
