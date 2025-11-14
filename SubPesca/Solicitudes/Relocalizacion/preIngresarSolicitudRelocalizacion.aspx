<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="preIngresarSolicitudRelocalizacion.aspx.cs" 
Inherits="SubPesca.Solicitudes.Relocalizacion.preIngresarSolicitudRelocalizacion" Theme="admin_style" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>
   


    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Generar Solicitud de Relocalización</span>
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />


    <asp:ValidationSummary ID="ValidationSummaryInicioSolicitud" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="ValidationSummaryErrores" />

    <fieldset>
        <legend>Datos de la Solicitud</legend>
        <br />
       
        
            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1"><span class="item">Nº PERT</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3"><asp:TextBox  ID="NumPert" runat="server" MaxLength="10" Columns="10" onKeyUp="return onlyNumeric(this);" ValidationGroup="test"></asp:TextBox>
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorPert" runat="server" ControlToValidate="NumPert"  ValidationGroup="ValidationSummaryErrores" ErrorMessage="Nº Pert" Display="Static">&nbsp;</asp:RequiredFieldValidator> *
                </td>
            </tr>
            <tr>
                <td class="col1"><span class="item">Fecha de Recepción</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                   
                    <asp:UpdatePanel ID="UpdatePanelFechaDesde" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                    <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaRecepcion" Columns="8" Width="120px" runat="server"></asp:TextBox>
                   
                    </div>
                    <div class="calendario_icono">
                        <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaRecepcion" alt="Calendario" style="vertical-align: middle" />
                    </div>

                    <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="FechaRecepcion"  ValidationGroup="grupo1"
                        ErrorMessage="Fecha de Recepción" Display="Static">&nbsp;</asp:RequiredFieldValidator> *

                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator1" 
                        runat="server"
                        ControlToValidate="FechaRecepcion"
                        ForeColor="Red"
                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d\s(([0-1][0-9])|([0-2][0-3])):[0-5][0-9]$" 
                        ErrorMessage="Ingrese formato válido"
                        ValidationGroup="grupo1">
                        </asp:RegularExpressionValidator>
                          
                     </div></ContentTemplate>
                    </asp:UpdatePanel>

                </td>
            </tr>

            <tr>
                <td class="col1"><span class="item">Fecha de Ingreso a Trámite</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                   
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                    <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaIngresoTramite" Columns="8" Width="120px" runat="server"></asp:TextBox>
                   
                    </div>
                    <div class="calendario_icono">
                        <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaIngresoTramite" alt="Calendario" style="vertical-align: middle" />
                    </div>

                    <asp:RequiredFieldValidator id="RequiredFieldValidatorFechRecep" runat="server" ControlToValidate="FechaIngresoTramite"  ValidationGroup="grupo1"
                        ErrorMessage="Fecha de Ingreso a Trámite" Display="Static">&nbsp;</asp:RequiredFieldValidator> *

                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator2" 
                        runat="server"
                        ControlToValidate="FechaIngresoTramite"
                        ForeColor="Red"
                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d\s(([0-1][0-9])|([0-2][0-3])):[0-5][0-9]$" 
                        ErrorMessage="Ingrese formato válido"
                        ValidationGroup="grupo1">
                        </asp:RegularExpressionValidator>
                          
                    </div></ContentTemplate>
                    </asp:UpdatePanel>

                </td>
            </tr>
            <tr>
                <td class="col1"><span class="item">&nbsp;</span></td>
                <td class="col2"><span class="item">&nbsp;</span></td>
                <td class="col3"><span class="item">
                
                    <asp:Panel ID="PanelBotonGuardar" runat="server" Visible="false">
                        <asp:Button ID="Guardar" runat="server" Text="Generar" CausesValidation="true" ValidationGroup="ValidationSummaryErrores" onclick="Generar_Click"  /></span>
                    </asp:Panel>
                </td>
            </tr>

            </table>
        
      
    </fieldset>

        <!-- JAVASCRIPT !-->
    <script type="text/javascript">
        invoca_calendarios("preIngresarSolicitudRelocalizacion");
    </script>
    
    
</asp:Content>


