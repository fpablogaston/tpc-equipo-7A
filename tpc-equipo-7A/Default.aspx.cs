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
    public partial class Default : System.Web.UI.Page
    {
        public List<Producto> ListaProductos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProductos();
            }
        }

        private void LoadProductos()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            ListaProductos = negocio.Listar();
            repRepetidor.DataSource = ListaProductos;
            repRepetidor.DataBind();
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            string idProducto = ((Button)sender).CommandArgument;
            AgregarAlCarrito(int.Parse(idProducto));
        }

        private void AgregarAlCarrito(int idProducto)
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                Producto producto = negocio.GetById(idProducto);

                Carrito carrito;
                if (Session["carrito"] != null)
                {
                    carrito = (Carrito)Session["carrito"];
                }
                else
                {
                    carrito = new Carrito();
                }

                carrito.AgregarProducto(producto, 1);
                Session["carrito"] = carrito;

                // Update Master Page cart totals if needed (assuming Master page has methods for this)
                // if (this.Master is Site masterPage)
                // {
                //     masterPage.UpdateCartUI(); 
                // }

                // Optional: Show success message or update UI
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                // Response.Redirect("Error.aspx", false); 
            }
        }
    }
}