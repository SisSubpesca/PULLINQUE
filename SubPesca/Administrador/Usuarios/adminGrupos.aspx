<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminGrupos.aspx.cs" Inherits="SubPesca.Administrador.Usuarios.adminGrupos" Theme="admin_style" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>

</head>
<body>
    <form id="dialog_gruposusuarios" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <br />
        
        <asp:Panel ID="Content_GrupoUsuario" Visible="true" runat="server">
        <table cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:100px"><span class="item">Grupo de usuario</span></td>
            <td style="width:10px; text-align:center;"><span class="item">:</span></td>
            <td>
                <asp:UpdatePanel ID="upd1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="Grupo" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                </ContentTemplate>                
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="upd3" UpdateMode="Always" runat="server">
                <ContentTemplate>
                  
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
                                                      
           
            
            <asp:HiddenField ID="KeySort" runat="server" />           
      </ContentTemplate> 
      <Triggers>
            
      </Triggers>
      </asp:UpdatePanel>
      
    </form>
</body>
</html>
