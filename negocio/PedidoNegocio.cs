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
                    list.Add(MapPedido(data.Reader));
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
                    WHERE P.Id = @Id");

                data.SetearParametro("@Id", id);
                data.EjecutarLectura();
                while (data.Reader.Read())
                {
                    aux = MapPedido(data.Reader);
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

        public List<Pedido> ListarPorCliente(int idCliente)
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
                    WHERE P.IdCliente = @IdCliente
                ");

                data.SetearParametro("@IdCliente", idCliente);
                data.EjecutarLectura();

                while (data.Reader.Read())
                {
                    list.Add(MapPedido(data.Reader));
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

        // Helper privado para mapear los datos repetitivos
        private Pedido MapPedido(SqlDataReader reader)
        {
            Pedido aux = new Pedido();
            aux.Id = (int)reader["Id"];
            aux.FechaPedido = (DateTime)reader["FechaPedido"];
            aux.Total = (decimal)reader["Total"];

            // Estado
            aux.Estado = new EstadoPedido();
            aux.Estado.Id = (int)reader["IdEstadoPedido"];
            aux.Estado.Descripcion = (string)reader["EstadoDescripcion"];

            // Cliente
            aux.Cliente = new Cliente();
            aux.Cliente.Id = (int)reader["IdCliente"];
            aux.Cliente.Nombre = (string)reader["ClienteNombre"];
            aux.Cliente.Apellido = (string)reader["ClienteApellido"];

            // Envio
            aux.Envio = new Envio();
            if (reader["IdEnvio"] != DBNull.Value)
            {
                aux.Envio.Id = (int)reader["IdEnvio"];
                aux.Envio.IdEstadoEnvio = reader["EstadoEnvioId"] != DBNull.Value ? (int)reader["EstadoEnvioId"] : 0;
                aux.Envio.EstadoDescripcion = reader["EstadoEnvioDesc"] != DBNull.Value ? (string)reader["EstadoEnvioDesc"] : "";
            }

            // Pago
            aux.Pago = new Pago();
            if (reader["IdPago"] != DBNull.Value)
            {
                aux.Pago.Id = (int)reader["IdPago"];
                aux.Pago.MetodoPago = new MetodoPago { Nombre = (string)reader["MetodoPagoStr"] };
                aux.Pago.Estado = new EstadoPago { Nombre = (string)reader["EstadoPago"] };

                // Pequeña corrección de ID para lógica de frontend
                if (aux.Pago.MetodoPago.Nombre.ToLower().Contains("efectivo"))
                    aux.Pago.MetodoPago.Id = 2;
                else
                    aux.Pago.MetodoPago.Id = 1;
            }

            return aux;
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
                DateTime fecha = DateTime.Now;
                decimal total = carrito.Total();

                // 1. INSERTAR PEDIDO
                SqlCommand cmdPedido = new SqlCommand("INSERT INTO Pedidos (FechaPedido, IdEstadoPedido, Total, IdCliente) OUTPUT INSERTED.Id VALUES (@Fecha, @IdEstado, @Total, @IdCliente)", conexion, transaccion);
                cmdPedido.Parameters.AddWithValue("@Fecha", fecha);
                cmdPedido.Parameters.AddWithValue("@IdEstado", 1); // 1 = Pendiente
                cmdPedido.Parameters.AddWithValue("@Total", total);
                cmdPedido.Parameters.AddWithValue("@IdCliente", cliente.Id);

                int idPedido = (int)cmdPedido.ExecuteScalar();

                // IMPORTANT: Populate the object to return it correctly to the view
                pedido.Id = idPedido;
                pedido.FechaPedido = fecha;
                pedido.Total = total;

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
                // Nota: Usamos las propiedades string del objeto 'envio' que ya vienen cargadas desde la pantalla Envios.aspx
                SqlCommand cmdEnvio = new SqlCommand("INSERT INTO Envios (DireccionEnvio, Ciudad, Provincia, CodigoPostal, IdEstadoEnvio, IdPedido) OUTPUT INSERTED.Id VALUES (@DireccionEnvio, @Ciudad, @Provincia, @CodigoPostal, @IdEstadoEnvio, @IdPedido)", conexion, transaccion);
                cmdEnvio.Parameters.AddWithValue("@DireccionEnvio", envio.DireccionEnvio);
                cmdEnvio.Parameters.AddWithValue("@Ciudad", envio.Ciudad);
                cmdEnvio.Parameters.AddWithValue("@Provincia", envio.Provincia);
                cmdEnvio.Parameters.AddWithValue("@CodigoPostal", envio.CodigoPostal);
                cmdEnvio.Parameters.AddWithValue("@IdEstadoEnvio", envio.IdEstadoEnvio);
                cmdEnvio.Parameters.AddWithValue("@IdPedido", idPedido);

                int idEnvio = (int)cmdEnvio.ExecuteScalar();
                pedido.Envio.Id = idEnvio;

                // 4. INSERTAR PAGO
                SqlCommand cmdPago = new SqlCommand("INSERT INTO Pagos (MetodoPago, Estado, Monto, FechaPago, IdPedido) OUTPUT INSERTED.Id VALUES (@Metodo, @EstadoPago, @Monto, @Fecha, @IdPedido)", conexion, transaccion);
                cmdPago.Parameters.AddWithValue("@Metodo", pago.MetodoPago.Nombre);

                string estadoPago = "Aprobado";
                // Si es efectivo o transferencia, queda pendiente de verificación
                if (pago.MetodoPago.Nombre.ToLower().Contains("efectivo") || pago.MetodoPago.Nombre.ToLower().Contains("transferencia"))
                    estadoPago = "Pendiente";

                cmdPago.Parameters.AddWithValue("@EstadoPago", estadoPago);
                cmdPago.Parameters.AddWithValue("@Monto", total);
                cmdPago.Parameters.AddWithValue("@Fecha", fecha);
                cmdPago.Parameters.AddWithValue("@IdPedido", idPedido);

                int idPago = (int)cmdPago.ExecuteScalar();
                pedido.Pago.Id = idPago;

                // 5. UPDATE REFERENCIAS CRUZADAS EN PEDIDO
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
                datos.SetQuery("UPDATE Pedidos SET FechaPedido = @Fecha, Total = @Total, IdCliente = @Cliente WHERE Id = @Id");
                datos.SetearParametro("@Fecha", pedido.FechaPedido);
                datos.SetearParametro("@Total", pedido.Total);
                datos.SetearParametro("@Cliente", pedido.Cliente.Id);
                datos.SetearParametro("@Id", pedido.Id);
                datos.EjecutarAccion();
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
                data.SetQuery("INSERT INTO Pedidos (FechaPedido, IdEstadoPedido, Total, IdCliente) OUTPUT INSERTED.Id VALUES (@Fecha, @IdEstado, @Total, @Cliente)");

                data.SetearParametro("@Fecha", pedido.FechaPedido);
                data.SetearParametro("@IdEstado", pedido.Estado != null ? pedido.Estado.Id : 1);
                data.SetearParametro("@Total", pedido.Total);
                data.SetearParametro("@Cliente", pedido.Cliente.Id);

                return data.EjecutarScalar();
            }
            finally
            {
                data.CerrarConexion();
            }
        }
    }
}