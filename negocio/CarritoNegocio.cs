using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dominio;

namespace negocio
{
    public class CarritoNegocio
    {
        private const string SESSION_KEY = "carrito";

        public Carrito ObtenerCarrito()
        {
            var session = HttpContext.Current.Session;

            if (session[SESSION_KEY] == null)
            {
                session[SESSION_KEY] = new Carrito();
            }

            return (Carrito)session[SESSION_KEY];
        }

        private void GuardarCarrito(Carrito carrito)
        {
            HttpContext.Current.Session[SESSION_KEY] = carrito;
        }

        public void Agregar(int idProducto, int cantidad = 1)
        {
            if (cantidad <= 0) cantidad = 1;

            ProductoNegocio prodNeg = new ProductoNegocio();
            Producto prod = prodNeg.Listar().FirstOrDefault(p => p.Id == idProducto);

            if (prod == null)
                throw new InvalidOperationException("El producto no existe.");

            Carrito carrito = ObtenerCarrito();
            carrito.AgregarProducto(prod, cantidad);
            GuardarCarrito(carrito);
        }

        public void ModificarCantidad(int idProducto, int nuevaCantidad)
        {
            if (nuevaCantidad < 0) nuevaCantidad = 0;

            Carrito carrito = ObtenerCarrito();
            carrito.ModificarCantidad(idProducto, nuevaCantidad);
            GuardarCarrito(carrito);
        }

        public void Eliminar(int idProducto)
        {
            Carrito carrito = ObtenerCarrito();
            carrito.ListaCarrito.RemoveAll(x => x.Producto?.Id == idProducto);
            GuardarCarrito(carrito);
        }

        public void Vaciar()
        {
            Carrito carrito = ObtenerCarrito();
            carrito.Vaciar();
            GuardarCarrito(carrito);
        }

        public List<ElementoCarrito> ObtenerItems()
        {
            return ObtenerCarrito().ListaCarrito ?? new List<ElementoCarrito>();
        }

        public decimal Total()
        {
            return ObtenerCarrito().Total();
        }

        public int TotalItems()
        {
            return ObtenerCarrito().ListaCarrito?.Sum(x => x.Cantidad) ?? 0;
        }
    }
}
