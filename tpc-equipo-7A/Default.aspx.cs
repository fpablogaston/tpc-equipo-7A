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
            repRepetidor.DataSource = new ProductoNegocio().Listar();
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
                Producto producto = new ProductoNegocio().GetById(idProducto);

                Carrito carrito = new Carrito();
                if (Session["carrito"] != null)
                {
                    carrito = (Carrito)Session["carrito"];

                    carrito.AgregarProducto(producto, 1);
                }
                else
                {
                    carrito.ListaCarrito = carrito.AgregarProducto(producto, 1);
                    Session.Add("carrito", carrito);
                }
                if (this.Master is Site master)
                {
                    master.LoadCarrito();
                    master.UpdateTotals();
                }
                LoadProductos();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                // Redirigir a error si necesario
            }
        }
    }
}