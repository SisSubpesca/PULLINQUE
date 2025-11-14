<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="adminUsuarioPrivAplicacion.aspx.cs" 
         Inherits="SubPesca.Administrador.Usuarios.adminUsuarioPrivAplicacion" Theme="admin_style" %>
         
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <fieldset> 
        <legend>Asignación de privilegios a usuario</legend>  
        <br />
        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Usuario</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:Label ID="Usuario" runat="server"></asp:Label>
                <asp:HiddenField ID="Id_Usuario" runat="server" />                
            </td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Grupo de usuario</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:Label ID="GrupoUsuario" runat="server"></asp:Label></td>
        </tr>
        </table>

        <br />
                      
        <asp:UpdatePanel ID="upd2" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Content_Botones" Visible="false" runat="server">
                <table class="formtop" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td align="right" valign="middle">
                        <asp:ImageButton ID="Guardar" ImageUrl="~/App_Themes/admin_style/images/guardar.png" Visible="true" OnClick="Guardar_Click" OnClientClick="javascript:open_message('msgGuardar');" Height="30px" ToolTip="Guardar información de usuario" runat="server" />
                        <asp:ImageButton ID="Cancelar" ImageUrl="~/App_Themes/admin_style/images/cancelar.png" Visible="true" OnClick="Cancelar_Click" OnClientClick="return Cancelar()" CausesValidation="false" Height="30px" ToolTip="Cancelar" runat="server" />
                    </td>
                </tr>
                </table>            
            </asp:Panel>        
            <asp:Panel ID="Content_Privilegios" Visible="false" runat="server"></asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Finalizar" EventName="Click" />
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
                <td style="vertical-align:middle;">Se han guardado satisfactoriamente los privilegios del grupo de usuario.</td>
            </tr>
            </table>            
            </div>
            <div class="msgForm_div3">
            <input id="Continuar" type="button" value="Continuar modificando" onclick="close_message(this);" />
            <asp:Button ID="Finalizar" Text="Finalizar" OnClick="Finalizar_Click" OnClientClick="javascript:close_message(this);" CausesValidation="false" runat="server" /></div>
        </div>
    </div>

</asp:Content>

