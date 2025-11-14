<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="arrendatarioUnidadEspacialComponente.ascx.cs" Inherits="SubPesca.Unidades.Concesion.arrendatarioUnidadEspacialComponente" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>



<asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>

<asp:Panel ID="PanelArrendatario"  Visible="true" runat="server" CssClass="Content_Grilla">
<asp:GridView 
           ID="GridViewArrendatarios"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           AllowPaging="True" PageSize="10" OnPageIndexChanging="GridViewArrendatarios_PageIndexChanged"
           OnRowCreated="GridViewArrendatarios_RowCreated"
           AllowSorting="True" 
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%">
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
               
               <asp:TemplateField HeaderText="NumeroID" SortExpression="NumeroID" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                    <%# DataBinder.Eval(Container, "DataItem.NumeroID")%>
                    
                </ItemTemplate>
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="CODIGO_CENTRO" SortExpression="CODIGO_CENTRO" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                <%# DataBinder.Eval(Container, "DataItem.CODIGO_CENTRO")%>
                    
                </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="FECHA_INSCRIPCION" SortExpression="FECHA_INSCRIPCION" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                
                <%# DataBinder.Eval(Container, "DataItem.FECHA_INSCRIPCION")%>    
                </ItemTemplate>
               </asp:TemplateField>

              <asp:TemplateField HeaderText="Desde" SortExpression="Desde" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.Desde")%>
                    
                </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="ARRENDATARIO_NOMBRE" SortExpression="ARRENDATARIO_NOMBRE" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.ARRENDATARIO_NOMBRE")%>
                    
                </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="ARRENDATARIO_RUT_INT" SortExpression="ARRENDATARIO_RUT_INT" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.ARRENDATARIO_RUT_INT")%>-<%# DataBinder.Eval(Container, "DataItem.ARRENDATARIO_RUT_DV")%>
                    
                </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="DURACION_DESDE" SortExpression="DURACION_DESDE" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.DURACION_DESDE")%>
                    
                </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="DURACION_HASTA" SortExpression="DURACION_HASTA" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.DURACION_HASTA")%>
                    
                </ItemTemplate>
               </asp:TemplateField>
                      
                <asp:TemplateField HeaderText="RENOVAR_AUTOMATICAMENTE" SortExpression="RENOVAR_AUTOMATICAMENTE" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.RENOVAR_AUTOMATICAMENTE")%>
                    
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