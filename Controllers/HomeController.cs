using AyudaTestProject.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AyudaTestProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BeerGoogles model = BeerGoogles.Singleton;

            return View(model);
        }

        public PartialViewResult Results(string styleId, string seasonId, string glasswareId, bool isOrganic, bool isLabel)
        {
            BeerSearch model = new BeerSearch(styleId, seasonId, glasswareId, isOrganic, isLabel);

            return PartialView(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}