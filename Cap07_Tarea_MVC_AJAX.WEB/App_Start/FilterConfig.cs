using System.Web;
using System.Web.Mvc;

namespace Cap07_Tarea_MVC_AJAX.WEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
