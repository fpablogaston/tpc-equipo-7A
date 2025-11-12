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
                    <p>Producto</p>
                    <img src="https://www.bigbuy.com.py/imagenes/sin_imagen.jpg" class="card-img-top" style="max-height: 200px; object-fit: contain;">
                    <div class="d-flex align-items-center gap-2">
                        <button type="button" class="btn btn-outline-secondary">−</button>
                        <span>1</span>
                        <button type="button" class="btn btn-outline-secondary">+</button>
                    </div>
                </div>
                <div class="modal-footer">
                    <a href="Pagos.aspx" class="btn btn-primary">Metodo de pago</a>
                    <a href="DetalleProducto.aspx" class="btn btn-secondary">Volver al detalle</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
