<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesAcopio.Master" AutoEventWireup="true" CodeBehind="pestanaInformeAmbientalAcopio.aspx.cs" 
Inherits="SubPesca.Solicitudes.Acopio.pestanaInformeAmbientalAcopio"  Theme="admin_style" %>


<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="../Registrar/informeSEACartaTitular.ascx"              tagname="informeSEACartaTitular"                    tagprefix="uc19" %>
<%@ Register src="../Registrar/informeSEARCA.ascx"                       tagname="informeSEARCA"                             tagprefix="uc18" %>
<%@ Register src="../Registrar/informeSEAConsultaSEA.ascx"               tagname="informeSEAConsultaSEA"                     tagprefix="uc17" %>
<%@ Register src="../Registrar/informeSEACartaAmbiental.ascx"            tagname="informeSEACartaAmbiental"                  tagprefix="uc16" %>
<%@ Register src="../Registrar/informeSEAInformeUnidadAmbiental.ascx"    tagname="informeSEAInformeUnidadAmbiental"          tagprefix="uc15" %>
<%@ Register src="../Registrar/informeSEAObservacion.ascx"               tagname="informeSEAObservacion"                     tagprefix="uc14" %>

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
                    <span id="titulo_modulo">Informe Ambiental</span>
                </td>
            </tr>
            </table>
            <hr style="width:100%;" />

            <asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
               <ContentTemplate>    
                   <asp:panel ID="Panel1" runat="server">
                        <asp:ValidationSummary ID="ValidationSummaryErrores" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="SometimientoSEA"  />
                    </asp:panel>
                </ContentTemplate>
            </asp:UpdatePanel>

      

            <asp:UpdatePanel ID="UpdatePanelSometimientoSEA" UpdateMode="Conditional" runat="server">
                <ContentTemplate>   
                    <asp:Panel ID="PanelSometimientoSEA" CssClass="Content_msgGrilla" Visible="false" runat="server">
                        <div class="msgGrilla_div2">
                            <asp:Label ID="msSometimientoSEA" runat="server"></asp:Label>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>


            <fieldset>
                <legend>Informe SEA</legend>
                <br />

                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Ingresa SEA</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            <asp:DropDownList ID="FlujoSEA"  runat="server"  ValidationGroup="SometimientoSEA"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1"><span class="item"></span></td>
                        <td class="col2"><span class="item"></span></td>
                        <td class="col3"><asp:Button ID="Guardar" runat="server" Text="Guardar"  CausesValidation="true" onclick="Guardar_Click" ValidationGroup="SometimientoSEA" /></td>
                    </tr>
                    </table>
            </fieldset>


            <!-- check debe evaluarse ambientalmente -->
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate> 

                    <asp:Panel ID="Panel2"  Visible="true" runat="server">
                     
                    </asp:Panel>

            </ContentTemplate>
            </asp:UpdatePanel>

                   
            <asp:UpdatePanel ID="UpdatePanelInformeSEA" runat="server"  UpdateMode="Conditional">
                <ContentTemplate>

                    <asp:Panel ID="PanelInformeSEACartaTitular"  Visible="false" runat="server">
                        <uc19:informeSEACartaTitular ID="informeSEACartaTitularUC" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="PanelInformeSEARCA"  Visible="false" runat="server">
                        <uc18:informeSEARCA ID="informeSEARCA" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="PanelInformeSEAConsultaSEA"  Visible="false" runat="server">
                        <uc17:informeSEAConsultaSEA ID="informeSEAConsultaSEA" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="PanelInformeSEACartaAmbiental"  Visible="false" runat="server">
                        <uc16:informeSEACartaAmbiental ID="informeSEACartaAmbiental" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="PanelInformeSEAInformeUnidadAmbiental"  Visible="false" runat="server">
                        <uc15:informeSEAInformeUnidadAmbiental ID="informeSEAInformeUnidadAmbiental" runat="server" />
                    </asp:Panel>

                </ContentTemplate>
            </asp:UpdatePanel>

              

            <asp:Panel ID="PanelInformeSEAObservacion"  Visible="true" runat="server">
                <uc14:informeSEAObservacion ID="informeSEAObservacion" runat="server" />
            </asp:Panel>

    
            

</asp:Content>