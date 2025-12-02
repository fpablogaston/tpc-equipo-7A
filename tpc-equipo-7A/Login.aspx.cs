using dominio;
using negocio;
using System;
using System.Web.UI;

namespace tpc_equipo_7A
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cliente"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            Page.Validate("Registro");
            if (!Page.IsValid) return;

            ClienteNegocio negocioVerificacion = new ClienteNegocio();

            // 1. Validar si el usuario ya existe
            if (negocioVerificacion.ExisteUsuario(txtUsuario.Text))
            {
                lblResultado.CssClass = "text-danger";
                lblResultado.Text = "El nombre de usuario ya existe.";
                MostrarModal();
                return;
            }

            // 2. Validar si el email ya existe
            if (negocioVerificacion.ExisteEmail(txtEmail.Text))
            {
                lblResultado.CssClass = "text-danger";
                lblResultado.Text = "El email ya está registrado.";
                MostrarModal();
                return;
            }

            try
            {
                Cliente nuevo = new Cliente();

                // Datos Personales
                nuevo.Nombre = txtNombre.Text;
                nuevo.Apellido = txtApellido.Text;
                nuevo.Email = txtEmail.Text;
                nuevo.Telefono = txtTelefono.Text;
                nuevo.Usuario = txtUsuario.Text;
                nuevo.Password = txtPassword.Text; // Recuerda hashear esto en producción

                // Datos de Dirección (Mapeo a la nueva estructura)
                nuevo.DireccionSeleccionada = new Direccion
                {
                    Calle = txtDireccion.Text,
                    Ciudad = txtCiudad.Text,
                    Provincia = txtProvincia.Text,
                    CodigoPostal = txtCodigoPostal.Text,
                    Alias = "Principal" // Asignamos un alias por defecto al registrarse
                };

                // Guardar
                ClienteNegocio negocio = new ClienteNegocio();
                negocio.Agregar(nuevo);

                // Éxito
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastRegistro", "mostrarToastRegistro();", true);

                // Opcional: Loguear al usuario automáticamente
                // Cliente clienteLogueado = negocio.Login(nuevo.Usuario, nuevo.Password);
                // Session["cliente"] = clienteLogueado;
                // Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                lblResultado.CssClass = "text-danger";
                lblResultado.Text = "Error al registrar: " + ex.Message;
                MostrarModal();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteNegocio negocio = new ClienteNegocio();
                Cliente cliente = negocio.Login(txtLoginUser.Text, txtLoginPass.Text);

                if (cliente != null)
                {
                    Session["cliente"] = cliente;
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    lblError.Text = "Usuario o contraseña incorrectos.";
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
                lblError.Visible = true;
            }
        }

        private void MostrarModal()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal",
                "var myModal = new bootstrap.Modal(document.getElementById('registroModal')); myModal.show();", true);
        }
    }
}