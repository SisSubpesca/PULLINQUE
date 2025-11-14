<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="inicioSolicitudExperimentalesAmerb.aspx.cs" 
Inherits="SubPesca.Solicitudes.ExperimentalesAmerb.inicioSolicitudExperimentalesAmerb" Theme="admin_style" %>

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
        <legend>Datos de la Solicitud Experimentales Amerb</legend>
        <br />
        <asp:ValidationSummary ID="ValidationSummaryInicioSolicitud" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
        
        <table class="form" cellpadding="0px" cellspacing="0px">
        


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

                    <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="FechaRecepcion"  ValidationGroup="grupo1"
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
            <td class="col1"><span class="item">Número CI</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="NumeroCI" autocomplete="tel-extension" MaxLength="10" Width="100px"  runat="server"
                         ontextchanged="NumeroCI_TextChanged"
                            AutoPostBack="true" onKeyUp="return onlyNumeric(this)"></asp:TextBox>
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
                        <asp:TextBox ID="FechaCI" Columns="8" Width="120px" runat="server"
                        ontextchanged="FechaCI_TextChanged" AutoPostBack="true"></asp:TextBox>
                   
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
            <td class="col1"><span class="item">Código Acuicultura en Amerb</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:TextBox ID="CodigoAcuiculturaAmerb" runat="server" AutoPostBack="true" OnTextChanged="CodigoAcuiculturaAmerb_OnTextChanged" CausesValidation="true" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)"></asp:TextBox>
            </td>
        </tr>


        <tr>
            <td class="col1"><span class="item">Lugar de Ingreso</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="Oficina"  Width="200px"  runat="server"  AutoPostBack="true"
                            OnSelectedIndexChanged="Oficina_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ControlToValidate="Oficina"  InitialValue="0" ValidationGroup="grupo1" ErrorMessage="Lugar de Ingreso" Display="Static">&nbsp;</asp:RequiredFieldValidator> *
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr>
            <td class="col1"><span class="item">N° trámite</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanelPert" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="numPert"  Width="200px"  ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
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

