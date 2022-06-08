using Parser.Parser.Interfaces;

namespace Parser.Parser.Implementation.ToyRu
{
    public class PageSettings : IParserSettings
    {
        public PageSettings(int start, int end)
        {
            StartPoint = start;
            EndPoint = end;
        }
        public string BaseUrl { get; set; } = "https://www.toy.ru/catalog/boy_transport/2/?filterseccode%5B0%5D=transport&";
        public string Prefix { get; set; } = "PAGEN_4={currentId}";
        public int StartPoint { get; set; }
        public int EndPoint { get ; set ; }
    }
}
