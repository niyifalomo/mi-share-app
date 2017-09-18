using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mi_Share.Controllers
{
    public class CollectionController : Controller
    {
        // GET: Collection
        public ActionResult Index()
        {
            //return View();
           return RedirectToAction("MyCollection");
        }
        public ActionResult MyCollection()
        {
            return View("MyCollection");
        }


        public ActionResult CreateItem()
        {

            return View();
        }
    }
}