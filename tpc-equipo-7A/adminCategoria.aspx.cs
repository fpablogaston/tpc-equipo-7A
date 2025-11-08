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
                //dominio.Categoria baja = new dominio.Categoria();
                //baja.Id = int.Parse(txtId.Text);

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