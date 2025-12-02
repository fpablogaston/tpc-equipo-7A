using System;
using System.Collections.Generic;
using dominio;

namespace negocio
{
    public class DireccionNegocio
    {
        public List<Direccion> ListarPorCliente(int idCliente)
        {
            List<Direccion> lista = new List<Direccion>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Only fetch active addresses
                datos.SetQuery("SELECT Id, IdCliente, Calle, Ciudad, Provincia, CodigoPostal, Alias FROM Direcciones WHERE IdCliente = @IdCliente AND Activo = 1");
                datos.SetearParametro("@IdCliente", idCliente);
                datos.EjecutarLectura();

                while (datos.Reader.Read())
                {
                    Direccion aux = new Direccion();
                    aux.Id = (int)datos.Reader["Id"];
                    aux.IdCliente = (int)datos.Reader["IdCliente"];
                    aux.Calle = (string)datos.Reader["Calle"];
                    aux.Ciudad = (string)datos.Reader["Ciudad"];
                    aux.Provincia = (string)datos.Reader["Provincia"];
                    aux.CodigoPostal = (string)datos.Reader["CodigoPostal"];
                    aux.Alias = datos.Reader["Alias"] != DBNull.Value ? (string)datos.Reader["Alias"] : "Casa";
                    aux.Activo = true;

                    lista.Add(aux);
                }
                return lista;
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

        public void Agregar(Direccion direccion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetQuery("INSERT INTO Direcciones (IdCliente, Calle, Ciudad, Provincia, CodigoPostal, Alias, Activo) VALUES (@IdCliente, @Calle, @Ciudad, @Provincia, @CodigoPostal, @Alias, 1)");
                datos.SetearParametro("@IdCliente", direccion.IdCliente);
                datos.SetearParametro("@Calle", direccion.Calle);
                datos.SetearParametro("@Ciudad", direccion.Ciudad);
                datos.SetearParametro("@Provincia", direccion.Provincia);
                datos.SetearParametro("@CodigoPostal", direccion.CodigoPostal);
                datos.SetearParametro("@Alias", (object)direccion.Alias ?? DBNull.Value);
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

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Logical Delete
                datos.SetQuery("UPDATE Direcciones SET Activo = 0 WHERE Id = @Id");
                datos.SetearParametro("@Id", id);
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
    }
}