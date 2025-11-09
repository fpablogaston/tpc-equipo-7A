using dominio;
using negocio;
using System;
using System.Collections.Generic;

namespace negocio
{
    public class DetallesPedidoNegocio
    {
        public List<DetallesPedido> ToList()
        {
            List<DetallesPedido> list = new List<DetallesPedido>();
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetQuery("Select Id, IdPedido, IdProducto, Cantidad, PrecioUnitario, Subtotal From DetallesPedido");
                data.EjecutarLectura();
                while (data.Reader.Read())
                {
                    DetallesPedido aux = new DetallesPedido();
                    aux.Id = (int)data.Reader["Id"];
                    aux.IdPedido = (int)data.Reader["IdPedido"];
                    aux.Producto = new ProductoNegocio().GetById((int)data.Reader["IdProducto"]);
                    aux.Cantidad = (int)data.Reader["Cantidad"];
                    aux.PrecioUnitario = (decimal)data.Reader["PrecioUnitario"];
                    aux.Subtotal = (decimal)data.Reader["Subtotal"];
                    list.Add(aux);
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
                throw ex;
            }
            finally
            {
                data.CerrarConexion();
            }
        }

        public List<DetallesPedido> GetByIdPedido(int IdPedido)
        {
            List<DetallesPedido> list = new List<DetallesPedido>();
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetQuery("select Id, IdPedido, IdProducto, Cantidad, PrecioUnitario, Subtotal from DETALLES_PEDIDO where IdPedido = @IdPedido");
                data.SetearParametro("@IdPedido", IdPedido);
                data.EjecutarLectura();
                while (data.Reader.Read())
                {
                    DetallesPedido aux = new DetallesPedido();
                    aux.Id = (int)data.Reader["Id"];
                    aux.IdPedido = (int)data.Reader["IdPedido"];
                    aux.Producto = new ProductoNegocio().GetById((int)data.Reader["IdProducto"]);
                    aux.Cantidad = (int)data.Reader["Cantidad"];
                    aux.PrecioUnitario = (decimal)data.Reader["PrecioUnitario"];
                    aux.Subtotal = (decimal)data.Reader["Subtotal"];
                    list.Add(aux);
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
                throw ex;
            }
            finally
            {
                data.CerrarConexion();
            }
        }
    }
}
