﻿using ControllersBasics.Util;
using System;
using System.Collections.Generic;
using System.IO;
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
            // Записываем Cookie в браузере клиента
            HttpContext.Response.Cookies["id"].Value = "ca-4353w";
            // Устанавливаем значение переменной сессии
            Session["name"] = "Tom";
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

        // Отправляет файл клиенту (браузер загружает файл)
        public ActionResult GetFile()
        {
            // Путь к файлу
            string file_path = Server.MapPath("~/Files/test.pdf");
            // Тип файла - content-type
            // string file_type = "application/pdf";
            // Универсальный content-type - подходит для файла любого типа
            string file_type = "application/octet-stream";
            // Имя файла - не обязательно
            string file_name = "test.pdf";
            return File(file_path, file_type, file_name);
        }

        // Отправляет файл клиенту (браузер загружает файл)
        public ActionResult GetBytes()
        {
            // Путь к файлу
            string file_path = Server.MapPath("~/Files/test.pdf");
            byte[] buffer = System.IO.File.ReadAllBytes(file_path);
            string file_type = "application/pdf";
            string file_name = "test.pdf";
            return File(buffer, file_type, file_name);
        }

        // Отправляет файл клиенту (браузер загружает файл)
        public ActionResult GetStream()
        {
            // Путь к файлу
            string file_path = Server.MapPath("~/Files/test.pdf");
            FileStream fs = new FileStream(file_path, FileMode.Open);
            string file_type = "application/pdf";
            string file_name = "test.pdf";
            return File(fs, file_type, file_name);
        }

        public string GetContext()
        {
            // Глобальный объект Response и свойство HttpContext.Response ссылаются на один и тот же объект
            // HttpContext.Response.Write("Привет, мир!");
            Response.Write("Привет, мир!");
            // string browser = HttpContext.Request.Browser.Browser;
            // Глобальный объект Request и свойство HttpContext.Request ссылаются на один и тот же объект
            string browser = Request.Browser.Browser;
            string userAgent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string referer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
            string cookieVarId = HttpContext.Request.Cookies["id"].Value;
            string sessionVarName = Session["name"] == null ? "" : Session["name"].ToString();
            // Чтобы удалить переменную сессии нужно присвоить ей null
            Session["name"] = null;
            return String.Format(
                $"<p>Browser: {browser}</p>" +
                $"<p>User-Agent: {userAgent}</p>" +
                $"<p>URL: {url}</p>" +
                $"<p>IP: {ip}</p>" +
                $"<p>Referer: {referer}</p>" +
                $"<p>Cookie var id: {cookieVarId}</p>" +
                $"<p>Session var Name: {sessionVarName}</p>"
            );
        }
    }
}
 