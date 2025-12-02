<%@ Page Title="Datos de Envío" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Envios.aspx.cs" Inherits="tpc_equipo_7A.Envios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
    <style>
        .address-card {
            cursor: pointer;
            transition: all 0.2s;
        }
        .address-card:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        }
        .selected-card {
            border: 2px solid #0d6efd !important;
            background-color: #f8f9fa;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5 mb-5">
        <div class="row justify-content-center">
            <div class="col-md-10 col-lg-8">
                <div class="card shadow-sm">
                    <div class="card-header bg-primary text-white text-center">
                        <h3><i class="bi bi-truck"></i> ¿Cómo quieres recibir tu compra?</h3>
                    </div>
                    <div class="card-body p-4">

                        <asp:UpdatePanel ID="upEnvios" runat="server">
                            <ContentTemplate>
                                
                                <%-- OPCIÓN 1: RETIRO EN TIENDA --%>
                                <div class="form-check mb-3 p-3 border rounded shadow-sm">
                                    <asp:RadioButton ID="rbRetiro" runat="server" GroupName="TipoEnvio" 
                                        AutoPostBack="true" OnCheckedChanged="TipoEnvio_CheckedChanged"
                                        CssClass="form-check-input fs-5" />
                                    <label class="form-check-label ms-2" for="<%= rbRetiro.ClientID %>">
                                        <strong>Retiro en el Local</strong> <span class="badge bg-success ms-2">Gratis</span>
                                        <br />
                                        <small class="text-muted">Av. Siempreviva 742, Springfield. (Lun-Vie 9-18hs)</small>
                                    </label>
                                </div>

                                <%-- OPCIÓN 2: ENVIO A DOMICILIO --%>
                                <div class="mb-3">
                                    <h5 class="mb-3">Envío a Domicilio</h5>
                                    
                                    <%-- LISTA DE DIRECCIONES --%>
                                    <asp:Repeater ID="repDirecciones" runat="server" OnItemCommand="repDirecciones_ItemCommand">
                                        <ItemTemplate>
                                            <div class='card mb-2 address-card <%# Eval("Id").ToString() == IdDireccionSeleccionada.ToString() ? "selected-card" : "" %>'>
                                                <div class="card-body d-flex align-items-center">
                                                    
                                                    <div class="me-3">
                                                        <asp:Button ID="btnSeleccionar" runat="server" 
                                                            CommandName="Seleccionar" CommandArgument='<%# Eval("Id") %>'
                                                            CssClass='<%# Eval("Id").ToString() == IdDireccionSeleccionada.ToString() ? "btn btn-primary btn-sm" : "btn btn-outline-secondary btn-sm" %>'
                                                            Text='<%# Eval("Id").ToString() == IdDireccionSeleccionada.ToString() ? "Seleccionada" : "Seleccionar" %>' />
                                                    </div>

                                                    <div class="flex-grow-1">
                                                        <strong><%# Eval("Alias") %></strong><br />
                                                        <%# Eval("Calle") %>, <%# Eval("Ciudad") %> (<%# Eval("CodigoPostal") %>)
                                                    </div>

                                                    <div>
                                                        <%-- BOTÓN ELIMINAR --%>
                                                        <asp:LinkButton ID="btnEliminar" runat="server" 
                                                            CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' 
                                                            CssClass="btn btn-link text-danger p-0" 
                                                            ToolTip="Eliminar dirección"
                                                            OnClientClick="return confirm('¿Estás seguro de que deseas eliminar esta dirección?');">
                                                            <i class="bi bi-trash fs-5"></i>
                                                        </asp:LinkButton>
                                                    </div>

                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <%-- BOTÓN NUEVA DIRECCIÓN --%>
                                    <div class="mt-3">
                                        <asp:RadioButton ID="rbNuevaDireccion" runat="server" GroupName="TipoEnvio"
                                            AutoPostBack="true" OnCheckedChanged="TipoEnvio_CheckedChanged" 
                                            Text="" CssClass="form-check-input" />
                                        <label class="form-check-label ms-2" for="<%= rbNuevaDireccion.ClientID %>">
                                            <strong>Agregar nueva dirección</strong>
                                        </label>
                                    </div>

                                </div>

                                <%-- FORMULARIO NUEVA DIRECCIÓN --%>
                                <asp:Panel ID="pnlNuevaDireccion" runat="server" Visible="false" CssClass="border-top pt-3 mt-3">
                                    <h5 class="text-primary">Nueva Dirección</h5>
                                    
                                    <div class="row g-2">
                                        <div class="col-md-6">
                                            <label class="form-label">Alias (Ej: Casa, Trabajo)</label>
                                            <asp:TextBox ID="txtAlias" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-md-6">
                                            <label class="form-label">Calle y Número</label>
                                            <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtCalle" ValidationGroup="NuevaDir" ForeColor="Red" runat="server" />
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-label">Ciudad</label>
                                            <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtCiudad" ValidationGroup="NuevaDir" ForeColor="Red" runat="server" />
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-label">Provincia</label>
                                            <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtProvincia" ValidationGroup="NuevaDir" ForeColor="Red" runat="server" />
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-label">CP</label>
                                            <asp:TextBox ID="txtCP" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtCP" ValidationGroup="NuevaDir" ForeColor="Red" runat="server" />
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold d-block mt-3" Visible="false"></asp:Label>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <hr class="mt-4"/>
                        
                        <div class="d-flex justify-content-between">
                            <a href="CarritoPage.aspx" class="btn btn-outline-secondary">Volver al Carrito</a>
                            <asp:Button ID="btnContinuar" runat="server" Text="Continuar al Pago" CssClass="btn btn-primary px-4 btn-lg" OnClick="btnContinuar_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>