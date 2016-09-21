using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AyudaTestProject.Models
{
    public class BeerSearch
    {
        public List<Beer> result;

        public BeerSearch(string styleId, string seasonId, string glasswareId, bool isOrganic, bool isLabel)
        {
            var client = new RestClient("http://api.brewerydb.com/v2/");
            var request = new RestRequest("beers", Method.GET);
            request.AddParameter("key", "c0940c86fbb07c352375906d2c9e212d"); // adds to POST or URL querystring based on Method

            request.AddParameter("styleId", styleId);
            request.AddParameter("availableId", seasonId);
            request.AddParameter("glasswareId", glasswareId);
            request.AddParameter("isOrganic", isOrganic ? "Y" : "N");
            request.AddParameter("hasLabels", isLabel ? "Y" : "N");

            request.AddParameter("order", "random");
            request.AddParameter("randomCount", "5");

            // execute the request
            IRestResponse response = client.Execute(request);
            //var content = response.Content; // raw content as string

            JObject jo = JObject.Parse(response.Content);
            JArray j = (JArray)jo["data"];
        
            List<Beer> list = new List<Beer>();

            if (j != null && j.Children().Any())
            {
                foreach (JToken jc in j.Children())
                {
                    Beer beer = new Beer()
                    {
                        id = jc["id"].ToString(),
                        name = jc["name"].ToString(),
                        description = (jc["description"] == null) ? "" : jc["description"].ToString()
                    };

                    if (jc["labels"] != null && jc["labels"].HasValues)
                        beer.label = jc["labels"].Last.Last.Value<string>();

                    list.Add(beer);
                }
            }
            result = list;
        }

    }

    public class Beer
    {
        public string id;
        public string name;
        public string description;
        public string label;
    }

}