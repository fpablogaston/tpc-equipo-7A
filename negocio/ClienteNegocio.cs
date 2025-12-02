using dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio
{
    public class ClienteNegocio
    {
        public int Agregar(Cliente cliente)
        {
            AccesoDatos Datos = new AccesoDatos();
            int idCliente;

            try
            {
                // 1. Insert Client Data (Profile only, no address columns)
                Datos.SetQuery("INSERT INTO Clientes (Nombre, Apellido, Email, Telefono, FechaRegistro, IdUsuario) OUTPUT INSERTED.Id VALUES (@Nombre, @Apellido, @Email, @Telefono, @FechaRegistro, @IdUsuario)");

                Datos.SetearParametro("@Nombre", cliente.Nombre);
                Datos.SetearParametro("@Apellido", cliente.Apellido);
                Datos.SetearParametro("@Email", cliente.Email);
                Datos.SetearParametro("@Telefono", cliente.Telefono);
                Datos.SetearParametro("@FechaRegistro", DateTime.Now);
                Datos.SetearParametro("@IdUsuario", cliente.IdUsuario);

                idCliente = Datos.EjecutarScalar();

                // 2. Insert the "Selected Address" as the first/principal address in the new table
                if (cliente.DireccionSeleccionada != null && !string.IsNullOrEmpty(cliente.DireccionSeleccionada.Calle))
                {
                    cliente.DireccionSeleccionada.IdCliente = idCliente;

                    // If no alias provided, default to 'Principal'
                    if (string.IsNullOrEmpty(cliente.DireccionSeleccionada.Alias))
                        cliente.DireccionSeleccionada.Alias = "Principal";

                    DireccionNegocio direccionNegocio = new DireccionNegocio();
                    direccionNegocio.Agregar(cliente.DireccionSeleccionada);
                }

                return idCliente;
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
                // Update only the Client profile data. 
                // Address updates are now handled exclusively by DireccionNegocio.
                Datos.SetQuery("UPDATE Clientes SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono WHERE Id = @Id");

                Datos.SetearParametro("@Id", cliente.Id);
                Datos.SetearParametro("@Nombre", cliente.Nombre);
                Datos.SetearParametro("@Apellido", cliente.Apellido);
                Datos.SetearParametro("@Email", cliente.Email);
                Datos.SetearParametro("@Telefono", cliente.Telefono);

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
                // 1. Delete associated addresses first (to prevent FK constraints issues)
                // We use a manual query here or could call DireccionNegocio logic if it had a "DeleteAllByClient" method.
                AccesoDatos datosDir = new AccesoDatos();
                datosDir.SetQuery("DELETE FROM Direcciones WHERE IdCliente = @IdCliente");
                datosDir.SetearParametro("@IdCliente", Id);
                datosDir.EjecutarAccion();
                datosDir.CerrarConexion();

                // 2. Delete the client
                Datos.SetQuery("DELETE FROM Clientes WHERE Id = @Id");
                Datos.SetearParametro("@Id", Id);
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

        public List<Cliente> Listar()
        {
            List<Cliente> Lista = new List<Cliente>();
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                // Removed address columns from the query
                Datos.SetQuery("SELECT Id, Nombre, Apellido, Email, Telefono, FechaRegistro, IdUsuario FROM Clientes");
                Datos.EjecutarLectura();

                while (Datos.Reader.Read())
                {
                    Cliente aux = new Cliente();
                    aux.Id = (int)Datos.Reader["Id"];
                    aux.IdUsuario = (int)Datos.Reader["IdUsuario"];
                    aux.Nombre = (string)Datos.Reader["Nombre"];
                    aux.Apellido = (string)Datos.Reader["Apellido"];
                    aux.Email = (string)Datos.Reader["Email"];
                    aux.Telefono = (string)Datos.Reader["Telefono"];
                    aux.FechaRegistro = (DateTime)Datos.Reader["FechaRegistro"];

                    // Initialize empty lists/objects to avoid NullReferenceExceptions in UI
                    // We typically DON'T load full address lists for every client in a bulk list for performance
                    aux.Direcciones = new List<Direccion>();
                    aux.DireccionSeleccionada = new Direccion();

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
                Datos.SetQuery("SELECT Id, Nombre, Apellido, Email, Telefono, FechaRegistro, IdUsuario FROM Clientes WHERE Id = @Id");
                Datos.SetearParametro("@Id", id);
                Datos.EjecutarLectura();

                if (Datos.Reader.Read())
                {
                    aux.Id = (int)Datos.Reader["Id"];
                    aux.IdUsuario = (int)Datos.Reader["IdUsuario"];
                    aux.Nombre = (string)Datos.Reader["Nombre"];
                    aux.Apellido = (string)Datos.Reader["Apellido"];
                    aux.Email = (string)Datos.Reader["Email"];
                    aux.Telefono = (string)Datos.Reader["Telefono"];
                    aux.FechaRegistro = (DateTime)Datos.Reader["FechaRegistro"];
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

            // 3. Load Addresses into 'Direcciones' list and set 'DireccionSeleccionada'
            if (aux.Id != 0)
            {
                DireccionNegocio dirNegocio = new DireccionNegocio();
                aux.Direcciones = dirNegocio.ListarPorCliente(aux.Id);

                if (aux.Direcciones.Count > 0)
                {
                    // By default, select the first one (or logic for 'Principal' alias)
                    aux.DireccionSeleccionada = aux.Direcciones[0];
                }
                else
                {
                    aux.DireccionSeleccionada = new Direccion();
                }
            }

            return aux;
        }

        public Cliente Login(string usuario, string password)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Query updated: removed Direccion/Ciudad/Provincia/CP columns
                datos.SetQuery(
                    "SELECT c.Id, c.Nombre, c.Apellido, c.Email, c.Telefono, c.FechaRegistro, " +
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
                    cliente.Usuario = (string)datos.Reader["Username"];
                    cliente.IdUsuario = (int)datos.Reader["IdUsuario"];

                    // If user is a Client (Role 1) and has a Client profile created
                    if (cliente.Rol == 1 && datos.Reader["Id"] != DBNull.Value)
                    {
                        cliente.Id = (int)datos.Reader["Id"];
                        cliente.Nombre = (string)datos.Reader["Nombre"];
                        cliente.Apellido = (string)datos.Reader["Apellido"];
                        cliente.Email = (string)datos.Reader["Email"];
                        cliente.Telefono = (string)datos.Reader["Telefono"];
                        cliente.FechaRegistro = (DateTime)datos.Reader["FechaRegistro"];
                    }

                    // Close connection before calling another Negocio method
                    datos.CerrarConexion();

                    // Load Addresses if client exists
                    if (cliente.Id != 0)
                    {
                        DireccionNegocio dirNegocio = new DireccionNegocio();
                        cliente.Direcciones = dirNegocio.ListarPorCliente(cliente.Id);

                        if (cliente.Direcciones.Count > 0)
                            cliente.DireccionSeleccionada = cliente.Direcciones[0];
                        else
                            cliente.DireccionSeleccionada = new Direccion();
                    }
                    else
                    {
                        // Even if no client ID (new user), initialize lists
                        cliente.Direcciones = new List<Direccion>();
                        cliente.DireccionSeleccionada = new Direccion();
                    }

                    return cliente;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                throw;
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

            datos.CerrarConexion();
            return false;
        }

        public bool ExisteEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            // Email column remains in Clientes table
            datos.SetQuery(@"SELECT COUNT(*) FROM Clientes WHERE Email = @e");
            datos.SetearParametro("@e", email);
            datos.EjecutarLectura();

            if (datos.Reader.Read())
                return (int)datos.Reader[0] > 0;

            datos.CerrarConexion();
            return false;
        }
    }
}