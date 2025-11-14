<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="detalleCentroCultivoP3.aspx.cs" 
Inherits="SubPesca.Administrador.ReportesP3.detalleCentroCultivoP3" Theme="admin_style" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Detalle del Centro de Cultivo</span>
        </td>
        <td align="right" valign="middle">
            <asp:ImageButton ID="Volver" ImageUrl="~/App_Themes/admin_style/images/volver.png" OnClick="Volver_Click" Height="30px" ToolTip="Volver" runat="server" />
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />

    <fieldset>
        <legend>Información del centro de cultivo</legend>

        <asp:HiddenField ID="IdSolConcesion" runat="server" />
        <br />
        <table cellpadding="0px" cellspacing="4px">
        <tr>
            <td><span class="item">Código de Centro de Cultivo</span></td>
            <td><span class="item">:</span></td>
            <td><asp:Label ID="IdCentroCultivo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><span class="item">Titulares</span></td>
            <td><span class="item">:</span></td>
            <td><asp:Label ID="Titulares" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><span class="item">Comuna</span></td>
            <td><span class="item">:</span></td>
            <td><asp:Label ID="Comuna" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><span class="item">Especies</span></td>
            <td><span class="item">:</span></td>
            <td><asp:Label ID="EAutorizadas" runat="server" Text=""></asp:Label></td>
        </tr>
        </table>     
    </fieldset>    

    <fieldset>
        <legend>Filtro</legend>
        <br />
        <table cellpadding="0px" cellspacing="4px">
        <tr>
            <td class="col1"><span class="item">Periodo mensual</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:DropDownList ID="Mes" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="Anio" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="col1"></td>
            <td class="col2"></td>
            <td class="col3">
                <asp:Button ID="Filtrar" runat="server" OnClick="Filtrar_Click" Text="Filtrar" OnClientClick="javascript:muestra_loading('cargando');" />
            </td>
        </tr>
        </table>
    </fieldset>
    
    <div id="cargando" class="message">
        <div class="background"></div>
        <div style="width:100%; text-align:center; margin-top:350px;">
            <asp:Image ID="Image1" ImageUrl="~/App_Themes/admin_style/images/loading.gif" Width="100px" runat="server" />
        </div>
    </div>
    
    <asp:Panel ID="detalle" Visible="false" runat="server">
    <fieldset>
        <legend>Detalle del Centro de Cultivo</legend>
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
                                     
            <asp:Panel ID="Content_Grilla" CssClass="Content_Grilla" runat="server">                                                         
            <asp:GridView ID="GridView1" runat="server" 
                AutoGenerateColumns="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                AllowPaging="True" PageSize="12" OnPageIndexChanging="GridView1_PageIndexChanged"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr">
                <RowStyle BackColor="#EFF3FB" />
                <FooterStyle BackColor="#004080" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#5794EF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            </asp:Panel>
            
            <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
        </Triggers>
        </asp:UpdatePanel>
    </fieldset>  
    </asp:Panel>
    
</asp:Content>
