<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="detalleEstado.aspx.cs" 
Inherits="SubPesca.Administrador.Reportes.detalleEstado" Theme="admin_style" %>
         
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Detalle del Estado</span>
        </td>
        <td align="right" valign="middle">
            <asp:ImageButton ID="Volver" ImageUrl="~/App_Themes/admin_style/images/volver.png" OnClick="Volver_Click" Height="30px" ToolTip="Volver" runat="server" />
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />

    <fieldset>
        <legend>Información del estado</legend>
        <br />
        <table cellpadding="0px" cellspacing="4px">
        <tr>
            <td style="width:50px"><span class="item">Nº Estado</span></td>
            <td style="width:10px"><span class="item">:</span></td>
            <td><asp:Label ID="NumEstado" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><span class="item">Estado</span></td>
            <td><span class="item">:</span></td>
            <td><asp:Label ID="Estado" runat="server" Text=""></asp:Label></td>
        </tr>
        </table>     
    </fieldset>
    
    <fieldset>
        <legend>Detalle del filtro de búsqueda</legend>
        <br />
        <table cellpadding="0px" cellspacing="4px">
        <tr>
            <td style="width:50px"><span class="item"><asp:Label ID="txtRegion" runat="server" Text="Región"></asp:Label></span></td>
            <td style="width:10px"><span class="item">:</span></td>
            <td><asp:Label ID="Region" runat="server" Text="Todas las regiones"></asp:Label></td>
        </tr>
         <tr id="tr_Pert" visible="false" runat="server">
            <td><span class="item">N° Pert/Identificador Solicitud</span></td>
            <td><span class="item">:</span></td>
            <td><asp:Label ID="Pert" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr id="tr_provincia" visible="false" runat="server">
            <td><span class="item">Provincia</span></td>
            <td><span class="item">:</span></td>
            <td><asp:Label ID="Provincia" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr id="tr_comuna" visible="false" runat="server">
            <td><span class="item">Comuna</span></td>
            <td><span class="item">:</span></td>
            <td><asp:Label ID="Comuna" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr id="tr_tiposolic" visible="false" runat="server">
            <td><span class="item">Tipo de Solicitud</span></td>
            <td><span class="item">:</span></td>
            <td><asp:Label ID="TipoSolic" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr id="tr_sectorialista" visible="false" runat="server">
            <td><span class="item">Sectorialista</span></td>
            <td><span class="item">:</span></td>
            <td><asp:Label ID="Sectorialista" runat="server" Text=""></asp:Label></td>
        </tr>
        </table>  
    </fieldset>  
    
    <fieldset>
        <legend>Solicitudes <asp:Label ID="TipoSolicitud" runat="server" Text=""></asp:Label></legend>
        <asp:UpdatePanel ID="upd1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla" runat="server"></asp:Label>
                </div>
            </asp:Panel>
                                                      
            <asp:GridView ID="GridView1" runat="server" 
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                DataKeyNames="IdSolicitud"
                AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanged"
                AllowSorting="true" OnSorting="GridView1_Sorting"
                OnRowCommand="GridView1_RowCommand"
                OnRowDataBound="GridView1_RowDataBound"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr" >
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="ID Solicitud" DataField="IdSolicitud" SortExpression="IdSolicitud" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Nro Pert | Número Identificador Solicitud" DataField="NroPert" SortExpression="NroPert" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Titular" DataField="TitularNombre" SortExpression="TitularNombre" ItemStyle-Width="150px" />
                    <asp:BoundField HeaderText="Fecha Ingreso" DataField="FechaHoraIngresoTramite" SortExpression="FechaHoraIngresoTramite" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd'/'MM'/'yyyy}" />
                    <asp:BoundField HeaderText="Comuna" DataField="Comuna" SortExpression="Comuna" ItemStyle-Width="120px" />                    
                    <asp:BoundField HeaderText="Superficie" DataField="AreaTotalSolicitada" SortExpression="AreaTotalSolicitada" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Sector solicitado" DataField="Toponimio" SortExpression="Toponimio" ItemStyle-Width="87px" />
                    <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdSolicitud") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/form.png" Height="20px" AlternateText="Ver Solicitud" ToolTip="Ver Solicitud" />
                            <asp:ImageButton ID="gHistorial" Visible="false" runat="server" CausesValidation="false" CommandName="Historial" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdSolicitud") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/historial.jpg" Height="20px" AlternateText="Historial de cambios de estado" ToolTip="Historial de cambios de estado" />
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
            
            <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
        </Triggers>
        </asp:UpdatePanel>

        <asp:HiddenField ID="IdEstado" runat="server" /> 
        <asp:HiddenField ID="Tipo" runat="server" />
    </fieldset>  
    
</asp:Content>
