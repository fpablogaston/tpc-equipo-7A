using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tpc_equipo_7A
{
    public partial class DetalleProducto : System.Web.UI.Page
    {
        public Producto ProductoActual { get; set; }
        private int IdProducto;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["id"] == null || !int.TryParse(Request.QueryString["id"], out IdProducto))
                {
                    Response.Redirect("Default.aspx", false);
                    return;
                }

                if (!IsPostBack)
                {
                    LoadProducto();
                    txtCantidad.Text = "1";
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Default.aspx");
            }
        }

        private void LoadProducto()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            ProductoActual = negocio.GetById(IdProducto);

            if (ProductoActual == null || ProductoActual.Id == 0)
            {
                Response.Redirect("Default.aspx", false);
                return;
            }

            Page.DataBind();
            ActualizarBadgeStock();
        }

        private void ActualizarBadgeStock()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            int stock = negocio.GetById(IdProducto).Stock;

            if (stock <= 0)
            {
                lblStock.Text = "Sin Stock";
                lblStock.CssClass = "stock-badge badge rounded-pill bg-danger";
                btnAgregarCarrito.Enabled = false;
                btnMas.Enabled = false;
                btnMenos.Enabled = false;
            }
            else if (stock < 10)
            {
                lblStock.Text = $"¡Solo quedan {stock}!";
                lblStock.CssClass = "stock-badge badge rounded-pill bg-warning text-dark";
                btnAgregarCarrito.Enabled = true;
            }
            else
            {
                lblStock.Text = "En Stock";
                lblStock.CssClass = "stock-badge badge rounded-pill bg-success";
                btnAgregarCarrito.Enabled = true;
            }
        }

        protected void btnMas_Click(object sender, EventArgs e)
        {
            int cantidad = int.Parse(txtCantidad.Text);
            cantidad++;
            txtCantidad.Text = cantidad.ToString();
            ValidarCantidad();
        }

        protected void btnMenos_Click(object sender, EventArgs e)
        {
            int cantidad = int.Parse(txtCantidad.Text);
            if (cantidad > 1)
            {
                cantidad--;
                txtCantidad.Text = cantidad.ToString();
            }
            ValidarCantidad();
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            ValidarCantidad();
        }

        private void ValidarCantidad()
        {
            int cantidad;
            if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad < 1)
            {
                txtCantidad.Text = "1";
                cantidad = 1;
            }

            // Aquí agregarías la validación contra el stock si lo deseas
            // Ejemplo:
            // if (cantidad > ProductoActual.Stock)
            // {
            //     txtCantidad.Text = ProductoActual.Stock.ToString();
            // }
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                int cantidad = int.Parse(txtCantidad.Text);
                if (cantidad <= 0) return;

                ProductoNegocio negocio = new ProductoNegocio();
                Producto producto = negocio.GetById(IdProducto);

                Carrito carrito;
                if (Session["carrito"] != null)
                {
                    carrito = (Carrito)Session["carrito"];
                }
                else
                {
                    carrito = new Carrito();
                }

                carrito.AgregarProducto(producto, cantidad);
                Session["carrito"] = carrito;

                if (this.Master is Site master)
                {
                    master.LoadCarrito();
                    master.UpdateTotals();
                }

                lblMensaje.Text = $"{cantidad} x {producto.Nombre} agregado(s) al carrito!";
                string script = "var myOffcanvas = new bootstrap.Offcanvas(document.getElementById('offcanvasExample')); myOffcanvas.show();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowCart", script, true);

                ActualizarBadgeStock();

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
    }
}