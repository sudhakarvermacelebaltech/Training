using System.Web;
using System.Web.Mvc;

namespace MVC_WEb_APi_Merge
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
