using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tpc_equipo_7A
{
    public partial class CarritoPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ///agrego esto a carritopage.aspx.cs
            if (!IsPostBack)
                CargarCarrito();
        }

        /// agrego esto a carritopage.aspx.cs
        private void CargarCarrito()
        {
            Carrito carrito = Session["carrito"] as Carrito;

            if (carrito != null)
            {
                //repCarrito.DataSource = carrito.Items; esto se modifico 
                repCarrito.DataSource = carrito.ListaCarrito;
                repCarrito.DataBind();

                lblTotal.Text = carrito.Total().ToString("C");
            }
        }

        ///agrego esto tambien a carritopage.aspx.cs
        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Envios.aspx");
        }
    }
}