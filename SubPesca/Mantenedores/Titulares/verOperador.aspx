<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="verOperador.aspx.cs" Theme="admin_style" 
Inherits="SubPesca.Mantenedores.Titulares.verOperador" MasterPageFile="~/Mantenedores/SitioMantenedorTitulares.Master" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="FormularioIngresoSolicitante" ContentPlaceHolderID="rightbody" runat="server">

<asp:ToolkitScriptManager ID="ToolkitScriptManageAdministracionTitulares" runat="server"></asp:ToolkitScriptManager>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Ver Operador</span>
            </td>
            <td align="right" valign="middle">
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

    <fieldset>
        
        <legend>Datos del Operador</legend>
        <br />

        <asp:UpdatePanel ID="UpdatePanelDatosTitular" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
        
        <asp:Panel ID="PanelDatosTitular" Visible="true" runat="server">
                        
            <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Rut Operador</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            <asp:TextBox ID="RutPersona" MaxLength="8" Width="100px" runat="server" BackColor="#ddddee" ReadOnly></asp:TextBox> - <asp:TextBox ID="DVPersona" MaxLength="1" Width="20px" runat="server" BackColor="#ddddee" ReadOnly></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1"><span class="item">Nombre del Operador</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            <asp:TextBox ID="NombreSolicitante" MaxLength="40" Width="200px" BackColor="#ddddee" ReadOnly runat="server"></asp:TextBox>
                        </td>
                    </tr>
            </table>

        <!-- Listado de Direcciones -->
        <asp:GridView 
           ID="GridContactoMatrizSucursales"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%"
           OnRowCreated="GridContactoMatrizSucursales_RowCreated"
           AllowPaging="True" PageSize="5" OnPageIndexChanging="GridContactoMatrizSucursales_PageIndexChanged">
           
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
              <asp:TemplateField HeaderText="Dirección" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.direccion")%>
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Región" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.region.region")%>
               </ItemTemplate>
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Comuna" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.region.comuna.comuna")%>
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

        <br />

        <!-- Listado de Contactos -->
        <asp:GridView 
           ID="GridViewContacto"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%"
           OnRowCreated="GridViewContacto_RowCreated"
           AllowPaging="True" PageSize="5" OnPageIndexChanging="GridViewContacto_PageIndexChanged">
           
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
              <asp:TemplateField HeaderText="Tipo de Contacto" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.tipoContacto.descripcion")%>
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Valor Contacto" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.valorContacto")%>
               </ItemTemplate>
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Detalle" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.detalle")%>
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

        <br />

       <!-- Listado de Archivos Adjuntos (para todas las personas)-->
        <asp:GridView 
           ID="GridArchivosAdjuntos"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%"
           OnRowCreated="GridArchivosAdjuntos_RowCreated"
           OnRowCommand="GridArchivosAdjuntos_RowCommand"
           OnRowDataBound="GridArchivosAdjuntos_RowDataBound"
           AllowPaging="True" PageSize="5" OnPageIndexChanging="GridArchivosAdjuntos_PageIndexChanged">
                      
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
               <asp:TemplateField HeaderText="Tipo Archivo" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.tipoDocumento.descripcion")%>
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Nombre Archivo" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.archivoBinario.nombreArchivo")%>
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Número CI" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.numCI")%>
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Fecha CI" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.fechaCI")%>
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Archivo Adjunto" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.archivoBinario.nombreFisico")%>
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                    <asp:ImageButton ID="gDescargar" Visible="false" runat="server" CausesValidation="false" CommandName="Descargar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivo") %>'
                     ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" OnPreRender="ImgAdd_PreRender" />

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

       <asp:HiddenField ID="BackPage" runat="server" />

        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>

        <!-- DIALOGOS -->
    
        <div id="verNombres" class="dialog">
        <div class="background"></div>
        <div class="content_dialog">
            <div class="top">
                <asp:LinkButton ID="cerrar_verNombres" CssClass="cerrar" OnClientClick="javascript:close_dialog('verNombres');" CausesValidation="false" runat="server"></asp:LinkButton>
            </div>
            <div class="body">
                <fieldset>
                    <legend>Ver Nombres</legend>
                    <iframe id="iframe_verNombres" src="" width="100%" height="249px" frameborder="0" scrolling="no"></iframe>
                </fieldset>
            </div>
        </div>
        </div>
    </fieldset>
</asp:Content>