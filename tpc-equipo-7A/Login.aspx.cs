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
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            ClienteNegocio negocioVerificacion = new ClienteNegocio();

            if(negocioVerificacion.ExisteUsuario(txtUsuario.Text))
            {
                lblResultado.CssClass = "text-danger";
                lblResultado.Text = "El nombre de usuario ya existe. Por favor, elija otro.";

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                "ShowModal", @"
                    var myModal = new bootstrap.Modal(document.getElementById('registroModal'));
                    myModal.show();
                ", true);

                return;
            }

            try
            {
                Cliente nuevo = new Cliente();
                nuevo.Direccion = new Direccion();
                nuevo.Email = txtEmail.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Apellido = txtApellido.Text;
                nuevo.Telefono = txtTelefono.Text;
                nuevo.Direccion.Calle = txtDireccion.Text;
                nuevo.Direccion.Ciudad = txtCiudad.Text;
                nuevo.Direccion.CodigoPostal = txtCodigoPostal.Text;   
                nuevo.Direccion.Provincia = txtProvincia.Text;
                nuevo.Usuario = txtUsuario.Text;
                nuevo.Password = txtPassword.Text;
                ClienteNegocio negocio = new ClienteNegocio();
                negocio.Agregar(nuevo);

                ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "toastRegistro",
                "setTimeout(mostrarToastRegistro, 500);",
                true);


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
                Session["Login"] = 1; 
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