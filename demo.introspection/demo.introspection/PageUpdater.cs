using RestSharp;
using RestSharp.Authenticators;

namespace demo.introspection
{
    public class PageUpdater
    {
        readonly IRestClient _restClient = new RestClient("https://magdevelopment.atlassian.net/wiki/rest/api");

        public PageUpdater()
        {
            _restClient.Authenticator = new HttpBasicAuthenticator("drusellers", "3jqLUaCqCrrX");
        }

        public string Get()
        {
            //https://magdevelopment.atlassian.net/wiki/display/SYS/Wilford+Brimley
            //https://magdevelopment.atlassian.net/wiki/rest/api/content?space=SYS&title=Wilford+Brimley&expand=body.view
            var req = new RestRequest("/content", Method.GET);
            req.AddQueryParameter("space", "SYS");

            //arg, RestSharp is encoding the +
            req.AddQueryParameter("title", "Wilford+Brimley");
            
            req.AddQueryParameter("expand", "body.view");

            var resp = _restClient.Execute(req);

            return resp.Content;
        }

        public void Put()
        {
            
        }
    }
}
