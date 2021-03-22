using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blogs.Core.Markdown.ElementParsers;

namespace Blogs.UnitTests.Core.Markdown.ElementParsers
{
    [TestClass()]
    public class BackslashEscapeTests
    {
        BaseElementParser backslashEscape = new BackslashEscapeParser();
        [TestMethod()]
        public void Parse_LiteralAsterisks_ReturnsHtmlCode()
        {
            string input = @"\*";
            string expected = "&#42;";
            string actual = backslashEscape.Parse(input);
            Assert.AreEqual(expected, actual);
        }
    }
}