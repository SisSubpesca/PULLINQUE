<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="plazosDocumentos.aspx.cs" Inherits="SubPesca.Mantenedores.Transversales.plazosDocumentos" Theme="admin_style" %>

    <%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
    </asp:Content>


    <asp:Content ID="FormularioPlazoDocumentos" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ScriptManager ID="ScriptManagerPlazoDocumentos" runat="server"></asp:ScriptManager>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Mantenedor de Plazos de Documentos</span>
            </td>
            <td align="right" valign="middle">
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

    <fieldset>
    <legend>Datos de Plazos de Documentos</legend>
    
        <asp:ValidationSummary ID="ValidationSummaryFormularioAgregar" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
        
        <br />

        <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Panel ID="Content_Panel" Visible="false" runat="server">
        <table class="form" cellpadding="0" cellspacing="0">
            <tr>
                <td class="col1"><span class="item">Subrequerimiento Origen</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="SubrequerimientoOrigen" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorSubrequerimientoOrigen" runat="server" 
                        ControlToValidate="SubrequerimientoOrigen" Display="Static" ErrorMessage="Subrequerimiento Origen" 
                        ValidationGroup="grupo1" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td class="col1"><span class="item">Tipo Unidad Espacial</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="TipoUnidadEspacial" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoUnidadEspacial" runat="server" 
                        ControlToValidate="TipoUnidadEspacial"  ValidationGroup="grupo1" ErrorMessage="Tipo Unidad Espacial" 
                        Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td> 
            </tr>
            <tr>
                <td class="col1"><span class="item">Plazo Días</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>

                   <asp:UpdatePanel ID="UpdatePanelDias" UpdateMode="Conditional" runat="server">
                   <ContentTemplate>
                   <asp:Panel ID="PanelDias" Visible="true" runat="server">

                   <asp:TextBox ID="PlazoDias" runat="server" MaxLength="20" Width="100px"></asp:TextBox>
                   <asp:RangeValidator ID="RangeValidatorPlazoDias" runat="server" Type="Integer" MinimumValue="0" MaximumValue="9999" ControlToValidate="PlazoDias" ForeColor="Red" ErrorMessage="Plazo Días rango no permitido" ValidationGroup="grupo1" />
                   <asp:CompareValidator ID="CompareValidatorPlazoDias" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="PlazoDias" ForeColor="Red" ErrorMessage="Plazo Días ingrese valores numéricos" ValidationGroup="grupo1" />

                   </asp:Panel>
                   </ContentTemplate>
                   </asp:UpdatePanel>

                </td> 
            </tr>
             <tr>
                <td class="col1"><span class="item">Plazo Meses</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                   <asp:UpdatePanel ID="UpdatePanelMeses" UpdateMode="Conditional" runat="server">
                   <ContentTemplate>
                   <asp:Panel ID="PanelMeses" Visible="true" runat="server">

                   <asp:TextBox ID="PlazoMeses" runat="server" MaxLength="20" Width="100px"></asp:TextBox>
                   <asp:RangeValidator ID="RangeValidatorPlazoMeses" runat="server" Type="Integer" MinimumValue="0" MaximumValue="9999" ControlToValidate="PlazoMeses" ForeColor="Red" ErrorMessage="Plazo Meses rango no permitido" ValidationGroup="grupo1" />
                   <asp:CompareValidator ID="CompareValidatorPlazoMeses" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="PlazoMeses" ForeColor="Red" ErrorMessage="Plazo Meses ingrese valores numéricos" ValidationGroup="grupo1" />
  
                   </asp:Panel>
                   </ContentTemplate>
                   </asp:UpdatePanel>
                </td>
                
            </tr>
            <tr>
                <td style="width:100px">
                    &nbsp;</td>
                <td style="width:10px; text-align:center;">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Agregar" runat="server" OnClick="Agregar_Click" 
                        OnClientClick="javascript:doIframe();" Text="Agregar" 
                        CausesValidation="true" ValidationGroup="grupo1" Height="26px" />
                    <asp:Button ID="Buscar" runat="server" OnClientClick="javascript:doIframe();" 
                        Text="Buscar" onclick="Buscar_Click" />
                </td>
            </tr>
        </table>
        </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
                       
        </fieldset>

        <asp:UpdatePanel ID="upd2" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <div style="height:30px;">
            <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla_dialog" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla" runat="server"></asp:Label>
                </div>
                <div class="msgGrilla_div3">
                    <a onclick="ocultarObjeto('Content_msgGrilla', 0)"><img src="../../App_Themes/admin_style/images/cerrar.jpg" height="20px" alt="borrar" /></a>
                </div>
            </asp:Panel>
            </div>     
                                                      
            <asp:GridView ID="GridView1" runat="server"
                RowStyle-VerticalAlign="top" AlternatingRowStyle-VerticalAlign="top"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                DataKeyNames="idPlazoDoc"
                AllowPaging="True" PageSize="30" OnPageIndexChanging="GridView1_PageIndexChanged"
                AllowSorting="true" OnSorting="GridView1_Sorting"
                OnRowDataBound="GridView1_RowDataBound"
                OnRowCommand="GridView1_RowCommand"
                OnRowEditing="GridView1_RowEditing"
                OnRowUpdating="GridView1_RowUpdating"
                OnRowCancelingEdit="GridView1_RowCancelingEdit"
                CssClass="mGrid_dialog"
                PagerStyle-CssClass="pgr" 
                Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="idPlazoDoc" ReadOnly="true" ItemStyle-Width="60px" />
                    <asp:TemplateField HeaderText="Subrequerimiento Origen" SortExpression="docOrigenString" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gSubrequerimientoOrigen" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("docOrigenString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                                <asp:DropDownList ID="ddleditCountry" runat="server" AutoPostBack="true" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorDdleditCountry" runat="server" ControlToValidate="ddleditCountry"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Subrequerimiento Origen" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                                <asp:HiddenField ID="hdnCountry" runat="server" Value='<%#Eval("docOrigenString") %>' />
                        </EditItemTemplate>  
                    </asp:TemplateField> 
                    
                    <asp:TemplateField HeaderText="Tipo Unidad Espacial" SortExpression="tipoUEString" ItemStyle-Width="458px">
                         <ItemTemplate>
                            <asp:Label ID="gTipoUnidadEspacial" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("tipoUEString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                                <asp:DropDownList ID="ddleditCountry2" runat="server" AutoPostBack="true" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorDdleditCountry2" runat="server" ControlToValidate="ddleditCountry2"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Tipo Unidad Espacial" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                                <asp:HiddenField ID="hdnCountry2" runat="server" Value='<%#Eval("tipoUEString") %>' />
                        </EditItemTemplate>  
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Plazo Dias" SortExpression="plazoDias" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gDias" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "plazoDias") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="geDias" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "plazoDias") %>' MaxLength="30" Width="99%"></asp:TextBox>
                            <asp:RangeValidator ID="RangeValidatorgeDias" runat="server" Type="Integer" MinimumValue="0" MaximumValue="9999" ControlToValidate="geDias" ForeColor="Red" ErrorMessage="Plazo Días rango no permitido" ValidationGroup="grupo2" />
                            <asp:CompareValidator ID="CompareValidatorgeDias" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="geDias" ForeColor="Red" ErrorMessage="Plazo Días ingrese valores numéricos" ValidationGroup="grupo2" />


                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Plazo Meses" SortExpression="plazoMeses" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gMeses" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "plazoMeses") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="geMeses" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "plazoMeses") %>' MaxLength="30" Width="99%"></asp:TextBox>
                            <asp:RangeValidator ID="RangeValidatorgeMeses" runat="server" Type="Integer" MinimumValue="0" MaximumValue="9999" ControlToValidate="geMeses" ForeColor="Red" ErrorMessage="Plazo Meses rango no permitido" ValidationGroup="grupo2" />
                            <asp:CompareValidator ID="CompareValidatorgeMeses" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="geMeses" ForeColor="Red" ErrorMessage="Plazo Meses ingrese valores numéricos" ValidationGroup="grupo2" />

                        </EditItemTemplate>
                    </asp:TemplateField>
                                          
                    <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:ImageButton ID="gModificar" Visible="false" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPlazoDoc") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Cambiar nombre" ToolTip="Cambiar nombre" />
                            <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPlazoDoc") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="gActualizar" runat="server" CausesValidation="true" ValidationGroup="grupo2" CommandName="Update"
                                 ImageUrl="~/App_Themes/admin_style/images/guardar2.png"  Height="20px" AlternateText="Actualizar" ToolTip="Actualizar" />
                            <asp:ImageButton ID="gCancelar" runat="server" CausesValidation="false" CommandName="Cancel" 
                                 ImageUrl="~/App_Themes/admin_style/images/cancelar.png"  Height="20px" AlternateText="Cancelar" ToolTip="Cancelar" />                            
                        </EditItemTemplate>
                    </asp:TemplateField>
                 </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#5794EF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView> 
            
            <asp:HiddenField ID="KeySort" runat="server" />
            <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" Visible="false" />
                       
      </ContentTemplate> 
      <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
            <asp:PostBackTrigger ControlID="ExportarGrilla" />

      </Triggers>
      </asp:UpdatePanel>

    </asp:Content>
