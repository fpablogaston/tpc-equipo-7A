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
    public partial class adminCategoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                gvCategoria.DataSource = negocio.Listar();
                gvCategoria.DataBind();
            }
        }
        
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ///aca validacion
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(TxtDescripcion.Text))
                {
                    lblMensaje.Text = "Debe completar los campos nombre y descripcion.";
                    lblMensaje.CssClass = "text-danger";
                    return;
                }

                dominio.Categoria nuevo = new dominio.Categoria();
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = TxtDescripcion.Text;

                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.Agregar(nuevo);

                gvCategoria.DataSource = negocio.Listar();
                gvCategoria.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
               ///aca validacion
                if (string.IsNullOrWhiteSpace(txtId.Text))
                {
                    lblMensaje.Text = "Seleccionar una categoria para modificar.";
                    lblMensaje.CssClass = "text-danger";
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(TxtDescripcion.Text))
                {
                    lblMensaje.Text = "Completar los campos nombre y descripción.";
                    lblMensaje.CssClass = "text-danger";
                    return;
                }

                dominio.Categoria modificar = new dominio.Categoria();
                modificar.Nombre = txtNombre.Text;
                modificar.Descripcion = TxtDescripcion.Text;

                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.Actualizar(modificar);

                gvCategoria.DataSource = negocio.Listar();
                gvCategoria.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ///aca validacion
                if (string.IsNullOrWhiteSpace(txtId.Text))
                {
                    lblMensaje.Text = "Seleccionar una categoria para eliminar.";
                    lblMensaje.CssClass = "text-danger";
                    return;
                }

                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.Eliminar(int.Parse(txtId.Text));

                gvCategoria.DataSource = negocio.Listar();
                gvCategoria.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }
    }
}