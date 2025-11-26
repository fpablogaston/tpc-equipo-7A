<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="tpc_equipo_7A.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- login -->
    <div class="d-flex justify-content-center align-items-center" style="min-height: 80vh;">
        <div class="card p-4 shadow" style="width: 350px;">
            <h3 class="text-center mb-4">Iniciar Sesión</h3>

            <div class="form-group mb-3">
                <asp:Label Text="Usuario" runat="server" />
                <asp:TextBox ID="txtLoginUser" CssClass="form-control" runat="server" />
            </div>

            <div class="form-group mb-3">
                <asp:Label Text="Contraseña" runat="server" />
                <asp:TextBox ID="txtLoginPass" TextMode="Password" CssClass="form-control" runat="server" />
            </div>
            <asp:Label ID="lblError" runat="server" CssClass="mt-3 d-block text-danger" />

            <asp:Button ID="btnLogin" Text="Ingresar" CssClass="form-group mb-3 btn btn-primary"
                runat="server" OnClick="btnLogin_Click" />
           
            <button type="button" class="form-group mb-3 btn btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#registroModal">
                Registrarme
            </button>
        </div>
    </div>

    <!-- modal -->
<div class="modal fade" id="registroModal" tabindex="-1" aria-hidden="true">
    <div class="modal-dialog modal-lg modal-dialog-scrollable">
        <div class="modal-content">

            <div class="modal-header">
                <h5 class="modal-title">Formulario de registro</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
            </div>

            <div class="modal-body">

                <div class="container-fluid">
                    <div class="row">


                        <div class="col-md-6">

                            <div class="form-group mb-3">
                                <asp:Label Text="Email" runat="server" />
                                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtEmail" ErrorMessage="Falta completar este campo."
                                    CssClass="text-danger" ValidationGroup="Registro" runat="server" />
                                <asp:RegularExpressionValidator ControlToValidate="txtEmail" ErrorMessage="Formato de email inválido"
                                    CssClass="text-danger" ValidationGroup="Registro" runat="server"
                                    ValidationExpression="^\S+@\S+\.\S+$" />
                            </div>

                            <div class="form-group mb-3">
                                <asp:Label Text="Nombre" runat="server" />
                                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtNombre"
                                    ErrorMessage="Falta completar este campo." CssClass="text-danger"
                                    ValidationGroup="Registro" runat="server" />
                            </div>

                            <div class="form-group mb-3">
                                <asp:Label Text="Apellido" runat="server" />
                                <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtApellido"
                                    ErrorMessage="Falta completar este campo." CssClass="text-danger"
                                    ValidationGroup="Registro" runat="server" />
                            </div>

                            <div class="form-group mb-3">
                                <asp:Label Text="Teléfono" runat="server" />
                                <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtTelefono"
                                    ErrorMessage="Falta completar este campo." CssClass="text-danger"
                                    ValidationGroup="Registro" runat="server" />
                                <asp:RegularExpressionValidator ControlToValidate="txtTelefono"
                                    ErrorMessage="Sólo números en el teléfono" CssClass="text-danger"
                                    ValidationGroup="Registro" runat="server" ValidationExpression="^[0-9]+$" />
                            </div>

                            <div class="form-group mb-3">
                                <asp:Label Text="Dirección" runat="server" />
                                <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtDireccion"
                                    ErrorMessage="Falta completar este campo." CssClass="text-danger"
                                    ValidationGroup="Registro" runat="server" />
                            </div>

                        </div>


                        <div class="col-md-6">

                            <div class="form-group mb-3">
                                <asp:Label Text="Ciudad" runat="server" />
                                <asp:TextBox ID="txtCiudad" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtCiudad"
                                    ErrorMessage="Falta completar este campo." CssClass="text-danger"
                                    ValidationGroup="Registro" runat="server" />
                            </div>

                            <div class="form-group mb-3">
                                <asp:Label Text="Provincia" runat="server" />
                                <asp:TextBox ID="txtProvincia" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtProvincia"
                                    ErrorMessage="Falta completar este campo." CssClass="text-danger"
                                    ValidationGroup="Registro" runat="server" />
                            </div>

                            <div class="form-group mb-3">
                                <asp:Label Text="Código Postal" runat="server" />
                                <asp:TextBox ID="txtCodigoPostal" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtCodigoPostal"
                                    ErrorMessage="Falta completar este campo." CssClass="text-danger"
                                    ValidationGroup="Registro" runat="server" />
                                <asp:RegularExpressionValidator ControlToValidate="txtCodigoPostal"
                                    ErrorMessage="Sólo números en el código postal" CssClass="text-danger"
                                    ValidationGroup="Registro" runat="server" ValidationExpression="^[0-9]+$" />
                            </div>

                            <div class="form-group mb-3">
                                <asp:Label Text="Usuario" runat="server" />
                                <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtUsuario"
                                    ErrorMessage="El usuario es obligatorio" CssClass="text-danger"
                                    ValidationGroup="Registro" runat="server" />
                            </div>

                            <div class="form-group mb-3">
                                <asp:Label Text="Contraseña" runat="server" />
                                <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtPassword"
                                    ErrorMessage="La contraseña es obligatoria" CssClass="text-danger"
                                    ValidationGroup="Registro" runat="server" />
                            </div>

                        </div>

                    </div>
                </div>

            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>

                <asp:Button ID="btnRegistrar" Text="Registrarme" CssClass="btn btn-primary"
                    runat="server" OnClick="btnRegistrar_Click" ValidationGroup="Registro" />

                <asp:Label ID="lblResultado" runat="server" />
            </div>

        </div>
    </div>
</div>



        <div class="toast-container position-fixed bottom-0 end-0 p-3">
          <div id="toastRegistro" class="toast align-items-center text-bg-success border-0" role="alert" aria-live="assertive" aria-atomic="true" data-bs-autohide="true" data-bs-delay="3000">
            <div class="d-flex">
              <div id="toastRegistroBody" class="toast-body">Te registraste correctamente.</div>
              <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
          </div>
        </div>

        <script>
            function mostrarToastRegistro() {
                const element = document.getElementById('toastRegistro');
                const toast = new bootstrap.Toast(element);
                toast.show();
            }
        </script>

</asp:Content>

