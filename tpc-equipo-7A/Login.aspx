<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="tpc_equipo_7A.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <!-- login -->
  <div class="d-flex justify-content-center align-items-center" style="min-height:80vh;">
    <div class="card p-4 shadow" style="width:350px;">
      <h3 class="text-center mb-4">Iniciar Sesión</h3>

      <div class="form-group">
        <asp:Label Text="Usuario" runat="server" />
        <asp:TextBox ID="txtLoginUser" CssClass="form-control" runat="server" />
    </div>

    <div class="form-group">
        <asp:Label Text="Contraseña" runat="server" />
        <asp:TextBox ID="txtLoginPass" TextMode="Password" CssClass="form-control" runat="server" />
    </div>

    <asp:Button ID="btnLogin" Text="Ingresar" CssClass="btn btn-primary"
                runat="server" OnClick="btnLogin_Click" />

    <asp:Label ID="lblError" runat="server" CssClass="mt-3 d-block text-danger" />

      <!-- reg -->
      <button type="button" class="btn btn-outline-secondary w-100" data-bs-toggle="modal" data-bs-target="#registroModal">
        Registrarme
      </button>
    </div>
  </div>

  <!-- modal -->
  <div class="modal fade" id="registroModal" tabindex="-1" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">

        <div class="modal-header">
          <h5 class="modal-title">Bienvenido :)</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>

        <div class="modal-body">
            <div class="form-group">
                <asp:Label Text="Email" runat="server" />
                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" />
            </div>
            <div class="form-group mb-3">
                <asp:Label Text="Nombre" runat="server" />
                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" />
            </div>
            <div class="form-group mb-3">
                <asp:Label Text="Apellido" runat="server" />
                <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server" />
            </div>
            <div class="form-group">
                <asp:Label Text="Teléfono" runat="server" />
                <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server" />
            </div>
            <div class="form-group">
                <asp:Label Text="Dirección" runat="server" />
                <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server" />
            </div>
            <div class="form-group">
                <asp:Label Text="Usuario" runat="server" />
                <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server" />
            </div>
            <div class="form-group">
                <asp:Label Text="Contraseña" runat="server" />
                <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server" />
            </div>
            </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
          <asp:Button ID="btnRegistrar" Text="Registrarme" CssClass="btn btn-primary"
                      runat="server" OnClick="btnRegistrar_Click" />

            <asp:Label ID="lblResultado" runat="server" CssClass="mt-3 d-block text-success" />
        </div>
      </div>
    </div>
  </div>

</asp:Content>

