<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="referenciasSanitariasConcesionComponente.ascx.cs" Inherits="SubPesca.Unidades.Concesion.referenciasSanitariasConcesionComponente" %>

 <%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

    <asp:ToolkitScriptManager ID="ToolkitScriptManagerSolicitante" runat="server" EnablePartialRendering="false"></asp:ToolkitScriptManager>
    
    <asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>
   
    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
               <span id="titulo_modulo"> Referencias Sanitarias 
                   <asp:Label ID="NombreUnidadEspacial" runat="server"></asp:Label></span>
            </td>
        </tr>
    </table>

    <hr style="width:100%;" />

    <fieldset>
    <legend>Descanso Sanitario ACS</legend>
    
    
    
    <asp:Panel ID="PanelSolicitudesMsg"  Visible="false" runat="server">
    <div class="msgGrilla_div1">
        <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
    </div>
    <div class="msgGrilla_Solicitud">
        <asp:Label ID="msgGrilla" runat="server"></asp:Label>
    </div>
    </asp:Panel>
    
    <asp:UpdatePanel ID="UpdatePanelDescansosSanitarios" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 

    <asp:Panel ID="PanelDescansosSanitarios"  Visible="false" runat="server">
    <asp:GridView ID="GridViewDescanso" runat="server"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                DataKeyNames="idDescanso"
                AllowPaging="True" PageSize="20" OnPageIndexChanging="GridViewDescanso_PageIndexChanged"
                AllowSorting="true" OnSorting="GridViewDescanso_Sorting"
                CssClass="mGrid_dialog"
                PagerStyle-CssClass="pgr" 
                Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="Tipo de Operación" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gTipoOperacion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tipoOperacionString") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Barrio" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gBarrio" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "barrioString") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="N° de Operación"  ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gNumeroOperacion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "numOperacion") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha Inicio" ItemStyle-Width="458px">
                         <ItemTemplate>
                            <asp:Label ID="gFechaInicio" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fechaInicio") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Fecha Fin" ItemStyle-Width="458px">
                         <ItemTemplate>
                            <asp:Label ID="gFechaFin" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fechaFin") %>'></asp:Label>
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
            <asp:HiddenField ID="KeySort" runat="server" /> 
    </asp:Panel>
    </ContentTemplate>

    <Triggers>
        <asp:PostBackTrigger ControlID="ExportarGrilla" />
    </Triggers>
    </asp:UpdatePanel>

    </fieldset>



     <br />


     <fieldset>
     <legend>Información Sanitaria</legend>

     

    <asp:Panel ID="PanelMensajeSanitaria"  Visible="false" runat="server">
    <div class="msgGrilla_div1">
        <asp:Image ID="ImageSanitaria" CssClass="Ico_msgGrilla" runat="server" />
    </div>
    <div class="msgGrilla_Solicitud">
        <asp:Label ID="msgGrillaSanitaria" runat="server"></asp:Label>
    </div>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanelSanitaria" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 

    <asp:Panel ID="PanelSanitaria"  Visible="true" runat="server" CssClass="Content_Grilla">
    <asp:GridView 
           ID="GridViewSanitaria"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           AllowPaging="True" PageSize="10" OnPageIndexChanging="GridViewSanitaria_PageIndexChanged"
           AllowSorting="True" 
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%">
           <RowStyle BackColor="#EFF3FB" />
           <Columns>

                <asp:TemplateField HeaderText="IdCaligus"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                
                    <%# DataBinder.Eval(Container, "DataItem.IdCaligus")%>
                </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Macrozona"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                
                    <%# DataBinder.Eval(Container, "DataItem.Macrozona")%>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ACS"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                
                    <%# DataBinder.Eval(Container, "DataItem.ACS")%>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="EmpresaOperadora"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.EmpresaOperadora")%>
                    
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CodigoCentro" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                
                    <%# DataBinder.Eval(Container, "DataItem.CodigoCentro")%>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="NombreCentro"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("NombreCentro")) %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Semana"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                
                    <%# DataBinder.Eval(Container, "DataItem.Semana")%>
                </ItemTemplate>
                </asp:TemplateField>
                      
                <asp:TemplateField HeaderText="Especie"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                
                    <%# DataBinder.Eval(Container, "DataItem.Especie")%>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="TotalPeces"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                
                    <%# DataBinder.Eval(Container, "DataItem.TotalPeces")%>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PesoPromedioGramo" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                
                    <%# DataBinder.Eval(Container, "DataItem.PesoPromedioGramo")%>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PromJV" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                
                    <%# DataBinder.Eval(Container, "DataItem.PromJV")%>
                </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="PromHO"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                
                    <%# DataBinder.Eval(Container, "DataItem.PromHO")%>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PromAD" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                
                    <%# DataBinder.Eval(Container, "DataItem.PromAD")%>
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
       <asp:Button ID="ExportarGrilla2" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla2_Click"  Visible="false" />  
    </asp:Panel>
    </ContentTemplate>
    
    <Triggers>
        <asp:PostBackTrigger ControlID="ExportarGrilla2" />
    </Triggers>
    </asp:UpdatePanel>

    </fieldset>