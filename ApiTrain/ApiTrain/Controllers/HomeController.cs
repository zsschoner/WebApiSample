using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Http.Description;
using System.Text;
using ApiMvc.Models;

namespace ApiMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExploreApis()
        {
            return View(new ApiModel(GlobalConfiguration.Configuration.Services.GetApiExplorer()));
        }
    }
}
