<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="historialSolicitudes.aspx.cs" Inherits="SubPesca.Unidades.Concesion.historialSolicitudes" Theme="admin_style" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="dialog_historial" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <asp:UpdatePanel ID="updPanel" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
                                                                   
            <asp:GridView 
               ID="GridNombres"
               runat="server"
               AutoGenerateColumns="False" 
               CellPadding="4" 
               ForeColor="#333333" 
               GridLines="None"
               CssClass="mGrid"
               PagerStyle-CssClass="pgr"
               Width="100%"
               OnRowCommand="GridNombres_RowCommand"
               OnRowDataBound="GridNombres_RowDataBound"
               AllowPaging="True" PageSize="5" OnPageIndexChanging="GridNombres_PageIndexChanged">
                <RowStyle BackColor="#EFF3FB"/>
                <Columns>
                    <asp:BoundField HeaderText="Id Solicitud" DataField="idSolConcesion" ReadOnly="true" ItemStyle-Width="60px" />
                    
                    <asp:TemplateField HeaderText="Código de Centro">
                        <ItemTemplate>
                            
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Fecha Cambio Estado" DataField="fechaIngresoTramite" ReadOnly="true" ItemStyle-Width="60px" />

                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Observaciones">
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
                       
      </ContentTemplate> 
      </asp:UpdatePanel>

      <asp:HiddenField ID="IdSolConcesion" runat="server" />
      
    </form>
</body>
</html>
