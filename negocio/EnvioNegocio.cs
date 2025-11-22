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

        /*
         * 
        Id	    DireccionEnvio	        Ciudad	        Provincia	    CodigoPostal	FechaEnvio	                FechaEntrega	Estado	    IdPedido
        1	    Av. Siempre Viva 742	Buenos Aires	Buenos Aires	1000	        2025-10-26  18:44:43.820	NULL	        Preparando	1
        2	    Calle Falsa 123	        Córdoba	        Córdoba	        5000	        2025-10-26  18:44:43.820	NULL	        Preparando	2
        */
        public List<Envio> Listar()
        {
            List<Envio> list = new List<Envio>();
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetQuery("Select Id, DireccionEnvio, Ciudad, Provincia, CodigoPostal, FechaEnvio, FechaEntrega, Estado, IdPedido From Envios");
                data.EjecutarLectura();
                while (data.Reader.Read())
                {
                    Envio aux = new Envio();
                    aux.Id = (int)data.Reader["Id"];
                    aux.DireccionEnvio = (string)data.Reader["DireccionEnvio"];
                    aux.Ciudad = (string)data.Reader["Estado"];
                    aux.Provincia = (string)data.Reader["Provincia"];
                    aux.CodigoPostal = (string)data.Reader["CodigoPostal"];
                    aux.FechaEnvio = (DateTime)data.Reader["FechaEnvio"];
                    aux.FechaEntrega = data.Reader["FechaEntrega"] == DBNull.Value ? (DateTime?)null : (DateTime)data.Reader["FechaEntrega"];
                    aux.Estado = (string)data.Reader["Estado"];
                    aux.IdPedido = (int)data.Reader["IdPedido"];
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
        public void Eliminar(int Id)
        {
            AccesoDatos Datos = new AccesoDatos();
            try
            {
                Datos.SetQuery("Delete From Envios Where Id = @Id");
                Datos.SetearParametro("@Id", Id);
                Datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                throw;
            }
        }
    }
}
