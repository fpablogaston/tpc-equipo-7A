using dominio;
using negocio;
using System;
using System.Data.SqlClient;

namespace tpc_equipo_7A
{
    public partial class CompraExitosa : System.Web.UI.Page
    {
        protected Pedido PedidoConfirmado { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["carrito"] == null || Session["pedido"] == null)
            {
                Response.Redirect("Default.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                try
                {

                    Cliente cliente = (Cliente)Session["cliente"];
                    Carrito carrito = (Carrito)Session["carrito"];
                    Envio envio = (Envio)Session["envio"];
                    Pago pago = (Pago)Session["pago"];



                    PedidoNegocio negocio = new PedidoNegocio();
                    Pedido PedidoConfirmado = (Pedido)Session["pedido"];

                    lblPedido.Text = "Pedido Nº " + PedidoConfirmado.Id.ToString();
                    lblFecha.Text = PedidoConfirmado.FechaPedido.ToString("dd/MM/yyyy HH:mm");
                    lblTotal.Text = PedidoConfirmado.Total.ToString("N2");
                    lblEnvio.Text = envio.DireccionEnvio + ", " + envio.Ciudad;
                    lblPago.Text = pago.MetodoPago.Nombre;


                    Session["carrito"] = new Carrito();

                    Session["carrito"] = null;
                    Session["envio"] = null;
                    Session["pago"] = null;
                }
                catch (Exception ex)
                {
                    lblError.Visible = true;
                    lblError.Text = "Ocurrió un error al procesar la compra: " + ex.Message;
                }
            }
        }
    }
}
