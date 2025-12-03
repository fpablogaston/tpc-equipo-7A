using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class PagoNegocio
    {
        public List<Pago> Listar()
        {
            List<Pago> lista = new List<Pago>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetQuery("Select Id, MetodoPago, Estado, Monto, FechaPago, IdPedido From Pagos");
                datos.EjecutarLectura();
                while (datos.Reader.Read())
                {
                    Pago pago = new Pago
                    {
                        Id = (int)datos.Reader["Id"],
                        MetodoPago = new MetodoPago
                        {
                            Id = 0,
                            Nombre = datos.Reader["MetodoPago"].ToString()
                        },
                        Estado = new EstadoPago
                        {
                            Id = 0,
                            Nombre = datos.Reader["Estado"].ToString()
                        },
                        Monto = (decimal)datos.Reader["Monto"],
                        FechaPago = (DateTime)datos.Reader["FechaPago"],
                        IdPedido = (int)datos.Reader["IdPedido"]
                    };
                    lista.Add(pago);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void Actualizar(Pago pago)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetQuery("Update Pagos set MetodoPago = @Metodo, Estado = @Estado, Monto = @Monto, FechaPago = @Fecha, IdPedido = @Pedido Where Id = @Id");
                datos.SetearParametro("@Id", pago.Id);
                datos.SetearParametro("@Metodo", pago.MetodoPago.Nombre);
                datos.SetearParametro("@Estado", pago.Estado.Nombre);
                datos.SetearParametro("@Monto", pago.Monto);
                datos.SetearParametro("@Fecha", pago.FechaPago);
                datos.SetearParametro("@Pedido", pago.IdPedido);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public Pago GetById(int id)
        {
            Pago pago = new Pago();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetQuery("Select Id, MetodoPago, Estado, Monto, FechaPago, IdPedido From Pagos Where Id = @Id");
                datos.SetearParametro("@Id", id);
                datos.EjecutarLectura();
                if (datos.Reader.Read())
                {
                    //pago.Id = (int)datos.Reader["Id"];
                    //pago.MetodoPago = (MetodoPago)datos.Reader["MetodoPago"];
                    //pago.Estado = (EstadoPago)datos.Reader["Estado"];

                    ///aca modifique y cree un objeto porque la bd pide un string
                    pago.Id = (int)datos.Reader["Id"];

                    pago.MetodoPago = new MetodoPago
                    {
                        Id = 0,
                        Nombre = datos.Reader["MetodoPago"].ToString()
                    };

                    pago.Estado = new EstadoPago
                    {
                        Id = 0,
                        Nombre = datos.Reader["Estado"].ToString()
                    };

                    pago.Monto = (decimal)datos.Reader["Monto"];
                    pago.FechaPago = (DateTime)datos.Reader["FechaPago"];
                    pago.IdPedido = (int)datos.Reader["IdPedido"];
                }
                return pago;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public List<MetodoPago> ListarMetodos()
        {
            return new List<MetodoPago>
            {
                new MetodoPago { Id = 1, Nombre = "Tarjeta" },
                new MetodoPago { Id = 2, Nombre = "Efectivo" },
                new MetodoPago { Id = 3, Nombre = "Transferencia" }
            };
        }
        public void Agregar(Pago pago)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetQuery("INSERT INTO Pagos (MetodoPago, Estado, Monto, FechaPago, IdPedido) " +
                                "VALUES (@Metodo, @Estado, @Monto, @Fecha, @Pedido)");

                datos.SetearParametro("@Metodo", pago.MetodoPago.Nombre);
                datos.SetearParametro("@Estado", pago.Estado.Nombre);
                datos.SetearParametro("@Monto", pago.Monto);
                datos.SetearParametro("@Fecha", pago.FechaPago);
                datos.SetearParametro("@Pedido", pago.IdPedido);

                datos.EjecutarAccion();
            }
            finally { datos.CerrarConexion(); }
        }

        public void Eliminar(int id)
        {
            AccesoDatos Datos = new AccesoDatos();
            try
            {
                Datos.SetQuery("Delete From Pagos Where Id = @Id");
                Datos.SetearParametro("@Id", id);
                Datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                throw;
            }
        }

        public void ActualizarEstado(int idPago, string nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            datos.SetQuery("UPDATE Pagos SET Estado = @estado WHERE Id = @id");
            datos.SetearParametro("@estado", nuevoEstado);
            datos.SetearParametro("@id", idPago);
            datos.EjecutarAccion();
            datos.CerrarConexion();
        }

    }
}
