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

            if (Session["loginOK"] != null)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "toastLogin",
                    "setTimeout(mostrarToastLogin, 300);",
                    true);

                Session.Remove("loginOK");
            }

            if (!IsPostBack)
            {
                ProductoNegocio negocio = new ProductoNegocio();
                List<Producto> lista = negocio.Listar();

                repRepetidor.DataSource = lista;
                repRepetidor.DataBind();
                Session["ListaProductos"] = lista;
            }
        }

        private void LoadProductos()
        {
            repRepetidor.DataSource = new ProductoNegocio().Listar();
            repRepetidor.DataBind();
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);

            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            TextBox txtCant = (TextBox)item.FindControl("txtCantidad");
            int cantidad = 1;
            if (txtCant != null) int.TryParse(txtCant.Text, out cantidad);
            if (cantidad <= 0) cantidad = 1;

            var cn = new negocio.CarritoNegocio();
            cn.Agregar(id, cantidad);

            if (this.Master is Site master)
            {
                master.LoadCarrito();
                master.UpdateTotals();
            }
        }





        /*private void AgregarAlCarrito(int idProducto)
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
            }
        }
        */
    }
}