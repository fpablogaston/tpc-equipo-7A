using dominio;
using negocio;
using System;
using System.Collections.Generic;

namespace tpc_equipo_7A
{
    public partial class CompraExitosa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["carrito"] == null || Session["pedido"] == null)
            {
                Response.Redirect("Default.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                try
                {
                    // 1. Recuperar objetos de la sesión antes de borrarlos
                    Carrito carrito = (Carrito)Session["carrito"];
                    Envio envio = (Envio)Session["envio"];
                    Pago pago = (Pago)Session["pago"];
                    Pedido pedido = (Pedido)Session["pedido"];

                    // 2. Mostrar datos del Pedido
                    lblPedido.Text = "Pedido #" + pedido.Id.ToString();

                    // Si el objeto pedido no tiene fecha (bug posible del negocio), usamos Hoy
                    DateTime fecha = pedido.FechaPedido != DateTime.MinValue ? pedido.FechaPedido : DateTime.Now;
                    lblFecha.Text = fecha.ToString("dd/MM/yyyy HH:mm");

                    // Si el objeto pedido no tiene total, usamos el del carrito
                    decimal total = pedido.Total > 0 ? pedido.Total : carrito.Total();
                    lblTotal.Text = total.ToString("N2");

                    // 3. Mostrar Dirección de Envío
                    if (envio != null)
                    {
                        // Chequeamos si es retiro en tienda (Id 6 o descripción)
                        if (envio.IdEstadoEnvio == 6 || envio.DireccionEnvio == "Retiro en Local")
                        {
                            lblEnvio.Text = "Retiro en Tienda (Av. Siempreviva 742)";
                        }
                        else
                        {
                            lblEnvio.Text = $"{envio.DireccionEnvio}, {envio.Ciudad} ({envio.CodigoPostal})";
                        }
                    }

                    // 4. Mostrar Método de Pago
                    if (pago != null)
                    {
                        lblPago.Text = pago.MetodoPago.Nombre;
                    }

                    // 5. Cargar la lista de productos (Esto faltaba)
                    if (carrito != null && carrito.ListaCarrito.Count > 0)
                    {
                        repResumen.DataSource = carrito.ListaCarrito;
                        repResumen.DataBind();
                    }

                    // 6. Limpiar la sesión (Compra finalizada)
                    Session["carrito"] = null; // Vaciar carrito
                    Session["envio"] = null;
                    Session["pago"] = null;
                    Session["pedido"] = null;
                }
                catch (Exception ex)
                {
                    lblError.Visible = true;
                    lblError.Text = "Ocurrió un error al cargar el resumen: " + ex.Message;
                }
            }
        }
    }
}