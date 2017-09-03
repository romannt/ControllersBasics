using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ControllersBasics.Controllers
{
    /*
     * Реализуем свой контроллер путём реализации интерфейса IController.
     * Для вызова нужно обратиться по пути My в командной строке браузера.
     */
    public class MyController : IController
    {
        public void Execute(RequestContext requestContext)
        {
            string ip = requestContext.HttpContext.Request.UserHostAddress;
            var response = requestContext.HttpContext.Response;
            response.Write("<h2>Ваш IP-адрес: " + ip + "</h2>");
        }
    }
}