using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tpc_equipo_7A
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Cliente> lista = new ClienteNegocio().Listar();
            List<Producto> productos = new ProductoNegocio().Listar();
            List<Categoria> categorias = new CategoriaNegocio().Listar();
            List<Pedido> pedidos = new PedidoNegocio().Listar();
            List<Pago> pagos = new PagoNegocio().Listar();
            List<Envio> envios = new EnvioNegocio().Listar();
            Carrito carrito = new Carrito();
        }
    }
}