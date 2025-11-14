<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="inicioSolicitudExperimentalesConcesion.aspx.cs" 
Inherits="SubPesca.Solicitudes.ExperimentalesConcesion.inicioSolicitudExperimentalesConcesion" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_usuarios.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"  ></asp:ToolkitScriptManager>


    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Nueva Solicitud</span>
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />


    <fieldset>
        <legend>Datos de la Solicitud Experimentales Concesión</legend>
        <br />
        <asp:ValidationSummary ID="ValidationSummaryInicioSolicitud" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
        
        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Número Identificador Solicitud</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="numPert" MaxLength="10" Width="100px"  runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorPert" runat="server" ControlToValidate="numPert"  ValidationGroup="grupo1" ErrorMessage="Número Identificador Solicitud" Display="Static">&nbsp;</asp:RequiredFieldValidator> *
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>


        <tr>
            <td class="col1"><span class="item">Número CI</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="NumeroCI" autocomplete="tel-extension" MaxLength="10" Width="100px"  runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="NumeroCI"  ValidationGroup="grupo1"
                             ErrorMessage="Número CI" Display="Static">&nbsp;</asp:RequiredFieldValidator> *
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr>
            <td class="col1"><span class="item">Fecha CI</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
                <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaCI" Columns="8" Width="120px" runat="server"></asp:TextBox>
                   
                    </div>
                    <div class="calendario_icono">
                        <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaCI" alt="Calendario" style="vertical-align: middle" />
                    </div>

                    <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="FechaCI"  ValidationGroup="grupo1"
                        ErrorMessage="Fecha CI" Display="Static">&nbsp;</asp:RequiredFieldValidator> *

                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator3" 
                        runat="server"
                        ControlToValidate="FechaCI"
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
            <td class="col1"><span class="item">Código Concesión Acuicultura</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:TextBox ID="CodigoCentro" runat="server" AutoPostBack="true"
                    OnTextChanged="CodigoAcuiculturaAmerb_OnTextChanged" CausesValidation="true" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidatorCodigoCentro" runat="server" ControlToValidate="CodigoCentro"  ValidationGroup="grupo1"
                        ErrorMessage="Código Concesión Acuicultura" Display="Static">&nbsp;</asp:RequiredFieldValidator> *     
                
            </td>
        </tr>
       
       
        <tr>
            <td class="col1"></td>
            <td class="col2"></td>
            <td class="col3">
                <asp:Panel ID="PanelBotonIngresar" Visible="false" runat="server">
                    <asp:Button ID="Filtrar" runat="server" OnClick="Guardar_Click" Text="Ingresar" CausesValidation="true" ValidationGroup="grupo1"/>
                </asp:Panel>
            </td>
        </tr>
        </table>
    </fieldset>


      <!-- JAVASCRIPT !-->
    <script type="text/javascript">
        invoca_calendarios("inicioSolicitud");
    </script>
    
</asp:Content>
