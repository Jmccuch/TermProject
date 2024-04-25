using Microsoft.AspNetCore.Mvc;

namespace TermProject.Controllers
{
    public class My2048Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FinalScore(string score)
        {
            System.Diagnostics.Debug.WriteLine("SCOOOOOOOORE: " + score);



            return RedirectToAction("Index","Dashboard");
        }
    }
}
