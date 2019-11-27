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
            bool Exclusividad;
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (Precio == null) {
                ViewBag.ErrMsg = "El precio debe ser numérico";
                return View("Index");

            }
            else {
                
                if (Exclusivo == "S")
                {
                    Exclusividad = true;
                }
                else {
                    Exclusividad = false;
                }
                Administradora.Instancia.AgregarProducto(new Producto(Nombre, Desc, Exclusividad, (Producto.EnumCategoria)Cat, Precio.Value));
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

        public ActionResult RealizarModificacion(int id, string Nombre, string Desc, string Exclusivo, int Cat, double? Precio)
        {
            bool Exclusividad;
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (Precio == null)
            {
                ViewBag.ErrMsg = "El precio debe ser numérico";
                return View("Index");

            }
            else
            {

                if (Exclusivo == "S")
                {
                    Exclusividad = true;
                }
                else
                {
                    Exclusividad = false;
                }
                Producto p = Administradora.Instancia.BuscarProducto(id);
                p.Nombre = Nombre;
                p.Descripcion = Desc;
                p.Exclusivo = Exclusividad;
                p.Categoria = (Producto.EnumCategoria)Cat;
                p.Precio = Precio.Value;
            }

            return View("Index");
        }

        public ActionResult Eliminar(int id)
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Administradora.Instancia.EliminarProducto(id);
            return View("Index");
        }

        public ActionResult UltimosProductosComprados()
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult MasComprado()
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {             
                return View();
            }
        }
    }
}