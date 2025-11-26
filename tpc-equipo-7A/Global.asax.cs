using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace tpc_equipo_7A
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo tipomoneda = new System.Globalization.CultureInfo("es-AR");
            System.Threading.Thread.CurrentThread.CurrentCulture = tipomoneda;
            System.Threading.Thread.CurrentThread.CurrentUICulture = tipomoneda;

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var tipomoneda = new System.Globalization.CultureInfo("es-AR");
            System.Threading.Thread.CurrentThread.CurrentCulture = tipomoneda;
            System.Threading.Thread.CurrentThread.CurrentUICulture = tipomoneda;
        }

    }
}