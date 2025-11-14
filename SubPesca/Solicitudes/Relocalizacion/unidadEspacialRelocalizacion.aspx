<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesRelocalizacion.Master" AutoEventWireup="true" 
CodeBehind="unidadEspacialRelocalizacion.aspx.cs"  Inherits="SubPesca.Solicitudes.Relocalizacion.unidadEspacialRelocalizacion" Theme="admin_style" %> 

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="../Registrar/informacionSolicitud.ascx"                   tagname="informacionSolicitud"                 tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
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
                <span id="titulo_modulo"><asp:Label ID="titulo" runat="server"></asp:Label></span>
            </td>
        </tr>
    </table>

    <hr style="width:100%;" />

    <!-- Datos de la Concesión -->
    <fieldset>
        <legend>Datos de la Concesión</legend>

        

        <!-- Crear Concesión -->
        
        <asp:UpdatePanel ID="UpdatePanelMensajeErrores" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="PanelMensajeErrores" runat="server">
                <asp:ValidationSummary ID="valSum" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />    
            </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="UpdatePanelMensajesSuperior" runat="server" UpdateMode="Conditional">
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


        <asp:UpdatePanel ID="UpdatePanelUnidadEspacialRelocalizacion" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelUnidadEspacialRelocalizacion" Visible="true" runat="server">

                <table class="form" cellpadding="0px" cellspacing="0px" width="100%">

                <asp:Panel id="PanelBotonLimpiar" runat="server" Visible="false">
                <tr>
                    <td class="col1" colspan="4"><asp:ImageButton  ID="LimpiarConcesion" runat="server" AlternateText="Limpiar" Height="20px"  ImageUrl="~/App_Themes/admin_style/images/clean.png" onclick="LimpiarConcesion_Click" ToolTip="Limpiar"  ImageAlign="Right" /></td>
                </tr>
                </asp:Panel>
                
                
                <tr>
                    <td class="col1"><span class="item">Código Centro</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3" colspan="2"><asp:TextBox ID="CodigoCentro" MaxLength="15" Width="80px" runat="server" OnTextChanged="VerificarCodigoCentro" autocomplete="tel-extension" AutoPostBack="true"></asp:TextBox></td>
                </tr>

                <asp:Panel ID="PanelMensajeCodigoCentro" Visible="false" runat="server">
                <tr>
                    <td class="col1"><span class="item"></span></td>
                    <td class="col2"><span class="item"></span></td>
                    <td class="col3" colspan="2">
                        <div class="Content_msgGrilla">
                            El código de centro ingresado ya existe como una unidad espacial, si relocaliza esta solicitud se modificará la unidad espacial ya existente.
                        </div>
                    </td>
                </tr>
                </asp:Panel>
                
                <asp:panel id="TRDiarioOficial" runat="server">
                <tr>
                    <td class="col1"><span class="item">Nº Diario Oficial</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3" colspan="2">
                        <asp:TextBox ID="NumeroDiarioOficial" MaxLength="9" Width="80px" runat="server" CausesValidation="true" ToolTip="Nº Diario Oficial" autocomplete="tel-extension"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="NumeroDiarioOficial" ValidationGroup="grupo1" ErrorMessage="Nº Diario Oficial" Display="Static" >*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" ControlToValidate="NumeroDiarioOficial" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="El Nº Diario Oficial debe ser Numérico." Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="col1"><span class="item">Fecha Diario Oficial</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3" colspan="2">
                    
                    <asp:UpdatePanel ID="UpdatePanelFechaDesde" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                    <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaRecepcion" Columns="8" Width="120px" runat="server"></asp:TextBox>
                    </div>

                    <asp:Panel ID="PanelCalendario" Visible="false" runat="server">
                        <div class="calendario_icono">
                            <img  src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaRecepcion" alt="Calendario" style="vertical-align: middle" />
                        </div>
                    </asp:Panel>

                    <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="FechaRecepcion"  ValidationGroup="grupo1"
                        ErrorMessage="Fecha Diario Oficial" Display="Static">*</asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator2" 
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
                </asp:panel>

              <%--  <asp:panel id="TRCapitaniaPuerto" runat="server">
                    <tr>
                       <td class="col1"><span class="item">Capitanía de Puerto</span></td>
                       <td class="col2"><span class="item">:</span></td>
                       <td class="col3" colspan="2">
                           <asp:DropDownList ID="CapitaniaPuerto" runat="server"></asp:DropDownList>
                           <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="CapitaniaPuerto"  ValidationGroup="grupo1"
                             ErrorMessage="Capitanía de Puerto" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                         </td>
                    </tr>
                </asp:panel>--%>

                <asp:Panel id="PanelBotonGuardar" runat="server" Visible="true">
                    <tr>
                       <td class="col1"></td>
                       <td class="col2" colspan="2"></td>
                       <td class="col3">
                             <asp:ImageButton ID="GuardarUnidadEspacial" runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                AlternateText="Crear Concesión" ToolTip="Crear Concesión" onclick="GuardarUnidadEspacial_Click" 
                                CausesValidation="false" ValidationGroup="grupo1" />
                            <span class="item"><asp:label runat="server" ID="textoBoton"></asp:label></span>
                        </td>
                    </tr>
                </asp:Panel>
                </table>


                <br />
                <br />

                <asp:Panel id="PanelBotonRelocalizacion" runat="server" Visible="false">
                    <table class="form" cellpadding="0px" cellspacing="0px" width="100%">   
                    <tr>
                        <td class="col1" colspan="4" align="center">
                            <asp:Button ID="RelocalizarConcesion" runat="server" OnClick="RelocalizarConcesion_Click" Text="Relocalizar Concesión" />
                        </td>
                    </tr>
                    </table>
                </asp:Panel>

            </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>
        
        
        

    </fieldset>
    
    <!-- JAVASCRIPT !-->
    <script type="text/javascript">
        invoca_calendarios("unidadEspacialRelocalizacion");
    </script>

</asp:Content>