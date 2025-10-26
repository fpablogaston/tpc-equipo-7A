using dominio;
using System;
using System.Collections.Generic;

namespace negocio
{
    public class CategoriaNegocio
    {
        public int Agregar(Categoria categoria)
        {
            AccesoDatos Datos = new AccesoDatos();
            int idCategoria;

            try
            {
                Datos.SetQuery("Insert Into Categorias (Nombre, Descripcion) Values (@Nombre, @Descripcion); SELECT SCOPE_IDENTITY();");
                Datos.SetearParametro("@Nombre", categoria.Nombre);
                Datos.SetearParametro("@Descripcion", categoria.Descripcion);
                return idCategoria = Datos.EjecutarScalar();
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
        public void Actualizar(Categoria categoria)
        {
            AccesoDatos Datos = new AccesoDatos();
            try
            {
                Datos.SetQuery("Update Categorias set Nombre = @Nombre, Descripcion = @Descripcion Where Id = @Id");
                Datos.SetearParametro("@Id", categoria.Id);
                Datos.SetearParametro("@Nombre", categoria.Nombre);
                Datos.SetearParametro("@Descripcion", categoria.Descripcion);
                Datos.EjecutarAccion();
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
        public void Eliminar(int id)
        {
            AccesoDatos Datos = new AccesoDatos();
            try
            {
                Datos.SetQuery("Delete From Categorias Where id = @id");
                Datos.SetearParametro("@id", id);
                Datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                throw;
            }
        }
        public List<Categoria> Listar()
        {
            List<Categoria> Lista = new List<Categoria>();
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.SetQuery("Select Id, Nombre, Descripcion from Categorias");
                Datos.EjecutarLectura();

                while (Datos.Reader.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)Datos.Reader["Id"];
                    aux.Nombre = (string)Datos.Reader["Nombre"];
                    aux.Descripcion = (string)Datos.Reader["Descripcion"];
                    Lista.Add(aux);
                }
                return Lista;
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
        public Categoria GetById(int id)
        {
            AccesoDatos Datos = new AccesoDatos();
            Categoria aux = new Categoria();
            try
            {
                Datos.SetQuery("Select Id, Nombre, Descripcion from Categorias where Id = @Id");
                Datos.SetearParametro("@Id", id);
                Datos.EjecutarLectura();

                while (Datos.Reader.Read())
                {
                    aux.Id = (int)Datos.Reader["Id"];
                    aux.Nombre = (string)Datos.Reader["Nombre"];
                    aux.Descripcion = (string)Datos.Reader["Descripcion"];
                }
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
            return aux;
        }
    }
}
