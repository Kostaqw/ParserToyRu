using AngleSharp.Html.Dom;
using Parser.Parser.Interfaces;
using System.Collections.Generic;

namespace Parser.Parser.Implementation.ToyRu
{
    class PageParser : IParser<string[]>
    {
        List<string> pages = new List<string>();

        public string[] Parse(IHtmlDocument document)
        {
            
            foreach (var item in document.QuerySelectorAll("a.d-block.p-1.product-name.gtm-click"))
            {
                pages.Add("toy.ru"+item.GetAttribute("href"));
            }
            return pages.ToArray();
        }

    }
}
