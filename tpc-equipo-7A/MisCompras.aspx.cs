using dominio;
using negocio;
using System;
using System.Collections;
using System.Collections.Generic;

namespace tpc_equipo_7A
{
    public partial class MisCompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var cliente = (Cliente)Session["cliente"];
                int idCliente = cliente.Id;

                PedidoNegocio pedidoNeg = new PedidoNegocio();
                EnvioNegocio envNeg = new EnvioNegocio();
                PagoNegocio pagoNeg = new PagoNegocio();

                var pedidos = pedidoNeg.ListarPorCliente(idCliente);

                var lista = new List<dynamic>();

                foreach (var p in pedidos)
                {
                    var envio = envNeg.GetById(p.Id);
                    var pago = pagoNeg.GetById(p.Id);

                    lista.Add(new
                    {
                        IdPedido = p.Id,
                        Total = p.Total,
                        FechaPedido = p.FechaPedido,
                        DireccionEnvio = envio?.DireccionEnvio,
                        Ciudad = envio?.Ciudad,
                        Provincia = envio?.Provincia,
                        EstadoEnvio = envio?.EstadoDescripcion,
                        MetodoPago = pago?.MetodoPago?.Nombre,
                        EstadoPago = pago?.Estado?.Nombre
                    });
                }

                gvCompras.DataSource = lista;
                gvCompras.DataBind();
            }
        }



    }
}
