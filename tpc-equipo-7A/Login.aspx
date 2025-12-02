<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="tpc_equipo_7A.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-center align-items-center" style="min-height: 80vh;">
        <div class="card p-4 shadow" style="width: 350px;">
            <h3 class="text-center mb-4">Iniciar Sesión</h3>

            <div class="mb-3">
                <label class="form-label">Usuario</label>
                <asp:TextBox ID="txtLoginUser" CssClass="form-control" runat="server" />
            </div>

            <div class="mb-3">
                <label class="form-label">Contraseña</label>
                <asp:TextBox ID="txtLoginPass" TextMode="Password" CssClass="form-control" runat="server" />
            </div>
            
            <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger d-block p-2 mb-3" Visible="false" />

            <div class="d-grid gap-2">
                <asp:Button ID="btnLogin" Text="Ingresar" CssClass="btn btn-primary" runat="server" OnClick="btnLogin_Click" />
                <button type="button" class="btn btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#registroModal">
                    Registrarme
                </button>
            </div>
        </div>
    </div>

    <div class="modal fade" id="registroModal" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Crear Cuenta</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row g-3">
                            
                            <%-- Columna Izquierda --%>
                            <div class="col-md-6">
                                <div class="mb-2">
                                    <label class="form-label">Email</label>
                                    <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" TextMode="Email" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtEmail" ErrorMessage="Requerido" CssClass="text-danger small" ValidationGroup="Registro" runat="server" Display="Dynamic" />
                                </div>
                                <div class="mb-2">
                                    <label class="form-label">Nombre</label>
                                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtNombre" ErrorMessage="Requerido" CssClass="text-danger small" ValidationGroup="Registro" runat="server" Display="Dynamic" />
                                </div>
                                <div class="mb-2">
                                    <label class="form-label">Apellido</label>
                                    <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtApellido" ErrorMessage="Requerido" CssClass="text-danger small" ValidationGroup="Registro" runat="server" Display="Dynamic" />
                                </div>
                                <div class="mb-2">
                                    <label class="form-label">Teléfono</label>
                                    <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtTelefono" ErrorMessage="Requerido" CssClass="text-danger small" ValidationGroup="Registro" runat="server" Display="Dynamic" />
                                </div>
                            </div>

                            <%-- Columna Derecha --%>
                            <div class="col-md-6">
                                <div class="mb-2">
                                    <label class="form-label">Usuario</label>
                                    <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtUsuario" ErrorMessage="Requerido" CssClass="text-danger small" ValidationGroup="Registro" runat="server" Display="Dynamic" />
                                </div>
                                <div class="mb-2">
                                    <label class="form-label">Contraseña</label>
                                    <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtPassword" ErrorMessage="Requerido" CssClass="text-danger small" ValidationGroup="Registro" runat="server" Display="Dynamic" />
                                </div>
                                
                                <hr class="my-3 text-muted" />
                                <h6 class="text-primary mb-3">Dirección Principal</h6>

                                <div class="mb-2">
                                    <label class="form-label">Calle y Altura</label>
                                    <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtDireccion" ErrorMessage="Requerido" CssClass="text-danger small" ValidationGroup="Registro" runat="server" Display="Dynamic" />
                                </div>
                                <div class="row">
                                    <div class="col-6 mb-2">
                                        <label class="form-label">Ciudad</label>
                                        <asp:TextBox ID="txtCiudad" CssClass="form-control" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtCiudad" ErrorMessage="*" CssClass="text-danger small" ValidationGroup="Registro" runat="server" Display="Dynamic" />
                                    </div>
                                    <div class="col-6 mb-2">
                                        <label class="form-label">CP</label>
                                        <asp:TextBox ID="txtCodigoPostal" CssClass="form-control" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtCodigoPostal" ErrorMessage="*" CssClass="text-danger small" ValidationGroup="Registro" runat="server" Display="Dynamic" />
                                    </div>
                                </div>
                                <div class="mb-2">
                                    <label class="form-label">Provincia</label>
                                    <asp:TextBox ID="txtProvincia" CssClass="form-control" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtProvincia" ErrorMessage="Requerido" CssClass="text-danger small" ValidationGroup="Registro" runat="server" Display="Dynamic" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnRegistrar" Text="Crear Cuenta" CssClass="btn btn-success" runat="server" OnClick="btnRegistrar_Click" ValidationGroup="Registro" />
                </div>
                <div class="px-3 pb-3 text-center">
                    <asp:Label ID="lblResultado" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <div class="toast-container position-fixed bottom-0 end-0 p-3">
        <div id="toastRegistro" class="toast align-items-center text-bg-success border-0" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="d-flex">
                <div class="toast-body">
                    ¡Registro exitoso! Ya puedes iniciar sesión.
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
        </div>
    </div>

    <script>
        function mostrarToastRegistro() {
            // Close modal first
            var modalEl = document.getElementById('registroModal');
            var modal = bootstrap.Modal.getInstance(modalEl);
            if (modal) modal.hide();

            // Show toast
            const toastEl = document.getElementById('toastRegistro');
            const toast = new bootstrap.Toast(toastEl);
            toast.show();
        }
    </script>

</asp:Content>