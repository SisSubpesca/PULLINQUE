<%@ Page Language="C#"  MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="redefinirTramiteModificacionECMPO.aspx.cs" 
Inherits="SubPesca.Solicitudes.ModificacionECMPO.redefinirTramiteModificacionECMPO" Theme="admin_style" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>

    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Redefinir Tr&aacute;mite de Modificac&oacute;n de ECMPO</span>
            </td>
        </tr>
    </table>
    <hr style="width:100%;" />


    <asp:UpdatePanel ID="UpdatePanelErroresValidacion" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 
    <asp:ValidationSummary ID="ValidationSummaryInicioSolicitud" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="InicioModificacion" />


    <fieldset>
        <legend>Datos de la Solicitud</legend>
        <br />
       
        
            <table class="form" cellpadding="0px" cellspacing="0px">

            <asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>
                
                <asp:UpdatePanel ID="UpdatePanelDatos" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <asp:Panel ID="PanelDatos" Visible="true" runat="server">
               
                    <tr>
                        <td class="col1"><span class="item">Código de Centro</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="CodigoCentro" AutoPostBack="true" runat="server" CausesValidation="false" Enabled="false"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="col1"><span class="item">Nº PERT</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox  ID="NumPert" runat="server" MaxLength="10" Columns="10" CausesValidation="false" Enabled="false"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td class="col1"><span class="item">Fecha de Recepción</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="FechaRecepcion" Columns="8" Width="120px" runat="server" CausesValidation="false" Enabled="false"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td class="col1"><span class="item">Fecha de Ingreso a Trámite</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="FechaIngresoTramite" Columns="8" Width="120px" runat="server" CausesValidation="false" Enabled="false"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td class="col1"><span class="item">Tipo Modificación actual</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:Label ID="TipoModificacion" runat="server"></asp:Label></td>
                    </tr>
            
                   
                </asp:Panel>      
                </ContentTemplate>  
                </asp:UpdatePanel>
               
            

            <tr>
                <td class="col1" rowspan="5"><span class="item">Nuevo Tipo de Modificación</span></td>
                <td class="col2" rowspan="5"><span class="item">:</span></td>
                <td class="col3" align="left"><asp:CheckBox ID="Especie" runat="server" Text="Especie" AutoPostBack="true"/></td>
            </tr>
            <tr>
                <td class="col3"><asp:CheckBox ID="ProyectoTecnico" runat="server" Text="Proyecto Técnico" AutoPostBack="true"/></td>
            </tr>
            <tr>
                <td class="col3"><asp:CheckBox ID="ampliacionSuperficie" runat="server" Text="Ampliación Superficie"  AutoPostBack="true"/></td>
            </tr>
            <tr>
                <td class="col3"><asp:CheckBox ID="reduccionSuperficie" runat="server" Text="Reducción Superficie"  AutoPostBack="true"/></td>
            </tr>
            <tr>
                <td class="col3"><asp:CheckBox ID="regularizacion" runat="server" Text="Regularización" AutoPostBack="true" /></td>
            </tr>
            

          
            
            <asp:Panel ID="panelCampos3" Visible="true" runat="server">     
            <asp:UpdatePanel ID="UpdatePanelCampos3" UpdateMode="Conditional" runat="server"> 
            <ContentTemplate>
            <tr>
                    <td class="col1"><span class="item">&nbsp;</span></td><td class="col2"><span class="item">&nbsp;</span></td>
                    <td class="col3"><span class="item"><asp:Button ID="Guardar" runat="server" Text="Redefinir"  CausesValidation="true" ValidationGroup="InicioModificacion"  onclick="Guardar_Click" /></span></td>
            </tr>
            </ContentTemplate>
            </asp:UpdatePanel>
            </asp:Panel>


    </table>
        
      
    </fieldset>
    </ContentTemplate>
    </asp:UpdatePanel>


    <div id="cargando" class="message">
        <div class="background"></div>
        <div style="width:100%; text-align:center; margin-top:350px;">
            <asp:Image ID="Image1" ImageUrl="~/App_Themes/admin_style/images/loading.gif" Width="100px" runat="server" />
        </div>
    </div>

            
</asp:Content>
     
           