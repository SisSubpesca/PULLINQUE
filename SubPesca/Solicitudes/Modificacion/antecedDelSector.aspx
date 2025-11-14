<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesModificacion.Master" Theme="admin_style" AutoEventWireup="true" CodeBehind="antecedDelSector.aspx.cs" Inherits="SubPesca.Solicitudes.Modificacion.antecedDelSector" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="~/Solicitudes/Registrar/capitaniaPuerto.ascx"   tagname="capitaniaPuerto"                   tagprefix="uc1" %>
<%@ Register src="~/Solicitudes/Registrar/informacionSolicitud.ascx"   tagname="informacionSolicitud"              tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>

    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
</asp:Content>





<asp:Content ID="FormularioAntecedentesSector" ContentPlaceHolderID="rightbody" runat="server">
    

    <asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>

    <asp:ToolkitScriptManager ID="ToolkitScriptManagerAntecedentesSector" runat="server" EnablePartialRendering="true" EnablePageMethods="true"></asp:ToolkitScriptManager>

        <asp:UpdatePanel ID="UpdatePanelInformacionSolicitud" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
                    <asp:Panel ID="PanelInformacionSolicitud"  Visible="true" runat="server">
                        <uc2:informacionSolicitud ID="informacionSolicitud" runat="server" />
                    </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- Título de la página -->
        <table class="formtop" cellpadding="0px" cellspacing="0px">
            <tr>
                <td align="left" valign="middle">
                    <span id="titulo_modulo">Antecedentes del Sector</span>
                </td>
            </tr>
        </table>

        <hr style="width:100%;" />

        <asp:ValidationSummary ID="ValidationSummaryUbicacionGeografica" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />

        <asp:ValidationSummary ID="ValidationSummaryBarrio" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo2" />

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

        <!-- Sección Ubicación Geográfica -->
        <asp:UpdatePanel ID="UpdatePanelUbicacionGeografica" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
        <asp:Panel ID="PanelUbicacionGeografica" runat="server" Visible="false"> 

        <fieldset>
        <legend>Ubicación Geográfica</legend>
        <br />

                <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                           <td class="col1"><span class="item">Región</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3" colspan="2">
                            <asp:UpdatePanel ID="UpdatePanelRegion" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="Region" AutoPostBack="true" OnSelectedIndexChanged="Region_OnSelectedIndexChanged" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorRegion" runat="server" ControlToValidate="Region"  ValidationGroup="grupo1"
                                 ErrorMessage="Región" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                            </ContentTemplate>
                            </asp:UpdatePanel> 
                            </td>
                        </tr>
                        <tr>
                           <td class="col1"><span class="item">Provincia</span></td>   
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3" colspan="2">
                           <asp:UpdatePanel ID="UpdatePanelProvincia" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                               <asp:DropDownList ID="Provincia" AutoPostBack="true" OnSelectedIndexChanged="Provincias_OnSelectedIndexChanged" runat="server"></asp:DropDownList> 
                               <asp:RequiredFieldValidator id="RequiredFieldValidatorProvincia" runat="server" ControlToValidate="Provincia"  ValidationGroup="grupo1"
                                ErrorMessage="Provincia" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                           </ContentTemplate>
                           <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="Region" EventName="SelectedIndexChanged" />
                           </Triggers>
                           </asp:UpdatePanel>    
                           </td>
                        </tr>
                        <tr>
                           <td class="col1"><span class="item">Comuna</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3" colspan ="2">
                           <asp:UpdatePanel ID="UpdatePanelComunas" UpdateMode="Conditional" runat="server">
                           <ContentTemplate>
                               <asp:ListBox ID="Comuna" runat="server" Width="256px" SelectionMode="Multiple"></asp:ListBox>

                               <asp:RequiredFieldValidator id="RequiredFieldValidatorComuna" runat="server" ControlToValidate="Comuna"  ValidationGroup="grupo1" ErrorMessage="Comuna" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                           
                           </ContentTemplate>
                           <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="Provincia" EventName="SelectedIndexChanged" />
                           </Triggers>
                           </asp:UpdatePanel> 
                           </td>
                        </tr>
                        <tr>
                           <td class="col1">&nbsp;</td>
                           <td class="col2">&nbsp;</td>
                           <td class="col3">
                                <asp:ImageButton ID="GuardarUbicacionGeografica" runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                    AlternateText="Guardar Ubicación Geográfica" ToolTip="Guardar Ubicación Geográfica" onclick="GuardarUbicacionGeografica_Click" CausesValidation="true" ValidationGroup="grupo1" />
                                <span class="item">Guardar Ubicación Geográfica</span>
                           </td>
                           <td class="col3">&nbsp;</td>
                        </tr>
                </table>
        </fieldset>
        
        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>

        <!-- Sección Barrio -->
        <asp:UpdatePanel ID="UpdatePanelDatosBarrio" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
        <asp:Panel ID="PanelSeccionBarrio" runat="server" Visible="false"> 
        <fieldset>
        <legend>Agrupación de Concesiones</legend>
        <br />

        <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                   <td class="col1"><span class="item">Tipo Barrio</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                        <asp:UpdatePanel ID="UpdatePanelTipoBarrio" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                           <asp:DropDownList ID="TipoBarrio" AutoPostBack="true" runat="server" OnSelectedIndexChanged="TipoBarrio_OnSelectedIndexChanged"></asp:DropDownList> 
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoBarrio" runat="server" ControlToValidate="TipoBarrio"  ValidationGroup="grupo2"
                             ErrorMessage="Tipo Barrio" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">Macrozona</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                   <asp:UpdatePanel ID="UpdatePanelMacrozona" UpdateMode="Conditional" runat="server">
                   <ContentTemplate>
                        <asp:DropDownList ID="Macrozona" AutoPostBack="false" runat="server" OnSelectedIndexChanged="Macrozona_OnSelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorMacrozona" runat="server" ControlToValidate="Macrozona"  ValidationGroup="grupo2"
                        ErrorMessage="Macrozona" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                   </ContentTemplate>
                   </asp:UpdatePanel>
                   </td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">Barrio</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                   <asp:UpdatePanel ID="UpdatePanelBarrio" UpdateMode="Conditional" runat="server">
                   <ContentTemplate>
                       <asp:DropDownList ID="Barrio" AutoPostBack="true" runat="server"></asp:DropDownList>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorBarrio" runat="server" ControlToValidate="Barrio"  ValidationGroup="grupo2"
                        ErrorMessage="Barrio" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator> 
                   </ContentTemplate>
                   <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="TipoBarrio" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Macrozona" EventName="SelectedIndexChanged" />
                   </Triggers>
                   </asp:UpdatePanel>
                   </td>
                </tr>
                
                <tr>
                   <td class="col1">&nbsp;</td>
                   <td class="col2">&nbsp;</td>
                   <td class="col3" style="margin-left: 40px">
                        <asp:ImageButton ID="GuardarDatosBarrio" runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                            AlternateText="Guardar Datos Barrio" ToolTip="Guardar Datos Barrio" onclick="GuardarDatosBarrio_Click" CausesValidation="true" ValidationGroup="grupo2"/>
                        <span class="item">Guardar Agrupación de Concesiones</span>
                    </td>
                </tr>
           </table>

        </fieldset>
        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>
        
        <!-- Otra definici+on geográfica -->
        <asp:UpdatePanel ID="UpdatePanelAdministracionPlanos" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        <asp:Panel ID="PanelAdministracionPlanos" runat="server" Visible="false"> 
        <fieldset>
        <legend> Otra Definición Geográfica </legend>
        <br />

        <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                   <td class="col1"><span class="item">Necesita</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3" style="margin-left: 40px">
                       <span class="item">
                           <asp:CheckBoxList ID="CheckBoxListNecesita" runat="server">
                               <asp:ListItem>Coordenadas de Entrega de Material</asp:ListItem>
                               <asp:ListItem>Regularización</asp:ListItem>
                           </asp:CheckBoxList>
                       </span>
                    </td>
                </tr>
                <tr>
                   <td class="col1">&nbsp;</td>
                   <td class="col2">&nbsp;</td>
                   <td class="col3" style="margin-left: 40px">
                        <asp:ImageButton ID="GuardarDatosPlanos" runat="server" 
                            ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                            AlternateText="Guardar Datos Planos" ToolTip="Guardar Datos Planos" 
                            CausesValidation="true" onclick="GuardarDatosPlanos_Click"/>
                        <span class="item">Guardar Otra Definición Geográfica</span></td>
                </tr>
           </table>

        </fieldset>

        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>

         <!-- Sección Flujo Documental: Capitanía de Puerto -->
        <asp:UpdatePanel ID="UpdatePanelCapitaniaPuerto" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
                <asp:Panel ID="PanelCapitaniaPuerto"  Visible="false" runat="server">
                    <uc1:capitaniaPuerto ID="capitaniaPuerto" runat="server" />
                </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>

        <!-- Secciones Antecedentes Espaciales, Antecedentes Terreno y Regulación -->
        <fieldset>
        <table cellpadding="0px" cellspacing="0px">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanelAntecedentesEspeciales" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 

                    <asp:Panel ID="PanelAntecedentesEspecialesVisibilidad"  Visible="false" runat="server">
                        <asp:LinkButton ID="lnk_AntecedentesEspaciales"  CssClass="tab1_selected"  runat="server" onclick="cambiaPestania_Click" CausesValidation="false">Coordenadas Originales</asp:LinkButton>
                    </asp:Panel>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnk_AntecedentesTerreno" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnk_Regularizacion" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
             </td>
             <td>
                <asp:UpdatePanel ID="UpdatePanelAntecedentesTerreno" UpdateMode="Conditional" runat="server" Visible="true">
                    <ContentTemplate>
                    <asp:Panel ID="PanelAntecedentesTerrenoVisibilidad" runat="server" Visible="false"> 
                        <asp:LinkButton ID="lnk_AntecedentesTerreno"  CssClass="tab2"  runat="server" onclick="cambiaPestania_Click" CausesValidation="false">Coordenadas de Entrega de Material</asp:LinkButton>
                    </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnk_AntecedentesEspaciales" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="lnk_Regularizacion" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="GuardarDatosBarrio" EventName="Click" />
                    </Triggers>
                    </asp:UpdatePanel>
            </td>
             <td>
                <asp:UpdatePanel ID="UpdatePanelRegularizacion" UpdateMode="Conditional" runat="server" Visible="true">
                    <ContentTemplate> 
                    <asp:Panel ID="PanelRegularizacionVisibilidad" runat="server" Visible="false">
                        <asp:LinkButton ID="lnk_Regularizacion"  CssClass="tab3"  runat="server" onclick="cambiaPestania_Click" CausesValidation="false">Regularización</asp:LinkButton>
                    </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnk_AntecedentesEspaciales" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="lnk_AntecedentesTerreno" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="GuardarDatosBarrio" EventName="Click" />
                    </Triggers>
                    </asp:UpdatePanel>
             </td>          
        </tr>
        </table>
    
    <asp:UpdatePanel ID="updPestanaAntecedentesEspaciales" UpdateMode="Conditional" runat="server">

            <ContentTemplate>                        
                    
                    <!-- Pestaña 1: Antecedentes Espaciales-->
                    <asp:Panel ID="PanelAntecedentesEspeciales"  Visible="false" runat="server">
                    <asp:UpdatePanel ID="UpdateAntecedentesEspeciales" UpdateMode="Conditional" runat="server">
                    <ContentTemplate> 
                    
                    <br />
                    
                    <asp:ValidationSummary ID="validacionesAntecedentesEspaciales" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo3" />
                    
                    <asp:ValidationSummary ID="ValidationSummaryArchivoAdjuntoAntEspaciales" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo20" />

                    <asp:ValidationSummary ID="ValidationSummaryPoligonoAntEspaciales" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo5" />
        
                    <asp:ValidationSummary ID="ValidationSummaryVerticeAntEspaciales" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo4" />

                    <asp:Panel ID="Content_msgGrillaGral_2" CssClass="Content_msgGrilla" Visible="false" runat="server">
                        <div class="msgGrilla_div1">
                            <asp:Image ID="IcoGral_2" CssClass="Ico_msgGrilla" runat="server" />
                        </div>
                        <div class="msgGrilla_div2">
                            <asp:Label ID="msgGrillaGral_2" runat="server"></asp:Label>
                        </div>
                    </asp:Panel>


                    <br />
                       
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>                    
                    
                    <fieldset>
                    <legend>Referencias Geográficas</legend>
                    <br />
                       
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <asp:HiddenField ID="IdCoordenadaGeoAntEspaciales" runat="server"></asp:HiddenField>
                   
                    <asp:HiddenField ID="IdCartaAntecedentesEspaciales" runat="server"></asp:HiddenField>
                    <tr>
                        <td class="col1"><span class="item">Carta</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            <link href="AutoCompleteStyle.css" rel="stylesheet" type="text/css" />
                            <asp:TextBox ID="NombreCartaAntecedentesEspaciales" AutoPostBack="true" runat="server" ontextchanged="NombreCartaAntecedentesEspaciales_TextChanged" Width="200px"></asp:TextBox>
                            <cc1:AutoCompleteExtender 
                                runat="server" 
                                ID="autoComplete1" 
                                TargetControlID="NombreCartaAntecedentesEspaciales"
                                ServicePath="antDelSector.asmx"
                                ServiceMethod="BuscarCartaAntSector"
                                MinimumPrefixLength="2" 
                                CompletionInterval="1000"
                                EnableCaching="true"
                                CompletionListCssClass="completionList"
                                CompletionListHighlightedItemCssClass="itemHighlighted"
                                CompletionListItemCssClass="listItem"
                                >
                            </cc1:AutoCompleteExtender>
                            
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorNombreCartaAntecedentesEspaciales" runat="server" ControlToValidate="NombreCartaAntecedentesEspaciales" ValidationGroup="grupo3" 
                            ErrorMessage="Carta" Display="Static" >*</asp:RequiredFieldValidator>

                        </td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">&nbsp;</td>
                    </tr>

                        <tr>
                            <td class="col1"><span class="item">Nº Carta</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3">
                                <asp:TextBox ID="NumeroCartaAntEspaciales" runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="col3"><span class="item">Edición</span></td>
                            <td class="col3"><span class="item">:</span></td>
                            <td class="col3">
                                <asp:TextBox ID="EdicionCartaAntEspaciales" runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="col3"><span class="item">Año</span></td>
                            <td class="col3"><span class="item">:</span></td>
                            <td class="col3">
                                <asp:TextBox ID="AnioCartaAntEspaciales" runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1"><span class="item">Datum</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3">
                                <asp:TextBox ID="DatumCartaAntEspaciales" runat="server" ReadOnly="true"></asp:TextBox>
                                <asp:HiddenField ID="IdDatumCartaAntEspaciales" runat="server"></asp:HiddenField>
                            </td>
                            <td class="col3"><span class="item">Huso</span></td>
                            <td class="col3"><span class="item">:</span></td>
                            <td class="col3">
                                <asp:TextBox ID="HusoCartaAntEspaciales" runat="server" ReadOnly="true"></asp:TextBox>
                                <asp:HiddenField ID="IdHusoCartaAntEspaciales" runat="server"></asp:HiddenField>
                            </td>
                            <td class="col3">&nbsp;</td>
                            <td class="col3">&nbsp;</td>
                            <td class="col3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col1" nowrap><span class="item">Coordenada Geográfica</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3">
                                <span class="item"><asp:CheckBox ID="SeUsaParaBancoAntEspaciales" 
                                    runat="server" Text="Se Usa para Banco" /></span>  
                            </td>
                            <td class="col3">&nbsp;</td>
                            <td class="col3">&nbsp;</td>
                            <td class="col3">&nbsp;</td>
                            <td class="col3">&nbsp;</td>
                            <td class="col3">&nbsp;</td>
                            <td class="col3">&nbsp;</td>
                        </tr>

                        <tr>
                            <td class="col1">
                                <span class="item">Superficie Total Solicitada</span></td>
                            <td class="col2">
                                <span class="item">:</span></td>
                            <td class="col3">
                                

                                <asp:UpdatePanel ID="UpdatePanelAreaTotalSolicitadaAntecedentesEspeciales" 
                                    runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="AreaTotalSolicitadaAntecedentesEspeciales" runat="server" 
                                            Columns="8" ReadOnly="True" Width="80px"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GuardarReferenciasGeograficasAntecedentesEspeciales" 
                                            EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td class="col3">
                                <span class="item">Superficie Total Calculada</span></td>
                            <td class="col3">
                                :</td>
                            <td class="col3">
                                <asp:UpdatePanel ID="UpdatePanelAreaTotalCalculadaAntecedentesEspeciales" 
                                    runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="AreaTotalCalculadaAntecedentesEspeciales" runat="server" 
                                            Columns="8" ReadOnly="True" Width="80px"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GuardarReferenciasGeograficasAntecedentesEspeciales" 
                                            EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td class="col3">
                                &nbsp;</td>
                            <td class="col3">
                                &nbsp;</td>
                            <td class="col3">
                                &nbsp;</td>
                        </tr>

                    <tr>
                        <td class="col1"></td>
                        <td class="col2"></td>
                        <td class="col3">
                            <asp:ImageButton ID="GuardarReferenciasGeograficasAntecedentesEspeciales" 
                                runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                AlternateText="Guardar Referencias Geográfica" 
                                ToolTip="Guardar Referencias Geográfica" 
                                onclick="GuardarReferenciasGeograficasAntecedentesEspeciales_Click" 
                                CausesValidation="true" ValidationGroup="grupo3" style="width: 20px"/>
                            <span class="item">Guardar Referencias Geográficas</span>
                        </td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">
                            &nbsp;</td>
                        <td class="col3">
                            &nbsp;</td>
                        <td class="col3">
                            &nbsp;</td>
                        <td class="col3">
                            &nbsp;</td>
                    </tr>
                    </table>
                    </fieldset>

                    </ContentTemplate>
                    </asp:UpdatePanel>

                    <fieldset>
                    <legend>Archivos Adjuntos</legend>
                    <br />
                         
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    
                   <tr>
                   <td class="col1"><span class="item">Tipo Archivo</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                   
                   <asp:DropDownList ID="TipoArchivoAntEspaciales" AutoPostBack="false" runat="server"></asp:DropDownList>
                   <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoArchivoAntEspaciales" runat="server" ControlToValidate="TipoArchivoAntEspaciales" ValidationGroup="grupo20" 
                   ErrorMessage="Tipo Archivo" InitialValue="-1" Display="Static" >*</asp:RequiredFieldValidator>

                   </td>
                    </tr>
                    <tr>
                       <td class="col1"><span class="item">Nombre Archivo</span></td>
                       <td class="col2"><span class="item">:</span></td>
                       <td class="col3">

                       <asp:TextBox ID="NombreArchivoAntEspaciales" MaxLength="40" Width="200px" runat="server"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorNombreArchivoAntEspaciales" runat="server" ControlToValidate="NombreArchivoAntEspaciales" ValidationGroup="grupo20" 
                       ErrorMessage="Nombre Archivo" Display="Static" >*</asp:RequiredFieldValidator>

                       </td>
                    </tr>
                    <tr>
                       <td class="col1"><span class="item">Archivo Adjunto</span></td>
                       <td class="col2"><span class="item">:</span></td>
                       <td class="col3">
                   
                       <asp:UpdatePanel ID="UpdatePanelArchivoAdjuntoAntEspaciales" UpdateMode="Conditional" runat="server">
                       <ContentTemplate>

                       <asp:FileUpload ID="ArchivoAdjuntoAntEspaciales" runat="server" />

                       <asp:RequiredFieldValidator id="RequiredFieldValidatorArchivoAdjuntoAntEspaciales" runat="server" ControlToValidate="ArchivoAdjuntoAntEspaciales" ValidationGroup="grupo20" 
                       ErrorMessage="Archivo Adjunto" Display="Static" >*</asp:RequiredFieldValidator>

                       <asp:RegularExpressionValidator ID="REGEXFileUploadLogo" runat="server" ErrorMessage="Solo .doc y .pdf" ControlToValidate="ArchivoAdjuntoAntEspaciales" ValidationExpression= "(.*).(.doc|.DOC|.pdf|.PDF)$" ValidationGroup="grupo20" />

                       </ContentTemplate>
                       <Triggers>

                            <asp:PostBackTrigger ControlID="GuardarArchivoAdjuntoAntEspaciales" />

                       </Triggers>
                       </asp:UpdatePanel>
                       </td>
                    </tr>

                    <tr>
                        <td class="col1"></td>
                        <td class="col2"></td>
                        <td class="col3">
                            <asp:ImageButton ID="GuardarArchivoAdjuntoAntEspaciales" runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                AlternateText="Guardar Archivo Adjunto" ToolTip="Guardar Archivo Adjunto" onclick="GuardarArchivoAdjuntoAntEspaciales_Click" CausesValidation="true" ValidationGroup="grupo20"/>
                            <span class="item">Guardar Archivo Adjunto</span>
                        </td>
                    </tr>
                    </table>

                    <asp:Panel ID="PanelArchivoAdjAntEspacial"  Visible="true" runat="server">

                    <asp:GridView 
                        ID="GridArchivoAdjuntoAntEspacial" 
                        runat="server"
                        AutoGenerateColumns="False"
                        DataKeyNames="idArchivoBinario" 
                        CellPadding="4" 
                        ForeColor="#333333"
                        GridLines="None"
                        AllowPaging="False"
                        AllowSorting="false" 
                        CssClass="mGrid"
                        PagerStyle-CssClass="pgr" 
                        width="100%"
                        PageIndex="1"
                        OnRowDataBound="GridArchivoAdjuntoAntEspacial_RowDataBound"
                        OnRowCommand="GridArchivoAdjuntoAntEspacial_RowCommand"
                        OnRowCreated="GridArchivoAdjuntoAntEspacial_RowCreated">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                           <asp:TemplateField HeaderText="Tipo Archivo">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.tipoDocumento.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="Nombre Archivo">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.archivoBinario.nombreArchivo")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Archivo Adjunto">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.archivoBinario.nombreFisico")%>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gDesasociar" Visible="false" runat="server" CausesValidation="false" CommandName="Desasociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBinario") %>'
                                    ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Desasociar" ToolTip="Desasociar" />
                    
                                    <asp:ImageButton ID="gAsociar" Visible="false" runat="server" CausesValidation="false" CommandName="Asociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBinario") %>'
                                     ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Asociar" ToolTip="Asociar" />

                                    <asp:ImageButton ID="gDescargar" Visible="false" runat="server" CausesValidation="false" CommandName="Descargar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBinario") %>' 
                                         ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" OnPreRender="ImgAdd_PreRender" />

                                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBinario") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>

                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#5794EF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    </asp:Panel>

                    </fieldset>

                    <asp:Panel ID="Panel1"  Visible="true" runat="server">
                    
        
        <fieldset>
        <legend>Polígono</legend>
        <br />

        <asp:HiddenField ID="IdPoligonoAntEspaciales" runat="server" value="0" ></asp:HiddenField>

        <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                   <td class="col1"><span class="item">Tipo Concesión</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3" colspan="3">
                       <asp:UpdatePanel ID="UpdatePanelTipoConcesionAntecedentesEspaciales" UpdateMode="Conditional" runat="server">
                           <ContentTemplate>
                               <asp:ListBox ID="TipoConcesionAntecedentesEspaciales" runat="server" Width="256px" SelectionMode="Multiple"></asp:ListBox>

                               <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoConcesionAntecedentesEspaciales" runat="server" ControlToValidate="TipoConcesionAntecedentesEspaciales"  ValidationGroup="grupo5" ErrorMessage="Tipo Concesión" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                           </ContentTemplate>
                       </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">Tipo Uso</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:DropDownList ID="TipoUsoAntecedentesEspeciales" AutoPostBack="false" runat="server"></asp:DropDownList> 
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoUsoAntecedentesEspeciales" runat="server" ControlToValidate="TipoUsoAntecedentesEspeciales"  ValidationGroup="grupo5"
                        ErrorMessage="Tipo Uso" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                   </td>
                   <td class="col3">&nbsp;</td>
                   <td class="col3">&nbsp;</td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">Ubicación Geográfica</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:TextBox ID="ToponimioAntecedentesEspeciales" runat="server" Columns="8" Width="200px"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorToponimioAntecedentesEspeciales" runat="server" ControlToValidate="ToponimioAntecedentesEspeciales"  ValidationGroup="grupo5"
                        ErrorMessage="Toponimio" Display="Static">*</asp:RequiredFieldValidator>
                   </td>
                   <td class="col3">&nbsp;</td>
                   <td class="col3">&nbsp;</td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">Superficie Solicitada</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:TextBox ID="AreaSolicitadaAntecedentesEspeciales" runat="server" Columns="8" Width="80px"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorAreaSolicitadaAntecedentesEspeciales" runat="server" ControlToValidate="AreaSolicitadaAntecedentesEspeciales"  ValidationGroup="grupo5"
                        ErrorMessage="Superficie Solicitada" Display="Static">*</asp:RequiredFieldValidator>
                        
                       <asp:RegularExpressionValidator ID="RegularExpressionValidatorAreaSolicitadaAntecedentesEspeciales" ControlToValidate="AreaSolicitadaAntecedentesEspeciales" ForeColor="Red"  ValidationGroup="grupo5" runat="server" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,9})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>


                   </td>
                   <td class="col3" style="margin-left: 40px"><span class="item">Superficie Calculada</span></td>
                   <td class="col3" style="margin-left: 40px">
                       
                        <asp:TextBox ID="AreaCalculadaAntecedentesEspeciales" runat="server" Columns="8" Width="80px" ></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorAreaCalculadaAntecedentesEspeciales" runat="server" ControlToValidate="AreaCalculadaAntecedentesEspeciales"  ValidationGroup="grupo5"
                        ErrorMessage="Superficie Calculada" Display="Static">*</asp:RequiredFieldValidator>

                       <asp:RegularExpressionValidator ID="RegularExpressionValidatorAreaCalculadaAntecedentesEspeciales" ControlToValidate="AreaCalculadaAntecedentesEspeciales" ForeColor="Red"  ValidationGroup="grupo5" runat="server" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,9})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>

                   </td>
                </tr>
           </table>
        
        <asp:Panel ID="Panel2"  Visible="true" runat="server">
              
        <br />

        <fieldset>
        <legend>Vértice</legend>
        <br />
        
        <asp:HiddenField ID="IdVerticeAntEspaciales" runat="server" value="0" ></asp:HiddenField>

        <table class="form" cellpadding="0px" cellspacing="0px">

                <tr>
                   <td class="col1"><span class="item">Vértice</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:DropDownList ID="VerticeAntecedentesEspeciales" AutoPostBack="false" runat="server"></asp:DropDownList> 
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorVerticeAntecedentesEspeciales" runat="server" ControlToValidate="VerticeAntecedentesEspeciales"  ValidationGroup="grupo4"
                       ErrorMessage="Vértice" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                       </td>
                    <td class="col3">&nbsp;</td>
                    <td class="col3">&nbsp;</td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">Latitud</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       
                       <asp:TextBox ID="LatitudHoraAntecedentesEspeciales" runat="server" AutoPostBack="true" MaxLength="2" Width="40px" ontextchanged="LatitudHoraAntecedentesEspeciales_TextChanged" CausesValidation="true"></asp:TextBox>
                       
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLatitudHoraAntecedentesEspeciales" runat="server" ControlToValidate="LatitudHoraAntecedentesEspeciales"  ValidationGroup="grupo4"
                        ErrorMessage="Latitud (Hora)" Display="Static">*</asp:RequiredFieldValidator>
                       
                       <asp:RangeValidator ID="RangeValidatorLatitudHoraAntecedentesEspeciales" ControlToValidate="LatitudHoraAntecedentesEspeciales" runat="server" ForeColor="Red"  Display="Dynamic" ErrorMessage="Latitud Hora debe ser entero y estar entre el rango 0 a 23." MinimumValue="0" MaximumValue="23" Type="Integer" ValidationGroup="AllValidator"></asp:RangeValidator>

                       <asp:TextBox ID="LatitudMinutoAntecedentesEspeciales" runat="server" AutoPostBack="true" MaxLength="2" Width="40px" ontextchanged="LatitudMinutoAntecedentesEspeciales_TextChanged" CausesValidation="true"></asp:TextBox>
                       
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLatitudMinutoAntecedentesEspeciales" runat="server" ControlToValidate="LatitudMinutoAntecedentesEspeciales"  ValidationGroup="grupo4"
                        ErrorMessage="Latitud (Minuto)" Display="Static">*</asp:RequiredFieldValidator>
                       <asp:RangeValidator ID="RangeValidatorLatitudMinutoAntecedentesEspeciales" ControlToValidate="LatitudMinutoAntecedentesEspeciales" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Latitud Minuto debe ser entero y estar entre el rango 0 a 59." MinimumValue="0" MaximumValue="59" Type="Integer" ValidationGroup="AllValidator"></asp:RangeValidator>


                       <asp:TextBox ID="LatitudSegundoAntecedentesEspeciales" runat="server" AutoPostBack="true" MaxLength="7" Width="70px" ontextchanged="LatitudSegundoAntecedentesEspeciales_TextChanged" CausesValidation="true"></asp:TextBox>
                       
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLatitudSegundoAntecedentesEspeciales" runat="server" ControlToValidate="LatitudSegundoAntecedentesEspeciales"  ValidationGroup="grupo4"
                        ErrorMessage="Latitud (Segundo)" Display="Static">*</asp:RequiredFieldValidator>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidatorLatitudSegundoAntecedentesEspeciales" ControlToValidate="LatitudSegundoAntecedentesEspeciales" ForeColor="Red"  ValidationGroup="grupo4" runat="server" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,4})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>


                        </td>
                        <td class="col3">
                            <span class="item">Latitud Decimal</span></td>
                        <td class="col3">
                    
                        <asp:UpdatePanel ID="UpdatePanelLatitudAntecedentesEspeciales" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>

                            <asp:TextBox ID="LatitudAntecedentesEspeciales" runat="server" Columns="15" Width="100px" ReadOnly="True"></asp:TextBox>
                      
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="LatitudHoraAntecedentesEspeciales" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="LatitudMinutoAntecedentesEspeciales" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="LatitudSegundoAntecedentesEspeciales" EventName="TextChanged" />
                        </Triggers>
                        </asp:UpdatePanel>

                    </td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">Longitud</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       
                       <asp:TextBox ID="LongitudHoraAntecedentesEspeciales" runat="server" AutoPostBack="true" MaxLength="2" Width="40px" ontextchanged="LongitudHoraAntecedentesEspeciales_TextChanged" CausesValidation="true"></asp:TextBox>
                       
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLongitudHoraAntecedentesEspeciales" runat="server" ControlToValidate="LongitudHoraAntecedentesEspeciales"  ValidationGroup="grupo4"
                        ErrorMessage="Longitud (Hora)" Display="Static">*</asp:RequiredFieldValidator>
                       <asp:RangeValidator ID="RangeValidatorLongitudHoraAntecedentesEspeciales" ControlToValidate="LongitudHoraAntecedentesEspeciales" runat="server" ForeColor="Red"  Display="Dynamic" ErrorMessage="Longitud Hora debe ser entero y estar entre el rango 0 a 23." MinimumValue="0" MaximumValue="23" Type="Integer" ValidationGroup="AllValidator"></asp:RangeValidator>

                       <asp:TextBox ID="LongitudMinutoAntecedentesEspeciales" runat="server" AutoPostBack="true" MaxLength="2" Width="40px" ontextchanged="LongitudMinutoAntecedentesEspeciales_TextChanged" CausesValidation="true"></asp:TextBox>
					   <asp:RequiredFieldValidator id="RequiredFieldValidatorLongitudMinutoAntecedentesEspeciales" runat="server" ControlToValidate="LongitudMinutoAntecedentesEspeciales"  ValidationGroup="grupo4"
                        ErrorMessage="Longitud (Minuto)" Display="Static">*</asp:RequiredFieldValidator>
                       <asp:RangeValidator ID="RangeValidatorLongitudMinutoAntecedentesEspeciales" ControlToValidate="LongitudMinutoAntecedentesEspeciales" runat="server" ForeColor="Red"  Display="Dynamic" ErrorMessage="Longitud Minuto debe ser entero y estar entre el rango 0 a 59." MinimumValue="0" MaximumValue="59" Type="Integer" ValidationGroup="AllValidator"></asp:RangeValidator>

                       <asp:TextBox ID="LongitudSegundoAntecedentesEspeciales" runat="server" AutoPostBack="true" Width="70px" ontextchanged="LongitudSegundoAntecedentesEspeciales_TextChanged" CausesValidation="true"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLongitudSegundoAntecedentesEspeciales" runat="server" ControlToValidate="LongitudSegundoAntecedentesEspeciales"  ValidationGroup="grupo4"
                        ErrorMessage="Longitud (Segundo)" Display="Static">*</asp:RequiredFieldValidator>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidatorLongitudSegundoAntecedentesEspeciales" ControlToValidate="LongitudSegundoAntecedentesEspeciales" ForeColor="Red"  runat="server" ValidationGroup="grupo4" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,4})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>

                       </td>
                    <td class="col3">
                        <span class="item">Longitud Decimal</span></td>
                    <td class="col3">

                        <asp:UpdatePanel ID="UpdatePanelLongitudAntecedentesEspeciales" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>

                            <asp:TextBox ID="LongitudAntecedentesEspeciales" runat="server" Columns="15" Width="100px" ReadOnly="True"></asp:TextBox>
                    
                        </ContentTemplate>
                        </asp:UpdatePanel>

                    </td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">UTM E</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:TextBox ID="UTMEAntecedentesEspeciales" runat="server" Columns="8" Width="80px"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorUTMEAntecedentesEspeciales" runat="server" ControlToValidate="UTMEAntecedentesEspeciales"  ValidationGroup="grupo4"
                       ErrorMessage="UTM E" Display="Static">*</asp:RequiredFieldValidator>
                       
                       <asp:RegularExpressionValidator ID="RegularExpressionValidatorUTMEAntecedentesEspeciales" ControlToValidate="UTMEAntecedentesEspeciales" ForeColor="Red"  ValidationGroup="grupo4" runat="server" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,9})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>
                           
                    </td>
                    <td class="col3" style="margin-left: 40px"><span class="item">UTM N</span></td>
                    <td class="col3" style="margin-left: 40px">
                        <asp:TextBox ID="UtmNAntecedentesEspeciales" runat="server" Columns="8" Width="80px"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorUtmNAntecedentesEspeciales" runat="server" ControlToValidate="UtmNAntecedentesEspeciales"  ValidationGroup="grupo4"
                         ErrorMessage="UTM N" Display="Static">*</asp:RequiredFieldValidator>
                         
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorUtmNAntecedentesEspeciales" ControlToValidate="UtmNAntecedentesEspeciales" ForeColor="Red"  ValidationGroup="grupo4" runat="server" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,9})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>
                           
                    </td>
                </tr>

            
             <tr>
                 <td colspan="5" nowrap>
                     <table width="100%" border="0">
                        <tr>
                            <td align="center">
                                   <asp:Panel ID="PanelBotonesVerticeAntEspaciales"  Visible="true" runat="server">
                                       <asp:ImageButton ID="GuardarVerticeAntecedentesEspeciales" runat="server" 
                                            ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                            AlternateText="Guardar Vértice" ToolTip="Guardar Vértice" 
                                            onclick="GuardarVerticeAntecedentesEspeciales_Click" CausesValidation="true" 
                                            ValidationGroup="grupo4" style="width: 20px" />
                                        <span class="item">Guardar Vértice</span>
                                    &nbsp;&nbsp;
                                    <asp:ImageButton ID="LimpiarVertice" runat="server" 
                                    ImageUrl="~/App_Themes/admin_style/images/clean.png" Height="20px" 
                                    AlternateText="Limpiar Vértice" ToolTip="Limpiar Vértice" onclick="LimpiarVertice_Click" 
                                    />
                                    </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>  
            </tr>
          

             <tr>
                <td colspan="5">
                    <!-- Lista de Vértices -->

                    <asp:GridView 
                       ID="GridVerticeAntEspaciales"
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" PageSize="10"
                       CssClass="mGrid"
                       OnRowDataBound="GridVerticeAntEspaciales_RowDataBound"
                       PagerStyle-CssClass="pgr"
                       OnRowCommand="GridVerticeAntEspaciales_RowCommand"
                       Width="100%">
                       <RowStyle BackColor="#EFF3FB" />
                       <Columns>
                           <asp:TemplateField HeaderText="Vértice">
                                <ItemTemplate>
                                <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                <%# DataBinder.Eval(Container, "DataItem.vertice.descripcion") %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="Latitud">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.latitudHora") + "º" + DataBinder.Eval(Container, "DataItem.latitudMinuto") + "'" + DataBinder.Eval(Container, "DataItem.latitudSegundo") + "''"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Latitud Decimal">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.latitudDecimal") %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Longitud">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.longitudHora") + "º" + DataBinder.Eval(Container, "DataItem.longitudMinuto") + "'" + DataBinder.Eval(Container, "DataItem.longitudSegundo") + "''"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Longitud Decimal">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.longitudDecimal") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UTM E">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.utmE")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UTM N">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.utmN")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idVertice") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                                    <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idVertice") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idVertice") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#5794EF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView> 
                </td>
                </tr>
           </table>

    </fieldset>

        </asp:Panel>

    <table class="form" cellpadding="0px" cellspacing="0px">
             
                 <tr>
                    <td colspan="5" align="center">
                        <asp:Panel ID="PanelBotonesPoligonoAntEspaciales"  Visible="true" runat="server">
                            <asp:ImageButton ID="GuardarPoligono" runat="server" 
                                AlternateText="Guardar Polígono" CausesValidation="true" Height="20px" 
                                ImageUrl="~/App_Themes/admin_style/images/add.png" 
                                onclick="GuardarPoligono_Click" style="width: 20px" ToolTip="Guardar Polígono" 
                                ValidationGroup="grupo5" />
                            <span class="item">Guardar Polígono</span>
                        </asp:Panel><asp:ImageButton ID="LimpiarPoligono" runat="server" 
                                AlternateText="Limpiar Poligono" Height="20px" 
                                ImageUrl="~/App_Themes/admin_style/images/clean.png" 
                                onclick="LimpiarPoligono_Click" ToolTip="Limpiar Poligono" />
                        </td>
                       
                 </tr>
            <tr>
                <td>
                    <!-- Lista de Polígonos -->
                    <asp:GridView ID="GridPoligonosAntecedentesEspaciales" runat="server" DataKeyNames="idPoligono" 
                        AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" CssClass="mGrid" 
                        ForeColor="#333333" GridLines="None" 
                        OnRowCommand="GridPoligonosAntecedentesEspaciales_RowCommand" 
                        OnRowCreated="GridPoligonosAntecedentesEspaciales_RowCreated" 
                        OnRowDataBound="GridPoligonosAntecedentesEspaciales_RowDataBound" 
                        PagerStyle-CssClass="pgr" PageSize="10" Width="100%" 
                        >
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="Tipo Concesión">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.tipoConcesionString")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo Uso">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.tipoUso.descripcion") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ubicación Geográfica">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.toponimio") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Superficie Solicitada">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.areaSolicitada")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Superficie Calculada">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.areaCalculada") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="200px">
                                <ItemTemplate>

                                    <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPoligono") %>'
                                    ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                                    <asp:ImageButton ID="gDesasociar" Visible="false" runat="server" CausesValidation="false" CommandName="Desasociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPoligono") %>'
                                    ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Desasociar" ToolTip="Desasociar" />
                    
                                    <asp:ImageButton ID="gAsociar" Visible="false" runat="server" CausesValidation="false" CommandName="Asociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPoligono") %>'
                                     ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Asociar" ToolTip="Asociar" />

                                    <asp:ImageButton ID="gModificar" runat="server" AlternateText="Modificar" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPoligono") %>' 
                                        CommandName="Modificar" Height="20px" ImageUrl="../../App_Themes/admin_style/images/modificar.png" ToolTip="Modificar" Visible="false" />
                                    
                                    <asp:ImageButton ID="gEliminar" runat="server" AlternateText="Eliminar" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPoligono") %>' 
                                        CommandName="Eliminar" Height="20px" ImageUrl="../../App_Themes/admin_style/images/eliminar.png" ToolTip="Eliminar" Visible="false" />

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#5794EF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>       
    </table>
    </fieldset>

        </asp:Panel>

                    </ContentTemplate>
                    </asp:UpdatePanel>
                     
                    </asp:Panel>

                    <!-- Pestaña 2: Antecedentes de Terreno -->
                    <asp:Panel ID="PanelAntecedentesTerreno"  Visible="false" runat="server">
                    
                    <asp:UpdatePanel ID="UpdateAntecedentesTerreno" UpdateMode="Conditional" runat="server">
                    <ContentTemplate> 
        
                    <br />

                    <asp:ValidationSummary ID="ValidationSummaryPanelAntecedentesTerreno" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo7" />
                    
                    <asp:ValidationSummary ID="ValidationSummaryArchivoAdjuntoAntTerreno" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo21" />
                    
                    <asp:ValidationSummary ID="ValidationSummaryPoligonoAntTerreno" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo8" />

                    <asp:ValidationSummary ID="ValidationSummaryVerticeAntTerreno" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo9" />

                    <asp:Panel ID="Content_msgGrillaGral_3" CssClass="Content_msgGrilla" Visible="false" runat="server">
                        <div class="msgGrilla_div1">
                            <asp:Image ID="IcoGrillaGral_3" CssClass="Ico_msgGrilla" runat="server" />
                        </div>
                        <div class="msgGrilla_div2">
                            <asp:Label ID="msgGrillaGral_3" runat="server"></asp:Label>
                        </div>
                    </asp:Panel>
                    
                    <asp:HiddenField ID="IdCoordenadaGeoAntTerreno" runat="server"></asp:HiddenField>
                            
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <fieldset>
                    <legend>Referencias Geográficas</legend>
                    <br />
                    
                    <table class="form" cellpadding="0px" cellspacing="0px">
                         
                    <tr>
                        <td class="col1"><span class="item">DATUM</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">

                            <asp:UpdatePanel ID="UpdatePanelDATUMAntTerreno" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DATUMAntTerreno" runat="server" AutoPostBack="false"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDATUMAntTerreno" runat="server" ControlToValidate="DATUMAntTerreno" Display="Static" ErrorMessage="DATUM" InitialValue="-1" ValidationGroup="grupo7">*</asp:RequiredFieldValidator>
                                    </ContentTemplate>
                             </asp:UpdatePanel>
                        </td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">&nbsp;</td>
                        
                    </tr>

                        <tr>
                            <td class="col1"><span class="item">Huso</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3">
                                <asp:DropDownList ID="HusoHorarioAntecedentesTerreno" AutoPostBack="false" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorHusoHorarioAntecedentesTerreno" runat="server" ControlToValidate="HusoHorarioAntecedentesTerreno"  ValidationGroup="grupo7" ErrorMessage="Huso Horario" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="col3">&nbsp;</td>
                            <td class="col3">&nbsp;</td>
                            <td class="col3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col1" nowrap>
                                <span class="item">Coordenada Geográfica</span></td>
                            <td class="col2">
                                <span class="item">:</span></td>
                            <td class="col3">
                                <span class="item"><asp:CheckBox ID="SeUsaParaBancoAntTerreno" Text="Se usa para Banco" runat="server" /></span>
                            </td>
                            <td class="col3">
                                &nbsp;</td>
                            <td class="col3">
                                &nbsp;</td>
                            <td class="col3">
                                &nbsp;</td>
                        </tr>

                        <tr>
                            <td class="col1">
                                <span class="item">Superficie Total Solicitada</span></td>
                            <td class="col2">
                                <span class="item">:</span></td>
                            <td class="col3">
                                <asp:UpdatePanel ID="UpdatePanelAreaTotalSolicitadaAntecedentesTerreno" 
                                    runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="AreaTotalSolicitadaAntecedentesTerreno" runat="server" 
                                            Columns="5" ReadOnly="True" Width="80px"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GuardarPoligonoAntecedentesTerreno" 
                                            EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                
                            </td>
                            <td class="col3">
                                <span class="item">Superficie Total Calculada</span></td>
                            <td class="col3">
                                :</td>
                            <td class="col3">
                                <asp:UpdatePanel ID="UpdatePanelAreaTotalCalculadaAntecedentesTerreno" 
                                    runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="AreaTotalCalculadaAntecedentesTerreno" runat="server" 
                                            Columns="5" ReadOnly="True" Width="80px"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GuardarPoligonoAntecedentesTerreno" 
                                            EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                        <tr>
                            <td class="col1">&nbsp;</td>
                            <td class="col2">&nbsp;</td>
                            <td class="col3">
                                <asp:ImageButton ID="GuardarReferenciasGeograficasAntTerreno0" runat="server" 
                                    AlternateText="Guardar Referencias Geográfica" CausesValidation="true" 
                                    Height="20px" ImageUrl="~/App_Themes/admin_style/images/add.png" 
                                    onclick="GuardarReferenciasGeograficasAntTerreno_Click" 
                                    ToolTip="Guardar Referencias Geográfica" ValidationGroup="grupo7" />
                                <span class="item">Guardar Referencias Geográficas</span></td>
                            <td class="col3">&nbsp;</td>
                            <td class="col3">&nbsp;</td>
                            
                        </tr>
                    </table>
                    </fieldset>
                    </ContentTemplate>
                    </asp:UpdatePanel>

                    <fieldset>
                    <legend>Archivos Adjuntos</legend>
                    <br /> 
                    
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    
                   <tr>
                   <td class="col1"><span class="item">Tipo Archivo</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                   
                   <asp:DropDownList ID="TipoArchivoAntTerreno" AutoPostBack="false" runat="server"></asp:DropDownList>
                   <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoArchivoAntTerreno" runat="server" ControlToValidate="TipoArchivoAntTerreno" ValidationGroup="grupo21" 
                   ErrorMessage="Tipo Archivo" InitialValue="-1" Display="Static" >*</asp:RequiredFieldValidator>

                   </td>
                    </tr>
                    <tr>
                       <td class="col1"><span class="item">Nombre Archivo</span></td>
                       <td class="col2"><span class="item">:</span></td>
                       <td class="col3">

                       <asp:TextBox ID="NombreArchivoAntTerreno" MaxLength="40" Width="200px" runat="server"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorNombreArchivoAntTerreno" runat="server" ControlToValidate="NombreArchivoAntTerreno" ValidationGroup="grupo21" 
                       ErrorMessage="Nombre Archivo" Display="Static" >*</asp:RequiredFieldValidator>

                       </td>
                    </tr>
                    <tr>
                       <td class="col1"><span class="item">Archivo Adjunto</span></td>
                       <td class="col2"><span class="item">:</span></td>
                       <td class="col3">
                   
                       <asp:UpdatePanel ID="UpdatePanelArchivoAdjuntoAntTerreno" UpdateMode="Conditional" runat="server">
                       <ContentTemplate>

                       <asp:FileUpload ID="ArchivoAdjuntoAntTerreno" MaxLength="40" Width="200px" runat="server" />

                       <asp:RequiredFieldValidator id="RequiredFieldValidatorArchivoAdjuntoAntTerreno" runat="server" ControlToValidate="ArchivoAdjuntoAntTerreno" ValidationGroup="grupo21" 
                       ErrorMessage="Archivo Adjunto" Display="Static" InitialValue="0">*</asp:RequiredFieldValidator>

                       <asp:RegularExpressionValidator ID="RegularExpressionValidatorArchivoAdjuntoAntTerreno" runat="server" ErrorMessage="Solo .doc y .pdf" ControlToValidate="ArchivoAdjuntoAntTerreno" ValidationExpression= "(.*).(.doc|.DOC|.pdf|.PDF)$" ValidationGroup="grupo21" />

                       </ContentTemplate>
                       <Triggers>

                            <asp:PostBackTrigger ControlID="GuardarArchivoAdjuntoAntTerreno" />

                       </Triggers>
                       </asp:UpdatePanel>

                       </td>
                    </tr>

                    <tr>
                        <td class="col1"></td>
                        <td class="col2"></td>
                        <td class="col3">
                            <asp:ImageButton ID="GuardarArchivoAdjuntoAntTerreno" runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                AlternateText="Guardar Archivo Adjunto" ToolTip="Guardar Archivo Adjunto" onclick="GuardarArchivoAdjuntoAntTerreno_Click" CausesValidation="true" ValidationGroup="grupo21"/>
                            <span class="item">Guardar Archivo Adjunto</span>
                        </td>
                    </tr>
                    </table>

                    <asp:Panel ID="PanelArchivoAdjAntTerreno"  Visible="true" runat="server">
                    <asp:GridView 
                        ID="GridArchivoAdjuntoAntTerreno" 
                        runat="server" 
                        AutoGenerateColumns="False"
                        DataKeyNames="idArchivoBinario"  
                        CellPadding="4" 
                        ForeColor="#333333"
                        GridLines="None"
                        AllowPaging="False"
                        AllowSorting="false" 
                        CssClass="mGrid"
                        PagerStyle-CssClass="pgr" 
                        width="100%" 
                        PageIndex= "1"
                        OnRowDataBound="GridArchivoAdjuntoAntTerreno_RowDataBound"
                        OnRowCommand="GridArchivoAdjuntoAntTerreno_RowCommand"
                        OnRowCreated="GridArchivoAdjuntoAntTerreno_RowCreated">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                           <asp:TemplateField HeaderText="Tipo Archivo">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.tipoDocumento.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="Nombre Archivo">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.archivoBinario.nombreArchivo")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Archivo Adjunto">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.archivoBinario.nombreFisico")%>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gDesasociar" Visible="false" runat="server" CausesValidation="false" CommandName="Desasociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBinario") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Desasociar" ToolTip="Desasociar" />
                    
                                    <asp:ImageButton ID="gAsociar" Visible="false" runat="server" CausesValidation="false" CommandName="Asociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBinario") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Asociar" ToolTip="Asociar" />

                                    <asp:ImageButton ID="gDescargar" Visible="false" runat="server" CausesValidation="false" CommandName="Descargar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBinario") %>' 
                                         ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" OnPreRender="ImgAdd_PreRender" />

                                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBinario") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>

                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#5794EF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    </asp:Panel>

                    </fieldset>
                    </ContentTemplate>
                    </asp:UpdatePanel>
        
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                    
                    <fieldset>
                        <legend>Polígono</legend>
                        <br />

                        <asp:HiddenField ID="IdPoligonoAntTerreno" runat="server" value="0" ></asp:HiddenField>

                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                           <td class="col1"><span class="item">Tipo Concesión</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3" colspan="3">
                                <asp:UpdatePanel ID="UpdatePanelTipoConcesionAntecedentesTerreno" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                <asp:ListBox ID="TipoConcesionAntecedentesTerreno" runat="server" Width="256px" SelectionMode="Multiple"></asp:ListBox>

                                <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoConcesionAntecedentesTerreno" runat="server" ControlToValidate="TipoConcesionAntecedentesTerreno"  ValidationGroup="grupo8" ErrorMessage="Tipo Concesión" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                           <td class="col1"><span class="item">Tipo Uso</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3">
                               <asp:DropDownList ID="TipoUsoAntecedentesTerreno" AutoPostBack="false" runat="server"></asp:DropDownList> 
                               <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoUsoAntecedentesTerreno" runat="server" ControlToValidate="TipoUsoAntecedentesTerreno"  ValidationGroup="grupo8" ErrorMessage="Tipo Uso" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                                
                           </td>
                           <td class="col3">&nbsp;</td>
                           <td class="col3">&nbsp;</td>
                        </tr>
                        <tr>
                           <td class="col1"><span class="item">Ubicación Geográfica</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3">
                               <asp:TextBox ID="ToponimioAntecedentesTerreno" runat="server" Columns="8" Width="200px"></asp:TextBox>
                               
                               <asp:RequiredFieldValidator id="RequiredFieldValidatorToponimioAntecedentesTerreno" runat="server" ControlToValidate="ToponimioAntecedentesTerreno"  ValidationGroup="grupo8" ErrorMessage="Toponimio" Display="Static">*</asp:RequiredFieldValidator>

                           </td>
                           <td class="col3">&nbsp;</td>
                           <td class="col3">&nbsp;</td>
                        </tr>
                        <tr>
                           <td class="col1"><span class="item">Superficie Calculada</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3">
                               <asp:TextBox ID="AreaCalculadaAntecedentesTerreno" runat="server" Columns="8" Width="80px"></asp:TextBox> 
                               <asp:RequiredFieldValidator id="RequiredFieldValidatorAreaCalculadaAntecedentesTerreno" runat="server" ControlToValidate="AreaCalculadaAntecedentesTerreno"  ValidationGroup="grupo8" ErrorMessage="Superficie Calculada" Display="Static">*</asp:RequiredFieldValidator>
                               
                               <asp:RegularExpressionValidator ID="RegularExpressionValidatorAreaCalculadaAntecedentesTerreno" ControlToValidate="AreaCalculadaAntecedentesTerreno" ForeColor="Red"  ValidationGroup="grupo8" runat="server" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,9})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>

                           </td>
                           <td class="col3" style="margin-left: 40px"><span class="item">Área Solicitada</span></td>
                           <td class="col3" style="margin-left: 40px">
                               <asp:TextBox ID="AreaSolicitadaAntecedentesTerreno" runat="server" Columns="8" Width="80px"></asp:TextBox>
                               <asp:RequiredFieldValidator id="RequiredFieldValidatorAreaSolicitadaAntecedentesTerreno" runat="server" ControlToValidate="AreaSolicitadaAntecedentesTerreno"  ValidationGroup="grupo8" ErrorMessage="Superficie Solicitada" Display="Static">*</asp:RequiredFieldValidator>
                               
                               <asp:RegularExpressionValidator ID="RegularExpressionValidatorAreaSolicitadaAntecedentesTerreno" ControlToValidate="AreaSolicitadaAntecedentesTerreno" ForeColor="Red"  ValidationGroup="grupo8" runat="server" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,9})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>
                                
                           </td>
                        </tr>
                   </table>
        
                <fieldset>
                <legend>Vértice</legend>
                <br />

                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 
        
                <asp:HiddenField ID="IdVerticeAntTerreno" runat="server" value="0" ></asp:HiddenField>

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                   <td class="col1"><span class="item">Vértice</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:DropDownList ID="VerticeAntecedentesTerreno" AutoPostBack="false" runat="server"></asp:DropDownList>
                       
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorVerticeAntecedentesTerreno" runat="server" ControlToValidate="VerticeAntecedentesTerreno"  ValidationGroup="grupo9" ErrorMessage="Vértice" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                        
                   </td>
                   <td class="col3">&nbsp;</td>
                   <td class="col3">&nbsp;</td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">Latitud</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:TextBox ID="LatitudHoraAntecedentesTerreno" runat="server" AutoPostBack="true" MaxLength="2" Width="40px" ontextchanged="LatitudHoraAntecedentesTerreno_TextChanged" CausesValidation="true"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLatitudHoraAntecedentesTerreno" runat="server" ControlToValidate="LatitudHoraAntecedentesTerreno"  ValidationGroup="grupo9" ErrorMessage="Latitud (Hora)" Display="Static">*</asp:RequiredFieldValidator>
                       <asp:RangeValidator ID="RangeValidatorLatitudHoraAntecedentesTerreno" ControlToValidate="LatitudHoraAntecedentesTerreno" runat="server" Display="Dynamic" ForeColor="Red"  ErrorMessage="Latitud Hora debe ser entero y estar entre el rango 0 a 23." MinimumValue="0" MaximumValue="23" Type="Integer" ValidationGroup="AllValidator"></asp:RangeValidator>

                       <asp:TextBox ID="LatitudMinutoAntecedentesTerreno" runat="server" AutoPostBack="true" MaxLength="2" Width="40px" ontextchanged="LatitudMinutoAntecedentesTerreno_TextChanged" CausesValidation="true"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLatitudMinutoAntecedentesTerreno" runat="server" ControlToValidate="LatitudMinutoAntecedentesTerreno"  ValidationGroup="grupo9" ErrorMessage="Latitud (Minuto)" Display="Static">*</asp:RequiredFieldValidator>
                       <asp:RangeValidator ID="RangeValidatorLatitudMinutoAntecedentesTerreno" ControlToValidate="LatitudMinutoAntecedentesTerreno" runat="server" Display="Dynamic" ForeColor="Red"  ErrorMessage="Longitud Minuto debe ser entero y estar entre el rango 0 a 59." MinimumValue="0" MaximumValue="59" Type="Integer" ValidationGroup="AllValidator"></asp:RangeValidator>

                       <asp:TextBox ID="LatitudSegundoAntecedentesTerreno" runat="server" AutoPostBack="true" MaxLength="7" Width="70px" ontextchanged="LatitudSegundoAntecedentesTerreno_TextChanged" CausesValidation="true"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLatitudSegundoAntecedentesTerreno" runat="server" ControlToValidate="LatitudSegundoAntecedentesTerreno"  ValidationGroup="grupo9" ErrorMessage="Latitud (Segundo)" Display="Static">*</asp:RequiredFieldValidator>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidatorLatitudSegundoAntecedentesTerreno" ControlToValidate="LatitudSegundoAntecedentesTerreno" ForeColor="Red" runat="server" ValidationGroup="grupo9" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,4})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>

                    </td>
                    <td class="col3">
                        <span class="item">Latitud Decimal</span></td>
                    <td class="col3">
                       

                       <asp:UpdatePanel ID="UpdatePanelLatitudDecimalAntecedentesTerreno" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>

                            <asp:TextBox ID="LatitudDecimalAntecedentesTerreno" runat="server" Width="100px" Columns="15" ReadOnly="true"></asp:TextBox>
                      
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="LatitudHoraAntecedentesTerreno" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="LatitudMinutoAntecedentesTerreno" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="LatitudSegundoAntecedentesTerreno" EventName="TextChanged" />
                        </Triggers>
                        </asp:UpdatePanel>

                    </td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">Longitud</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:TextBox ID="LongitudHoraAntecedentesTerreno" runat="server" AutoPostBack="true" MaxLength="2" Width="40px" ontextchanged="LongitudHoraAntecedentesTerreno_TextChanged" CausesValidation="true"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLongitudHoraAntecedentesTerreno" runat="server" ControlToValidate="LongitudHoraAntecedentesTerreno"  ValidationGroup="grupo9" ErrorMessage="Longitud (Hora)" Display="Static">*</asp:RequiredFieldValidator>
                       <asp:RangeValidator ID="RangeValidatorLongitudHoraAntecedentesTerreno" ControlToValidate="LongitudHoraAntecedentesTerreno" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Longitud Hora debe ser entero y estar entre el rango 0 a 23." MinimumValue="0" MaximumValue="23" Type="Integer" ValidationGroup="grupo9"></asp:RangeValidator>

                       <asp:TextBox ID="LongitudMinutoAntecedentesTerreno" runat="server" AutoPostBack="true" MaxLength="2" Width="40px" ontextchanged="LongitudMinutoAntecedentesTerreno_TextChanged" CausesValidation="true"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLongitudMinutoAntecedentesTerreno" runat="server" ControlToValidate="LongitudMinutoAntecedentesTerreno"  ValidationGroup="grupo9" ErrorMessage="Longitud (Minuto)" Display="Static">*</asp:RequiredFieldValidator>
                       <asp:RangeValidator ID="RangeValidatorLongitudMinutoAntecedentesTerreno" ControlToValidate="LongitudMinutoAntecedentesTerreno" runat="server" ForeColor="Red"  Display="Dynamic" ErrorMessage="Longitud Minuto debe ser entero y estar entre el rango 0 a 59." MinimumValue="0" MaximumValue="59" Type="Integer" ValidationGroup="grupo9"></asp:RangeValidator>

                       <asp:TextBox ID="LongitudSegundoAntecedentesTerreno" runat="server" AutoPostBack="true" MaxLength="7" Width="70px" ontextchanged="LongitudSegundoAntecedentesTerreno_TextChanged" CausesValidation="true"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLongitudSegundoAntecedentesTerreno" runat="server" ControlToValidate="LongitudSegundoAntecedentesTerreno"  ValidationGroup="grupo9" ErrorMessage="Longitud (Segundo)" Display="Static">*</asp:RequiredFieldValidator>
                       
                       <asp:RegularExpressionValidator ID="RegularExpressionValidatorLongitudSegundoAntecedentesTerreno" ControlToValidate="LongitudSegundoAntecedentesTerreno" ForeColor="Red"  runat="server" ValidationGroup="grupo9" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,4})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>

                       </td>
                    <td class="col3">
                        <span class="item">Longitud Decimal</span></td>
                    <td class="col3">
                        
                        <asp:UpdatePanel ID="UpdatePanelLongitudDecimalAntecedentesTerreno" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>

                            <asp:TextBox ID="LongitudDecimalAntecedentesTerreno" runat="server" Columns="15" Width="100px" ReadOnly="true"></asp:TextBox>
                      
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="LongitudHoraAntecedentesTerreno" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="LongitudMinutoAntecedentesTerreno" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="LongitudSegundoAntecedentesTerreno" EventName="TextChanged" />
                        </Triggers>
                        </asp:UpdatePanel>

                    </td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">UTM E</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:TextBox ID="UTMEAntecedentesTerreno" runat="server" Columns="8" Width="80px"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorUTMEAntecedentesTerreno" runat="server" ControlToValidate="UTMEAntecedentesTerreno"  ValidationGroup="grupo9" ErrorMessage="UTM E" Display="Static">*</asp:RequiredFieldValidator>
                       
                       <asp:RegularExpressionValidator ID="RegularExpressionValidatorUTMEAntecedentesTerreno" ControlToValidate="UTMEAntecedentesTerreno" ForeColor="Red"  ValidationGroup="grupo9" runat="server" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,9})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>
                        
                    </td>
                    <td class="col3" style="margin-left: 40px"><span class="item">UTM N</span></td>
                    <td class="col3" style="margin-left: 40px">
                        <asp:TextBox ID="UTMNAntecedentesTerreno" runat="server" Columns="8" Width="80px"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorUTMNAntecedentesTerreno" runat="server" ControlToValidate="UTMNAntecedentesTerreno"  ValidationGroup="grupo9" ErrorMessage="UTM N" Display="Static">*</asp:RequiredFieldValidator>
                        
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorUTMNAntecedentesTerreno" ControlToValidate="UTMNAntecedentesTerreno" ForeColor="Red"  ValidationGroup="grupo9" runat="server" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,9})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>

                    </td>
                </tr>
               
                 <tr>
                 <td colspan="5">
                     <table width="100%" border="0">
                        <tr>
                            <td align="center">
                            <asp:Panel ID="PanelBotonesVerticeAntTerreno"  Visible="true" runat="server">

                            <asp:ImageButton ID="GuardarVerticeAntTerreno" runat="server" 
                            ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                            AlternateText="Guardar Vértice" ToolTip="Guardar Vértice" 
                            CausesValidation="true" ValidationGroup="grupo9" onclick="GuardarVerticeAntTerreno_Click" 
                            />
                            <span class="item">Guardar Vértice</span>
                            &nbsp;&nbsp;
                                    <asp:ImageButton ID="LimpiatVerticeAntTerreno" runat="server" 
                                    ImageUrl="~/App_Themes/admin_style/images/clean.png" Height="20px" 
                                    AlternateText="Limpiar Vértice" ToolTip="Limpiar Vértice" onclick="LimpiatVerticeAntTerreno_Click" 
                                    />

                            </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>  
                </tr>

                <tr>
                <td colspan="5">
                    <!-- Lista de Vértices -->
                    <asp:GridView 
                       ID="GridVerticeAntTerreno"
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" PageSize="10"
                       CssClass="mGrid"
                       OnRowDataBound="GridVerticeAntTerreno_RowDataBound"
                       PagerStyle-CssClass="pgr"
                       OnRowCommand="GridVerticeAntTerreno_RowCommand"
                       Width="100%">
                       <RowStyle BackColor="#EFF3FB" />
                       <Columns>
                           <asp:TemplateField HeaderText="Vértice">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.vertice.descripcion") %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="Latitud">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.latitudHora") + "º" + DataBinder.Eval(Container, "DataItem.latitudMinuto") + "'" + DataBinder.Eval(Container, "DataItem.latitudSegundo") + "''"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Latitud Decimal">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.latitudDecimal") %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Longitud">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.longitudHora") + "º" + DataBinder.Eval(Container, "DataItem.longitudMinuto") + "'" + DataBinder.Eval(Container, "DataItem.longitudSegundo") + "''"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Longitud Decimal">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.longitudDecimal") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UTM E">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.utmE")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UTM N">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.utmN")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    
                                    <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idVertice") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                                    <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idVertice") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                                    
                                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idVertice") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#5794EF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView> 
                </td>
                </tr>
           </table>
                </ContentTemplate>
                </asp:UpdatePanel>
                </fieldset>
                
                <table class="form" cellpadding="0px" cellspacing="0px">
                 <tr>
                 <td colspan="5">
                     <table width="100%" border="0">
                        <tr>
                            <td align="center">
                            <asp:Panel ID="PanelBotonesAntecedentesTerreno"  Visible="true" runat="server">
                            <asp:ImageButton ID="GuardarPoligonoAntecedentesTerreno" runat="server" AutoPostBack="true" 
                             ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px"  AlternateText="Guardar Polígono" ToolTip="Guardar Polígono" 
                             CausesValidation="true" ValidationGroup="grupo8" onclick="GuardarPoligonoAntecedentesTerreno_Click"/>
                             <span class="item">Guardar Polígono</span>
                             </asp:Panel>
                             <asp:ImageButton ID="LimpiarPoligonoAntTerreno" runat="server" 
                                AlternateText="Limpiar Poligono" Height="20px" 
                                ImageUrl="~/App_Themes/admin_style/images/clean.png" 
                                onclick="LimpiarPoligonoAntTerreno_Click" ToolTip="Limpiar Poligono" />

                            </td>
                        </tr>
                    </table>
                </td>  
                </tr>

                <tr>
                <td colspan="5">
                    <!-- Lista de Polígonos -->
                    <asp:GridView ID="GridPoligonoAntecedentesTerreno" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" CellPadding="4" CssClass="mGrid" 
                        ForeColor="#333333" GridLines="None" DataKeyNames="idPoligono"
                        PageSize="10" Width="100%"
                        OnRowDataBound="GridPoligonoAntecedentesTerreno_RowDataBound"
                        PagerStyle-CssClass="pgr"
                        OnRowCommand="GridPoligonoAntecedentesTerreno_RowCommand"
                        OnRowCreated="GridPoligonoAntecedentesTerreno_RowCreated" 
                        >
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="Tipo Concesión">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.tipoConcesionString")%>
                                </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="Tipo Uso">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.tipoUso.descripcion") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ubicación Geográfica">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.toponimio") %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            
                            <asp:TemplateField HeaderText="Superficie Solicitada">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.areaSolicitada")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Superficie Calculada">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.areaCalculada") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                <ItemTemplate>

                                    <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPoligono") %>'
                                    ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                                    <asp:ImageButton ID="gDesasociar" Visible="false" runat="server" CausesValidation="false" CommandName="Desasociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPoligono") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Desasociar" ToolTip="Desasociar" />
                    
                                    <asp:ImageButton ID="gAsociar" Visible="false" runat="server" CausesValidation="false" CommandName="Asociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPoligono") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Asociar" ToolTip="Asociar" />

                                    <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPoligono") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                                    
                                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPoligono") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#5794EF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
    </table>
     
                    </fieldset>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </asp:Panel>

                    <!-- Pestaña 3: Regularización -->
                    <asp:Panel ID="PanelRegularizacion"  Visible="false" runat="server">
                    <asp:UpdatePanel ID="UpdatePanelPanelRegularizacion" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                    
                    <br />
                    
                    <asp:ValidationSummary ID="ValidationSummaryRegularizacion" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo10" />
                    
                    <asp:ValidationSummary ID="ValidationSummaryArchivoAdjuntoRegul" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo22" />
                     
                    <asp:ValidationSummary ID="ValidationSummaryPoligonoRegul" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo12" />
                    
                    <asp:ValidationSummary ID="ValidationSummaryVerticeRegul" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo11" />
                    
                    <asp:Panel ID="Content_msgGrillaGral_4" CssClass="Content_msgGrilla" Visible="false" runat="server">
                        <div class="msgGrilla_div1">
                            <asp:Image ID="IcoGrillaGral_4" CssClass="Ico_msgGrilla" runat="server" />
                        </div>
                        <div class="msgGrilla_div2">
                            <asp:Label ID="msgGrillaGral_4" runat="server"></asp:Label>
                        </div>
                    </asp:Panel>
                     
                     <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                     <ContentTemplate>              
                        <fieldset>
                    <legend>Referencias Geográficas</legend>
                    
                    <br />       
                    
                    <asp:HiddenField ID="IdReferenciaGeograficaRegularizacion" runat="server"></asp:HiddenField>

                    <asp:HiddenField ID="IdCartaRegul" runat="server"></asp:HiddenField>

                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Carta</span></td>
                        <td class="col2">&nbsp;</td>
                        <td class="col3">
                            <link href="AutoCompleteStyle.css" rel="stylesheet" type="text/css" />
                            <asp:TextBox ID="NombreCartaRegul" AutoPostBack="true" runat="server" Width="200px" ontextchanged="NombreCartaRegul_TextChanged"></asp:TextBox>
                            <cc1:AutoCompleteExtender 
                                runat="server" 
                                ID="AutoCompleteExtenderRegul" 
                                TargetControlID="NombreCartaRegul"
                                ServicePath="antDelSector.asmx"
                                ServiceMethod="BuscarCartaAntSector"
                                MinimumPrefixLength="2" 
                                CompletionInterval="1000"
                                EnableCaching="true"
                                CompletionListCssClass="completionList"
                                CompletionListHighlightedItemCssClass="itemHighlighted"
                                CompletionListItemCssClass="listItem"
                                >
                            </cc1:AutoCompleteExtender>

                            <asp:RequiredFieldValidator id="RequiredFieldValidatorNombreCartaRegul" runat="server" ControlToValidate="NombreCartaRegul" ValidationGroup="grupo10" 
                            ErrorMessage="Carta" Display="Static" >*</asp:RequiredFieldValidator>

                        </td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">&nbsp;</td>
                    </tr>
                        <tr>
                            <td class="col1">
                                <span class="item">Nº Carta</span></td>
                            <td class="col2">
                                <span class="item">:</span></td>
                            <td class="col3">
                                <asp:TextBox ID="NumeroCartaRegul" runat="server" ReadOnly ="true"></asp:TextBox>
                            </td>
                            <td class="col3"><span class="item">
                                Edición</span></td>
                            <td class="col3">
                                :</td>
                            <td class="col3">
                                <asp:TextBox ID="EdicionCartaRegul" runat="server" ReadOnly ="true" ></asp:TextBox>
                            </td>
                            <td class="col3">
                                <span class="item">Año</span></td>
                            <td class="col3">
                                <span class="item">:</span></td>
                            <td class="col3">
                                <asp:TextBox ID="AnioCartaRegul" runat="server" ReadOnly ="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">
                                <span class="item">DATUM</span></td>
                            <td class="col2">
                                <span class="item">:</span></td>
                            <td class="col3">
                                <asp:TextBox ID="DatumRegul" runat="server" ReadOnly ="true"></asp:TextBox>
                                <asp:HiddenField ID="IdDatumRegul" runat="server"></asp:HiddenField>
                            </td>
                            <td class="col3">
                                <span class="item">Huso Horario</span></td>
                            <td class="col3">
                                <span class="item">:</span></td>
                            <td class="col3">
                                <asp:TextBox ID="HusoHorarioRegul" runat="server" ReadOnly="true"></asp:TextBox>
                                <asp:HiddenField ID="IdHusoHorarioRegul" runat="server"></asp:HiddenField>
                            </td>
                            <td class="col3">&nbsp;</td>
                            <td class="col3">&nbsp;</td>
                            <td class="col3">&nbsp;</td>
                        </tr>


                    <tr>
                        <td class="col1" nowrap><span class="item">Coordenada Geográfica</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3" colspan="4">
                            
                            <span class="item"><asp:CheckBox ID="SeUsaParaBancoRegularizacion" runat="server" Text="Se Usa para Banco"/></span>

                        </td>
                        <td class="col3">
                            &nbsp;</td>
                        <td class="col3">
                            &nbsp;</td>
                        <td class="col3">
                            &nbsp;</td>
                    </tr>

                        <tr>
                            <td class="col1">
                                <span class="item">Superficie Total Regularización</span></td>
                            <td class="col2">
                                <span class="item">:</span></td>
                            <td class="col3" colspan="4">
                                <asp:UpdatePanel ID="UpdatePanelAreaTotalRegularizacion" runat="server" 
                                    UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="AreaTotalRegularizacion" runat="server" Columns="8" 
                                            ReadOnly="True" Width="80px"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GuardarPoligonoRegul" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td class="col3">
                                &nbsp;</td>
                            <td class="col3">
                                &nbsp;</td>
                            <td class="col3">
                                &nbsp;</td>
                        </tr>

                    <tr>
                        <td class="col1"></td>
                        <td class="col2"></td>
                        <td class="col3">
                            <asp:ImageButton ID="GuardarReferenciasGeograficasRegul" runat="server" 
                                ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                AlternateText="Guardar Referencias Geográfica" 
                                ToolTip="Guardar Referencias Geográfica" 
                                onclick="GuardarReferenciasGeograficasRegul_Click" CausesValidation="true" 
                                ValidationGroup="grupo10" style="width: 20px" />
                            <span class="item">Guardar Referencias Geográficas</span>
                        </td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">
                            &nbsp;</td>
                        <td class="col3">&nbsp;</td>
                        <td class="col3">
                            &nbsp;</td>
                        <td class="col3">
                            &nbsp;</td>
                        <td class="col3">
                            &nbsp;</td>
                    </tr>
                    </table>
                    

                             
                    </fieldset>
                     </ContentTemplate>
                     </asp:UpdatePanel>
                      
                    <fieldset>
                    <legend>Archivos Adjuntos</legend>
                    <br />
                          
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    
                   <tr>
                   <td class="col1"><span class="item">Tipo Archivo</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                   
                   <asp:DropDownList ID="TipoArchivoRegularizacion" AutoPostBack="false" runat="server"></asp:DropDownList>
                   <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoArchivoRegularizacion" runat="server" ControlToValidate="TipoArchivoRegularizacion" ValidationGroup="grupo22" 
                   ErrorMessage="Tipo Archivo" InitialValue="-1" Display="Static" >*</asp:RequiredFieldValidator>

                   </td>
                    </tr>
                    <tr>
                       <td class="col1"><span class="item">Nombre Archivo</span></td>
                       <td class="col2"><span class="item">:</span></td>
                       <td class="col3">

                       <asp:TextBox ID="NombreArchivoRegularizacion" MaxLength="40" Width="200px" runat="server"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorNombreArchivoRegularizacion" runat="server" ControlToValidate="NombreArchivoRegularizacion" ValidationGroup="grupo22" 
                       ErrorMessage="Nombre Archivo" Display="Static" >*</asp:RequiredFieldValidator>

                       </td>
                    </tr>
                    <tr>
                       <td class="col1"><span class="item">Archivo Adjunto</span></td>
                       <td class="col2"><span class="item">:</span></td>
                       <td class="col3">
                   
                       <asp:UpdatePanel ID="UpdatePanelArchivoAdjuntoRegularizacion" UpdateMode="Conditional" runat="server">
                       <ContentTemplate>

                       <asp:FileUpload ID="ArchivoAdjuntoRegularizacion" runat="server" />

                       <asp:RequiredFieldValidator id="RequiredFieldValidatorArchivoAdjuntoRegularizacion" runat="server" ControlToValidate="ArchivoAdjuntoRegularizacion" ValidationGroup="grupo22" 
                       ErrorMessage="Archivo Adjunto" Display="Static" InitialValue="0">*</asp:RequiredFieldValidator>

                       <asp:RegularExpressionValidator ID="RegularExpressionValidatorArchivoAdjuntoRegularizacion" runat="server" ErrorMessage="Solo .doc .jpg y .pdf" ControlToValidate="ArchivoAdjuntoRegularizacion" ValidationExpression= "(.*).(.doc|.DOC|.pdf|.PDF|.jpg|.JPG|.jpeg|.JPEG)$" ValidationGroup="grupo22" />

                       </ContentTemplate>
                       <Triggers>

                            <asp:PostBackTrigger ControlID="GuardarArchivoAdjuntoRegularizacion" />

                       </Triggers>
                       </asp:UpdatePanel>
                       </td>
                    </tr>

                    <tr>
                        <td class="col1"></td>
                        <td class="col2"></td>
                        <td class="col3">
                            <asp:ImageButton ID="GuardarArchivoAdjuntoRegularizacion" runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                AlternateText="Guardar Archivo Adjunto" ToolTip="Guardar Archivo Adjunto" onclick="GuardarArchivoAdjuntoRegularizacion_Click" CausesValidation="true" ValidationGroup="grupo22"/>
                            <span class="item">Guardar Archivo Adjunto</span>
                        </td>
                    </tr>
                    </table>

                    <asp:Panel ID="PanelArchivoAdjRegularizacion"  Visible="true" runat="server">
                    <asp:GridView 
                        ID="GridArchivoAdjuntoRegularizacion" 
                        runat="server"
                        DataKeyNames="idArchivoBinario"  
                        AutoGenerateColumns="False" 
                        CellPadding="4" 
                        ForeColor="#333333"
                        GridLines="None"
                        AllowPaging="False"
                        AllowSorting="false" 
                        CssClass="mGrid"
                        PagerStyle-CssClass="pgr" 
                        width="100%" 
                        PageIndex="1"
                        OnRowDataBound="GridArchivoAdjuntoRegularizacion_RowDataBound"
                        OnRowCommand="GridArchivoAdjuntoRegularizacion_RowCommand"
                        OnRowCreated="GridArchivoAdjuntoRegularizacion_RowCreated">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                           <asp:TemplateField HeaderText="Tipo Archivo">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.tipoDocumento.descripcion") %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="Nombre Archivo">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.archivoBinario.nombreArchivo")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Archivo Adjunto">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.archivoBinario.nombreFisico")%>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gDesasociar" Visible="false" runat="server" CausesValidation="false" CommandName="Desasociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBinario") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Desasociar" ToolTip="Desasociar" />
                    
                                    <asp:ImageButton ID="gAsociar" Visible="false" runat="server" CausesValidation="false" CommandName="Asociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBinario") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Asociar" ToolTip="Asociar" />

                                    <asp:ImageButton ID="gDescargar" Visible="false" runat="server" CausesValidation="false" CommandName="Descargar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBinario") %>' 
                                         ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" OnPreRender="ImgAdd_PreRender" />

                                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBinario") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>

                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#5794EF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    </asp:Panel>

                    </fieldset>

                    </ContentTemplate>
                    </asp:UpdatePanel>

        <fieldset>
        <legend>Polígono</legend>
        <br />

        <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
        
        <asp:HiddenField ID="IdPoligonoRegul" runat="server" value="0" ></asp:HiddenField>

        <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                   <td class="col1"><span class="item">Tipo Concesión</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanelTipoConsecionRegularizacion" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                        <asp:ListBox ID="TipoConsecionRegularizacion" runat="server" Width="256px" SelectionMode="Multiple"></asp:ListBox>

                        <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoConsecionRegularizacion" runat="server" ControlToValidate="TipoConsecionRegularizacion"  ValidationGroup="grupo12" ErrorMessage="Tipo Concesión" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>   
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">Tipo Uso</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:DropDownList ID="TipoUsoRegularizacion" AutoPostBack="false" runat="server"></asp:DropDownList> 
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoUsoRegularizacion" runat="server" ControlToValidate="TipoUsoRegularizacion"  ValidationGroup="grupo12" ErrorMessage="Tipo Uso" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>

                   </td>
                   <td class="col3">&nbsp;</td>
                   <td class="col3">&nbsp;</td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">Ubicación Geográfica</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:TextBox ID="ToponimioRegularizacion" runat="server" Columns="8" Width="200px"></asp:TextBox>

                       <asp:RequiredFieldValidator id="RequiredFieldValidatorToponimioRegularizacion" runat="server" ControlToValidate="ToponimioRegularizacion"  ValidationGroup="grupo12" ErrorMessage="Toponimio" Display="Static">*</asp:RequiredFieldValidator>

                       </td>
                    <td class="col3">&nbsp;</td>
                    <td class="col3">&nbsp;</td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">Superficie Regularización</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:TextBox ID="AreaRegularizacion" runat="server" Columns="8" Width="80px"></asp:TextBox>
                        
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorAreaRegularizacion" runat="server" ControlToValidate="AreaRegularizacion"  ValidationGroup="grupo12" ErrorMessage="Superficie Regularización" Display="Static">*</asp:RequiredFieldValidator>
                       
                       <asp:RegularExpressionValidator ID="RegularExpressionValidatorAreaRegularizacion" ControlToValidate="AreaRegularizacion" ForeColor="Red"  ValidationGroup="grupo12" runat="server" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,9})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>

                   </td>
                </tr>
           </table>
        
        <fieldset>
        <legend>Vértice</legend>
        <br />

        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
        
        <asp:HiddenField ID="IdVerticeRegul" runat="server" value="0" ></asp:HiddenField>

        <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                   <td class="col1"><span class="item">Vértice</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:DropDownList ID="VerticeRegularizacion" AutoPostBack="false" runat="server"></asp:DropDownList> 
                   
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorVerticeRegularizacion" runat="server" ControlToValidate="VerticeRegularizacion"  ValidationGroup="grupo11" ErrorMessage="Vértice" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>

                   </td>
                    <td class="col3">&nbsp;</td>
                    <td class="col3">&nbsp;</td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">Latitud</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:TextBox ID="LatitudHoraRegularizacion" runat="server" AutoPostBack="true" MaxLength="2" Width="40px" ontextchanged="LatitudHoraRegularizacion_TextChanged" CausesValidation="true"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLatitudHoraRegularizacion" runat="server" ControlToValidate="LatitudHoraRegularizacion"  ValidationGroup="grupo11" ErrorMessage="Latitud (Hora)" Display="Static">*</asp:RequiredFieldValidator>
                       <asp:RangeValidator ID="RangeValidatorLatitudHoraRegularizacion" ControlToValidate="LatitudHoraRegularizacion" runat="server" Display="Dynamic" ForeColor="Red"  ErrorMessage="Latitud Hora debe ser entero y estar entre el rango 0 a 23." MinimumValue="0" MaximumValue="23" Type="Integer" ValidationGroup="AllValidator"></asp:RangeValidator>

                       <asp:TextBox ID="LatitudMinutoRegularizacion" runat="server" AutoPostBack="true" MaxLength="2" Width="40px" ontextchanged="LatitudMinutoRegularizacion_TextChanged" CausesValidation="true"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLatitudMinutoRegularizacion" runat="server" ControlToValidate="LatitudMinutoRegularizacion"  ValidationGroup="grupo11" ErrorMessage="Latitud (Minuto)" Display="Static">*</asp:RequiredFieldValidator>
                       <asp:RangeValidator ID="RangeValidatorLatitudMinutoRegularizacion" ControlToValidate="LatitudMinutoRegularizacion" runat="server" Display="Dynamic" ForeColor="Red"  ErrorMessage="Latitud Minuto debe ser entero y estar entre el rango 0 a 59." MinimumValue="0" MaximumValue="59" Type="Integer" ValidationGroup="AllValidator"></asp:RangeValidator>

                       <asp:TextBox ID="LatitudSegundoRegularizacion" runat="server" AutoPostBack="true" MaxLength="7" Width="70px" ontextchanged="LatitudSegundoRegularizacion_TextChanged" CausesValidation="true"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLatitudSegundoRegularizacion" runat="server" ControlToValidate="LatitudSegundoRegularizacion"  ValidationGroup="grupo11" ErrorMessage="Latitud (Segundo)" Display="Static">*</asp:RequiredFieldValidator>
                       
                       <asp:RegularExpressionValidator ID="RegularExpressionValidatorLatitudSegundoRegularizacion" ControlToValidate="LatitudSegundoRegularizacion" ForeColor="Red"  runat="server" ValidationGroup="grupo11" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,4})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>

                       </td>
                    <td class="col3">
                        <span class="item">Latitud Decimal</span></td>
                    <td class="col3">

                        <asp:UpdatePanel ID="UpdatePanelLatitudDecimalRegularizacion" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>

                            <asp:TextBox ID="LatitudDecimalRegularizacion" runat="server" Width="100px" Columns="15" ReadOnly="true"></asp:TextBox>
                      
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="LatitudHoraRegularizacion" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="LatitudMinutoRegularizacion" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="LatitudSegundoRegularizacion" EventName="TextChanged" />
                        </Triggers>
                        </asp:UpdatePanel>

                       
                    </td>
                   
                </tr>
                <tr>
                   <td class="col1"><span class="item">Longitud</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">

                       <asp:TextBox ID="LongitudHoraRegularizacion" runat="server" AutoPostBack="true" MaxLength="2" Width="40px" ontextchanged="LongitudHoraRegularizacion_TextChanged" CausesValidation="true"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLongitudHoraRegularizacion" runat="server" ControlToValidate="LongitudHoraRegularizacion"  ValidationGroup="grupo11" ErrorMessage="Longitud (Hora)" Display="Static">*</asp:RequiredFieldValidator>
                       <asp:RangeValidator ID="RangeValidatorLongitudHoraRegularizacion" ControlToValidate="LongitudHoraRegularizacion" runat="server" ForeColor="Red"  Display="Dynamic" ErrorMessage="Longitud Hora debe ser entero y estar entre el rango 0 a 23." MinimumValue="0" MaximumValue="23" Type="Integer" ValidationGroup="grupo11"></asp:RangeValidator>

                       <asp:TextBox ID="LongitudMinutoRegularizacion" runat="server" AutoPostBack="true" MaxLength="2" Width="40px" ontextchanged="LongitudMinutoRegularizacion_TextChanged" CausesValidation="true"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLongitudMinutoRegularizacion" runat="server" ControlToValidate="LongitudMinutoRegularizacion"  ValidationGroup="grupo11" ErrorMessage="Longitud (Minuto)" Display="Static">*</asp:RequiredFieldValidator>
                       <asp:RangeValidator ID="RangeValidatorLongitudMinutoRegularizacion" ControlToValidate="LongitudMinutoRegularizacion" runat="server" ForeColor="Red"  Display="Dynamic" ErrorMessage="Longitud Minuto debe ser entero y estar entre el rango 0 a 59." MinimumValue="0" MaximumValue="59" Type="Integer" ValidationGroup="grupo11"></asp:RangeValidator>

                       <asp:TextBox ID="LongitudSegundoRegularizacion" runat="server" AutoPostBack="true" MaxLength="7" Width="70px" ontextchanged="LongitudSegundoRegularizacion_TextChanged" CausesValidation="true"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorLongitudSegundoRegularizacion" runat="server" ControlToValidate="LongitudSegundoRegularizacion"  ValidationGroup="grupo11" ErrorMessage="Longitud (Segundo)" Display="Static">*</asp:RequiredFieldValidator>
                       
                       <asp:RegularExpressionValidator ID="RegularExpressionValidatorLongitudSegundoRegularizacion" ControlToValidate="LongitudSegundoRegularizacion" ForeColor="Red"  runat="server" ValidationGroup="grupo11" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,4})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>

                    </td>
                    <td class="col3">
                        <span class="item">Longitud Decimal</span></td>
                    <td class="col3">

                        <asp:UpdatePanel ID="UpdatePanelLongitudDecimalRegularizacion" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>

                            <asp:TextBox ID="LongitudDecimalRegularizacion" runat="server" Columns="15" Width="100px" ReadOnly="true"></asp:TextBox>
                      
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="LongitudHoraRegularizacion" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="LongitudMinutoRegularizacion" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="LongitudSegundoRegularizacion" EventName="TextChanged" />
                        </Triggers>
                        </asp:UpdatePanel>

                    </td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">UTM E</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:TextBox ID="UTMERegularizacion" runat="server" Columns="8" Width="80px"></asp:TextBox>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorUTMERegularizacion" runat="server" ControlToValidate="UTMERegularizacion"  ValidationGroup="grupo11" ErrorMessage="UTM E" Display="Static">*</asp:RequiredFieldValidator>
                       
                       <asp:RegularExpressionValidator ID="RegularExpressionValidatorUTMERegularizacion" ControlToValidate="UTMERegularizacion" ForeColor="Red"  ValidationGroup="grupo11" runat="server" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,9})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator> 
                    
                    </td>
                    <td class="col3" style="margin-left: 40px"><span class="item">UTM N</span></td>
                    <td class="col3" style="margin-left: 40px">
                        <asp:TextBox ID="UTMNRegularizacion" runat="server" Columns="8" Width="80px"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorUTMNRegularizacion" runat="server" ControlToValidate="UTMNRegularizacion"  ValidationGroup="grupo11" ErrorMessage="UTM N" Display="Static">*</asp:RequiredFieldValidator>
                        
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorUTMNRegularizacion" ControlToValidate="UTMNRegularizacion" ForeColor="Red"  ValidationGroup="grupo11" runat="server" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,9})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator> 

                    </td>
                </tr>
                
                <tr>
                 <td colspan="5">
                     <table width="100%" border="0">
                        <tr>
                            <td align="center">

                                 <asp:Panel ID="PanelBotonesVerticesRegul"  Visible="true" runat="server">
                                     <asp:ImageButton ID="GuardarVerticeRegul" runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                     AlternateText="Guardar Vértice" ToolTip="Guardar Vértice" onclick="GuardarVerticeRegul_Click" CausesValidation="true" ValidationGroup="grupo11" />
                                     <span class="item">Guardar Vértice</span>
                                    &nbsp;&nbsp;
                                    <asp:ImageButton ID="LimpiarVerticeRegul" runat="server" 
                                    ImageUrl="~/App_Themes/admin_style/images/clean.png" Height="20px" 
                                    AlternateText="Limpiar Vértice" ToolTip="Limpiar Vértice" onclick="LimpiarVerticeRegul_Click" 
                                    />
                                    </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>  
                </tr>

                <tr>
                <td colspan="5">
                    <!-- Lista de Vértices -->
                    <asp:GridView 
                       ID="GridVerticeRegularizacion"
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" PageSize="10"
                       CssClass="mGrid"
                       OnRowDataBound="GridVerticeRegularizacion_RowDataBound"
                       PagerStyle-CssClass="pgr"
                       OnRowCommand="GridVerticeRegularizacion_RowCommand"
                       Width="100%">
                       <RowStyle BackColor="#EFF3FB" />
                       <Columns>
                           <asp:TemplateField HeaderText="Vértice">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.vertice.descripcion") %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="Latitud">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.latitudHora") + "º" + DataBinder.Eval(Container, "DataItem.latitudMinuto") + "'" + DataBinder.Eval(Container, "DataItem.latitudSegundo") + "''"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Latitud Decimal">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.latitudDecimal") %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Longitud">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.longitudHora") + "º" + DataBinder.Eval(Container, "DataItem.longitudMinuto") + "'" + DataBinder.Eval(Container, "DataItem.longitudSegundo") + "''"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Longitud Decimal">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.longitudDecimal") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UTM E">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.utmE")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UTM N">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.utmN")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                <ItemTemplate>

                                    <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idVertice") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                                    <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idVertice") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                                    
                                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idVertice") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                                </ItemTemplate> 
                            </asp:TemplateField>
                        </Columns>

                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#5794EF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView> 
                </td>
                </tr>
           </table>

        </ContentTemplate>
        </asp:UpdatePanel>

        </fieldset>
        <table class="form" cellpadding="0px" cellspacing="0px">
               
                 <tr>
                 <td colspan="5">
                     <table width="100%" border="0">
                        <tr>
                            <td align="center">
                                <asp:Panel ID="PanelBotonesPoligonosRegul"  Visible="true" runat="server">
                                 <asp:ImageButton ID="GuardarPoligonoRegul" runat="server" AutoPostBack="true" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                 AlternateText="Guardar Polígono" ToolTip="Guardar Polígono"  onclick="GuardarPoligonoRegul_Click" CausesValidation="true" ValidationGroup="grupo12" />
                                 <span class="item">Guardar Polígono</span>
                                 </asp:Panel>
                                <asp:ImageButton ID="LimpiarPoligonoRegul" runat="server" 
                                AlternateText="Limpiar Poligono" Height="20px" 
                                ImageUrl="~/App_Themes/admin_style/images/clean.png" 
                                onclick="LimpiarPoligonoRegul_Click" ToolTip="Limpiar Poligono" />

                            </td>
                        </tr>
                    </table>
                </td>  
                </tr>
            <tr>
                <td colspan="5">
                    <!-- Lista de Polígonos -->
                    <asp:GridView ID="GridPoligonoRegul" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" CellPadding="4" CssClass="mGrid" 
                        ForeColor="#333333" GridLines="None" 
                        PageSize="10" Width="100%" DataKeyNames="idPoligono"
                        OnRowDataBound="GridPoligonoRegul_RowDataBound"
                        PagerStyle-CssClass="pgr"
                        OnRowCommand="GridPoligonoRegul_RowCommand"
                        OnRowCreated="GridPoligonoRegul_RowCreated" 
                        >
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="Tipo Concesión">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.tipoConcesionString")%>
                                </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="Tipo Uso">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.tipoUso.descripcion") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ubicación Geográfica">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.toponimio") %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Superficie Regularización">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.areaRegularizacion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                <ItemTemplate>

                                   <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPoligono") %>'
                                    ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                                   <asp:ImageButton ID="gDesasociar" Visible="false" runat="server" CausesValidation="false" CommandName="Desasociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPoligono") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Desasociar" ToolTip="Desasociar" />
                    
                                    <asp:ImageButton ID="gAsociar" Visible="false" runat="server" CausesValidation="false" CommandName="Asociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPoligono") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Asociar" ToolTip="Asociar" />

                                    <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPoligono") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                                    
                                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPoligono") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#5794EF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            </table>
        
        </ContentTemplate>
        </asp:UpdatePanel>
        </fieldset>
                    
            </asp:Panel>

            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnk_AntecedentesEspaciales" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="lnk_AntecedentesTerreno" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="lnk_Regularizacion" EventName="Click" />
            </Triggers>

      </asp:UpdatePanel>

    </fieldset>

    <!-- Sección Observaciones -->
   <asp:UpdatePanel ID="UpdatePanelObservacionesGenerales" UpdateMode="Conditional" runat="server">
   <ContentTemplate>

   <asp:Panel ID="PanelObservacionesGenerales"  Visible="false" runat="server">
                    
        <asp:ValidationSummary ID="ValidationSummaryObservaciones" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo6" />
        <br />

        <fieldset>
        <legend>Observaciones</legend>
        <br />
        <asp:Panel ID="PanelObservaciones" CssClass="Content_msgGrilla" Visible="false" runat="server">
            <div class="msgGrilla_div1">
                <asp:Image ID="IcoObservaciones" CssClass="Ico_msgGrilla" runat="server" />
            </div>
            <div class="msgGrilla_div2">
                <asp:Label ID="MensajeObservaciones" runat="server"></asp:Label>
            </div>
        </asp:Panel>

        <table class="form" cellpadding="0px" cellspacing="0px">
          <tr>
            <td class="col1">
            <asp:TextBox ID="observaciones" TextMode="multiline" Columns="50" Rows="5" MaxLength="2000" runat="server"></asp:TextBox><b id="caracteresProy">  2000</b> Caracteres 
                       Disponibles 
            <asp:RequiredFieldValidator id="RequiredFieldValidatorObservaciones" runat="server" ControlToValidate="observaciones"  ValidationGroup="grupo6"
                       ErrorMessage="Observaciones" Display="Static">*</asp:RequiredFieldValidator>
                       
            </td>
         </tr>
         <tr>
            <td class="col1"><asp:ImageButton ID="GuardarObservaciones" runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                    AlternateText="Guardar Observaciones" ToolTip="Guardar Observaciones" onclick="GuardarObservaciones_Click"  CausesValidation="true" ValidationGroup="grupo6"/>
            <span class="item">Guardar Observaciones</span>&nbsp;
            </td>
         </tr>
         
        </table>

   </fieldset> 
   </asp:Panel>   

   </ContentTemplate>
   </asp:UpdatePanel>

</asp:Content>
