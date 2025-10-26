using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string ImagenUrl { get; set; }
        public Categoria Categoria { get; set; }

        public override string ToString()
        {
            return $"{Nombre} - {Descripcion} - ${Precio} - Stock: {Stock}";
        }
        public void AgregarProducto()
        {
            // Falta crear logica para agregar producto
        }
        public void ActualizarProducto()
        {
            // Falta crear logica para actualizar producto
        }
        public void EliminarProducto()
        {
            // Falta crear logica para eliminar producto
        }

    }
}
