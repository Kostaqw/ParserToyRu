using AngleSharp.Html.Parser;
using Parser.Parser.Interfaces;
using System;

namespace Parser.Parser
{
    public class Worker<T> where T : class
    {

        HtmlLoader loader;
        public bool isActive { get; set; }
        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;


        private IParser<T> parser;
        private IParserSettings settings;

        public IParser<T> Parser
        { 
            get { return parser; }
            set { parser = value; }
        }
        public IParserSettings Settings 
        {
           
            get { return settings; }
            set
            {
                settings = value;
                loader = new HtmlLoader(value);
            }
        }

        public Worker(IParser<T> parser)
        {
            Parser = parser;
        }

        public Worker(IParser<T> parser, IParserSettings settings) :this(parser)
        {
            Settings=settings;
        }

        public void Start()
        {
            isActive = true;
            Work();
        }

        public void Stop()
        {
            isActive = false;
        }

        private async void Work()
        {
            if (settings.EndPoint != 0)
            {
                for (int i = Settings.StartPoint; i <= Settings.EndPoint; i++)
                {
                    if (!isActive)
                    {
                        OnCompleted?.Invoke(this);
                        return;
                    }

                    var source = await loader.GetSourceById(i);
                    var domParser = new HtmlParser();
                    
                    var document = await domParser.ParseDocumentAsync(source);
                    var result = Parser.Parse(document);
                    OnNewData?.Invoke(this, result);
                }
            }
            else
            {
                if (!isActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }
                var source = await loader.GetSource();
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(source);
                var result = Parser.Parse(document);
                OnNewData?.Invoke(this, result);
            }

            OnCompleted?.Invoke(this);
            isActive = false;
        }
    }
}
