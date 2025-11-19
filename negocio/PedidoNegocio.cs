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
        public Pedido GetById(int id)
        {
            Pedido aux = new Pedido();
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetQuery("Select Id, FechaPedido, Estado, Total, IdCliente, IdEnvio, IdPago From Pedidos Where Id = @Id");
                data.SetearParametro("@Id", id);
                data.EjecutarLectura();
                while (data.Reader.Read())
                {
                    aux.Id = (int)data.Reader["Id"];
                    aux.FechaPedido = (DateTime)data.Reader["FechaPedido"];
                    aux.Estado = (string)data.Reader["Estado"];
                    aux.Total = (decimal)data.Reader["Total"];
                    aux.Cliente = new ClienteNegocio().GetById((int)data.Reader["IdCliente"]);
                    aux.Envio = new EnvioNegocio().GetById((int)data.Reader["IdEnvio"]);
                    aux.Pago = new PagoNegocio().GetById((int)data.Reader["IdPago"]);
                }
                return aux;
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


        ///agrego esto a pedidonegocio
        public int Agregar(Pedido pedido)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.SetQuery("INSERT INTO Pedidos (FechaPedido, Estado, Total, IdCliente) " +
                              "OUTPUT INSERTED.Id VALUES (@Fecha, @Estado, @Total, @Cliente)");

                data.SetearParametro("@Fecha", pedido.FechaPedido);
                data.SetearParametro("@Estado", pedido.Estado);
                data.SetearParametro("@Total", pedido.Total);
                data.SetearParametro("@Cliente", pedido.Cliente.Id);

                return data.EjecutarScalar();
            }
            finally
            {
                data.CerrarConexion();
            }
        }
    }
}
