﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllersBasics.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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
        [HttpPost]
        public string PostBook()
        {
            string title = Request.Form["title"];
            string author = Request.Form["author"];
            return author + " " + title;
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
 