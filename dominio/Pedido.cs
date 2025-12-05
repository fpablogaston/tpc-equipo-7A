using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pedido
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime FechaPedido { get; set; }
        public decimal Total { get; set; }
        public EstadoPedido Estado { get; set; }
        public Pago Pago { get; set; }
        public Envio Envio { get; set; }
        public List<DetallesPedido> Detalles { get; set; }
        public bool EsRetiroEnTienda
        {
            get
            {
                return Envio != null && Envio.IdEstadoEnvio == 6;
            }
        }
        public bool EsPagoEfectivo
        {
            get
            {
                if (Pago?.MetodoPago == null)
                    return false;

                string nombre = Pago.MetodoPago.Nombre?.ToLower() ?? "";

                return Pago.MetodoPago.Id == 2 || nombre.Contains("efectivo");
            }
        }
    }
}