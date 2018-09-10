using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using WebAPISample.Controllers;
using WebAPISample.Models;
using System.Linq;

namespace WebAPITest
{
    [TestClass]
    public class GreetingTest
    {
        [TestMethod]
        public void TestGreetingAdd()
        {
            //准备
            var greetingName = "newgreeting";
            var greetingMessage = "Hello World";
            var fakeRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8080/api/greeting");
            var greeting = new Greeting { Name = greetingName, Message = greetingMessage };

            var service = new GreetingController();
            service.Request = fakeRequest;

            //操作
            var response = service.PostGreeting(greeting);

            //断言
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(new Uri("Http://localhost:8080/api/greeting/newgreeting"), response.Headers.Location);
        }

        [TestMethod]
        public void TestGreetingGet()
        {
            //准备
        
            var fakeRequest = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8080/api/greeting");
            var service = new GreetingController();
            service.Request = fakeRequest;

            //操作
            var response = service.GetGreeting();

            //断言
            Assert.IsNotNull(response);
            Assert.AreEqual("Hello World!", response.ToString());
        }


        [TestMethod]
        public void TestGreetingGetByID()
        {
            //准备

            var fakeRequest = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8080/api/greeting");
            var service = new GreetingController();
            var name = "AAA";
            var _greeting = service.GetGreetingData();
            service.Request = fakeRequest;

            //操作
            var response = service.GetGreeting(name);

            //断言
            Assert.IsNotNull(response);
            Assert.AreEqual(_greeting.FirstOrDefault(t=>t.Name== name).Message, response.ToString());
        }
    }
}
