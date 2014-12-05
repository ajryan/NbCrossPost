using System;
using System.Configuration;
using System.Linq;
using NbEventApiClient;

namespace NbCrossPost.Web.Models
{
    public class EventsViewModel
    {
        public string ApiKey { get; set; }
        public string NationSlug { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }

        public EventData SourceEventData { get; private set; }
        public int SourcePage { get; set; }

        public EventData TargetEventData { get; private set; }
        public int TargetPage { get; set;  }

        public EventsViewModel()
        {
            ApiKey = ConfigurationManager.AppSettings["ApiKey"];
            NationSlug = ConfigurationManager.AppSettings["NationSlug"];
            Source = ConfigurationManager.AppSettings["DefaultSource"];
            Target = ConfigurationManager.AppSettings["DefaultTarget"];

            SourcePage = 1;
            TargetPage = 1;
        }
        
        public void LoadEventData()
        {
            var eventClient = new JsonNbApiClient(ApiKey, NationSlug);
            
            SourceEventData = eventClient.GetEventData(Source, SourcePage);
            TargetEventData = eventClient.GetEventData(Target, TargetPage);
        }

        public void TransferEvent(int sourceCalId)
        {
            if (SourceEventData == null)
                throw new InvalidOperationException("Must load event data before posting.");

            var eventToTransfer = SourceEventData.results.FirstOrDefault(evt => evt.calendar_id == sourceCalId);
            if (eventToTransfer == null)
                throw new InvalidOperationException("Event with calendar_id " + sourceCalId + " was not found in current page of source results.");

            var eventClient = new JsonNbApiClient(ApiKey, NationSlug);
            eventClient.PostEvent(Target, eventToTransfer);

            // if we have target events, re-load to get the event just posted
            if (TargetEventData != null)
                TargetEventData = eventClient.GetEventData(Target, TargetPage);
        }
    }
}