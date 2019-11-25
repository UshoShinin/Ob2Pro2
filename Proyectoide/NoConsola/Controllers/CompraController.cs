using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;

namespace NoConsola.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ComprarProductos()
        {            
            return View();
        }

        public ActionResult AgregarAlCarrito(int id, int cantidad)
        {
            Producto p = Administradora.Instancia.BuscarProducto(id);
            return View();
        }
    }
}