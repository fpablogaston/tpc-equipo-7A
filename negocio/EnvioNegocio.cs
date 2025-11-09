using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class EnvioNegocio
    {
        private List<Envio> Envios = new List<Envio>();
        public Envio GetById(int id)
        {
            Envio aux = new Envio();
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.SetQuery("Select Id, DireccionEnvio, Ciudad, Provincia, CodigoPostal, FechaEnvio, FechaEntrega, Estado, IdPedido From Envios Where Id = @Id");
                Datos.SetearParametro("@Id", id);
                Datos.EjecutarLectura();

                while (Datos.Reader.Read())
                {
                    aux.Id = (int)Datos.Reader["Id"];
                    aux.DireccionEnvio = (string)Datos.Reader["DireccionEnvio"];
                    aux.Ciudad = (string)Datos.Reader["Ciudad"];
                    aux.Provincia = (string)Datos.Reader["Provincia"];
                    aux.CodigoPostal = (string)Datos.Reader["CodigoPostal"];
                    aux.FechaEnvio = (DateTime)Datos.Reader["FechaEnvio"];
                    aux.FechaEntrega = Datos.Reader["FechaEntrega"] == DBNull.Value ? (DateTime?)null : (DateTime)Datos.Reader["FechaEntrega"];
                    aux.Estado = (string)Datos.Reader["Estado"];
                    aux.IdPedido = (int)Datos.Reader["IdPedido"];
                }
                return aux;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                throw;
            }
            finally
            {
                Datos.CerrarConexion();
            }
        }
        public void CrearEnvio(Envio envio)
        {
            // Falta crear logica para programar el envio
        }
        public void ActualizarEstado(int id, string nuevoEstado)
        {
            // Falta crear logica para actualizar el estado del envio
        }
        public List<Envio> Listar()
        {
            return Envios;
        }

        public Envio Buscar(int id)
        {
            return null;
            // Falta crear logica para buscar un envio por su ID
        }
    }
}
