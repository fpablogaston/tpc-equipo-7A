using System;
using System.Data.SqlClient;

namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection Conexion;
        private SqlCommand Comando;
        private SqlDataReader Lector;

        public SqlDataReader Reader
        {
            get { return Lector; }
        }
        public AccesoDatos()
        {
            Conexion = new SqlConnection("server=.\\SQLEXPRESS; database=Ecommerce; integrated security=true");
            Comando = new SqlCommand();
        }
        public void SetQuery(string consulta)
        {
            Comando.CommandType = System.Data.CommandType.Text;
            Comando.CommandText = consulta;

            Comando.Parameters.Clear();
        }
        public void EjecutarLectura()
        {
            try
            {
                Comando.Connection = Conexion;
                Conexion.Open();
                Lector = Comando.ExecuteReader();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CerrarConexion()
        {
            if (Lector != null)
                Lector.Close();
            Conexion.Close();
        }

        public void SetearParametro(string nombre, object valor)
        {
            if (Comando.Parameters.Contains(nombre))
                Comando.Parameters.RemoveAt(nombre);

            Comando.Parameters.AddWithValue(nombre, valor);
        }

        public void EjecutarAccion()
        {
            Comando.Connection = Conexion;
            try
            {
                Conexion.Open();
                Comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EjecutarScalar()
        {

            Comando.Connection = Conexion;
            try
            {
                Conexion.Open();
                object result = Comando.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    return 0;

                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                throw ex;
            }
            finally
            {
                Conexion.Close();
            }
        }
    }
}
