using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tpc_equipo_7A
{
    public partial class Site : MasterPage
    {
        private readonly CarritoNegocio carritoNegocio = new CarritoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categorias = new CategoriaNegocio();

            try
            {
                if (!IsPostBack)
                {

                    // cargar categorías
                    ddlCategorias.DataSource = categorias.Listar();
                    ddlCategorias.DataTextField = "Nombre";
                    ddlCategorias.DataValueField = "Id";
                    ddlCategorias.DataBind();
                    ddlCategorias.Items.Insert(0, new ListItem("Categorías", "0"));
                    LoadCarrito();
                }

                UpdateTotals();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void LoadCarrito()
        {
            var carrito = carritoNegocio.ObtenerItems();

            repCarrito.DataSource = carrito;
            repCarrito.DataBind();
        }

        public void UpdateTotals()
        {
            lblTotalItems.Text = carritoNegocio.ObtenerItems().Sum(x => x.Cantidad).ToString();
            lblTotalPrice.Text = carritoNegocio.Total().ToString("N2");
            badgeCarrito.InnerText = carritoNegocio.ObtenerItems().Sum(x => x.Cantidad).ToString();

        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            RepeaterItem item = (RepeaterItem)txt.NamingContainer;

            HiddenField hf = (HiddenField)item.FindControl("hfIdProducto");
            int idProducto = int.Parse(hf.Value);

            int nuevaCantidad = 1;
            int.TryParse(txt.Text, out nuevaCantidad);

            var carrito = new negocio.CarritoNegocio();
            carrito.ModificarCantidad(idProducto, nuevaCantidad);

            LoadCarrito();
            UpdateTotals();
        }

        protected void repCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var cn = new negocio.CarritoNegocio();
            int id = int.Parse(e.CommandArgument.ToString());

            switch (e.CommandName)
            {
                /*case "CambiarCantidad":
                    TextBox txt = (TextBox)e.Item.FindControl("txtCantidad");
                    int nuevaCant = int.Parse(txt.Text);
                    cn.ModificarCantidad(id, nuevaCant);  
                break;
                */
                case "EliminarItem":
                    cn.Eliminar(id);
                    break;
            }

            LoadCarrito();
            UpdateTotals();
        }

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategorias.SelectedValue != "0")
            {
                Response.Redirect("Categorias.aspx?id=" + ddlCategorias.SelectedValue);
            }
        }

        protected void btnVerCarrito_Click(object sender, EventArgs e)
        {
            Response.Redirect("CarritoPage.aspx");
        }



    }
}
