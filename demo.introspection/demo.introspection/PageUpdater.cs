using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;

namespace demo.introspection
{
    public class PageUpdater
    {
        readonly IRestClient _restClient = new RestClient("https://magdevelopment.atlassian.net/wiki/rest/api");

        public PageUpdater()
        {
            _restClient.Authenticator = new HttpBasicAuthenticator("dru.sellers", "3jqLUaCqCrrX");
        }

        public GetPageResponse Get(int pageId)
        {
            //https://magdevelopment.atlassian.net/wiki/display/SYS/Wilford+Brimley
            //https://magdevelopment.atlassian.net/wiki/rest/api/content?space=SYS&title=Wilford+Brimley&expand=body.view
            //pageId=12714628
            var req = new RestRequest("/content/{pageId}", Method.GET);
            req.AddUrlSegment("pageId", pageId.ToString());
            req.AddQueryParameter("space", "SYS");

            req.AddQueryParameter("expand", "body.view,version");


            var resp = _restClient.Execute<GetPageResponse>(req);

            return resp.Data;
        }

        public void Put(int pageId, int currentVersion, string content)
        {
            var req = new RestRequest("/content/{pageId}", Method.PUT);
            req.AddUrlSegment("pageId", pageId.ToString());

            var pu = new PageUpdate
            {
                version = new PageVersion
                {
                    number = currentVersion + 1
                },
                title = "TEST",
                type = "page",
                body = new PageBody
                {
                    storage = new PageStorage
                    {
                        value = content,
                        representation = "storage"
                    }
                }
            };

            req.AddJsonBody(pu);
            var resp = _restClient.Execute(req);
            Console.WriteLine(resp.StatusCode);
            Console.WriteLine(resp.Content);
        }
    }

    public class GetPageResponse
    {
        public string id { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public GetPageBody body { get; set; }
        public GetPageVersion version { get; set; }
    }

    public class GetPageVersion
    {
        public int number { get; set; }
    }
    public class GetPageBody
    {
        public GetPageView view { get; set; }
    }

    public class GetPageView
    {
        public string value { get; set; }
    }

    public class PageStorage
    {
        public string value { get; set; }
        public string representation { get; set; }
    }
    public class PageBody
    {
        public PageStorage storage { get; set; }
    }
    public class PageUpdate
    {
        public PageVersion version { get; set; }
        public List<PageAncestors> ancestors { get; set; }
        public string type { get; set; }
        public PageBody body { get; set; }

        public string title { get; set; }
    }

    public class PageAncestors
    {
        public int id { get; set; }
    }
    public class PageVersion
    {
        public int number { get; set; }
    }
}
