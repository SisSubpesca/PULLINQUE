<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="detalleSolicitud.aspx.cs" 
         Inherits="SubPesca.Administrador.Reportes.detalleSolicitud" Theme="admin_style" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Detalle de la Solicitud</span>
        </td>
        <td align="right" valign="middle">
            <asp:ImageButton ID="Volver" ImageUrl="~/App_Themes/admin_style/images/volver.png" OnClick="Volver_Click" Height="30px" ToolTip="Volver" Visible="false" runat="server" />
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />    
    
    <fieldset>
        <legend>Información de la solicitud</legend>
        <br />
        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Nº Solicitud</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:Label ID="IdSolicitud" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nº Pert</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:Label ID="Pert" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Estado</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:Label ID="Estado" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Titulares</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:Label ID="Titulares" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Fecha ingreso solicitud</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:Label ID="FechaIngreso" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Comuna</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:Label ID="Comuna" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Superficie</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:Label ID="Superficie" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Sector solicitado</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:Label ID="Sector" runat="server" Text=""></asp:Label></td>
        </tr>
        </table>
        <asp:HiddenField ID="IdEstado" runat="server" />  
    </fieldset>        
        
    <fieldset>
    <legend>Estructuras</legend>
        <asp:UpdatePanel ID="upd1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>       
            <asp:Panel ID="Content_msgGrilla1" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrilla1" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla1" runat="server"></asp:Label>
                </div>
            </asp:Panel>
            
            <asp:GridView ID="GridView1" runat="server" 
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                DataKeyNames="IdEstructuraTecnica"
                AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanged"
                AllowSorting="true" OnSorting="GridView1_Sorting"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr" >
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="Tipo de unidad de cultivo" DataField="EstructuraTecnica" SortExpression="EstructuraTecnica" ItemStyle-Width="220px" />
                    <asp:BoundField HeaderText="Tipo de Forma" DataField="NombreTipoForma" SortExpression="EstructuraTecnica" ItemStyle-Width="220px" />
                    <asp:BoundField HeaderText="Cantidad 1er año" DataField="CantidadPrimerAno" SortExpression="CantidadPrimerAno" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Right" />
                    
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#5794EF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            
            <asp:Button ID="ExportarGrilla1" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" /> 
            <asp:HiddenField ID="KeySort1" runat="server" /> 
        </ContentTemplate> 
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla1" />
        </Triggers>
        </asp:UpdatePanel>        
    </fieldset>
    
    <fieldset>
    <legend>Producción</legend>
        <asp:UpdatePanel ID="upd2" UpdateMode="Conditional" runat="server">
        <ContentTemplate>       
            <asp:Panel ID="Content_msgGrilla2" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrilla2" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla2" runat="server"></asp:Label>
                </div>
            </asp:Panel>
            
            <asp:GridView ID="GridView2" runat="server" 
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                DataKeyNames="IdProgramaProduccion"
                AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView2_PageIndexChanged"
                AllowSorting="true" OnSorting="GridView2_Sorting"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr" >
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="Especie / Grupo de Especie" DataField="TipoEspecie" SortExpression="TipoEspecie" ItemStyle-Width="377px" />
                    <asp:BoundField HeaderText="Nº Ejemplares" DataField="NroEjemplares" SortExpression="NroEjemplares" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField HeaderText="Peso Promedio" DataField="PesoPromedio" SortExpression="PesoPromedio" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Right" />
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#5794EF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            
            <asp:Button ID="ExportarGrilla2" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" /> 
            <asp:HiddenField ID="KeySort2" runat="server" />  
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla2" />
        </Triggers>
        </asp:UpdatePanel>
    </fieldset>
    
    <fieldset>
    <legend>Vértices</legend>
        <asp:UpdatePanel ID="upd3" UpdateMode="Conditional" runat="server">
        <ContentTemplate>       
            <asp:Panel ID="Content_msgGrilla3" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrilla3" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla3" runat="server"></asp:Label>
                </div>
            </asp:Panel>
            
            <asp:GridView ID="GridView3" runat="server" 
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                DataKeyNames="IdVertice"
                AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView3_PageIndexChanged"
                AllowSorting="true" OnSorting="GridView3_Sorting"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr" >
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="Vertice" DataField="Vertice" SortExpression="Vertice" ItemStyle-Width="50px" />
                    <asp:BoundField HeaderText="Carta Geográfica" DataField="CartaGeografica" SortExpression="CartaGeografica" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Datum" DataField="Datum" SortExpression="Datum" ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="UTMNorte" DataField="UTMNorte" SortExpression="UTMNorte" ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField HeaderText="UTMEste" DataField="UTMEste" SortExpression="UTMEste" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField HeaderText="Latitud" DataField="Latitud" SortExpression="Latitud" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Longitud" DataField="Longitud" SortExpression="Longitud" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#5794EF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            
            <asp:Button ID="ExportarGrilla3" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" /> 
            <asp:HiddenField ID="KeySort3" runat="server" /> 
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla3" />
        </Triggers>
        </asp:UpdatePanel> 
    </fieldset>    
    
    <asp:HiddenField ID="BackPage" runat="server" />  

</asp:Content>
