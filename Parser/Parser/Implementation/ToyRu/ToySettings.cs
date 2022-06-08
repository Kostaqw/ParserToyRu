using Parser.Parser.Interfaces;

namespace Parser.Parser.Implementation.ToyRu
{
    public class ToySettings : IParserSettings
    {

        public ToySettings(string url)
        {
            BaseUrl = url;
        }
        public string BaseUrl { get; set; } 
        public string Prefix { get; set; }
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
