using dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
                Datos.SetearProcedimiento("CrearUsuarioYCliente");
                Datos.SetearParametro("@Username", cliente.Usuario);
                //Datos.SetearParametro("@PasswordHash", cliente.Password);
                Datos.SetearParametro("@Email", cliente.Email);
                Datos.SetearParametro("@Nombre", cliente.Nombre);
                Datos.SetearParametro("@Apellido", cliente.Apellido);
                Datos.SetearParametro("@Telefono", cliente.Telefono);
                Datos.SetearParametro("@Direccion", cliente.Direccion);

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
                Datos.SetQuery("UPDATE Clientes SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono, Direccion = @Direccion, FechaRegistro = @FechaRegistro WHERE Id = @Id");
                Datos.SetearParametro("@Id", cliente.Id);
                Datos.SetearParametro("@Nombre", cliente.Nombre);
                Datos.SetearParametro("@Apellido", cliente.Apellido);
                Datos.SetearParametro("@Email", cliente.Email);
                Datos.SetearParametro("@Telefono", cliente.Telefono);
                Datos.SetearParametro("@Direccion", cliente.Direccion);
                //Datos.SetearParametro("@Contraseña", cliente.Password);
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
                Datos.SetQuery("Select Id, Nombre, Apellido, Email, Telefono, Direccion, FechaRegistro from Clientes");
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
                    //aux.Password = (string)Datos.Reader["Contraseña"];
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
                Datos.SetQuery("Select Id, Nombre, Apellido, Email, Telefono, Direccion, FechaRegistro from Clientes where Id = @Id");
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
                    //aux.Password = (string)Datos.Reader["Contraseña"];
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

        public Cliente Login(string usuario, string password)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetQuery(
                    "SELECT c.Id, c.Nombre, c.Apellido, c.Email, c.Telefono, c.Direccion, " +
                    "u.Id AS IdUsuario, u.Username, u.IdRol " +
                    "FROM Clientes c " +
                    "RIGHT JOIN Usuarios u ON u.Id = c.IdUsuario " +
                    "WHERE u.Username = @Usuario AND u.PasswordHash = @Password"
                );

                datos.SetearParametro("@Usuario", usuario);
                datos.SetearParametro("@Password", password);

                datos.EjecutarLectura();

                if (datos.Reader.Read())
                {
                    Cliente cliente = new Cliente();

                    cliente.Rol = (int)datos.Reader["IdRol"];
                    if (cliente.Rol == 1)
                    {
                    cliente.Id = (int)datos.Reader["Id"];
                    cliente.Nombre = (string)datos.Reader["Nombre"];
                    cliente.Apellido = (string)datos.Reader["Apellido"];
                    cliente.Email = (string)datos.Reader["Email"];
                    cliente.Telefono = (string)datos.Reader["Telefono"];
                    cliente.Direccion = (string)datos.Reader["Direccion"];
                    }
                    cliente.Usuario = (string)datos.Reader["Username"];
                    cliente.IdUsuario = (int)datos.Reader["IdUsuario"];

                    return cliente;
                }

                return null;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
