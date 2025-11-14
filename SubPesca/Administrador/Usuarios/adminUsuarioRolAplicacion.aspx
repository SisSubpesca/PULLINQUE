<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="adminUsuarioRolAplicacion.aspx.cs" 
Inherits="SubPesca.Administrador.Usuarios.adminUsuarioRolAplicacion" Theme="admin_style" %>

  
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">


    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>



    
    <fieldset> 
        <legend>Asignación de Roles a Usuario</legend>


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
        </table>

        <br />
        <br />
       
        <asp:UpdatePanel ID="upd2" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Content_Botones" Visible="true" runat="server">
                <table class="formtop" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td align="right" valign="middle">
                        <asp:ImageButton ID="Guardar" ImageUrl="~/App_Themes/admin_style/images/guardar.png" Visible="false" OnClick="Guardar_Click" OnClientClick="javascript:open_message('msgGuardar');" Height="30px" ToolTip="Guardar información de usuario" runat="server" />
                    </td>
                </tr>
                </table>            
            </asp:Panel>   
            
                

                 
            <asp:Panel ID="Content_Privilegios" Visible="true" runat="server"></asp:Panel>
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
                <td style="vertical-align:middle;">Se han guardado satisfactoriamente la asignación de roles al usuario.</td>
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
                <asp:LinkButton ID="cerrar_adminRoles" CssClass="cerrar" OnClientClick="javascript:close_dialog('adminRoles');"  CausesValidation="false" runat="server"></asp:LinkButton>
            </div>
            <div class="body">
                <fieldset>
                    <legend>Administración de Roles</legend>
                    <iframe id="iframe_adminRoles" src="" width="100%" height="249px" frameborder="0" scrolling="no"></iframe>
                </fieldset>
            </div>
        </div>
    </div>

    
</asp:Content>


