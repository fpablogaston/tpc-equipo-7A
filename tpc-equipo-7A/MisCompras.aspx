<%@ Page Title="Mis Compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="tpc_equipo_7A.MisCompras" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <title>Mis Compras</title>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 class="mt-4 mb-4 text-center">Historial de Compras</h2>

    <asp:GridView ID="gvCompras" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-warning table-bordered shadow"
        GridLines="None">

    <Columns>

        <asp:BoundField DataField="IdPedido" HeaderText="N° Pedido" />

        <asp:BoundField DataField="FechaPedido" HeaderText="Fecha del Pedido"
                        DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />

        <asp:BoundField DataField="Total" HeaderText="Total ($)"
                        DataFormatString="{0:N2}" HtmlEncode="false" />

        <asp:BoundField DataField="DireccionEnvio" HeaderText="Dirección" />
        <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
        <asp:BoundField DataField="Provincia" HeaderText="Provincia" />

        <asp:BoundField DataField="EstadoEnvio" HeaderText="Estado Envío" />

        <asp:BoundField DataField="MetodoPago" HeaderText="Método de Pago" />
        <asp:BoundField DataField="EstadoPago" HeaderText="Estado del Pago" />

    </Columns>

    </asp:GridView>

</asp:Content>
