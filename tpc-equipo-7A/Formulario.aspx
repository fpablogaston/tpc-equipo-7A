<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="tpc_equipo_7A.Formulario" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5 mb-5">
        <div class="card shadow-lg" style="max-width: 900px; margin: auto;">
            <div class="card-header">
                <h2 class="mb-0">
                    <asp:Label ID="lblFormTitulo" runat="server" Text="Formulario"></asp:Label></h2>
            </div>
            <div class="card-body p-4 p-md-5">

                <%-- FORMULARIO DE PRODUCTO --%>
                <asp:PlaceHolder ID="phProducto" runat="server" Visible="false">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label for="txtProductoId" class="form-label">ID:</label>
                                <asp:TextBox runat="server" ID="txtProductoId" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="mb-3">
                                <label for="txtProductoNombre" class="form-label">* Nombre:</label>
                                <asp:TextBox runat="server" ID="txtProductoNombre" CssClass="form-control" />
                                <asp:RequiredFieldValidator ErrorMessage="El nombre es requerido." ControlToValidate="txtProductoNombre" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>
                            <div class="mb-3">
                                <label for="txtProductoPrecio" class="form-label">* Precio:</label>
                                <asp:TextBox runat="server" ID="txtProductoPrecio" CssClass="form-control" TextMode="Number" step="0.01" />
                                <asp:RequiredFieldValidator ErrorMessage="El precio es requerido." ControlToValidate="txtProductoPrecio" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>
                            <div class="mb-3">
                                <label for="txtProductoStock" class="form-label">* Stock:</label>
                                <asp:TextBox runat="server" ID="txtProductoStock" CssClass="form-control" TextMode="Number" />
                                <asp:RequiredFieldValidator ErrorMessage="El stock es requerido." ControlToValidate="txtProductoStock" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>
                            <div class="mb-3">
                                <label for="ddlProductoCategoria" class="form-label">* Categoría:</label>
                                <asp:DropDownList runat="server" ID="ddlProductoCategoria" CssClass="form-select" />
                                <asp:RequiredFieldValidator ErrorMessage="La categoría es requerida." ControlToValidate="ddlProductoCategoria" ForeColor="DarkRed" runat="server" InitialValue="0" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label for="txtProductoDescripcion" class="form-label">Descripción:</label>
                                <asp:TextBox runat="server" ID="txtProductoDescripcion" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                            </div>
                            <asp:UpdatePanel ID="upProductoImagen" runat="server">
                                <ContentTemplate>
                                    <div class="mb-3">
                                        <label for="txtProductoImagenUrl" class="form-label">Url Imagen:</label>
                                        <asp:TextBox runat="server" ID="txtProductoImagenUrl" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtProductoImagenUrl_TextChanged" />
                                    </div>
                                    <asp:Image ImageUrl="https://placehold.co/600x400?text=No+Image" runat="server" ID="imgProducto" CssClass="img-fluid rounded border" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </asp:PlaceHolder>

                <%-- FORMULARIO DE CATEGORIA --%>
                <asp:PlaceHolder ID="phCategoria" runat="server" Visible="false">
                    <div class="row justify-content-center">
                        <div class="col-md-8">
                            <div class="mb-3">
                                <label for="txtCategoriaId" class="form-label">ID:</label>
                                <asp:TextBox runat="server" ID="txtCategoriaId" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="mb-3">
                                <label for="txtCategoriaNombre" class="form-label">* Nombre:</label>
                                <asp:TextBox runat="server" ID="txtCategoriaNombre" CssClass="form-control" />
                                <asp:RequiredFieldValidator ErrorMessage="El nombre es requerido." ControlToValidate="txtCategoriaNombre" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>
                            <div class="mb-3">
                                <label for="txtCategoriaDescripcion" class="form-label">Descripción:</label>
                                <asp:TextBox runat="server" ID="txtCategoriaDescripcion" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>

                <%-- FORMULARIO DE CLIENTE (Placeholder) --%>
                <asp:PlaceHolder ID="phCliente" runat="server" Visible="false">
                    <div class="row justify-content-center">
                        <div class="col-md-8">
                            <div class="mb-3">
                                <label for="txtClienteId" class="form-label">ID:</label>
                                <asp:TextBox ID="txtClienteId" runat="server" CssClass="form-control"  Enabled="false"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="txtClienteNombre" class="form-label">Nombre:</label>
                                <asp:TextBox ID="txtClienteNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtClienteNombre" ID="RequiredFieldValidator1" runat="server" ErrorMessage="El nombre es requerido"></asp:RequiredFieldValidator>
                            </div>

                            <div class="mb-3">
                                <label for="txtClienteApellido" class="form-label">Apellido:</label>
                                <asp:TextBox ID="txtClienteApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtClienteApellido" ID="RequiredFieldValidator2" runat="server" ErrorMessage="El apellido es requerido"></asp:RequiredFieldValidator>
                            </div>

                            <div class="mb-3">
                                <label for="txtClienteEmail" class="form-label">Email:</label>
                                <asp:TextBox ID="txtClienteEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtClienteEmail" ID="RequiredFieldValidator3" runat="server" ErrorMessage="El email es requerido"></asp:RequiredFieldValidator>
                            </div>

                            <div class="mb-3">
                                <label for="txtClienteTelefono" class="form-label">Telefono:</label>
                                <asp:TextBox ID="txtClienteTelefono" runat="server" CssClass="form-control" ></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtClienteTelefono" ID="RequiredFieldValidator4" runat="server" ErrorMessage="El telefono es requerido"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>

                <%-- FORMULARIO DE PEDIDO (Placeholder) --%>
                <asp:PlaceHolder ID="phPedido" runat="server" Visible="false">
                    <div class="alert alert-info">Formulario para Pedidos aquí.</div>
                </asp:PlaceHolder>

                <%-- FORMULARIO DE ENVIO (Placeholder) --%>
                <asp:PlaceHolder ID="phEnvio" runat="server" Visible="false">
                    <div class="alert alert-info">Formulario para Envíos aquí.</div>
                </asp:PlaceHolder>

                <%-- Botones de Acción --%>
                <hr class="mt-4" />
                <div class="row mt-3">
                    <div class="col-12 d-flex justify-content-end gap-2">
                        <asp:Button Text="Guardar" ID="btnGuardar" CssClass="btn btn-primary btn-lg" OnClick="btnGuardar_Click" runat="server" />
                        <asp:Button Text="Cancelar" ID="btnCancelar" CssClass="btn btn-secondary btn-lg" OnClick="btnCancelar_Click" runat="server" CausesValidation="false" />
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
