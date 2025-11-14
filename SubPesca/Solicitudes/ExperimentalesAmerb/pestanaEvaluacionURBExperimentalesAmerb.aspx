<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesExperimentalesAmerb.Master" AutoEventWireup="true" CodeBehind="pestanaEvaluacionURBExperimentalesAmerb.aspx.cs" 
Inherits="SubPesca.Solicitudes.ExperimentalesAmerb.pestanaEvaluacionURBExperimentalesAmerb"  Theme="admin_style" %>



<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="../Registrar/antecedentesURB.ascx"                                tagname="antecedentesURB"                       tagprefix="uc34" %> 

<%@ Register src="../Registrar/informacionSolicitud.ascx"            tagname="informacionSolicitud"                          tagprefix="uc0" %>      

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"></asp:ToolkitScriptManager>
    
        <asp:UpdatePanel ID="UpdatePanelInformacionSolicitud" UpdateMode="Conditional" runat="server">
            <ContentTemplate> 
                <asp:Panel ID="PanelInformacionSolicitud"  Visible="true" runat="server">
                    <uc0:informacionSolicitud ID="informacionSolicitud" runat="server" />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


              <!-- Título de la página -->
            <table class="formtop" cellpadding="0px" cellspacing="0px">
            <tr>
                <td align="left" valign="middle">
                    <span id="titulo_modulo">Antecedentes URB</span>
                </td>
            </tr>
            </table>
            <hr style="width:100%;" />
            <asp:Panel ID="PanelAntecedentesURB"  Visible="true" runat="server">
                <uc34:antecedentesURB ID="antecedentesURB" runat="server" />
            </asp:Panel>

            

    

</asp:Content>