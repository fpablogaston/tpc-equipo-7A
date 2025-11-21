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
                    lblFormTitulo.Text = "Modificar Envío";
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
                Categoria = new Categoria { Id = int.Parse(ddlProductoCategoria.SelectedValue) }
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

        protected void txtProductoImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgProducto.ImageUrl = !string.IsNullOrEmpty(txtProductoImagenUrl.Text) ? txtProductoImagenUrl.Text : "https://placehold.co/600x400?text=No+Image";
        }
    }
}