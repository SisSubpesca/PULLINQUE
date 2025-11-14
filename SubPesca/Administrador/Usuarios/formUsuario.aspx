<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="formUsuario.aspx.cs"
         Inherits="SubPesca.Administrador.Usuarios.formUsuario" Theme="admin_style" %>
         
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
            <asp:ImageButton ID="Guardar" ImageUrl="~/App_Themes/admin_style/images/guardar.png" OnClick="Guardar_Click" Height="30px" ToolTip="Guardar información de usuario" Visible="false" runat="server" />
            <asp:ImageButton ID="Cancelar" ImageUrl="~/App_Themes/admin_style/images/cancelar.png" OnClick="Cancelar_Click" OnClientClick="return Cancelar()" CausesValidation="false" Height="30px" ToolTip="Cancelar" runat="server" />
        </td>
    </tr>
    </table>
    <hr style="width:100%" />

    <asp:ValidationSummary ID="valSum" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" />
    <asp:ValidationSummary ID="ValidationSummary1" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="RutValidacion" />
          
    <fieldset>
        <legend>Datos personales</legend>
        <br />
        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Nombres</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:TextBox ID="Nombre" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="Nombre" ErrorMessage="Nombres" Display="Static">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="col1" style="vertical-align:top; padding-top:14px"><span class="item">Apellidos</span></td>
            <td class="col2" style="vertical-align:top; padding-top:14px"><span class="item">:</span></td>
            <td class="col3">
                <table class="apellidos" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td>
                        <asp:TextBox ID="Apellidos" runat="server" MaxLength="40" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="Apellidos" ErrorMessage="Apellidos" Display="Static">*</asp:RequiredFieldValidator>
                    </td>
                    
                </tr>
                
                </table>
            </td>
        </tr>
        <tr>
            <td class="col1"><span class="item">RUT</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:TextBox ID="RUT" runat="server" MaxLength="10" Width="70px"></asp:TextBox>&nbsp;<asp:TextBox ID="dvUsuario" runat="server"  Width="10px" MaxLength="1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ccNumReqVal" ControlToValidate="RUT" ErrorMessage="RUT" Display="Dynamic" Font-Size="10" runat="server">*</asp:RequiredFieldValidator>
                <br />
                <span class="descripcion">Ej.: 12345678-k</span>
            </td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Región</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:DropDownList ID="Region" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="Region" ErrorMessage="Region" Display="Dynamic" Font-Size="10" runat="server" InitialValue="-1">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Correo electónico</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:TextBox ID="Correo" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="emailReqVal" ControlToValidate="Correo" ErrorMessage="Correo electrónico" Display="Dynamic" Font-Size="10" runat="server">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="emailFormatoVal" ControlToValidate="Correo" ErrorMessage="Correo electrónico" Display="Static" ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$" Font-Size="10" runat="server">Dirección de mail no válida</asp:RegularExpressionValidator>
                <asp:CustomValidator ID="emailInUsedVal" ControlToValidate="Correo" ErrorMessage="Correo" ClientValidationFunction="validaCorreo" Display="Static" Font-Size="10" runat="server">El correo está asignado a otro usuario</asp:CustomValidator>
            </td>
        </tr>
       
        </table>
    </fieldset>
    

    <fieldset> 
        <legend>Configuración de la cuenta de usuario</legend>
        <br />
        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Nombre de usuario</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:TextBox ID="Nick" runat="server" MaxLength="30" Width="250px"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ControlToValidate="Nick" ErrorMessage="Usuario" Display="Static">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator2" ControlToValidate="Nick" ErrorMessage="Usuario" ClientValidationFunction="validaNickUsuario" Display="Static" Font-Size="10" runat="server">El usuario ya existe</asp:CustomValidator>
            </td>
        </tr>
        <tr id="fila_clave" runat="server">
            <td class="col1"><span class="item">Contraseña</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:TextBox ID="Clave" TextMode="Password" runat="server" MaxLength="30" Width="250px"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" ControlToValidate="Clave" ErrorMessage="Contraseña" Display="Static">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="fila_newclave" runat="server">
            <td class="col1"><span class="item">Nueva Contraseña</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:LinkButton ID="cambiar_clave" OnClick="CambiarClave_Click" CausesValidation="false" runat="server">Cambiar Contraseña</asp:LinkButton>
                <asp:TextBox ID="NewClave" TextMode="Password" runat="server" MaxLength="30" Width="250px" Visible="false"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" ControlToValidate="NewClave" Visible="false" ErrorMessage="Nueva Contraseña" Display="Static">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="fila_reclave" runat="server">
            <td class="col1"><span class="item">Confirmar Contraseña</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:TextBox ID="ReClave" TextMode="Password" runat="server" MaxLength="30" Width="250px"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" ControlToValidate="ReClave" ErrorMessage="Confirmar Contraseña" Display="Static">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="ReClave" ErrorMessage="Confirmar Contraseña" Display="Dynamic" Font-Size="10" runat="server">Los campos de contraseña no coinciden</asp:CompareValidator>
            </td>
        </tr>
   
        <tr>
            <td class="col1"><span class="item">Estado del Usuario</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:RadioButton GroupName="usu_estados" ID="usu_desactivado" Text="Desactivado" runat="server" /><br />
                <asp:RadioButton GroupName="usu_estados" ID="usu_activado" Text="Activado" runat="server" />
            </td>
        </tr>
        </table>
    </fieldset>


    <!-- VARIABLES OCULTAS --------------------------------------------------------------------------------------------------------------------------------------------->   
    <asp:HiddenField ID="Id_Usuario" runat="server" />


    <!-- DIALOGOS ------------------------------------------------------------------------------------------------------------------------------------------------------>
    
    <div id="msgGuardar" class="message">
        <div class="background"></div>
        <div class="content_message">
            <div class="msgForm_div1"><asp:Image ID="Ico_ok" ImageUrl="~/App_Themes/admin_style/images/ico_ok.png" runat="server" /></div>
            <div class="msgForm_div2">
            <table class="msgFormGuardar" cellpadding="0px" cellspacing="0px">
            <tr>
                <td style="vertical-align:middle;">Se ha guardado satisfactoriamente el formulario de usuario.</td>
            </tr>
            </table>            
            </div>
            <div class="msgForm_div3">
            <asp:Button ID="Continuar" Text="Continuar modificando" OnClick="Continuar_Click" OnClientClick="javascript:close_message(this)" CausesValidation="false" runat="server" />
            <asp:Button ID="Finalizar" Text="Finalizar" OnClick="Finalizar_Click" CausesValidation="false" runat="server" /></div>
        </div>
    </div>

    <div id="adminGrupos" class="dialog">
        <div class="background"></div>
        <div class="content_dialog">
            <div class="top">
                <asp:LinkButton ID="cerrar_adminGrupos" CssClass="cerrar" OnClientClick="javascript:close_dialog('adminGrupos');" onClick="Carga_Combobox" CausesValidation="false" runat="server"></asp:LinkButton>
            </div>
            <div class="body">
                <fieldset>
                    <legend>Administración de grupos de usuario</legend>
                    <iframe id="iframe_adminGrupos" src="" width="100%" height="249px" frameborder="0" scrolling="no"></iframe>
                </fieldset>
            </div>
        </div>
    </div>
    
    

    
    <script language="javascript" type="text/javascript">
        document.getElementById("ctl00_rightbody_emailFormatoVal").style.display = "none"; 
        document.getElementById("ctl00_rightbody_emailInUsedVal").style.display = "none"; 
    </script>
    

</asp:Content>

