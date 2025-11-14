<%@ Page Language="C#"  AutoEventWireup="true"  MasterPageFile="~/Administrador/SitioAdmin.Master" CodeBehind="tester.aspx.cs" Inherits="SubPesca.Solicitudes.Registrar.tester"
Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"></asp:ToolkitScriptManager>


  



        <br />


    <!-- Archivo Adjunto -->
    <asp:UpdatePanel ID="UpdatePanel_ArchivoAdjunto" runat="server" UpdateMode="Conditional">
    <ContentTemplate>  
    <asp:Panel ID="Panel_ArchivoAdjunto"  Visible="true" runat="server">
    <fieldset>
        <legend>Archivos Adjuntos</legend>
        <br /> 
                    
        <asp:panel ID="PanelFormularioIngresoArchivoAdjunto" runat="server">
                    
        </asp:panel>                        
                   
    </fieldset>
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>


                       <asp:UpdatePanel ID="UpdatePanelArchivoAdjuntoGrilla" UpdateMode="Conditional" runat="server">
                       <ContentTemplate>
                        
                           <asp:Panel ID="PanelArchivoAdj"  Visible="true" runat="server">
                            
                            <asp:GridView  ID="GridArchivoAdjunto" 
                            runat="server" 
                            AutoGenerateColumns="False"
                            DataKeyNames="idArchivoBin"  
                            CellPadding="4" 
                            ForeColor="#333333"
                            GridLines="None"
                            AllowPaging="False"
                            AllowSorting="false" 
                            CssClass="mGrid"
                            PagerStyle-CssClass="pgr" 
                            width="100%" 
                            PageIndex= "1"
                            >
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                               <asp:TemplateField HeaderText="Tipo Archivo">
                                    <ItemTemplate>

                                    <%# DataBinder.Eval(Container, "DataItem.tipoDocumento.descripcion")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre Archivo">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.archivoBinario.nombreArchivo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Numero CI">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.numCI")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha CI">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.fechaCIString")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Archivo Adjunto">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.archivoBinario.nombreFisico")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.estadoVigencia.descripcion")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 
                                <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                    <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                    
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
                            <asp:Button ID="ExportarGrillaAXU" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla2_Click"  Visible="false" />  
                            
                       </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="ExportarGrillaAXU" />
                        </Triggers>   
                       </asp:UpdatePanel>


 </asp:Content>