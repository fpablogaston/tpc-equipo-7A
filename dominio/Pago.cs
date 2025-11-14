using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pago
    {
        public int Id { get; set; }
        ///metodopago ya no es string igual que estado
        public MetodoPago MetodoPago { get; set; }
        public EstadoPago Estado { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public int IdPedido { get; set; }
    }
}
