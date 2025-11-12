<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pagos.aspx.cs" Inherits="tpc_equipo_7A.Pagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class=" d-flex justify-content-center align-items-center mt-5">
    <div class="card p-4 text-center"> 
        <h2>Elegir metodo de pago</h2>
        <div>
            <input type="radio" name="metodoPago" value="tarjeta">
            Tarjeta
        </div>
        <div>
            <input type="radio" name="metodoPago" value="efectivo">
            Efectivo
        </div>
        <div>
            <input type="radio" name="metodoPago" value="transferencia">
            Transferencia
        </div>
        <div>
            <a href="Envios.aspx" class="btn btn-primary">Continuar al envio</a>
            <button type="button" class="btn btn-secondary"
                data-bs-toggle="offcanvas"
                data-bs-target="#offcanvasExample"
                aria-controls="offcanvasExample">
                Volver al carrito
            </button>
        </div>
       </div>
    </div>
</asp:Content>
