<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarritoPage.aspx.cs" Inherits="tpc_equipo_7A.CarritoPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="modal fade show" tabindex="-1" style="display: block;">
        <div class="modal-dialog" style="margin-top: 150px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Carrito de compras</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <%--codigo nuevo--%>
                    <asp:Repeater ID="repCarrito" runat="server">
                        <ItemTemplate>
                            <div class="d-flex justify-content-between align-items-center mb-3 border-bottom pb-2">

                                <div>
                                    <strong><%# Eval("Producto.Nombre") %></strong><br />
                                    <span>Precio: <%# Eval("Producto.Precio", "{0:C}") %></span><br />
                                    <span>Subtotal: <%# Eval("Subtotal", "{0:C}") %></span>
                                </div>

                                <div class="d-flex align-items-center gap-2">
                                    <button type="button" class="btn btn-outline-secondary">−</button>
                                    <span><%# Eval("Cantidad") %></span>
                                    <button type="button" class="btn btn-outline-secondary">+</button>
                                </div>

                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <hr />

                    <h5>Total:
                        <asp:Label ID="lblTotal" runat="server" Text="$0"></asp:Label></h5>


<%--                    <p>Producto</p>
                    <img src="https://www.bigbuy.com.py/imagenes/sin_imagen.jpg" class="card-img-top" style="max-height: 200px; object-fit: contain;">
                    <div class="d-flex align-items-center gap-2">
                        <button type="button" class="btn btn-outline-secondary">−</button>
                        <span>1</span>
                        <button type="button" class="btn btn-outline-secondary">+</button>
                    </div>--%>
                </div>
                <div class="modal-footer">
                    <%--agrego este boton continuar a carritopage.aspx--%>
                    <a href="Envios.aspx" class="btn btn-primary">Continuar</a>
                    <%--<a href="Pagos.aspx" class="btn btn-primary">Metodo de pago</a>--%>
                    <a href="DetalleProducto.aspx" class="btn btn-secondary">Volver al detalle</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
