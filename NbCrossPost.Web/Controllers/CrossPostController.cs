using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using NbCrossPost.Web.Models;
using NbEventApiClient;
using Newtonsoft.Json;

namespace NbCrossPost.Web.Controllers
{
    [Authorize]
    public class CrossPostController : Controller
    {
        public ActionResult Events(int sourcePage = 1, int targetPage = 1)
        {
            var eventsViewModel = new EventsViewModel { SourcePage = sourcePage, TargetPage = targetPage };
            eventsViewModel.LoadEventData();

            return View(eventsViewModel);
        }

        [HttpPost]
        public ActionResult TransferEvent(int sourcePage, int sourceCalId)
        {
            var eventsViewModel = new EventsViewModel {SourcePage = sourcePage};
            eventsViewModel.LoadEventData();

            try
            {
                
                eventsViewModel.TransferEvent(sourceCalId);
                ViewBag.TransferMessage = "TRANSFER SUCCESS!";
            }
            catch (Exception ex)
            {
                ViewBag.TransferMessage = ex.Message;
                var webException = ex as WebException;
                if (webException != null && webException.Response != null)
                {
                    var responseStream = webException.Response.GetResponseStream();
                    if (responseStream != null)
                        ViewBag.TransferMessage += "<br/>HTTP Response:<br/>" +
                                                    GetPrettyJson(responseStream);
                }
            }

            return View("Events", eventsViewModel);
        }

        private static string GetPrettyJson(Stream responseStream)
        {
            var jsonString = new StreamReader(responseStream).ReadToEnd();
            dynamic parsed = JsonConvert.DeserializeObject(jsonString);
            return "<pre>" + JsonConvert.SerializeObject(parsed, Formatting.Indented) + "</pre>";
        }
    }
}