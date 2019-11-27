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
            if (Administradora.Instancia.BuscarClienteComun(Email) != null) {
                ViewBag.ErrMsg = "Este Email ya está en uso";
                return View("Index");
            }
            if (Cedula == null)
            {
                ViewBag.ErrMsg = "La cédula debe ser un valor numérico sin - o .";
                return View("Index");
            }
            if (Celular == null)
            {
                ViewBag.ErrMsg = "El celular debe ser un valor numerico";
                return View("Index");
            }
            Usuario u = new Usuario(username, password, Usuario.EnumTipoUsuario.CLIENTE);
            ClienteComun CC = new ClienteComun(new List<Compra> { }, Nombre, Email, Fecha, (Cliente.EnumProcedencia)Procedencia, Direccion, u, Cedula.Value,Celular.Value);
            u.Cliente = CC;
            Administradora.Instancia.Clientes.Add(CC);
            return View("Index");

        }

        public ActionResult AgregarEmpresa(string Nombre, string Email, DateTime Fecha, int Procedencia, string Direccion, string razonSocial, long? rut, double? Descuento, string username, string password)
        {
            if (rut == null)
            {
                ViewBag.ErrMsg = "El rut debe ser un valor numérico";
                return View("Index");
            }
            if (Descuento == null)
            {
                ViewBag.ErrMsg = "El descuento debe ser un valor numérico";
                return View("Index");
            }
            Usuario U = new Usuario(username, password,Usuario.EnumTipoUsuario.CLIENTE);
            ClienteEmpresa CE = new ClienteEmpresa(new List<Compra>(), Nombre, Email, Fecha, (Cliente.EnumProcedencia)Procedencia, Direccion, U, razonSocial, rut.Value, Descuento.Value);
            U.Cliente = CE;
            Administradora.Instancia.AgregarCliente(CE);
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

        public ActionResult RealizarModificacionComun(string Nombre,DateTime Fecha, int Procedencia,string Direccion,int? Cedula, int? Celular, string username, string password,string Email)
        {
            if (Cedula == null)
            {
                return View("Index");
            }
            if (Celular == null)
            {
                return View("Index");
            }
            ClienteComun CC = Administradora.Instancia.BuscarClienteComun(Email);
            CC.Nombre = Nombre;
            CC.Fecha = Fecha;
            CC.Procedencia = (Cliente.EnumProcedencia)Procedencia;
            CC.Direccion = Direccion;
            CC.Cedula = Cedula.Value;
            CC.Celular = Celular.Value;
            CC.Usuario.Username = username;
            CC.Usuario.Password = password;
            return View("Index");
        }

        public ActionResult RealizarModificacionEmpresa(string Nombre, DateTime Fecha, int Procedencia, string Direccion, string razonSocial, long? rut, double? Descuento, string username, string password, string Email)
        {
            if (rut == null)
            {
                ViewBag.ErrMsg = "El rut debe ser un valor numérico";
                return View("Index");
            }
            if (Descuento == null)
            {
                ViewBag.ErrMsg = "El descuento debe ser un valor numérico";
                return View("Index");
            }
            ClienteEmpresa CE = Administradora.Instancia.BuscarClienteEmpresa(Email);
            CE.Nombre = Nombre;
            CE.Fecha = Fecha;
            CE.Procedencia = (Cliente.EnumProcedencia)Procedencia;
            CE.Direccion = Direccion;
            CE.RazonSocial = razonSocial;
            CE.Rut = rut.Value;
            CE.Descuento = (Descuento.Value)/100;
            CE.Usuario.Username = username;
            CE.Usuario.Password = password;
            return View("Index");
        }

        public ActionResult Eliminar(string Email)
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Administradora.Instancia.EliminarCliente(Email);
            return View("Index");
        }
    }
}