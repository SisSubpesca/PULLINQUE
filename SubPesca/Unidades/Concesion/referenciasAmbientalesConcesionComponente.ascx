<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="referenciasAmbientalesConcesionComponente.ascx.cs" Inherits="SubPesca.Unidades.Concesion.referenciasAmbientalesConcesionComponente" %>

    <%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

    <asp:ToolkitScriptManager ID="ToolkitScriptManagerSolicitante" runat="server" EnablePartialRendering="false"></asp:ToolkitScriptManager>
    
    <asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>
   
    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
               <span id="titulo_modulo">Referencias Ambientales <asp:Label ID="NombreUnidadEspacial" 
                    runat="server"></asp:Label></span></td>
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


    <asp:UpdatePanel ID="UpdatePanelAmbiental" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 

    <asp:Panel ID="PanelAmbiental"  Visible="true" runat="server" CssClass="Content_Grilla">
    <asp:GridView 
           ID="GridViewAmbiental"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           AllowPaging="True" PageSize="10" OnPageIndexChanging="GridViewAmbiental_PageIndexChanged"
           AllowSorting="True" 
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%">
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
               
                <asp:TemplateField HeaderText="Region"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Region") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Titular Infa"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TitularInfa") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="SpCultivo"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SpCultivo") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Laboratorio"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Laboratorio") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="FechaMuestreoSedimiento" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FechaMuestreoSedimiento") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="RecepOfReg" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RecepOfReg") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="TipoInfa" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TipoInfa") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                      
                <asp:TemplateField HeaderText="Categoria"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("Categoria")) %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="AMS"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AMS") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Calificacion"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("Calificacion")) %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Observaciones"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("Observaciones")) %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Institucion"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Institucion") %>'></asp:Label>
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