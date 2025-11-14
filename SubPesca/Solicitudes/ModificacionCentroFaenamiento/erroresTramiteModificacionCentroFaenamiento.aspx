<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="erroresTramiteModificacionCentroFaenamiento.aspx.cs" 
Inherits="SubPesca.Solicitudes.ModificacionCentroFaenamiento.erroresTramiteModificacionCentroFaenamiento" Theme="admin_style" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></asp:ToolkitScriptManager>

    
    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Listado de Errores</span>
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />


    <fieldset>
        <legend>Tramite de Modificación Nº Pert: <asp:Label ID="numeroPert" runat="server"></asp:Label></legend>

        <br />

        <asp:GridView 
           ID="GridErroresSolicitud"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%">
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
               
               <asp:TemplateField HeaderText="Tipo Error Trámite Modificación" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <%# DataBinder.Eval(Container, "DataItem.tipoWarning.descripcion")%>  
                </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Descripción" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <%# DataBinder.Eval(Container, "DataItem.descripcion")%>  
                </ItemTemplate>
               </asp:TemplateField>
            </Columns>

            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#5794EF" />
            <AlternatingRowStyle BackColor="White" />
       </asp:GridView> 

    </fieldset>

</asp:Content>
