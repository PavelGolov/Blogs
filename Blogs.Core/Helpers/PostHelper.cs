using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Core.Helpers
{
    public static class PostHelper
    {
        public static string GetShortHtmlDesc(string HtmlDesc)
        {
            string text = HtmlDesc;
            int nWords = 0;
            string shortText = "";
            for (int i = 0; (i < text.Length) && (nWords != 15); i++)
            {
                if (text[i] == ' ')
                    nWords++;
                if (text[i] == '<')
                {
                    while ((text[i] != '>') && (i < text.Length))
                    {
                        i++;
                    }
                }
                else
                    shortText += text[i];
            }
            return shortText + "...";
        }
    }
}
