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
        private List<Pago> Pagos = new List<Pago>();
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

        ///cree listar metodo de pago
        public List<MetodoPago> ListarMetodos()
        {
            return new List<MetodoPago>
            {
                new MetodoPago { Id = 1, Nombre = "Tarjeta" },
                new MetodoPago { Id = 2, Nombre = "Efectivo" },
                new MetodoPago { Id = 3, Nombre = "Transferencia" }
            };
        }

        public void ProcesarPago(Pago pago)
        {
            // Falta crear logica para procesar el pago
        }

        public void CambiarEstado(int id, string nuevoEstado)
        {
            // Falta crear logica para cambiar el estado del pago
        }
        public List<Pago> Listar()
        {
            return Pagos;
        }
    }
}
