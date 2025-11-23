<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmarCompra.aspx.cs" Inherits="tpc_equipo_7A.ConfirmarCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="row">
            <div class="col-md-8 offset-md-2">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white">
                        <h3>Resumen del Pedido</h3>
                    </div>
                    <div class="card-body">
                        
                        <%-- Resumen de Productos --%>
                        <h5 class="card-title">Productos</h5>
                        <asp:GridView ID="gvProductos" runat="server" CssClass="table table-sm" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="PrecioTotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                            </Columns>
                        </asp:GridView>
                        <div class="text-end fw-bold fs-4 mb-3">
                            Total: <asp:Label ID="lblTotal" runat="server" />
                        </div>

                        <hr />

                        <div class="row">
                            <%-- Datos de Envio --%>
                            <div class="col-md-6">
                                <h5>Datos de Envío</h5>
                                <p>
                                    <strong>Dirección:</strong> <asp:Label ID="lblDireccion" runat="server" /><br />
                                    <strong>Ciudad:</strong> <asp:Label ID="lblCiudad" runat="server" />
                                </p>
                            </div>
                            <%-- Datos de Pago --%>
                            <div class="col-md-6">
                                <h5>Forma de Pago</h5>
                                <p>
                                    <strong>Método:</strong> <asp:Label ID="lblMetodoPago" runat="server" />
                                </p>
                            </div>
                        </div>

                        <div class="alert alert-warning mt-3" role="alert">
                            <i class="bi bi-exclamation-triangle"></i> Por favor revise los datos antes de confirmar.
                        </div>

                        <div class="d-grid gap-2 mt-4">
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Compra" CssClass="btn btn-success btn-lg" OnClick="btnConfirmar_Click" />
                            <a href="Pagos.aspx" class="btn btn-outline-secondary">Volver Atrás</a>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>