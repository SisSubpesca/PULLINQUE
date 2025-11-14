<%@ Page Language="C#"  MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" Theme="admin_style"
CodeBehind="TitularesPendientes.aspx.cs" Inherits="SubPesca.Solicitudes.TitularesPendientes.TitularesPendientes" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></asp:ToolkitScriptManager>

    <asp:Panel ID="Panel1"  Visible="true" runat="server">
    <fieldset>
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Titulares Pendientes de Creación</span>
            </td>
        </tr>
    </table>
   
    <asp:UpdatePanel ID="UpdatePanelMensajeGrilla" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

        <asp:GridView ID="GridTitularesPendientes" 
            runat="server"
            AutoGenerateColumns="False" 
            CellPadding="4" 
            ForeColor="#333333" 
            GridLines="None"
            AllowPaging="True" PageSize="10" OnPageIndexChanging="GridTitularesPendientes_PageIndexChanged"
            CssClass="mGrid"
            OnRowDataBound="GridTitularesPendientes_RowDataBound"
            OnRowCommand="GridTitularesPendientes_RowCommand"
            PagerStyle-CssClass="pgr"
            Width="100%">
                <Columns>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label HeaderText="EstadoVigencia" ID="EstadoVigencia" runat="server" Visible="false" Text='<%# (DataBinder.Eval(Container, "DataItem.idTitularRCA")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Codigo Centro"> 
                        <ItemTemplate>
                           <%# DataBinder.Eval(Container, "DataItem.CodigoCentro")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Nombre Titular">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.adquirienteNombre")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Rut Titular">
                       <ItemTemplate>
                       <%# DataBinder.Eval(Container, "DataItem.rutCompleto")%>
                       </ItemTemplate> 
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha Inscripcion">
                       <ItemTemplate>
                       <%# DataBinder.Eval(Container, "DataItem.fechaInscripcion")%>
                       </ItemTemplate> 
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Observación">
                       <ItemTemplate>
                       <%# DataBinder.Eval(Container, "DataItem.observaciones")%>
                       </ItemTemplate> 
                    </asp:TemplateField>

                    <asp:TemplateField>
                         <ItemTemplate>
                            <asp:ImageButton ID="gSolucionar" Visible="false" runat="server" CausesValidation="false" CommandName="Solucionar" 
                                 ImageUrl="../../App_Themes/admin_style/images/evaluar.png" Height="20px" AlternateText="Solucionar" ToolTip="Solucionar" 
                                 CommandArgument='<%# DataBinder.Eval(Container, "DataItem.idTitularRCA")  %>'/>
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
        <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" Visible="true" /> 
    </fieldset>
    </asp:Panel>
</asp:Content>