using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace tpc_equipo_7A
{
    public partial class ConfirmarCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validaciones de Seguridad
            if (Session["cliente"] == null) Response.Redirect("Login.aspx");
            if (Session["carrito"] == null) Response.Redirect("Default.aspx");
            if (Session["envio"] == null) Response.Redirect("Envios.aspx");
            if (Session["pago"] == null) Response.Redirect("Pagos.aspx");

            if (!IsPostBack)
            {
                CargarResumen();
            }
        }

        private void CargarResumen()
        {
            Carrito carrito = (Carrito)Session["carrito"];
            Envio envio = (Envio)Session["envio"];
            Pago pago = (Pago)Session["pago"];

            // Cargar Grilla
            gvProductos.DataSource = carrito.ListaCarrito;
            gvProductos.DataBind();
            lblTotal.Text = carrito.Total().ToString("C");

            // Cargar Datos Envio
            lblDireccion.Text = envio.DireccionEnvio;
            lblCiudad.Text = envio.Ciudad;

            // Cargar Datos Pago
            lblMetodoPago.Text = pago.MetodoPago.Nombre;
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = (Cliente)Session["cliente"];
                Carrito carrito = (Carrito)Session["carrito"];
                Envio envio = (Envio)Session["envio"];
                Pago pago = (Pago)Session["pago"];

                PedidoNegocio negocio = new PedidoNegocio();

                Pedido pedido = negocio.GuardarPedidoCompleto(cliente, carrito, envio, pago);

                Session["pedido"] = pedido;

                if (this.Master is Site master)
                {
                    master.UpdateTotals();
                }

                EmailService email = new EmailService();

                string cuerpo = $@"
                <h2>¡Gracias por tu compra!</h2>
                <p>Tu pedido fue procesado correctamente.</p>

                <p><strong>Número de Pedido:</strong> {pedido.Id}</p>
                <p><strong>Total:</strong> ${pedido.Total}</p>
                <p><strong>Método de Pago:</strong> {pago.MetodoPagoNombre}</p>
                <p><strong>Estado del Pago:</strong> {pago.Estado.Nombre}</p>
                <p><strong>Estado del Envío:</strong> {envio.EstadoDescripcion}</p>

                <hr/>
                <p>¡Que tengas un excelente día!</p>
                ";

                email.EnviarMail(cliente.Email, "Confirmación de compra", cuerpo);

                Response.Redirect("CompraExitosa.aspx");
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Write("<script>alert('Hubo un error al procesar el pedido.');</script>");
            }
        }

    }
}