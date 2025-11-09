using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Carrito
    {
        public List<ElementoCarrito> ListaCarrito;
        public ElementoCarrito ElementoCarrito = null;
        public Carrito()
        {
            ListaCarrito = new List<ElementoCarrito>();
        }
        public List<ElementoCarrito> AgregarProducto(Producto producto, int cantidad)
        {
            ElementoCarrito = ListaCarrito.Find(item => item.Producto.Id == producto.Id);
            if (ElementoCarrito != null)
            {
                if (ElementoCarrito.Cantidad > 0)
                    ElementoCarrito.Cantidad += cantidad;

                return ListaCarrito;
            }
            else
            {
                ElementoCarrito elemento = new ElementoCarrito();
                elemento.Producto = producto;
                elemento.Cantidad = cantidad;
                ListaCarrito.Add(elemento);

                return ListaCarrito;
            }
        }
        public void ModificarCantidad(int id, int cantidad)
        {
            ElementoCarrito = ListaCarrito.Find(item => item.Producto.Id == id);
            if (ElementoCarrito != null)
                ElementoCarrito.Cantidad = cantidad;
        }
    }
}
