<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="CompraExitosa.aspx.cs" Inherits="tpc_equipo_7A.CompraExitosa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Compra realizada con éxito</title>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            Swal.fire({
                icon: 'success',
                title: '¡Gracias por tu compra!',
                text: 'Tu pedido fue procesado exitosamente.',
                showConfirmButton: false,
                timer: 1800
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger d-block mt-3" Visible="false"></asp:Label>

    <div class="container py-5">
        <div class="row justify-content-center">
            <div class="col-lg-8">

                <div class="card shadow-lg border-0">
                    <div class="card-body p-5 text-center">

                        <div class="mb-4">
                            <svg xmlns="http://www.w3.org/2000/svg" width="90" height="90"
                                 fill="none" stroke="#28a745" stroke-width="2"
                                 stroke-linecap="round" stroke-linejoin="round"
                                 class="feather feather-check-circle mb-3">
                                <circle cx="12" cy="12" r="10" />
                                <path d="M9 12l2 2 4-4" />
                            </svg>
                        </div>

                        <h1 class="text-success fw-bold mb-3">¡Compra realizada con éxito!</h1>

                        <!-- Nro de Pedido -->
                        <asp:Label ID="lblPedido" runat="server"
                            CssClass="h5 text-muted d-block mb-3"></asp:Label>

                        <!-- Fecha del Pedido -->
                        <asp:Label ID="lblFecha" runat="server"
                            CssClass="text-muted d-block mb-3"></asp:Label>

                        <!-- Dirección de Envío -->
                        <asp:Label ID="lblEnvio" runat="server"
                            CssClass="text-muted d-block mb-3"></asp:Label>

                        <!-- Método de Pago -->
                        <asp:Label ID="lblPago" runat="server"
                            CssClass="text-muted d-block mb-4"></asp:Label>

                        <p class="mb-4" style="font-size: 1.2rem;">
                            Tu pedido fue registrado correctamente y será preparado para el envío.
                        </p>

                        <h4 class="fw-bold text-start mb-3">Resumen del pedido</h4>

                        <asp:Repeater ID="repResumen" runat="server">
                            <HeaderTemplate>
                                <table class="table table-bordered text-start">
                                    <thead class="table-light">
                                        <tr>
                                            <th>Producto</th>
                                            <th>Cant.</th>
                                            <th>Precio</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Nombre") %></td>
                                    <td><%# Eval("Cantidad") %></td>
                                    <td>$ <%# Eval("Precio", "{0:N2}") %></td>
                                </tr>
                            </ItemTemplate>

                            <FooterTemplate>
                                    </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>

                        <h4 class="mt-3">Total: 
                            <span class="text-primary fw-bold">$<asp:Label ID="lblTotal" runat="server"></asp:Label></span>
                        </h4>

                        <div class="mt-4 d-flex justify-content-center gap-3">
                            <a href="Default.aspx" class="btn btn-primary btn-lg px-4">Volver al Inicio</a>

                            <a href="MisCompras.aspx" class="btn btn-outline-dark btn-lg px-4">
                                Ver Mis Compras
                            </a>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
