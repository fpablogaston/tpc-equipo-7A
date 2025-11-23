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
    public partial class PanelAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cliente"] == null || ((Cliente)Session["cliente"]).IdUsuario != 1)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                MostrarVista("Inicio");
            }
        }
        protected void ddlSelectEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = ddlSelectEntity.SelectedValue;
            MostrarVista(seleccion);
        }
        private void MostrarVista(string vista)
        {
            phInicio.Visible = false;
            phCategorias.Visible = false;
            phProductos.Visible = false;
            phClientes.Visible = false;
            phPedidos.Visible = false;
            phPagos.Visible = false;
            phEnvios.Visible = false;

            switch (vista)
            {
                case "Categorias":
                    phCategorias.Visible = true;
                    BindCategoriasGrid();
                    break;
                case "Productos":
                    phProductos.Visible = true;
                    BindProductosGrid();
                    break;
                case "Clientes":
                    phClientes.Visible = true;
                    BindClientesGrid();
                    break;
                case "Pedidos":
                    phPedidos.Visible = true;
                    BindPedidosGrid();
                    break;
                case "Pagos":
                    phPagos.Visible = true;
                    BindPagosGrid();
                    break;
                case "Envios":
                    phEnvios.Visible = true;
                    BindEnviosGrid();
                    break;
                case "Inicio":
                default:
                    phInicio.Visible = true;
                    break;
            }
        }
        // --- CATEGORÍAS ---
        private void BindCategoriasGrid()
        {
            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                gvCategorias.DataSource = negocio.Listar();
                gvCategorias.DataBind();
            }
            catch (Exception ex)
            {
                lblMensajeCategoria.Text = "Error al cargar categorías: " + ex.Message;
                lblMensajeCategoria.CssClass = "text-danger";
            }
        }
        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formulario.aspx?entity=Categoria");
        }
        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Response.Redirect($"Formulario.aspx?entity=Categoria&id={id}");
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.Eliminar(id);
                    lblMensajeCategoria.Text = "Categoría eliminada.";
                    lblMensajeCategoria.CssClass = "text-success";
                    BindCategoriasGrid();
                }
                catch (Exception ex)
                {
                    lblMensajeCategoria.Text = "Error al eliminar categoría: " + ex.Message;
                    lblMensajeCategoria.CssClass = "text-danger";
                }
            }
        }
        // --- PRODUCTOS ---
        private void BindProductosGrid()
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                gvProductos.DataSource = negocio.Listar();
                gvProductos.DataBind();
            }
            catch (Exception ex)
            {
                lblMensajeProducto.Text = "Error al cargar productos: " + ex.Message;
                lblMensajeProducto.CssClass = "text-danger";
            }
        }
        protected void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formulario.aspx?entity=Producto");
        }
        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Response.Redirect($"Formulario.aspx?entity=Producto&id={id}");
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    ProductoNegocio negocio = new ProductoNegocio();
                    negocio.Eliminar(id);
                    lblMensajeProducto.Text = "Producto eliminado.";
                    lblMensajeProducto.CssClass = "text-success";
                    BindProductosGrid();
                }
                catch (Exception ex)
                {
                    lblMensajeProducto.Text = "Error al eliminar producto: " + ex.Message;
                    lblMensajeProducto.CssClass = "text-danger";
                }
            }
        }
        // --- CLIENTES ---
        private void BindClientesGrid()
        {
            try
            {
                ClienteNegocio negocio = new ClienteNegocio();
                gvClientes.DataSource = negocio.Listar();
                gvClientes.DataBind();
            }
            catch (Exception ex)
            {
                lblMensajeCliente.Text = "Error al cargar clientes: " + ex.Message;
                lblMensajeCliente.CssClass = "text-danger";
            }
        }
        protected void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formulario.aspx?entity=Cliente");
        }
        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Response.Redirect($"Formulario.aspx?entity=Cliente&id={id}");
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    ClienteNegocio negocio = new ClienteNegocio();
                    negocio.Eliminar(id);
                    lblMensajeCliente.Text = "Cliente eliminado.";
                    lblMensajeCliente.CssClass = "text-success";
                    BindClientesGrid();
                }
                catch (Exception ex)
                {
                    lblMensajeCliente.Text = "Error al eliminar cliente: " + ex.Message;
                    lblMensajeCliente.CssClass = "text-danger";
                }
            }
        }
        // --- PEDIDOS ---
        private void BindPedidosGrid()
        {
            try
            {
                PedidoNegocio negocio = new PedidoNegocio();
                gvPedidos.DataSource = negocio.Listar();
                gvPedidos.DataBind();
            }
            catch (Exception ex)
            {
                lblMensajePedido.Text = "Error al cargar pedidos: " + ex.Message;
                lblMensajePedido.CssClass = "text-danger";
            }
        }
        protected void gvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Editar")
            {
                Response.Redirect($"Formulario.aspx?entity=Pedido&id={id}");
            }
        }
        // --- PAGOS ---
        private void BindPagosGrid()
        {
            try
            {
                PagoNegocio negocio = new PagoNegocio();
                gvPagos.DataSource = negocio.Listar();
                gvPagos.DataBind();
            }
            catch (Exception ex)
            {
                lblMensajePago.Text = "Error al cargar pagos: " + ex.Message;
                lblMensajePago.CssClass = "text-danger";
            }
        }
        protected void btnNuevoPago_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formulario.aspx?entity=Pago");
        }
        protected void gvPagos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Response.Redirect($"Formulario.aspx?entity=Pago&id={id}");
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    EnvioNegocio negocio = new EnvioNegocio();
                    negocio.Eliminar(id);
                    lblMensajePago.Text = "Pago eliminado.";
                    lblMensajePago.CssClass = "text-success";
                    BindPagosGrid();
                }
                catch (Exception ex)
                {
                    lblMensajeEnvio.Text = "Error al eliminar pago: " + ex.Message;
                    lblMensajeEnvio.CssClass = "text-danger";
                }
            }
        }
        // --- ENVIOS ---
        private void BindEnviosGrid()
        {
            try
            {
                EnvioNegocio negocio = new EnvioNegocio();
                gvEnvios.DataSource = negocio.Listar();
                gvEnvios.DataBind();
            }
            catch (Exception ex)
            {
                lblMensajeEnvio.Text = "Error al cargar envíos: " + ex.Message;
                lblMensajeEnvio.CssClass = "text-danger";
            }
        }
        protected void btnNuevoEnvio_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formulario.aspx?entity=Envio");
        }
        protected void gvEnvios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Response.Redirect($"Formulario.aspx?entity=Envio&id={id}");
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    EnvioNegocio negocio = new EnvioNegocio();
                    negocio.Eliminar(id);
                    lblMensajeEnvio.Text = "Envio eliminado.";
                    lblMensajeEnvio.CssClass = "text-success";
                    BindEnviosGrid();
                }
                catch (Exception ex)
                {
                    lblMensajeEnvio.Text = "Error al eliminar envio: " + ex.Message;
                    lblMensajeEnvio.CssClass = "text-danger";
                }
            }
        }
    }
}