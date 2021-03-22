using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blogs.Core.Markdown;
using Blogs.Core.Markdown.ElementParsers;

namespace Blogs.UnitTests.Core.Markdown.ElementParsers
{
    [TestClass()]
    public class HorizontalLineTests
    {
        BaseElementParser horizontalLine = new HorizontalLineParser();

        [TestMethod()]
        public void Parse_AsterisksAndSpaces_ReturnsHr()
        {
            string input = "* * *";
            string expected = "<hr />\n";
            string actual = horizontalLine.Parse(input);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Parse_Asterisks_ReturnsHr()
        {
            string input = "***";
            string expected = "<hr />\n";
            string actual = horizontalLine.Parse(input);
            Assert.AreEqual(expected, actual);
        }
    }
}