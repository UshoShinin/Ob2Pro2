using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoConsola.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            if (Session["usuarioLogueado"] == null)
                return RedirectToAction("Index", "Login");
            return View();
        }

        public ActionResult Agregar()
        {
            if (Session["usuarioLogueado"] == null)
                return RedirectToAction("Index", "Login");

            return View();

        }
    }
}