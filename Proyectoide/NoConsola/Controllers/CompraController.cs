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
                Session["precioCarrito"] = 0.0;
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

        public ActionResult CarritoModificarCantidad(int cantidad, int id)
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<CantidadProducto> listaCompra = (List<CantidadProducto>)Session["carrito"];
            foreach (CantidadProducto cantPrd in listaCompra)
            {
                if (cantPrd.Producto.ID == id)
                {
                    cantPrd.Cantidad = cantidad;
                    return View("ComprarProductos");
                }
            }
            return View("ComprarProductos");
        }

        public ActionResult CarritoEliminarProducto(int id)
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<CantidadProducto> listaCompra = (List<CantidadProducto>)Session["carrito"];
            foreach (CantidadProducto cantPrd in listaCompra)
            {
                if (cantPrd.Producto.ID == id)
                {
                    listaCompra.Remove(cantPrd);
                    return View("ComprarProductos");
                }
            }
            return View("ComprarProductos");
        }

        public ActionResult FinalizarCompra()
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult AgregarCompra(int formaDePago, int tipoEntrega)
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Compra nuevaC = (Compra)Session["compra"];

            if (formaDePago == 1)
                nuevaC.FormaDePago = Compra.EnumFormaPago.TARJETA;
            else
                nuevaC.FormaDePago = Compra.EnumFormaPago.EFECTIVO;

            if (tipoEntrega == 1)
                nuevaC.TipoEntrega = Compra.EnumTipoEntrega.RETIROLOCAL;
            else
                nuevaC.TipoEntrega = Compra.EnumTipoEntrega.DOMICILIO;

            nuevaC.Fecha = DateTime.Now;
            Usuario usuLogueado = (Usuario)Session["usuarioLogueado"];
            Compra c = new Compra(nuevaC.Productos, nuevaC.Cliente, nuevaC.Fecha, nuevaC.FormaDePago, nuevaC.TipoEntrega);
            usuLogueado.Cliente.Compras.Add(c);
            List<CantidadProducto> listaAux = (List<CantidadProducto>)Session["carrito"];
            listaAux.RemoveRange(0, listaAux.Count);            
            Session["compra"] = null;
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