<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="adminPert.aspx.cs"  Inherits="SubPesca.Administrador.Usuarios.adminPert" Theme="admin_style" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Página sin título</title>

</head>
<body>
    <form id="dialog_gruposusuarios" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <br />
        
        <asp:HiddenField ID="IdUSuarioCampo" runat="server" />


        <asp:UpdatePanel ID="upd2" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
           
                                                      
            <asp:GridView ID="GridView1" runat="server" 
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                DataKeyNames="idSolConcesion"
                AllowPaging="True" 
                PageSize="20" 
                OnPageIndexChanging="GridView1_PageIndexChanged"
                AllowSorting="true" 
                OnRowDataBound="GridView1_RowDataBound"
                CssClass="mGrid_dialog"
                PagerStyle-CssClass="pgr" >
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    
                    <asp:TemplateField HeaderText="Tipo Tramite" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gTipo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nombreTipo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    
                    <asp:TemplateField HeaderText="Pert/Identificador de la Solicitud"  ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gPert" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "numPert") %>'></asp:Label>
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
            
            <asp:HiddenField ID="KeySort" runat="server" />           
      </ContentTemplate> 
      </asp:UpdatePanel>
      
    </form>
</body>
</html>
