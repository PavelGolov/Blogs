
namespace Blogs.Core.Intefaces
{
    public interface IElementParser : IParser
    {
        public bool IsElementFound(string text);
        public string GetFirstElement(string text);
    }
}
