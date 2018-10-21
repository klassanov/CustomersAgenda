using CustomersAgenda.WebUI.CustomExceptionFilters;
using System.Web;
using System.Web.Mvc;

namespace CustomersAgenda.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new DefaultExceptionAttribute() { View = "ServerError" });
            filters.Add(new ProfileAllAttribute());
        }
    }
}
