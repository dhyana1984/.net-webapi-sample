using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPISample.Models;

namespace WebAPISample.Controllers
{
    public class GreetingController : ApiController
    {
        public string GetGreeting()
        {
            return "Hello World!";
        }

        private static List<Greeting> _greeting = new List<Greeting>
        {
          new Greeting(){ Name="AAA",Message="aaaaaaaaaaaa"},
          new Greeting(){Name="BBB",Message="bbbbbbbbbbb"}
        };

        [NonAction]
        public List<Greeting> GetGreetingData()
        {
            return _greeting;
        }
        public HttpResponseMessage PostGreeting(Greeting greeting)
        {
            _greeting.Add(greeting);

            var greetingLocation = new Uri(this.Request.RequestUri, "greeting/" + greeting.Name);
            var response = this.Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = greetingLocation;

            return response;
        }

        public string GetGreeting(string id)
        {
            var greeting = _greeting.FirstOrDefault(t => t.Name == id);
            if (greeting == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return  greeting.Message;
        }

     
    }
}
