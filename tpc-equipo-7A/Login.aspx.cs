using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tpc_equipo_7A
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loginOK"] != null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastLoginOk", "mostrarToastLogin();", true);
                Session.Remove("loginOK");
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente nuevo = new Cliente();
                nuevo.Email = txtEmail.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Apellido = txtApellido.Text;
                nuevo.Telefono = txtTelefono.Text;
                nuevo.Direccion = txtDireccion.Text;
                nuevo.Usuario = txtUsuario.Text;
                nuevo.Password = txtPassword.Text;

                ClienteNegocio negocio = new ClienteNegocio();
                negocio.Agregar(nuevo);

                lblResultado.Text = "Te registraste correctamente.";
            }
            catch (Exception ex)
            {
                lblResultado.CssClass = "text-danger";
                lblResultado.Text = "Error: " + ex.Message;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ClienteNegocio negocio = new ClienteNegocio();
            Cliente cliente = negocio.Login(txtLoginUser.Text, txtLoginPass.Text);

            if (cliente != null)
            {
                Session["cliente"] = cliente;
                Session["loginOK"] = "1";
                Response.Redirect("Default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                lblError.Text = "Usuario o contraseña incorrectos.";
                lblError.CssClass = "text-danger";
            }
        }


    }
}