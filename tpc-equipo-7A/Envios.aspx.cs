using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace tpc_equipo_7A
{
    public partial class Envios : System.Web.UI.Page
    {
        // Property to track selected address ID in ViewState/UpdatePanel
        protected int IdDireccionSeleccionada
        {
            get { return (int)(ViewState["IdDireccionSeleccionada"] ?? 0); }
            set { ViewState["IdDireccionSeleccionada"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Security Checks
            if (Session["cliente"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            if (Session["carrito"] == null)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarDirecciones();
            }
        }

        private void CargarDirecciones()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            DireccionNegocio negocio = new DireccionNegocio();

            // Refresh list from DB to ensure it's up to date
            List<Direccion> direcciones = negocio.ListarPorCliente(cliente.Id);

            repDirecciones.DataSource = direcciones;
            repDirecciones.DataBind();

            // Logic to restore previous selection if user came back from "Pagos"
            if (Session["envio"] != null)
            {
                Envio envioPrevio = (Envio)Session["envio"];

                // If ID is 6 (Retiro en Local), select RadioButton
                if (envioPrevio.IdEstadoEnvio == 6)
                {
                    rbRetiro.Checked = true;
                    IdDireccionSeleccionada = 0;
                }
                else
                {
                    // Attempt to match address
                    // Since Envio doesn't store Address ID directly (it copies string fields), 
                    // we might default to 0 or try to match strings. Simpler to reset for now.
                }
            }
        }

        protected void TipoEnvio_CheckedChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (rbRetiro.Checked)
            {
                pnlNuevaDireccion.Visible = false;
                IdDireccionSeleccionada = 0; // Deselect any address
                CargarDirecciones(); // Re-bind to remove visual selection
            }
            else if (rbNuevaDireccion.Checked)
            {
                pnlNuevaDireccion.Visible = true;
                IdDireccionSeleccionada = 0;
                CargarDirecciones();
            }
        }

        protected void repDirecciones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "Seleccionar")
            {
                // Uncheck other options
                rbRetiro.Checked = false;
                rbNuevaDireccion.Checked = false;
                pnlNuevaDireccion.Visible = false;
                lblError.Visible = false;

                // Set selection
                IdDireccionSeleccionada = id;
                CargarDirecciones(); // Re-bind to update UI styles
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    DireccionNegocio negocio = new DireccionNegocio();
                    negocio.Eliminar(id);

                    if (IdDireccionSeleccionada == id)
                        IdDireccionSeleccionada = 0;

                    CargarDirecciones();
                }
                catch (Exception ex)
                {
                    lblError.Text = "No se pudo eliminar: " + ex.Message;
                    lblError.Visible = true;
                }
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            Envio envio = new Envio();
            DireccionNegocio dirNegocio = new DireccionNegocio();
            Cliente cliente = (Cliente)Session["cliente"];

            try
            {
                // CASE 1: STORE PICKUP
                if (rbRetiro.Checked)
                {
                    envio.DireccionEnvio = "Retiro en Local";
                    envio.Ciudad = "Springfield";
                    envio.Provincia = "Buenos Aires";
                    envio.CodigoPostal = "0000";
                    envio.IdEstadoEnvio = 6; // Assuming 6 is "Retiro en Local" based on your DB Script
                    envio.EstadoDescripcion = "Retiro en Local";
                }
                // CASE 2: NEW ADDRESS
                else if (rbNuevaDireccion.Checked)
                {
                    // Validate manually if validators didn't catch it
                    if (string.IsNullOrWhiteSpace(txtCalle.Text) || string.IsNullOrWhiteSpace(txtCiudad.Text))
                    {
                        lblError.Text = "Por favor, complete todos los campos de la dirección.";
                        lblError.Visible = true;
                        return;
                    }

                    Direccion nueva = new Direccion();
                    nueva.IdCliente = cliente.Id;
                    nueva.Alias = string.IsNullOrWhiteSpace(txtAlias.Text) ? "Nuevo" : txtAlias.Text;
                    nueva.Calle = txtCalle.Text;
                    nueva.Ciudad = txtCiudad.Text;
                    nueva.Provincia = txtProvincia.Text;
                    nueva.CodigoPostal = txtCP.Text;

                    // Save to DB immediately
                    dirNegocio.Agregar(nueva);

                    // Set Envio data
                    envio.DireccionEnvio = nueva.Calle;
                    envio.Ciudad = nueva.Ciudad;
                    envio.Provincia = nueva.Provincia;
                    envio.CodigoPostal = nueva.CodigoPostal;
                    envio.IdEstadoEnvio = 1; // Pendiente
                    envio.EstadoDescripcion = "Pendiente";
                }
                // CASE 3: EXISTING ADDRESS
                else if (IdDireccionSeleccionada > 0)
                {
                    List<Direccion> lista = dirNegocio.ListarPorCliente(cliente.Id);
                    Direccion seleccionada = lista.FirstOrDefault(x => x.Id == IdDireccionSeleccionada);

                    if (seleccionada == null)
                    {
                        lblError.Text = "Error al recuperar la dirección seleccionada.";
                        lblError.Visible = true;
                        return;
                    }

                    envio.DireccionEnvio = seleccionada.Calle;
                    envio.Ciudad = seleccionada.Ciudad;
                    envio.Provincia = seleccionada.Provincia;
                    envio.CodigoPostal = seleccionada.CodigoPostal;
                    envio.IdEstadoEnvio = 1;
                    envio.EstadoDescripcion = "Pendiente";
                }
                else
                {
                    lblError.Text = "Por favor, selecciona una forma de envío.";
                    lblError.Visible = true;
                    return;
                }

                // SAVE TO SESSION AND PROCEED
                Session["envio"] = envio;
                Response.Redirect("Pagos.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = "Ocurrió un error: " + ex.Message;
                lblError.Visible = true;
            }
        }
    }
}