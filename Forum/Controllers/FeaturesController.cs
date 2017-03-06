using System.Web.Mvc;

namespace Forum.Controllers
{
    public class FeaturesController : Controller
    {
        // GET: Features
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Markdown()
        {
            return View();
        }
    }
}