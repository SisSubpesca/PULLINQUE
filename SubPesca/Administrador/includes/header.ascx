<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="SubPesca.Administrador.includes.header" %>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <%--Esta linea es para permitir que 2 versiones de jquery funciones en una misma pagina, se debe usar la version 1.7 para el autocompletar--%>
    <script type="text/javascript">
        var $j = jQuery.noConflict();
    </script> 
    

    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.cycle.all.js")); %>" type="text/javascript"></script>

<div id="content_banner">
    <div id="banner_logo">
        <asp:Image ID="Image0" ImageUrl="~/App_Themes/admin_style/images/logo.png" Height="130px" runat="server" />    
    </div>
    <div id="banner">
        <asp:Image ID="Image1" ImageUrl="~/App_Themes/admin_style/images/head1.jpg" Width="1130px" Height="130px" CssClass="first" runat="server" />
        <asp:Image ID="Image2" ImageUrl="~/App_Themes/admin_style/images/head2.jpg" Width="1130px" Height="130px" runat="server" />
        <asp:Image ID="Image3" ImageUrl="~/App_Themes/admin_style/images/head3.jpg" Width="1130px" Height="130px" runat="server" />
    </div>
    <div id="sistema">
        <div id="sistema_logo"></div>
    </div>
</div>

<script type="text/javascript">
    // BANNER
    //$(document).ready(function() {
        //$('#banner').cycle({
           // fx: 'fade', 
            //speed: 1000,
            //timeout: 5000
        //});
        //$('#banner').show();
    //});
</script>
