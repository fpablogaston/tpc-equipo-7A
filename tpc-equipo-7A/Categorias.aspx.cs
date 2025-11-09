using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tpc_equipo_7A
{
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {
                if (!IsPostBack)
                {
                    //desplegable desde db
                    DropDownList1.DataSource = categoriaNegocio.Listar();
                    DropDownList1.DataTextField = "Descripcion";
                    DropDownList1.DataValueField = "Id";
                    DropDownList1.DataBind();

                    DropDownList1.Items.Insert(0, new ListItem("Seleccionar Categoria", "0"));
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }   
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idCategoria = DropDownList1.SelectedValue;
            Response.Redirect("Productos.aspx?idCategoria=" + idCategoria);
        }

    }   
}