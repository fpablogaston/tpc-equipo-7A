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
    public partial class Formulario : System.Web.UI.Page
    {
        private string Entidad { get; set; }
        private int IdEntidad { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Entidad = Request.QueryString["entity"];
            int idEntidadTemp;
            if (int.TryParse(Request.QueryString["id"], out idEntidadTemp))
            {
                IdEntidad = idEntidadTemp;
            }
            else
            {
                IdEntidad = 0;
            }

            if (string.IsNullOrEmpty(Entidad))
            {
                Response.Redirect("PanelAdmin.aspx");
            }

            if (!IsPostBack)
            {
                MostrarFormulario();

                if (Entidad == "Producto")
                    txtProductoId.Enabled = false;

                if (Entidad == "Categoria")
                    txtCategoriaId.Enabled = false;

                if (Entidad == "Cliente")
                    txtClienteId.Enabled = false;

                if (Entidad == "Pago")
                    txtPagoId.Enabled = false;

                if (Entidad == "Pedido")
                    txtPedidoId.Enabled = false; 

                if (IdEntidad != 0)
                {
                    CargarDatos();
                }
            }
        }

        private void MostrarFormulario()
        {
            phProducto.Visible = false;
            phCategoria.Visible = false;
            phCliente.Visible = false;
            phPago.Visible = false;
            phEnvio.Visible = false;
            phPedido.Visible = false;

            switch (Entidad)
            {
                case "Producto":
                    phProducto.Visible = true;
                    lblFormTitulo.Text = IdEntidad != 0 ? "Modificar Producto" : "Nuevo Producto";
                    BindCategoriasDropdown();
                    break;
                case "Categoria":
                    phCategoria.Visible = true;
                    lblFormTitulo.Text = IdEntidad != 0 ? "Modificar Categoría" : "Nueva Categoría";
                    break;
                case "Cliente":
                    phCliente.Visible = true;
                    lblFormTitulo.Text = IdEntidad != 0 ? "Modificar Cliente" : "Nuevo Cliente";
                    break;
                case "Pago":
                    phPago.Visible = true;
                    lblFormTitulo.Text = IdEntidad != 0 ? "Modificar Pago" : "Nuevo Pago";
                    BindPedidosDropdown();
                    break;
                case "Envio":
                    phEnvio.Visible = true;
                    lblFormTitulo.Text = IdEntidad != 0 ? "Modificar Envio" : "Nuevo Envio";
                    BindPedidosDropdown();
                    break;
                case "Pedido":
                    phPedido.Visible = true;
                    lblFormTitulo.Text = IdEntidad != 0 ? "Modificar Pedido" : "Nuevo Pedido";
                    BindPedidos();
                    break;
                default:
                    Response.Redirect("PanelAdmin.aspx");
                    break;
            }
        }

        private void CargarDatos()
        {
            try
            {
                switch (Entidad)
                {
                    case "Producto":
                        ProductoNegocio pNegocio = new ProductoNegocio();
                        Producto producto = pNegocio.GetById(IdEntidad);
                        if (producto != null)
                        {
                            txtProductoId.Text = producto.Id.ToString();
                            txtProductoNombre.Text = producto.Nombre;
                            txtProductoDescripcion.Text = producto.Descripcion;
                            txtProductoPrecio.Text = producto.Precio.ToString("N2", new System.Globalization.CultureInfo("es-AR"));
                            txtProductoStock.Text = producto.Stock.ToString();
                            txtProductoImagenUrl.Text = producto.ImagenUrl;
                            imgProducto.ImageUrl = !string.IsNullOrEmpty(producto.ImagenUrl) ? producto.ImagenUrl : "https://placehold.co/600x400?text=No+Image";
                            ddlProductoCategoria.SelectedValue = producto.Categoria.Id.ToString();
                        }
                        break;
                    case "Categoria":
                        CategoriaNegocio cNegocio = new CategoriaNegocio();
                        Categoria categoria = cNegocio.GetById(IdEntidad);
                        if (categoria != null)
                        {
                            txtCategoriaId.Text = categoria.Id.ToString();
                            txtCategoriaNombre.Text = categoria.Nombre;
                            txtCategoriaDescripcion.Text = categoria.Descripcion;
                        }
                        break;
                    case "Cliente":
                        ClienteNegocio clNegocio = new ClienteNegocio();
                        Cliente cliente = clNegocio.GetById(IdEntidad);
                        if (cliente != null)
                        {
                            txtClienteId.Text = cliente.Id.ToString();
                            txtClienteNombre.Text = cliente.Nombre;
                            txtClienteApellido.Text = cliente.Apellido;
                            txtClienteEmail.Text = cliente.Email;
                            txtClienteTelefono.Text = cliente.Telefono;
                        }
                        break;
                    case "Envio":
                        EnvioNegocio eNegocio = new EnvioNegocio();
                        Envio envio = eNegocio.GetById(IdEntidad);
                        if (envio != null)
                        {
                            txtEnvioId.Text = envio.Id.ToString();
                            txtDireccionEnvio.Text = envio.DireccionEnvio;
                            txtCiudad.Text = envio.Ciudad;
                            txtProvincia.Text = envio.Provincia;
                            txtCodigoPostal.Text = envio.CodigoPostal;
                            if (envio.FechaEnvio != null) txtFechaEnvio.Text = envio.FechaEnvio.Value.ToString("yyyy-MM-dd");
                            txtEstadoEnvio.Text = envio.EstadoDescripcion;
                            if (envio.FechaEntrega != null) txtFechaEntrega.Text = envio.FechaEntrega.Value.ToString("yyyy-MM-dd");
                            ddlEnvioPedido.SelectedValue = envio.IdPedido.ToString();
                        }
                        break;

                    case "Pago":
                        PagoNegocio negocio = new PagoNegocio();
                        Pago pago = negocio.GetById(IdEntidad);
                        if (pago != null)
                        {
                            txtPagoId.Text = pago.Id.ToString();
                            txtMetodoPago.Text = pago.MetodoPago.Nombre;
                            txtEstadoPago.Text = pago.Estado.Nombre;
                            txtMonto.Text = pago.Monto.ToString("N2", new System.Globalization.CultureInfo("es-AR"));
                            txtFechaPago.Text = pago.FechaPago.ToString("yyyy-MM-dd");
                            ddlPagoPedido.SelectedValue = pago.IdPedido.ToString();
                        }
                        break;

                    case "Pedido":
                        PedidoNegocio pdoNegocio = new PedidoNegocio();
                        Pedido pedido = pdoNegocio.GetById(IdEntidad);
                        if (pedido != null)
                        {
                            txtPedidoId.Text = pedido.Id.ToString();
                            txtPedidoFecha.Text = pedido.FechaPedido.ToString("yyyy-MM-dd");
                            txtPedidoTotal.Text = pedido.Total.ToString("N2", new System.Globalization.CultureInfo("es-AR"));
                            txtPedidoEstado.Text = pedido.Estado.ToString();

                            ddlPedidoCliente.SelectedValue = pedido.Cliente.Id.ToString();
                            ddlPedidoPago.SelectedValue = pedido.Pago.Id.ToString();
                            ddlPedidoEnvio.SelectedValue = pedido.Envio.Id.ToString();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            try
            {
                switch (Entidad)
                {
                    case "Producto":
                        GuardarProducto();
                        break;
                    case "Categoria":
                        GuardarCategoria();
                        break;
                    case "Cliente":
                        GuardarCliente();
                        break;
                    case "Envio":
                        GuardarEnvio();
                        break;
                    case "Pago":
                        GuardarPago();
                        break;
                    case "Pedido":
                        GuardarPedido();
                        break;
                }
                Response.Redirect("PanelAdmin.aspx");
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
        private void GuardarProducto()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            Producto producto = new Producto
            {
                Id = IdEntidad,
                Nombre = txtProductoNombre.Text,
                Descripcion = txtProductoDescripcion.Text,
                Precio = decimal.Parse(txtProductoPrecio.Text),
                Stock = int.Parse(txtProductoStock.Text),
                ImagenUrl = txtProductoImagenUrl.Text,
                Categoria = new CategoriaNegocio().GetById(int.Parse(ddlProductoCategoria.SelectedValue))
            };

            if (IdEntidad != 0)
                negocio.Actualizar(producto);
            else
                negocio.Agregar(producto);
        }
        private void GuardarPago()
        {
            PagoNegocio negocio = new PagoNegocio();
            Pago pago = new Pago
            {
                Id = IdEntidad,
                MetodoPago = new MetodoPago { Id = 1, Nombre = txtMetodoPago.Text },
                Estado = new EstadoPago { Id = 1, Nombre = txtEstadoPago.Text },
                Monto = decimal.Parse(txtMonto.Text),
                FechaPago = DateTime.Parse(txtFechaPago.Text),
                IdPedido = int.Parse(ddlPagoPedido.SelectedValue)
            };
            if (IdEntidad != 0)
                negocio.Actualizar(pago);
            else
                negocio.Agregar(pago);
        }
        private void GuardarCategoria()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria categoria = new Categoria
            {
                Id = IdEntidad,
                Nombre = txtCategoriaNombre.Text,
                Descripcion = txtCategoriaDescripcion.Text
            };

            if (IdEntidad != 0)
                negocio.Actualizar(categoria);
            else
                negocio.Agregar(categoria);
        }
        private void GuardarCliente()
        {
            ClienteNegocio negocio = new ClienteNegocio();

            Cliente cliente = new Cliente
            {
                Id = IdEntidad,
                Nombre = txtClienteNombre.Text,
                Apellido = txtClienteApellido.Text,
                Email = txtClienteEmail.Text,
                Telefono = txtClienteTelefono.Text,
                //Direccion = ,
                //Password = "",
                FechaRegistro = (IdEntidad == 0) ? DateTime.Now : new ClienteNegocio().GetById(IdEntidad).FechaRegistro,
            };

            if (IdEntidad != 0)
                negocio.Actualizar(cliente);
            else
                negocio.Agregar(cliente);
        }
        private void GuardarEnvio()
        {
            EnvioNegocio negocio = new EnvioNegocio();
            Envio envio = new Envio();

            envio.Id = IdEntidad;
            envio.DireccionEnvio = txtDireccionEnvio.Text;
            envio.Ciudad = txtCiudad.Text;
            envio.Provincia = txtProvincia.Text;
            envio.CodigoPostal = txtCodigoPostal.Text;
            envio.IdEstadoEnvio = negocio.ListarEstados()
                                         .First(x => x.Value == txtEstadoEnvio.Text).Key;
            envio.IdPedido = int.Parse(ddlEnvioPedido.SelectedValue);
            envio.FechaEnvio = DateTime.Parse(txtFechaEnvio.Text);
            DateTime fechaEntrega;
            if (DateTime.TryParse(txtFechaEntrega.Text, out fechaEntrega))
                envio.FechaEntrega = fechaEntrega;
            else
                envio.FechaEntrega = null;

            if (IdEntidad != 0)
                negocio.Actualizar(envio);
            else
                negocio.Agregar(envio);
        }

        private void GuardarPedido()
        {
            PedidoNegocio negocio = new PedidoNegocio();

            Pedido pedido = new Pedido
            {
                Id = IdEntidad,
                Cliente = new ClienteNegocio().GetById(int.Parse(ddlPedidoCliente.SelectedValue)),
                FechaPedido = DateTime.Parse(txtPedidoFecha.Text),
                Total = decimal.Parse(txtPedidoTotal.Text),
                Estado = new EstadoPedido { Id = 1, Descripcion = txtPedidoEstado.Text },
                Pago = new PagoNegocio().GetById(int.Parse(ddlPedidoPago.SelectedValue)),
                Envio = new EnvioNegocio().GetById(int.Parse(ddlPedidoEnvio.SelectedValue)),
            };

            if (IdEntidad != 0)
                negocio.Actualizar(pedido);
            else
                negocio.Agregar(pedido);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelAdmin.aspx");
        }
        // --- Helpers Producto ---
        private void BindCategoriasDropdown()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            ddlProductoCategoria.DataSource = negocio.Listar();
            ddlProductoCategoria.DataTextField = "Nombre";
            ddlProductoCategoria.DataValueField = "Id";
            ddlProductoCategoria.DataBind();
            ddlProductoCategoria.Items.Insert(0, new ListItem("Seleccionar Categoría", "0"));
        }
        private void BindPedidosDropdown()
        {
            PedidoNegocio negocio = new PedidoNegocio();

            ddlEnvioPedido.DataSource = negocio.Listar();
            ddlEnvioPedido.DataTextField = "Id";
            ddlEnvioPedido.DataValueField = "Id";
            ddlEnvioPedido.DataBind();
            ddlEnvioPedido.Items.Insert(0, new ListItem("Seleccionar Pedido", "0"));

            ddlPagoPedido.DataSource = negocio.Listar();
            ddlPagoPedido.DataTextField = "Id";
            ddlPagoPedido.DataValueField = "Id";
            ddlPagoPedido.DataBind();
            ddlPagoPedido.Items.Insert(0, new ListItem("Seleccionar Pedido", "0"));
        }
        protected void txtProductoImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgProducto.ImageUrl = !string.IsNullOrEmpty(txtProductoImagenUrl.Text) ? txtProductoImagenUrl.Text : "https://placehold.co/600x400?text=No+Image";
        }

        private void BindPedidos()
        {
            ClienteNegocio clienteNeg = new ClienteNegocio();
            ddlPedidoCliente.DataSource = clienteNeg.Listar();
            ddlPedidoCliente.DataTextField = "Nombre";
            ddlPedidoCliente.DataValueField = "Id";
            ddlPedidoCliente.DataBind();
            ddlPedidoCliente.Items.Insert(0, new ListItem("Seleccionar Cliente", "0"));

            PagoNegocio pagoNeg = new PagoNegocio();
            ddlPedidoPago.DataSource = pagoNeg.Listar();
            ddlPedidoPago.DataTextField = "MetodoPagoNombre";
            ddlPedidoPago.DataValueField = "Id";
            ddlPedidoPago.DataBind();
            ddlPedidoPago.Items.Insert(0, new ListItem("Seleccionar Pago", "0"));

            EnvioNegocio envioNeg = new EnvioNegocio();
            ddlPedidoEnvio.DataSource = envioNeg.Listar();
            ddlPedidoEnvio.DataTextField = "DireccionEnvio";
            ddlPedidoEnvio.DataValueField = "Id";
            ddlPedidoEnvio.DataBind();
            ddlPedidoEnvio.Items.Insert(0, new ListItem("Seleccionar Envío", "0"));
        }

    }
}