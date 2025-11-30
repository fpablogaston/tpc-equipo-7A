<%@ Page Title="Mis Compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="tpc_equipo_7A.MisCompras" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <title>Mis Compras</title>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <h2 class="mb-4 text-center">Historial de Compras</h2>
        
        <div class="table-responsive">
            <asp:GridView ID="gvCompras" runat="server"
                AutoGenerateColumns="False"
                CssClass="table table-hover table-bordered shadow align-middle"
                HeaderStyle-CssClass="table-dark"
                GridLines="None"
                EmptyDataText="No has realizado compras aún.">

            <Columns>

                <asp:BoundField DataField="IdPedido" HeaderText="N° Pedido" ItemStyle-CssClass="fw-bold" />

                <asp:BoundField DataField="FechaPedido" HeaderText="Fecha"
                                DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />

                <asp:BoundField DataField="Total" HeaderText="Total"
                                DataFormatString="{0:C}" HtmlEncode="false" ItemStyle-CssClass="text-success fw-bold" />

                <asp:BoundField DataField="DireccionEnvio" HeaderText="Dirección" />
                <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
                
                <asp:TemplateField HeaderText="Estado Envío">
                    <ItemTemplate>
                        <span class='badge rounded-pill <%# Eval("EstadoEnvio").ToString() == "Entregado" ? "bg-success" : (Eval("EstadoEnvio").ToString() == "Cancelado" ? "bg-danger" : "bg-warning text-dark") %>'>
                            <%# Eval("EstadoEnvio") %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="MetodoPago" HeaderText="Pago" />
                
                <asp:TemplateField HeaderText="Estado Pago">
                    <ItemTemplate>
                         <span class='<%# Eval("EstadoPago").ToString() == "Aprobado" ? "text-success fw-bold" : "text-muted" %>'>
                            <%# Eval("EstadoPago") %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

            </asp:GridView>
        </div>
        
        <div class="mt-4 text-center">
             <a href="Default.aspx" class="btn btn-outline-primary">Seguir Comprando</a>
        </div>
    </div>

</asp:Content>