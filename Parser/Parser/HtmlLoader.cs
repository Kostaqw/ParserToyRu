using Parser.Parser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Parser
{
    public class HtmlLoader
    {
        private readonly HttpClient _client;
        private readonly string _url;

        public HtmlLoader(IParserSettings settings)
        {
            _client = new HttpClient();
            
            _url = settings.BaseUrl + settings.Prefix;
        }

        public async Task<string> GetSourceById(int id)
        {
            var CurrentUrl = _url.Replace("{currentId}", id.ToString());

            var response = await _client.GetAsync(CurrentUrl);

            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            { 
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }

        public async Task<string> GetSource()
        {
            var CurrentUrl = "https://www." + _url;

            var response = await _client.GetAsync(CurrentUrl);

            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
