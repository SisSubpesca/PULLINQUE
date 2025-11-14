<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="listUsuarios.aspx.cs"
         Inherits="SubPesca.Administrador.Usuarios.listUsuarios" Theme="admin_style" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_usuarios.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Administración de usuarios</span>
        </td>
        <td align="right" valign="middle">
            <asp:Panel ID="PanelBotonAgregar" runat="server" Visible="false">
                <asp:ImageButton ID="Agregar" ImageUrl="~/App_Themes/admin_style/images/agregar.png" OnClick="Agregar_Click" Height="30px" ToolTip="Agregar nuevo Usuario" runat="server" />
            </asp:Panel>
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />

    <fieldset>
        <legend>Filtro de búsqueda</legend>
        <br />
        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Usuario</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="upd1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="Usuario" runat="server" MaxLength="30" Width="300px"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre y/o Apellido(s)</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="upd2" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="Nombre" runat="server" MaxLength="80" Width="300px"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>                    
            </td>
        </tr>
        <tr>
            <td class="col1"></td>
            <td class="col2"></td>
            <td class="col3">
                <asp:Button ID="Filtrar" runat="server" OnClick="Filtrar_Click" Text="Filtrar" />
                <asp:Button ID="Limpiar" runat="server" OnClick="Limpiar_Click" Text="Limpiar" />
            </td>
        </tr>
        </table>
    </fieldset>

    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
    <asp:Panel ID="Content_ListUsuarios" runat="server">
    <fieldset>    
        <legend>Listado de usuarios</legend>
        
        <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
            <div class="msgGrilla_div1">
                <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
            </div>
            <div class="msgGrilla_div2">
                <asp:Label ID="msgGrilla" runat="server"></asp:Label>
            </div>
        </asp:Panel>
                       
        <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
            AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanged"
            AllowSorting="False" OnSorting="GridView1_Sorting"
            OnRowCommand="GridView1_RowCommand"
            OnRowDataBound="GridView1_RowDataBound"
            CssClass="mGrid"
            PagerStyle-CssClass="pgr" >
            <RowStyle BackColor="#EFF3FB" />
            <Columns>


                <asp:BoundField HeaderText="ID" DataField="id_usuario" ItemStyle-Width="57px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Usuario" DataField="usuario" SortExpression="Usuario" ItemStyle-Width="110px" />
                <asp:BoundField HeaderText="Nombres"  DataField="nombre"   SortExpression="Nombre" ItemStyle-Width="150px" />
                <asp:BoundField HeaderText="Apellidos"  DataField="apellidos" SortExpression="ApPaterno" ItemStyle-Width="100px" />
                <asp:BoundField HeaderText="Estado" DataField="_estado" SortExpression="Estado" ItemStyle-Width="100px" />
                
                <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="gModificar" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_usuario") %>' Visible="false"  runat="server">
                            <asp:Image ID="gImgModificar" runat="server" ImageUrl="~/App_Themes/admin_style/images/form.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                        </asp:LinkButton>
                        <asp:LinkButton ID="gPermisos" CommandName="Permisos" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_usuario") %>' Visible="false" runat="server">
                            <asp:Image ID="gImgPermisos" runat="server" ImageUrl="~/App_Themes/admin_style/images/acceso.gif" Height="20px" AlternateText="Permisos de acceso" ToolTip="Permisos de acceso" />
                        </asp:LinkButton>
                        <asp:LinkButton ID="gSectores" CommandName="Sectores" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_usuario") %>' Visible="false" runat="server">
                            <asp:Image ID="gImgSectores" runat="server" ImageUrl="~/App_Themes/admin_style/images/casa.png" Height="20px" AlternateText="Permisos comunales" ToolTip="Permisos comunales" />
                        </asp:LinkButton>
                        <asp:LinkButton ID="gPert" CommandName="Asignacion" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_usuario") %>' Visible="false" runat="server">
                            <asp:Image ID="gImgPert" runat="server" ImageUrl="~/App_Themes/admin_style/images/obligaciones.png" Height="20px" AlternateText="Asignación de Pert" ToolTip="Asignación de Pert" />
                        </asp:LinkButton>
                        <asp:LinkButton ID="gEliminar" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id_usuario") %>' Visible="false" runat="server">
                            <asp:Image ID="gImgEliminar" runat="server" ImageUrl="~/App_Themes/admin_style/images/eliminar.png" AlternateText="Eliminar" ToolTip="Eliminar" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>


               
             </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </fieldset>
    </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Filtrar" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
    </Triggers>
    </asp:UpdatePanel>


    <!-- DIALOGOS ------------------------------------------------------------------------------------------------------------------------------------------------------>
    
    <div id="alcanceRegional" class="dialog">
        <div class="background"></div>
        <div class="content_dialog">
            <div class="top">
                <asp:LinkButton ID="cerrar_alcanceRegional" CssClass="cerrar" OnClientClick="javascript:close_dialog('alcanceRegional');" CausesValidation="false" runat="server"></asp:LinkButton>

                <!-- <a id="cerrar_alcanceRegional" class="cerrar" onclick="javascript:close_dialog('alcanceRegional');"></a> -->
            </div>
            <div class="body">
                <fieldset>
                    <legend>Permisos comunales del usuario</legend>
                    <iframe id="iframe_alcanceRegional" src="" class="autoHeight" width="100%" frameborder="0" scrolling="no"></iframe>
                </fieldset>
            </div>
        </div>
    </div>
    

</asp:Content>
