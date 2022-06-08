using AngleSharp.Html.Dom;

namespace Parser.Parser.Interfaces
{
    public interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
