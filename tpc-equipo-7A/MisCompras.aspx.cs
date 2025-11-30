using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    var cliente = (Cliente)Session["cliente"];
                    int idCliente = cliente.Id;

                    PedidoNegocio pedidoNeg = new PedidoNegocio();
                    var pedidos = pedidoNeg.ListarPorCliente(idCliente);

                    var lista = new List<dynamic>();

                    foreach (var p in pedidos)
                    {
                        lista.Add(new
                        {
                            IdPedido = p.Id,
                            Total = p.Total,
                            FechaPedido = p.FechaPedido,
                            DireccionEnvio = p.Envio?.DireccionEnvio ?? "-",
                            Ciudad = p.Envio?.Ciudad ?? "-",
                            Provincia = p.Envio?.Provincia ?? "-",
                            EstadoEnvio = p.Envio?.EstadoDescripcion ?? "Pendiente",
                            MetodoPago = p.Pago?.MetodoPago?.Nombre ?? "-",
                            EstadoPago = p.Pago?.Estado?.Nombre ?? "-"
                        });
                    }

                    gvCompras.DataSource = lista.OrderByDescending(x => x.FechaPedido).ToList();
                    gvCompras.DataBind();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                }
            }
        }
    }
}