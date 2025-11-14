<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="ingresarSolicitudModificacionAmerb.aspx.cs" 
Inherits="SubPesca.Solicitudes.ModificacionAmerb.ingresarSolicitudModificacionAmerb" Theme="admin_style" %>

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
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>

    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Nueva Solicitud de Modificación de Amerb</span>
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
            <tr>
                <td class="col1" rowspan="5"><span class="item">Tipo de Modificación</span></td><td class="col2" rowspan="5"><span class="item">:</span></td>
                <td class="col3" align="left"><asp:CheckBox ID="Especie" runat="server" Text="Especie" oncheckedchanged="TiposModificacion_CheckedChanged" AutoPostBack="true"/></td>
            </tr>
            <tr>
                <td class="col3">
                    <asp:CheckBox ID="ProyectoTecnico" runat="server" Text="Proyecto Técnico" oncheckedchanged="TiposModificacion_CheckedChanged" AutoPostBack="true"/>
                </td>
            </tr>
            <tr>
                <td class="col3">
                    <asp:CheckBox ID="ampliacionSuperficie" runat="server" Text="Ampliación Superficie" oncheckedchanged="TiposModificacion_CheckedChanged" AutoPostBack="true"/>
                </td>
            </tr>
            <tr>
                <td class="col3">
                    <asp:CheckBox ID="reduccionSuperficie" runat="server" Text="Reducción Superficie" oncheckedchanged="TiposModificacion_CheckedChanged" AutoPostBack="true"/>
                </td>
            </tr>
            <tr>
                <td class="col3">
                    <asp:CheckBox ID="regularizacion" runat="server" Text="Regularización" oncheckedchanged="TiposModificacion_CheckedChanged" AutoPostBack="true" />
                </td>
            </tr>
            
            <asp:Panel ID="panelCampos1" Visible="false" runat="server">
            <asp:UpdatePanel ID="UpdatePanelCampos1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            
            
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
                                ErrorMessage="Fecha de Recepción" Display="Static">*</asp:RequiredFieldValidator>

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
                <td class="col1"><span class="item">Fecha de Ingreso a Trámite</span></td><td class="col2"><span class="item">:</span></td>
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
                                ErrorMessage="Fecha de Ingreso a Trámite" Display="Static">*</asp:RequiredFieldValidator>

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

            </ContentTemplate>
            </asp:UpdatePanel>
            </asp:Panel>
            

            <asp:Panel ID="panelCampos2" Visible="false" runat="server">
            <asp:UpdatePanel ID="UpdatePanelCampos2" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            
            <tr>
                <td class="col1"><span class="item">Código de Centro</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                   
                        <asp:TextBox ID="CodigoCentro" AutoPostBack="true" runat="server" OnTextChanged="CodigoCentro_OnTextChanged"  CausesValidation="True"></asp:TextBox> *
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorNombreCentro" runat="server" ControlToValidate="CodigoCentro"  ValidationGroup="InicioModificacion" ErrorMessage="Ingrese Código de Centro" ForeColor="Red" Display="Static" InitialValue=""></asp:RequiredFieldValidator>

                        <asp:CompareValidator ID="CompareValidatorCodigoCentro" ControlToValidate="CodigoCentro" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="El Código de Centro debe ser Numérico." Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>        
                </td>
            </tr>

            <tr>
                <td class="col1"><span class="item">Rut Titular del Centro</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                
                        <asp:TextBox ID="TitularCentro" MaxLength="10" Width="100px" runat="server" OnTextChanged="TitularCentro_OnTextChanged" AutoPostBack="true" ></asp:TextBox> * (Ej: 12345678-9)

                        <asp:RequiredFieldValidator id="RequiredFieldValidatorTitularCentro" runat="server" ControlToValidate="TitularCentro"  ValidationGroup="InicioModificacion" ForeColor="Red"
                         ErrorMessage="Ingrese Rut Titular del Centro" Display="Static"></asp:RequiredFieldValidator>

                        <asp:CustomValidator ID="ccNumCustVal" ControlToValidate="TitularCentro" ErrorMessage="Rut Titular del Centro sin formato válido" ForeColor="Red" ClientValidationFunction="validaRUT" Display="Static" Font-Size="10" runat="server" ValidationGroup="InicioModificacion"></asp:CustomValidator>

                </td>
           </tr>

            <tr>
                <td class="col1"><span class="item">Número CI</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="NumeroCI" autocomplete="tel-extension" MaxLength="10" 
                            Width="100px"  runat="server" ontextchanged="NumeroCI_TextChanged"
                            AutoPostBack="true"
                            onKeyUp="return onlyNumeric(this)" ></asp:TextBox> *
                            
                            <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="NumeroCI"  ValidationGroup="InicioModificacion" ForeColor="Red"
                            ErrorMessage="Ingrese Número CI"  Display="Static"></asp:RequiredFieldValidator>

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
                            ontextchanged="FechaCI_TextChanged" AutoPostBack="true"></asp:TextBox> *
                        </div>
                        <div class="calendario_icono">
                            <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaCI" alt="Calendario" style="vertical-align: middle" />
                        </div>

                        <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="FechaCI"  ValidationGroup="InicioModificacion"
                            ErrorMessage="Ingrese Fecha CI" Display="Static"  ForeColor="Red"></asp:RequiredFieldValidator>

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


                  
            </ContentTemplate>                     
            </asp:UpdatePanel>
            </asp:Panel>


            <asp:Panel ID="panelCampos4" Visible="false" runat="server">
            <asp:UpdatePanel ID="UpdatepanelCampos4" UpdateMode="Conditional" runat="server">
            <ContentTemplate>

                <tr>
                    <td class="col1"><span class="item">Lugar de Ingreso</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                        <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="Oficina"  Width="200px"  runat="server"  AutoPostBack="true"
                                    OnSelectedIndexChanged="Oficina_SelectedIndexChanged"></asp:DropDownList> *
                                <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ControlToValidate="Oficina"  ValidationGroup="InicioModificacion" ErrorMessage="Ingrese Lugar de Ingreso" ForeColor="Red" Display="Static" InitialValue="0"></asp:RequiredFieldValidator>
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
                            <asp:TextBox  ID="NumPert"  Width="200px"  ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>

            </ContentTemplate>                     
            </asp:UpdatePanel>
            </asp:Panel>


     

            
            <asp:Panel ID="panelCampos3" Visible="false" runat="server">     
            <asp:UpdatePanel ID="UpdatePanelCampos3" UpdateMode="Conditional" runat="server"> 
            <ContentTemplate>
                <tr>
                    <td class="col1"><span class="item">&nbsp;</span></td><td class="col2"><span class="item">&nbsp;</span></td>
                    <td class="col3"><span class="item"><asp:Button ID="Guardar" runat="server" Text="Generar"  CausesValidation="true" OnClientClick="return validatePre('InicioModificacion');"  ValidationGroup="InicioModificacion" onclick="Guardar_Click" /></span></td>
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


            <!-- JAVASCRIPT !-->
            <script type="text/javascript">
                invoca_calendarios("ingresarSolicitudModificacion");
            </script></asp:Content>
