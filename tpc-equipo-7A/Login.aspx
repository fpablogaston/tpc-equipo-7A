<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="tpc_equipo_7A.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <!-- login -->
  <div class="d-flex justify-content-center align-items-center" style="min-height:80vh;">
    <div class="card p-4 shadow" style="width:350px;">
      <h3 class="text-center mb-4">Iniciar Sesión</h3>

      <div class="form-floating mb-3">
        <input type="text" class="form-control" id="txtLoginUser" placeholder="usuario">
        <label for="txtLoginUser">Usuario</label>
      </div>

      <div class="form-floating mb-4">
        <input type="password" class="form-control" id="txtLoginPass" placeholder="contraseña">
        <label for="txtLoginPass">Contraseña</label>
      </div>

      <button type="button" class="btn btn-primary w-100 mb-3" id="btnIngresarClient">Iniciar sesión</button>

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
          <div class="mb-3">
            <label class="form-label">Email</label>
            <input type="email" class="form-control" id="FCEmail" placeholder="@ejemplo.com">
          </div>
            <div class="mb-3">
             <label class="form-label">Nombre</label>
             <input type="text" class="form-control" id="floatNombre">
          </div>
          <div class="mb-3">
                <label class="form-label">Apellido</label>
                <input type="text" class="form-control" id="floatApellido">
          </div>
          <div class="mb-3">
            <label class="form-label">Teléfono</label>
            <input type="text" class="form-control" id="floatTelefono">
          </div>
          <div class="mb-3">
            <label class="form-label">Dirección</label>
            <input type="text" class="form-control" id="floatDireccion">
          </div>
          <div class="form-floating mb-3">
            <input type="text" class="form-control" id="floatUser" placeholder="Usuario">
            <label class="form-label">Usuario</label>
          </div>
          <div class="form-floating mb-3">
            <input type="password" class="form-control" id="floatPass" placeholder="Contraseña">
            <label for="floatPass">Contraseña</label>
          </div>
        </div>

        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
          <button type="button" class="btn btn-primary" id="btnRegistrarClient">Registrarme</button>
        </div>

      </div>
    </div>
  </div>

</asp:Content>

