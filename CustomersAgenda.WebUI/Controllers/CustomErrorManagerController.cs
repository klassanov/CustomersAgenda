using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomersAgenda.WebUI.Controllers
{
    public class CustomErrorManagerController : Controller
    {
       public ViewResult ServerError()
       {
            return View();
       }

        public ViewResult Status404()
        {
            return View();
        }
    }
}