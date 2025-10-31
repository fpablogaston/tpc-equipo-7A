<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmarCompra.aspx.cs" Inherits="tpc_equipo_7A.ConfirmarCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-center align-items-center  mt-5">
        <div class="card p-4 text-center shadow" style="width: 30rem;">
            <div class="alert alert-success mb-3" role="alert">
                <h4 class="alert-heading">Compra realizada exitosamente</h4>
                <p>Gracias por tu compra. Recibiras un correo con la confirmación del pedido</p>
            </div>
            <a href="Default.aspx" class="btn btn-primary">Volver al inicio</a>
        </div>
    </div>

</asp:Content>
