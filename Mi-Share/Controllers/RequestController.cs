using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mi_Share.Controllers
{
    public class RequestController : Controller
    {
        // GET: Request
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyRequests()
        {
            return View();
        }

        public ActionResult PendingRequests()
        {
            return View();
        }
    }
}