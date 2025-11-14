<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesColector.Master" AutoEventWireup="true" 
CodeBehind="unidadEspacialColector.aspx.cs" Inherits="SubPesca.Solicitudes.Colector.unidadEspacialColector" Theme="admin_style" %>


<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="../Registrar/informacionSolicitud.ascx"                   tagname="informacionSolicitud"                 tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
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
                <span id="titulo_modulo">Creación de Colector de Semilla</span>
            </td>
        </tr>
    </table>

    <hr style="width:100%;" />

    <!-- Datos de la Concesión -->
    <fieldset>
        <legend>Datos del Colector de Semilla</legend>

        <asp:HiddenField ID="IdUnidadEspacial" runat="server" Value="0"></asp:HiddenField>

        <!-- Crear Concesión -->
        
        <asp:ValidationSummary ID="valSum" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />

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

        <br /> 
                <table class="form" cellpadding="0px" cellspacing="0px">
                
                <asp:UpdatePanel ID="UpdatePanelCodigoCentro" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="PanelCodigoCentro" runat="server" Visible="true">
                        
                        <tr>
                           <td class="col1"><span class="item">Código Centro</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3"><asp:TextBox ID="CodigoCentro" MaxLength="15" Width="80px" runat="server" CausesValidation="true"></asp:TextBox></td>
                        </tr>

                    </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>

               
                <asp:UpdatePanel ID="UpdatePanelNumeroDiarioOficial" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="PanelNumeroDiarioOficial" runat="server" Visible="true">
                    
                        <tr>
                           <td class="col1"><span class="item">Nº Diario Oficial</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3">
                                <asp:TextBox ID="NumeroDiarioOficial" MaxLength="15" Width="80px" runat="server" CausesValidation="true"></asp:TextBox>
                                <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="NumeroDiarioOficial" ValidationGroup="grupo1" ErrorMessage="Nº Diario Oficial" Display="Static" >*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidatorNumeroDiarioOficial" ControlToValidate="NumeroDiarioOficial" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="El Nº Diario Oficial debe ser Numérico." Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
                            </td>
                        </tr>

                    </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="UpdatePanelFechaDiarioOficial" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="PanelFechaDiarioOficial" runat="server" Visible="true">

                         <tr>
                            <td class="col1"><span class="item">Fecha Diario Oficial</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3">
                    
                                <div class="calendario">
                                    <div class="calendario_textbox">               
                                        <asp:TextBox ID="FechaDiarioOficial" Width="120px" runat="server"></asp:TextBox>
                                        <asp:Panel ID="ImagenFecha2" runat="server" style="display:inline;">
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Enabled="True" FirstDayOfWeek="Monday"
                                            Format="dd'/'MM'/'yyyy HH':'mm'" TargetControlID="FechaDiarioOficial" PopupButtonID="endCal2"  CssClass="cal_Theme1"/>
                                            <img runat="server" id="endCal2" alt ="CalendarioTramite" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                                             <asp:RequiredFieldValidator id="RequiredFieldValidatorFechaIngresoTramite" runat="server" ControlToValidate="FechaDiarioOficial"  ValidationGroup="grupo1"
                                             ErrorMessage="Fecha de Ingreso a Trámite" Display="Static">*</asp:RequiredFieldValidator>
                                             <asp:RegularExpressionValidator 
                                             ID="RegularExpressionValidator2" 
                                             runat="server"
                                             ControlToValidate="FechaDiarioOficial"
                                             ForeColor="Red"
                                             ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d\s(([0-1][0-9])|([2][0-3])):[0-5][0-9]$" 
                                             ErrorMessage="Ingrese formato válido"
                                             ValidationGroup="grupo1">
                                            </asp:RegularExpressionValidator>
                                        </asp:Panel>
                                    </div>
                                </div>
                           </td>
                        </tr>

                    </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>


                <asp:UpdatePanel ID="UpdatePanelNumeroActaEntrega" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="PanelNumeroActaEntrega" runat="server" Visible="true">

                        <tr>
                            <td class="col1"><span class="item">Nº Acta de Entrega</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3">
                   
                            <asp:TextBox ID="NumeroActaEntrega" MaxLength="15" Width="80px" runat="server" CausesValidation="true"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidatorNumeroActaEntrega" ControlToValidate="NumeroActaEntrega" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="El Nº Acta de Entrega debe ser Numérico." Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>

                            </td>
                        </tr>

                    </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>


                <asp:UpdatePanel ID="UpdatePanelFechaActaEntrega" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="PanelFechaActaEntrega" runat="server" Visible="true">

                        <tr>
                           <td class="col1"><span class="item">Fecha Acta Entrega</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3">
                    
                                 <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                    <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaActaEntrega" Columns="8" Width="120px" runat="server"></asp:TextBox>
                   
                    </div>

                    <asp:Panel ID="PanelCalendarioFechaActaEntrega" Visible="false" runat="server">
                        <div class="calendario_icono">
                            <img  src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaActaEntrega" alt="Calendario" style="vertical-align: middle" />
                        </div>
                    </asp:Panel>
                    
                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidatorFechaActaEntrega" 
                        runat="server"
                        ControlToValidate="FechaActaEntrega"
                        ForeColor="Red"
                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d\s(([0-1][0-9])|([0-2][0-3])):[0-5][0-9]$" 
                        ErrorMessage="Ingrese formato válido"
                        ValidationGroup="grupo1">
                        </asp:RegularExpressionValidator>
                          
                     </div></ContentTemplate>
                     </asp:UpdatePanel>
            
                           </td>
                        </tr>

                    </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>

                
                <asp:UpdatePanel ID="UpdatePanelCapitaniaDePuerto" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="PanelCapitaniaDePuerto" runat="server" Visible="true">
                        <tr>
                           <td class="col1"><span class="item">Capitanía de Puerto</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3">
                               <asp:DropDownList ID="CapitaniaPuerto" runat="server"></asp:DropDownList>
                               <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="CapitaniaPuerto"  ValidationGroup="grupo1"
                                 ErrorMessage="Capitanía de Puerto" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                             </td>
                        </tr>
                    </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>

      
                <asp:UpdatePanel ID="UpdatePanelPlazoNominalInicio" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <asp:Panel ID="PanelPlazoNominalInicio" Visible="false" runat="server">
                <tr>
                    <td class="col1"><span class="item">Plazo de Inicio</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                       
                <asp:UpdatePanel ID="UpdatePanelPlazoInicio" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="PlazoInicio" Columns="8" Width="120px" runat="server" CausesValidation = "true"></asp:TextBox>
                   
                    </div>

                    <asp:Panel ID="PanelPlazoInicio" Visible="true" runat="server">
                        <div class="calendario_icono">
                            <img  src="../../App_Themes/admin_style/images/calendar.png" id="imgPlazoInicio" alt="Calendario" style="vertical-align: middle" />
                        </div>
                    </asp:Panel>


                    <asp:RequiredFieldValidator id="RequiredFieldValidatorPlazoInicio" runat="server" ControlToValidate="PlazoInicio"  ValidationGroup="grupo1"
                        ErrorMessage="Plazo de Inicio" Display="Static">*</asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidatorPlazoInicio" 
                        runat="server"
                        ControlToValidate="PlazoInicio"
                        ForeColor="Red"
                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d\s(([0-1][0-9])|([0-2][0-3])):[0-5][0-9]$" 
                        ErrorMessage="Ingrese formato válido"
                        ValidationGroup="grupo1">
                        </asp:RegularExpressionValidator>
                          
                     </div></ContentTemplate>
                     </asp:UpdatePanel>
                    </td>
                </tr>
                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>


                <asp:UpdatePanel ID="UpdatePanelPlazoNominal" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <asp:Panel ID="PanelPlazoNominal" Visible="false" runat="server">
                <tr>
                    <td class="col1"><span class="item">Plazo Nominal</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                       
                        <asp:DropDownList ID="PlazoNominal" runat="server" CausesValidation ="true" OnSelectedIndexChanged="PlazoNominal_change" AutoPostBack="true"></asp:DropDownList>
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorPlazoNominal" runat="server" ControlToValidate="PlazoNominal" ValidationGroup="grupo1" ErrorMessage="Plazo Nominal" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>

                    </td>
                </tr>
                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>


                <asp:UpdatePanel ID="UpdatePanelNumeroPlazo" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <asp:Panel ID="PanelNumeroPlazo" Visible="false" runat="server">
                <tr>
                    <td class="col1"><span class="item">Nº de Mes/Año</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                   
                    <asp:TextBox ID="NumeroPlazo" MaxLength="3" Width="80px" runat="server" CausesValidation="true"  OnTextChanged="NumeroPlazo_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidatorNumeroPlazo" ControlToValidate="NumeroPlazo" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="El Nº de Mes/Año debe ser Numérico." Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>

                    <asp:RequiredFieldValidator id="RequiredFieldValidatorNumeroPlazo" runat="server" ControlToValidate="NumeroPlazo"  ValidationGroup="grupo1"
                        ErrorMessage="Nº de Mes/Año" Display="Static">*</asp:RequiredFieldValidator>

                    </td>
                </tr>
                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="UpdatePanelPlazoVencimiento" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <asp:Panel ID="PanelPlazoNominalVencimiento" Visible="false" runat="server">
                <tr>
                    <td class="col1"><span class="item">Plazo de Vencimiento</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">

                        <div class="calendario_textbox">               
                            <asp:TextBox ID="PlazoVencimiento" Columns="8" Width="120px" runat="server" CausesValidation="false" ReadOnly="true" CssClass="campoDeshabilitado"></asp:TextBox>
                        </div>

                        <asp:Panel ID="PanelFechaVencimiento" Visible="true" runat="server">
                        <div class="calendario_icono">
                            <img src="../../App_Themes/admin_style/images/calendar.png" id="imgPlazoVencimiento" alt="Calendario" style="vertical-align: middle" />
                        </div>
                        </asp:Panel>
                        

                        <asp:RequiredFieldValidator id="RequiredFieldValidatorPlazoVencimiento" runat="server" ControlToValidate="PlazoVencimiento"  ValidationGroup="grupo1"
                        ErrorMessage="Plazo de Vencimiento" Display="Static" ForeColor="Red">*</asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator1" 
                        runat="server"
                        ControlToValidate="PlazoVencimiento"
                        ForeColor="Red"
                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d\s(([0-1][0-9])|([0-2][0-3])):[0-5][0-9]$" 
                        ErrorMessage="Ingrese formato válido"
                        ValidationGroup="grupo1">
                        </asp:RegularExpressionValidator>
                        
                        

                    </td>
                </tr>
                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>

                <asp:Panel id="PanelBotonGuardar" runat="server" Visible="true">
                <tr>
                    <td class="col1"></td>
                    <td class="col2"></td>
                    <td class="col3">
                            <asp:ImageButton ID="GuardarUnidadEspacial" runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                            AlternateText="Crear Concesión" ToolTip="Crear Concesión" onclick="GuardarUnidadEspacial_Click" 
                            CausesValidation="true" ValidationGroup="grupo1" />
                        <span class="item">Guardar Unidad Espacial</span>
                    </td>
                </tr>
                </asp:Panel>
                </table>


                <br />
                <br />

                <asp:UpdatePanel ID="UpdatePanelBotonCreacion" UpdateMode="Conditional" runat="server">
                <ContentTemplate>

                    <asp:Panel id="PanelBotonCreacion" runat="server" Visible="false">
                    <table class="form" cellpadding="0px" cellspacing="0px" width="100%">   
                    <tr>
                        <td class="col1" colspan="4" align="center">
                            
                                <asp:ImageButton ID="CrearConcesionButton" runat="server" 
                                    ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                    AlternateText="Crear Colector" ToolTip="Crear Concesión" onclick="CrearConcesion_Click" 
                                    CausesValidation="true" ValidationGroup="grupo1" style="width: 20px" />
                            <span class="item">Crear Colector</span>&nbsp;&nbsp;<asp:ImageButton 
                                ID="LimpiarConcesion" runat="server" AlternateText="Limpiar Concesion" Height="20px" 
                                ImageUrl="~/App_Themes/admin_style/images/clean.png" 
                                onclick="LimpiarConcesion_Click" ToolTip="Limpiar Concesion" />
                                </td>
                    </tr>
                    </table>
                    </asp:Panel>

                </ContentTemplate>
                </asp:UpdatePanel>

            

                <asp:Panel id="PanelMensajePendientes" runat="server" Visible="false">
                    <table class="form" cellpadding="0px" cellspacing="0px" width="100%">   
                    <tr>
                        <td class="col1" colspan="4" align="center"><span style="color:#772222;">Advertencia: Aun existen documentos pendientes que no permiten la Creación del Colector de Semilla, sin embargo puede guardar la información contenida en esta página (El Colector no se creará)</span></td>
                    </tr>
                    <tr>
                        <td class="col1" colspan="4" align="center"><span style="color:#0080FF;"><b><asp:Label ID="mensajeRequerimientosAbiertos" runat="server"></asp:Label></b></span></td>
                    </tr>
                    </table>
                </asp:Panel>

        
    </fieldset>
    
    <script type="text/javascript">
        invoca_calendarios("unidadEspacialColector");
    </script>
</asp:Content>
