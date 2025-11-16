<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="tpc_equipo_7A.DetalleProducto" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .stock-badge {
            font-size: 0.9rem;
            padding: 0.4em 0.7em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container mt-5">
        <div class="card shadow-lg border-0" style="max-width: 900px; margin: auto;">
            <div class="row g-0">
                <div class="col-md-6 d-flex align-items-center justify-content-center bg-light p-4">
                    <img src="<%# ProductoActual.ImagenUrl %>" 
                         class="img-fluid rounded-start" 
                         alt="<%# ProductoActual.Nombre %>" 
                         style="max-height: 400px; object-fit: contain;"
                         onerror="this.src='https://placehold.co/600x400?text=No+Image'">
                </div>
                <div class="col-md-6">
                    <div class="card-body p-4 p-md-5">
                        
                        <asp:UpdatePanel ID="upDetalleProducto" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="d-flex justify-content-between align-items-center mb-2">
                                    <asp:Label ID="lblCategoria" runat="server" Text="<%# ProductoActual.Categoria.Nombre %>" CssClass="text-muted text-uppercase fw-bold" Font-Size="Small"></asp:Label>
                                    <asp:Label ID="lblStock" runat="server" CssClass="stock-badge badge rounded-pill"></asp:Label>
                                </div>

                                <h1 class="card-title display-5 fw-bold"><asp:Label ID="lblNombre" runat="server" Text="<%# ProductoActual.Nombre %>"></asp:Label></h1>
                                
                                <h2 class="card-text text-primary my-3"><asp:Label ID="lblPrecio" runat="server" Text='<%# ProductoActual.Precio.ToString("C") %>'></asp:Label></h2>
                                
                                <p class="card-text mb-4"><asp:Label ID="lblDescripcion" runat="server" Text="<%# ProductoActual.Descripcion %>"></asp:Label></p>

                                <hr />

                                <div class="row align-items-center mb-4">
                                    <label for="<%= txtCantidad.ClientID %>" class="col-md-4 col-form-label fw-bold">Cantidad:</label>
                                    <div class="col-md-8">
                                        <div class="input-group" style="width: 150px;">
                                            <asp:Button ID="btnMenos" runat="server" Text="-" CssClass="btn btn-outline-secondary" OnClick="btnMenos_Click" />
                                            <asp:TextBox ID="txtCantidad" runat="server" Text="1" CssClass="form-control text-center" AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btnMas" runat="server" Text="+" CssClass="btn btn-outline-secondary" OnClick="btnMas_Click" />
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="d-grid">
                                    <asp:Button ID="btnAgregarCarrito" runat="server" Text="Agregar al Carrito" CssClass="btn btn-primary btn-lg" OnClick="btnAgregarCarrito_Click" />
                                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success mt-2" EnableViewState="false"></asp:Label>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAgregarCarrito" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnMenos" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnMas" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="txtCantidad" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>