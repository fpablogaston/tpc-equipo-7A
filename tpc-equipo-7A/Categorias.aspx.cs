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
    public partial class Categorias : System.Web.UI.Page
    {
        public List<Producto> ListaProductos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }

        protected void btnDetalle_Click(object sender, EventArgs e)
        {
            string valor = ((Button)sender).CommandArgument;
            Response.Redirect("DetalleProducto.aspx");
        }

        private void LoadProducts()
        {
            if(Request.QueryString["id"] == null)
                Response.Redirect("Default.aspx");

            else
            {
                try 
                {
                    int id = Int32.Parse(Request.QueryString["id"]);

                    ProductoNegocio negocio = new ProductoNegocio();
                    ListaProductos = negocio.Listar();

                    if (id != 0)
                    {
                        ListaProductos = ListaProductos.FindAll(x => x.Categoria.Id == id);
                    }

                    repProducto.DataSource = ListaProductos;
                    repProducto.DataBind();
                }
                catch (Exception)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }
}