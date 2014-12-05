// Author:         Anthony Baker
// Date:           May 4th, 2013
// Description:    JsonAPIClient - Sample JSON API Consumption

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;


namespace NbEventApiClient
{
    /// <summary>
    /// Class to represent the WeatherData
    /// </summary>
    [DataContract]
    public class EventData
    {
        [DataMember]
        public int page { get; set; }

        [DataMember]
        public int total_pages { get; set; }

        [DataMember]
        public int per_page { get; set; }

        [DataMember]
        public int total { get; set; }

        [DataMember]
        public List<Result> results { get; set; }
    }


    public class Contact
    {
        public string name { get; set; }
        public string phone { get; set; }
        public bool show_phone { get; set; }
        public string email { get; set; }
        public bool show_email { get; set; }
    }

    public class RsvpForm
    {
        public string phone { get; set; }
        public string address { get; set; }
        public bool allow_guests { get; set; }
        public bool accept_rsvps { get; set; }
        public bool gather_volunteers { get; set; }
    }

    public class Address
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country_code { get; set; }
        public string zip { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class Venue
    {
        public string name { get; set; }
        public Address address { get; set; }
    }

    public class Autoresponse
    {
        public object broadcaster_id { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }

    public class Result
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string path { get; set; }
        public string status { get; set; }
        public string site_slug { get; set; }
        public string name { get; set; }  //
        public string headline { get; set; }
        public string title { get; set; }
        public string excerpt { get; set; }
        public int? author_id { get; set; }
        public DateTime? published_at { get; set; }
        public int? external_id { get; set; }
        public List<string> tags { get; set; }
        public string intro { get; set; }  //
        public int calendar_id { get; set; }  //
        public Contact contact { get; set; }    //
        public string start_time { get; set; }  //
        public string end_time { get; set; }   //
        public string time_zone { get; set; } //
        public RsvpForm rsvp_form { get; set; }  //
        public int capacity { get; set; }    //
        public bool show_guests { get; set; }  //
        public Venue venue { get; set; }    //
        public Autoresponse autoresponse { get; set; }
        public List<object> shifts { get; set; }   //
    }

   
    ///
    public class EventDataold
    {
        [DataMember]
        public double message { get; set; }
        [DataMember]
        public string cod { get; set; }
        [DataMember]
        public string calctime { get; set; }
        [DataMember]
        public int cnt { get; set; }
        [DataMember]
        public List<string> list { get; set; }
    }

    [DataContract]
    public class EventDataPost
    {
        [DataMember]
        public EventNew @event { get; set; }

        public EventDataPost(EventNew evt)
        {
            @event = evt;
        }
    }

        [DataContract]
        public class EventNew
        {
            [DataMember]
            public string slug { get; set; }

      //      [DataMember]
      //      public string path { get; set; }

     //       [DataMember]
    //        public string site_slug { get; set; }
            [DataMember]
            public string name { get; set; }  //
            [DataMember]
            public string headline { get; set; }
            [DataMember]
            public string title { get; set; }
     //       [DataMember]
     //       public int author_id { get; set; }
     //       [DataMember]
     //       public List<object> tags { get; set; }
            [DataMember]
            public string intro { get; set; }  //
            [DataMember]
            public int calendar_id { get; set; }  //
            [DataMember]
            public Contact contact { get; set; }    //
            [DataMember]
            public string start_time { get; set; }  //
            [DataMember]
            public string end_time { get; set; }   //
            [DataMember]
            public string time_zone { get; set; } //
            [DataMember]
            public RsvpForm rsvp_form { get; set; }  //
            [DataMember]
            public int capacity { get; set; }    //
            [DataMember]
            public bool show_guests { get; set; }  //
            [DataMember]
            public Venue venue { get; set; }    //
            [DataMember]
            public Autoresponse autoresponse { get; set; }
      //      [DataMember]
      //      public List<object> shifts { get; set; }   //
            [DataMember]
            public string status { get; set; }
  
      
            public EventNew(Result r)
        {
            slug = r.slug;
      //      path = r.path;
      //      site_slug= r.site_slug;
            name = r.name;
            headline = r.headline;
            title = r.title;
       //     author_id = r.author_id;
       //     tags = r.tags;
            intro = r.intro; //
            calendar_id = calendar_id; //
            contact = r.contact;   //
            start_time = r.start_time;  //
            end_time = r.end_time;  //
            time_zone = r.time_zone; //
            rsvp_form = r.rsvp_form; //
            capacity = r.capacity;    //
            show_guests= r.show_guests;//
            venue = r.venue;    //
            autoresponse = r.autoresponse;
     //       shifts = r.shifts;  //
            status = "drafted"; //  r.status;
        } 
        
        }
        
    }


