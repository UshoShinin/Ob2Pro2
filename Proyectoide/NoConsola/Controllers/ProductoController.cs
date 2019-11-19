using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;

namespace NoConsola.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            if (Session["usuarioLogueado"] == null) {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult Agregar(string Nombre, string Desc, string Exclusivo, int Cat, double? Precio)
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (Precio == null) {
                ViewBag.ErrMsg = "El precio debe ser numérico";
                return View("Index");

            }
            else {
                Producto p;
                if (Exclusivo == "S")
                {
                    p = new Producto(Nombre, Desc, true, (Producto.EnumCategoria)Cat, Precio.Value);
                }
                else {
                    p = new Producto(Nombre, Desc, false, (Producto.EnumCategoria)Cat, Precio.Value);
                }
                
                Administradora.Instancia.AgregarProducto(p);
            }
            
            return View("Index");
        }

        public ActionResult Modificar(int id)
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Producto p = Administradora.Instancia.BuscarProducto(id);
            return View("Index", p);
        }


    }
}