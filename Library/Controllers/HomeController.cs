using System.Web.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Prosta biblioteka do wykorzystania w domu.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "O mnie";
            ViewBag.Name = "Grzegorz Pietrzak";
            ViewBag.Location = "Rzeszów";

            return View();
        }
    }
}