using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;

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

        public ActionResult AgregarComun(string Nombre,string Email,DateTime Fecha,int Procedencia,string Direccion,int? Cedula, int? Celular,string username, string password)
        {
            if (Session["usuarioLogueado"] == null)
                return RedirectToAction("Index", "Login");
            if (Cedula == null)
            {
                ViewBag.ErrMsg = "La cédula no puede ser un valor vacío";
                return View("Index");
            }
            if (Celular == null)
            {
                ViewBag.ErrMsg = "El celular no puede ser un valor vacío";
                return View("Index");
            }
            Usuario u = new Usuario(username, password, Usuario.EnumTipoUsuario.CLIENTE);
            ClienteComun CC = new ClienteComun(new List<Compra> { }, Nombre, Email, Fecha, (Cliente.EnumProcedencia)Procedencia, Direccion, u, Cedula.Value,Celular.Value);
            u.Cliente = CC;
            Administradora.Instancia.Clientes.Add(CC);
            return View("Index");

        }

        public ActionResult ModificarClienteComun(string Email)
        {
            if (Session["usuarioLogueado"] == null)
                return RedirectToAction("Index", "Login");
            ClienteComun C = Administradora.Instancia.BuscarClienteComun(Email);
            return View("Index",C);
        }

        public ActionResult ModificarClienteEmpresa(string Email)
        {
            if (Session["usuarioLogueado"] == null)
                return RedirectToAction("Index", "Login");
            ClienteEmpresa C = Administradora.Instancia.BuscarClienteEmpresa(Email);
            return View("Index", C);
        }
    }
}