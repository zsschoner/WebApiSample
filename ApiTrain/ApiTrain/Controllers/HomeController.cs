using System.Web.Mvc;
using System.Web.Http;
using ApiMvc.Models;

namespace ApiMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Explores registered web apis in the solution
        /// </summary>
        /// <returns></returns>
        public ActionResult ExploreApis()
        {
            return View(new ApiModel(GlobalConfiguration.Configuration.Services.GetApiExplorer()));
        }
    }
}
