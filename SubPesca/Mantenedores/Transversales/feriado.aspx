<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administrador/SitioAdmin.Master" CodeBehind="feriado.aspx.cs" Inherits="SubPesca.Mantenedores.Transversales.feriado" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
        <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
        <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
        <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
        <style type="text/css">
            .style1
            {
                width: 158px;
            }
        </style>
    </asp:Content>

    <asp:Content ID="FormularioAdministracionTitulares" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnableScriptGlobalization="True"></asp:ToolkitScriptManager>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Mantenedor de Feriados</span>
            </td>
            <td align="right" valign="middle">
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

    <fieldset>
    <legend>Datos de Feriados</legend>
    
        <asp:ValidationSummary ID="ValidationSummaryFormularioAgregar" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
        
        <br />

        <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Panel ID="Content_Panel" Visible="false" runat="server">
        <table class="form" cellpadding="0" cellspacing="0">
             <tr>
                <td class="col1"><span class="item">Descripción Feriado</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="nombre" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorNombre" runat="server" ControlToValidate="nombre" ValidationGroup="grupo1" ErrorMessage="Nombre Dirección Zonal" Display="Static">*</asp:RequiredFieldValidator>    

                    
                </td>
            </tr>
             <tr>
                <td class="col1"><span class="item">Fecha Feriado</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                <asp:UpdatePanel ID="UpdatePanel_FechaRecepcion" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaTextRecepcion" Width="120px" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Enabled="True"
                        Format="dd'/'MM'/'yyyy'"  TargetControlID="FechaTextRecepcion" PopupButtonID="endCal1"  CssClass="cal_Theme1" FirstDayOfWeek="Monday"/>
                        <img runat="server" id="endCal1" alt ="CalendarioRecepcion" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                       
                        <asp:RegularExpressionValidator 
                         ID="RegularExpressionValidator1" 
                         runat="server"
                         ControlToValidate="FechaTextRecepcion"
                         ForeColor="Red"
                         ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"  
                         ErrorMessage="Ingrese formato válido"
                         ValidationGroup="grupo1"> </asp:RegularExpressionValidator>

                         <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="FechaTextRecepcion" ValidationGroup="grupo1" ErrorMessage="Fecha Feriado" Display="Static">*</asp:RequiredFieldValidator>    

                    </div>
                </div>
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
                                                      
            <asp:GridView ID="GridView1" runat="server"
                RowStyle-VerticalAlign="top" AlternatingRowStyle-VerticalAlign="top"
                DataKeyNames="idFeriado"
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
                    
                     <asp:TemplateField HeaderText="Descripcion Feriado" SortExpression="descripcion">
                         <ItemTemplate>
                            <asp:Label ID="gDescripcionFeriado" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "descripcion") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                            <asp:HiddenField ID="hdnDescripcionFeriado" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "descripcion") %>' />
                            <asp:TextBox ID="geDescripcionFeriado" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "descripcion") %>'></asp:TextBox>
                            
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorgeDescripcionFeriado" runat="server" ControlToValidate="geDescripcionFeriado"  ValidationGroup="grupo2" ErrorMessage="Seleccione valor para Descripciòn Feriado" ForeColor="Red" Display="Static" InitialValue= ""></asp:RequiredFieldValidator>
                            
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha Feriado" SortExpression="fechaString">
                         <ItemTemplate>
                            <asp:Label ID="gFechaReemplazo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fechaString") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                            <asp:HiddenField ID="hdnFechaReemplazo" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "fechaString") %>' />
                            <asp:TextBox ID="geFechaReemplazo" Width="120px" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "fechaString") %>'></asp:TextBox>

                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Enabled="True"
                            Format="dd'/'MM'/'yyyy'"  TargetControlID="geFechaReemplazo" PopupButtonID="endCal1"  CssClass="cal_Theme1" FirstDayOfWeek="Monday"/>
                            <img runat="server" id="endCal1" alt ="CalendarioRecepcion" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                           
                            <asp:RegularExpressionValidator 
                            ID="RegularExpressionValidatorFechaReemplazo" 
                            runat="server"
                            ControlToValidate="geFechaReemplazo"
                            ForeColor="Red"
                            ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" 
                            ErrorMessage="Ingrese formato válido"
                            ValidationGroup="grupo2">
                            </asp:RegularExpressionValidator>

                        </EditItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Opciones">
                        <ItemTemplate>
                            <asp:ImageButton ID="gModificar" Visible="false" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idFeriado") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                            <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idFeriado") %>'
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
            <asp:AsyncPostBackTrigger ControlID="Buscar" EventName="Click" />
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
      </Triggers>
      </asp:UpdatePanel>

      <script type="text/javascript">
          invoca_calendarios("feriado");
      </script>

    </asp:Content>
