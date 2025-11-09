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
                    pago.Id = (int)datos.Reader["Id"];
                    pago.MetodoPago = (string)datos.Reader["MetodoPago"];
                    pago.Estado = (string)datos.Reader["Estado"];
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
