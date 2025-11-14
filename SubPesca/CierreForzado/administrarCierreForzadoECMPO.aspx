<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="administrarCierreForzadoECMPO.aspx.cs" 
Inherits="SubPesca.CierreForzado.administrarCierreForzadoECMPO"  Theme="admin_style" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Reference Page="GenerarCierreForzadoColectores.aspx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></asp:ToolkitScriptManager>


    
    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Administrar Cierres Forzados Solicitud ECMPO</span>
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />

    <fieldset>
        <legend>Búsqueda de Informes Técnico de Rechazo</legend>


    <asp:UpdatePanel ID="UpdatePanelMensajeSuperior" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelMensajeSuperior" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="MensajeSuperior" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>



    <asp:Panel ID="FormularioBusqueda" runat="server" Visible="true">
             
        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Infome Técnico de Cierre</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3" colspan="4">
                <asp:UpdatePanel ID="UpdatePanelInformeTecnicoCierre" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                         <asp:TextBox ID="InformeTecnicoCierre" runat="server" onKeyUp="return onlyNumeric(this)"  MaxLength="8" ></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        </table>

        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Nº Pert</span></td>
            <td class="col2"><span class="item">:</span></td>  
            <td class="col3" colspan="4"><asp:TextBox ID="NPert" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        </table>

        <table class="form" cellpadding="0px" cellspacing="0px">   
        <tr>
            <td class="col1"></td>
            <td class="col2"></td>
            <td class="col3"><asp:Button ID="Buscar" runat="server" Text="Buscar"  CausesValidation="true"  OnClick="FiltrarCargaGrilla" /></td>
        </tr>
        </table>
    </asp:Panel>


    


    <asp:UpdatePanel ID="UpdatePanelSolicitudes" UpdateMode="Conditional" runat="server">
        <ContentTemplate>      
            <asp:Panel ID="PanelSolicitudesMsg"  Visible="false" runat="server">
                     <div class="msgGrilla_div1">
                        <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
                    </div>
                    <div class="msgGrilla_Solicitud">
                        <asp:Label ID="msgGrilla_Sol" runat="server"></asp:Label>
                    </div>
                   
            </asp:Panel>                  
                   

                    <asp:GridView ID="GridCierreForzado"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" 
                       PageSize="10" 
                       CssClass="mGrid"
                       OnPageIndexChanging="GridCierreForzado_PageIndexChanged"
                       OnRowDataBound="GridCierreForzado_RowDataBound"
                       OnRowCommand="GridCierreForzado_RowCommand"
                       PagerStyle-CssClass="pgr"
                       Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Informe Tecnico">
                                <ItemTemplate>
                                   <%#DataBinder.Eval(Container.DataItem, "nombreTipo") %>  <%#DataBinder.Eval(Container.DataItem, "numero") %>  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Fecha Del Informe" DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:TemplateField HeaderText="Cantidad Solicitudes">
                                <ItemTemplate>
                                     <%#DataBinder.Eval(Container.DataItem, "solicIT") %>  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Solicitudes con SPP">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "solicSSP") %>  
                                </ItemTemplate>
                            </asp:TemplateField>
                          

                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gModificar" Visible="true" runat="server" CausesValidation="false" CommandName="AsignarResolucionSSP" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocCierre") %>'
                                         ImageUrl="../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver Cierre Forzado" ToolTip="Ver Cierre Forzado" />

                                          <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="EliminarCierreForzado" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocCierre") %>'
                                         ImageUrl="../App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Eliminar Cierre Forzado" ToolTip="EliminarCierreForzado" />
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
                   <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" Visible="false" />
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
        </Triggers>

    </asp:UpdatePanel>



    </fieldset>


    

</asp:Content>