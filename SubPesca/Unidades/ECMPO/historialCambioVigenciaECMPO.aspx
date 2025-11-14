<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="historialCambioVigenciaECMPO.aspx.cs" Inherits="SubPesca.Unidades.ECMPO.historialCambioVigenciaECMPO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="dialog_historialCambioVigenciaECMPO" runat="server">
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
                     <asp:TemplateField HeaderText="Resolución">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.cadena")%>
                                </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha Cambio Estado">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.fechaIngresoSist")%>
                                </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado de Documento">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.estadoVigencia.descripcion")%>
                                </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Observaciones">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.observaciones")%>
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