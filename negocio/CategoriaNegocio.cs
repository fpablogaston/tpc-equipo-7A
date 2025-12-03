using dominio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            //AccesoDatos Datos = new AccesoDatos();
            //try
            //{
            //    Datos.SetQuery("Delete From Categorias Where id = @id");
            //    Datos.SetearParametro("@id", id);
            //    Datos.EjecutarAccion();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error: {ex.ToString()}");
            //    throw;
            //}

            AccesoDatos Datos = new AccesoDatos();
            try
            {
                Datos.SetQuery("UPDATE Categorias SET Activo = 0 WHERE Id = @Id");
                Datos.SetearParametro("@Id", id);
                Datos.EjecutarAccion();
            }
            finally
            {
                Datos.CerrarConexion();
            }

        }
        public List<Categoria> Listar()
        {
            List<Categoria> Lista = new List<Categoria>();
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                //Datos.SetQuery("Select Id, Nombre, Descripcion from Categorias");
                ///nuevo
                Datos.SetQuery("SELECT Id, Nombre, Descripcion, Activo FROM Categorias WHERE Activo = 1"); 
                Datos.EjecutarLectura();

                while (Datos.Reader.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)Datos.Reader["Id"];
                    aux.Nombre = (string)Datos.Reader["Nombre"];
                    aux.Descripcion = (string)Datos.Reader["Descripcion"];
                    ///nuevo
                    aux.Activo = (bool)Datos.Reader["Activo"];
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

        public List<Categoria> ListarTodos()
        {
            List<Categoria> Lista = new List<Categoria>();
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.SetQuery("SELECT Id, Nombre, Descripcion, Activo FROM Categorias");
                Datos.EjecutarLectura();

                while (Datos.Reader.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)Datos.Reader["Id"];
                    aux.Nombre = (string)Datos.Reader["Nombre"];
                    aux.Descripcion = (string)Datos.Reader["Descripcion"];
                    aux.Activo = (bool)Datos.Reader["Activo"];

                    Lista.Add(aux);
                }

                return Lista;
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
                //Datos.SetQuery("Select Id, Nombre, Descripcion from Categorias where Id = @Id");

                Datos.SetQuery("SELECT Id, Nombre, Descripcion, Activo FROM Categorias WHERE Id = @Id AND Activo = 1");

                Datos.SetearParametro("@Id", id);
                Datos.EjecutarLectura();

                while (Datos.Reader.Read())
                {
                    aux.Id = (int)Datos.Reader["Id"];
                    aux.Nombre = (string)Datos.Reader["Nombre"];
                    aux.Descripcion = (string)Datos.Reader["Descripcion"];
                    ///nuevo
                    aux.Activo = (bool)Datos.Reader["Activo"];
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

        public void Habilitar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetQuery("UPDATE Categorias SET Activo = 1 WHERE Id = @Id");
                datos.SetearParametro("@Id", id);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

    }
}
