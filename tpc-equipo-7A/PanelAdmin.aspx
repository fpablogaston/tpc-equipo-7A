<%@ Page Title="Admin Panel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelAdmin.aspx.cs" Inherits="tpc_equipo_7A.PanelAdmin" UnobtrusiveValidationMode="None" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap Icons -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
    <style>
        /* Styles for PO Diagram */
        .step-icon {
            font-size: 1.2rem;
            width: 30px;
            height: 30px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            border-radius: 50%;
        }
        .step-line {
            display: inline-block;
            width: 40px;
            height: 2px;
            background-color: #dee2e6;
            vertical-align: middle;
            margin: 0 5px;
        }
        .step-active { background-color: #0d6efd; color: white; }
        .step-completed { background-color: #198754; color: white; }
        .step-pending { background-color: #e9ecef; color: #6c757d; }
        .step-warning { background-color: #ffc107; color: #000; }
    </style>
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

                <!-- VISTA PEDIDOS (PO DIAGRAM) -->
                <asp:PlaceHolder ID="phPedidos" runat="server" Visible="false">
                    <div class="mt-4">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h3 class="mb-0">Seguimiento de Pedidos (PO)</h3>
                        </div>
                        <asp:Label ID="lblMensajePedido" runat="server" CssClass="fw-bold d-block mt-3 mb-2"></asp:Label>

                        <div class="mb-3">
                            <asp:Label ID="Label1" runat="server" Text="Filtrar" CssClass="text-dark fw-bold"></asp:Label>
                            <asp:TextBox ID="txtFiltroPedido" runat="server" CssClass="form-control"
                                Placeholder="Buscar pedido por ID, Cliente..." AutoPostBack="true"
                                OnTextChanged="txtFiltroPedido_TextChanged" />
                        </div>

                        <asp:GridView ID="gvPedidos" runat="server"
                            CssClass="table table-hover align-middle shadow-sm bg-white rounded"
                            AutoGenerateColumns="false"
                            DataKeyNames="Id"
                            OnRowDataBound="gvPedidos_RowDataBound"
                            OnRowCommand="gvPedidos_RowCommand">

                            <Columns>
                                <%-- ID y Cliente --%>
                                <asp:TemplateField HeaderText="Orden">
                                    <ItemTemplate>
                                        <div class="fw-bold">#<%# Eval("Id") %></div>
                                        <div class="small text-muted"><%# Eval("FechaPedido", "{0:dd/MM/yyyy}") %></div>
                                        <div><%# Eval("Cliente.Nombre") %> <%# Eval("Cliente.Apellido") %></div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- Diagrama de Hitos (Milestones) --%>
                                <asp:TemplateField HeaderText="Progreso (PO)">
                                    <ItemTemplate>
                                        <div class="d-flex align-items-center">

                                            <%-- 1. PAGO --%>
                                            <div class="text-center" title="Pago">
                                                <asp:Panel ID="pnlPagoIcon" runat="server" CssClass="step-icon">
                                                    <i class="bi bi-currency-dollar"></i>
                                                </asp:Panel>
                                                <div class="small mt-1" style="font-size: 0.7rem">Pago</div>
                                            </div>

                                            <div class="step-line"></div>

                                            <%-- 2. ENVIO / RETIRO --%>
                                            <div class="text-center" title="Logística">
                                                <asp:Panel ID="pnlEnvioIcon" runat="server" CssClass="step-icon">
                                                    <i id="iconEnvio" runat="server" class="bi bi-truck"></i>
                                                </asp:Panel>
                                                <div class="small mt-1" style="font-size: 0.7rem">
                                                    <asp:Label ID="lblTipoEnvio" runat="server" Text="Envío"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="step-line"></div>

                                            <%-- 3. ESTADO FINAL --%>
                                            <div class="text-center" title="Estado Actual">
                                                <span class="badge rounded-pill bg-primary">
                                                    <%# Eval("Estado.Descripcion") %>
                                                </span>
                                            </div>

                                        </div>

                                        <%-- Alerta Visual si es Efectivo --%>
                                        <asp:Panel ID="pnlCashWarning" runat="server" Visible="false" CssClass="mt-2 badge bg-warning text-dark">
                                            <i class="bi bi-exclamation-triangle"></i>Pago en Efectivo (Validar en caja)
                                        </asp:Panel>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" ItemStyle-CssClass="fw-bold text-success" />

                                <%-- Acciones (Cambiar Estado) --%>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <div class="input-group input-group-sm mb-1">
                                            <asp:DropDownList ID="ddlCambiarEstado" runat="server" CssClass="form-select">
                                                <%-- Se llena en RowDataBound --%>
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="btnUpdateEstado" runat="server" CommandName="ActualizarEstado" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-outline-primary" ToolTip="Guardar Estado">
                                                <i class="bi bi-save"></i>
                                            </asp:LinkButton>
                                        </div>
                                        <asp:LinkButton ID="btnVerDetalle" runat="server" CssClass="btn btn-sm btn-info text-white w-100" CommandName="VerDetalle" CommandArgument='<%# Eval("Id") %>'>
                                            <i class="bi bi-eye"></i> Ver Detalle
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:PlaceHolder>

                <asp:PlaceHolder ID="phCategorias" runat="server" Visible="false">
                    <div class="mt-4">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h3 class="mb-0">Gestión de Categorías</h3>
                            <asp:Button ID="btnNuevaCategoria" runat="server" Text="Nueva Categoría" CssClass="btn btn-success" OnClick="btnNuevaCategoria_Click" />
                        </div>
                        <asp:Label ID="lblMensajeCategoria" runat="server" CssClass="fw-bold d-block mt-3 mb-2"></asp:Label>

                        <div class="mb-3">
                            <asp:Label ID="Label4" runat="server" Text="Filtrar" CssClass="text-dark fw-bold"></asp:Label>
                            <asp:TextBox ID="txtFiltroCategorias" runat="server" CssClass="form-control"
                                Placeholder="Buscar categoría..." AutoPostBack="true" OnTextChanged="txtFiltroCategorias_TextChanged"></asp:TextBox>
                        </div>

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
                                        <asp:Button ID="btnEditarCategoria" runat="server" Text="✏️" ToolTip="Editar Categoria" CssClass="btn btn-sm btn-warning me-2" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                        <asp:Button ID="btnEliminarCategoria" runat="server" Text="🗑️" ToolTip="Eliminar Categoria" CssClass="btn btn-sm btn-danger" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('¿Está seguro de que desea eliminar esta categoría?');" />
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

                        <div class="mb-3">
                            <asp:Label ID="Label3" runat="server" Text="Filtrar" CssClass="text-dark fw-bold"></asp:Label>
                            <asp:TextBox ID="txtFiltroProducto" runat="server" CssClass="form-control"
                                Placeholder="Buscar producto..." AutoPostBack="true" OnTextChanged="txtFiltroProducto_TextChanged"></asp:TextBox>
                        </div>

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
                                        <asp:Button ID="btnEditarProducto" runat="server" Text="✏️" ToolTip="Editar Producto" CssClass="btn btn-sm btn-warning me-2" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                        <asp:Button ID="btnEliminarProducto" runat="server" Text="🗑️" ToolTip="Eliminar Producto" CssClass="btn btn-sm btn-danger" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('¿Está seguro de que desea eliminar este producto?');" />
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

                        <div class="mb-3">
                            <asp:Label ID="Label2" runat="server" Text="Filtrar" CssClass="text-dark fw-bold"></asp:Label>
                            <asp:TextBox ID="txtFiltroCliente" runat="server" CssClass="form-control"
                                Placeholder="Buscar cliente..." AutoPostBack="true"
                                OnTextChanged="txtFiltroCliente_TextChanged" />
                        </div>


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

                <asp:PlaceHolder ID="phPagos" runat="server" Visible="false">
                    <div class="mt-4">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h3 class="mb-0">Gestión de Pagos</h3>
                            <asp:Button runat="server" Text="Nuevo Pago" CssClass="btn btn-success disabled" OnClick="btnNuevoPago_Click" />
                        </div>
                        <asp:Label ID="lblMensajePago" runat="server" CssClass="fw-bold d-block mt-3 mb-2"></asp:Label>

                        <div class="mb-3">
                            <asp:Label ID="Label6" runat="server" Text="Filtrar" CssClass="text-dark fw-bold"></asp:Label>
                            <asp:TextBox ID="txtFiltroPago" runat="server" CssClass="form-control"
                                Placeholder="Buscar pago..." AutoPostBack="true"
                                OnTextChanged="txtFiltroPago_TextChanged" />
                        </div>

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
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditarPago" runat="server" Text="✏️" ToolTip="Editar Pago" CssClass="btn btn-sm btn-warning me-2 disabled" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                        <asp:Button ID="btnEliminarPago" runat="server" Text="🗑️" ToolTip="Eliminar Pago" CssClass="btn btn-sm btn-danger disabled" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('¿Está seguro de que desea eliminar este pago?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:PlaceHolder>

                <asp:PlaceHolder ID="phEnvios" runat="server" Visible="false">
                    <div class="mt-4">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h3 class="mb-0">Gestión de Envíos</h3>
                            <asp:Button runat="server" Text="Nuevo Envio" CssClass="btn btn-success disabled" OnClick="btnNuevoEnvio_Click" />
                        </div>
                        <asp:Label ID="lblMensajeEnvio" runat="server" CssClass="fw-bold d-block mt-3 mb-2"></asp:Label>

                        <div class="mb-3">
                            <asp:Label ID="Label5" runat="server" Text="Filtrar" CssClass="text-dark fw-bold"></asp:Label>
                            <asp:TextBox ID="txtFiltroEnvio" runat="server" CssClass="form-control"
                                Placeholder="Buscar envio..." AutoPostBack="true"
                                OnTextChanged="txtFiltroEnvio_TextChanged" />
                        </div>

                        <asp:GridView ID="gvEnvios" runat="server"
                            CssClass="table table-striped table-bordered table-hover"
                            AutoGenerateColumns="false"
                            DataKeyNames="Id"
                            OnRowCommand="gvEnvios_RowCommand"
                            OnRowDataBound="gvEnvios_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" />
                                <asp:BoundField DataField="IdPedido" HeaderText="ID Pedido" />
                                <asp:BoundField DataField="DireccionEnvio" HeaderText="Dirección" />
                                <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
                                <asp:BoundField DataField="Provincia" HeaderText="Provincia" />
                                <asp:BoundField DataField="CodigoPostal" HeaderText="CP" />

                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlEstadoEnvio" runat="server" CssClass="form-select form-select-sm">
                                            <asp:ListItem Text="Pendiente" Value="Pendiente"></asp:ListItem>
                                            <asp:ListItem Text="Preparando" Value="Preparando"></asp:ListItem>
                                            <asp:ListItem Text="En Camino" Value="En Camino"></asp:ListItem>
                                            <asp:ListItem Text="Entregado" Value="Entregado"></asp:ListItem>
                                            <asp:ListItem Text="Cancelado" Value="Cancelado"></asp:ListItem>
                                            <asp:ListItem Text="Retiro en Local" Value="Retiro en Local"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfEstadoActual" runat="server" Value='<%# Eval("EstadoDescripcion") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="FechaEnvio" HeaderText="Fecha Envío" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="FechaEntrega" HeaderText="Fecha Entrega" DataFormatString="{0:d}" />

                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <%-- Botón para guardar cambio de estado --%>
                                        <asp:LinkButton ID="btnGuardarEstado" runat="server" CssClass="btn btn-sm btn-primary me-1"
                                            CommandName="ActualizarEstado" CommandArgument='<%# Container.DataItemIndex %>'
                                            ToolTip="Guardar Estado">
                                                <i class="bi bi-check-lg">💾</i>
                                        </asp:LinkButton>

                                        <asp:Button ID="btnEditarEnvio" runat="server" Text="✏️" ToolTip="Editar Estado" CssClass="btn btn-sm btn-warning me-1 disabled" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                        <asp:Button ID="btnEliminarEnvio" runat="server" Text="🗑️" ToolTip="Eliminar Estado" CssClass="btn btn-sm btn-danger disabled" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('¿Está seguro de que desea eliminar este envio?');" />
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
