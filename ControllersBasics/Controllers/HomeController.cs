using ControllersBasics.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllersBasics.Controllers
{
    public class HomeController : Controller
    {
        // ViewResult унаследован от ActionResult
        // public ActionResult Index()
        public ViewResult Index()
        {
            // Если запускать View без параметров, нужный View ищется по названию
            // return View(); // new ViewResult
            // При желании, можно указать нужный View (из папки Home)
            // return View("About");
            // или можно указать полный путь к представлению
            // return View("~/Views/Some/Index.cshtml");

            // Передача параметров в представление
            // Через ViewData
            ViewData["HeadVD"] = "Привет, мир! ViewData";
            // Через ViewBag
            ViewBag.HeadVB = "Привет, мир! ViewBag";
            ViewBag.Fruit = new List<string> { "яблоки", "груши", "вишни" };
            return View();
        }

        public ActionResult GetHtml()
        {
            return new HtmlResult("<h2>Привет, мир!</h2>");
        }

        public ActionResult GetImage()
        {
            return new ImageResult("../Content/Images/winter.jpg");
        }

        public string GetId(int id)
        {
            return id.ToString();
        }

        /*
        // Передача параметров: вариант 1
        public string Square(int a, int h)
        {
            double s = a * h / 2;
            return "<h2>Площадь треугольника с основанием " + a + " и высотой " + h + " равна " + s + "</h2>";
        }
        */

        // Передача параметров: вариант 2
        public string Square()
        {
            int a = Int32.Parse(Request.Params["a"]);
            int h = Int32.Parse(Request.Params["h"]);
            double s = a * h / 2;
            return "<h2>Площадь треугольника с основанием " + a + " и высотой " + h + " равна " + s + "</h2>";
        }

        public void GetVoid()
        {
        }

        // Демонстрация переадресации
        public ActionResult Redirect()
        {
            // Временная переадресация
            // return Redirect("/Home/Contact");
            // Постоянная переадресация
            // return RedirectPermanent("/Home/Contact");
            // Переадресация по маршруту
            // return RedirectToRoute(new { Controller = "Home", Action = "Contact" });
            // Переадресация на метод из этого контроллера
            // return RedirectToAction("Contact");
            // Переадресация на метод из другого контроллера
            // return RedirectToAction("Index", "Book");
            // Переадресация с передачей параметров
            return RedirectToAction("Square", "Home", new { a = 10, h = 12});
        }

        // Демонстрация возврата статусных кодов
        public ActionResult Status()
        {
            // return new HttpStatusCodeResult(404);
            // return new HttpNotFoundResult(); // 404
            return new HttpUnauthorizedResult(); // 401
        }

        public ActionResult ConditionalRedirect(int id)
        {
            if (id > 3)
            {
                return Redirect("/Home/Contact");
            }
            return View("About");
        }

        [HttpGet]
        public ActionResult GetBook()
        {
            return View();
        }

        /*
        // Передача параметров: вариант 1
        [HttpPost]
        public string GetBook(string title, string author)
        {
            return author + " " + title;
        }
        */

        // Передача параметров: вариант 2
        // Название GetBook уже использовать не получится, так как метод с такой сигнатурой уже есть
        /*
        [HttpPost]
        public string PostBook()
        {
            string title = Request.Form["title"];
            string author = Request.Form["author"];
            return author + " " + title;
        }
        */

        [HttpPost]
        // ContentResult унаследован от ActionResult
        // public ContentResult PostBook()
        public ActionResult PostBook()
        {
            string title = Request.Form["title"];
            string author = Request.Form["author"];
            return Content(author + " " + title);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
 