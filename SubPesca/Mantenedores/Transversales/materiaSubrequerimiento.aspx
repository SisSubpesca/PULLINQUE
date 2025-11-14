
<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="materiaSubrequerimiento.aspx.cs" Inherits="SubPesca.Mantenedores.Transversales.materiaSubrequerimiento" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
    </asp:Content>

    <asp:Content ID="FormularioAdministracionMateriaSubrequerimiento" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnableScriptGlobalization="True"></asp:ToolkitScriptManager>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Mantenedor de Materia Subrequerimiento</span>
            </td>
            <td align="right" valign="middle">
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

    <fieldset>
    <legend>Datos de Materia Subrequerimiento</legend>
    
        <asp:ValidationSummary ID="ValidationSummaryFormularioAgregar" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
        
        <br />

        <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Panel ID="Content_Panel" Visible="false" runat="server">
        <table class="form" cellpadding="0" cellspacing="0">
              <tr>
                <td class="col1"><span class="item">Tipo Documento</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="TipoDocumento" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTipoDocumento" runat="server" 
                        ControlToValidate="TipoDocumento" Display="Static" ErrorMessage="Tipo Documento" 
                        ValidationGroup="grupo1" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
             </tr>
             <tr>
                <td class="col1"><span class="item">Origen</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="TipoDestinatario" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTipoDestinatario" runat="server" 
                        ControlToValidate="TipoDestinatario" Display="Static" ErrorMessage="Tipo Destinatario" 
                        ValidationGroup="grupo1" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
             </tr>
             <tr>
                <td class="col1"><span class="item">Tipo Materia</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="TipoMateria" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTipoMateria" runat="server" 
                        ControlToValidate="TipoMateria" Display="Static" ErrorMessage="Tipo Materia" 
                        ValidationGroup="grupo1" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
             </tr>
             <tr>
                <td class="col1"><span class="item">Subrequerimiento</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="Subrequerimiento" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorSubrequerimiento" runat="server" 
                        ControlToValidate="Subrequerimiento" Display="Static" ErrorMessage="Subrequerimiento" 
                        ValidationGroup="grupo1" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
             </tr>
             <tr>
                <td class="col1"><span class="item">Estado Materia</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="EstadoMateria" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEstadoMateria" runat="server" 
                        ControlToValidate="EstadoMateria" Display="Static" ErrorMessage="Estado Materia" 
                        ValidationGroup="grupo1" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
             </tr>
             <tr>
                <td class="col1"><span class="item">Estado Subrequerimiento</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="EstadoSubrequerimiento" runat="server"></asp:DropDownList>
                   
                </td>
             </tr>
             <tr>
                <td class="col1"><span class="item">Número</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                  <asp:DropDownList ID="numero" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorNumero" runat="server" 
                        ControlToValidate="numero" Display="Static" ErrorMessage="Número" 
                        ValidationGroup="grupo1" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
             </tr>
             <tr>
                <td class="col1"><span class="item">Fecha</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                   <asp:DropDownList ID="fecha" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFecha" runat="server" 
                        ControlToValidate="fecha" Display="Static" ErrorMessage="Fecha" 
                        ValidationGroup="grupo1" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
             </tr>
             <tr>
                <td class="col1"><span class="item">Número CI</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="numeroCI" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorNumeroCI" runat="server" 
                        ControlToValidate="numeroCI" Display="Static" ErrorMessage="Número CI" 
                        ValidationGroup="grupo1" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
             </tr>
             <tr>
                <td class="col1"><span class="item">Fecha CI</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="fechaCI" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFechaCI" runat="server" 
                        ControlToValidate="fechaCI" Display="Static" ErrorMessage="Fecha CI" 
                        ValidationGroup="grupo1" InitialValue="-1">*</asp:RequiredFieldValidator>
                    
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
            <asp:AsyncPostBackTrigger ControlID="Buscar" EventName="Click" />
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
            
            <asp:Panel ID="PanelGridRequerimiento"  Visible="true" runat="server" CssClass="Content_Grilla">                                          
            <asp:GridView ID="GridView1" runat="server"
                RowStyle-VerticalAlign="top" AlternatingRowStyle-VerticalAlign="top"
                DataKeyNames="idEquivalencia"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                AllowPaging="True" PageSize="20" OnPageIndexChanging="GridView1_PageIndexChanged"
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
                    
                    <asp:BoundField HeaderText="ID" DataField="idEquivalencia" ReadOnly="true" ItemStyle-Width="60px" />
                    <asp:TemplateField HeaderText="Tipo Documento" SortExpression="tipoDocumentoString" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gTipoDocumento" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("tipoDocumentoString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                                <asp:DropDownList ID="ddleditCountry" runat="server" AutoPostBack="true" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorDdleditCountry" runat="server" ControlToValidate="ddleditCountry"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Tipo Documento" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                                <asp:HiddenField ID="hdnCountry" runat="server" Value='<%#Eval("tipoDocumentoString") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Origen" SortExpression="origenString" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gTipoDestinatario" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("origenString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                                <asp:DropDownList ID="ddleditCountry2" runat="server" AutoPostBack="true" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorDdleditCountry2" runat="server" ControlToValidate="ddleditCountry2"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Tipo Destinatario" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                                <asp:HiddenField ID="hdnCountry2" runat="server" Value='<%#Eval("origenString") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tipo Materia" SortExpression="materiaString" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gTipoMateria" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("materiaString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                                <asp:DropDownList ID="ddleditCountry3" runat="server" AutoPostBack="true" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorDdleditCountry3" runat="server" ControlToValidate="ddleditCountry3"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Tipo Materia" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                                <asp:HiddenField ID="hdnCountry3" runat="server" Value='<%#Eval("materiaString") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subrequerimiento" SortExpression="subRequerimientoString" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gSubrequerimiento" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("subRequerimientoString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                                <asp:DropDownList ID="ddleditCountry4" runat="server" AutoPostBack="true" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorDdleditCountry4" runat="server" ControlToValidate="ddleditCountry4"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Subrequerimiento" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                                <asp:HiddenField ID="hdnCountry4" runat="server" Value='<%#Eval("subRequerimientoString") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado Materia" SortExpression="estadoMateriaString" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gEstadoMateria" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("estadoMateriaString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                                <asp:DropDownList ID="ddleditCountry5" runat="server" AutoPostBack="true" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorDdleditCountry5" runat="server" ControlToValidate="ddleditCountry5"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Estado Materia" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                                <asp:HiddenField ID="hdnCountry5" runat="server" Value='<%#Eval("estadoMateriaString") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado Subrequerimiento" SortExpression="estadoSubRequerimientoString" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gEstadoSubrequerimiento" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("estadoSubRequerimientoString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                                <asp:DropDownList ID="ddleditCountry6" runat="server" AutoPostBack="true" />
                                
                                <asp:HiddenField ID="hdnCountry6" runat="server" Value='<%#Eval("estadoSubRequerimientoString") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Numero" SortExpression="numeroString" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gNumero" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("numeroString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                                <asp:DropDownList ID="ddleditCountry7" runat="server" AutoPostBack="true" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorDdleditCountry7" runat="server" ControlToValidate="ddleditCountry7"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Número" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                                <asp:HiddenField ID="hdnCountry7" runat="server" Value='<%#Eval("numeroString") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha" SortExpression="fechaString" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gFecha" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("fechaString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                                <asp:DropDownList ID="ddleditCountry8" runat="server" AutoPostBack="true" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorDdleditCountry8" runat="server" ControlToValidate="ddleditCountry8"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Fecha" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                                <asp:HiddenField ID="hdnCountry8" runat="server" Value='<%#Eval("fechaString") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Numero CI" SortExpression="numeroCIString" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gNumeroCI" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("numeroCIString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                                <asp:DropDownList ID="ddleditCountry9" runat="server" AutoPostBack="true" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorDdleditCountry9" runat="server" ControlToValidate="ddleditCountry9"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Número CI" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                                <asp:HiddenField ID="hdnCountry9" runat="server" Value='<%#Eval("numeroCIString") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha CI" SortExpression="fechaCIString" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gFechaCI" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("fechaCIString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                                <asp:DropDownList ID="ddleditCountry10" runat="server" AutoPostBack="true" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorDdleditCountry10" runat="server" ControlToValidate="ddleditCountry10"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Fecha CI" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                                <asp:HiddenField ID="hdnCountry10" runat="server" Value='<%#Eval("fechaCIString") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Opciones">
                        <ItemTemplate>
                            <asp:ImageButton ID="gModificar" Visible="false" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idEquivalencia") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                            <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idEquivalencia") %>'
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
            </asp:Panel>

            <asp:HiddenField ID="KeySort" runat="server" />
            <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" Visible="false" />           
      </ContentTemplate> 
      <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Buscar" EventName="Click" />
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
      </Triggers>
      </asp:UpdatePanel>
    </asp:Content>
