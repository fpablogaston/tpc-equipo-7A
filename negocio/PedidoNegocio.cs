using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class PedidoNegocio
    {
        public List<Pedido> Listar()
        {
            List<Pedido> list = new List<Pedido>();
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetQuery("Select Id, FechaPedido, Estado, Total, IdCliente, IdEnvio, IdPago From Pedidos");
                data.EjecutarLectura();
                while (data.Reader.Read())
                {
                    Pedido aux = new Pedido();
                    aux.Id = (int)data.Reader["Id"];
                    aux.FechaPedido = (DateTime)data.Reader["FechaPedido"];
                    aux.Estado = (string)data.Reader["Estado"];
                    aux.Total = (decimal)data.Reader["Total"];
                    aux.Cliente = new ClienteNegocio().GetById((int)data.Reader["IdCliente"]);
                    aux.Envio = new EnvioNegocio().GetById((int)data.Reader["IdEnvio"]);
                    aux.Pago = new PagoNegocio().GetById((int)data.Reader["IdPago"]);
                    list.Add(aux);
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
                throw ex;
            }
            finally
            {
                data.CerrarConexion();
            }
        }
    }
}
