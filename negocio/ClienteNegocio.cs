using dominio;
using System;
using System.Collections.Generic;

namespace negocio
{
    public class ClienteNegocio
    {
       public int Agregar (Cliente cliente)
       {
            AccesoDatos Datos = new AccesoDatos();
            int idCliente;

            try
            {
                Datos.SetQuery("Insert Into Clientes (Nombre, Descripcion, Email, Telefono, Direccion, Contraseña, FechaRegistro) Values (@Nombre, @Descripcion, @Email, @Telefono, @Direccion, @Contraseña, @FechaRegistro); SELECT SCOPE_IDENTITY();");
                Datos.SetearParametro("@Nombre", cliente.Nombre);
                Datos.SetearParametro("@Apellido", cliente.Apellido);
                Datos.SetearParametro("@Email", cliente.Email);
                Datos.SetearParametro("@Telefono", cliente.Telefono);
                Datos.SetearParametro("@Direccion", cliente.Direccion);
                Datos.SetearParametro("@Contraseña", cliente.Contraseña);
                Datos.SetearParametro("@FechaRegistro", cliente.FechaRegistro);
                return idCliente = Datos.EjecutarScalar();
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
        public void Actualizar(Cliente cliente)
        {
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.SetQuery("Update Clientes set Nombre = @Nombre, Descripcion = @Descripcion, Email = @Email, Telefono = @Telefono, Direccion = @Direccion, Contraseña = @Contraseña, FechaRegistro = @FechaRegistro Where Id = @Id) Values (@Nombre, @Descripcion, @Email, @Telefono, @Direccion, @Contraseña, @FechaRegistro); SELECT SCOPE_IDENTITY();");
                Datos.SetearParametro("@Id", cliente.Id);
                Datos.SetearParametro("@Nombre", cliente.Nombre);
                Datos.SetearParametro("@Apellido", cliente.Apellido);
                Datos.SetearParametro("@Email", cliente.Email);
                Datos.SetearParametro("@Telefono", cliente.Telefono);
                Datos.SetearParametro("@Direccion", cliente.Direccion);
                Datos.SetearParametro("@Contraseña", cliente.Contraseña);
                Datos.SetearParametro("@FechaRegistro", cliente.FechaRegistro);
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
        public void Eliminar(int Id)
        {
            AccesoDatos Datos = new AccesoDatos();
            try
            {
                Datos.SetQuery("Delete From Clientes Where Id = @Id");
                Datos.SetearParametro("@Id", Id);
                Datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                throw;
            }
        }
        public List<Cliente> Listar()
        {
            List<Cliente> Lista = new List<Cliente>();
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.SetQuery("Select Id, Nombre, Apellido, Email, Telefono, Direccion, Contraseña, FechaRegistro from Clientes");
                Datos.EjecutarLectura();

                while (Datos.Reader.Read())
                {
                    Cliente aux = new Cliente();
                    aux.Id = (int)Datos.Reader["Id"];
                    aux.Nombre = (string)Datos.Reader["Nombre"];
                    aux.Apellido = (string)Datos.Reader["Apellido"];
                    aux.Email = (string)Datos.Reader["Email"];
                    aux.Telefono = (string)Datos.Reader["Telefono"];
                    aux.Direccion = (string)Datos.Reader["Direccion"];
                    aux.Contraseña = (string)Datos.Reader["Contraseña"];
                    aux.FechaRegistro = (DateTime)Datos.Reader["FechaRegistro"];
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
        public Cliente GetById(int id)
        {
            Cliente aux = new Cliente();
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.SetQuery("Select Id, Nombre, Apellido, Email, Telefono, Direccion, Contraseña, FechaRegistro from Clientes where Id = @Id");
                Datos.SetearParametro("@Id", id);
                Datos.EjecutarLectura();

                while (Datos.Reader.Read())
                {
                    aux.Id = (int)Datos.Reader["Id"];
                    aux.Nombre = (string)Datos.Reader["Nombre"];
                    aux.Apellido = (string)Datos.Reader["Apellido"];
                    aux.Email = (string)Datos.Reader["Email"];
                    aux.Telefono = (string)Datos.Reader["Telefono"];
                    aux.Direccion = (string)Datos.Reader["Direccion"];
                    aux.Contraseña = (string)Datos.Reader["Contraseña"];
                    aux.FechaRegistro = (DateTime)Datos.Reader["FechaRegistro"];
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
