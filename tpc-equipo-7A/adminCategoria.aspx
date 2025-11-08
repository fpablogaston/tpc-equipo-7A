<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="adminCategoria.aspx.cs" Inherits="tpc_equipo_7A.adminCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-center mb-5">
        <div class="col-4">
            <asp:GridView ID="gvCategoria" runat="server" CssClass="table table-striped"></asp:GridView>
        </div>
    </div>

    <div class="d-flex justify-content-center">
        <div class="col-5">
            <div class="mb-3">
                <label for="txtId" class="form-label">ID</label>
                <asp:TextBox runat="server" ID="txtId" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtId" class="form-label">Nombre</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtId" class="form-label">Descripcion</label>
                <asp:TextBox runat="server" ID="TxtDescripcion" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="d-flex gap-2">
                <asp:Button ID="btnAgregar" OnClick="btnAgregar_Click" runat="server" Text="Agregar" CssClass="btn btn-dark" />
                <asp:Button ID="btnModificar" OnClick="btnModificar_Click" runat="server" Text="Modificar" CssClass="btn btn-dark" />
                <asp:Button ID="btnEliminar" OnClick="btnEliminar_Click" runat="server" Text="Eliminar" CssClass="btn btn-danger" />
            </div>
        </div>
    </div>

</asp:Content>
