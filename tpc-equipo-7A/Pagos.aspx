<%@ Page Title="Método de Pago" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pagos.aspx.cs" Inherits="tpc_equipo_7A.Pagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card shadow-sm">
                    <div class="card-header bg-primary text-white text-center">
                        <h3><i class="bi bi-credit-card"></i> Método de Pago</h3>
                    </div>
                    <div class="card-body p-4">
                        <%-- Resumen de Monto --%>
                        <div class="alert alert-info text-center mb-4">
                            <h4 class="mb-0">Total a Pagar: <asp:Label ID="lblTotalPagar" runat="server" Font-Bold="true"></asp:Label></h4>
                        </div>
                        <h5 class="mb-3">Seleccione una opción:</h5>
                        <%-- Lista de Métodos --%>
                        <div class="list-group mb-3">
                            <asp:Repeater ID="repMetodos" runat="server">
                                <ItemTemplate>
                                    <label class="list-group-item d-flex gap-3">
                                        <input class="form-check-input flex-shrink-0" type="radio" name="metodoPago" value='<%# Eval("Id") %>' style="font-size: 1.375em;">
                                        <span class="pt-1 form-check-label fs-5">
                                            <%# Eval("Nombre") %>
                                        </span>
                                    </label>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <%-- Mensaje de Error --%>
                        <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-3 fw-bold" Visible="false"></asp:Label>
                        <hr />
                        <%-- Botones de Navegación --%>
                        <div class="d-grid gap-2 d-md-flex justify-content-md-between mt-4">
                            <a href="Envios.aspx" class="btn btn-outline-secondary px-4">
                                <i class="bi bi-arrow-left"></i> Volver a Envíos
                            </a>
                            <asp:Button ID="btnContinuarPago" runat="server" CssClass="btn btn-success px-4" OnClick="btnContinuarPago_Click" Text="Continuar" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>