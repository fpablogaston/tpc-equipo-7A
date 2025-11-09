using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class ElementoCarrito
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public int CodigoProducto
        {
            get
            {
                return Producto != null ? Producto.Id : -1;
            }
        }
        public decimal PrecioTotal
        {
            get
            {
                return Producto != null ? Producto.Precio * Cantidad : 0;
            }
        }
        public decimal Costo(Producto producto)
        {
            if (Cantidad > 0) 
                return producto.Precio * Cantidad;
            else 
                return 0;
        }
    }
}
