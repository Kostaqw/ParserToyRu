using AngleSharp.Html.Dom;
using Parser.Parser.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace Parser.Parser.Implementation.ToyRu
{
    public class ToyParser : IParser<Toy>
    {
        public Toy Parse(IHtmlDocument document)
        {

            var toy = new Toy();
            int oldPrice = 0;

            var priceS =new string(document.QuerySelector("span.price").TextContent.Where(char.IsDigit).ToArray());
            int price = Convert.ToInt32(priceS);
            try
            {
                var oldPriceS = new string(document.QuerySelector("span.old-price").TextContent.Where(char.IsDigit).ToArray());
                oldPrice = Convert.ToInt32(oldPriceS);
            }
            catch (NullReferenceException)
            {
                
            }
            toy.Region = document.QuerySelector("div.col-12.select-city-link>a").InnerHtml.Trim();
            toy.Category = GetPath(document);
            toy.Price = price;
            toy.OldPrice = oldPrice;
            toy.Available = document.QuerySelector("span.ok").TextContent;
            toy.Url = document.QuerySelector("link").GetAttribute("href");
            toy.ImageUrl = GetImages(document);
            toy.Title = document.QuerySelector("h1.detail-name").TextContent;

            return toy;
        }

        private string GetImages(IHtmlDocument document)
        {
            var imagesUrl = new StringBuilder();

            var images = document.QuerySelectorAll("img.img-fluid");
            foreach (var item in images)
            {
                var src = item.GetAttribute("src");
                if (src.Contains("https"))
                {
                    imagesUrl.Append(" " + item.GetAttribute("src"));
                }
            }
            return imagesUrl.ToString();
        }

        private string GetPath(IHtmlDocument document)
        {
            StringBuilder fullPath = new StringBuilder();
            var path = document.QuerySelectorAll("a.breadcrumb-item");
            foreach (var item in path)
            {
                fullPath.Append(item.TextContent + "/");
            }
            fullPath.Append(document.QuerySelector("span.breadcrumb-item").InnerHtml.Trim());

            return fullPath.ToString();
        }
    }
}
