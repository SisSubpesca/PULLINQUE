<%@ Page Language="C#" Theme="admin_style" AutoEventWireup="true" CodeBehind="administrarOperadores.aspx.cs" 
Inherits="SubPesca.Mantenedores.Titulares.administrarOperadores" MasterPageFile="~/Administrador/SitioAdmin.Master" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_usuarios.js")); %>" type="text/javascript"></script>
</asp:Content>

    <asp:Content ID="FormularioAdministracionOperadores" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManageAdministracionOperadores" runat="server"></asp:ToolkitScriptManager>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Administrar Operadores</span>
            </td>
            <td align="right" valign="middle">
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />



    
    <fieldset>
    <legend>Búsqueda de Operadores</legend>

     
     
    <asp:UpdatePanel ID="UpdatePanelMgs" UpdateMode="Conditional" runat="server">
    <ContentTemplate>

    <asp:ValidationSummary ID="ValidationSummaryAdministrarOperadores" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />

    <asp:Panel ID="Panel1"  Visible="false" runat="server">
        <div class="msgGrilla_div1">
            <asp:Image ID="Ico_msgGrillaGral" CssClass="Ico_msgGrilla" runat="server" />
        </div>
        <div class="msgGrilla_Solicitud">
            <asp:Label ID="msgGrilla" runat="server"></asp:Label>
        </div>
    </asp:Panel>
    </ContentTemplate> 
    </asp:UpdatePanel>

     <table class="form" cellpadding="0px" cellspacing="0px">
     <tr>
        <td class="col1"><span class="item">Rut Operador</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>

            <asp:TextBox ID="RutPersona" MaxLength="10" Width="100px" runat="server"></asp:TextBox>
            
            <asp:Label ID="EjemploRut" runat="server" Text="12345678-K"></asp:Label>
            
            <asp:CustomValidator ID="ccNumCustVal" ControlToValidate="RutPersona" ErrorMessage="Rut Persona sin formato válido" ForeColor="Red" ClientValidationFunction="validaRUT" Display="Static" Font-Size="10" runat="server" ValidationGroup="grupo1"></asp:CustomValidator>
            
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="col1"><span class="item">Nombre Operador</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            <asp:UpdatePanel ID="UpdatePanelNombreOperador" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="NombrePersona" Width="300px" MaxLength="100" runat="server"></asp:TextBox>
            </ContentTemplate>
            </asp:UpdatePanel>

        </td>
    </tr>
    <tr>
        <td class="col1"></td>
        <td class="col2"></td>
        <td class="col3">
            <asp:Button ID="Crear" runat="server" Text="Crear"  
                CausesValidation="false" onclick="Crear_Click" />
            <asp:Button ID="Buscar" runat="server" Text="Buscar"  CausesValidation="true"  ValidationGroup="grupo1" onclick="Buscar_Click" />
        </td>
    </tr>
    </table>

    <asp:UpdatePanel ID="UpdatePanelOperadores" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
                    
            <asp:Panel ID="PanelOperadores"  Visible="true" runat="server">
                   
                 <asp:GridView ID="GridOperadores"  
                       runat="server"
                       DataKeyNames="rutOperador"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" PageSize="20" OnPageIndexChanging="GridOperadores_PageIndexChanged"
                       OnRowCreated="GridOperadores_RowCreated"
                       OnRowDataBound="GridOperadores_RowDataBound"
                       OnRowCommand="GridOperadores_RowCommand"
                       CssClass="mGrid"
                       PagerStyle-CssClass="pgr"
                       Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Rut">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.operador.rut")%>-<%# DataBinder.Eval(Container, "DataItem.operador.dv")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.operador.nombreSolicitante")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                         
                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="120px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "rutOperador") %>'
                                    ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                                    <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "rutOperador") %>'
                                    ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />

                                    <asp:ImageButton ID="gNoVigente" Visible="false" runat="server" CausesValidation="false" CommandName="NoVigente" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "rutOperador") %>'
                                    ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Pasar a No Vigente" ToolTip="Pasar a No Vigente" />

                                    <asp:ImageButton ID="gVigente" Visible="false" runat="server" CausesValidation="false" CommandName="Vigente" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "rutOperador") %>'
                                    ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Pasar a Vigente" ToolTip="Pasar a Vigente" />

                                    <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "rutOperador") %>'
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
        </ContentTemplate>
    </asp:UpdatePanel>

    </fieldset>
    </asp:Content>
