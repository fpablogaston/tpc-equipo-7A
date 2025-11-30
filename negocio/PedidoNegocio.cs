using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PedidoNegocio
    {
        public List<Pedido> Listar()
        {
            List<Pedido> list = new List<Pedido>();
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetQuery("Select Id, FechaPedido, Estado, Total, IdCliente, IdEnvio, IdPago From Pedidos");
                data.EjecutarLectura();
                while (data.Reader.Read())
                {
                    Pedido aux = new Pedido();
                    aux.Id = (int)data.Reader["Id"];
                    aux.FechaPedido = (DateTime)data.Reader["FechaPedido"];
                    aux.Estado = (string)data.Reader["Estado"];
                    aux.Total = (decimal)data.Reader["Total"];
                    aux.Cliente = new ClienteNegocio().GetById((int)data.Reader["IdCliente"]);
                    aux.Envio = new EnvioNegocio().GetById((int)data.Reader["IdEnvio"]);
                    aux.Pago = new PagoNegocio().GetById((int)data.Reader["IdPago"]);
                    list.Add(aux);
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Listar. " + ex.ToString());
                throw ex;
            }
            finally
            {
                data.CerrarConexion();
            }
        }
        public Pedido GetById(int id)
        {
            Pedido aux = new Pedido();
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetQuery("Select Id, FechaPedido, Estado, Total, IdCliente, IdEnvio, IdPago From Pedidos Where Id = @Id");
                data.SetearParametro("@Id", id);
                data.EjecutarLectura();
                while (data.Reader.Read())
                {
                    aux.Id = (int)data.Reader["Id"];
                    aux.FechaPedido = (DateTime)data.Reader["FechaPedido"];
                    aux.Estado = (string)data.Reader["Estado"];
                    aux.Total = (decimal)data.Reader["Total"];
                    aux.Cliente = new ClienteNegocio().GetById((int)data.Reader["IdCliente"]);
                    aux.Envio = new EnvioNegocio().GetById((int)data.Reader["IdEnvio"]);
                    aux.Pago = new PagoNegocio().GetById((int)data.Reader["IdPago"]);
                }
                return aux;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: GetById. " + ex.ToString());
                throw ex;
            }
            finally
            {
                data.CerrarConexion();
            }
        }

        public int Agregar(Pedido pedido)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetQuery("INSERT INTO Pedidos (FechaPedido, Estado, Total, IdCliente) " +
                              "OUTPUT INSERTED.Id VALUES (@Fecha, @Estado, @Total, @Cliente)");

                data.SetearParametro("@Fecha", pedido.FechaPedido);
                data.SetearParametro("@Estado", pedido.Estado);
                data.SetearParametro("@Total", pedido.Total);
                data.SetearParametro("@Cliente", pedido.Cliente.Id);

                return data.EjecutarScalar();
            }
            finally
            {
                data.CerrarConexion();
            }
        }
        public Pedido GuardarPedidoCompleto(Cliente cliente, Carrito carrito, Envio envio, Pago pago)
        {
            SqlConnection conexion = new SqlConnection("server=.\\SQLEXPRESS; database=Ecommerce; integrated security=true");
            conexion.Open();
            SqlTransaction transaccion = conexion.BeginTransaction();
            Pedido pedido = new Pedido();
            pedido.Envio = new Envio();
            pedido.Pago = new Pago();


            try
            {
                // 1. INSERTAR PEDIDO (Inicialmente sin Pago ni Envio)
                SqlCommand cmdPedido = new SqlCommand("INSERT INTO Pedidos (FechaPedido, Estado, Total, IdCliente) OUTPUT INSERTED.Id VALUES (@Fecha, @Estado, @Total, @IdCliente)", conexion, transaccion);
                cmdPedido.Parameters.AddWithValue("@Fecha", DateTime.Now);
                cmdPedido.Parameters.AddWithValue("@Estado", "Pendiente");
                cmdPedido.Parameters.AddWithValue("@Total", carrito.Total());
                cmdPedido.Parameters.AddWithValue("@IdCliente", cliente.Id);

                int idPedido = (int)cmdPedido.ExecuteScalar();

                pedido.Id = idPedido;
                pedido.Total = carrito.Total();
                pedido.FechaPedido = DateTime.Now;


                // 2. INSERTAR DETALLES
                foreach (var item in carrito.ListaCarrito)
                {
                    SqlCommand cmdDetalle = new SqlCommand("INSERT INTO DetallesPedido (IdPedido, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@IdPedido, @IdProducto, @Cantidad, @Precio, @Subtotal)", conexion, transaccion);
                    cmdDetalle.Parameters.AddWithValue("@IdPedido", idPedido);
                    cmdDetalle.Parameters.AddWithValue("@IdProducto", item.Producto.Id);
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@Precio", item.Producto.Precio);
                    cmdDetalle.Parameters.AddWithValue("@Subtotal", item.Subtotal);
                    cmdDetalle.ExecuteNonQuery();

                    // Restar Stock
                    SqlCommand cmdStock = new SqlCommand("UPDATE Productos SET Stock = Stock - @Cant WHERE Id = @IdProd", conexion, transaccion);
                    cmdStock.Parameters.AddWithValue("@Cant", item.Cantidad);
                    cmdStock.Parameters.AddWithValue("@IdProd", item.Producto.Id);
                    cmdStock.ExecuteNonQuery();
                }

                // 3. INSERTAR ENVIO
                SqlCommand cmdEnvio = new SqlCommand("INSERT INTO Envios (DireccionEnvio, Ciudad, Provincia, CodigoPostal, FechaEnvio, Estado, IdPedido, IdEstadoEnvio) OUTPUT INSERTED.Id VALUES (@Dir, @Ciudad, 'Provincia', '0000', @Fecha, 'Pendiente', @IdPedido, 1)", conexion, transaccion);
                cmdEnvio.Parameters.AddWithValue("@Dir", envio.DireccionEnvio);
                cmdEnvio.Parameters.AddWithValue("@Ciudad", envio.Ciudad); // Usando Ciudad como localidad
                cmdEnvio.Parameters.AddWithValue("@Fecha", DateTime.Now.AddDays(1)); // Fecha estimada mañana
                cmdEnvio.Parameters.AddWithValue("@IdPedido", idPedido);

                int idEnvio = (int)cmdEnvio.ExecuteScalar();
                pedido.Envio.Id = idEnvio;

                // 4. INSERTAR PAGO
                SqlCommand cmdPago = new SqlCommand("INSERT INTO Pagos (MetodoPago, Estado, Monto, FechaPago, IdPedido) OUTPUT INSERTED.Id VALUES (@Metodo, 'Aprobado', @Monto, @Fecha, @IdPedido)", conexion, transaccion);
                cmdPago.Parameters.AddWithValue("@Metodo", pago.MetodoPago.Nombre);
                cmdPago.Parameters.AddWithValue("@Monto", carrito.Total());
                cmdPago.Parameters.AddWithValue("@Fecha", DateTime.Now);
                cmdPago.Parameters.AddWithValue("@IdPedido", idPedido);

                int idPago = (int)cmdPago.ExecuteScalar();
                pedido.Pago.Id = idPago;

                // 5. ACTUALIZAR PEDIDO
                SqlCommand cmdUpdate = new SqlCommand("UPDATE Pedidos SET IdEnvio = @IdEnvio, IdPago = @IdPago WHERE Id = @IdPedido", conexion, transaccion);
                cmdUpdate.Parameters.AddWithValue("@IdEnvio", idEnvio);
                cmdUpdate.Parameters.AddWithValue("@IdPago", idPago);
                cmdUpdate.Parameters.AddWithValue("@IdPedido", idPedido);
                cmdUpdate.ExecuteNonQuery();

                transaccion.Commit();
                return pedido;
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        public void Actualizar(Pedido pedido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetQuery("UPDATE Pedidos SET " +
                              "FechaPedido = @Fecha, " +
                              "Estado = @Estado, " +
                              "Total = @Total, " +
                              "IdCliente = @Cliente, " +
                              "IdEnvio = @Envio, " +
                              "IdPago = @Pago " +
                              "WHERE Id = @Id");

                datos.SetearParametro("@Fecha", pedido.FechaPedido);
                datos.SetearParametro("@Estado", pedido.Estado);
                datos.SetearParametro("@Total", pedido.Total);
                datos.SetearParametro("@Cliente", pedido.Cliente.Id);
                datos.SetearParametro("@Envio", pedido.Envio.Id);
                datos.SetearParametro("@Pago", pedido.Pago.Id);
                datos.SetearParametro("@Id", pedido.Id);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<Pedido> ListarPorCliente(int idCliente)
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.SetQuery("SELECT Id, FechaPedido, Estado, Total, IdCliente, IdEnvio, IdPago FROM Pedidos WHERE IdCliente = @IdCliente");
                data.SetearParametro("@IdCliente", idCliente);
                data.EjecutarLectura();

                while (data.Reader.Read())
                {
                    Pedido aux = new Pedido();
                    aux.Id = (int)data.Reader["Id"];
                    aux.FechaPedido = (DateTime)data.Reader["FechaPedido"];
                    aux.Estado = (string)data.Reader["Estado"];
                    aux.Total = (decimal)data.Reader["Total"];

                    aux.Cliente = new Cliente { Id = idCliente };
                    if (data.Reader["IdEnvio"] != DBNull.Value)
                    {
                        aux.Envio = new EnvioNegocio().GetById((int)data.Reader["IdEnvio"]);
                    }
                    if (data.Reader["IdPago"] != DBNull.Value)
                    {
                        aux.Pago = new PagoNegocio().GetById((int)data.Reader["IdPago"]);
                    }

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { data.CerrarConexion(); }
        }
    }
}