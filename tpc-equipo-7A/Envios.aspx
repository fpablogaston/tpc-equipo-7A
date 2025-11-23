<%@ Page Title="Datos de Envío" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Envios.aspx.cs" Inherits="tpc_equipo_7A.Envios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card shadow-sm">
                    <div class="card-header bg-primary text-white text-center">
                        <h3>Datos de Envío</h3>
                    </div>
                    <div class="card-body p-4">
                        <%-- Dirección --%>
                        <div class="mb-3">
                            <label for="txtDireccion" class="form-label fw-bold">Dirección de Entrega</label>
                            <asp:TextBox ID="txtDireccion" class="form-control" runat="server" placeholder="Ej: Av. Siempre Viva 742"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="La dirección es requerida." ControlToValidate="txtDireccion" ForeColor="Red" Display="Dynamic" runat="server" />
                        </div>
                        <div class="row">
                            <%-- Ciudad --%>
                            <div class="col-md-6 mb-3">
                                <label for="txtCiudad" class="form-label fw-bold">Ciudad / Localidad</label>
                                <asp:TextBox ID="txtCiudad" class="form-control" runat="server" placeholder="Ej: Springfield"></asp:TextBox>
                                <asp:RequiredFieldValidator ErrorMessage="La ciudad es requerida." ControlToValidate="txtCiudad" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                            <%-- Provincia --%>
                            <div class="col-md-6 mb-3">
                                <label for="txtProvincia" class="form-label fw-bold">Provincia</label>
                                <asp:TextBox ID="txtProvincia" class="form-control" runat="server" placeholder="Ej: Buenos Aires"></asp:TextBox>
                                <asp:RequiredFieldValidator ErrorMessage="La provincia es requerida." ControlToValidate="txtProvincia" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <%-- Código Postal --%>
                            <div class="col-md-6 mb-3">
                                <label for="txtCodigoPostal" class="form-label fw-bold">Código Postal</label>
                                <asp:TextBox ID="txtCodigoPostal" class="form-control" runat="server" placeholder="Ej: 1648" MaxLength="8"></asp:TextBox>
                                <asp:RequiredFieldValidator ErrorMessage="El código postal es requerido." ControlToValidate="txtCodigoPostal" ForeColor="Red" Display="Dynamic" runat="server" />
                                <asp:RegularExpressionValidator ErrorMessage="Solo números." ControlToValidate="txtCodigoPostal" ValidationExpression="^[0-9]+$" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                        </div>
                        <hr />
                        <div class="d-grid gap-2 d-md-flex justify-content-md-between mt-4">
                            <a href="CarritoPage.aspx" class="btn btn-outline-secondary px-4">
                                Volver al Carrito
                            </a>
                            <asp:Button ID="btnContinuar" runat="server" CssClass="btn btn-primary px-4" Text="Continuar al Pago" OnClick="btnContinuar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>