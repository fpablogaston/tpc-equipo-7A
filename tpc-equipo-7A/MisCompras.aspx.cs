using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace tpc_equipo_7A
{
    public partial class MisCompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cliente"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                try
                {
                    Cliente cliente = (Cliente)Session["cliente"];
                    int idCliente = cliente.Id;

                    PedidoNegocio pedidoNeg = new PedidoNegocio();
                    List<Pedido> pedidos = pedidoNeg.ListarPorCliente(idCliente);

                    // Sort by newest first
                    gvCompras.DataSource = pedidos.OrderByDescending(x => x.FechaPedido).ToList();
                    gvCompras.DataBind();
                }
                catch (Exception ex)
                {
                    // In a real app, log error or show user friendly message
                    Session.Add("error", ex.ToString());
                }
            }
        }

        protected void gvCompras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Pedido pedido = (Pedido)e.Row.DataItem;

                // --- VISUAL LOGIC FOR "PO DIAGRAM" ---

                // 1. PAGO
                Panel pnlPago = (Panel)e.Row.FindControl("pnlPagoIcon");
                Panel pnlCashWarning = (Panel)e.Row.FindControl("pnlCashWarning");

                bool isCash = pedido.EsPagoEfectivo;
                bool isPaid = pedido.Pago != null && pedido.Pago.Estado.Nombre == "Aprobado";

                if (isPaid)
                {
                    pnlPago.CssClass += " step-completed"; // Green Check
                }
                else if (isCash)
                {
                    pnlPago.CssClass += " step-warning"; // Yellow Warning
                    pnlCashWarning.Visible = true;
                }
                else
                {
                    pnlPago.CssClass += " step-pending"; // Grey
                }

                // 2. LOGISTICS (ENVIO / RETIRO)
                Panel pnlEnvio = (Panel)e.Row.FindControl("pnlEnvioIcon");
                HtmlGenericControl iconEnvio = (HtmlGenericControl)e.Row.FindControl("iconEnvio");
                Label lblTipoEnvio = (Label)e.Row.FindControl("lblTipoEnvio");

                bool isPickup = pedido.EsRetiroEnTienda;

                if (isPickup)
                {
                    // It's a Store Pickup
                    iconEnvio.Attributes["class"] = "bi bi-shop";
                    lblTipoEnvio.Text = "Retiro";

                    // Logic based on Order Status IDs (Standardized)
                    // 3 = En Preparacion, 5 = Listo para Retiro, 6 = Entregado
                    if (pedido.Estado.Id >= 6) // Finalizado/Entregado
                        pnlEnvio.CssClass += " step-completed";
                    else if (pedido.Estado.Id >= 3) // Being Prepared or Ready
                        pnlEnvio.CssClass += " step-active"; // Blue
                    else
                        pnlEnvio.CssClass += " step-pending";
                }
                else
                {
                    // It's a Delivery
                    iconEnvio.Attributes["class"] = "bi bi-truck";
                    lblTipoEnvio.Text = "Envío";

                    // 4 = En Camino, 6 = Entregado
                    if (pedido.Estado.Id >= 6) // Delivered
                        pnlEnvio.CssClass += " step-completed";
                    else if (pedido.Estado.Id == 4) // On the way
                        pnlEnvio.CssClass += " step-active"; // Blue
                    else
                        pnlEnvio.CssClass += " step-pending";
                }
            }
        }
    }
}