<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="SubPesca.errors.Error" Theme="admin_style" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ register src="~/Administrador/includes/header.ascx"     tagname="header"     tagprefix="include" %>
<%@ register src="~/Administrador/includes/footer.ascx"     tagname="footer"     tagprefix="include" %>


<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pullinque 4.0</title>
</head>
<body class="admin_basebody">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></asp:ToolkitScriptManager>

    <div id="base_main">  
        <div id="main">    
            <div id="main_content">
                <div id="main_head">
                    <include:header ID="header" runat="server"></include:header>
                    <div style="height:3px;" ></div>
                </div>
                <div id="main_body">
                    <div id="main_leftbody">
                    </div>
                    <div id="main_rightbody">
                        
                            <fieldset>
                            <legend>Error</legend>  
                            <br />
                            Ha ocurrido un error. Por favor intente nuevamente mas tarde, si el error persiste comuniquese con un administrador.
                            <br />
                            <br />
                            <asp:Button ID="Volver" runat="server" OnClick="cmdVolver_Click" Text="Volver al Inicio" />
                            

                            <asp:UpdatePanel id="UpdatePanelError" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                <asp:Panel id="PanelError" runat="server" Visible="false">
                                    <table class="form" cellpadding="0px" cellspacing="5px">
                                    <tr>
                                        <td><asp:Label ID="mensajeError" runat="server"></asp:Label></td>
                                    </tr>
                                    </table>
                                </asp:Panel>
                                </ContentTemplate>

                            </asp:UpdatePanel>


                            </fieldset>
                            
                        
                    </div>
                </div>
                <div id="main_footer">
                    <include:footer ID="footer" runat="server"></include:footer>
                </div>
            </div>
        </div>
    </div>

    </form>
</body>
</html>
