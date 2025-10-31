<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tpc_equipo_7A.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="carouselBanner" class="carousel slide w-75 mx-auto" data-bs-ride="carousel">
   
        <div class="carousel-inner">
        <div class="carousel-item active">
    <div class="d-flex justify-content-center gap-3">
      <img src="https://www.bigbuy.com.py/imagenes/sin_imagen.jpg" class="d-block" style="width: 25%; max-height:300px; object-fit:contain;">
      <img src="https://www.bigbuy.com.py/imagenes/sin_imagen.jpg" class="d-block" style="width: 25%; max-height:300px; object-fit:contain;">
      <img src="https://www.bigbuy.com.py/imagenes/sin_imagen.jpg" class="d-block" style="width: 25%; max-height:300px; object-fit:contain;">
    </div>
   </div>
    <div class="carousel-item">
      <div class="d-flex justify-content-center gap-3">
        <img src="https://www.bigbuy.com.py/imagenes/sin_imagen.jpg" class="d-block" style="width: 25%; max-height:300px; object-fit:contain;">
        <img src="https://www.bigbuy.com.py/imagenes/sin_imagen.jpg" class="d-block" style="width: 25%; max-height:300px; object-fit:contain;">
        <img src="https://www.bigbuy.com.py/imagenes/sin_imagen.jpg" class="d-block" style="width: 25%; max-height:300px; object-fit:contain;">
      </div>
    </div>
   </div>
  <button class="carousel-control-prev" type="button" data-bs-target="#carouselBanner" data-bs-slide="prev">
    <span class="carousel-control-prev-icon"></span>
  </button>
  <button class="carousel-control-next" type="button" data-bs-target="#carouselBanner" data-bs-slide="next">
    <span class="carousel-control-next-icon"></span>
  </button>
</div>

</asp:Content>
