using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomersAgenda.WebUI.CustomExceptionFilters
{
    public class LogExceptionFilter : FilterAttribute, IExceptionFilter
    {
        private static ILog Logger = LogManager.GetLogger(typeof(LogExceptionFilter));

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                string url = ConfigurationManager.AppSettings["logExceptionPage"];
                filterContext.Result = new RedirectResult(url);
                
                Logger.Error(filterContext.Exception.Message, filterContext.Exception);
                if (filterContext.Exception.InnerException != null)
                {
                    Logger.Error(filterContext.Exception.InnerException.Message, filterContext.Exception.InnerException); ;
                }
                
                filterContext.ExceptionHandled = true;
            }
            
               
        }
    }
}