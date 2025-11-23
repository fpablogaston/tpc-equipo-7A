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
    public partial class Pagos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validaciones de seguridad de flujo
            if (Session["carrito"] == null) Response.Redirect("Default.aspx");
            if (Session["cliente"] == null) Response.Redirect("Login.aspx");
            if (Session["envio"] == null) Response.Redirect("Envios.aspx");

            if (!IsPostBack)
            {
                CargarMetodosPago();
                MostrarTotal();
            }
        }
        private void CargarMetodosPago()
        {
            PagoNegocio negocio = new PagoNegocio();
            repMetodos.DataSource = negocio.ListarMetodos();
            repMetodos.DataBind();
        }
        private void MostrarTotal()
        {
            Carrito carrito = (Carrito)Session["carrito"];
            if (carrito != null)
            {
                lblTotalPagar.Text = carrito.Total().ToString("C");
            }
        }
        protected void btnContinuarPago_Click(object sender, EventArgs e)
        {
            string metodoElegidoId = Request.Form["metodoPago"];
            if (string.IsNullOrEmpty(metodoElegidoId))
            {
                lblError.Text = "⚠️ Por favor, seleccione un método de pago para continuar.";
                lblError.Visible = true;
                return;
            }
            try
            {
                int idMetodo = int.Parse(metodoElegidoId);
                Carrito carrito = (Carrito)Session["carrito"];

                Pago pago = new Pago
                {
                    MetodoPago = new MetodoPago { Id = idMetodo, Nombre = ObtenerNombreMetodo(idMetodo) },
                    Estado = new EstadoPago { Nombre = "Pendiente" }, // Estado inicial por defecto
                    Monto = carrito.Total(),
                    FechaPago = DateTime.Now
                };
                Session["pago"] = pago;
                Response.Redirect("ConfirmarCompra.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al procesar el pago: " + ex.Message;
                lblError.Visible = true;
            }
        }
        private string ObtenerNombreMetodo(int idMetodo)
        {
            PagoNegocio negocio = new PagoNegocio();
            var metodos = negocio.ListarMetodos();
            var metodo = metodos.Find(x => x.Id == idMetodo);
            return metodo != null ? metodo.Nombre : "Desconocido";
        }
    }
}