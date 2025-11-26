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
                Datos.SetearParametro("@PasswordHash", cliente.Password);
                Datos.SetearParametro("@Email", cliente.Email);
                Datos.SetearParametro("@Nombre", cliente.Nombre);
                Datos.SetearParametro("@Apellido", cliente.Apellido);
                Datos.SetearParametro("@Telefono", cliente.Telefono);
                Datos.SetearParametro("@Direccion", cliente.Direccion.Calle);
                Datos.SetearParametro("@Ciudad", cliente.Direccion.Ciudad);
                Datos.SetearParametro("@Provincia", cliente.Direccion.Provincia);
                Datos.SetearParametro("@CodigoPostal", cliente.Direccion.CodigoPostal);

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
                Datos.SetQuery("UPDATE Clientes SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono, Direccion = @Direccion, Ciudad = @Ciudad, Provincia = @Provincia, CodigoPostal = @CodigoPostal, FechaRegistro = @FechaRegistro WHERE Id = @Id");

                Datos.SetearParametro("@Id", cliente.Id);
                Datos.SetearParametro("@Nombre", cliente.Nombre);
                Datos.SetearParametro("@Apellido", cliente.Apellido);
                Datos.SetearParametro("@Email", cliente.Email);
                Datos.SetearParametro("@Telefono", cliente.Telefono);

                Datos.SetearParametro("@Direccion", cliente.Direccion.Calle);
                Datos.SetearParametro("@Ciudad", cliente.Direccion.Ciudad);
                Datos.SetearParametro("@Provincia", cliente.Direccion.Provincia);
                Datos.SetearParametro("@CodigoPostal", cliente.Direccion.CodigoPostal);

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
                Datos.SetQuery("Select Id, Nombre, Apellido, Email, Telefono, Direccion, Ciudad, Provincia, CodigoPostal, FechaRegistro from Clientes");
                Datos.EjecutarLectura();

                while (Datos.Reader.Read())
                {
                    Cliente aux = new Cliente();
                    aux.Direccion = new Direccion();
                    aux.Id = (int)Datos.Reader["Id"];
                    aux.Nombre = (string)Datos.Reader["Nombre"];
                    aux.Apellido = (string)Datos.Reader["Apellido"];
                    aux.Email = (string)Datos.Reader["Email"];
                    aux.Telefono = (string)Datos.Reader["Telefono"];
                    aux.Direccion.Calle = (string)Datos.Reader["Direccion"];
                    aux.Direccion.Ciudad = (string)Datos.Reader["Ciudad"];
                    aux.Direccion.Provincia = (string)Datos.Reader["Provincia"];
                    aux.Direccion.CodigoPostal= (string)Datos.Reader["CodigoPostal"];
                    aux.Password = (string)Datos.Reader["Contraseña"];
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
            aux.Direccion = new Direccion();
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.SetQuery("Select Id, Nombre, Apellido, Email, Telefono, Direccion, Ciudad, Provincia, CodigoPostal, FechaRegistro from Clientes where Id = @Id");
                Datos.SetearParametro("@Id", id);
                Datos.EjecutarLectura();

                while (Datos.Reader.Read())
                {
                    aux.Id = (int)Datos.Reader["Id"];
                    aux.Nombre = (string)Datos.Reader["Nombre"];
                    aux.Apellido = (string)Datos.Reader["Apellido"];
                    aux.Email = (string)Datos.Reader["Email"];
                    aux.Telefono = (string)Datos.Reader["Telefono"];
                    aux.Direccion.Calle = (string)Datos.Reader["Direccion"];
                    aux.Direccion.Ciudad = (string)Datos.Reader["Ciudad"];
                    aux.Direccion.Provincia = (string)Datos.Reader["Provincia"];
                    aux.Direccion.CodigoPostal = (string)Datos.Reader["CodigoPostal"];
                    aux.Password = (string)Datos.Reader["Contraseña"];
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
                    "SELECT c.Id, c.Nombre, c.Apellido, c.Email, c.Telefono, c.Direccion, c.Ciudad, c.Provincia, c.CodigoPostal, " +
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
                    cliente.Direccion = new Direccion();

                    cliente.Rol = (int)datos.Reader["IdRol"];
                    if (cliente.Rol == 1)
                    {
                    cliente.Id = (int)datos.Reader["Id"];
                    cliente.Nombre = (string)datos.Reader["Nombre"];
                    cliente.Apellido = (string)datos.Reader["Apellido"];
                    cliente.Email = (string)datos.Reader["Email"];
                    cliente.Telefono = (string)datos.Reader["Telefono"];
                    cliente.Direccion.Calle = (string)datos.Reader["Direccion"];
                    cliente.Direccion.Ciudad = (string)datos.Reader["Ciudad"];
                    cliente.Direccion.Provincia = (string)datos.Reader["Provincia"];
                    cliente.Direccion.CodigoPostal = (string)datos.Reader["CodigoPostal"];
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

        public bool ExisteUsuario(string username)
        {
            AccesoDatos datos = new AccesoDatos();
            datos.SetQuery("SELECT COUNT(*) FROM Usuarios WHERE Username = @u");
            datos.SetearParametro("@u", username);
            datos.EjecutarLectura();

            if (datos.Reader.Read())
                return (int)datos.Reader[0] > 0;

            return false;
        }
        public bool ExisteEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            datos.SetQuery(@"SELECT COUNT(*) 
                         FROM Clientes c 
                         INNER JOIN Usuarios u ON u.Id = c.IdUsuario
                         WHERE c.Email = @e");
            datos.SetearParametro("@e", email);
            datos.EjecutarLectura();

            if (datos.Reader.Read())
                return (int)datos.Reader[0] > 0;

            return false;
        }
    }
}
