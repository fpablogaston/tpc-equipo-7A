using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class PagoNegocio
    {
        private List<Pago> Pagos = new List<Pago>();
        public Pago GetById(int id)
        {
            return null;
            // Falta crear logica para obtener un pago por su ID
        }
        public void ProcesarPago(Pago pago)
        {
            // Falta crear logica para procesar el pago
        }

        public void CambiarEstado(int id, string nuevoEstado)
        {
            // Falta crear logica para cambiar el estado del pago
        }
        public List<Pago> Listar()
        {
            return Pagos;
        }       
    }
}
