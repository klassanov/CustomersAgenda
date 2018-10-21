using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomersAgenda.WebUI.CustomExceptionFilters
{
    public class DefaultExceptionAttribute: HandleErrorAttribute
    {
        private static ILog Logger = LogManager.GetLogger(typeof(LogExceptionFilter));

        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            Logger.Error(filterContext.Exception.Message, filterContext.Exception);
            if (filterContext.Exception.InnerException != null){
                Logger.Error(filterContext.Exception.InnerException.Message, filterContext.Exception.InnerException); ;
            }
        }
    }
}