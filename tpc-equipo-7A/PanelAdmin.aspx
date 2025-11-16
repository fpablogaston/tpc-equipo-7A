<%@ Page Title="Admin Panel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelAdmin.aspx.cs" Inherits="tpc_equipo_7A.PanelAdmin" UnobtrusiveValidationMode="None" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container mt-5">
        <h1 class="display-4">Panel de Administración</h1>
        <p class="lead">Seleccione la entidad que desea administrar.</p>
        
        <hr />

        <div class="row">
            <div class="col-md-6">
                <div class="mb-3">
                    <label for="<%=ddlSelectEntity.ClientID%>" class="form-label fs-5">Administrar:</label>
                    <asp:DropDownList ID="ddlSelectEntity" runat="server" CssClass="form-select form-select-lg" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectEntity_SelectedIndexChanged">
                        <asp:ListItem Value="Inicio" Text="Seleccionar entidad..."></asp:ListItem>
                        <asp:ListItem Value="Categorias" Text="Categorías"></asp:ListItem>
                        <asp:ListItem Value="Productos" Text="Productos"></asp:ListItem>
                        <asp:ListItem Value="Clientes" Text="Clientes"></asp:ListItem>
                        <asp:ListItem Value="Pedidos" Text="Pedidos"></asp:ListItem>
                        <asp:ListItem Value="Pagos" Text="Pagos"></asp:ListItem>
                        <asp:ListItem Value="Envios" Text="Envíos"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="upAdminContent" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                
                <asp:PlaceHolder ID="phInicio" runat="server" Visible="true">
                    <div class="alert alert-info mt-3">
                        Por favor, seleccione una entidad del menú desplegable para comenzar a administrar.
                    </div>
                </asp:PlaceHolder>

                <asp:PlaceHolder ID="phCategorias" runat="server" Visible="false">
                    <div class="mt-4">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h3 class="mb-0">Gestión de Categorías</h3>
                            <asp:Button ID="btnNuevaCategoria" runat="server" Text="Nueva Categoría" CssClass="btn btn-success" OnClick="btnNuevaCategoria_Click" />
                        </div>
                        <asp:Label ID="lblMensajeCategoria" runat="server" CssClass="fw-bold d-block mt-3 mb-2"></asp:Label>
                        
                        <asp:GridView ID="gvCategorias" runat="server" 
                            CssClass="table table-striped table-bordered table-hover" 
                            AutoGenerateColumns="false" 
                            DataKeyNames="Id"
                            OnRowCommand="gvCategorias_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditarCategoria" runat="server" Text="Editar" CssClass="btn btn-sm btn-warning me-2" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                        <asp:Button ID="btnEliminarCategoria" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-danger" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('¿Está seguro de que desea eliminar esta categoría?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:PlaceHolder>

                <asp:PlaceHolder ID="phProductos" runat="server" Visible="false">
                    <div class="mt-4">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h3 class="mb-0">Gestión de Productos</h3>
                            <asp:Button ID="btnNuevoProducto" runat="server" Text="Nuevo Producto" CssClass="btn btn-success" OnClick="btnNuevoProducto_Click" />
                        </div>
                        <asp:Label ID="lblMensajeProducto" runat="server" CssClass="fw-bold d-block mt-3 mb-2"></asp:Label>
                        
                        <asp:GridView ID="gvProductos" runat="server" 
                            CssClass="table table-striped table-bordered table-hover"
                            AutoGenerateColumns="false" 
                            DataKeyNames="Id"
                            OnRowCommand="gvProductos_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Categoria.Nombre" HeaderText="Categoría" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                                <asp:BoundField DataField="Stock" HeaderText="Stock" />
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditarProducto" runat="server" Text="Editar" CssClass="btn btn-sm btn-warning me-2" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                        <asp:Button ID="btnEliminarProducto" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-danger" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('¿Está seguro de que desea eliminar este producto?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:PlaceHolder>

                <asp:PlaceHolder ID="phClientes" runat="server" Visible="false">
                     <div class="mt-4">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h3 class="mb-0">Gestión de Clientes</h3>
                            <asp:Button ID="btnNuevoCliente" runat="server" Text="Nuevo Cliente" CssClass="btn btn-success" OnClick="btnNuevoCliente_Click" />
                        </div>
                        <asp:Label ID="lblMensajeCliente" runat="server" CssClass="fw-bold d-block mt-3 mb-2"></asp:Label>
                        
                        <asp:GridView ID="gvClientes" runat="server" 
                            CssClass="table table-striped table-bordered table-hover"
                            AutoGenerateColumns="false" 
                            DataKeyNames="Id"
                            OnRowCommand="gvClientes_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditarCliente" runat="server" Text="Editar" CssClass="btn btn-sm btn-warning me-2" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                        <asp:Button ID="btnEliminarCliente" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-danger" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('¿Está seguro de que desea eliminar este cliente?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:PlaceHolder>

                <asp:PlaceHolder ID="phPedidos" runat="server" Visible="false">
                     <div class="mt-4">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h3 class="mb-0">Gestión de Pedidos</h3>
                        </div>
                        <asp:Label ID="lblMensajePedido" runat="server" CssClass="fw-bold d-block mt-3 mb-2"></asp:Label>
                        
                        <asp:GridView ID="gvPedidos" runat="server" 
                            CssClass="table table-striped table-bordered table-hover"
                            AutoGenerateColumns="false" 
                            DataKeyNames="Id"
                            OnRowCommand="gvPedidos_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" />
                                <asp:BoundField DataField="Cliente.Nombre" HeaderText="Cliente" />
                                <asp:BoundField DataField="FechaPedido" HeaderText="Fecha" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />
                                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnVerPedido" runat="server" Text="Ver Detalles" CssClass="btn btn-sm btn-info me-2" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:PlaceHolder>

                <asp:PlaceHolder ID="phPagos" runat="server" Visible="false">
                     <div class="mt-4">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h3 class="mb-0">Gestión de Pagos</h3>
                        </div>
                        <asp:Label ID="lblMensajePago" runat="server" CssClass="fw-bold d-block mt-3 mb-2"></asp:Label>
                        
                        <asp:GridView ID="gvPagos" runat="server" 
                            CssClass="table table-striped table-bordered table-hover"
                            AutoGenerateColumns="false" 
                            DataKeyNames="Id"
                            OnRowCommand="gvPagos_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" />
                                <asp:BoundField DataField="IdPedido" HeaderText="ID Pedido" />
                                <asp:BoundField DataField="MetodoPago.Nombre" HeaderText="Método" />
                                <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C}" />
                                <asp:BoundField DataField="FechaPago" HeaderText="Fecha" DataFormatString="{0:g}" />
                                <asp:BoundField DataField="Estado.Nombre" HeaderText="Estado" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:PlaceHolder>
                
                <asp:PlaceHolder ID="phEnvios" runat="server" Visible="false">
                     <div class="mt-4">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h3 class="mb-0">Gestión de Envíos</h3>
                        </div>
                        <asp:Label ID="lblMensajeEnvio" runat="server" CssClass="fw-bold d-block mt-3 mb-2"></asp:Label>
                        
                        <asp:GridView ID="gvEnvios" runat="server" 
                            CssClass="table table-striped table-bordered table-hover"
                            AutoGenerateColumns="false" 
                            DataKeyNames="Id"
                            OnRowCommand="gvEnvios_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" />
                                <asp:BoundField DataField="IdPedido" HeaderText="ID Pedido" />
                                <asp:BoundField DataField="DireccionEnvio" HeaderText="Dirección" />
                                <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
                                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                <asp:BoundField DataField="FechaEnvio" HeaderText="Fecha Envío" DataFormatString="{0:d}" />
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditarEnvio" runat="server" Text="Editar Estado" CssClass="btn btn-sm btn-warning me-2" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:PlaceHolder>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSelectEntity" EventName="SelectedIndexChanged" />
                
                <asp:AsyncPostBackTrigger ControlID="btnNuevaCategoria" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="gvCategorias" EventName="RowCommand" />

                <asp:AsyncPostBackTrigger ControlID="btnNuevoProducto" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="gvProductos" EventName="RowCommand" />

                <asp:AsyncPostBackTrigger ControlID="btnNuevoCliente" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="gvClientes" EventName="RowCommand" />
                
                <asp:AsyncPostBackTrigger ControlID="gvPedidos" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="gvPagos" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="gvEnvios" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>

    </div>
</asp:Content>