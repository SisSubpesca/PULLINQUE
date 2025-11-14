<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="descasoACS.aspx.cs" 
Inherits="SubPesca.Mantenedores.Generales.descasoACS" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
     <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
        }
    </style>
</asp:Content>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

    <asp:Content ID="Formulario" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManagerCarta" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>
    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Mantenedor de Descanso ACS</span>
            </td>
            <td align="right" valign="middle">
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

      <fieldset>
    <legend>Datos de Descanso ACS</legend>
    
    <asp:UpdatePanel ID="UpdatePanelMgs" UpdateMode="Conditional" runat="server">
    <ContentTemplate>

    <asp:ValidationSummary ID="ValidationSummaryADescansos" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />

    <asp:Panel ID="Panel1"  Visible="false" runat="server">
        <div class="msgGrilla_div1">
            <asp:Image ID="Ico_msgGrillaGral" CssClass="Ico_msgGrilla" runat="server" />
        </div>
    </asp:Panel>
    </ContentTemplate> 
    </asp:UpdatePanel>

        <br />

        <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Panel ID="Content_Panel" Visible="false" runat="server">
        <table class="form" cellpadding="0" cellspacing="0">
            <tr>
                <td class="style1"><span class="item">Barrio</span></td>
                <td class="col2">:</td>
                <td>
                    
                    <asp:DropDownList ID="Barrio" runat="server" 
                        onselectedindexchanged="Barrio_SelectedIndexChanged" AutoPostBack="true" CausesValidation="true"></asp:DropDownList>

                    <asp:RequiredFieldValidator id="RequiredFieldValidatorBarrio" runat="server" ControlToValidate="Barrio"  ValidationGroup="grupo1"
                        ErrorMessage="Barrio" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>

                </td>
            </tr>
                      
            <tr>
                <td class="style1">
                   <span class="item">Fecha Inicio Descanso</span></td>
                <td style="width:10px; text-align:center;">
                    :</td>
                <td>
                <asp:UpdatePanel ID="UpdatePanelFechaDesde" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaRecepcion" Columns="8" Width="120px" runat="server" CausesValidation="true"></asp:TextBox>
                   
                    </div>
                    <div class="calendario_icono">
                        <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaRecepcion" alt="Calendario" style="vertical-align: middle" runat="server" />
                    </div>

                    <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="FechaRecepcion"  ValidationGroup="grupo1"
                        ErrorMessage="Fecha de Inicio" Display="Static" InitialValue="" >*</asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator1" 
                        runat="server"
                        ControlToValidate="FechaRecepcion"
                        ForeColor="Red"
                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" 
                        ErrorMessage="Ingrese formato válido"
                        ValidationGroup="grupo1">
                        </asp:RegularExpressionValidator>
                          
                     </div></ContentTemplate>
            </asp:UpdatePanel>
            </td>
            </tr>
            <tr>
                <td class="style1">
                    <span class="item">Fecha Fin Descanso</span></td>
                <td style="width:10px; text-align:center;">
                    :</td>
                <td>
                     <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaIngresoTramite" Columns="8" Width="120px" runat="server" CausesValidation="true"></asp:TextBox>
                   
                    </div>
                    <div class="calendario_icono">
                        <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaIngresoTramite" alt="Calendario" style="vertical-align: middle" runat="server" />
                    </div>

                    <asp:RequiredFieldValidator id="RequiredFieldValidatorFechRecep" runat="server" ControlToValidate="FechaIngresoTramite"  ValidationGroup="grupo1"
                        ErrorMessage="Fecha Fin" Display="Static" InitialValue="">*</asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator2" 
                        runat="server"
                        ControlToValidate="FechaIngresoTramite"
                        ForeColor="Red"
                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" 
                        ErrorMessage="Ingrese formato válido"
                        ValidationGroup="grupo1">
                        </asp:RegularExpressionValidator>
                          
                     </div></ContentTemplate>
            </asp:UpdatePanel>
            </td>
            </tr>


                <asp:UpdatePanel ID="UpdatePanelFechaInicioProduccion" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <asp:Panel ID="PanelFechaInicioProduccion" Visible="false" runat="server">

                <tr>
                <td class="style1">
                    <span class="item">Fecha Inicio Producción</span></td>
                <td style="width:10px; text-align:center;">
                    :</td>
                <td>
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaInicioProduccion" Columns="8" Width="120px" runat="server" CausesValidation="true"></asp:TextBox>
                   
                    </div>
                    <div class="calendario_icono">
                        <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaInicioProduccion" alt="Calendario" style="vertical-align: middle" runat="server"/>
                    </div>

                    <asp:RequiredFieldValidator id="RequiredFieldValidatorFechaInicioProduccion" runat="server" ControlToValidate="FechaInicioProduccion"  ValidationGroup="grupo1"
                        ErrorMessage="Fecha Inicio Producción" Display="Static" InitialValue="">*</asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidatorFechaInicioProduccion" 
                        runat="server"
                        ControlToValidate="FechaInicioProduccion"
                        ForeColor="Red"
                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" 
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


            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td style="width:10px; text-align:center;">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
             <tr>
                <td class="style1">
                    &nbsp;</td>
                <td style="width:10px; text-align:center;">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Agregar" runat="server" OnClick="Agregar_Click" 
                        OnClientClick="javascript:doIframe();" Text="Agregar" CausesValidation="true" ValidationGroup="grupo1" />
                    <asp:Button ID="Buscar" runat="server" Text="Buscar" onclick="Buscar_Click" />
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="3">
                    <span class="item">**Sólo puede ser eliminado el último periodo de descanso por barrio.</span></td>
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
                 
            
            <asp:GridView ID="GridViewDescanso" runat="server"
                RowStyle-VerticalAlign="top" AlternatingRowStyle-VerticalAlign="top"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                DataKeyNames="idDescanso"
                AllowPaging="True" PageSize="40" OnPageIndexChanging="GridViewDescanso_PageIndexChanged"
                AllowSorting="true" OnSorting="GridViewDescanso_Sorting"
                OnRowDataBound="GridViewDescanso_RowDataBound"
                OnRowCommand="GridViewDescanso_RowCommand"
                OnRowEditing="GridViewDescanso_RowEditing"
                CssClass="mGrid_dialog"
                PagerStyle-CssClass="pgr" 
                Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>

                    
                    <asp:BoundField HeaderText="ID" DataField="idDescanso" ReadOnly="true" ItemStyle-Width="60px" />
                    
                    <asp:TemplateField HeaderText="Barrio" SortExpression="barrioString" ItemStyle-Width="458px">
                        <ItemTemplate>
                       
                            <asp:Label ID="gIdBarrio" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "barrio.id") %>' Visible="false"/>
                            <asp:Label ID="gIdTipoOperacion" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tipoOperacion.id") %>' Visible="false" />
                            
                            <asp:Label ID="gBarrio" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "barrioString") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tipo de Operacion" SortExpression="tipoOperacionString" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gTipoOperacion" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("tipoOperacionString")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="N° de Operacion" SortExpression="numOperacion" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gNumeroOperacion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "numOperacion") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha Inicio" SortExpression="fechaInicioString" ItemStyle-Width="458px">
                         <ItemTemplate>
                            <asp:Label ID="gFechaInicio" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fechaInicioString") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha Fin" SortExpression="fechaFinString" ItemStyle-Width="458px">
                         <ItemTemplate>
                            <asp:Label ID="gFechaFin" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fechaFinString") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                        
                    <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                        <ItemTemplate>

                     

                            
                            <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDescanso") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />

                            
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

            
            <asp:HiddenField ID="KeySort" runat="server" />
            <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" />
                        
      </ContentTemplate> 
      <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
      </Triggers>
      </asp:UpdatePanel>
    <script type="text/javascript">
        invoca_calendarios("descansoACS");
    </script>

    </asp:Content>

