using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tpc_equipo_7A
{
    public partial class Envios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx?msg=login_required");
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtLocalidad.Text))
            {
                lblError.Text = "Debe completar dirección y localidad.";
                lblError.Visible = true;
                return;
            }

            Envio envio = new Envio
            {
                DireccionEnvio = txtDireccion.Text,
                Ciudad = txtLocalidad.Text,
                Estado = "Pendiente",
                FechaEnvio = DateTime.Now,
                InfoAdicional = txtInfoAdicional.Text
            };

            Session["envio"] = envio;

            Response.Redirect("Pagos.aspx");
        }
    }
}