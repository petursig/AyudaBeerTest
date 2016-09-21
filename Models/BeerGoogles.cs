using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AyudaTestProject.Models
{
    public class BeerGoogles
    {
        public static BeerGoogles Singleton = new BeerGoogles();

        private BeerGoogles() { }

        private List<Category> _categories;
        public List<Category> Categories{
            get{
                if(_categories == null) 
                    _categories = CollectCategories();
                return _categories;
            }
        }

        private List<BeerStyle> _styles;
        public List<BeerStyle> Styles{
            get{
                if(_styles == null)
                    _styles = CollectStyles();
                return _styles;
            }
        }
        
        private List<GlassWare> _glasswares;
        public List<GlassWare> Glasswares{
            get{
                if(_glasswares == null)
                    _glasswares = CollectGlassware();
                return _glasswares;
            }
        }

        private List<BeerSeason> _beerseasons;
        public List<BeerSeason> BeerSeasons{
            get{
                if (_beerseasons == null)
                    _beerseasons = CollectBeerSeasons();
                return _beerseasons;
            }
        }
        
        private List<Category> CollectCategories()
        {
            var client = new RestClient("http://api.brewerydb.com/v2/");
            var request = new RestRequest("menu/categories", Method.GET);
            request.AddParameter("key", "c0940c86fbb07c352375906d2c9e212d"); // adds to POST or URL querystring based on Method

            // execute the request
            IRestResponse response = client.Execute(request);
            //var content = response.Content; // raw content as string

            JObject jo = JObject.Parse(response.Content);
            JArray j = (JArray)jo["data"];

            List<Category> list = new List<Category>();
            foreach (JToken jc in j.Children())
                list.Add(new Category() { id = jc["id"].ToString(), name = jc["name"].ToString() });

            return list;
        }

        private List<BeerStyle> CollectStyles()
        {
                var client = new RestClient("http://api.brewerydb.com/v2/");
                var request = new RestRequest("menu/styles", Method.GET);
                request.AddParameter("key", "c0940c86fbb07c352375906d2c9e212d"); // adds to POST or URL querystring based on Method

                IRestResponse response = client.Execute(request);
            
                JObject jo = JObject.Parse(response.Content);
                JArray j = (JArray)jo["data"];

                List<BeerStyle> list = new List<BeerStyle>();
                foreach (JToken jc in j.Children())
                {
                    BeerStyle style = new BeerStyle() {
                        id = jc["id"].ToString(),
                        description = (jc["description"] == null) ? "" : jc["description"].ToString(),
                        name = jc["name"].ToString(),
                        categoryId = jc["categoryId"].ToString()
                    };
                    list.Add(style);
                }
                return list;
        }

        private List<GlassWare> CollectGlassware()
        {
            var client = new RestClient("http://api.brewerydb.com/v2/");
            var request = new RestRequest("menu/glassware", Method.GET);
            request.AddParameter("key", "c0940c86fbb07c352375906d2c9e212d"); // adds to POST or URL querystring based on Method
            // execute the request
            IRestResponse response = client.Execute(request);
            //var content = response.Content; // raw content as string

            JObject jo = JObject.Parse(response.Content);
            JArray j = (JArray)jo["data"];

            List<GlassWare> list = new List<GlassWare>();
            foreach (JToken jc in j.Children())
                list.Add(new GlassWare() { id = jc["id"].ToString(), name = jc["name"].ToString() });

            return list;
        }

        private List<BeerSeason> CollectBeerSeasons()
        {
            var client = new RestClient("http://api.brewerydb.com/v2/");
            var request = new RestRequest("menu/beer-availability", Method.GET);
            request.AddParameter("key", "c0940c86fbb07c352375906d2c9e212d"); // adds to POST or URL querystring based on Method
            // execute the request
            IRestResponse response = client.Execute(request);
            //var content = response.Content; // raw content as string

            JObject jo = JObject.Parse(response.Content);
            JArray j = (JArray)jo["data"];

            List<BeerSeason> list = new List<BeerSeason>();
            foreach (JToken jc in j.Children())
                list.Add(new BeerSeason() { id = jc["id"].ToString(), name = jc["name"].ToString(), description = jc["description"].ToString() });

            return list;
        }

    }


    public class Category
    {
        public string id;
        public string name;
        public List<BeerStyle> Styles
        {
            get
            {
                return BeerGoogles.Singleton.Styles.Where(s => s.categoryId == this.id).ToList();
            }
        }
    }

    public class BeerStyle
    {
        public string id;
        public string description;
        public string name;
        public string categoryId;
        public Category Category
        {
            get
            {
                return BeerGoogles.Singleton.Categories.First(c => c.id == this.categoryId);
            }
        }

    }

    public class GlassWare
    {
        public string id;
        public string name;
    }

    public class BeerSeason
    {
        public string id;
        public string description;
        public string name;
    }
}