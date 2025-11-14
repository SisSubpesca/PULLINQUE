<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="barrioTipo.aspx.cs" Inherits="SubPesca.Mantenedores.Generales.barrioTipo" Theme="admin_style"  %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

    <asp:Content ID="FormularioAdministracionTipoBarrioConBarrio" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Mantenedor de Tipo Barrio - Barrio</span>
            </td>
            <td align="right" valign="middle">  
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

    <fieldset>
    <legend>Datos de Tipo Barrio - Barrio</legend>
    
        <asp:ValidationSummary ID="ValidationSummaryFormularioAgregar" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
        
        <br />

        <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Panel ID="Content_Panel" Visible="false" runat="server">
        <table class="form" cellpadding="0" cellspacing="0">
            <tr>
                <td class="col1"><span class="item">Tipo Barrio</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="TipoBarrio" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorRegion" runat="server" 
                        ControlToValidate="TipoBarrio" Display="Static" ErrorMessage="Tipo Barrio" 
                        ValidationGroup="grupo1" InitialValue="-1" >*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
                        <tr>
                            <td class="col1">
                                <span class="item">Barrio</span></td>
                            <td class="col2">
                                <span class="item">:</span></td>
                            <td>
                                <asp:DropDownList ID="Barrio" runat="server">
                                </asp:DropDownList>

                                 <asp:RequiredFieldValidator ID="RequiredFieldValidatorBarrio" runat="server" 
                        ControlToValidate="Barrio" Display="Static" ErrorMessage="Barrio" 
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
                        OnClientClick="javascript:doIframe();" Text="Agregar" CausesValidation="true" ValidationGroup="grupo1" />
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
                AllowPaging="True" PageSize="25" OnPageIndexChanging="GridView1_PageIndexChanged"
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
                    
                    <asp:TemplateField HeaderText="Tipo Barrio" SortExpression="nombreTipoBarrio" ItemStyle-Width="458px">
                         <ItemTemplate>
                            <asp:Label ID="gTipoBarrio" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nombreTipoBarrio") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddleditCountry" runat="server" AutoPostBack="true" />
                            <asp:HiddenField ID="hdnCountry" runat="server" Value='<%#Eval("nombreTipoBarrio") %>' />
                    
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorddleditCountry" runat="server" ControlToValidate="ddleditCountry"  ValidationGroup="grupo2" ErrorMessage="Seleccione valor para Tipo Barrio" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>
                        
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Barrio" SortExpression="barrio" ItemStyle-Width="458px">
                         <ItemTemplate>
                            <asp:Label ID="gBarrio" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "barrio") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddleditCountry2" runat="server" AutoPostBack="true" />
                            <asp:HiddenField ID="hdnCountry2" runat="server" Value='<%#Eval("barrio") %>' />
                    
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorddleditCountry2" runat="server" ControlToValidate="ddleditCountry2"  ValidationGroup="grupo2" ErrorMessage="Seleccione valor para Barrio" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>
                        
                        </EditItemTemplate>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idTipoBarrio") + ";" + DataBinder.Eval(Container.DataItem, "id_barrio") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                        </ItemTemplate>
                        <EditItemTemplate>
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
            <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" /> 
                       
      </ContentTemplate> 
      <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
      </Triggers>
      </asp:UpdatePanel>

    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td style="width:100px">
        <asp:Button ID="Volver" runat="server" Text="Volver" onclick="Volver_Click" />
        </td>
       
    </tr>
    </table>

    </asp:Content>
