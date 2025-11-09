using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class EnvioNegocio
    {
        private List<Envio> Envios = new List<Envio>();
        public Envio GetById(int id)
        {
            return null;
            // Falta crear logica para obtener un envio por su ID
        }
        public void CrearEnvio(Envio envio)
        {
            // Falta crear logica para programar el envio
        }
        public void ActualizarEstado(int id, string nuevoEstado)
        {
            // Falta crear logica para actualizar el estado del envio
        }
        public List<Envio> Listar()
        {
            return Envios;
        }

        public Envio Buscar(int id)
        {
            return null;
            // Falta crear logica para buscar un envio por su ID
        }
    }
}
