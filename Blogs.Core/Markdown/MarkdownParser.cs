using Blogs.Core.Intefaces;
using System.Collections.Generic;

namespace Blogs.Core.Markdown
{
    public class MarkdownParser : IParser
    {
        private readonly IEnumerable<IElementParser> _parsers;
        public MarkdownParser(IEnumerable<IElementParser> parsers)
        {
            _parsers = parsers;
        }
        public string Parse(string text)
        {
            text = text + "\n\n";

            foreach (var parser in _parsers)
            {
                while (parser.IsElementFound(text))
                {
                    var mdElement = parser.GetFirstElement(text);
                    text = text.Replace(mdElement, parser.Parse(mdElement));
                }
            }

            return text;
        }
    }
}
