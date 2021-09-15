using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using AddressBookRestSharp;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace AddressBookTest
{

    [TestClass]
    public class AddressBookRestSharpTest
    {
        //Initializing the restclient 
        RestClient client;

        [TestInitialize]

        public void Setup()
        {
            client = new RestClient("http://localhost:3000");
        }
        public IRestResponse GetAllPersons()
        {
            IRestResponse response = default;
            try
            {
                //Get request from json server
                RestRequest request = new RestRequest("/addressBook", Method.GET);
                //Requesting server and execute , getting response
                response = client.Execute(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return response;
        }
        [TestMethod]
        public void TestMethodToGetAllPersons()
        {
            try
            {
                //calling get all persom method 
                IRestResponse response = GetAllPersons();
                //converting response to list og objects
                var res = JsonConvert.DeserializeObject<List<Person>>(response.Content);
                //Check whether all contents are received or not
                Assert.AreEqual(3, res.Count);
                //Checking the response statuscode 200-ok
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                res.ForEach((x) =>
                {
                    Console.WriteLine($"id = {x.id} ,First name = {x.FirstName} , Last name = {x.LastName} , Phone number = {x.PhoneNumber} , address = {x.Address} , city ={x.City} , state = {x.State} , zipcode = {x.ZipCode} , emailid = {x.EmailId} ");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public IRestResponse AddToJsonServer(JsonObject json)
        {
            IRestResponse response = default;
            try
            {
                RestRequest request = new RestRequest("/persons", Method.POST);
                //adding type as json in request and pasing the json object as a body of request
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                //Execute the request
                response = client.Execute(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;

        }
        [TestMethod]
        public void TestForAddMultipleDataToJsonServerFile()
        {
            try
            {
                //list for storing person  data json objects
                List<JsonObject> personList = new List<JsonObject>();
                JsonObject json = new JsonObject();
                json.Add("FirstName", "Lionel");
                json.Add("LastName", "Messi");
                json.Add("PhoneNumber", 1234509876);
                json.Add("Address", "avadi");
                json.Add("City", "chennai");
                json.Add("State", "Tn");
                json.Add("ZipCode", 852963);
                json.Add("EmailId", "lm10@asd.com");

                //add object to list
                personList.Add(json);
                JsonObject json1 = new JsonObject();
                json1.Add("FirstName", "Neymar");
                json1.Add("LastName", "jr");
                json1.Add("PhoneNumber", 9874509876);
                json1.Add("Address", "surapet");
                json1.Add("City", "chennai");
                json1.Add("State", "Tn");
                json1.Add("ZipCode", 231963);
                json1.Add("EmailId", "njr@asd.com");
                personList.Add(json1);

                personList.ForEach((x) =>
                {
                    AddToJsonServer(x);
                });
                //Check by gettting all person details
                IRestResponse response = GetAllPersons();
                //convert json object to person object
                var res = JsonConvert.DeserializeObject<List<Person>>(response.Content);

                res.ForEach((x) =>
                {
                    Console.WriteLine($"id = {x.id} ,First name = {x.FirstName} , Last name = {x.LastName} , Phone number = {x.PhoneNumber} , address = {x.Address} , city ={x.City} , state = {x.State} , zipcode = {x.ZipCode} , emailid = {x.EmailId} ");
                });
                //Checking the response statuscode 200-ok
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


    }
}
