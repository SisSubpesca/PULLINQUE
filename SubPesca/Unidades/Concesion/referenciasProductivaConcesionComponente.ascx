<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="referenciasProductivaConcesionComponente.ascx.cs" Inherits="SubPesca.Unidades.Concesion.referenciasProductivaConcesionComponente" %>

 <%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

    <asp:ToolkitScriptManager ID="ToolkitScriptManagerSolicitante" runat="server" EnablePartialRendering="false"></asp:ToolkitScriptManager>
    
    <asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>
   
    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
               <span id="titulo_modulo"> Referencias Productivas 
                   <asp:Label ID="NombreUnidadEspacial" runat="server"></asp:Label></span>
            </td>
        </tr>
    </table>

    <hr style="width:100%;" />
    
    <fieldset>

    <asp:Panel ID="PanelSolicitudesMsg"  Visible="false" runat="server">
    <div class="msgGrilla_div1">
        <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
    </div>
    <div class="msgGrilla_Solicitud">
        <asp:Label ID="msgGrilla" runat="server"></asp:Label>
    </div>
    </asp:Panel>       

    <asp:UpdatePanel ID="UpdatePanelProductiva" UpdateMode="Conditional" runat="server">
    <ContentTemplate>     


    <asp:Panel ID="PanelProductiva"  Visible="true" runat="server" CssClass="Content_Grilla">
    <asp:GridView 
           ID="GridViewProductiva"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           AllowPaging="True" PageSize="10" OnPageIndexChanging="GridViewProductiva_PageIndexChanged"
           AllowSorting="True" 
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%">
           <RowStyle BackColor="#EFF3FB" />
           <Columns>

                <asp:TemplateField HeaderText="CdCentro"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.CdCentro")%>
                    
                </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="NrAno" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.NrAno")%>
                    
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="NrMes" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.NrMes")%>
                    
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CdEspecie"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.CdEspecie")%>
                    
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="NmEspecie" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.NmEspecie")%>
                    
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="NmEtapaDesarrollo"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.NmEtapaDesarrollo")%>
                    
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="NrUnidades"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.NrUnidades")%>
                    
                </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="NrKilos" HeaderText="NrKilos"   DataFormatString="{0:N0}" />
                
                <asp:BoundField DataField="PesoPromedio" HeaderText="PesoPromedio"  DataFormatString="{0:N0}" />

                <asp:TemplateField HeaderText="CdDestino"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.CdDestino")%>
                    
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="NmDesOri"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.NmDesOri")%>
                    
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="NrDestino"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.NrDestino")%>
                    
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OrigenDestino"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.OrigenDestino")%>
                    
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tipo"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.Tipo")%>
                    
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.Fecha")%>
                    
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Id" SortExpression="Id" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.Id")%>
                    
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Origen" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.Origen")%>
                    
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
       <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click"  Visible="false" />  

    </asp:Panel>
    </ContentTemplate>

    <Triggers>
        <asp:PostBackTrigger ControlID="ExportarGrilla" />
    </Triggers>
    </asp:UpdatePanel>

    </fieldset>