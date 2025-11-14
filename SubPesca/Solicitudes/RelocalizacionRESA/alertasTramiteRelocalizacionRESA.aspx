<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administrador/SitioAdmin.Master" CodeBehind="alertasTramiteRelocalizacionRESA.aspx.cs" 
Inherits="SubPesca.Solicitudes.RelocalizacionRESA.alertasTramiteRelocalizacionRESA" Theme="admin_style" %>

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
            <span id="titulo_modulo">Listado de Alertas</span>
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />


    <fieldset>
        <legend>Tramite de Relocalización RESA Nº Pert: <asp:Label ID="numeroPert" runat="server"></asp:Label></legend>

        <br />
            
            <asp:Label ID="mensajeCeroAlertas" runat="server"></asp:Label>

           <asp:ListView 
                ID="ListViewErrores" 
                runat="server" 
                DataKeyNames="idError"
                OnItemDataBound="ListViewErrores_ItemDataBound">
                
            <LayoutTemplate>
                        <table runat="server" id="table1" border="0" cellpadding="2" cellspacing="2" width="100%" style="border: 1px solid Silver; width: 100%; border-collapse: collapse;">
                        <tr style="background-color: SkyBlue; height: 30px;">
                            <td align="center"><b></b></td>
                        </tr>
                        <tr runat="server" id="itemPlaceholder"></tr>
                        </table>
                </LayoutTemplate>

                <ItemTemplate>
                    <tr id="Tr1" runat="server">
    
                        <td id="Td1" runat="server" align="left">
                            <asp:HiddenField runat="server" ID="HiddenIdError"          Value='<%#Eval("idError") %>' />
                            <asp:HiddenField runat="server" ID="HiddenEstadoErrorId"    Value='<%#Eval("estadoError.id") %>' />
                            <asp:Label ID="Label2" runat="server"  Text='<%#Eval("tipoError.descripcion") %>' /> <asp:Label ID="Label1" runat="server"  Text='<%#Eval("detalleError") %>' />
                        </td>
                        
                    </tr>

                </ItemTemplate>
            </asp:ListView>


    </fieldset>

</asp:Content>