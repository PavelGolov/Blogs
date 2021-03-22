using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blogs.Core.Markdown;
using Blogs.Core.Markdown.ElementParsers;

namespace Blogs.UnitTests.Core.Markdown.ElementParsers
{
    [TestClass()]
    public class EmphasisTests
    {
        BaseElementParser emphasis = new EmphasisParser();
        [TestMethod()]
        public void Parse_SingleAsterisks_ReturnsEm()
        {
            string input = "*single asterisks*";
            string expected = "<em>single asterisks</em>";
            string actual = emphasis.Parse(input);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Parse_SingleUnderscores_ReturnsEm()
        {
            string input = "_single underscores_";
            string expected = "<em>single underscores</em>";
            string actual = emphasis.Parse(input);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Parse_DoubleAsterisks_ReturnsStrong()
        {
            string input = "**double asterisks**";
            string expected = "<strong>double asterisks</strong>";
            string actual = emphasis.Parse(input);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Parse_DoubleUnderscores_ReturnsStrong()
        {
            string input = "__double underscores__";
            string expected = "<strong>double underscores</strong>";
            string actual = emphasis.Parse(input);
            Assert.AreEqual(expected, actual);
        }
    }
}