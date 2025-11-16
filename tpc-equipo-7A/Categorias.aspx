<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="tpc_equipo_7A.Categorias" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <h3>Categorias</h3>
    <asp:Repeater runat="server" id="repProducto">
        <ItemTemplate>
            <div class="col">
               <div class="card">
                 <img src="<%#Eval("ImagenURL") %>" class="card-img-top" alt="...">
                 <div class="card-body">
                     <h5 class="card-title"><%#Eval("Nombre")%> </h5>
                     <p class="card-text"><%#Eval("Descripcion")%>.</p>
                     <a href="DetalleProducto.aspx?id=<%#Eval("Id") %>">Ver detalle</a>
                     <asp:button text="Detalle" CssClass="btn btn-primary" runat="server" id="btnDetalle" CommandArgument='<%#Eval("Id") %>' CommandName="ProductoId" OnClick="btnDetalle_Click"/>
                 </div>
                </div> 
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>