<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesModificacion.Master" AutoEventWireup="true" CodeBehind="proyTecnico.aspx.cs" 
Inherits="SubPesca.Solicitudes.Modificacion.proyectoTecnico" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register src="~/Solicitudes/Registrar/informacionSolicitud.ascx"                   tagname="informacionSolicitud"                 tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>

    <style type="text/css">
        .item
        {
            text-align: center;
        }
        .style2
        {
            width: 51px;
            text-align: center;
        }
        .col3
        {
            text-align: center;
        }
        .style4
        {
            text-align: left;
            width: 219px;
        }
        .style5
        {
            text-align: left;
        }
        .style6
        {
            text-align: center;
            width: 218px;
        }
        .style7
        {
            line-height: 200%;
        }
    </style>

</asp:Content>


 
 

<asp:Content ID="FormularioProyectoTecnico" ContentPlaceHolderID="rightbody" runat="server">

  <asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>
  <asp:HiddenField ID="IdProyectoTecnico" runat="server"></asp:HiddenField>

  <asp:ToolkitScriptManager ID="ToolkitScriptManagerProyectoTecnico" runat="server"></asp:ToolkitScriptManager>

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

        <!-- Forma de Cultivo -->
        <asp:UpdatePanel ID="UpdatePanelFormaCultivo" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 
    <asp:Panel ID="PanelFormaCultivo" Visible="false" runat="server">

    <fieldset>
        <legend>Forma de Cultivo</legend>
        <br />

        <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                   <td class="col1"><span class="item">Tipo de Cultivo</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                      
                      <asp:DropDownList ID="TipoCultivo" AutoPostBack="true" runat="server" OnSelectedIndexChanged="AlimentoPorTC_OnSelectedIndexChanged"></asp:DropDownList> *
                      
                   </td>
                   <td class="style4">
                       &nbsp;</td>
                   <td class="col3">
                       &nbsp;</td>
                </tr>
        </table>
                
                <asp:UpdatePanel ID="UpdatePanel_TipoAlimento" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                      <asp:Panel ID="PanelTipoAlimento"  runat="server">
                      <table class="form" cellpadding="0px" cellspacing="0px">
                      <tr>
                       <td class="col1"><span class="item">Tipo de Alimento</span></td>
                       <td class="col2"><span class="item">:</span></td>
                       <td class="col3">
                            <asp:CheckBox ID="AlimentoAlgaFresca" runat="server" OnCheckedChanged="AlgaChecked" AutoPostBack="true"/>
                            Alga Fresca
                            <asp:CheckBox ID="AlimentoPellet" runat="server"/>
                            Pellet
                            <asp:CheckBox ID="AlimentoOtro" runat="server" OnCheckedChanged="TipoAlimentoChecked" AutoPostBack="true" />
                            Otro
                            
                           <asp:UpdatePanel ID="UpdatePanelNombreAlimentoOtro" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                     <asp:Panel ID="Panel_Alimento_otro"  runat="server">
                                        <asp:TextBox ID="NombreAlimentoOtro" AutoPostBack="false" runat="server" style="text-align: left;"></asp:TextBox>  
                                    </asp:Panel>
                                </ContentTemplate>
                               <Triggers>
                                   <asp:AsyncPostBackTrigger ControlID="AlimentoOtro" EventName="CheckedChanged" />
                               </Triggers>
                           </asp:UpdatePanel> 


                       </td>
                       <td class="style4">&nbsp;</td>   
                        </tr>  
                        </table>
                       </asp:Panel>   
                    </ContentTemplate>
                    <Triggers>
                       <asp:AsyncPostBackTrigger ControlID="TipoCultivo" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                   
   </fieldset>            
   </asp:Panel>
   </ContentTemplate>
   </asp:UpdatePanel>             
        
        <asp:UpdatePanel ID="UpdatePanel_MSG_EspecieAu" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="Panel_MSG_EspecieAu"  Visible="true" runat="server">   
                    <asp:ValidationSummary ID="ValidationSummary1" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />             
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        
       <!-- Especies Autorizadas -->
       <asp:UpdatePanel ID="UpdatePanel_EspecieAutorizada" runat="server" UpdateMode="Conditional">

    <ContentTemplate>

        <asp:Panel ID="Panel_EspecieAutorizada"  Visible="false" runat="server">

        <fieldset>
        <legend>Especies Autorizadas</legend>
        <br />

         <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>

                   <td class="col1"><span class="item">
                     <asp:RadioButton ID="EspeciesRad" runat="server" AutoPostBack="true" GroupName="RadGrupoEspecie" Text="Especies" OnCheckedChanged="RadGrupoEspecieChecked"/>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                   <asp:UpdatePanel ID="UpdatePanelEspeciesAutorizada" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="EspecieAutorizada" AutoPostBack="true" runat="server" OnSelectedIndexChanged="EtapaPorEspecieAutorizada_OnSelectedIndexChanged" ValidationGroup="grupo2"></asp:DropDownList> 
                            </ContentTemplate>
                    </asp:UpdatePanel>

                             
                    </td>
                   <td class="col3"><span class="item">
                       <asp:RadioButton ID="GrupoEspeciesRad" runat="server" AutoPostBack="true" GroupName="RadGrupoEspecie" Text="Grupo Especies"  OnCheckedChanged="RadGrupoEspecieChecked"/>
                   </td>
                   <td class="col3"><span class="item">:</span></td>
                   <td class="col3">
                       <asp:DropDownList ID="GrupoEspecieAutorizadas" AutoPostBack="true" runat="server" OnSelectedIndexChanged="EtapaPorEspecieAutorizada_OnSelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">Etapa de Cultivo</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                        <asp:UpdatePanel ID="UpdatePaneEtapaCultivoAut" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="EtapaDeCultivoAutorizadas" AutoPostBack="false" runat="server"></asp:DropDownList> *
                            </ContentTemplate>
                            <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="EspecieAutorizada" EventName="SelectedIndexChanged" />
                               <asp:AsyncPostBackTrigger ControlID="GrupoEspecieAutorizadas" EventName="SelectedIndexChanged" />
                           </Triggers>
                        </asp:UpdatePanel>
                       
                       
                    </td>
                   <td class="col3" colspan="4">&nbsp;</td>
                </tr>
                <tr>
                   <td colspan="7" class="item">
                            
                           <asp:ImageButton ID="GuardarEspecieAutorizada" runat="server" 
                                ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                AlternateText="Agregar Especie Autorizada" ToolTip="Agregar Especie Autorizada" 
                                onclick="GuardarEspecieAutorizada_Click" CausesValidation="true"
                                ValidationGroup="grupo1" style="width: 20px" />
                           <asp:Label id="LABEL_EESPECIE_AUTORIZ" runat="server"> <span class="item">Agregar Especie Autorizada</span> </asp:Label>
                            

                    </td>
                </tr>
                <tr>
                    <td colspan="7">
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
                            <asp:TemplateField HeaderText="Etapa de Desarrollo">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.etapaCultivo.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gEliminar" Visible="true" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "index") %>'
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
         <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                   <td class="col1"><span class="item">Tipo Estructura</span></td>
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
                   <td class="col1"><span class="item">Forma Estructura</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                     <asp:UpdatePanel ID="UpdatePanelFormaEstructura" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="FormaEstructura" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ManejoCamposEstructuraMedidasChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator id="RequiredFieldValidator_FormaEstructura" runat="server" ControlToValidate="FormaEstructura"  ValidationGroup="grupo2"
                                ErrorMessage="Forma Estructura" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>   
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
                   <td class="col1"><span class="item">Unidad de Medida</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                     <asp:UpdatePanel ID="UpdatePanelUnidadMedida" UpdateMode="Conditional" runat="server">
                           <ContentTemplate>
                                <asp:DropDownList ID="UnidadDeMedida" AutoPostBack="false" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator id="RequiredFieldValidator_UnidadDeMedida" runat="server" ControlToValidate="UnidadDeMedida"  ValidationGroup="grupo2"
                                ErrorMessage="Unidad de Medida" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>        
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
                        <asp:UpdatePanel ID="EstructMedidas_UpdatePanel" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                            <asp:Panel ID="EstructMedidas_Panel"  Visible="true" runat="server">
                                <table class="form" cellpadding="0px" cellspacing="0px">
                                <tr>

                                   <td class="col1"><span class="item">Largo (m)</span></td>
                                   <td class="col2"><span class="item">:</span></td>
                                   <td class="col3">
                                        <asp:TextBox ID="TextBoxLargoM" runat="server" AutoPostBack="true" OnTextChanged="CalculoVolumenAutomaticoChange" onKeyUp="return onlyNumericoComa(this)"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator_TextBoxLargoM" runat="server" Operator="DataTypeCheck" Type="Double" 
                                                ControlToValidate="TextBoxLargoM" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo2" />
                                   </td>
                                   <td class="col3"><span class="item">Ancho (m)</span></td>
                                   <td class="col2"><span class="item">:</span></td>
                                   <td class="col3">
                                        <asp:TextBox ID="TextBoxAnchoM" runat="server" AutoPostBack="true" OnTextChanged="CalculoVolumenAutomaticoChange" onKeyUp="return onlyNumericoComa(this)"></asp:TextBox></span>
                                        <asp:CompareValidator ID="CompareValidator_TextBoxAnchoM" runat="server" Operator="DataTypeCheck" Type="Double" 
                                                ControlToValidate="TextBoxAnchoM" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo2" />
                                   </td>
                                   <td class="col3">&nbsp;</td>
                                </tr>
                                <tr>
                                   <td class="col1"><span class="item">Alto (m)</span></td>
                                   <td class="col2"><span class="item">:</span></td>
                                   <td class="col3">
                                        <asp:TextBox ID="TextBoxAltoM" runat="server" AutoPostBack="true" OnTextChanged="CalculoVolumenAutomaticoChange" onKeyUp="return onlyNumericoComa(this)"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator_TextBoxAltoM" runat="server" Operator="DataTypeCheck" Type="Double" 
                                                ControlToValidate="TextBoxAltoM" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo2" />
                                    </td>
                                   <td class="col3"><span class="item">Diámetro (m)</span></td>
                                   <td class="col2"><span class="item">:</span></td>
                                   <td class="col3">
                                        <asp:TextBox ID="TextBoxDiametroM" runat="server" AutoPostBack="true" OnTextChanged="CalculoVolumenAutomaticoChange" onKeyUp="return onlyNumericoComa(this)"></asp:TextBox>
                                         <asp:CompareValidator ID="CompareValidator_TextBoxDiametroM" runat="server" Operator="DataTypeCheck" Type="Double" 
                                                ControlToValidate="TextBoxDiametroM" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo2" />
                                   </td>
                                   <td class="col3">&nbsp;</td>
                                </tr>
                                <tr>
                                   <td class="col1"><span class="item">Volumen Unidad de Medida</span></td>
                                   <td class="col2"><span class="item">:</span></td>
                                   <td class="col3">
                                       <asp:DropDownList ID="VolumenUnidadMedida" AutoPostBack="false" runat="server"></asp:DropDownList>
                                    </td>
                                   <td class="col3">&nbsp;</td>
                                   <td class="col3">&nbsp;</td>
                                   <td class="col3">&nbsp;</td>
                                   <td class="col3">&nbsp;</td>
                                </tr>
                                <tr>
                                   <td class="col1"><span class="item">Volumen Valor de Medida</span></td>
                                   <td class="col2"><span class="item">:</span></td>
                                   <td class="col3">
                                      <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TextBoxVolumenValorMedida" runat="server" AutoPostBack="true" OnTextChanged="CalculoDimensionAcumuladoChange" onKeyUp="return onlyNumericoComa(this)"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator_TextBoxVolumenValorMedida" runat="server" Operator="DataTypeCheck" Type="Double" 
                                                ControlToValidate="TextBoxVolumenValorMedida" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo2" />
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
                                               <asp:AsyncPostBackTrigger ControlID="TextBoxDiametroM" EventName="TextChanged" />
                                        </Triggers>
                                      </asp:UpdatePanel>  
                                    </td>
                                   <td class="col3">&nbsp;</td>
                                   <td class="col3">&nbsp;</td>
                                   <td class="col3">&nbsp;</td>
                                   <td class="col3">&nbsp;</td>
                                </tr>
                                </table>  
                            </asp:Panel>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                
                </td>
                </tr>
                <tr>
                   <td class="col1" rowspan="2"><span class="item">Años</span></td>
                   <td class="col2" rowspan="2"><span class="item">:</span></td>
                   <td class="col3" rowspan="2">
                    <asp:UpdatePanel ID="UpdatePanelAniosEstructura" UpdateMode="Conditional" runat="server">
                       <ContentTemplate>
                            <asp:DropDownList ID="Anio" AutoPostBack="true"  OnSelectedIndexChanged="TipoAnioEstructuraTecnica_Selected" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator_Anio" runat="server" ControlToValidate="Anio"  ValidationGroup="grupo2"
                                ErrorMessage="Años" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>      
                       </ContentTemplate>
                    </asp:UpdatePanel>
                   </td>
                   <td colspan="4" rowspan="2">
                     <asp:UpdatePanel ID="UpdatePanelAnios" UpdateMode="Conditional" runat="server">
                         <ContentTemplate>
                         <table>
                         <tr>
                           <td class="col3"><span class="item">Año 1</span></td>
                           <td class="col3"><span class="item">Año 2</span></td>
                           <td class="col3"><span class="item">Año 3</span></td>        
                           <td class="col3"><span class="item">Año 4</span></td>   
                         </tr>
                         <tr>
                               <td class="col2">
                                    <span class="item">
                                    <asp:TextBox ID="TextBoxAnio1" runat="server" AutoPostBack="true" OnTextChanged="CalculoTotalAcumuladoChange" onKeyUp="return onlyNumeric(this)"  MaxLength="8"></asp:TextBox>
                                    </span></td>
                               <td class="col3">
                                    <span class="item">
                                    <asp:TextBox ID="TextBoxAnio2" runat="server" AutoPostBack="true" OnTextChanged="CalculoTotalAcumuladoChange" onKeyUp="return onlyNumeric(this)"  MaxLength="8"></asp:TextBox>
                                    </span></td>
                               <td class="col3">
                                    <span class="item">
                                    <asp:TextBox ID="TextBoxAnio3" runat="server" AutoPostBack="true" OnTextChanged="CalculoTotalAcumuladoChange" onKeyUp="return onlyNumeric(this)"  MaxLength="8"></asp:TextBox>
                                    </span></td>
                               <td class="col3">
                                    <span class="item">
                                    <asp:TextBox ID="TextBoxAnio4" runat="server" AutoPostBack="true" OnTextChanged="CalculoTotalAcumuladoChange" onKeyUp="return onlyNumeric(this)"  MaxLength="8"></asp:TextBox>
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
                <tr><td colspan="7" rowspan="2"></td></tr>
                <tr>
                   <td class="col1"><span class="item">Total Acumulado - Número</span></td>
                   <td class="col2"><span class="item">:</span></td>
                   <td class="col3">
                        
                        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                           <ContentTemplate><asp:TextBox ID="TotalAcumuladoNumero" AutoPostBack="true" ReadOnly="true" runat="server"></asp:TextBox> </ContentTemplate>
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
                          
                        </asp:UpdatePanel>
                    </td>
                   <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                   <td class="col1"><span class="item">Total Acumulado - Dimensión</span></td>
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
                <tr><td class="item" colspan="7">&nbsp;</td></tr>
                <tr>
                   <td class="item" colspan="7" style="text-align: center">
                     <asp:updatepanel runat="server" ID="UpdatePanelBotonesEstructuraTecnica" UpdateMode="Conditional">
                         <ContentTemplate>  
                           <asp:Panel ID="Panel_Agregar_EstructuraTecnica" runat="server" Visible="true">
                                <asp:ImageButton ID="AgregarEstructuraTecnica" runat="server" 
                                        ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                        AlternateText="Guardar Estructura Técnica" ToolTip="Guardar Estructura Técnica" 
                                        onclick="GuardarEstructuraTecnica_Click" CausesValidation="true" 
                                        ValidationGroup="grupo2" style="width: 20px" />
                                <span class="item">Agregar Estructura Técnica</span>
                                 <asp:ImageButton ID="ImageButton3" runat="server" 
                                        ImageUrl="~/App_Themes/admin_style/images/clean.png" Height="20px" 
                                        AlternateText="Limpiar Estructura Técnica" ToolTip="Limpiar Estructura Técnica" 
                                        onclick="LimpiarEstructuraTecnica_Click" CausesValidation="false" 
                                        style="width: 20px" />
                                <span class="item">Limpiar Estructura Técnica</span>
                            </asp:Panel>
                            <asp:Panel ID="Panel_Modificar_EstructuraTecnica" runat="server" Visible="false">
                                <asp:ImageButton ID="ModificarEstructuraTecnica" runat="server" 
                                        ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                        AlternateText="Modificar Estructura Técnica" ToolTip="Modificar Estructura Técnica" 
                                        onclick="ModificarEstructuraTecnica_Click" CausesValidation="true" 
                                        ValidationGroup="grupo2" style="width: 20px" />
                                <span class="item">Modificar Estructura Técnica</span>
                                 <asp:ImageButton ID="ImageButton1" runat="server" 
                                        ImageUrl="~/App_Themes/admin_style/images/clean.png" Height="20px" 
                                        AlternateText="Limpiar Estructura Técnica" ToolTip="Limpiar Estructura Técnica" 
                                        onclick="LimpiarEstructuraTecnica_Click" CausesValidation="false" 
                                        style="width: 20px" />
                                <span class="item">Limpiar Estructura Técnica</span>
                            </asp:Panel>
                        </ContentTemplate>
                      </asp:updatepanel>
                    </td>
                    
                </tr>
                <tr>
                    <td colspan="7">
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
                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idEstructPT") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                                    <asp:ImageButton ID="gEliminar" Visible="true" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idEstructPT") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
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
        
        <!-- Cultivo de Algas -->
        <asp:UpdatePanel ID="UpdatePanel_PanelCultivoAlgas" runat="server" UpdateMode="Conditional">
    <ContentTemplate>  
     <asp:Panel ID="PanelCultivoAlgas"  Visible="false" runat="server" 
            style="text-align: left">
        <fieldset>
        <legend>Cultivo de Algas</legend>
        <br />

         <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                   <td class="style5">
                            <p class="style7">
                                <asp:CheckBox ID="Algas_DirSustrato" runat="server"/>
                                Directo al sustrato<br />
                                <asp:CheckBox ID="Algas_IndirSustrato" runat="server"/>
                                Indirecto al sustrato<br />
                                <asp:CheckBox ID="Algas_Suspendido" runat="server"/>
                                Suspendido<br />
                                <asp:CheckBox ID="Algas_Estanque" runat="server"/>
                                Estanque<br />
                                <asp:CheckBox ID="Algas_Otro" OnCheckedChanged="AlgaOtroChecked" AutoPostBack="true" runat="server"/>
                                Otro
                                <asp:TextBox ID="AlgaOtroDef" runat="server" AutoPostBack="false" style="text-align: left;"></asp:TextBox>
                                
                                <br />

                                

                                <p>
                                </p>

                                <p>
                                </p>

                                <p>
                                </p>

                                <p>
                                </p>

                                <p>
                                </p>

                                <p>
                                </p>
                                <p>
                                </p>

                            </p>
                   </td>
                    <td colspan="2"><span class="item">Utiliza Mangas Plásticas:</span>&nbsp;
                        <asp:RadioButton ID="RadioButtonListUtilizaMangasPlasticas1" runat="server" GroupName="MangasPlasticas" Text="Si" />
                        <asp:RadioButton ID="RadioButtonListUtilizaMangasPlasticas2" runat="server" GroupName="MangasPlasticas" Text="No" />
                        <span class="item">
                        <br />
                        <br />
                        Densidad de la Siembra [Kg/m<sup>2</sup>]:</span>
                        <asp:TextBox ID="TextBoxDensidadSiembra" runat="server" onKeyUp="return onlyNumericoComa(this)"></asp:TextBox>  
                        <asp:CompareValidator ID="CompareValidator_TextBoxDensidadSiembra" runat="server" Operator="DataTypeCheck" Type="Double" 
                            ControlToValidate="TextBoxDensidadSiembra" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo6" />

                        <span class="item">
                        <br />
                        <br />
                        <br />
                        Tipo Fondo:<asp:CheckBox ID="TipoFondoDuro" runat="server" />
                        Roca (Duro)<asp:CheckBox ID="TipoFondoSemi" runat="server" />
                        Gravilla, Laja, Tertel, bolones (Semiduro)<br />
                        <asp:CheckBox ID="TipoFondoBlando" runat="server" />
                        Arena, Conchuela, Fango (Blando)<asp:CheckBox ID="TipoFondoOtro" 
                            OnCheckedChanged="FondoOtroChecked" AutoPostBack="true" runat="server" />
                        Otro<asp:TextBox ID="FondoOtroDef" runat="server" AutoPostBack="false" 
                            style="text-align: left;"></asp:TextBox>&nbsp;
                        <br /> 
                     </span></td>
                </tr>
                <tr>
                   <td>
                       <br />
                       &nbsp;<br/>
                       <br/>
                    </td>
                   <td class="style5" align="right">
                       <br/> </span>
                   </td>
                   <td class="col3">&nbsp;</td>
                </tr>
                </table>
        </fieldset>
        </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="AlimentoAlgaFresca" EventName="CheckedChanged" />
        </Triggers>
     </asp:UpdatePanel>
        
        
        <asp:UpdatePanel ID="UpdatePanel_MSG_ProgrProduccionPT" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel4"  Visible="true" runat="server">   
                <asp:ValidationSummary ID="ValidationSummary3" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo4" />             
            </asp:Panel>
        </ContentTemplate>
       </asp:UpdatePanel>
   
        <!-- Programa de Producción -->  
        <asp:UpdatePanel ID="UpdatePanel_ProgrProduccionPT" runat="server" UpdateMode="Conditional">
    <ContentTemplate>  
     <asp:Panel ID="Panel_ProgrProduccionPT"  Visible="true" runat="server">
        <fieldset>
        <legend>Programa de Producción</legend>
        <br />
        <asp:HiddenField ID="IdProgrProd_ProyTecnico" runat="server" value="0" ></asp:HiddenField>
        <asp:HiddenField ID="IndexProd_ProyTecnico" runat="server" value="0" ></asp:HiddenField>
         <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                   <td class="col1" colspan="7"><span class="item"> Producción <b>máxima</b> anual proyectada por especie. 
                       Cuando se trate de modificaciones, señalar la producción del último año.</span></td>
                </tr>
                
                <tr >
                   <td class="item"><span class="item"><br /><br />Especie (Grupo) y Etapa</span></td>
                   <td class="item"><span class="item"><br /><br />Unidad</span></td>
                   <td class="item"><span class="item"><br /><br />Peso promedio de ejemplares <br />(valor único o rango)</span></td>
                   <td class="item"><span class="item"><br /><br />Densidad</span></td>
                   <td class="item"><span class="item"><br /><br />Producción último año</span></td>
                   <td class="item"><span class="item"><br /><br />Años</span></td>
                </tr>

                <tr>
                    <td class="col3" style="text-align: center">
                       <table>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel_EspecieProgramaProduccion" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate><asp:DropDownList ID="EspecieProgramaProduccion" AutoPostBack="true" runat="server" OnSelectedIndexChanged="EtapaPorEspeciePP_OnSelectedIndexChanged"></asp:DropDownList></ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel_GrupoProgramaProduccion" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate><asp:DropDownList ID="GrupoProgramaProduccion" AutoPostBack="true" runat="server" OnSelectedIndexChanged="EtapaPorGrupoPP_OnSelectedIndexChanged"></asp:DropDownList></ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel_EtapaProgrProd" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                         <asp:DropDownList ID="EtapaCultivoProgramaProduccion" runat="server" 
                                             style="margin-bottom: 0px; text-align: left;"></asp:DropDownList>
                                             <asp:RequiredFieldValidator id="RequiredFieldValidator_EtapaCultivoProgramaProduccion" runat="server" ControlToValidate="EtapaCultivoProgramaProduccion"  ValidationGroup="grupo4"
                                                ErrorMessage="Etapa de Desarrollo" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>    
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="EspecieProgramaProduccion" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GrupoProgramaProduccion" EventName="SelectedIndexChanged" />
                                        </Triggers>
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
                 
                 <td class="col3" style="text-align: center;"><asp:TextBox ID="DensidadProgProd" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px"></asp:TextBox>
                     <asp:RequiredFieldValidator id="RequiredFieldValidator_DensidadProgProd" runat="server" ControlToValidate="DensidadProgProd"  ValidationGroup="grupo4"
                                ErrorMessage="Densidad" Display="Static">*</asp:RequiredFieldValidator>
                      <asp:CompareValidator ID="CompareValidator_DensidadProgProd" runat="server" Operator="DataTypeCheck" Type="Double" 
                                                ControlToValidate="DensidadProgProd" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />
                 </td>
                 <td class="col3" style="text-align: center;"><asp:TextBox ID="ProdUltimoAnio" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator_ProdUltimoAnio" runat="server" ControlToValidate="ProdUltimoAnio"  ValidationGroup="grupo4"
                                ErrorMessage="Producción ultimo año" Display="Static">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator_ProdUltimoAnio" runat="server" Operator="DataTypeCheck" Type="Double" 
                                                ControlToValidate="ProdUltimoAnio" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />
                 </td>
                 
                 <td class="col3" style="text-align: center">
                    <table width="100%">
                        <tr>
                            <td style="text-align: center">Año 1<asp:TextBox ID="AnioProd1" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px"></asp:TextBox>
                             <asp:RequiredFieldValidator id="RequiredFieldValidator_AnioProd1" runat="server" ControlToValidate="AnioProd1"  ValidationGroup="grupo4"
                                ErrorMessage="Año 1" Display="Static">*</asp:RequiredFieldValidator>
                              <asp:CompareValidator ID="CompareValidator_AnioProd1" runat="server" Operator="DataTypeCheck" Type="Double" 
                                                ControlToValidate="AnioProd1" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />
                            </td>
                        </tr>
                         <tr>
                            <td style="text-align: center">Año 2<asp:TextBox ID="AnioProd2" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px"></asp:TextBox>
                             <asp:RequiredFieldValidator id="RequiredFieldValidator_AnioProd2" runat="server" ControlToValidate="AnioProd2"  ValidationGroup="grupo4"
                                ErrorMessage="Año 2" Display="Static">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator_AnioProd2" runat="server" Operator="DataTypeCheck" Type="Double" 
                                                ControlToValidate="AnioProd2" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />
                            </td>
                        </tr>
                         <tr>
                            <td style="text-align: center">Año 3<asp:TextBox ID="AnioProd3" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px"></asp:TextBox>
                             <asp:RequiredFieldValidator id="RequiredFieldValidator_AnioProd3" runat="server" ControlToValidate="AnioProd3"  ValidationGroup="grupo4"
                                ErrorMessage="Año 3" Display="Static">*</asp:RequiredFieldValidator>
                             <asp:CompareValidator ID="CompareValidator_AnioProd3" runat="server" Operator="DataTypeCheck" Type="Double" 
                                                ControlToValidate="AnioProd3" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">Año 4<asp:TextBox ID="AnioProd4" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator_AnioProd4" runat="server" ControlToValidate="AnioProd4"  ValidationGroup="grupo4"
                                ErrorMessage="Año 4" Display="Static">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator_AnioProd4" runat="server" Operator="DataTypeCheck" Type="Double" 
                                                ControlToValidate="AnioProd4" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">Año 5<asp:TextBox ID="AnioProd5" runat="server" onKeyUp="return onlyNumericoComa(this)" Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator_AnioProd5" runat="server" ControlToValidate="AnioProd5"  ValidationGroup="grupo4"
                                ErrorMessage="Año 5" Display="Static">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator_AnioProd5" runat="server" Operator="DataTypeCheck" Type="Double" 
                                                ControlToValidate="AnioProd5" ErrorMessage="Ingrese formato válido Ej: 12,3" ValidationGroup="grupo4" />
                            </td>
                        </tr>
                        
                    </table>
                  </td>
                   
                </tr>
                <tr><td class="item" colspan="7">&nbsp;</td></tr>
               <tr>
                   <td class="item" colspan="7" style="text-align: center">
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

                <tr>
                    <td colspan="7">
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
                            <asp:TemplateField HeaderText="Etapa de Desarrollo">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.etapaCultivo.descripcion")%>
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
                             <asp:TemplateField HeaderText="Densidad">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.densidad")%>
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
                                    <asp:ImageButton ID="gEliminar" Visible="true" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idProgrProduccion")+ ";" + DataBinder.Eval(Container.DataItem, "index") %>'
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

        <!-- Observaciones PT -->
        <asp:UpdatePanel ID="UpdatePanelObservacionesPT" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Panel ID="PanelObservacionesPT"  Visible="false" runat="server"> 
            <fieldset>
            <legend>Observaciones Proyecto Técnico</legend>
            <br />

            <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                       <td class="col1"><asp:TextBox ID="observaciones" TextMode="multiline" Columns="50" Rows="5" MaxLength="2000" runat="server"></asp:TextBox><b id="caracteresProy">  2000</b> Caracteres 
                           Disponibles</td>
                       <td class="col2" valign="bottom">&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>
        
        <table class="form" cellpadding="0px" cellspacing="0px">
         <tr>
         <td class="col2">
            &nbsp;
            </td>
         </tr>

         <asp:UpdatePanel ID="BotonesProyTecnico" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
         <asp:Panel ID="GuardarProyTecnico" runat="server" Visible="true">
             <tr>
                <td class="col2">
                &nbsp;
                </td>
                <td class="col1">
                     <asp:Button ID="Guardar" runat="server" Text="Guardar Proyecto Técnico"  CausesValidation="false" onclick="Guardar_ProyTecnico_Click"/>
                </td>
                <td class="col2">
                &nbsp;
                </td>
             </tr>
         </asp:Panel>
         <asp:Panel ID="ModificarProyTecnico" runat="server" Visible="false">
             <tr>
                <td class="col2">
                &nbsp;
                </td>
                <td class="col1">
                     <asp:Button ID="Modificar" runat="server" Text="Modificar Proyecto Técnico"  CausesValidation="false" onclick="Modificar_ProyTecnico_Click"/>
                </td>
                <td class="col2">
                &nbsp;
                </td>
             </tr>
         </asp:Panel>
         
         </ContentTemplate>
         </asp:UpdatePanel>
        </table>
        

    <!-- DIALOGOS ------------------------------------------------------------------------------------------------------------------------------------------------------>
    
    <div id="msgGuardar" class="message">
        <div class="background"></div>
        <div class="content_message">
            <div class="msgForm_div1"><asp:Image ID="Ico_ok" ImageUrl="~/App_Themes/admin_style/images/ico_ok.png" runat="server" /></div>
            <div class="msgForm_div2">
            <table class="msgFormGuardar" cellpadding="0px" cellspacing="0px">
            <tr>
                <td style="vertical-align:middle;">Se han guardado satisfactoriamente los privilegios del grupo de usuario.</td>
            </tr>
            </table>            
            </div>
            <div class="msgForm_div3">
            <input id="Continuar" type="button" value="Continuar modificando" onclick="close_message(this);" />
            <asp:Button ID="Finalizar" Text="Finalizar" OnClientClick="javascript:close_message(this);" CausesValidation="false" runat="server" /></div>
        </div>
    </div>

    <div id="adminGrupos" class="dialog">
        <div class="background"></div>
        <div class="content_dialog">
            <div class="top">
                <asp:LinkButton ID="cerrar_adminGrupos" CssClass="cerrar" OnClientClick="javascript:close_dialog('adminGrupos');" CausesValidation="false" runat="server"></asp:LinkButton>
            </div>
            <div class="body">
                <fieldset>
                    <legend>Administración de grupos de usuario</legend>
                    <iframe id="iframe_adminGrupos" src="" width="100%" height="249px" frameborder="0" scrolling="no"></iframe>
                </fieldset>
            </div>
        </div>
    </div>
    
    <!------------------------------------------------------------------------------------------------------------------------------------------------------------------>    
</asp:Content>
