<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tpc_equipo_7A.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Carousel Banner --%>
    <div id="carouselBanner" class="carousel slide w-75 mx-auto mb-5 mt-3" data-bs-ride="carousel">
        <div class="carousel-inner">
            <div class="carousel-item active">
                <div class="d-flex justify-content-center gap-3">
                    <img src="https://www.bigbuy.com.py/imagenes/sin_imagen.jpg" class="d-block" style="width: 25%; max-height: 300px; object-fit: contain;">
                    <img src="https://www.bigbuy.com.py/imagenes/sin_imagen.jpg" class="d-block" style="width: 25%; max-height: 300px; object-fit: contain;">
                    <img src="https://www.bigbuy.com.py/imagenes/sin_imagen.jpg" class="d-block" style="width: 25%; max-height: 300px; object-fit: contain;">
                </div>
            </div>
            <div class="carousel-item">
                <div class="d-flex justify-content-center gap-3">
                    <img src="https://www.bigbuy.com.py/imagenes/sin_imagen.jpg" class="d-block" style="width: 25%; max-height: 300px; object-fit: contain;">
                    <img src="https://www.bigbuy.com.py/imagenes/sin_imagen.jpg" class="d-block" style="width: 25%; max-height: 300px; object-fit: contain;">
                    <img src="https://www.bigbuy.com.py/imagenes/sin_imagen.jpg" class="d-block" style="width: 25%; max-height: 300px; object-fit: contain;">
                </div>
            </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselBanner" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" style="filter: invert(1);"></span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselBanner" data-bs-slide="next">
            <span class="carousel-control-next-icon" style="filter: invert(1);"></span>
        </button>
    </div>

    <%-- Product Grid --%>
    <div class="container">
        <div class="row row-cols-1 row-cols-md-3 g-4">
            <asp:Repeater ID="repRepetidor" runat="server">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100 shadow-sm">
                            <a href="DetalleProducto.aspx?id=<%# Eval("Id") %>" style="text-decoration: none; color: inherit;">
                                <img src="<%# Eval("ImagenUrl") %>" class="card-img-top" alt="Imagen del producto"
                                    style="height: 250px; object-fit: contain; padding: 10px;"
                                    onerror="this.src='https://placehold.co/600x400?text=No+Image'">
                                <div class="card-body d-flex flex-column">
                                    <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                    <p class="card-text text-truncate"><%# Eval("Descripcion") %></p>
                                    <h4 class="card-text mt-auto text-primary">$<%# Eval("Precio", "{0:N2}") %></h4>
                                </div>
                            </a>
                            <div class="card-footer bg-transparent border-top-0">
                                <div class="d-flex align-items-center gap-2">
                                    <asp:TextBox ID="txtCantidad" runat="server" Text="1"
                                        CssClass="form-control form-control-sm"
                                        Width="50px"></asp:TextBox>

                                    <asp:Button ID="btnAgregarCarrito" runat="server" Text="Agregar"
                                        CssClass="btn btn-primary btn-sm"
                                        CommandArgument='<%# Eval("Id") %>'
                                        OnClick="btnAgregarCarrito_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

<%-- Toast --%>

        <div class="toast-container position-fixed bottom-0 end-0 p-3">
          <div id="toastLogin" class="toast align-items-center text-bg-success border-0" role="alert" aria-live="assertive" aria-atomic="true" data-bs-autohide="true" data-bs-delay="3000">
            <div class="d-flex">
              <div id="toastLoginBody" class="toast-body">Sesión iniciada</div>
              <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
          </div>
        </div>




<script>
    function mostrarToastLogin() {
        const element = document.getElementById('toastLogin');
        const toast = new bootstrap.Toast(element);
        toast.show();
    }
</script>



</asp:Content>
