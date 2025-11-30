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
                data.SetQuery(@"
                    SELECT P.Id, P.FechaPedido, P.Total, P.IdCliente, 
                           P.IdEstadoPedido, EP.Descripcion as EstadoDescripcion,
                           P.IdEnvio, P.IdPago,
                           C.Nombre as ClienteNombre, C.Apellido as ClienteApellido,
                           PA.MetodoPago as MetodoPagoStr, PA.Estado as EstadoPago,
                           E.IdEstadoEnvio as EstadoEnvioId, EE.Descripcion as EstadoEnvioDesc
                    FROM Pedidos P
                    INNER JOIN EstadoPedido EP ON P.IdEstadoPedido = EP.Id
                    INNER JOIN Clientes C ON P.IdCliente = C.Id
                    LEFT JOIN Pagos PA ON P.IdPago = PA.Id
                    LEFT JOIN Envios E ON P.IdEnvio = E.Id
                    LEFT JOIN EstadoEnvio EE ON E.IdEstadoEnvio = EE.Id
                ");

                data.EjecutarLectura();
                while (data.Reader.Read())
                {
                    Pedido aux = new Pedido();
                    aux.Id = (int)data.Reader["Id"];
                    aux.FechaPedido = (DateTime)data.Reader["FechaPedido"];
                    aux.Total = (decimal)data.Reader["Total"];

                    aux.Estado = new EstadoPedido();
                    aux.Estado.Id = (int)data.Reader["IdEstadoPedido"];
                    aux.Estado.Descripcion = (string)data.Reader["EstadoDescripcion"];

                    aux.Cliente = new Cliente();
                    aux.Cliente.Id = (int)data.Reader["IdCliente"];
                    aux.Cliente.Nombre = (string)data.Reader["ClienteNombre"];
                    aux.Cliente.Apellido = (string)data.Reader["ClienteApellido"];

                    aux.Envio = new Envio();
                    if (data.Reader["IdEnvio"] != DBNull.Value)
                    {
                        aux.Envio.Id = (int)data.Reader["IdEnvio"];
                        aux.Envio.IdEstadoEnvio = data.Reader["EstadoEnvioId"] != DBNull.Value ? (int)data.Reader["EstadoEnvioId"] : 0;
                        aux.Envio.EstadoDescripcion = data.Reader["EstadoEnvioDesc"] != DBNull.Value ? (string)data.Reader["EstadoEnvioDesc"] : "";
                    }

                    aux.Pago = new Pago();
                    if (data.Reader["IdPago"] != DBNull.Value)
                    {
                        aux.Pago.Id = (int)data.Reader["IdPago"];
                        aux.Pago.MetodoPago = new MetodoPago { Nombre = (string)data.Reader["MetodoPagoStr"] };
                        aux.Pago.Estado = new EstadoPago { Nombre = (string)data.Reader["EstadoPago"] };
                        if (aux.Pago.MetodoPago.Nombre.ToLower().Contains("efectivo")) aux.Pago.MetodoPago.Id = 2;
                    }

                    list.Add(aux);
                }
                return list;
            }
            catch (Exception ex)
            {
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
                data.SetQuery(@"
                    SELECT P.Id, P.FechaPedido, P.Total, P.IdCliente, 
                           P.IdEstadoPedido, EP.Descripcion as EstadoDescripcion,
                           P.IdEnvio, P.IdPago 
                    FROM Pedidos P
                    INNER JOIN EstadoPedido EP ON P.IdEstadoPedido = EP.Id
                    WHERE P.Id = @Id");

                data.SetearParametro("@Id", id);
                data.EjecutarLectura();
                while (data.Reader.Read())
                {
                    aux.Id = (int)data.Reader["Id"];
                    aux.FechaPedido = (DateTime)data.Reader["FechaPedido"];
                    aux.Total = (decimal)data.Reader["Total"];

                    aux.Estado = new EstadoPedido();
                    aux.Estado.Id = (int)data.Reader["IdEstadoPedido"];
                    aux.Estado.Descripcion = (string)data.Reader["EstadoDescripcion"];

                    aux.Cliente = new ClienteNegocio().GetById((int)data.Reader["IdCliente"]);

                    if (data.Reader["IdEnvio"] != DBNull.Value)
                        aux.Envio = new EnvioNegocio().GetById((int)data.Reader["IdEnvio"]);

                    if (data.Reader["IdPago"] != DBNull.Value)
                        aux.Pago = new PagoNegocio().GetById((int)data.Reader["IdPago"]);
                }
                return aux;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.CerrarConexion();
            }
        }

        public void ActualizarEstado(int idPedido, int idEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetQuery("UPDATE Pedidos SET IdEstadoPedido = @IdEstado WHERE Id = @Id");
                datos.SetearParametro("@IdEstado", idEstado);
                datos.SetearParametro("@Id", idPedido);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<EstadoPedido> ListarEstados()
        {
            List<EstadoPedido> list = new List<EstadoPedido>();
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetQuery("SELECT Id, Descripcion FROM EstadoPedido");
                data.EjecutarLectura();
                while (data.Reader.Read())
                {
                    list.Add(new EstadoPedido
                    {
                        Id = (int)data.Reader["Id"],
                        Descripcion = (string)data.Reader["Descripcion"]
                    });
                }
                return list;
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
                // 1. INSERTAR PEDIDO (Default State: 1 - Pendiente de Pago)
                SqlCommand cmdPedido = new SqlCommand("INSERT INTO Pedidos (FechaPedido, IdEstadoPedido, Total, IdCliente) OUTPUT INSERTED.Id VALUES (@Fecha, @IdEstado, @Total, @IdCliente)", conexion, transaccion);
                cmdPedido.Parameters.AddWithValue("@Fecha", DateTime.Now);
                cmdPedido.Parameters.AddWithValue("@IdEstado", 1); // Pendiente
                cmdPedido.Parameters.AddWithValue("@Total", carrito.Total());
                cmdPedido.Parameters.AddWithValue("@IdCliente", cliente.Id);

                int idPedido = (int)cmdPedido.ExecuteScalar();
                pedido.Id = idPedido;

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
                SqlCommand cmdEnvio = new SqlCommand("INSERT INTO Envios (DireccionEnvio, Ciudad, Provincia, CodigoPostal, IdEstadoEnvio, IdPedido) OUTPUT INSERTED.Id VALUES (@DireccionEnvio, @Ciudad, @Provincia, @CodigoPostal, @IdEstadoEnvio, @IdPedido)", conexion, transaccion);
                cmdEnvio.Parameters.AddWithValue("@DireccionEnvio", envio.DireccionEnvio);
                cmdEnvio.Parameters.AddWithValue("@Ciudad", envio.Ciudad);
                cmdEnvio.Parameters.AddWithValue("@Provincia", envio.Provincia);
                cmdEnvio.Parameters.AddWithValue("@CodigoPostal", envio.CodigoPostal);
                cmdEnvio.Parameters.AddWithValue("@IdEstadoEnvio", 1);
                cmdEnvio.Parameters.AddWithValue("@IdPedido", idPedido);

                int idEnvio = (int)cmdEnvio.ExecuteScalar();
                pedido.Envio.Id = idEnvio;

                // 4. INSERTAR PAGO
                SqlCommand cmdPago = new SqlCommand("INSERT INTO Pagos (MetodoPago, Estado, Monto, FechaPago, IdPedido) OUTPUT INSERTED.Id VALUES (@Metodo, @EstadoPago, @Monto, @Fecha, @IdPedido)", conexion, transaccion);
                cmdPago.Parameters.AddWithValue("@Metodo", pago.MetodoPago.Nombre);
                // Logic: If Cash/Transfer -> Pendiente, if Card -> Aprobado (Simulated)
                string estadoPago = "Aprobado";
                if (pago.MetodoPago.Nombre.ToLower().Contains("efectivo") || pago.MetodoPago.Nombre.ToLower().Contains("transferencia"))
                    estadoPago = "Pendiente";

                cmdPago.Parameters.AddWithValue("@EstadoPago", estadoPago);
                cmdPago.Parameters.AddWithValue("@Monto", carrito.Total());
                cmdPago.Parameters.AddWithValue("@Fecha", DateTime.Now);
                cmdPago.Parameters.AddWithValue("@IdPedido", idPedido);

                int idPago = (int)cmdPago.ExecuteScalar();
                pedido.Pago.Id = idPago;

                // 5. UPDATE REFERENCIAS
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
                    aux.Estado = new EstadoPedido { Id = 1, Descripcion = (string)data.Reader["Estado"] };
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