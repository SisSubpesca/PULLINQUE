<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesExperimentalesConcesion.Master" AutoEventWireup="true" CodeBehind="identificacionTitularExperimentalesConcesion.aspx.cs" 
Inherits="SubPesca.Solicitudes.ExperimentalesConcesion.identificacionTitularExperimentalesConcesion" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="../Registrar/informacionSolicitud.ascx"                   tagname="informacionSolicitud"                 tagprefix="uc1" %>


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
                    <uc1:informacionSolicitud ID="informacionSolicitud" runat="server" />
                </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Identificación del Solicitante</span>
            </td>
        </tr>
    </table>

    <hr style="width:100%;" />

    <!-- Formulario de Identificación del Solicitante -->
    <fieldset>
        
        <legend>Datos del Solicitante</legend>
        <br />

        <asp:UpdatePanel ID="UpdatePanelDatosSolicitante" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
        
        <asp:Panel ID="PanelDatosSolicitante" Visible="true" runat="server">

        <asp:Panel id="FormularioIngreso" runat="server">

        <asp:Panel ID="PanelRut" Visible="true" runat="server">
            
            <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla" runat="server"></asp:Label>
                </div>
            </asp:Panel>

            <asp:ValidationSummary ID="ValidationSummaryIngresoSolicitante" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
 
            <table class="form" cellpadding="0px" cellspacing="0px">
                    
                    <tr>
                        <td class="col1"><span class="item">Rut Persona</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                        
                        <asp:TextBox ID="RutPersona" MaxLength="10" Width="100px" runat="server" OnTextChanged="CambiaTipoPersona_Click" ></asp:TextBox>
                        <asp:Button ID="BuscarSolicitante" runat="server" Text="Buscar" onclick="BuscarSolicitante_Click" CausesValidation="true" ValidationGroup="grupo1" />
                            12345678-K

                        <asp:RequiredFieldValidator id="RequiredFieldValidatorRutPersona" runat="server" ControlToValidate="RutPersona"  ValidationGroup="grupo1"
                         ErrorMessage="Rut Persona" Display="Static">*</asp:RequiredFieldValidator>

                        <asp:CustomValidator ID="ccNumCustVal" ControlToValidate="RutPersona" ErrorMessage="Rut Persona sin formato válido" ForeColor="Red" ClientValidationFunction="validaRUT" Display="Static" Font-Size="10" runat="server" ValidationGroup="grupo1"></asp:CustomValidator>

                        </td>
                    </tr>
                </table>
            
        </asp:Panel>

        <asp:Panel ID="PanelDatosPersonaNatural" Visible="false" runat="server">

        <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1" nowrap><span class="item">Nombre del Solicitante</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:HiddenField ID="TipoPersonaNatural" runat="server" Value="12"></asp:HiddenField>
                    <asp:TextBox ID="NombreSolicitanteNatural" MaxLength="40" Width="200px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="col1"><span class="item">Género</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:TextBox ID="Genero" MaxLength="15" Width="80px" Readonly runat="server" ></asp:TextBox>
                </td>
            </tr>
        </table>

        </asp:Panel>
                
        <asp:Panel ID="PanelPersonaJuridica"  Visible="false" runat="server">

        <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1"><span class="item">Tipo</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:HiddenField ID="TipoPersonaJuridica" runat="server" Value="13"></asp:HiddenField>
                    
                    <asp:TextBox ID="SubtipoPersonaJuridica" MaxLength="40" Width="200px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="col1" nowrap><span class="item">Nombre del Solicitante</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3"><asp:TextBox ID="NombreSolicitanteJuridico" MaxLength="40" Width="200px" ReadOnly="true" runat="server"></asp:TextBox>
                    &nbsp;</td>
            </tr>
        </table>

        </asp:Panel>
        
        <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1">&nbsp;</td>
                <td class="col2" nowrap><asp:ImageButton ID="AgregarSolicitante" runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" AlternateText="Agregar Solicitante" ToolTip="Agregar Solicitante" onclick="AgregarSolicitante_Click" CausesValidation="true" ValidationGroup="grupo1" />
                    <span class="item">Agregar Solicitante</span></td>
                <td class="col3">
                  <asp:ImageButton ID="LimpiarSolicitante" runat="server" 
                        ImageUrl="~/App_Themes/admin_style/images/clean.png" Height="20px" 
                        AlternateText="Limpiar Solicitante" ToolTip="Limpiar Solicitante" 
                        onclick="LimpiarSolicitante_Click" />
                </td>
            </tr>
        </table>


        </asp:Panel>


        <asp:UpdatePanel ID="UpdatePanelSolicitante" UpdateMode="Conditional" runat="server">

        <ContentTemplate>                        

        <asp:Panel ID="PanelSolicitante"  Visible="true" runat="server">

        <!-- Listado de Titulares de la Solicitud -->
        <asp:GridView 
           ID="GridSolicitante"
           DataKeyNames="rutPersona" 
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           AllowPaging="True" PageSize="10" OnPageIndexChanging="GridSolicitante_PageIndexChanged"
           AllowSorting="True" OnSorting="GridSolicitante_Sorting"
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

                    <asp:ImageButton ID="gDesasociar" Visible="false" runat="server" CausesValidation="false" CommandName="Desasociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "rutPersona") %>'
                     ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Pasar a Vigente" ToolTip="Pasar a Vigente" />
                    
                    <asp:ImageButton ID="gAsociar" Visible="false" runat="server" CausesValidation="false" CommandName="Asociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "rutPersona") %>'
                     ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Pasar a No Vigente" ToolTip="Pasar a No Vigente" />

                    <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idPersonasLeg") %>'
                     ImageUrl="../../App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />

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
           AllowSorting="True" OnSorting="GridViewSolicitudesPendientes_Sorting"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           OnRowCommand="GridViewSolicitudesPendientes_RowCommand"
           OnRowDataBound="GridViewSolicitudesPendientes_RowDataBound"
           OnRowCreated="GridViewSolicitudesPendientes_RowCreated"
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
