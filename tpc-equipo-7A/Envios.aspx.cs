using dominio;
using System;
using System.Web.UI;

namespace tpc_equipo_7A
{
    public partial class Envios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["carrito"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            if (Session["cliente"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                if (Session["envio"] != null)
                {
                    Envio envioGuardado = (Envio)Session["envio"];
                    txtDireccion.Text = envioGuardado.DireccionEnvio;
                    txtCiudad.Text = envioGuardado.Ciudad;
                    txtProvincia.Text = envioGuardado.Provincia;
                    txtCodigoPostal.Text = envioGuardado.CodigoPostal;
                }
                else
                {
                    Cliente cliente = (Cliente)Session["cliente"];
                    if (!string.IsNullOrEmpty(cliente.Direccion))
                    {
                        txtDireccion.Text = cliente.Direccion;
                    }
                }
            }
        }
        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                Envio envio = new Envio
                {
                    DireccionEnvio = txtDireccion.Text,
                    Ciudad = txtCiudad.Text,
                    Provincia = txtProvincia.Text,
                    CodigoPostal = txtCodigoPostal.Text,
                    Estado = "Pendiente", // Estado inicial por defecto
                    FechaEnvio = DateTime.Now // Fecha de creación del registro
                };
                Session["envio"] = envio;
                Response.Redirect("Pagos.aspx");
            }
            catch (Exception ex)
            {
                Session["error"] = "Error al procesar envío: " + ex.Message;
            }
        }
    }
}