using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class DetallePedido
    {
        public int id { get; set; }
        public Producto producto { get; set; }
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal subtotal 
        { 
            get 
            { 
                return cantidad * precioUnitario; 
            }
        }
    }
}
