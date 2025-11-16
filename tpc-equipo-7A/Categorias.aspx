<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="tpc_equipo_7A.Categorias" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container mt-4">
        <h3 class="mb-4">Categorías</h3>
        <div class="row row-cols-1 row-cols-md-3 g-4">
            <asp:Repeater runat="server" id="repProducto">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100 shadow-sm">
                            <a href="DetalleProducto.aspx?id=<%# Eval("Id") %>" style="text-decoration: none; color: inherit;">
                                <img src="<%# Eval("ImagenUrl") %>" class="card-img-top" alt="Imagen del producto" 
                                     style="height: 250px; object-fit: contain; padding: 10px;" 
                                     onerror="this.src='https://placehold.co/600x400?text=No+Image'">
                                <div class="card-body d-flex flex-column">
                                    <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                    <p class="card-text text-truncate"><%# Eval("Descripcion") %></p>
                                    <h4 class="card-text mt-auto text-primary">$<%# Eval("Precio", "{0:N2}") %></h4>
                                </div>
                            </a>
                            <div class="card-footer bg-transparent border-top-0">
                                <div class="d-grid gap-2">
                                    <asp:Button ID="btnAgregarCarrito" runat="server" Text="Agregar al carrito" 
                                        CssClass="btn btn-outline-primary" CommandArgument='<%# Eval("Id") %>' 
                                        OnClick="btnAgregarCarrito_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>