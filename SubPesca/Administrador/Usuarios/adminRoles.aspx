<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminRoles.aspx.cs" Inherits="SubPesca.Administrador.Usuarios.adminRoles" Theme="admin_style" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Página sin título</title>

</head>
<body>
    <form id="dialog_gruposusuarios" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <br />
        
        <asp:Panel ID="Content_GrupoUsuario" Visible="false" runat="server">
        <table class="form" cellpadding="1px" cellspacing="1px">
        <tr>
            <td class="col1"><span class="item">Rol</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"> 
                <asp:UpdatePanel ID="upd1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="Rol" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                </ContentTemplate>                
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Grupo</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Grupo" runat="server" />
                </ContentTemplate>                
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Filtra por sectorialista (Resumen de estados)</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="despliegue" runat="server" />
                </ContentTemplate>                
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td><span class="item"></span></td>
            <td style="width:10px; text-align:center;"></td>
            <td>
                <asp:UpdatePanel ID="upd3" UpdateMode="Always" runat="server">
                <ContentTemplate>
                    <asp:Button ID="Agregar" runat="server" Text="Agregar" OnClick="Agregar_Click" OnClientClick="javascript:doIframe();" />
                </ContentTemplate>
                </asp:UpdatePanel>        
            </td>
        </tr>

        </table>
        </asp:Panel>

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
                DataKeyNames="idRol"
                AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanged"
                AllowSorting="true" OnSorting="GridView1_Sorting"
                OnRowDataBound="GridView1_RowDataBound"
                OnRowCommand="GridView1_RowCommand"
                OnRowEditing="GridView1_RowEditing"
                OnRowUpdating="GridView1_RowUpdating"
                OnRowCancelingEdit="GridView1_RowCancelingEdit"
                CssClass="mGrid_dialog"
                PagerStyle-CssClass="pgr" >
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="idRol" ReadOnly="true" ItemStyle-Width="60px" />
                    
                    <asp:TemplateField HeaderText="Rol" SortExpression="Rol" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gRol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nombreRol") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="geRol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nombreRol") %>' MaxLength="30" Width="99%"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>  
                    
                    <asp:TemplateField HeaderText="Grupo" SortExpression="Rol" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gGrupo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tipoRol.descripcion") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>

                            <asp:HiddenField ID="hdnGrupo" runat="server" Value='<%#Eval("tipoRol.id") %>' />
                            <asp:DropDownList ID="geGrupo" runat="server" AutoPostBack="true" />
                            
                        </EditItemTemplate>
                    </asp:TemplateField>   
                    
                    <asp:TemplateField HeaderText="Filtra por sectorialista Resumen Estados" SortExpression="Rol" ItemStyle-Width="458px">   
                        <ItemTemplate>
                            <asp:Label ID="gDespliegue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaDespliegueFiltroDetalle") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>

                            <asp:HiddenField ID="hdnDespliegue" runat="server" Value='<%#Eval("aplicaDespliegueFiltro") %>' />
                            <asp:DropDownList ID="geDespliegue" runat="server" AutoPostBack="true" />
                            
                        </EditItemTemplate>
                    </asp:TemplateField>  
                                           
                    <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idRol") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Cambiar nombre" ToolTip="Cambiar nombre" />
                            <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idRol") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="gActualizar" runat="server" CausesValidation="false" CommandName="Update"
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
      </ContentTemplate> 
      <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
      </Triggers>
      </asp:UpdatePanel>
      
    </form>
</body>
</html>
