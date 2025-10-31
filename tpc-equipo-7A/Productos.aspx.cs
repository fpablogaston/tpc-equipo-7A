using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace tpc_equipo_7A
{
    public partial class Productos : System.Web.UI.Page
    {
        public List<Producto> ListaProductos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            ListaProductos = negocio.Listar();

            if (!IsPostBack)
            {
            repProducto.DataSource = ListaProductos;
            repProducto.DataBind();
            }
        }

        protected void btnDetalle_Click(object sender, EventArgs e)
        {
            string valor = ((Button)sender).CommandArgument;
            Response.Redirect("DetalleProducto.aspx");
        }
    }
}