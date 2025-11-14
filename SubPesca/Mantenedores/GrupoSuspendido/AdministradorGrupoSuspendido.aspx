<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministradorGrupoSuspendido.aspx.cs" Inherits="SubPesca.Mantenedores.GrupoSuspendido.AdministradorGrupoSuspendido" 
MasterPageFile="~/Administrador/SitioAdmin.Master" Theme="admin_style"%>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Administrador Grupo Suspendido</span>
            </td>
        </tr>
    </table>

    <fieldset>

    <asp:ValidationSummary ID="ValidationSummaryGrupoSuspendido" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />    

    <asp:ToolkitScriptManager ID="ToolkitScriptManageAdministracionContactos" runat="server" EnablePartialRendering="true" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>


       <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1"><span class="item">Nombre Grupo Suspendido</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3" colspan="4">
                    <asp:UpdatePanel ID="UpdatePanelNPert" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="NombreGrupoSus" Width="80px" AutoPostBack="true" runat="server"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="col1"><span class="item">Seleccione Tipo Grupo Suspendido</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:UpdatePanel ID="UpdatePanelUnidadEsp" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="TipoGrupoSuspendido"  runat="server" AutoPostBack="true" ></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
       </table>

       <table class="form" cellpadding="0px" cellspacing="0px">   
            <tr>
                <td class="col1"></td>
                <td class="col2"></td>
                <td class="col3">
                    <asp:Button ID="BuscarGrupoSuspendido"  runat="server" Text="Buscar Grupo Suspendido"   CausesValidation="true" OnClick="_BuscarGrupoSuspendido" />
                    <asp:Button ID="NuevoGrupoSuspendido"  runat="server" Text="Nuevo Grupo Suspendido"   CausesValidation="true" OnClick="_NuevoGrupoSuspendido" />
                </td>
            </tr>
       </table>
       
     
        <asp:GridView ID="GridGrupoSuspendido" 
        runat="server"
        AutoGenerateColumns="False" 
        CellPadding="4" 
        ForeColor="#333333" 
        GridLines="None"
        AllowPaging="True" PageSize="10"
        OnPageIndexChanging="GridGrupoSuspendido_PageIndexChanged"
        CssClass="mGrid"
        OnRowDataBound="_GridGrupoSuspendido_RowDataBound"
        OnRowCommand="_GridGrupoSuspendido_RowCommand"
        PagerStyle-CssClass="pgr"
        Width="100%">
            <Columns>
                <asp:TemplateField Visible=false>
                    <ItemTemplate>
                        <asp:Label HeaderText="EstadoVigencia" ID="EstadoVigencia" runat="server" Visible="false" Text='<%# (DataBinder.Eval(Container, "DataItem.estadoVigencia.id")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label HeaderText="id" ID="id" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.idGrupoSuspend")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                      
                <asp:TemplateField HeaderText="Nombre Grupo Suspendido">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.nombreGrupoSuspend")%>
                    </ItemTemplate>
                </asp:TemplateField>
                        
                <asp:TemplateField HeaderText="Tipo Agrupacion"> 
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.tipoAgrupacion.descripcion")%>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Pert Asociados"> 
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.pertAsignados")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Estado Asociación">
                   <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.estadoVigencia.descripcion")%>
                   </ItemTemplate> 
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="70px">
                    <ItemTemplate>
                        <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" 
                             ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" 
                             CommandArgument='<%# DataBinder.Eval(Container, "DataItem.idGrupoSuspend")%>' />

                        <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar"
                             ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar"
                             CommandArgument='<%# DataBinder.Eval(Container, "DataItem.idGrupoSuspend")%>'/>

                        <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" 
                             ImageUrl="../../App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" 
                             CommandArgument='<%# DataBinder.Eval(Container, "DataItem.idGrupoSuspend")%>' />
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
    </fieldset>
</asp:Content>