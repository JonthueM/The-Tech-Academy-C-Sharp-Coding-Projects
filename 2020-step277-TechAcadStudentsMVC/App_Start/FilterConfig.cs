using System.Web;
using System.Web.Mvc;

namespace _2020_step277_TechAcadStudentsMVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
