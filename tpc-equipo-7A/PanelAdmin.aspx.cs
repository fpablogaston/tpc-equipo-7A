using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace tpc_equipo_7A
{
    public partial class PanelAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check for admin access (Rol 2 is Admin)
            if (Session["cliente"] == null || ((Cliente)Session["cliente"]).Rol != 2)
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

        // --- PEDIDOS (NEW PO LOGIC) ---
        private void BindPedidosGrid()
        {
            try
            {
                PedidoNegocio negocio = new PedidoNegocio();
                var lista = negocio.Listar().OrderByDescending(x => x.FechaPedido).ToList();
                Session["listaPedidos"] = lista;
                gvPedidos.DataSource = lista;
                gvPedidos.DataBind();
            }
            catch (Exception ex)
            {
                lblMensajePedido.Text = "Error al cargar pedidos: " + ex.Message;
                lblMensajePedido.CssClass = "text-danger";
            }
        }

        protected void gvPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Pedido pedido = (Pedido)e.Row.DataItem;

                // 1. Populate DropDownList for Status
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlCambiarEstado");
                if (ddl != null)
                {
                    PedidoNegocio negocio = new PedidoNegocio();
                    ddl.DataSource = negocio.ListarEstados();
                    ddl.DataTextField = "Descripcion";
                    ddl.DataValueField = "Id";
                    ddl.DataBind();
                    ddl.SelectedValue = pedido.Estado.Id.ToString();
                }

                // 2. VISUAL LOGIC FOR "DIAGRAM" (PO)

                // --- PAGO ---
                Panel pnlPago = (Panel)e.Row.FindControl("pnlPagoIcon");
                Panel pnlCashWarning = (Panel)e.Row.FindControl("pnlCashWarning");

                bool isCash = pedido.EsPagoEfectivo;
                // Assuming logic: if Payment State is "Aprobado" then it's paid.
                bool isPaid = pedido.Pago != null && pedido.Pago.Estado.Nombre == "Aprobado";

                if (isPaid)
                {
                    pnlPago.CssClass += " step-completed"; // Green
                }
                else if (isCash)
                {
                    pnlPago.CssClass += " step-warning"; // Yellow (User request)
                    pnlCashWarning.Visible = true;
                }
                else
                {
                    pnlPago.CssClass += " step-pending"; // Grey
                }

                // --- ENVIO / RETIRO ---
                Panel pnlEnvio = (Panel)e.Row.FindControl("pnlEnvioIcon");
                HtmlGenericControl iconEnvio = (HtmlGenericControl)e.Row.FindControl("iconEnvio");
                Label lblTipoEnvio = (Label)e.Row.FindControl("lblTipoEnvio");

                bool isPickup = pedido.EsRetiroEnTienda;

                if (isPickup)
                {
                    iconEnvio.Attributes["class"] = "bi bi-shop"; // Change icon to Shop
                    lblTipoEnvio.Text = "Retiro";

                    // Logic based on Order Status ID (Assuming 5 is 'Listo para Retiro')
                    if (pedido.Estado.Id >= 5)
                        pnlEnvio.CssClass += " step-completed";
                    else if (pedido.Estado.Id >= 3) // Preparing
                        pnlEnvio.CssClass += " step-active"; // Blue
                    else
                        pnlEnvio.CssClass += " step-pending";
                }
                else
                {
                    iconEnvio.Attributes["class"] = "bi bi-truck"; // Standard Truck

                    if (pedido.Estado.Id >= 6) // Delivered
                        pnlEnvio.CssClass += " step-completed";
                    else if (pedido.Estado.Id == 4) // En Camino
                        pnlEnvio.CssClass += " step-active"; // Blue
                    else
                        pnlEnvio.CssClass += " step-pending";
                }
            }
        }

        protected void gvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ActualizarEstado")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvPedidos.Rows[index];

                int idPedido = (int)gvPedidos.DataKeys[index].Value;
                DropDownList ddl = (DropDownList)row.FindControl("ddlCambiarEstado");
                int nuevoEstadoId = int.Parse(ddl.SelectedValue);

                PedidoNegocio negocio = new PedidoNegocio();
                negocio.ActualizarEstado(idPedido, nuevoEstadoId);

                // Refresh grid
                BindPedidosGrid();
            }
            else if (e.CommandName == "VerDetalle")
            {
                string id = e.CommandArgument.ToString();
                Response.Redirect("Formulario.aspx?entity=Pedido&id=" + id);
            }
        }

        protected void txtFiltroPedido_TextChanged(object sender, EventArgs e)
        {
            var lista = (List<Pedido>)Session["listaPedidos"];
            if (lista != null)
            {
                string filtro = txtFiltroPedido.Text.ToUpper();
                var filtrada = lista.FindAll(x =>
                    x.Id.ToString().Contains(filtro) ||
                    x.Cliente.Nombre.ToUpper().Contains(filtro) ||
                    x.Estado.Descripcion.ToUpper().Contains(filtro)
                );
                gvPedidos.DataSource = filtrada;
                gvPedidos.DataBind();
            }
        }

        // --- CATEGORÍAS ---
        private void BindCategoriasGrid()
        {
            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                var lista = negocio.Listar();
                Session["listaCategorias"] = lista;
                gvCategorias.DataSource = lista;
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
            else if (e.CommandName == "Inactivar")
            {
                try
                {
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.Eliminar(id); // Activo = 0
                    lblMensajeCategoria.Text = "Categoría inactivada.";
                    lblMensajeCategoria.CssClass = "text-success";
                    BindCategoriasGrid();
                }
                catch (Exception ex)
                {
                    lblMensajeCategoria.Text = "Error al inactivar categoría: " + ex.Message;
                    lblMensajeCategoria.CssClass = "text-danger";
                }
            }
            else if (e.CommandName == "Reactivar")
            {
                try
                {
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.Habilitar(id); // Activo = 1
                    lblMensajeCategoria.Text = "Categoría reactivada.";
                    lblMensajeCategoria.CssClass = "text-success";
                    BindCategoriasGrid();
                }
                catch (Exception ex)
                {
                    lblMensajeCategoria.Text = "Error al reactivar categoría: " + ex.Message;
                    lblMensajeCategoria.CssClass = "text-danger";
                }
            }
        }

        protected void txtFiltroCategorias_TextChanged(object sender, EventArgs e)
        {
            List<Categoria> lista = (List<Categoria>)Session["listaCategorias"];
            if (lista != null)
            {
                List<Categoria> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltroCategorias.Text.ToUpper()));
                gvCategorias.DataSource = listaFiltrada;
                gvCategorias.DataBind();
            }
        }

        // --- PRODUCTOS ---
        private void BindProductosGrid()
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                var lista = negocio.Listar();
                Session["listaProductos"] = lista;
                gvProductos.DataSource = lista;
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

        protected void txtFiltroProducto_TextChanged(object sender, EventArgs e)
        {
            List<Producto> lista = (List<Producto>)Session["listaProductos"];
            if (lista != null)
            {
                var filtrada = lista.FindAll(x =>
                  x.Nombre.ToUpper().Contains(txtFiltroProducto.Text.ToUpper()) ||
                     x.Categoria.Nombre.ToUpper().Contains(txtFiltroProducto.Text.ToUpper()));
                gvProductos.DataSource = filtrada;
                gvProductos.DataBind();
            }
        }

        // --- CLIENTES ---
        private void BindClientesGrid()
        {
            try
            {
                ClienteNegocio negocio = new ClienteNegocio();
                var lista = negocio.Listar();
                Session["listaClientes"] = lista;
                gvClientes.DataSource = lista;
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

        protected void txtFiltroCliente_TextChanged(object sender, EventArgs e)
        {
            var lista = (List<Cliente>)Session["listaClientes"];
            if (lista != null)
            {
                var filtrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltroCliente.Text.ToUpper()) ||
                    x.Apellido.ToUpper().Contains(txtFiltroCliente.Text.ToUpper())
                );
                gvClientes.DataSource = filtrada;
                gvClientes.DataBind();
            }
        }

        // --- PAGOS ---
        private void BindPagosGrid()
        {
            try
            {
                PagoNegocio negocio = new PagoNegocio();
                var lista = negocio.Listar();
                Session["listaPagos"] = lista;
                gvPagos.DataSource = lista;
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
                    PagoNegocio negocio = new PagoNegocio();
                    negocio.Eliminar(id);
                    lblMensajePago.Text = "Pago eliminado.";
                    lblMensajePago.CssClass = "text-success";
                    BindPagosGrid();
                }
                catch (Exception ex)
                {
                    lblMensajePago.Text = "Error al eliminar pago: " + ex.Message;
                    lblMensajePago.CssClass = "text-danger";
                }
            }
        }

        protected void txtFiltroPago_TextChanged(object sender, EventArgs e)
        {
            var lista = (List<Pago>)Session["listaPagos"];
            if (lista != null)
            {
                string filtro = txtFiltroPago.Text.ToUpper();
                var filtrada = lista.FindAll(x =>
                    x.IdPedido.ToString().Contains(filtro) ||
                    (x.EstadoNombre ?? "").ToUpper().Contains(filtro)
                );
                gvPagos.DataSource = filtrada;
                gvPagos.DataBind();
            }
        }

        // --- ENVIOS ---
        private void BindEnviosGrid()
        {
            try
            {
                EnvioNegocio negocio = new EnvioNegocio();
                var lista = negocio.Listar();
                Session["listaEnvios"] = lista;
                gvEnvios.DataSource = lista;
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
            if (e.CommandName == "ActualizarEstado")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvEnvios.Rows[index];

                DropDownList ddl = (DropDownList)row.FindControl("ddlEstadoEnvio");
                string nuevoEstado = ddl.SelectedValue;

                int idEnvio = (int)gvEnvios.DataKeys[index].Value;

                try
                {
                    EnvioNegocio negocio = new EnvioNegocio();
                    Envio envio = negocio.GetById(idEnvio);
                    envio.IdEstadoEnvio = negocio.ListarEstados()
                                         .First(x => x.Value == nuevoEstado).Key;
                    envio.EstadoDescripcion = nuevoEstado;

                    if (nuevoEstado == "Cancelado")
                    {
                        envio.FechaEnvio = null;
                        envio.FechaEntrega = null;
                    }
                    if (nuevoEstado == "En Camino" && envio.FechaEnvio == null)
                    {
                        envio.FechaEnvio = DateTime.Now;
                        envio.FechaEntrega = null;
                    }
                    if (nuevoEstado == "Entregado" && envio.FechaEntrega == null)
                    {
                        envio.FechaEntrega = DateTime.Now;
                    }
                    negocio.Actualizar(envio);

                    lblMensajeEnvio.Text = $"Estado actualizado a '{nuevoEstado}' para el envío #{idEnvio}.";
                    lblMensajeEnvio.CssClass = "text-success";

                    BindEnviosGrid();
                }
                catch (Exception ex)
                {
                    lblMensajeEnvio.Text = "Error al actualizar estado: " + ex.Message;
                    lblMensajeEnvio.CssClass = "text-danger";
                }
            }
            else
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

        protected void gvEnvios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlEstadoEnvio");
                HiddenField hf = (HiddenField)e.Row.FindControl("hfEstadoActual");

                if (ddl != null && hf != null)
                {
                    if (ddl.Items.FindByValue(hf.Value) != null)
                    {
                        ddl.SelectedValue = hf.Value;
                    }
                }
            }
        }

        protected void txtFiltroEnvio_TextChanged(object sender, EventArgs e)
        {
            var lista = (List<Envio>)Session["listaEnvios"];
            if (lista != null)
            {
                string filtro = txtFiltroEnvio.Text.ToUpper();
                var filtrada = lista.FindAll(x =>
                    x.IdPedido.ToString().Contains(filtro) ||
                    (x.EstadoDescripcion ?? "").ToUpper().Contains(filtro)
                );
                gvEnvios.DataSource = filtrada;
                gvEnvios.DataBind();
            }
        }
    }
}