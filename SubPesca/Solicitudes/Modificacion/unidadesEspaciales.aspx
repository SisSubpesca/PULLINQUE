<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesModificacion.Master" AutoEventWireup="true" CodeBehind="unidadesEspaciales.aspx.cs" 
Inherits="SubPesca.Solicitudes.Modificacion.unidadesEspaciales" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="~/Solicitudes/Registrar/informacionSolicitud.ascx"                   tagname="informacionSolicitud"                 tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="FormularioUnidadesEspeciales" ContentPlaceHolderID="rightbody" runat="server">

    <asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManagerUnidadesEspeciales" runat="server" EnableScriptGlobalization="True"></asp:ToolkitScriptManager>

     <asp:UpdatePanel ID="UpdatePanelInformacionSolicitud" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
                    <asp:Panel ID="PanelInformacionSolicitud"  Visible="true" runat="server">
                        <uc1:informacionSolicitud ID="informacionSolicitud" runat="server" />
                    </asp:Panel>
            </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Modificación de Concesión</span>
            </td>
        </tr>
    </table>

    <hr style="width:100%;" />

    <!-- Datos de la Concesión -->
    <fieldset>
        <legend>Datos de la Concesión</legend>

        <asp:HiddenField ID="IdUnidadEspacial" runat="server" Value="0"></asp:HiddenField>

        <!-- Crear Concesión -->
        
        <asp:ValidationSummary ID="valSum" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />

        <asp:UpdatePanel ID="UpdatePanelMensaje" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        <asp:Panel ID="Content_msgGrillaGral_1" CssClass="Content_msgGrilla" Visible="false" runat="server">
            <div class="msgGrilla_div1">
                <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
            </div>
            <div class="msgGrilla_div2">
                <asp:Label ID="msgGrillaGral_1" runat="server"></asp:Label>
            </div>
        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>

        <table class="form" cellpadding="0px" cellspacing="0px" width="100%">

        <asp:UpdatePanel ID="UpdatePanelBotonLimpiar" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        <asp:Panel id="PanelBotonLimpiar" runat="server" Visible="false">
        <tr>
            <td class="col1" colspan="2">&nbsp;</td>
            <td class="col1" colspan="2"><asp:ImageButton  ID="LimpiarConcesion" runat="server" AlternateText="Limpiar Concesion" Height="20px"  ImageUrl="~/App_Themes/admin_style/images/clean.png"  onclick="LimpiarConcesion_Click" ToolTip="Limpiar Concesion"  ImageAlign="Right" /></td>
        </tr>
        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>

        <tr>
            <td class="col1"><span class="item">Código Centro</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="upd5" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="CodigoCentro" runat="server"></asp:HiddenField>
                    <asp:TextBox ID="CodigoCentroReadOnly" MaxLength="15" Width="80px" runat="server" ReadOnly="true" CssClass="campoDeshabilitado"></asp:TextBox> 
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>

        </table>

           <asp:UpdatePanel ID="UpdatePanelModConcesion" UpdateMode="Conditional" runat="server">
           <ContentTemplate>
           <asp:Panel ID="PanelModConcesion" Visible="false" runat="server">
           
                <table class="form" cellpadding="0px" cellspacing="0px" width="100%">    
                <tr>
                    <td class="col1"><span class="item">Nº Diario Oficial</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                        <asp:TextBox ID="NumeroDiarioOficial" MaxLength="15" Width="80px" runat="server" CausesValidation="true"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidatorNumeroDiarioOficial" ControlToValidate="NumeroDiarioOficial" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="El Nº Diario Oficial debe ser Numérico." Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>

                    </td>
                </tr>
                <tr>
                    <td class="col1"><span class="item">Fecha Diario Oficial</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                    
                    <asp:UpdatePanel ID="UpdatePanelFechaDesde" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                    <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaDiarioOficial" Columns="8" Width="120px" runat="server"></asp:TextBox>
                   
                    </div>

                    <asp:Panel ID="PanelCalendario" Visible="false" runat="server">
                        <div class="calendario_icono">
                            <img  src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaDiarioOficial" alt="Calendario" style="vertical-align: middle" />
                        </div>
                    </asp:Panel>

                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator2" 
                        runat="server"
                        ControlToValidate="FechaDiarioOficial"
                        ForeColor="Red"
                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d\s(([0-1][0-9])|([0-2][0-3])):[0-5][0-9]$" 
                        ErrorMessage="Ingrese formato válido"
                        ValidationGroup="grupo1">
                        </asp:RegularExpressionValidator>
                          
                     </div></ContentTemplate>
                     </asp:UpdatePanel>
                    </td>
                </tr>
                
            
                </table>

           </asp:Panel>
           </ContentTemplate>
           </asp:UpdatePanel>


           <asp:Panel id="PanelBotonCreacion" runat="server" Visible="false">

               <asp:UpdatePanel ID="UpdatePanelGuardarUnidEspacial" UpdateMode="Conditional" runat="server">
               <ContentTemplate>
               <asp:Panel ID="PanelGuardarUnidEspacial" Visible="true" runat="server">
               <table class="form" cellpadding="0px" cellspacing="0px" width="100%">   
               <tr>
                    <td class="col1" colspan="4" align="left">
                        <asp:Button ID="GuardarUnidEspacial" runat="server" Text="Guardar Unidad Espacial" CausesValidation="true" ValidationGroup="grupo1" 
                        AlternateText="Guardar Unidad Espacial" ToolTip="Guardar Unidad Espacial" onclick="GuardarUnidEspacial_Click" />
                    </td>
                </tr>
                </table>

                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>

               <asp:UpdatePanel ID="UpdatePanelModificaConcesion" UpdateMode="Conditional" runat="server">
               <ContentTemplate>
               <asp:Panel ID="PanelModificaConcesion" Visible="false" runat="server">

                   <table class="form" cellpadding="0px" cellspacing="0px" width="100%">   
                   <tr>
                        <td class="col1" colspan="4" align="center">
                            <asp:ImageButton ID="CrearConcesionButton" runat="server" 
                                ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                            AlternateText="Modificar Concesión" ToolTip="Modificar Concesión" onclick="CrearConcesion_Click" 
                            CausesValidation="true" ValidationGroup="grupo1" style="width: 20px" />
                            <span class="item">Modificar Concesión</span>
                        </td>
                    </tr>
                    </table>

                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>

            </asp:Panel>

    </fieldset>
    
    <!-- JAVASCRIPT !-->
    <script type="text/javascript">
        invoca_calendarios("modConcesion");
    </script>

</asp:Content>