<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="logEstadosSolicitud.aspx.cs" 
         Inherits="SubPesca.Administrador.Reportes.logEstadosSolicitud" Theme="admin_style" %>
         
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Histórico de cambios de estado</span>
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
        </table>     
    </fieldset>
    
    
    <fieldset>
        <legend>Histórico: cambios de estado de la solicitud</legend>
        <asp:UpdatePanel ID="upd1" UpdateMode="Always" runat="server">
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
                AutoGenerateColumns="False" 
                CellPadding="4" 
                ForeColor="#333333" 
                GridLines="None"
                DataKeyNames="idHistEstados"
                AllowPaging="True" PageSize="30" 
                OnPageIndexChanging="GridView1_PageIndexChanged"
                AllowSorting="false"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr" >
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="Id Log" DataField="idHistEstados" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Fecha" DataField="fechaInsercion" ItemStyle-Width="110px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Estado" DataField="nombreEstado" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />  
                    <asp:BoundField HeaderText="Motivo de Cambio" DataField="motivoCambio" ItemStyle-Width="477px" />
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#5794EF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>

            <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" />            
            <asp:HiddenField ID="KeySort" runat="server" />
        </ContentTemplate> 
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
        </Triggers>
        </asp:UpdatePanel>
        <asp:HiddenField ID="IdEstado" runat="server" />
        
    </fieldset> 
    
    <asp:HiddenField ID="BackPage" runat="server" />      
    
</asp:Content>
