<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" Theme="admin_style" AutoEventWireup="true" CodeBehind="agregarOperador.aspx.cs" Inherits="SubPesca.Mantenedores.Titulares.agregarOperador" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="agregarArchivoAdjunto.ascx"            tagname="agregarArchivoAdjunto"                          tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_usuarios.js")); %>" type="text/javascript"></script>
</asp:Content>

    <asp:Content ID="FormularioAdministracionTitulares" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManageAdministracionTitulares" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo"><asp:Label ID="LabelAccion" runat="server"></asp:Label> Operador</span>
            </td>
            <td align="right" valign="middle">
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

    <fieldset>
    <legend>Datos del Operador</legend>

    <asp:ValidationSummary ID="ValidationSummaryIngresoSolicitante" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />

     <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
        <div class="msgGrilla_div1">
            <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
        </div>
        <div class="msgGrilla_div2">
            <asp:Label ID="msgGrilla" runat="server"></asp:Label>
        </div>
    </asp:Panel>

    <table class="form" cellpadding="0px" cellspacing="0px">
    
    <tr>
        <td class="col1"><span class="item">Rut Operador</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            <asp:TextBox ID="RutPersona" MaxLength="10" Width="100px" runat="server"></asp:TextBox>
            <asp:Label ID="EjemploRut" runat="server" Text="12345678-K" Visible = "false"></asp:Label>

            <asp:RequiredFieldValidator id="RequiredFieldValidatorRutPersona" runat="server" ControlToValidate="RutPersona"  ValidationGroup="grupo1"
            ErrorMessage="Rut Persona" Display="Static">*</asp:RequiredFieldValidator>

            <asp:CustomValidator ID="ccNumCustVal" ControlToValidate="RutPersona" ErrorMessage="Rut Persona sin formato válido" ForeColor="Red" ClientValidationFunction="validaRUT" Display="Static" Font-Size="10" runat="server" ValidationGroup="grupo1"></asp:CustomValidator>
  
        </td>
    </tr>

    <asp:HiddenField ID="nombreOperadorAnterior" runat="server" />

    <tr>
        <td class="col1"><span class="item">Nombre Operador</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            
            <asp:TextBox ID="NombreOperador" runat="server" Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator id="RequiredFieldValidatorNombreOperador" runat="server" ControlToValidate="NombreOperador"  ValidationGroup="grupo1"
            ErrorMessage="Nombre Operador" Display="Static">*</asp:RequiredFieldValidator>

            <asp:ImageButton ID="cambiarNombrePersona" 
                                runat="server" ImageUrl="~/App_Themes/admin_style/images/editar_nombre.jpg" 
                                AlternateText="Cambiar Nombre Persona" ToolTip="Cambiar Nombre Persona" 
                                onclick="ImageButtonCambiarNombre_Click" style="width: 20px" Height="20px" Visible = "false"  />
            
        </td>
    </tr>
    <tr>
        <td class="col1"></td>
        <td class="col2"></td>
        <td class="col3">
            <asp:LinkButton ID="VerNombresPersona" 
                OnClientClick="javascript:abre_dialogo2('verNombres',document.getElementById('ctl00_rightbody_RutPersona').value,3)" 
                runat="server" Visible="false" >[Ver Nombres]</asp:LinkButton>
        </td>
    </tr>
    </table>
    
    <!-- Listado de Direcciones -->
    <asp:Panel ID="PanelGridContactoMatrizSucursal"  Visible="true" runat="server" CssClass="Content_Grilla">                                          
    <asp:GridView 
           ID="GridContactoMatrizSucursales"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%"
           OnRowCreated="GridContactoMatrizSucursales_RowCreated"
           OnRowDataBound="GridContactoMatrizSucursales_RowDataBound"
           OnRowCommand="GridContactoMatrizSucursales_RowCommand"
           AllowPaging="false">
           
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
               <asp:TemplateField HeaderText="Dirección" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>

                    <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                    <%# DataBinder.Eval(Container, "DataItem.direccion")%>

               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Región" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.region.region")%>
               </ItemTemplate>
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Comuna" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.region.comuna.comuna")%>
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                    <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idMatrizSuc") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                    ImageUrl="../../App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                                
                 </ItemTemplate>
               </asp:TemplateField>
            </Columns>

            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#5794EF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView> 
    </asp:Panel>

        <table class="form" cellpadding="0px" cellspacing="0px" width="100%">
                    <tr>
                        <td class="col1" colspan="6">
                            <asp:ImageButton ID="ImageButtonMatrizSucursal" 
                                runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                AlternateText="Agregar Matriz/Sucursal" ToolTip="Agregar Matriz/Sucursal" 
                                onclick="ImageButtonMatrizSucursal_Click" style="width: 20px" CausesValidation="true" ValidationGroup="grupo2"/>
                            <span class="item">Agregar Dirección</span></td>
                    </tr>
        </table>
        
        <br /><br />

        <!-- Listado de Contactos -->
        <asp:Panel ID="PanelContacto"  Visible="true" runat="server" CssClass="Content_Grilla">                                          
        <asp:GridView 
               ID="GridViewContacto"
               runat="server"
               AutoGenerateColumns="False" 
               CellPadding="4" 
               ForeColor="#333333" 
               GridLines="None"
               CssClass="mGrid"
               PagerStyle-CssClass="pgr"
               Width="100%"
               OnRowCreated="GridViewContacto_RowCreated"
               OnRowDataBound="GridViewContacto_RowDataBound"
               OnRowCommand="GridViewContacto_RowCommand"
               AllowPaging="false">
           
               <RowStyle BackColor="#EFF3FB" />
               <Columns>
                   <asp:TemplateField HeaderText="Tipo Contacto" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>

                        <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                        <%# DataBinder.Eval(Container, "DataItem.tipoContacto.descripcion")%>

                   </ItemTemplate>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Valor Contacto" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.valorContacto")%>
                   </ItemTemplate>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Detalle" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.detalle")%>
                   </ItemTemplate>
                   </asp:TemplateField>
               
                   <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>

                        <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idContacto") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                        ImageUrl="../../App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                                
                     </ItemTemplate>
                   </asp:TemplateField>
                </Columns>

                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#5794EF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView> 
        </asp:Panel>

        <table class="form" cellpadding="0px" cellspacing="0px" width="100%">
                        <tr>
                            <td class="col1" colspan="6">
                                <asp:ImageButton ID="AgregarContacto" 
                                    runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                    AlternateText="Agregar Matriz/Sucursal" ToolTip="Agregar Matriz/Sucursal" 
                                     style="width: 20px" CausesValidation="true" ValidationGroup="grupo2" 
                                    onclick="AgregarContacto_Click" />
                                <span class="item">Agregar Contacto</span></td>
                        </tr>
            </table>
        
        <br /><br />
       
    </fieldset>
   
   <asp:Panel ID="PanelArchivoAdjunto"  Visible="true" runat="server">
        <uc1:agregarArchivoAdjunto ID="agregarArchivoAdjunto" runat="server" />
    </asp:Panel>
    
    <br />
    
    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1">
        <asp:Button ID="GuardarOperador" runat="server" Text="Crear Operador" onclick="GuardarOperador_Click" CausesValidation="true" ValidationGroup="grupo1" />
        <asp:Button ID="ModificarOperador" runat="server" Text="Modificar Operador" 
                CausesValidation="true" ValidationGroup="grupo1" Visible="false" onclick="ModificarOperador_Click"
                 />
            </td>
        <td class="col2" colspan="5"><asp:Button ID="Cancelar" runat="server" Text="Cancelar" onclick="Cancelar_Click" /></td>
        
    </tr>
    </table>

        <!-- DIALOGOS -->

        <div id="verNombres" class="dialog">
        <div class="background"></div>
        <div class="content_dialog">
            <div class="top">
                <asp:LinkButton ID="cerrar_verNombres" CssClass="cerrar" OnClientClick="javascript:close_dialog('verNombres');" CausesValidation="false" runat="server"></asp:LinkButton>
            </div>
            <div class="body">
                <fieldset>
                    <legend>Ver Nombres</legend>
                    <iframe id="iframe_verNombres" src="" width="100%" height="249px" frameborder="0" scrolling="no"></iframe>
                </fieldset>
            </div>
        </div>
        </div>

    </asp:Content>