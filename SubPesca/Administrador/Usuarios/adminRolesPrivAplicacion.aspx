<%@Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="adminRolesPrivAplicacion.aspx.cs" 
EnableEventValidation="false" Inherits="SubPesca.Administrador.Usuarios.adminRolesPrivAplicacion" Theme="admin_style" %>

        
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <fieldset> 
        <legend>Asignación de Accesos a Roles</legend>
        <br />
        <table class="form" cellpadding="0px" cellspacing="0px">
         <tr>
            <td class="col1"><span class="item">Módulo</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Modulos" runat="server" OnSelectedIndexChanged="Modulos_change" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="Modulos" ErrorMessage="Módulo" Display="Static" InitialValue="-1" ValidationGroup="GrupoRolAcceso" >*</asp:RequiredFieldValidator>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Cancelar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="Finalizar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cerrar_adminRoles" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Menú</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Menus" runat="server" AutoPostBack="false"></asp:DropDownList>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="Menus" ErrorMessage="Menú" Display="Static" InitialValue="-1" ValidationGroup="GrupoRolAcceso">*</asp:RequiredFieldValidator>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Modulos" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Cancelar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="Finalizar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cerrar_adminRoles" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Rol</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Roles" runat="server"></asp:DropDownList>
                    <asp:LinkButton ID="link_adminRoles" Visible="false" OnClientClick="javascript:abre_dialogo('adminRoles', 0);" runat="server" CausesValidation="false">[Administrar Roles]</asp:LinkButton>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="Roles" ErrorMessage="Rol" Display="Static" InitialValue="-1" ValidationGroup="GrupoRolAcceso">*</asp:RequiredFieldValidator>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Cancelar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="Finalizar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cerrar_adminRoles" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="col1"><span class="item"></span></td>
            <td class="col2"><span class="item"></span></td>
            <td class="col3"><asp:Button id="BotonFiltrar" runat="server" Text="Filtrar" onclick="Carga_Privilegios" ValidationGroup="GrupoRolAcceso" /></td>
        </tr>
        </table>

        <br /><br />
        
        <asp:UpdatePanel ID="upd2" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Content_Botones" Visible="false" runat="server">
                <table class="formtop" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td align="right" valign="middle">
                        <asp:ImageButton ID="Guardar" ImageUrl="~/App_Themes/admin_style/images/guardar.png" Visible="false" OnClick="Guardar_Click" OnClientClick="javascript:open_message('msgGuardar');" Height="30px" ToolTip="Guardar información de usuario" runat="server" />
                        <asp:ImageButton ID="Cancelar" ImageUrl="~/App_Themes/admin_style/images/cancelar.png" Visible="false" OnClick="Cancelar_Click" OnClientClick="return Cancelar()" CausesValidation="false" Height="30px" ToolTip="Cancelar" runat="server" />
                    </td>
                </tr>
                </table>            
            </asp:Panel>        
            <asp:Panel ID="Content_Privilegios" Visible="false" runat="server"></asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Finalizar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Roles" EventName="SelectedIndexChanged" />
        </Triggers>
        </asp:UpdatePanel>    
        
    </fieldset>
    
    <!-- DIALOGOS ------------------------------------------------------------------------------------------------------------------------------------------------------>
    
    <div id="msgGuardar" class="message">
        <div class="background"></div>
        <div class="content_message">
            <div class="msgForm_div1"><asp:Image ID="Ico_ok" ImageUrl="~/App_Themes/admin_style/images/ico_ok.png" runat="server" /></div>
            <div class="msgForm_div2">
            <table class="msgFormGuardar" cellpadding="0px" cellspacing="0px">
            <tr>
                <td style="vertical-align:middle;">Se han guardado satisfactoriamente los permisos al Rol.</td>
            </tr>
            </table>            
            </div>
            <div class="msgForm_div3">
            <input id="Continuar" type="button" value="Continuar modificando" onclick="close_message('msgGuardar');" />
            <asp:Button ID="Finalizar" Text="Finalizar" OnClick="Finalizar_Click" OnClientClick="javascript:close_message('msgGuardar');" CausesValidation="false" runat="server" /></div>
        </div>
    </div>

    <div id="adminRoles" class="dialog">
        <div class="background"></div>
        <div class="content_dialog">
            <div class="top">
                <asp:LinkButton ID="cerrar_adminRoles" CssClass="cerrar" OnClientClick="javascript:close_dialog('adminRoles');" onClick="Carga_Combobox" CausesValidation="false" runat="server"></asp:LinkButton>
            </div>
            <div class="body">
                <fieldset>
                    <legend>Administración de Roles</legend>
                    <iframe id="iframe_adminRoles" src="" width="100%" height="600px" frameborder="0" scrolling="no"></iframe>
                </fieldset>
            </div>
        </div>
    </div>
    
    
</asp:Content>

