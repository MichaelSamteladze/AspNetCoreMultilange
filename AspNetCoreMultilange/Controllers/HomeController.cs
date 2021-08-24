using AspNetCoreMultilange.Models;
using AspNetCoreMultilange.Properties;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMultilange.Controllers
{
    public class HomeController : Controller
    {
        [Route("", Name = "Home")]
        [Route("{Culture:length(2)}", Name = "HomeCulture")]
        public IActionResult Index()
        {
            var Model = new HomeModel();

            Model.TextHelloWorld = Resources.TextHelloWorld;
            Model.UrlEnglish = Url.RouteUrl("Home");
            Model.UrlGeorgian = Url.RouteUrl("HomeCulture", new { Culture = "ka" });
            Model.UrlSpanish = Url.RouteUrl("HomeCulture", new { Culture = "es" });
            Model.UrlJapanese = Url.RouteUrl("HomeCulture", new { Culture = "ja" });

            return View("~/Views/Home/Index.cshtml", Model);
        }       
    }
}