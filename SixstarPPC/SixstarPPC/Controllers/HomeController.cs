using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SixstarPPC.Models;

namespace SixstarPPC.Controllers
{
    public class HomeController : Controller
    {
        ppcdbEntities model = new ppcdbEntities();
        public ActionResult Index()
        {
            var porperty = model.Properties.ToList();
            return View(porperty);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult News()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Collections()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SinglePropety(int id)
        {
            var property = model.Properties.FirstOrDefault(x => x.ID == id);
            return View(property);
        }
    }
}