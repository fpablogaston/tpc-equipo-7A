using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tpc_equipo_7A
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public void LoadCarrito()
        {
            Carrito carrito = new Carrito();
            if (Session["carrito"] != null)
            {
                carrito = (Carrito)Session["carrito"];
            }
            repCarrito.DataSource = carrito.ListaCarrito;
            repCarrito.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categorias = new CategoriaNegocio();
            PedidoNegocio pedidos = new PedidoNegocio();
            ClienteNegocio clientes = new ClienteNegocio();

            try
            {
                UpdateTotals();
                if (!IsPostBack)
                {
                    // Cargar categorías dinámicamente
                    List<Categoria> listaCategorias = categorias.Listar();
                    ddlCategorias.DataSource = listaCategorias;
                    ddlCategorias.DataTextField = "Nombre"; // Ajustado a propiedad de Categoria
                    ddlCategorias.DataValueField = "Id";
                    ddlCategorias.DataBind();

                    // Agregar opción por defecto
                    ddlCategorias.Items.Insert(0, new ListItem("Categorías", "0"));
                }

                if (Session.Count > 0) LoadCarrito();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // throw ex; // Comentado para no romper ejecución en desarrollo si falla algo no critico
            }
        }

        public void UpdateTotals()
        {
            if (Session["carrito"] != null)
            {
                lblTotalPrice.Text = ((Carrito)Session["carrito"]).ListaCarrito.Sum(item => item.Cantidad * item.Producto.Precio).ToString("N2");
                lblTotalPrice.DataBind();

                lblTotalItems.Text = ((Carrito)Session["carrito"]).ListaCarrito.Sum(item => item.Cantidad).ToString();
                lblTotalItems.DataBind();
            }
            else
            {
                lblTotalItems.Text = "0";
                lblTotalPrice.Text = "0.00";
            }
        }

        protected void btnVerCarrito_Click(object sender, EventArgs e)
        {
            Response.Redirect("CarritoPage.aspx");
        }

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Redireccionar a productos filtrados por categoría
            string id = ddlCategorias.SelectedValue;
            if (id != "0")
            {
                Response.Redirect("Categorias.aspx?id=" + id);
            }
        }
    }
}