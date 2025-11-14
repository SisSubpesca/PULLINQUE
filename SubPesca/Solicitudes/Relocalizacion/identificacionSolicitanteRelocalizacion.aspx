<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesRelocalizacion.Master" AutoEventWireup="true" CodeBehind="identificacionSolicitanteRelocalizacion.aspx.cs" 
Inherits="SubPesca.Solicitudes.Relocalizacion.identificacionSolicitanteRelocalizacion" Theme="admin_style" validateRequest="false" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register src="~/Solicitudes/Registrar/informacionSolicitud.ascx"                   tagname="informacionSolicitud"                 tagprefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_usuarios.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="FormularioIngresoSolicitante" ContentPlaceHolderID="rightbody" runat="server">

    <asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>

    <asp:ToolkitScriptManager ID="ToolkitScriptManagerSolicitante" runat="server" EnablePartialRendering="false"></asp:ToolkitScriptManager>

    <asp:UpdatePanel ID="UpdatePanelInformacionSolicitud" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
                    <asp:Panel ID="PanelInformacionSolicitud"  Visible="true" runat="server">
                        <uc2:informacionSolicitud ID="informacionSolicitud" runat="server" />
                    </asp:Panel>
            </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Identificación del Titular de la Concesión</span>
            </td>
        </tr>
    </table>

    <hr style="width:100%;" />

    <!-- Formulario de Identificación del Solicitante -->
    <fieldset>
        
        <legend>Datos del Titular</legend>
        <br />

        <asp:UpdatePanel ID="UpdatePanelDatosSolicitante" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
        
        <asp:Panel ID="PanelDatosSolicitante" Visible="true" runat="server">

        <asp:UpdatePanel ID="UpdatePanelSolicitante" UpdateMode="Conditional" runat="server">

        <ContentTemplate>                        

        <asp:Panel ID="PanelSolicitante"  Visible="true" runat="server">

        <!-- Listado de Titulares de la Solicitud -->
        <asp:GridView 
           ID="GridSolicitante"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           OnRowCommand="GridSolicitante_RowCommand"
           OnRowDataBound="GridSolicitante_RowDataBound"
           OnRowCreated="GridSolicitante_RowCreated"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%">
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
               
                <asp:TemplateField HeaderText="Rut Persona" SortExpression="rutPersona" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.rutPersona") %>-<%# DataBinder.Eval(Container, "DataItem.digitoVerificador") %>
                    </ItemTemplate>
               </asp:TemplateField>
               
               <asp:BoundField HeaderText="Nombre Solicitante" DataField="nombre" SortExpression="nombre" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                          
                <asp:TemplateField HeaderText="Género" SortExpression="genero" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Eval("genero").ToString().ToLower() == "true" ? "Femenino" : "Masculino"%>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Estado" SortExpression="nombreEstadoAsociacion" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.nombreEstadoAsociacion")%>
                    
                </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="APE" SortExpression="nombreEstadoAPE" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.nombreEstadoAPE")%>
                    
                </ItemTemplate>
                </asp:TemplateField>
                    
                <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                    <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "rutPersona") %>'
                     ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />
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
        </ContentTemplate>
        </asp:UpdatePanel>

       </asp:Panel>
       
        </ContentTemplate>
        </asp:UpdatePanel>
            
       <br />
       <!-- Listado de Arrendatarios -->
        <asp:GridView 
           ID="GridViewArrendatarios"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           AllowPaging="True" PageSize="10"
           AllowSorting="True" 
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%">
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
               
               <asp:TemplateField HeaderText="Rut" SortExpression="rutPersona" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.rutPersona") %>-<%# DataBinder.Eval(Container, "DataItem.digitoVerificador") %>
                    
                </ItemTemplate>
               </asp:TemplateField>
               
               <asp:BoundField HeaderText="Nombre Arrendatario" DataField="nombre" SortExpression="nombre" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                          
               <asp:TemplateField HeaderText="Género" SortExpression="genero" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# Eval("genero").ToString().ToLower() == "true" ? "Femenino" : "Masculino"%>
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
        </ContentTemplate>
        </asp:UpdatePanel>



       <br />
       <!-- Solicitudes Pendientes asociadas a los Titulares  -->
        <asp:UpdatePanel ID="UpdatePanelSolicitudesPendientes" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
        
        <asp:Panel ID="PanelSolicitudesPendientes" Visible="true" runat="server">

        <asp:GridView 
           ID="GridViewSolicitudesPendientes" 
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           AllowPaging="True" PageSize="10" OnPageIndexChanging="GridViewSolicitudesPendientes_PageIndexChanged"
           AllowSorting="True"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           OnRowCreated="GridViewSolicitudesPendientes_RowCreated"
           OnRowCommand="GridViewSolicitudesPendientes_RowCommand"
           OnRowDataBound="GridViewSolicitudesPendientes_RowDataBound"
           Width="100%">
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
               
               <asp:TemplateField HeaderText="Tipo Unidad Espacial" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                     <asp:HiddenField ID="gSolicitud" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.solicitud.idSolConcesion") %>' />
                     <%# DataBinder.Eval(Container, "DataItem.solicitud.tipoTramite.descripcion")%>
                </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Nº Pert" SortExpression="numPert" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.solicitud.numPert")%>
                </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Ámbito" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.ambitoTipo[0].ambito.descripcion")%>
                </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Requerimiento" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.ambitoTipo[0].tipo.descripcion")%>
                </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Titulares" SortExpression="titularesCad" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.titularesCad")%>
                </ItemTemplate>
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Fecha de Ingreso" SortExpression="fechaIngresoSistema" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.fechaIngresoSistema")%>
                </ItemTemplate>
               </asp:TemplateField>
                                              
               <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                    <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idRequerimientoString") %>'
                     ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                    <asp:ImageButton ID="gDescargar" Visible="false" runat="server" CausesValidation="false" CommandName="Descargar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivo") %>'
                     ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" />

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
        </ContentTemplate>
        </asp:UpdatePanel>

    </fieldset>

</asp:Content>