using dominio;
using negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tpc_equipo_7A
{
    public partial class Productos : System.Web.UI.Page
    {
        public List<Producto> ListaProductos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            string idCategoria =  Request.QueryString["idCategoria"];

            ProductoNegocio negocio = new ProductoNegocio();
            ListaProductos = negocio.Listar();

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(idCategoria))
                {
                    ListaProductos = ListaProductos.FindAll(x => x.Categoria.Id.ToString() == idCategoria);
                }

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