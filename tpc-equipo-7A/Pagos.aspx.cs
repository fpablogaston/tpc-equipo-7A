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
           
            if (!IsPostBack)
            {
                PagoNegocio negocio = new PagoNegocio();
                repMetodos.DataSource = negocio.ListarMetodos();
                repMetodos.DataBind();
            }
        }

        protected void btnContinuarPago_Click(object sender, EventArgs e)
        {
            string metodoElegido = Request.Form["metodoPago"];

            if (string.IsNullOrEmpty(metodoElegido))
            {
                lblError.Text = "Debe elegir metodo de pago";
                lblError.Visible = true;
                return;
            }

            MetodoPago metodo = new MetodoPago
            {
                Id = int.Parse(metodoElegido),
                Nombre = ObtenerElegido(int.Parse(metodoElegido))
            };

            Pago pago = new Pago
            {
                MetodoPago = metodo,
                Estado = new EstadoPago { Nombre = "Pendiente" },
                FechaPago = DateTime.Now
            };

            Session["pago"] = pago;

            Response.Redirect("ConfirmarCompra.aspx");
        }

        private string ObtenerElegido(int idMetodo)
        {
            PagoNegocio negocio = new PagoNegocio();
            var metodos = negocio.ListarMetodos();
            return metodos.Find(x => x.Id == idMetodo)?.Nombre;
        }

    }
}