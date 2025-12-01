using dominio;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace negocio
{
    public class ProductoNegocio
    {
        public int Agregar (Producto producto)
        {
            AccesoDatos Datos = new AccesoDatos();
            int idProducto;

            try
            {
                Datos.SetQuery("Insert Into Productos (Nombre, Descripcion, Precio, Stock, ImagenUrl, IdCategoria) Values (@Nombre, @Descripcion, @Precio, @Stock, @ImagenUrl, @IdCategoria); SELECT SCOPE_IDENTITY();");
                Datos.SetearParametro("@Nombre", producto.Nombre);
                Datos.SetearParametro("@Descripcion", producto.Descripcion);
                Datos.SetearParametro("@Precio", producto.Precio);
                Datos.SetearParametro("@Stock", producto.Stock);
                Datos.SetearParametro("@ImagenUrl", producto.ImagenUrl);
                Datos.SetearParametro("@IdCategoria", producto.Categoria.Id);
                return idProducto = Datos.EjecutarScalar();
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
        public void Actualizar (Producto producto)
        {
            AccesoDatos Datos = new AccesoDatos();
            try
            {
                Datos.SetQuery("Update Productos set Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, Stock = @Stock, ImagenUrl = @ImagenUrl, IdCategoria = @IdCategoria Where Id = @Id");
                Datos.SetearParametro("@Id", producto.Id);
                Datos.SetearParametro("@Nombre", producto.Nombre);
                Datos.SetearParametro("@Descripcion", producto.Descripcion);
                Datos.SetearParametro("@Precio", producto.Precio);
                Datos.SetearParametro("@Stock", producto.Stock);
                Datos.SetearParametro("@ImagenUrl", producto.ImagenUrl);
                Datos.SetearParametro("@IdCategoria", producto.Categoria.Id);
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
            //    Datos.SetQuery("Delete From Productos Where Id = @Id");
            //    Datos.SetearParametro("@Id", id);
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
                Datos.SetQuery("UPDATE Productos SET Activo = 0 WHERE Id = @Id");
                Datos.SetearParametro("@Id", id);
                Datos.EjecutarAccion();
            }
            finally
            {
                Datos.CerrarConexion();
            }

        }
        public List<Producto> Listar()
        {
            List<Producto> Lista = new List<Producto>();
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                //Datos.SetQuery("Select P.Id, P.Nombre, P.Descripcion, P.Precio, P.Stock, P.ImagenUrl, C.Id as IdCategoria, C.Nombre as CategoriaNombre, C.Descripcion as CategoriaDescripcion From Productos as P, Categorias as C Where P.IdCategoria = C.Id");

                Datos.SetQuery("SELECT P.Id, P.Nombre, P.Descripcion, P.Precio, P.Stock, P.ImagenUrl, P.Activo,C.Id AS IdCategoria, C.Nombre AS CategoriaNombre, C.Descripcion AS CategoriaDescripcion,C.Activo AS CategoriaActivo FROM Productos P INNER JOIN Categorias C ON P.IdCategoria = C.Id WHERE P.Activo = 1 AND C.Activo = 1");
                Datos.EjecutarLectura();

                while (Datos.Reader.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)Datos.Reader["Id"];
                    aux.Nombre = (string)Datos.Reader["Nombre"];
                    aux.Descripcion = (string)Datos.Reader["Descripcion"];
                    aux.Precio = (decimal)Datos.Reader["Precio"];
                    aux.Stock = (int)Datos.Reader["Stock"];
                    aux.ImagenUrl = (string)Datos.Reader["ImagenUrl"];
                    ///nuevo
                    aux.Activo = (bool)Datos.Reader["Activo"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)Datos.Reader["IdCategoria"];
                    aux.Categoria.Nombre = (string)Datos.Reader["CategoriaNombre"];
                    aux.Categoria.Descripcion = (string)Datos.Reader["CategoriaDescripcion"];
                    /// nuevo
                    aux.Categoria.Activo = (bool)Datos.Reader["CategoriaActivo"];
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
        public Producto GetById (int id)
        {
            Producto aux = new Producto();
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                //Datos.SetQuery("Select Id, Nombre, Descripcion, Precio, Stock, ImagenUrl, IdCategoria From Productos Where Id = @Id");
                //Datos.SetearParametro("@Id", id);

                Datos.SetQuery("SELECT Id, Nombre, Descripcion, Precio, Stock, ImagenUrl, IdCategoria, Activo  FROM Productos WHERE Id = @Id AND Activo = 1");
                Datos.SetearParametro("@Id", id);

                Datos.EjecutarLectura();

                while (Datos.Reader.Read())
                {
                    aux.Id = (int)Datos.Reader["Id"];
                    aux.Nombre = (string)Datos.Reader["Nombre"];
                    aux.Descripcion = (string)Datos.Reader["Descripcion"];
                    aux.Precio = (decimal)Datos.Reader["Precio"];
                    aux.Stock = (int)Datos.Reader["Stock"];
                    aux.ImagenUrl = (string)Datos.Reader["ImagenUrl"];
                    aux.Activo = (bool)Datos.Reader["Activo"];
                    aux.Categoria = new CategoriaNegocio().GetById((int)Datos.Reader["IdCategoria"]);
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
        public List<Producto> GetByIdCategoria(int idCategoria)
        {
            List<Producto> list = new List<Producto>();
            AccesoDatos Datos = new AccesoDatos();
            try
            {
                //Datos.SetQuery("Select Id, Nombre, Descripcion, Precio, Stock, ImagenUrl, IdCategoria From Productos Where IdCategoria = @idCategoria");

                Datos.SetQuery("SELECT Id, Nombre, Descripcion, Precio, Stock, ImagenUrl, IdCategoria, Activo  FROM Productos WHERE IdCategoria = @idCategoria AND Activo = 1");


                Datos.SetearParametro("@idCategoria", idCategoria);
                Datos.EjecutarLectura();
                while (Datos.Reader.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)Datos.Reader["Id"];
                    aux.Nombre = (string)Datos.Reader["Nombre"];
                    aux.Descripcion = (string)Datos.Reader["Descripcion"];
                    aux.Precio = (decimal)Datos.Reader["Precio"];
                    aux.Stock = (int)Datos.Reader["Stock"];
                    aux.ImagenUrl = (string)Datos.Reader["ImagenUrl"];
                    ///nuevo
                    aux.Activo = (bool)Datos.Reader["Activo"];
                    aux.Categoria = new CategoriaNegocio().GetById((int)Datos.Reader["IdCategoria"]);
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
                Datos.CerrarConexion();
            }
        }
    }

}
