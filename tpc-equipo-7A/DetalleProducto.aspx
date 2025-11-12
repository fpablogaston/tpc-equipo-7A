<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="tpc_equipo_7A.DetalleProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card mx-auto text-center" style="width: 25rem;">
        <h1>Nombre del producto</h1>
        <img src="https://www.bigbuy.com.py/imagenes/sin_imagen.jpg" class="card-img-top" style="max-height: 300px; object-fit: contain;">
        <div class="card-body">
            <p>Descripcion del producto</p>
            <%--<a href="CarritoPage.aspx" class="btn btn-primary">Agregar al carrito</a>--%>
            <button type="button" class="btn btn-primary"
                data-bs-toggle="offcanvas"
                data-bs-target="#offcanvasExample"
                aria-controls="offcanvasExample">
                Agregar al carrito
            </button>
        </div>
    </div>
</asp:Content>
