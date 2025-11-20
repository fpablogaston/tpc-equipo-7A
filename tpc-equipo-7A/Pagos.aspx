<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pagos.aspx.cs" Inherits="tpc_equipo_7A.Pagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class=" d-flex justify-content-center align-items-center mt-5">
        <div class="card p-4 text-center">
            <h2>Elegir metodo de pago</h2>

            <asp:Repeater ID="repMetodos" runat="server">
                <ItemTemplate>
                    <div>
                        <input type="radio" name="metodoPago" value='<%# Eval("Id") %>' />
                        <%# Eval("Nombre") %>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

            <div>
                <asp:Button ID="btnContinuarPago" runat="server" CssClass="btn btn-primary" OnClick="btnContinuarPago_Click" Text="Continuar" />
                <a href="CarritoPage.aspx" class="btn btn-secondary">Volver al carrito</a>
            </div>
        </div>
    </div>
</asp:Content>
