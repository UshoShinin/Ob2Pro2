using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        #region EnumDeclaration
        public enum EnumTipoUsuario
        {
            ADMINISTRADOR = 1,
            CLIENTE,
            VISITANTE
        }
        #endregion

        private string username;
        private string password;
        private EnumTipoUsuario tipo;
        private Cliente cliente;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public EnumTipoUsuario Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
        public Usuario(string username, string password, EnumTipoUsuario tipo)
        {
            this.username = username;
            this.password = password;
            this.tipo = tipo;
        }


    }
}
