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
        public string Estado { get; set; }
        public Pago Pago { get; set; }
        public Envio Envio { get; set; }
        public List<DetallesPedido> Detalles { get; set; }
    }
}
