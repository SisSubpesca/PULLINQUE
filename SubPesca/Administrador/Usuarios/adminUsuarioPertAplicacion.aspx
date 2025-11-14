<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="adminUsuarioPertAplicacion.aspx.cs" 
Inherits="SubPesca.Administrador.Usuarios.adminUsuarioPertAplicacion"  Theme="admin_style" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <fieldset> 
        <legend>Asignación de Pert/Identificadores a Usuario</legend>


        <br />
        
        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Usuario</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:Label ID="Usuario" runat="server"></asp:Label>
                <asp:HiddenField ID="Id_Usuario" runat="server" />
                <a id="link_adminPert" onclick="javascript:abre_dialogo('adminPert', 0);">[Pert/Identificadores ya asignados]</a>
            </td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Tipo Tramite</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="TipoTramite" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="TipoTramite" ErrorMessage="Tipo Trámite" Display="Static" InitialValue="-1" ValidationGroup="GrupoRolAcceso">*</asp:RequiredFieldValidator>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Finalizar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cerrar_adminPert" EventName="Click" />
               </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Pert o Identificador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="NPert" runat="server"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Finalizar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cerrar_adminPert" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="col1"><span class="item"></span></td>
            <td class="col2"><span class="item"></span></td>
            <td class="col3"><asp:Button id="BotonFiltrar" runat="server" Text="Filtrar" ValidationGroup="GrupoRolAcceso" onclick="BotonFiltrar_Click" /></td>
        </tr>
        </table>
        

        <br />
        <br />
       
         
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
            <asp:Panel ID="Content_Pert" Visible="false" runat="server"></asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Finalizar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="TipoTramite" EventName="SelectedIndexChanged" />
        </Triggers>
        </asp:UpdatePanel>    
        
    </fieldset>



    <asp:UpdatePanel ID="UpdatePanelMensajeBusqueda" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelMensajeBusqueda" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="MensajeBusqueda" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>



    <asp:Panel ID="PanelListaPert" runat="server" Visible="false">
    <fieldset>
            <asp:UpdatePanel ID="UpdatePanelSolicitudes" UpdateMode="Conditional" runat="server">
                <ContentTemplate>      

                    <asp:GridView 
                        ID="GridVwHeaderChckbox" 
                        DataKeyNames="id"
                        runat="server" 
                        AutoGenerateColumns="False" 
                        AllowPaging="true"
                        PageSize="30"
                        OnPageIndexChanging="GridVwHeaderChckbox_PageIndexChanged"
                        AllowSorting="true"
                        CellPadding="4" 
                        ForeColor="#333333"
                        TabIndex="1"
                        GridLines="None" 
                        CssClass="mGrid"
                        OnRowDataBound="GridVwHeaderChckbox_RowDataBound"
                        PagerStyle-CssClass="pgr"
                        OnRowCommand="GridVwHeaderChckbox_RowCommand"
                        OnRowCreated="GridVwHeaderChckbox_RowCreated">
                        
                        <Columns>

                            <asp:TemplateField ItemStyle-Width="40px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll2" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEmp" runat="server" CausesValidation="false" CssClass="HiddenText" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'  AutoPostBack="true" OnCheckedChanged="chkEmp_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="NumPert/Identificador">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="HiddenIdSolConcesion" Value='<%#Eval("id") %>' />
                                    <%# DataBinder.Eval(Container, "DataItem.numPert")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tipo Trámite">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.nombreTipoTramite")%>
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

             
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </asp:Panel>

    
    <!-- DIALOGOS ------------------------------------------------------------------------------------------------------------------------------------------------------>
    
    <div id="msgGuardar" class="message">
        <div class="background"></div>
        <div class="content_message">
            <div class="msgForm_div1"><asp:Image ID="Ico_ok" ImageUrl="~/App_Themes/admin_style/images/ico_ok.png" runat="server" /></div>
            <div class="msgForm_div2">
            <table class="msgFormGuardar" cellpadding="0px" cellspacing="0px">
            <tr>
                <td style="vertical-align:middle;">Se han modificado satisfactoriamente los pert asociados al usuario.</td>
            </tr>
            </table>            
            </div>
            <div class="msgForm_div3">
            <input id="Continuar" type="button" value="Continuar modificando" onclick="close_message('msgGuardar');" />
            <asp:Button ID="Finalizar" Text="Finalizar" OnClick="Finalizar_Click" OnClientClick="javascript:close_message('msgGuardar');" CausesValidation="false" runat="server" /></div>
        </div>
    </div>
    
    <div id="adminPert" class="dialog">
        <div class="background"></div>
        <div class="content_dialog">
            <div class="top">
                <asp:LinkButton ID="cerrar_adminPert" CssClass="cerrar" OnClientClick="javascript:close_dialog('adminPert');" CausesValidation="false" runat="server"></asp:LinkButton>
            </div>
            <div class="body">
                <fieldset>
                    <legend>Pert/Identificadores asignados al usuario</legend>
                    <iframe id="iframe_adminPert" src="" width="100%" height="550px" frameborder="0" scrolling="no"></iframe>
                </fieldset>
            </div>
        </div>
    </div>
    
</asp:Content>

