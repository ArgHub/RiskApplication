using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RiskApplication.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult BetsHistory()
        {
            return View();
        }

        public ActionResult RiskyUnsettledBets()
        {
            return View();
        }

    }
}