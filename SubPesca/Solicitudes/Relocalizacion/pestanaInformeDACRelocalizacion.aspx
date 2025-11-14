<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesRelocalizacion.Master" AutoEventWireup="true" CodeBehind="pestanaInformeDACRelocalizacion.aspx.cs"
Inherits="SubPesca.Solicitudes.Relocalizacion.pestanaInformeDACRelocalizacion" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="../Registrar/informeDAC.ascx"                                 tagname="informeDAC"                    tagprefix="uc8" %>
<%@ Register src="../Registrar/informeDACDevolucionJuridica.ascx"               tagname="informeDACDevolucionJuridica"  tagprefix="uc7" %>
<%@ Register src="../Registrar/informeDACObservacion.ascx"                      tagname="informeDACObservacion"         tagprefix="uc6" %> 
<%@ Register src="~/Solicitudes/Registrar/checkCertificadoOperacion.ascx"       tagname="checkCertificadoOperacion"     tagprefix="uc1" %>

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
                    <span id="titulo_modulo">Informe DAC</span>
                </td>
            </tr>
            </table>
            <hr style="width:100%;" />

            <!-- check certificado operacion -->
            <asp:UpdatePanel ID="UpdatePanelCheck" UpdateMode="Conditional" runat="server">
            <ContentTemplate> 

                    <asp:Panel ID="PanelCheck"  Visible="true" runat="server">
                        <uc1:checkCertificadoOperacion ID="checkCertificadoOperacion" runat="server" />
                    </asp:Panel>

            </ContentTemplate>
            </asp:UpdatePanel>

            <asp:Panel ID="PanelInformeDAC"  Visible="true" runat="server">
                <uc8:informeDAC ID="informeDAC" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelInformeDACDevolucionJuridica"  Visible="true" runat="server">
                <uc7:informeDACDevolucionJuridica ID="informeDACDevolucionJuridica" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelInformeDACObservacion"  Visible="true" runat="server">
                <uc6:informeDACObservacion ID="informeDACObservacion" runat="server" />
            </asp:Panel>
    
            

</asp:Content>