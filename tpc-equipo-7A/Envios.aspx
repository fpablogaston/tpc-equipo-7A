<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Envios.aspx.cs" Inherits="tpc_equipo_7A.Envio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card p-4 w-50 mx-auto">
        <h2>Agregar direccion</h2>
        <div class="mb-3">
            <label for="exampleFormControlInput1" class="form-label">Direccion</label>
            <input type="text" class="form-control" id="exampleFormControlInput1" placeholder="calle falsa 123">
        </div>
        <div class="mb-3">
            <label for="exampleFormControlInput1" class="form-label">Localidad</label>
            <input type="text" class="form-control" id="exampleFormControlInput2" placeholder="Belen de escobar">
        </div>
        <div class="mb-3">
            <label for="exampleFormControlTextarea1" class="form-label">Informacion adicional</label>
            <textarea class="form-control" id="exampleFormControlTextarea3" rows="3"></textarea>
        </div>
        <div>
            <a href="ConfirmarCompra.aspx" class="btn btn-primary">Confirmar compra</a>
            <a href="Pago.aspx" class="btn btn-secondary">Volver al pago</a>
        </div>
    </div>

</asp:Content>
