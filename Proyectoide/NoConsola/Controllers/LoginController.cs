﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;

namespace NoConsola.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session["usuarioLogueado"] = null;
            return View("Index");
        }
        public ActionResult PerformLogin(string username, string password)
        {
            Usuario usu = Administradora.Instancia.BuscarUsuario(username, password);
            if (usu == null)
            {
                ViewBag.ErrMsg = "Este usuario no existe";
                return View("Index");
            }
            else {
                Session["usuarioLogueado"] = usu;
                if (usu.Tipo == Usuario.EnumTipoUsuario.ADMINISTRADOR)
                {
                    return RedirectToAction("Index", "Producto");
                }
                else {
                    return RedirectToAction("Index", "Home");
                }
                
            }

        }
    }
}