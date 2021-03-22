using Blogs.Core.Intefaces;

namespace Blogs.Core.Helpers
{
    public class PostRedactor : IPostRedactor
    {
        public string GetShortHtmlDesc(string HtmlDesc)
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
