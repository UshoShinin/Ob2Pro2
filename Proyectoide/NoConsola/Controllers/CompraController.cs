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

        public ActionResult AgregarAlCarrito(int producto, int cantidad)
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Producto p = Administradora.Instancia.BuscarProducto(producto);
            List<CantidadProducto> listaCompra = (List<CantidadProducto>)Session["carrito"];
            foreach (CantidadProducto cantPrd in listaCompra)
            {
                if(cantPrd.Producto.ID == p.ID)
                {
                    cantPrd.Cantidad += cantidad;
                    return View("ComprarProductos");
                }                
            }
            listaCompra.Add(new CantidadProducto(cantidad, p));
            return View("ComprarProductos");
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