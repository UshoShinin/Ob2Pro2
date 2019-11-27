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
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult ComprarProductos()
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (Session["carrito"] == null)
            {
                Session["carrito"] = new List<CantidadProducto>();
            }            
            return View();
        }

        public ActionResult AgregarAlCarrito(int id, int cantidad)
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Producto p = Administradora.Instancia.BuscarProducto(id);
            return View();
        }

        public ActionResult FiltrarCompras(DateTime FI, DateTime FF)
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (FI > FF)
            {
                DateTime Aux = FI;
                FI = FF;
                FF = Aux;
            }
            List<Compra> Compras = new List<Compra>();
            foreach(Compra C in Administradora.Instancia.Compras)
            {
                if(C.Fecha>= FI && C.Fecha <= FF)
                {
                    Compras.Add(C);
                }
            }
            Compras.Sort();
            return View("Index", Compras);
        }
    }
}