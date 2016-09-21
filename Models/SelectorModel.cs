using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AyudaTestProject.Models
{
    public class SelectorModel
    {


        public List<Category> GetCategories() {

            var client = new RestClient("http://api.brewerydb.com/v2/");
            var request = new RestRequest("categories", Method.GET);
            request.AddParameter("key", "c0940c86fbb07c352375906d2c9e212d"); // adds to POST or URL querystring based on Method
            
            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            IRestResponse<List<Category>> response2 = client.Execute<List<Category>>(request);
            List<Category> list = response2.Data;

            return list;

        }

    }

    public class Category {
        public string id;
        public string name;
    }
}