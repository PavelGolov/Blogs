using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blogs.Core.Markdown;
using Blogs.Core.Markdown.ElementParsers;

namespace Blogs.UnitTests.Core.Markdown.ElementParsers
{
    [TestClass()]
    public class OrderedListTests
    {
        BaseElementParser list = new OrderedListParser();
        [TestMethod()]
        public void Parse_GradualList_ReturnsOrderedList()
        {
            string input = "1.  Bird\n2.  McHale\n3.  Parish\n";
            string expected = "<ol>\n<li>Bird</li>\n<li>McHale</li>\n<li>Parish</li>\n</ol>\n";
            string actual = list.Parse(input);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Parse_NotGradualList_ReturnsOrderedList()
        {
            string input = "1.  Bird\n1.  McHale\n1.  Parish\n";
            string expected = "<ol>\n<li>Bird</li>\n<li>McHale</li>\n<li>Parish</li>\n</ol>\n";
            string actual = list.Parse(input);
            Assert.AreEqual(expected, actual);
        }
    }
}