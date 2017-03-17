using System.Web;
using System.Web.Mvc;

namespace InternalModule.Boilerplate
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // NOTE : When don't need SSL
            //filters.Add(new RequireHttpsAttribute());
        }
    }
}
