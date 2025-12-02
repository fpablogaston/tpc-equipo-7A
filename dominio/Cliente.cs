using System;
using System.Collections.Generic;

namespace dominio
{
    public class Cliente
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public int Rol { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public List<Direccion> Direcciones { get; set; }
        public Direccion DireccionSeleccionada { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Cliente()
        {
            Direcciones = new List<Direccion>();
            DireccionSeleccionada = new Direccion();
        }
    }
}