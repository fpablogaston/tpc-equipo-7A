<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="tpc_equipo_7A.Formulario" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5 mb-5">
        <div class="card shadow-lg" style="max-width: 900px; margin: auto;">
            <div class="card-header">
                <h2 class="mb-0"><asp:Label ID="lblFormTitulo" runat="server" Text="Formulario"></asp:Label></h2>
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
                                <label for="txtProductoNombre" class="form-label">Nombre:</label>
                                <asp:TextBox runat="server" ID="txtProductoNombre" CssClass="form-control" MaxLength="50" />
                                <asp:RequiredFieldValidator ErrorMessage="El nombre es requerido." ControlToValidate="txtProductoNombre" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                                <asp:RegularExpressionValidator ErrorMessage="Máximo 50 caracteres permitidos." ControlToValidate="txtProductoNombre" ValidationExpression="^[\s\S]{0,50}$" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>
                            <div class="mb-3">
                                <label for="txtProductoPrecio" class="form-label">Precio:</label>
                                <asp:TextBox runat="server" ID="txtProductoPrecio" CssClass="form-control"/>
                                <asp:RequiredFieldValidator ErrorMessage="El precio es requerido." ControlToValidate="txtProductoPrecio" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                                <asp:RegularExpressionValidator ControlToValidate="txtProductoPrecio" ValidationExpression="^\d{1,3}(\.\d{3})*(,\d{1,2})?$" ErrorMessage="Ingrese un precio válido (ej: 1234.56)" runat="server" ForeColor="DarkRed" />

                            </div>
                            <div class="mb-3">
                                <label for="txtProductoStock" class="form-label">Stock:</label>
                                <asp:TextBox runat="server" ID="txtProductoStock" CssClass="form-control" TextMode="Number" />
                                <asp:RequiredFieldValidator ErrorMessage="El stock es requerido." ControlToValidate="txtProductoStock" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>
                            <div class="mb-3">
                                <label for="ddlProductoCategoria" class="form-label">Categoría:</label>
                                <asp:DropDownList runat="server" ID="ddlProductoCategoria" CssClass="form-select" />
                                <asp:RequiredFieldValidator ErrorMessage="La categoría es requerida." ControlToValidate="ddlProductoCategoria" ForeColor="DarkRed" runat="server" InitialValue="0" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label for="txtProductoDescripcion" class="form-label">Descripción:</label>
                                <asp:TextBox runat="server" ID="txtProductoDescripcion" CssClass="form-control" TextMode="MultiLine" Rows="4" MaxLength="300" />
                                <asp:RegularExpressionValidator 
                                    ErrorMessage="Máximo 300 caracteres permitidos." 
                                    ControlToValidate="txtProductoDescripcion" 
                                    ValidationExpression="^[\s\S]{0,300}$" 
                                    ForeColor="DarkRed" 
                                    runat="server" 
                                    Display="Dynamic" />
                            </div>
                            <asp:UpdatePanel ID="upProductoImagen" runat="server">
                                <ContentTemplate>
                                    <div class="mb-3">
                                        <label for="txtProductoImagenUrl" class="form-label">Url Imagen:</label>
                                        <asp:TextBox runat="server" ID="txtProductoImagenUrl" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtProductoImagenUrl_TextChanged" MaxLength="500" />
                                        <asp:RegularExpressionValidator ErrorMessage="Máximo 500 caracteres permitidos." ControlToValidate="txtProductoImagenUrl" ValidationExpression="^[\s\S]{0,500}$" ForeColor="DarkRed" runat="server" Display="Dynamic" />
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
                                <label for="txtCategoriaNombre" class="form-label">Nombre:</label>
                                <asp:TextBox runat="server" ID="txtCategoriaNombre" CssClass="form-control" MaxLength="50" />
                                <asp:RequiredFieldValidator ErrorMessage="El nombre es requerido." ControlToValidate="txtCategoriaNombre" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                                <asp:RegularExpressionValidator ErrorMessage="Máximo 50 caracteres permitidos." ControlToValidate="txtCategoriaNombre" ValidationExpression="^[\s\S]{0,50}$" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>
                            <div class="mb-3">
                                <label for="txtCategoriaDescripcion" class="form-label">Descripción:</label>
                                <asp:TextBox runat="server" ID="txtCategoriaDescripcion" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="150" />
                                <asp:RegularExpressionValidator ErrorMessage="Máximo 150 caracteres permitidos." ControlToValidate="txtCategoriaDescripcion" ValidationExpression="^[\s\S]{0,150}$" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>

                <%-- FORMULARIO DE CLIENTE --%>
                <asp:PlaceHolder ID="phCliente" runat="server" Visible="false">
                    <div class="row justify-content-center">
                        <div class="col-md-8">
                            <div class="mb-3">
                                <label for="txtClienteId" class="form-label">ID:</label>
                                <asp:TextBox ID="txtClienteId" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="txtClienteNombre" class="form-label">Nombre:</label>
                                <asp:TextBox ID="txtClienteNombre" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtClienteNombre" ID="RequiredFieldValidator1" runat="server" ErrorMessage="El nombre es requerido" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ErrorMessage="Máximo 50 caracteres permitidos." ControlToValidate="txtClienteNombre" ValidationExpression="^[\s\S]{0,50}$" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>

                            <div class="mb-3">
                                <label for="txtClienteApellido" class="form-label">Apellido:</label>
                                <asp:TextBox ID="txtClienteApellido" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtClienteApellido" ID="RequiredFieldValidator2" runat="server" ErrorMessage="El apellido es requerido" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ErrorMessage="Máximo 50 caracteres permitidos." ControlToValidate="txtClienteApellido" ValidationExpression="^[\s\S]{0,50}$" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>
                            <div class="mb-3">
                                <label for="txtClienteEmail" class="form-label">Email:</label>
                                <asp:TextBox ID="txtClienteEmail" runat="server" CssClass="form-control" TextMode="Email" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtClienteEmail" ID="RequiredFieldValidator3" runat="server" ErrorMessage="El email es requerido" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ErrorMessage="Máximo 100 caracteres permitidos." ControlToValidate="txtClienteEmail" ValidationExpression="^[\s\S]{0,100}$" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>

                            <div class="mb-3">
                                <label for="txtClienteTelefono" class="form-label">Teléfono:</label>
                                <asp:TextBox ID="txtClienteTelefono" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtClienteTelefono" ID="RequiredFieldValidator4" runat="server" ErrorMessage="El teléfono es requerido" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ErrorMessage="Máximo 20 caracteres permitidos." ControlToValidate="txtClienteTelefono" ValidationExpression="^[\s\S]{0,20}$" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>

                            <div class="mb-3">
                                <asp:Label ID="lblUsuario" for="txtClienteUsuario" class="form-label" runat="server">Usuario: </asp:Label>
                                <asp:TextBox ID="txtClienteUsuario" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtClienteUsuario" ID="RequiredFieldValidator5" runat="server" ErrorMessage="El usuario es requerido" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ErrorMessage="Máximo 20 caracteres permitidos." ControlToValidate="txtClienteUsuario" ValidationExpression="^[\s\S]{0,20}$" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>

                            <div class="mb-3">
                                <asp:Label ID="lblPass" for="txtClientePassword" runat="server" class="form-label">Password: </asp:Label>
                                <asp:TextBox ID="txtClientePassword" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtClientePassword" ID="RequiredFieldValidator6" runat="server" ErrorMessage="La contraseña es requerida" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ErrorMessage="Máximo 20 caracteres permitidos." ControlToValidate="txtClientePassword" ValidationExpression="^[\s\S]{0,20}$" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>

                            <div class="px-3 pb-3 text-center">
                                <asp:Label ID="lblResultado" runat="server" />
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>

                <%-- FORMULARIO DE PAGO --%>
                <asp:PlaceHolder ID="phPago" runat="server" Visible="false">
                    <div class="row justify-content-center">
                        <div class="col-md-8">
                            <div class="mb-3">
                                <label for="txtPagoId" class="form-label">ID:</label>
                                <asp:TextBox ID="txtPagoId" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="ddlPagoPedido" class="form-label">Pedido Asociado:</label>
                                <asp:DropDownList runat="server" ID="ddlPagoPedido" CssClass="form-select" />
                                <asp:RequiredFieldValidator ErrorMessage="El pedido es requerido." ControlToValidate="ddlPagoPedido" ID="rfvPagoPedido" ForeColor="DarkRed" runat="server" InitialValue="0" Display="Dynamic" />
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="mb-3">
                                        <label for="txtMetodoPago" class="form-label">Método de Pago:</label>
                                        <asp:TextBox ID="txtMetodoPago" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ErrorMessage="El método de pago es requerido." ControlToValidate="txtMetodoPago" ID="rfvMetodoPago" ForeColor="DarkRed" runat="server" InitialValue="0" Display="Dynamic" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="mb-3">
                                        <label for="txtEstadoPago" class="form-label">Estado:</label>
                                        <asp:TextBox ID="txtEstadoPago" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ErrorMessage="El estado es requerido." ControlToValidate="txtEstadoPago" ID="rfvEstadoPago" ForeColor="DarkRed" runat="server" InitialValue="0" Display="Dynamic" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="mb-3">
                                        <label for="txtMonto" class="form-label">Monto:</label>
                                        <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control" step="0.01"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtMonto" ID="rfvMonto" runat="server" ErrorMessage="El monto es requerido" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ControlToValidate="txtMonto" ValidationExpression="^\d{1,3}(\.\d{3})*(,\d{1,2})?$" ErrorMessage="Ingrese un monto válido (ej: 1234.56)" runat="server" ForeColor="DarkRed" />

                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="mb-3">
                                        <label for="txtFechaPago" class="form-label">Fecha de Pago:</label>
                                        <asp:TextBox ID="txtFechaPago" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>

                    <%-- FORMULARIO DE PEDIDO --%>
                    <asp:PlaceHolder ID="phPedido" runat="server" Visible="false">
                        <div class="row justify-content-center">
                            <div class="col-md-8">

                                <div class="mb-3">
                                    <label for="txtPedidoId" class="form-label">ID:</label>
                                    <asp:TextBox ID="txtPedidoId" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>

                                <div class="mb-3">
                                    <label for="ddlPedidoCliente" class="form-label">Cliente:</label>
                                    <asp:DropDownList ID="ddlPedidoCliente" runat="server" CssClass="form-select" />
                                    <asp:RequiredFieldValidator ID="rfvPedidoCliente" runat="server"
                                        ControlToValidate="ddlPedidoCliente" InitialValue="0"
                                        ErrorMessage="El cliente es requerido." ForeColor="DarkRed" Display="Dynamic" />
                                </div>

                                <div class="mb-3">
                                    <label for="txtPedidoFecha" class="form-label">Fecha del Pedido:</label>
                                    <asp:TextBox ID="txtPedidoFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPedidoFecha" runat="server"
                                        ControlToValidate="txtPedidoFecha" ErrorMessage="La fecha es requerida."
                                        ForeColor="DarkRed" Display="Dynamic" />
                                </div>

                                <div class="mb-3">
                                    <label for="txtPedidoTotal" class="form-label">Total:</label>
                                    <asp:TextBox ID="txtPedidoTotal" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPedidoTotal" runat="server"
                                        ControlToValidate="txtPedidoTotal" ErrorMessage="El total es requerido."
                                        ForeColor="DarkRed" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ID="revPedidoTotal" runat="server"
                                        ControlToValidate="txtPedidoTotal"
                                        ValidationExpression="^\d{1,3}(\.\d{3})*(,\d{1,2})?$"
                                        ErrorMessage="Formato inválido. Ejemplo: 999.99"
                                        ForeColor="DarkRed" Display="Dynamic" />
                                </div>

                                <div class="mb-3">
                                    <label for="txtPedidoEstado" class="form-label">Estado:</label>
                                    <asp:TextBox ID="txtPedidoEstado" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPedidoEstado" runat="server"
                                        ControlToValidate="txtPedidoEstado" ErrorMessage="El estado es requerido"
                                        ForeColor="DarkRed" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ID="revPedidoEstado" runat="server"
                                        ControlToValidate="txtPedidoEstado"
                                        ValidationExpression="^[\s\S]{0,20}$"
                                        ErrorMessage="Máximo 20 caracteres."
                                        ForeColor="DarkRed" Display="Dynamic" />
                                </div>

                                <div class="mb-3">
                                    <label for="ddlPedidoPago" class="form-label">Método de Pago:</label>
                                    <asp:DropDownList ID="ddlPedidoPago" runat="server" CssClass="form-select" />
                                    <asp:RequiredFieldValidator ID="rfvPedidoPago" runat="server"
                                        ControlToValidate="ddlPedidoPago" InitialValue="0"
                                        ErrorMessage="El pago es requerido."
                                        ForeColor="DarkRed" Display="Dynamic" />
                                </div>

                                <div class="mb-3">
                                    <label for="ddlPedidoEnvio" class="form-label">Envío Asociado:</label>
                                    <asp:DropDownList ID="ddlPedidoEnvio" runat="server" CssClass="form-select" />
                                    <asp:RequiredFieldValidator ID="rfvPedidoEnvio" runat="server"
                                        ControlToValidate="ddlPedidoEnvio" InitialValue="0"
                                        ErrorMessage="El envío es requerido."
                                        ForeColor="DarkRed" Display="Dynamic" />
                                </div>

                            </div>
                        </div>
                    </asp:PlaceHolder>
                
                <%-- FORMULARIO DE ENVIO --%>
                <asp:PlaceHolder ID="phEnvio" runat="server" Visible="false">
                    <div class="row justify-content-center">
                        <div class="col-md-8">
                            <div class="mb-3">
                                <label for="txtEnvioId" class="form-label">ID:</label>
                                <asp:TextBox ID="txtEnvioId" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="ddlEnvioPedido" class="form-label">Pedido Asociado:</label>
                                <asp:DropDownList runat="server" ID="ddlEnvioPedido" CssClass="form-select" />
                                <asp:RequiredFieldValidator ErrorMessage="El pedido es requerido." ControlToValidate="ddlEnvioPedido" ID="rfvEnvioPedido" ForeColor="DarkRed" runat="server" InitialValue="0" Display="Dynamic" />
                            </div>
                            <div class="mb-3">
                                <label for="txtDireccionEnvio" class="form-label">Dirección de Envío:</label>
                                <asp:TextBox ID="txtDireccionEnvio" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtDireccionEnvio" ID="rfvDireccionEnvio" runat="server" ErrorMessage="La dirección es requerida" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ErrorMessage="Máximo 100 caracteres permitidos." ControlToValidate="txtDireccionEnvio" ValidationExpression="^[\s\S]{0,100}$" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="mb-3">
                                        <label for="txtCiudad" class="form-label">Ciudad:</label>
                                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtCiudad" ID="rfvCiudad" runat="server" ErrorMessage="La ciudad es requerida" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ErrorMessage="Máximo 50 caracteres permitidos." ControlToValidate="txtCiudad" ValidationExpression="^[\s\S]{0,50}$" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="mb-3">
                                        <label for="txtProvincia" class="form-label">Provincia:</label>
                                        <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtProvincia" ID="rfvProvincia" runat="server" ErrorMessage="La provincia es requerida" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ErrorMessage="Máximo 50 caracteres permitidos." ControlToValidate="txtProvincia" ValidationExpression="^[\s\S]{0,50}$" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                                    </div>
                                </div>
                            </div>
                            <div class="mb-3">
                                <label for="txtCodigoPostal" class="form-label">Código Postal:</label>
                                <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtCodigoPostal" ID="rfvCodigoPostal" runat="server" ErrorMessage="El código postal es requerido" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ErrorMessage="Máximo 10 caracteres permitidos." ControlToValidate="txtCodigoPostal" ValidationExpression="^[\s\S]{0,10}$" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="mb-3">
                                        <label for="txtFechaEnvio" class="form-label">Fecha de Envío:</label>
                                        <asp:TextBox ID="txtFechaEnvio" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="mb-3">
                                        <label for="txtFechaEntrega" class="form-label">Fecha de Entrega (Estimada/Real):</label>
                                        <asp:TextBox ID="txtFechaEntrega" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="mb-3">
                                <label for="txtEstadoEnvio" class="form-label">Estado:</label>
                                <asp:TextBox ID="txtEstadoEnvio" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtEstadoEnvio" ID="rfvEstadoEnvio" runat="server" ErrorMessage="El estado es requerido" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ErrorMessage="Máximo 20 caracteres permitidos." ControlToValidate="txtEstadoEnvio" ValidationExpression="^[\s\S]{0,20}$" ForeColor="DarkRed" runat="server" Display="Dynamic" />
                            </div>
                        </div>
                    </div>
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