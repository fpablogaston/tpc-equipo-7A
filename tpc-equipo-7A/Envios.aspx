<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Envios.aspx.cs" Inherits="tpc_equipo_7A.Envios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card p-4 w-50 mx-auto">
        <h2>Datos de envio</h2>

        <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

        <div class="mb-3">
            <label for="exampleFormControlInput1" class="form-label">Direccion</label>
            <asp:TextBox ID="txtDireccion" class="form-control" runat="server" placeholder="calle falsa 123"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label for="exampleFormControlInput1" class="form-label">Localidad</label>
            <asp:TextBox ID="txtLocalidad" class="form-control" runat="server" placeholder="Belen de escobar"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label for="exampleFormControlTextarea1" class="form-label">Informacion adicional</label>
            <asp:TextBox ID="txtInfoAdicional" class="form-control" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnContinuar" runat="server" CssClass="btn btn-primary" Text="Continuar al pago" OnClick="btnContinuar_Click" />
            <a href="CarritoPage.aspx" class="btn btn-secondary">Volver al carrito</a>
        </div>
    </div>

</asp:Content>
