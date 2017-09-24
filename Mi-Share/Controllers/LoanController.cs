using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mi_Share.Controllers
{
    [Authorize]
    public class LoanController : Controller
    {
        // GET: Lending
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoanedItems()
        {
            return View();
        }

        public ActionResult BorrowedItems()
        {
            return View();
        }
    }
}