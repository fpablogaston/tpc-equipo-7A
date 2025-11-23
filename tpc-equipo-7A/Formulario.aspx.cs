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
            phPedido.Visible = false;
            phEnvio.Visible = false;

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
                case "Pedido":
                    phPedido.Visible = true;
                    lblFormTitulo.Text = "Detalle de Pedido";
                    break;
                case "Envio":
                    phEnvio.Visible = true;
                    lblFormTitulo.Text = IdEntidad != 0 ? "Modificar Envio" : "Nuevo Envio";
                    BindPedidosDropdown();
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
                            txtProductoPrecio.Text = producto.Precio.ToString();
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
                            txtFechaEnvio.Text = envio.FechaEnvio.ToString("yyyy-MM-dd");
                            txtEstadoEnvio.Text = envio.Estado;
                            if (envio.FechaEntrega != null) txtFechaEntrega.Text = envio.FechaEntrega.Value.ToString("yyyy-MM-dd");
                            ddlEnvioPedido.SelectedValue = envio.IdPedido.ToString();
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
                Direccion = "",
                Contraseña = "",
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
            envio.Estado = txtEstadoEnvio.Text;
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
        }
        protected void txtProductoImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgProducto.ImageUrl = !string.IsNullOrEmpty(txtProductoImagenUrl.Text) ? txtProductoImagenUrl.Text : "https://placehold.co/600x400?text=No+Image";
        }
    }
}