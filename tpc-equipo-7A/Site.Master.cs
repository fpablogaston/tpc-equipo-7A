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
                Cliente cliente = Session["cliente"] as Cliente;

                if (cliente == null)
                {
                    phLogin.Visible = true;
                    phUser.Visible = false;
                    phAdmin.Visible = false;
                    phLogout.Visible = false;
                }
                else
                {
                    phUser.Visible = true;
                    phLogout.Visible = true;
                    phLogin.Visible = false;
                    phAdmin.Visible = false;

                    if (cliente.Rol == 2) // Admin
                    {
                        phAdmin.Visible = true;
                        phLogout.Visible = true;
                        phLogin.Visible = false;
                        lblUser.Text = "Administrador";
                    }
                    else
                    {
                        phAdmin.Visible = false;
                        lblUser.Text = cliente.Nombre;
                    }
                }


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

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["cliente"] = null;
            Response.Redirect("Default.aspx"); 
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
            UpdateBurbujaCarrito();
        }

        protected void repCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EliminarItem")
            {
                int idProducto = int.Parse(e.CommandArgument.ToString());

                var carrito = new negocio.CarritoNegocio();
                carrito.Eliminar(idProducto);  

                LoadCarrito();          
                UpdateTotals();         
                UpdateBurbujaCarrito(); 
            }
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

        public void UpdateBurbujaCarrito()
        {
            int total = carritoNegocio.ObtenerItems().Sum(x => x.Cantidad);

            badgeCarrito.InnerText = total.ToString();

            var up = (UpdatePanel)FindControl("UPBadge");
            if (up != null)
                up.Update();
        }




    }



}

