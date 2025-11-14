using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace tpc_equipo_7A
{
    public partial class Pagos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PagoNegocio negocio = new PagoNegocio();
                repMetodos.DataSource = negocio.ListarMetodos();
                repMetodos.DataBind();
            }
        }
    }
}