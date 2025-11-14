<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ingreso.aspx.cs" Inherits="SubPesca.ingreso" Theme="admin_style" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ register src="~/Administrador/includes/header.ascx"     tagname="header"     tagprefix="include" %>
<%@ register src="~/Administrador/includes/footer.ascx"     tagname="footer"     tagprefix="include" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pullinque 4.0</title>
</head>
<body class="admin_basebody">
    <form id="form1" runat="server">

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
                        <div class="login">                               
                            <div class="content_login">
                            <asp:Label ID="Version" runat="server" Text=""></asp:Label>
                            <fieldset>
                            <legend>Inicio de sesión</legend>  
                            <br />
                            <table class="form" cellpadding="0px" cellspacing="5px">

                            <tr>
                                <td style="width:60px;"><span class="item">Usuario</span></td>
                                <td style="width:15px; text-align:center;"><span class="item">:</span></td>
                                <td><asp:TextBox ID="txtUsername" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>            
                            </tr>

                            <tr>
                                <td><span class="item">Contraseña</span></td>
                                <td style="text-align:center"><span class="item">:</span></td>
                                <td><asp:TextBox ID="txtPassword" TextMode="Password" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
                            </tr>

                          <%--  <tr>
                                <td colspan="3"><span class="item"><asp:CheckBox ID="chkPersist" runat="server" Text="Persist Cookie" /></span></td>
                            </tr>--%>

                            <tr>
                                <td colspan="2"></td>
                                <td>
                                    <asp:Button ID="Ingresar" runat="server" Text="Ingresar" OnClick="Login_Click" />
                                    <br/>
                                    <asp:Label ID="errorLabel" runat="server"></asp:Label>
                                </td>
                            </tr>

                            </table>
                            </fieldset>
                            </div>
                        </div>
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
