using dominio;
using negocio;
using System;
using System.Collections;
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

                    var listaOrdenada = pedidos
                       .Select(p => new
                       {
                           Pedido = p,
                           PrioridadEstado =
                               p.Envio.IdEstadoEnvio == 5 ? 3 :
                               p.Envio.IdEstadoEnvio == 4 ? 2 :
                               1
                       })
                        .OrderBy(x => x.PrioridadEstado)
                        .ThenByDescending(x => x.Pedido.FechaPedido)
                        .Select(x => x.Pedido)
                        .ToList();

                    gvCompras.DataSource = listaOrdenada;
                    gvCompras.DataBind();

                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                }
            }
        }

        protected void gvCompras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Pedido pedido = (Pedido)e.Row.DataItem;

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
                    iconEnvio.Attributes["class"] = "bi bi-shop";
                    lblTipoEnvio.Text = "Retiro";

                    if (pedido.Estado.Id >= 6) // 
                        pnlEnvio.CssClass += " step-completed";
                    else if (pedido.Estado.Id >= 3) // 
                        pnlEnvio.CssClass += " step-active"; // 
                    else
                        pnlEnvio.CssClass += " step-pending";
                }
                else
                {
                    iconEnvio.Attributes["class"] = "bi bi-truck";
                    lblTipoEnvio.Text = "Envío";

                    if (pedido.Envio.IdEstadoEnvio == 4)
                        pnlEnvio.CssClass += " step-completed";
                    else if (pedido.Envio.IdEstadoEnvio == 2 || pedido.Envio.IdEstadoEnvio == 3)
                        pnlEnvio.CssClass += " step-active";
                    else if (pedido.Envio.IdEstadoEnvio == 1)
                        pnlEnvio.CssClass += " step-warning";
                    else
                        pnlEnvio.CssClass += " step-pending";
                }

                HtmlGenericControl lblEstado = (HtmlGenericControl)e.Row.FindControl("lblEstado");

                if (pedido.Envio.EstadoDescripcion == "Entregado")
                {
                    lblEstado.InnerText = "Cerrado";
                    lblEstado.Attributes["class"] = "badge bg-success";
                }
                else if (pedido.Envio.EstadoDescripcion == "Cancelado")
                {
                    lblEstado.InnerText = "Cancelado";
                    lblEstado.Attributes["class"] = "badge bg-danger text-dark";
                }
                else
                {
                    lblEstado.InnerText = "Abierto";
                    lblEstado.Attributes["class"] = "badge bg-warning text-dark";
                }
            }
        }
    }
}