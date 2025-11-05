using dominio;
using System;
using System.Collections.Generic;
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
            AccesoDatos Datos = new AccesoDatos();
            try
            {
                Datos.SetQuery("Delete From Productos Where Id = @Id");
                Datos.SetearParametro("@Id", id);
                Datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                throw;
            }
        }
        public List<Producto> Listar()
        {
            List<Producto> Lista = new List<Producto>();
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.SetQuery("Select P.Id, P.Nombre, P.Descripcion, P.Precio, P.Stock, P.ImagenUrl, C.Id as IdCategoria, C.Nombre as CategoriaNombre, C.Descripcion as CategoriaDescripcion From Productos as P, Categorias as C Where P.IdCategoria = C.Id");
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
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)Datos.Reader["IdCategoria"];
                    aux.Categoria.Nombre = (string)Datos.Reader["CategoriaNombre"];
                    aux.Categoria.Descripcion = (string)Datos.Reader["CategoriaDescripcion"];
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
                Datos.SetQuery("Select P.Id, P.Nombre, P.Descripcion, P.Precio, P.Stock, P.ImagenUrl, C.Id as IdCategoria, C.Nombre as CategoriaNombre, C.Descripcion as CategoriaDescripcion From Productos as P, Categorias as C Where P.IdCategoria = C.Id AND Id = @Id");
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
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)Datos.Reader["IdCategoria"];
                    aux.Categoria.Nombre = (string)Datos.Reader["CategoriaNombre"];
                    aux.Categoria.Descripcion = (string)Datos.Reader["CategoriaDescripcion"];
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
    }

}
