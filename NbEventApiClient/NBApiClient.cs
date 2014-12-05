using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;

namespace NbEventApiClient
{
    public class JsonNbApiClient
    {
        private readonly string _apiKey;
        private readonly string _nationSlug;

        public JsonNbApiClient(string apiKey, string nationSlug)
        {
            _apiKey = apiKey;
            _nationSlug = nationSlug;
        }

        public EventData GetEventData(string siteSlug, int page = 1)
        {
            // Customize URL
            var url = String.Format(
                "https://{0}.nationbuilder.com/api/v1/sites/{1}/pages/events?page={2}&access_token={3}",
                _nationSlug,
                siteSlug,
                page,
                _apiKey);

            // Synchronous Consumption
            using (var syncClient = GetNewWebClient())
            {
                string eventListContent = syncClient.DownloadString(url);
                return JsonConvert.DeserializeObject<EventData>(eventListContent);
            }
        }

        // TODO: this may need to take source page #, source calId
        public void PostEvent(string targetSiteSlug, Result eventToTransfer)
        {
            var evtPostData = new EventNew(eventToTransfer)
            {
                calendar_id = 1,
                autoresponse = { broadcaster_id = 1 },
                time_zone = "-07:00"
            };

            var newEvtData = new EventDataPost(evtPostData);
            string postData2 = JsonConvert.SerializeObject(newEvtData);

            using (var syncClient = GetNewWebClient())
            {
                var urlTarget = String.Format(
                "https://{0}.nationbuilder.com/api/v1/sites/{1}/pages/events?access_token={2}",
                _nationSlug,
                targetSiteSlug,
                _apiKey);

                syncClient.UploadString(urlTarget, "POST", postData2);
            }
        }

        private static WebClient GetNewWebClient()
        {
            var syncClient = new WebClient();
            syncClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            syncClient.Headers.Add(HttpRequestHeader.Accept, "application/json");
            syncClient.Encoding = Encoding.UTF8;
            return syncClient;
        }
    }
}
