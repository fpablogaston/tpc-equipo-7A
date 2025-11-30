<%@ Page Title="Mis Compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="tpc_equipo_7A.MisCompras" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <title>Mis Compras</title>
    <!-- Bootstrap Icons -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
    <style>
        /* Shared Styles for PO Diagram */
        .step-icon {
            font-size: 1.1rem;
            width: 35px;
            height: 35px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            border-radius: 50%;
            border: 2px solid #dee2e6;
            background-color: #fff;
            color: #6c757d;
            transition: all 0.3s;
        }
        .step-line {
            display: inline-block;
            width: 30px;
            height: 3px;
            background-color: #dee2e6;
            vertical-align: middle;
            margin: 0 2px;
        }
        
        /* Status Colors */
        .step-active { background-color: #0d6efd; color: white; border-color: #0d6efd; }
        .step-completed { background-color: #198754; color: white; border-color: #198754; }
        .step-warning { background-color: #ffc107; color: #000; border-color: #ffc107; }
        .step-pending { background-color: #f8f9fa; color: #adb5bd; }

    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <h2 class="display-5 mb-4"><i class="bi bi-bag-heart"></i> Mis Compras</h2>
        
        <%-- Mensaje informativo --%>
        <div class="alert alert-light border shadow-sm mb-4">
            <i class="bi bi-info-circle-fill text-primary"></i> 
            Aquí puedes ver el estado de tus pedidos en tiempo real.
        </div>

        <div class="card shadow-sm border-0">
            <div class="card-body p-0">
                <div class="table-responsive">
                    <asp:GridView ID="gvCompras" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="table table-hover align-middle mb-0"
                        GridLines="Horizontal"
                        border="0"
                        OnRowDataBound="gvCompras_RowDataBound"
                        EmptyDataText="<div class='p-4 text-center text-muted'>No has realizado compras aún. <br/><a href='Default.aspx' class='btn btn-link'>Ir a comprar</a></div>">

                        <Columns>
                            <%-- Nro Pedido y Fecha --%>
                            <asp:TemplateField HeaderText="Pedido">
                                <ItemTemplate>
                                    <div class="d-flex flex-column">
                                        <span class="fw-bold fs-5">#<%# Eval("Id") %></span>
                                        <span class="text-muted small"><%# Eval("FechaPedido", "{0:dd/MM/yyyy}") %></span>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- Diagrama Visual (Timeline) --%>
                            <asp:TemplateField HeaderText="Estado del Pedido">
                                <ItemTemplate>
                                    <div class="d-flex align-items-center py-2">
                                        
                                        <%-- 1. PAGO --%>
                                        <div class="text-center position-relative" title="Pago">
                                            <asp:Panel ID="pnlPagoIcon" runat="server" CssClass="step-icon">
                                                <i class="bi bi-currency-dollar"></i>
                                            </asp:Panel>
                                            <div class="small mt-1 text-muted" style="font-size:0.75rem">Pago</div>
                                        </div>

                                        <div class="step-line"></div>

                                        <%-- 2. ENVIO / RETIRO --%>
                                        <div class="text-center position-relative" title="Logística">
                                            <asp:Panel ID="pnlEnvioIcon" runat="server" CssClass="step-icon">
                                                <i id="iconEnvio" runat="server" class="bi bi-truck"></i>
                                            </asp:Panel>
                                            <div class="small mt-1 text-muted" style="font-size:0.75rem">
                                                <asp:Label ID="lblTipoEnvio" runat="server" Text="Envío"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="step-line"></div>

                                        <%-- 3. ESTADO FINAL --%>
                                        <div class="text-center position-relative" title="Estado Actual">
                                            <span class="badge rounded-pill bg-light text-dark border">
                                                <%# Eval("Estado.Descripcion") %>
                                            </span>
                                        </div>

                                    </div>
                                    
                                    <%-- Avisos extra --%>
                                    <asp:Panel ID="pnlCashWarning" runat="server" Visible="false" CssClass="mt-1">
                                        <small class="text-warning fw-bold"><i class="bi bi-exclamation-triangle"></i> Pagar en caja</small>
                                    </asp:Panel>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- Total --%>
                            <asp:BoundField DataField="Total" HeaderText="Total"
                                DataFormatString="{0:C}" HtmlEncode="false" ItemStyle-CssClass="fw-bold text-success fs-5 text-end pe-4" />

                            <%-- Boton Detalle --%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="text-end">
                                        <button type="button" class="btn btn-outline-secondary btn-sm rounded-pill" disabled>
                                            Ver detalle <i class="bi bi-chevron-right"></i>
                                        </button>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        
        <div class="mt-5 text-center">
             <a href="Default.aspx" class="btn btn-primary px-4 py-2 shadow-sm rounded-pill">
                 <i class="bi bi-cart-plus me-2"></i>Seguir Comprando
             </a>
        </div>
    </div>

</asp:Content>