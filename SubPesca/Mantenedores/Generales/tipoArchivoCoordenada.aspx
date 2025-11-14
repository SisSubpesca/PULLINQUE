<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="tipoArchivoCoordenada.aspx.cs" Inherits="SubPesca.Mantenedores.Generales.tipoArchivo" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

    <asp:Content ID="FormularioAdministracionTitulares" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Mantenedor de Tipo Archivo Coordenada</span>
            </td>
            <td align="right" valign="middle">
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

    <fieldset>
    <legend>Datos de Tipo Archivo Coordenada</legend>
    
        <asp:ValidationSummary ID="ValidationSummaryFormularioAgregar" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
        
        <br />

        <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Panel ID="Content_Panel" Visible="false" runat="server">
        <table class="form" cellpadding="0" cellspacing="0">
            <tr>
                <td class="style1"><span class="item">Tipo Coordenada</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="TipoCoordenada" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTipoCoordenada" runat="server" 
                        ControlToValidate="TipoCoordenada" Display="Static" ErrorMessage="Tipo Coordenada" 
                        ValidationGroup="grupo1">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td class="style1"><span class="item">Tipo Archivo</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    
                    <asp:TextBox ID="nombre" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorNombre" runat="server" ControlToValidate="nombre" ValidationGroup="grupo1" ErrorMessage="Nombre" Display="Static">*</asp:RequiredFieldValidator>    
                </td>
            </tr>
            <tr>
                <td class="style1">
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
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                DataKeyNames="id_provincia"
                AllowPaging="True" PageSize="4" OnPageIndexChanging="GridView1_PageIndexChanged"
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
                    <asp:BoundField HeaderText="ID" DataField="id_provincia" ReadOnly="true" ItemStyle-Width="60px" />
                    <asp:TemplateField HeaderText="Región" SortExpression="region" ItemStyle-Width="458px">
                         <ItemTemplate>
                            <asp:Label ID="gRegion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "codigo") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="geRegion" Text='<%# DataBinder.Eval(Container.DataItem, "codigo") %>' runat="server"></asp:DropDownList>
                            
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorgeCodigo" runat="server" ControlToValidate="geRegion"  ValidationGroup="grupo2" ErrorMessage="Seleccione valor para Región" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>
                            
                        </EditItemTemplate>
                    </asp:TemplateField> 
                    
                    <asp:TemplateField HeaderText="Tipo Archivo" SortExpression="region" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gNombre" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "region") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="geNombre" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "region") %>' MaxLength="30" Width="99%"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorgeNombre" runat="server" ControlToValidate="geNombre"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Nombre" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>

                        </EditItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:ImageButton ID="gModificar" Visible="false" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_provincia") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Cambiar nombre" ToolTip="Cambiar nombre" />
                            <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_provincia") %>'
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
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#5794EF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView> 
            
            <asp:HiddenField ID="KeySort" runat="server" />           
      </ContentTemplate> 
      <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
      </Triggers>
      </asp:UpdatePanel>

    </asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            width: 175px;
        }
    </style>
</asp:Content>

