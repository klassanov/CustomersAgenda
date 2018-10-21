using CustomersAgenda.WebUI.Controllers;
using CustomersAgenda.WebUI.Infrastructure;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CustomersAgenda.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();          
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolver.SetResolver(new NinjectDependencyResolver());

        }

      
      
        protected void Application_Error(Object sender, EventArgs e)
        {
            if (Server != null)
            {
                HttpContext appContext = ((MvcApplication)sender).Context;
                Exception ex=Server.GetLastError();
                ILog logger = LogManager.GetLogger(typeof(MvcApplication));
                logger.Error("Unhandled application exception occured", ex);
                Server.ClearError();
                
                //Static html error page, not a route
                string redirectRoute = ConfigurationManager.AppSettings["errorDefaultRoute"];

                HttpException httpException = ex as HttpException;
                if(httpException!=null)
                {
                    int httpCode = httpException.GetHttpCode();
                    switch (httpCode)
                    {
                        case (int) HttpStatusCode.NotFound:
                            redirectRoute = ConfigurationManager.AppSettings["error404Route"];
                            break;
                        default:
                            break;
                    }
                }
                
                Response.Redirect(string.Format("~{0}", redirectRoute));
            }
        }
       
       
       
    }
}
