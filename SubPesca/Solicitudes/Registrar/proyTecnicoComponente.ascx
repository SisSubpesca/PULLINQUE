<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="proyTecnicoComponente.ascx.cs" Inherits="SubPesca.Solicitudes.Registrar.proyTecnicoComponente" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="informacionSolicitud.ascx"                   tagname="informacionSolicitud"                 tagprefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

  
  <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnableScriptGlobalization="True"></asp:ToolkitScriptManager>

  <asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>
  <asp:HiddenField ID="IdProyectoTecnico" runat="server"></asp:HiddenField>



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
                <span id="titulo_modulo">Proyecto Técnico</span>
            </td>
        </tr>
    </table>

    <!-- Mensaje de error generico -->
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

    <hr style="width:100%;" />

    <!-- Error Especie Autorizada -->  
    <asp:UpdatePanel ID="UpdatePanel_MSG_EspecieAu" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel_MSG_EspecieAu"  Visible="true" runat="server">   
                <asp:ValidationSummary ID="ValidationSummary1" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />             
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <!-- Especie Autorizada -->  
    <asp:UpdatePanel ID="UpdatePanel_EspecieAutorizada" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="PanelEspecieAutorizada"  Visible="false" runat="server">

                <fieldset>
                <legend>Especies/Grupo Autorizado</legend>
                <br />

                <asp:panel ID="PanelFormularioIngresoEspecieAutorizada" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item"><asp:RadioButton ID="EspeciesRad" runat="server" AutoPostBack="true" GroupName="RadGrupoEspecie" Text="Especies" Checked="true" OnCheckedChanged="RadGrupoEspecieChecked"/>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                        <asp:UpdatePanel ID="UpdatePanelEspeciesAutorizada" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="EspecieAutorizada" AutoPostBack="true" runat="server" OnSelectedIndexChanged="EtapaPorEspecieAutorizada_OnSelectedIndexChanged" ValidationGroup="grupo1"></asp:DropDownList> 
                            </ContentTemplate>
                        </asp:UpdatePanel>

                             
                    </td>
                    <td class="col3"><span class="item"><asp:RadioButton ID="GrupoEspeciesRad" runat="server" AutoPostBack="true" GroupName="RadGrupoEspecie" Text="Grupo Autorizado"  OnCheckedChanged="RadGrupoEspecieChecked"/></td>
                    <td class="col3"><span class="item" style="visibility:hidden;">:</span></td>
                    <td class="col3"><asp:DropDownList ID="GrupoEspecieAutorizadas" AutoPostBack="true" runat="server" OnSelectedIndexChanged="EtapaPorEspecieAutorizada_OnSelectedIndexChanged"></asp:DropDownList></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="col1"><span class="item">Etapa de Cultivo</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                        
                        <asp:UpdatePanel ID="UpdatePaneEtapaCultivoAut" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:ListBox ID="EtapaDeCultivoAutorizadas" runat="server" Width="256px" SelectionMode="Multiple"></asp:ListBox>

                            <asp:RequiredFieldValidator id="RequiredFieldValidatorEtapaDeCultivoAutorizadas" runat="server" ControlToValidate="EtapaDeCultivoAutorizadas"  GroupName="grupo1" ErrorMessage="Etapa de Cultivo" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                           
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="EspecieAutorizada" EventName="SelectedIndexChanged" />
                        </Triggers>
                        </asp:UpdatePanel> 

                    </td>
                    <td class="col3" colspan="4">&nbsp;</td>
                </tr>
                <tr>
                       <td class="col1"><span class="item">Tipo de Cultivo</span></td>
                       <td class="col2"><span class="item">:</span></td>
                       <td class="col3">
                       
                       <asp:DropDownList ID="TipoCultivo" AutoPostBack="true" runat="server"></asp:DropDownList> *
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoCultivo" runat="server" ControlToValidate="TipoCultivo"  GroupName="grupo1" ErrorMessage="Tipo de Cultivo" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                       
                       </td>
                       <td class="col3" colspan="4">&nbsp;</td>
                       
                </tr>
                <tr>
                       <td class="col1"><span class="item">Tipo de Alimento</span></td>
                       <td class="col2"><span class="item">:</span></td>
                       <td class="col3">
                       
                       <asp:DropDownList ID="TipoAlimento" AutoPostBack="true" runat="server" onselectedindexchanged="TipoAlimento_SelectedIndexChanged"></asp:DropDownList> *
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoAlimento" runat="server" ControlToValidate="TipoAlimento"  GroupName="grupo1" ErrorMessage="Tipo de Alimento" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>

                       </td>
                       <td class="col3" colspan="4">&nbsp;</td>
                       
                </tr>
                
                <asp:Panel ID="PanelNombreOtroTipoAlimento"  Visible="false" runat="server">
                    <tr>
                           <td class="col1"><span class="item">Nombre Otro</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3"><asp:TextBox ID="NombreOtroTipoAlimento" runat="server"></asp:TextBox> *</td>
                           <td class="col3" colspan="4">&nbsp;</td>
                       
                    </tr>
                </asp:Panel>

                <tr>
                    <td colspan="7" class="item">
                            
                        <asp:ImageButton ID="GuardarEspecieAutorizada" runat="server" 
                            ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                            AlternateText="Agregar Especie Autorizada" ToolTip="Agregar Especie Autorizada" 
                            onclick="GuardarEspecieAutorizada_Click" CausesValidation="true"
                            ValidationGroup="grupo1" style="width: 20px" />
                        <asp:Label id="LABEL_EESPECIE_AUTORIZ" runat="server"> <span class="item">Agregar Especie/Grupo Autorizado</span> </asp:Label>

                    </td>
                </tr>
                </table>
                </asp:panel>


                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td>
                    <!-- Lista de Especies Autorizadas -->
                        <asp:GridView ID="GridEspecieAutorizadaProyTecnico"  
                        runat="server"
                        AutoGenerateColumns="False" 
                        CellPadding="4" 
                        ForeColor="#333333" 
                        GridLines="None"
                        AllowPaging="True" PageSize="20"
                        CssClass="mGrid"
                        OnRowDataBound="GridEspecieAutorizadaProyTecnico_RowDataBound"
                        PagerStyle-CssClass="pgr"
                        OnRowCommand="GridEspecieAutorizadaProyTecnico_RowCommand"
                        Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Especie">
                                <ItemTemplate>
                                     <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                     <%# DataBinder.Eval(Container, "DataItem.especie.descripcion")%>
                                     
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo de Cultivo">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.tipoCultivo.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo de Alimento">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.tipoAlimento.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Otro Tipo de Alimento">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.detalle")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Etapa de Cultivo">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.etapaCultivoString")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Grupo Autorizado">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.grupoEspecieAutoriz.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Grupo Informativo">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.grupoEspecie.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "index") %>'
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
                        &nbsp;</td>
                </tr>
                </table>
                

                </fieldset>

            </asp:Panel>
        </ContentTemplate>
       
    </asp:UpdatePanel>  
    
    <!-- Cultivo de Algas -->
    <asp:UpdatePanel ID="UpdatePanel_PanelCultivoAlgas" runat="server" UpdateMode="Conditional">
        <ContentTemplate>  
            <asp:Panel ID="PanelCultivoAlgas"  Visible="false" runat="server" style="text-align: left">
            
            <fieldset>
            <legend><span class="item">Cultivo de Algas</span></legend>
            
            <br />

            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td>
                   <span class="item">Tipo de Cultivo de Algas</span>
                </td>
                <td>
                    <span class="item"><asp:DropDownList ID="TipoCultivoAlgas" runat="server"></asp:DropDownList></span>
                </td>
                <td><span class="item">Utiliza Mangas Plásticas:</span>&nbsp;
                    <asp:RadioButton ID="RadioButtonListUtilizaMangasPlasticas1" runat="server" GroupName="MangasPlasticas" Text="Si" />
                    <asp:RadioButton ID="RadioButtonListUtilizaMangasPlasticas2" runat="server" GroupName="MangasPlasticas" Text="No" />
               </td>
            </tr>
            </table>
            </fieldset>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Error Estructura Técnica -->  
    <asp:UpdatePanel ID="UpdatePanel_MSG_EstructuraTecnica" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel3"  Visible="true" runat="server">   
                <asp:ValidationSummary ID="ValidationSummary2" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo2" />             
            </asp:Panel>
        </ContentTemplate>
   </asp:UpdatePanel>
   
    <!-- Estructura Técnica -->  
    <asp:UpdatePanel ID="UpdatePanel_EstructuraTecnica" runat="server" UpdateMode="Conditional">
        <ContentTemplate>  
            <asp:Panel ID="Panel_EstructuraTecnica"  Visible="false" runat="server">
            
                <fieldset>
                <legend>Estructuras técnicas a instalar cada año</legend>
                <br />

                <asp:HiddenField ID="IdEstructProyTecnico" runat="server" value="0" ></asp:HiddenField>
                <asp:HiddenField ID="IndexEstructProyTecnico" runat="server" value="0" ></asp:HiddenField>

                <asp:Panel ID="PanelFormularioIngresoEstructuraTecnica" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                
                <tr>
                    
                    <td class="col1">
                        <span class="item">Tipo Estructura</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                        
                        <asp:UpdatePanel ID="UpdatePanelTipoEstructura" UpdateMode="Conditional" runat="server">
                             <ContentTemplate>
                                <asp:DropDownList ID="TipoEstructura" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FormaPorEstructura_OnSelectedIndexChanged"></asp:DropDownList> 
                                <asp:RequiredFieldValidator id="RequiredFieldValidator_TipoEstructura" runat="server" ControlToValidate="TipoEstructura"  ValidationGroup="grupo2"
                                ErrorMessage="Tipo Estructura" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                             </ContentTemplate> 
                        </asp:UpdatePanel>
                       
                    </td>
                    <td class="col3">&nbsp;</td>
                    <td class="col3">&nbsp;</td>
                    <td class="col3">&nbsp;</td>
                    <td class="col3">&nbsp;</td>
                </tr>
               
                <tr>
                    
                    <td class="col1">
                        <span class="item">Forma Estructura</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                        <asp:UpdatePanel ID="UpdatePanelFormaEstructura" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="FormaEstructura" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ManejoCamposEstructuraMedidasChanged"></asp:DropDownList>
                                
                             </ContentTemplate>
                             <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="TipoEstructura" EventName="SelectedIndexChanged" />
                             </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="col3">&nbsp;</td>
                    <td class="col3">&nbsp;</td>
                    <td class="col3">&nbsp;</td>
                    <td class="col3">&nbsp;</td>
                </tr>
                <tr>
                    
                    <td class="col1">
                        <span class="item">Unidad de Medida</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                        
                        <asp:UpdatePanel ID="UpdatePanelUnidadMedida" UpdateMode="Conditional" runat="server">
                           <ContentTemplate>
                                <asp:DropDownList ID="UnidadDeMedida" AutoPostBack="false" runat="server"></asp:DropDownList>
                                
                           </ContentTemplate>
                           <Triggers>
                                   <asp:AsyncPostBackTrigger ControlID="TipoEstructura" EventName="SelectedIndexChanged" />
                           </Triggers>
                        </asp:UpdatePanel> 

                    </td>
                    <td class="col3">&nbsp;</td>
                    <td class="col3">&nbsp;</td>
                    <td class="col3">&nbsp;</td>
                    <td class="col3">&nbsp;</td>
                </tr>
                <tr>                  
                    <td colspan="7">
                        <asp:UpdatePanel ID="EstructMedidas_UpdatePanel" runat="server" 
                            UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="EstructMedidas_Panel" runat="server" Visible="true">
                                    <table cellpadding="0px" cellspacing="0px" class="form">
                                        <tr>
                                            <td class="col1">
                                                <span class="item">Largo (m)</span></td>
                                            <td class="col2">
                                                <span class="item">:</span></td>
                                            <td class="col3">
                                                <asp:TextBox ID="TextBoxLargoM" runat="server" AutoPostBack="true" 
                                                    onKeyUp="return onlyNumericoComa(this)" 
                                                    OnTextChanged="CalculoVolumenAutomaticoChange"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator_TextBoxLargoM" runat="server" 
                                                    ControlToValidate="TextBoxLargoM" 
                                                    ErrorMessage="Ingrese formato válido Ej: 12,3" Operator="DataTypeCheck" 
                                                    Type="Double" ValidationGroup="grupo2" />
                                            </td>
                                            <td class="col3">
                                                <span class="item">Ancho (m)</span></td>
                                            <td class="col2">
                                                <span class="item">:</span></td>
                                            <td class="col3">
                                                <asp:TextBox ID="TextBoxAnchoM" runat="server" AutoPostBack="true" 
                                                    onKeyUp="return onlyNumericoComa(this)" 
                                                    OnTextChanged="CalculoVolumenAutomaticoChange"></asp:TextBox>
                                                </span>
                                                <asp:CompareValidator ID="CompareValidator_TextBoxAnchoM" runat="server" 
                                                    ControlToValidate="TextBoxAnchoM" 
                                                    ErrorMessage="Ingrese formato válido Ej: 12,3" Operator="DataTypeCheck" 
                                                    Type="Double" ValidationGroup="grupo2" />
                                            </td>
                                            <td class="col3">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="col1">
                                                <span class="item">Alto (m)</span></td>
                                            <td class="col2">
                                                <span class="item">:</span></td>
                                            <td class="col3">
                                                <asp:TextBox ID="TextBoxAltoM" runat="server" AutoPostBack="true" 
                                                    onKeyUp="return onlyNumericoComa(this)" 
                                                    OnTextChanged="CalculoVolumenAutomaticoChange"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator_TextBoxAltoM" runat="server" 
                                                    ControlToValidate="TextBoxAltoM" ErrorMessage="Ingrese formato válido Ej: 12,3" 
                                                    Operator="DataTypeCheck" Type="Double" ValidationGroup="grupo2" />
                                            </td>
                                            <td class="col3">
                                                <span class="item">Diámetro (m)</span></td>
                                            <td class="col2">
                                                <span class="item">:</span></td>
                                            <td class="col3">
                                                <asp:TextBox ID="TextBoxDiametroM" runat="server" AutoPostBack="true" 
                                                    onKeyUp="return onlyNumericoComa(this)" 
                                                    OnTextChanged="CalculoVolumenAutomaticoChange"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator_TextBoxDiametroM" runat="server" 
                                                    ControlToValidate="TextBoxDiametroM" 
                                                    ErrorMessage="Ingrese formato válido Ej: 12,3" Operator="DataTypeCheck" 
                                                    Type="Double" ValidationGroup="grupo2" />
                                            </td>
                                            <td class="col3">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="col1">
                                                <span class="item">Volumen Unidad de Medida</span></td>
                                            <td class="col2">
                                                <span class="item">:</span></td>
                                            <td class="col3">
                                                <asp:DropDownList ID="VolumenUnidadMedida" runat="server" AutoPostBack="false">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="col3">
                                                &nbsp;</td>
                                            <td class="col3">
                                                &nbsp;</td>
                                            <td class="col3">
                                                &nbsp;</td>
                                            <td class="col3">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="col1">
                                                <span class="item">Volumen Valor de Medida</span></td>
                                            <td class="col2">
                                                <span class="item">:</span></td>
                                            <td class="col3">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="TextBoxVolumenValorMedida" runat="server" AutoPostBack="true" 
                                                            onKeyUp="return onlyNumericoComa(this)" 
                                                            OnTextChanged="CalculoDimensionAcumuladoChange"></asp:TextBox>
                                                        <asp:CompareValidator ID="CompareValidator_TextBoxVolumenValorMedida" 
                                                            runat="server" ControlToValidate="TextBoxVolumenValorMedida" 
                                                            ErrorMessage="Ingrese formato válido Ej: 12,3" Operator="DataTypeCheck" 
                                                            Type="Double" ValidationGroup="grupo2" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TextBoxLargoM" EventName="TextChanged" />
                                                    </Triggers>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TextBoxAnchoM" EventName="TextChanged" />
                                                    </Triggers>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TextBoxAltoM" EventName="TextChanged" />
                                                    </Triggers>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TextBoxDiametroM" 
                                                            EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
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
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="col1">
                        <span class="item">Años</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3" colspan="5">
                        <asp:UpdatePanel ID="UpdatePanelAniosEstructura" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="Anio" AutoPostBack="true"  OnSelectedIndexChanged="TipoAnioEstructuraTecnica_Selected" runat="server"></asp:DropDownList>
                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                 </tr>
                 <tr>
                    <td colspan="7">
                        
                        <asp:UpdatePanel ID="UpdatePanelAnios" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <table>
                                <tr>
                                    <td class="col1"><span class="item">Año 1</span></td>
                                    <td class="col1"><span class="item"><asp:label Text="Año 2" runat="server" ID="labelAnioMaximo"></asp:label></span></td>
                                    <td class="col3"><div id="divAnio3" runat="server"><span class="item">Año 3</span></div></td>        
                                    <td class="col3"><div id="divAnio4" runat="server"><span class="item">Año 4</span></div></td>
                                    <td class="col3"><div id="divAnio5" runat="server"><span class="item">Año 5</span></div></td>      
                                </tr>
                               
                                <tr>
                                    <td class="col1">
                                        <span class="item">
                                        <asp:TextBox ID="TextBoxAnio1" runat="server" AutoPostBack="true" OnTextChanged="CalculoTotalAcumuladoChange" onKeyUp="return onlyNumeric(this)"  MaxLength="8"></asp:TextBox>
                                        </span></td>
                                    <td class="col1">
                                        <span class="item">
                                        <asp:TextBox ID="TextBoxAnio2" runat="server" AutoPostBack="true" OnTextChanged="CalculoTotalAcumuladoChange" onKeyUp="return onlyNumeric(this)"  MaxLength="8"></asp:TextBox>
                                        </span></td>
                                    <td class="col1">
                                        <span class="item">
                                        <asp:TextBox ID="TextBoxAnio3" runat="server" AutoPostBack="true" OnTextChanged="CalculoTotalAcumuladoChange" onKeyUp="return onlyNumeric(this)"  MaxLength="8"></asp:TextBox>
                                        </span></td>
                                    <td class="col3">
                                        <span class="item">
                                        <asp:TextBox ID="TextBoxAnio4" runat="server" AutoPostBack="true" OnTextChanged="CalculoTotalAcumuladoChange" onKeyUp="return onlyNumeric(this)"  MaxLength="8"></asp:TextBox>
                                        </span></td>
                                    <td class="col3">
                                        <span class="item">
                                        <asp:TextBox ID="TextBoxAnio5" runat="server" AutoPostBack="true" OnTextChanged="CalculoTotalAcumuladoChange" onKeyUp="return onlyNumeric(this)"  MaxLength="8"></asp:TextBox>
                                        </span></td>
                                </tr>
                                
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Anio" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                       
                        <td class="col1">
                            <span class="item">Densidad de la Siembra [Kg/m<sup>2</sup>]:</span></td>
                        <td class="col2">
                            :</td>
                        <td class="col3">
                            <asp:TextBox ID="DensidadSiembra" runat="server" 
                                onKeyUp="return onlyNumericoComa(this)"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator_DensidadSiembra" runat="server" 
                                ControlToValidate="DensidadSiembra" 
                                ErrorMessage="Ingrese formato válido Ej: 12,3" Operator="DataTypeCheck" 
                                Type="Double" ValidationGroup="grupo6" />
                        </td>
                        <td colspan="4">
                            &nbsp;</td>
                    </tr>
                <tr>
                       
                        <td class="col1">
                            <span class="item">Total Acumulado - Número</span></td>
                        <td class="col2">
                            <span class="item">:</span></td>
                        <td class="col3">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="TotalAcumuladoNumero" runat="server" AutoPostBack="true" 
                                        ReadOnly="true"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TextBoxAnio1" EventName="TextChanged" />
                                </Triggers>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TextBoxAnio2" EventName="TextChanged" />
                                </Triggers>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TextBoxAnio3" EventName="TextChanged" />
                                </Triggers>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TextBoxAnio4" EventName="TextChanged" />
                                </Triggers>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TextBoxAnio5" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td colspan="4">
                            &nbsp;</td>
                    </tr>
                <tr>
                    
                    <td class="col1">
                        <span class="item">Total Acumulado - Dimensión</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                        
                        <asp:UpdatePanel ID="UpdatePanel_TotalAcumuladoDimension" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="TotalAcumuladoDimension" ReadOnly="true" runat="server"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item">&nbsp;</td>
                    <td class="item" colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    
                    <td class="item" colspan="7" style="text-align: center">
                        <asp:UpdatePanel ID="UpdatePanelBotonesEstructuraTecnica" runat="server" 
                            UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="Panel_Agregar_EstructuraTecnica" runat="server" Visible="true">
                                    <asp:ImageButton ID="AgregarEstructuraTecnica" runat="server" 
                                        AlternateText="Guardar Estructura Técnica" CausesValidation="true" 
                                        Height="20px" ImageUrl="~/App_Themes/admin_style/images/add.png" 
                                        onclick="GuardarEstructuraTecnica_Click" style="width: 20px" 
                                        ToolTip="Guardar Estructura Técnica" ValidationGroup="grupo2" />
                                    <span class="item">Agregar Estructura Técnica</span>
                                    <asp:ImageButton ID="ImageButton3" runat="server" 
                                        AlternateText="Limpiar Estructura Técnica" CausesValidation="false" 
                                        Height="20px" ImageUrl="~/App_Themes/admin_style/images/clean.png" 
                                        onclick="LimpiarEstructuraTecnica_Click" ToolTip="Limpiar Estructura Técnica" />
                                    <span class="item">Limpiar Estructura Técnica</span>
                                </asp:Panel>
                                <asp:Panel ID="Panel_Modificar_EstructuraTecnica" runat="server" 
                                    Visible="false">
                                    <asp:ImageButton ID="ModificarEstructuraTecnica" runat="server" 
                                        AlternateText="Modificar Estructura Técnica" CausesValidation="true" 
                                        Height="20px" ImageUrl="~/App_Themes/admin_style/images/add.png" 
                                        onclick="ModificarEstructuraTecnica_Click" style="width: 20px" 
                                        ToolTip="Modificar Estructura Técnica" ValidationGroup="grupo2" />
                                    <span class="item">Modificar Estructura Técnica</span>
                                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                        AlternateText="Limpiar Estructura Técnica" CausesValidation="false" 
                                        Height="20px" ImageUrl="~/App_Themes/admin_style/images/clean.png" 
                                        onclick="LimpiarEstructuraTecnica_Click" style="width: 20px" 
                                        ToolTip="Limpiar Estructura Técnica" />
                                    <span class="item">Limpiar Estructura Técnica</span>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                </table>
                </asp:Panel>


                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td>
                    <!-- Lista de Estructuras Técnicas -->
                    <asp:GridView ID="GridEstructuraTecnicaProyTecnico"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" PageSize="10"
                       CssClass="mGrid"
                       OnRowDataBound="GridEstructuraTecnicaProyTecnico_RowDataBound"
                       PagerStyle-CssClass="pgr"
                       OnRowCommand="GridEstructuraTecnicaProyTecnico_RowCommand"
                       Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Tipo Estructura">
                                <ItemTemplate>
                                     <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                     <%# DataBinder.Eval(Container, "DataItem.tipoEstructura.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="Forma Estructura">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.formaEstructura.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unidad de medida">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.unidadMedida.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Largo">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.largo")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ancho">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.ancho")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Alto">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.alto")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Diámetro">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.diametro")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Años">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.aniosCad")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Densidad de Siembra [Kg/m2]">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.densidadSiembra")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idEstructPT") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idEstructPT") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
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
        </ContentTemplate>
    </asp:UpdatePanel>
      
    <!-- Estructura Técnica a Instalar cada Año (Colectores) -->
    <asp:UpdatePanel ID="UpdatePanelEstructuraColector" runat="server" UpdateMode="Conditional">
        <ContentTemplate>  
            <asp:Panel ID="PanelEstructuraColector"  Visible="false" runat="server" style="text-align: left">
            
            <fieldset>
            <legend>Estructura Técnica a Instalar cada Año (Colectores)</legend>
            
            <br />
            
            <asp:HiddenField id="idEstructuraProyectoTecnico" runat ="server"></asp:HiddenField>

            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
            <td class="col1"><span class="item">N° de Colectores</span></td>
            <td class="col2">:</td>
            <td class="col3">
                <asp:TextBox ID="NumeroColectores" onKeyUp="return onlyNumeric(this)" runat="server"></asp:TextBox>
              
            </td>
            </tr>
            <tr>
            <td class="col1"><span class="item">N° de Líneas</span></td>
            <td class="col2">:</td>
            <td class="col3">
                <asp:TextBox ID="NumeroLineasColectores" onKeyUp="return onlyNumeric(this)" runat="server"></asp:TextBox>
                             
            </td>
            </tr>
            </table>

            </fieldset>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Error Programa de Producción -->    
    <asp:UpdatePanel ID="UpdatePanel_MSG_ProgrProduccionPT" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel_MSG_ProgrProduccionPT"  Visible="true" runat="server">   
                <asp:ValidationSummary ID="ValidationSummary3" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo4" />             
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <!-- Programa de Producción -->
    <asp:UpdatePanel ID="UpdatePanel_ProgrProduccionPT" runat="server" UpdateMode="Conditional">
        <ContentTemplate>  
            <asp:Panel ID="Panel_ProgrProduccionPT"  Visible="false" runat="server">
            
            <fieldset>
            <legend>Programa de Producción</legend>
            <br />
            <asp:HiddenField ID="IdProgrProd_ProyTecnico" runat="server" value="0" ></asp:HiddenField>
            <asp:HiddenField ID="IndexProd_ProyTecnico" runat="server" value="0" ></asp:HiddenField>
            

            <asp:Panel ID="PanelFormularioIngresoProgramaProduccion" Visible="true" runat="server">

            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1" colspan="6"><span class="item"> Producción <b>máxima</b> anual proyectada por especie. 
                    Cuando se trate de modificaciones, señalar la producción del último año.</span></td>
            </tr>
                
            <tr>
                <td class="item"><span class="item"><br /><br />Especie o Grupo </span></td>
                <td class="item"><span class="item"><br /><br />Unidad</span></td>
                <td class="item"><span class="item"><br /><br />Peso promedio de ejemplares <br />(valor único o rango)</span></td>
                <td class="item"><span class="item"><br /><br />Producción último año</span></td>
                <td class="item"><span class="item"><br /><br />Años</span></td>
                <td class="item"><span class="item"><br /><br /></span></td>
            </tr>

            <tr>
                <td class="col3" style="text-align: center">
                    
                    <table>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel_EspecieProgramaProduccion" UpdateMode="Conditional" runat="server">
                                <ContentTemplate><asp:DropDownList ID="EspecieProgramaProduccion" AutoPostBack="true" runat="server"></asp:DropDownList></ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel_GrupoProgramaProduccion" UpdateMode="Conditional" runat="server">
                                <ContentTemplate><asp:DropDownList ID="GrupoProgramaProduccion" AutoPostBack="true" runat="server"></asp:DropDownList></ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    
                    </table>


                </td>

                <td class="col3">
                       <asp:DropDownList ID="UnidadProgramaProduccion" AutoPostBack="true" 
                           runat="server" style="text-align: left">
                       </asp:DropDownList>
                       <asp:RequiredFieldValidator id="RequiredFieldValidator_UnidadProgramaProduccion" runat="server" ControlToValidate="UnidadProgramaProduccion"  ValidationGroup="grupo4"
                                ErrorMessage="Unidad" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
                
                <td class="col3" style="width:170px;">
                        <table style="width:160px;">
                        <tr>
                            <td>
                                <asp:DropDownList ID="PesoPromedioEjemplares" AutoPostBack="true" OnSelectedIndexChanged="DespliegaPesoProm_OnSelectedIndexChanged" runat="server"></asp:DropDownList> 
                                <asp:RequiredFieldValidator id="RequiredFieldValidator_PesoPromedioEjemplares" runat="server" ControlToValidate="PesoPromedioEjemplares"  ValidationGroup="grupo4"
                                ErrorMessage="Peso promedio de ejemplares " Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>  
                            </td>
                        </tr>
                        <tr>
                          <asp:UpdatePanel ID="UpdatePanelPesoProm" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <td>Peso promedio (Kg.):<br/><asp:TextBox ID="PesoPromSinRango" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator_PesoPromSinRango" runat="server" Operator="DataTypeCheck" Type="Double" 
                                                ControlToValidate="PesoPromSinRango" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />

                                    
                                    </td>
                                </ContentTemplate>
                                <Triggers>
                                   <asp:AsyncPostBackTrigger ControlID="PesoPromedioEjemplares" EventName="SelectedIndexChanged" />
                                </Triggers>
                          </asp:UpdatePanel> 
                       </tr>
                       <tr>
                            <asp:UpdatePanel ID="UpdatePanelPesoPromR1" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <td>Peso mínimo Promedio (Kg.):<asp:TextBox ID="PesoRango1" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator_PesoRango1" runat="server" Operator="DataTypeCheck" Type="Double" 
                                                ControlToValidate="PesoRango1" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />
                                    </td>
                                </ContentTemplate>
                                <Triggers>
                                   <asp:AsyncPostBackTrigger ControlID="PesoPromedioEjemplares" EventName="SelectedIndexChanged" />
                                </Triggers>
                           </asp:UpdatePanel> 
                       </tr>
                       <tr>
                            <asp:UpdatePanel ID="UpdatePanelPesoPromR2" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <td>Peso máximo Promedio (Kg.):<asp:TextBox ID="PesoRango2" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px" ></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator_PesoRango2" runat="server" Operator="DataTypeCheck" Type="Double" 
                                                ControlToValidate="PesoRango2" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />
                                       
                                    </td>
                                </ContentTemplate>
                                <Triggers>
                                   <asp:AsyncPostBackTrigger ControlID="PesoPromedioEjemplares" EventName="SelectedIndexChanged" />
                                </Triggers>
                           </asp:UpdatePanel> 
                       </tr>
                      </table>  
                 </td>
                 
                <td class="col3" style="text-align: center;">
                   <table border="0">
                    <tr>
                        <td style="text-align: center;">
                            <asp:TextBox ID="ProdUltimoAnio" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator_ProdUltimoAnio" runat="server" ControlToValidate="ProdUltimoAnio"  ValidationGroup="grupo4" ErrorMessage="Producción ultimo año" Display="Static">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align: center;">
                            &nbsp;</td>
                     </tr>
                     <tr>
                        <td>
                            <asp:CompareValidator ID="CompareValidator_ProdUltimoAnio" runat="server" Operator="DataTypeCheck" Type="Double"  ControlToValidate="ProdUltimoAnio" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />
                        </td>
                         <td>
                             &nbsp;</td>
                    </tr>
                    </table>
                   
                </td>
                 
                <td class="col3" style="text-align: center;">
                     <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="AnioProgramaProducc" AutoPostBack="true"  OnSelectedIndexChanged="TipoAnioProgramaProduccion_Selected" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorAnioProgramaProducc" runat="server" ControlToValidate="AnioProgramaProducc"  ValidationGroup="grupo4"
                                ErrorMessage="Año" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>      
                            </ContentTemplate>
                        </asp:UpdatePanel>
                </td>
                 
                <td class="col3" style="text-align: center">
                    <table width="100%">
                    <tr>
                    <td class="col3">
                        <div id="divAnio1ProgProd" runat="server"><span class="item">Año 1</span></div>
                        <asp:TextBox ID="AnioProd1" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator_AnioProd1" runat="server" ControlToValidate="AnioProd1"  ValidationGroup="grupo4" ErrorMessage="Año 1" Display="Static">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator_AnioProd1" runat="server" Operator="DataTypeCheck" Type="Double" ControlToValidate="AnioProd1" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />
                    </td>
                    </tr>
                    <tr>
                    <td class="col3">
                        <div id="divAnio2ProgProd" runat="server"><span class="item"><asp:label Text="Año 2" runat="server" ID="Anio2ProgProd"></asp:label></span></div>
                        <asp:TextBox ID="AnioProd2" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator_AnioProd2" runat="server" ControlToValidate="AnioProd2"  ValidationGroup="grupo4" ErrorMessage="Año 2" Display="Static">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator_AnioProd2" runat="server" Operator="DataTypeCheck" Type="Double" ControlToValidate="AnioProd2" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />

                    </td>
                    </tr>
                    <tr>
                    <td class="col3">
                        <div id="divAnio3ProgProd" runat="server"><span class="item">Año 3</span></div>
                        <asp:TextBox ID="AnioProd3" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator_AnioProd3" runat="server" Operator="DataTypeCheck" Type="Double" ControlToValidate="AnioProd3" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />
                    </td>
                    </tr>
                    <tr>
                    <td class="col3">
                        <div id="divAnio4ProgProd" runat="server"><span class="item">Año 4</span></div>
                        <asp:TextBox ID="AnioProd4" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator_AnioProd4" runat="server" Operator="DataTypeCheck" Type="Double" ControlToValidate="AnioProd4" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />
                    </td>
                    </tr>
                    <tr>
                    <td class="col3">
                        <div id="divAnio5ProgProd" runat="server"><span class="item">Año 5</span></div>
                        <asp:TextBox ID="AnioProd5" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator_AnioProd5" runat="server" Operator="DataTypeCheck" Type="Double" ControlToValidate="AnioProd5" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />
                    </td>
                    </tr>
                    </table>

                </td>
                   
            </tr>
                
            <tr>
                <td class="item" colspan="6">&nbsp;</td>
            </tr>


            <tr>
                <td class="item" colspan="6" style="text-align: center">
                    <asp:updatepanel runat="server" ID="UpdatePanelBotonesProgrProd" UpdateMode="Conditional">
                        <ContentTemplate>  
                        <asp:Panel ID="Panel_AgregarProgrProd" runat="server" Visible="true">
                            <asp:ImageButton ID="AgregarProgrProd" runat="server" 
                                AlternateText="Guardar Programa de Producción" Height="20px" 
                                ImageUrl="~/App_Themes/admin_style/images/add.png" 
                                onclick="GuardarProgramaProd_Click" style="width: 20px" ToolTip="Guardar Programa de Producción" 
                                CausesValidation="true" ValidationGroup="grupo4" />
                            <span class="item">Agregar Programa de Producción</span>
                                <asp:ImageButton ID="LimpiarProgrProd" runat="server" 
                                    ImageUrl="~/App_Themes/admin_style/images/clean.png" Height="20px" 
                                    AlternateText="Limpiar Programa de Producción" ToolTip="Limpiar Programa de Producción" 
                                    onclick="LimpiarProgramaProd_Click" CausesValidation="false" style="width: 20px" />
                            <span class="item"> Limpiar Programa de Producción</span>
                        </asp:Panel>
                        <asp:Panel ID="Panel_ModificarProgrProd" runat="server" Visible="false">
                            <asp:ImageButton ID="ModificarProgrProd" runat="server" 
                                    ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                    AlternateText="Modificar Programa de Producción" ToolTip="Modificar Programa de Producción" 
                                    onclick="ModificarProgramaProd_Click" CausesValidation="true" 
                                    ValidationGroup="grupo4" style="width: 20px" />
                            <span class="item">Modificar Programa de Producción</span>
                                <asp:ImageButton ID="ImageButton2" runat="server" 
                                    ImageUrl="~/App_Themes/admin_style/images/clean.png" Height="20px" 
                                    AlternateText="Limpiar Programa de Producción" ToolTip="Limpiar Programa de Producción" 
                                    onclick="LimpiarProgramaProd_Click" CausesValidation="false" style="width: 20px" />
                            <span class="item"> Limpiar Programa de Producción</span>
                        </asp:Panel>
                           
                    </ContentTemplate>
                    </asp:updatepanel>
                   
                      
                </td>
            </tr>
            </table>


            </asp:Panel>

            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td>
                <!-- Lista de Programa Produccion -->
                <asp:GridView ID="GridProgrProdProyTecnico"  
                    runat="server"
                    AutoGenerateColumns="False" 
                    CellPadding="4" 
                    ForeColor="#333333" 
                    GridLines="None"
                    AllowPaging="True" PageSize="10"
                    CssClass="mGrid"
                    OnRowDataBound="GridProgrProdProyTecnico_RowDataBound"
                    PagerStyle-CssClass="pgr"
                    OnRowCommand="GridProgrProdProyTecnico_RowCommand"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Especie">
                            <ItemTemplate>
                                <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                    <%# DataBinder.Eval(Container, "DataItem.especie.descripcion")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grupo Especie">
                            <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.grupo.descripcion")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Unidad">
                            <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.tipoUnidProgramaProd.descripcion")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Peso Prom.">
                            <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.tipoPesoPromEjemplares.descripcion")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prod. ultimo Año">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.produccionUltimoAnio")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Años">
                            <ItemTemplate>
                                 <%# DataBinder.Eval(Container, "DataItem.aniosCadProgrProd")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                            <ItemTemplate>
                                <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idProgrProduccion") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                                <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idProgrProduccion")+ ";" + DataBinder.Eval(Container.DataItem, "index") %>'
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
            </ContentTemplate>
    </asp:UpdatePanel>
    
    <!-- Error Fecha Solicitada por el Titular (Colectores) -->  
    <asp:UpdatePanel ID="UpdatePanel_MSG_FechaSolicitadaColector" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel_MSG_FechaSolicitadaColector"  Visible="true" runat="server">   
                <asp:ValidationSummary ID="ValidationSummary4" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo10" />             
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
   
    <!-- Fecha Solicitada por el Titular (Colectores) -->
    <asp:UpdatePanel ID="UpdatePanelFechaSolicitadaTitular" runat="server" UpdateMode="Conditional">
        <ContentTemplate>  
            <asp:Panel ID="PanelFechaSolicitadaTitular"  Visible="false" runat="server" style="text-align: left">
            
            <fieldset>
            <legend>Fecha Solicitada por el Titular (Colectores)</legend>
            
            <br />

            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
            <td class="col1"><span class="item">Fecha de Inicio</span></td>
            <td class="col2">:</td>
            <td class="col3" colspan="5">
             <asp:UpdatePanel ID="UpdatePanelFechaDesde" UpdateMode="Conditional" runat="server">
                   <ContentTemplate>
                   <div class="calendario">
                   <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaRecepcion" Width="120px" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtenderFechaRecepcion" runat="server"  Enabled="True"
                        Format="dd'/'MM'/'yyyy'"  TargetControlID="FechaRecepcion" PopupButtonID="endCalFechaRecepcion"  CssClass="cal_Theme1" FirstDayOfWeek="Monday"/>
                        <img runat="server" id="endCalFechaRecepcion" alt ="CalendarioFechaRecepcion" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                       
                        <asp:RegularExpressionValidator 
                         ID="RegularExpressionValidatorFechaRecepcion" 
                         runat="server"
                         ControlToValidate="FechaRecepcion"
                         ForeColor="Red"
                         ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"
                         ErrorMessage="Ingrese formato válido"
                         ValidationGroup="grupo1"> </asp:RegularExpressionValidator>
                    </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>  
            </td>
            </tr>


            <tr>
            <td class="col1"><span class="item">Fecha de Término</span></td>
            <td class="col2">:</td>
            <td class="col3" colspan="5">
               <asp:UpdatePanel ID="UpdatePanelFechaIngresoTramite" UpdateMode="Conditional" runat="server">
                   <ContentTemplate>
                   <div class="calendario">
                   <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaIngresoTramite" Width="120px" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtenderFechaIngresoTramite" runat="server"  Enabled="True"
                        Format="dd'/'MM'/'yyyy'"  TargetControlID="FechaIngresoTramite" PopupButtonID="endCalFechaIngresoTramite"  CssClass="cal_Theme1" FirstDayOfWeek="Monday"/>
                        <img runat="server" id="endCalFechaIngresoTramite" alt ="CalendarioFechaIngresoTramite" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                       
                        <asp:RegularExpressionValidator 
                         ID="RegularExpressionValidatorFechaIngresoTramite" 
                         runat="server"
                         ControlToValidate="FechaIngresoTramite"
                         ForeColor="Red"
                         ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"
                         ErrorMessage="Ingrese formato válido"
                         ValidationGroup="grupo1"> </asp:RegularExpressionValidator>
                    </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>  
            
            </td> 
            </tr>
            </table>

            </fieldset>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Observaciones -->
    <asp:updatepanel  ID="UpdatePanelObservacionesProyectoTecnico" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="PanelObservacionesProyectoTecnico" runat="server" Visible="false">

                <fieldset>
                <legend>Observaciones Proyecto Técnico</legend>
                <br />

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><asp:TextBox ID="observaciones" TextMode="multiline" Columns="50" Rows="5" MaxLength="2000" runat="server"></asp:TextBox><b id="caracteresProy">  2000</b> Caracteres Disponibles</td>
                    <td class="col2" valign="bottom">&nbsp;</td>
                </tr>
                </table>
                </fieldset>
                
            </asp:Panel>
        </ContentTemplate>
    </asp:updatepanel>

    <!-- Botones guardar y modificar -->
    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col2">&nbsp;</td>
    </tr>
    <tr>
        <td class="col2">
            <asp:UpdatePanel ID="BotonesProyTecnico" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    
                    <asp:Panel ID="GuardarProyTecnico" runat="server" Visible="true">
                    <tr>
                        <td class="col2">&nbsp;</td>
                        <td class="col1"><asp:Button ID="Guardar" runat="server" Text="Guardar Proyecto Técnico"  CausesValidation="false" onclick="Guardar_ProyTecnico_Click"/></td>
                        <td class="col2">&nbsp;</td>
                        </tr>
                    </asp:Panel>

                    <asp:Panel ID="ModificarProyTecnico" runat="server" Visible="false">
                    <tr>
                        <td class="col2">&nbsp;</td>
                        <td class="col1"><asp:Button ID="Modificar" runat="server" 
                                Text="Modificar Proyecto Técnico"  CausesValidation="false" 
                                onclick="Modificar_ProyTecnico_Click" Height="26px"/></td>
                        <td class="col2">&nbsp;</td>
                    </tr>
                    </asp:Panel>
         
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    </table>
           
    <!-- Error Archivo Adjunto -->  
    <asp:UpdatePanel ID="UpdatePanel_MSG_ArchivoAdjunto" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel_MSG_ArchivoAdjunto"  Visible="true" runat="server">   
                <asp:ValidationSummary ID="ValidationSummary5" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupoArchivo" />             
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Archivo Adjunto -->
    <asp:UpdatePanel ID="UpdatePanel_ArchivoAdjunto" runat="server" UpdateMode="Conditional">
    <ContentTemplate>  
    <asp:Panel ID="Panel_ArchivoAdjunto"  Visible="false" runat="server">
    <fieldset>
                        <legend>Archivos Adjuntos</legend>
                        <br /> 
                    
                       <asp:panel ID="PanelFormularioIngresoArchivoAdjunto" runat="server">
                       <table class="form" cellpadding="0px" cellspacing="0px">
                    
                       <tr>
                       <td class="col1"><span class="item">Tipo Archivo</span></td>
                       <td class="col2"><span class="item">:</span></td>
                       <td class="col3">
                   
                       <asp:DropDownList ID="TipoArchivo" AutoPostBack="false" runat="server"></asp:DropDownList>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoArchivoAntTerreno" runat="server" ControlToValidate="TipoArchivo" ValidationGroup="grupoArchivo" 
                       ErrorMessage="Tipo Archivo" InitialValue="-1" Display="Static" >*</asp:RequiredFieldValidator>

                       </td>
                        </tr>
                        <tr>
                           <td class="col1"><span class="item">Nombre Archivo</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3">

                           <asp:TextBox ID="NombreArchivo" MaxLength="40" Width="200px" runat="server"></asp:TextBox>
                           <asp:RequiredFieldValidator id="RequiredFieldValidatorNombreArchivoAntTerreno" runat="server" ControlToValidate="NombreArchivo" ValidationGroup="grupoArchivo" 
                           ErrorMessage="Nombre Archivo" Display="Static" >*</asp:RequiredFieldValidator>

                           </td>
                        </tr>

                        <tr>
                           <td class="col1"><span class="item">N° Control Ingreso</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3">
                           <asp:TextBox ID="NumeroCI" runat="server"></asp:TextBox>
                           <asp:RequiredFieldValidator id="RequiredFieldValidatorNumeroCI" runat="server" ControlToValidate="NumeroCI" ErrorMessage="Número CI" Display="Static" ValidationGroup="grupoArchivo">*</asp:RequiredFieldValidator>

                           </td>
                        </tr>

                        <tr>
                           <td class="col1"><span class="item">Fecha Control Ingreso</span></td>
                           <td class="col2">&nbsp;</td>
                           <td class="col3">
                            <asp:UpdatePanel ID="UpdatePanel_FechaRecepcion" UpdateMode="Conditional" runat="server">
                   <ContentTemplate>
                   <div class="calendario">
                   <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaTextRecepcion" Width="120px" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Enabled="True"
                        Format="dd'/'MM'/'yyyy'"  TargetControlID="FechaTextRecepcion" PopupButtonID="endCal1"  CssClass="cal_Theme1" FirstDayOfWeek="Monday"/>
                        <img runat="server" id="endCal1" alt ="CalendarioRecepcion" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                       
                        <asp:RegularExpressionValidator 
                         ID="RegularExpressionValidatorFechaTextRecepcion" 
                         runat="server"
                         ControlToValidate="FechaTextRecepcion"
                         ForeColor="Red"
                         ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"
                         ErrorMessage="Ingrese formato válido"
                         ValidationGroup="grupoArchivo"> </asp:RegularExpressionValidator>
                    </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>  
                               
                           </td>
                        </tr>

                        <tr>
                           <td class="col1"><span class="item">Archivo Adjunto</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3">
                           
                           <asp:UpdatePanel ID="UpdatePanelArchivoAdjunto" UpdateMode="Conditional" runat="server">
                           <ContentTemplate>

                           <asp:FileUpload ID="ArchivoAdjunto" MaxLength="40" Width="200px" runat="server" />

                           <asp:RequiredFieldValidator id="RequiredFieldValidatorArchivoAdjunto" runat="server" ControlToValidate="ArchivoAdjunto" ValidationGroup="grupoArchivo" 
                           ErrorMessage="Archivo Adjunto" Display="Static" InitialValue="0">*</asp:RequiredFieldValidator>

                           <asp:RegularExpressionValidator ID="RegularExpressionValidatorArchivoAdjunto" runat="server" ErrorMessage="Formato Archivo Incorrecto" ControlToValidate="ArchivoAdjunto" ValidationExpression= "(.*).(.doc|.DOC|.pdf|.PDF|.docx|.DOCX|.xls|.xlsx|.XLS|.XLSX|.dwg|.DWG|.dxf|.DXF|.jpg|.JPG|.jpeg|.JPEG)$" ValidationGroup="grupoArchivo" />

                           </ContentTemplate>
                           <Triggers>

                                <asp:PostBackTrigger ControlID="GuardarArchivoAdjunto" />
                              
                               
                           </Triggers>
                           </asp:UpdatePanel>

                           </td>
                        </tr>

                        <tr>
                            <td class="col1"></td>
                            <td class="col2"></td>
                            <td class="col3">
                                <asp:ImageButton ID="GuardarArchivoAdjunto" runat="server" 
                                    ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                    AlternateText="Guardar Archivo Adjunto" ToolTip="Guardar Archivo Adjunto" 
                                    CausesValidation="true" ValidationGroup="grupoArchivo" 
                                    onclick="GuardarArchivoAdjunto_Click"/>
                                <span class="item">Guardar Archivo Adjunto</span>

                                
                            </td>
                        </tr>
                        </table>
                       </asp:panel>                        
                   




                       </fieldset>
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>



        
                       <asp:UpdatePanel ID="UpdatePanelArchivoAdjuntoGrilla" UpdateMode="Conditional" runat="server">
                       <ContentTemplate>
                        
                           <asp:Panel ID="PanelArchivoAdj"  Visible="true" runat="server">
                            
                            <asp:GridView  ID="GridArchivoAdjunto" 
                            runat="server" 
                            AutoGenerateColumns="False"
                            DataKeyNames="idArchivoBin"  
                            CellPadding="4" 
                            ForeColor="#333333"
                            GridLines="None"
                            AllowPaging="False"
                            AllowSorting="false" 
                            CssClass="mGrid"
                            PagerStyle-CssClass="pgr" 
                            width="100%" 
                            PageIndex= "1"
                            OnRowDataBound="GridArchivoAdjunto_RowDataBound"
                            OnRowCommand="GridArchivoAdjunto_RowCommand"
                            >
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
                                <asp:TemplateField HeaderText="Numero CI">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.numCI")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha CI">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.fechaCIString")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Archivo Adjunto">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.archivoBinario.nombreFisico")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.estadoVigencia.descripcion")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 
                                <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                    <ItemTemplate>

                                       <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                 
                                        <asp:ImageButton ID="gDesasociar" Visible="false" runat="server" CausesValidation="false" CommandName="Desasociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBin") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                            ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Pasar a Vigente" ToolTip="Pasar a Vigente" />
                    
                                        <asp:ImageButton ID="gAsociar" Visible="false" runat="server" CausesValidation="false" CommandName="Asociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBin") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                            ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Pasar a No Vigente" ToolTip="Pasar a No Vigente" />

                                        <asp:ImageButton ID="gDescargar" Visible="true" runat="server" CausesValidation="false" CommandName="Descargar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBin") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" OnPreRender="ImgAdd_PreRender" />

                                        <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBin") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar"  />

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
                            <asp:Button ID="ExportarGrilla" runat="server" CausesValidation="false" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click"  Visible="false" />  
                            
                       </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="ExportarGrilla" />
                        </Triggers>   
                       </asp:UpdatePanel>


