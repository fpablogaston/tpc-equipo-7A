using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ClienteNegocio
    {
       
        private List<Cliente> clientes = new List<Cliente>();
        public void Registrar()
        {
            // Falta crear logica para registrar al cliente
        }

        public void IniciarSesion(string email, string contraseña)
        {
            // Falta crear logica para iniciar sesion
        }

        public void ActualizarDatos()
        {
            // Falta crear logica para actualizar datos del cliente
        }

        public List<Cliente> listar()
        {
            return clientes;
        }

    }
}
