using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ProductoNegocio
    {
        private List<Producto> productos = new List<Producto>();
        public void Agregar(Producto producto)
        {
            //Falta crear logica para agregar producto
        }
        public void Actualizar(Producto producto)
        {
            // Falta crear logica para actualizar producto
        }
        public void Eliminar(int id)
        {
            // Falta crear logica para eliminar producto
        }
        public List<Producto> listar()
        {
            return productos;
        }
    }

}
