<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="verBitacoraCambios.aspx.cs" 
Inherits="SubPesca.Bitacora.verBitacoraCambios" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></asp:ToolkitScriptManager>


    
    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Bitácora de Cambios</span>
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />

    <fieldset>
        <legend>Búsqueda de Cambios</legend>



             
    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"><span class="item">Nº PERT</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanelNPert" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="NPert" Width="186px" AutoPostBack="true" runat="server" 
                    MaxLength="20"></asp:TextBox>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>

        </td>
    </tr>
    <tr>
        <td class="col1"><span class="item">Identificador Solicitud</span></td>
        <td class="col2">&nbsp;</td>
        <td class="col3">

        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="IdentificadorSolicitud" Width="80px" AutoPostBack="true" runat="server" 
                    onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" 
                    MaxLength="10"></asp:TextBox>
                     </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="col1"><span class="item">Código Centro</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            <asp:TextBox ID="CodigoCentro" AutoPostBack="true" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="col1"><span class="item">Campo Modificado</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            <asp:DropDownList ID="CampoModificado" AutoPostBack="true" runat="server"></asp:DropDownList>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    </table>

    <table class="form" cellpadding="0px" cellspacing="0px">   
    <tr>
        <td class="col1"></td>
        <td class="col2"></td>
        <td class="col3">
            <asp:Button ID="Limpiar" runat="server" Text="Limpiar"  CausesValidation="false" OnClick="Limpiar_Click"  />
            <asp:Button ID="Buscar" runat="server" Text="Buscar"  CausesValidation="true"  OnClick="FiltrarCargaGrilla" />
        </td>
    </tr>
    </table>


    <asp:UpdatePanel ID="UpdatePanelBitacora" UpdateMode="Conditional" runat="server">
        <ContentTemplate>      
                           
            <asp:Panel ID="PanelBitacora"  Visible="false" runat="server">
                   

                    <asp:GridView ID="GridBitacora"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" PageSize="10"
                       CssClass="mGrid"
                       PagerStyle-CssClass="pgr"
                       Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Tipo Unidad Espacial">
                                <ItemTemplate>
                                     
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Identificador Solicitud">
                                <ItemTemplate>
                                     
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Campo Modificado">
                                <ItemTemplate>
                                     
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valor">
                                <ItemTemplate>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Usuario">
                                <ItemTemplate>
                                
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