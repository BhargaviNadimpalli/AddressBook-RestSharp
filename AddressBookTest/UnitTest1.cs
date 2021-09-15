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

        /// <summary>
        /// Method to get all person details from server
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Test method to get all person details
        /// </summary>
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
    }
}
