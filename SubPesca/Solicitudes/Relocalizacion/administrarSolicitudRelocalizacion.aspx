<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Administrador/SitioAdmin.Master" CodeBehind="administrarSolicitudRelocalizacion.aspx.cs" 
Inherits="SubPesca.Solicitudes.Relocalizacion.administrarSolicitudRelocalizacion" Theme="admin_style" %>

<%@ Reference Page="erroresTramiteRelocalizacion.aspx" %>
<%@ Reference Page="ingresarSolicitudRelocalizacion.aspx" %>
<%@ Reference Page="redefinirSolicitudRelocalizacion.aspx" %>



<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></asp:ToolkitScriptManager>




    
    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Administrar Tr&aacute;mites de Relocalizaci&oacute;n</span>
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />

    <fieldset>


    <asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
       <ContentTemplate>    
           <asp:panel ID="Panel1" runat="server">
                <asp:ValidationSummary ID="ValidationSummaryErrores" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList"   ValidationGroup="FormBusquedaRelocalizacion" />
            </asp:panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanelMensajeSuperior" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelMensajeSuperior" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="MensajeSuperior" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <legend>B&uacute;squeda de Tr&aacute;mites de Relocalizaci&oacute;n</legend>

             
    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"><span class="item">Nº PERT</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            <asp:UpdatePanel ID="UpdatePanelNPert" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="NPert" Width="80px" AutoPostBack="true" runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="10"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>


    <tr>
        <td class="col1"><span class="item">Fecha Ingreso Tr&aacute;mite Desde</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanelFechaIngresoTramiteDesde" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div class="calendario">
                        <div class="calendario_textbox">               
                            <asp:TextBox ID="FechaIngresoTramiteDesde" Columns="8" Width="80px" runat="server"></asp:TextBox>
                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="FechaIngresoTramiteDesde"
                                Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                CultureDateFormat="DMY" CultureDatePlaceholder="/">
                            </asp:MaskedEditExtender>
                        </div>
                        <div class="calendario_icono">
                            <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaDesde" alt="Calendario" style="vertical-align: middle" />
                        </div>
                        &nbsp;&nbsp;&nbsp;<asp:CustomValidator ID="ccFecha" ControlToValidate="FechaIngresoTramiteDesde"  ClientValidationFunction="validaFechaDDMMAAAA" Display="static" Font-Size="10" runat="server">Fecha no válida</asp:CustomValidator>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
                                        
                                        
        <td class="col1"><span class="item">Fecha Ingreso Tr&aacute;mite Hasta</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanelFechaIngresoTramiteHasta" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div class="calendario">
                        <div class="calendario_textbox">               
                            <asp:TextBox ID="FechaIngresoTramiteHasta" Columns="8" Width="80px" runat="server"></asp:TextBox><asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="FechaIngresoTramiteHasta"
                                Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                CultureDateFormat="DMY" CultureDatePlaceholder="/">
                            </asp:MaskedEditExtender>
                        </div>
                        <div class="calendario_icono">
                            <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaHasta" alt="Calendario" style="vertical-align: middle" />
                        </div>
                        &nbsp;&nbsp;&nbsp;<asp:CustomValidator ID="CustomValidator1" ControlToValidate="FechaIngresoTramiteHasta"  ClientValidationFunction="validaFechaDDMMAAAA" Display="static" Font-Size="10" runat="server">Fecha no válida</asp:CustomValidator>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>


    <tr>
        <td class="col1"><span class="item">Titular</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            <asp:UpdatePanel ID="UpdatePanelTitular" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="TitularNombre" Width="300px" AutoPostBack="true" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender 
                        runat="server" 
                        ID="autoComplete1" 
                        TargetControlID="TitularNombre"
                        ServicePath="../Registrar/adminSolicitudConcesion.asmx"
                        ServiceMethod="BuscarTitulares"
                        MinimumPrefixLength="2" 
                        CompletionInterval="1000"
                        EnableCaching="true"
                        CompletionListCssClass="autocompletionList"
                        CompletionListHighlightedItemCssClass="autoitemHighlighted"
                        CompletionListItemCssClass="autolistItem"
                        >
                    </asp:AutoCompleteExtender>

                </ContentTemplate>
            </asp:UpdatePanel>

        </td>
    </tr>


    
    <tr>
        <td class="col1"><span class="item">Código Centro Origen</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">

            <asp:UpdatePanel ID="UpdatePanelCentroOrigen" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    
                    <asp:TextBox  ID="CodigoSiepCentroOrigen"   runat="server" MaxLength="40" Columns="40" AutoPostBack="true" ></asp:TextBox><asp:AutoCompleteExtender 
                        runat="server" 
                        ID="AutoCompleteExtender1" 
                        TargetControlID="CodigoSiepCentroOrigen"
                        ServicePath="../CentroAcuicolaWS.asmx"
                        ServiceMethod="BuscarCentros"
                        MinimumPrefixLength="2" 
                        CompletionInterval="1000"
                        EnableCaching="true"
                        CompletionListCssClass="autocompletionList"
                        CompletionListHighlightedItemCssClass="autoitemHighlighted"
                        CompletionListItemCssClass="autolistItem"
                        >
                    </asp:AutoCompleteExtender>

                </ContentTemplate>
            </asp:UpdatePanel>
                        
        </td>
    </tr>
                

    <tr>
        <td class="col1"><span class="item">Código Centro Destino</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">

            <asp:UpdatePanel ID="UpdatePanelCentroDestino" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    
                    <asp:TextBox  ID="CodigoSiepCentroDestino"   runat="server" MaxLength="40" Columns="40" AutoPostBack="true" ></asp:TextBox>
                    <asp:AutoCompleteExtender 
                        runat="server" 
                        ID="AutoCompleteExtender2" 
                        TargetControlID="CodigoSiepCentroDestino"
                        ServicePath="../CentroAcuicolaWS.asmx"
                        ServiceMethod="BuscarCentros"
                        MinimumPrefixLength="2" 
                        CompletionInterval="1000"
                        EnableCaching="true"
                        CompletionListCssClass="autocompletionList"
                        CompletionListHighlightedItemCssClass="autoitemHighlighted"
                        CompletionListItemCssClass="autolistItem"
                        >
                    </asp:AutoCompleteExtender>

                </ContentTemplate>
            </asp:UpdatePanel>
                        
        </td>
    </tr>


    <tr>
        <td class="col1"><span class="item">Region Origen</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanelRegionOrigen" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="RegionOrigen" AutoPostBack="true" runat="server"  OnSelectedIndexChanged="RegionOrigen_OnSelectedIndexChanged"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>

        </td>
   
        <td class="col1"><span class="item">Comuna Origen</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanelComunaOrigen" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ComunaOrigen" AutoPostBack="true" runat="server"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
       </td>
    </tr>


    <tr>
        <td class="col1"><span class="item">Region Destino</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanelRegionDestino" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="RegionDestino" AutoPostBack="true" runat="server"  OnSelectedIndexChanged="RegionDestino_OnSelectedIndexChanged"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>

        </td>
   
        <td class="col1"><span class="item">Comuna Destino</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanelComunaDestino" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ComunaDestino" AutoPostBack="true" runat="server"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
       </td>
    </tr>

    <tr>
        <td class="col1"><span class="item">Tipo Relocalización</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanelSubtipoTramite" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="SubtipoTramite" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Subtipo_OnSelectedIndexChanged"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    
    <tr>
        <td class="col1"><span class="item">Estado</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanelEstado" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Estado" AutoPostBack="true" runat="server"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>

    </table>


    <table class="form" cellpadding="0px" cellspacing="0px">   
    <tr>
        <td class="col1"></td>
        <td class="col2"></td>
        <td class="col3">
            <asp:Button ID="Limpiar" runat="server" Text="Limpiar"  CausesValidation="false" onclick="Limpiar_Click"/>
            <asp:Button ID="Buscar"  runat="server" Text="Buscar"   CausesValidation="true"  onclick="Buscar_Click"/>
        </td>
    </tr>
    </table>


    
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
            <div class="msgGrilla_div1">
                <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
            </div>
            <div class="msgGrilla_div2">
                <asp:Label ID="msgGrilla" runat="server"></asp:Label>
            </div>
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>



    <table cellpadding="0px" cellspacing="0px">
    <tr>
        <td>
            
            <asp:UpdatePanel ID="UpdatePanelPestanaEnTramite" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 
                    <asp:LinkButton ID="lnk_EnTramite"  CssClass="tab1_selected"  runat="server" onclick="cambiaPestania_Click" CausesValidation="false">En Tramite</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td>
            
            <asp:UpdatePanel ID="UpdatePanelPestanaAprobada" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 
                    <asp:LinkButton ID="lnk_Aprobada"  CssClass="tab2" runat="server"  onclick="cambiaPestania_Click" CausesValidation="false">Aprobada</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        
        <td>
            <asp:UpdatePanel ID="UpdatePanelPestanaRechazada" UpdateMode="Conditional" runat="server">
            <ContentTemplate> 
                <asp:LinkButton ID="lnk_Rechazada"  CssClass="tab2" runat="server"  onclick="cambiaPestania_Click" CausesValidation="false">Rechazada</asp:LinkButton>
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    </table>


    <asp:UpdatePanel ID="UpdatePanelEnTramite" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelEnTramite"  Visible="true" runat="server">
               
                
                <asp:GridView ID="GridRelocalizacionTramite"  
                       DataKeyNames="idTramiteRel"
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" 
                       PageSize="50" 
                       OnPageIndexChanging="GridRelocalizacionTramite_PageIndexChanged"
                       CssClass="mGrid"
                       OnRowDataBound="GridRelocalizacionTramite_RowDataBound"
                       OnRowCommand="GridRelocalizacionTramite_RowCommand"
                       PagerStyle-CssClass="pgr"
                       Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Nº Pert">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.numPert")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fecha de Ingreso Trámite">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.fechaIngresoTramite")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nº Sector">
                                <ItemTemplate>
                                    <asp:LinkButton  ID="gModificar" Visible="true" runat="server" CausesValidation="false" CommandName="ModificarSector" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.sectores[0].idSolConcesion")%>' Height="20px" AlternateText="Modificar" Text=' <%# DataBinder.Eval(Container, "DataItem.sectores[0].numSector")%>'/>
                                    <asp:Label ID="gNumSector" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "sectores[0].numSector") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            

                            <asp:TemplateField HeaderText="Tipo Relocalización">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].tipoRelocalizacion.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Centro Origen">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].origenesDetalle")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Región Origen">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].regionOrigenes")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Comuna Origen">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].comunaOrigenes")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Centro Destino">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Región Destino">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].concesionDestino.region.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Comuna Destino">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].concesionDestino.DescripcionComuna")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            
                            <asp:TemplateField HeaderText="Toponimio Destino">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].concesionDestino.DescripcionToponimios")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].concesionDestino.estadoActual.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="120px">
                                <ItemTemplate>

                                
                                    <asp:HiddenField ID="gNumPert" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "numPert") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="gIdSolConcesion" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "sectores[0].idSolConcesion") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="gColumnas" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "columnas") %>'></asp:HiddenField>                                        
                                    <asp:HiddenField ID="gDespliegaRedefinicion" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "despliegaRedefinicion") %>'></asp:HiddenField>                                        
                                    <asp:HiddenField ID="gDespliegaErrores" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "despliegaErrores") %>'></asp:HiddenField>                                        
                                    <asp:HiddenField ID="gDespliegaAlertas" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "despliegaAlertas") %>'></asp:HiddenField>                                        
                                    <asp:HiddenField ID="gDespliegaModificacion" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "despliegaModificacion") %>'></asp:HiddenField>   
                                    <asp:HiddenField ID="gEstadoSSP" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "sectores[0].resultadoResolucion.id") %>'></asp:HiddenField>                                     
                                

                                    <asp:ImageButton ID="gVer" Visible="true" runat="server" CausesValidation="false" CommandName="VerTramite" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idTramiteRel") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver Trámite" ToolTip="Ver Trámite" />
                                    
                                    <asp:ImageButton ID="gRedefinir" Visible="false" runat="server" CausesValidation="false" CommandName="redefinir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idTramiteRel") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/flechas.jpg" Height="20px" AlternateText="Redefinir Trámite" ToolTip="Redefinir Trámite" />

                                    <asp:ImageButton ID="gIntercambiar" Visible="false" runat="server" CausesValidation="false" CommandName="cambiarTipo" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idTramiteRel") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/exchange.png" Height="20px" AlternateText="Cambiar a Relocalizacion RESA" ToolTip="Cambiar a Relocalizacion RESA" />

                                    <asp:ImageButton ID="gErroresTramite" Visible="false" runat="server" CausesValidation="false" CommandName="VerErrores" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idTramiteRel") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/exclamacion.png" Height="20px" AlternateText="Ver Errores Trámite" ToolTip="Ver Errores Trámite" />
                                    
                                    <asp:ImageButton ID="gErroresSector" Visible="false" runat="server" CausesValidation="false" CommandName="erroresSector" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idTramiteRel") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/info.png" Height="20px" AlternateText="Ver Errores Solicitud" ToolTip="Ver Errores Solicitud" />
                                    
                                    <asp:ImageButton ID="gModificarTramite" Visible="false" runat="server" CausesValidation="false" CommandName="ModificarTramite" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idTramiteRel") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar Trámite" ToolTip="Modificar Trámites" />
                                
                                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "sectores[0].idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />

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
                   <asp:Button ID="ExportarGrilla1" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla1_Click" Visible="false" />


            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla1" />
        </Triggers>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanelAprobada" UpdateMode="Conditional" runat="server">
        <ContentTemplate>      
            <asp:Panel ID="PanelAprobada"  Visible="false" runat="server">

                    <asp:GridView ID="GridRelocalizacionAprobada"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" 
                       PageSize="50" 
                       OnPageIndexChanging="GridRelocalizacionAprobada_PageIndexChanged"
                       CssClass="mGrid"
                       OnRowDataBound="GridRelocalizacionAprobada_RowDataBound"
                       OnRowCommand="GridRelocalizacionAprobada_RowCommand"
                       PagerStyle-CssClass="pgr"
                       Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Nº Pert">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.numPert")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fecha de Ingreso Trámite">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.fechaIngresoTramite")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nº Sector">
                                <ItemTemplate>
                                    <asp:LinkButton  ID="gModificar" Visible="true" runat="server" CausesValidation="false" CommandName="ModificarSector" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.sectores[0].idSolConcesion")%>' Height="20px" AlternateText="Modificar" Text='<%# DataBinder.Eval(Container, "DataItem.sectores[0].numSector")%>'/>
                                    <asp:Label ID="gNumSector" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "sectores[0].numSector") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            

                            <asp:TemplateField HeaderText="Tipo Relocalización">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].tipoRelocalizacion.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Centro Origen">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].origenesDetalle")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Región Origen">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].regionOrigenes")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Comuna Origen">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].comunaOrigenes")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Centro Destino">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Región Destino">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].concesionDestino.region.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Comuna Destino">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].concesionDestino.DescripcionComuna")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            
                            <asp:TemplateField HeaderText="Toponimio Destino">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].concesionDestino.DescripcionToponimios")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].concesionDestino.estadoActual.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="120px">
                                <ItemTemplate>


                                    <asp:HiddenField ID="gNumPert" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "numPert") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="gColumnas" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "columnas") %>'></asp:HiddenField>                                        
                                    <asp:HiddenField ID="gDespliegaRedefinicion" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "despliegaRedefinicion") %>'></asp:HiddenField>                                        
                                    <asp:HiddenField ID="gDespliegaErrores" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "despliegaErrores") %>'></asp:HiddenField>                                        
                                    <asp:HiddenField ID="gDespliegaAlertas" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "despliegaAlertas") %>'></asp:HiddenField>                                        
                                    <asp:HiddenField ID="gDespliegaModificacion" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "despliegaModificacion") %>'></asp:HiddenField> 
                                    <asp:HiddenField ID="gEstadoSSP" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "sectores[0].resultadoResolucion.id") %>'></asp:HiddenField>                                       

                                    <asp:ImageButton ID="gVer" Visible="true" runat="server" CausesValidation="false" CommandName="VerTramite" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idTramiteRel") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver Trámite" ToolTip="Ver Trámite" />
                                    
                                    <asp:ImageButton ID="gErroresTramite" Visible="false" runat="server" CausesValidation="false" CommandName="VerErrores" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idTramiteRel") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/exclamacion.png" Height="20px" AlternateText="Ver Errores Trámite" ToolTip="Ver Errores Trámite" />
                                    
                                    <asp:ImageButton ID="gErroresSector" Visible="false" runat="server" CausesValidation="false" CommandName="erroresSector" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idTramiteRel") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/info.png" Height="20px" AlternateText="Ver Errores Solicitud" ToolTip="Ver Errores Solicitud" />
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
                   <asp:Button ID="ExportarGrilla2" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla2_Click" Visible="false" />
                     
                   
            </asp:Panel>                  
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla2" />
        </Triggers>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanelRechazada" UpdateMode="Conditional" runat="server">
        <ContentTemplate>      
            <asp:Panel ID="PanelRechazada"  Visible="false" runat="server">

                <asp:GridView ID="GridRelocalizacionRechazada"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" 
                       PageSize="50" 
                       OnPageIndexChanging="GridRelocalizacionRechazada_PageIndexChanged"
                       CssClass="mGrid"
                       OnRowDataBound="GridRelocalizacionRechazada_RowDataBound"
                       OnRowCommand="GridRelocalizacionRechazada_RowCommand"
                       PagerStyle-CssClass="pgr"
                       Width="100%">
                        <Columns>
                            
                            <asp:TemplateField HeaderText="Nº Pert">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.numPert")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fecha de Ingreso Trámite">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.fechaIngresoTramite")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                    
                            <asp:TemplateField HeaderText="Nº Sector">
                                <ItemTemplate>
                                    <asp:LinkButton  ID="gModificar" Visible="true" runat="server" CausesValidation="false" CommandName="ModificarSector" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.sectores[0].idSolConcesion")%>' Height="20px" AlternateText="Modificar" Text='<%# DataBinder.Eval(Container, "DataItem.sectores[0].numSector")%>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            


                            <asp:TemplateField HeaderText="Tipo Relocalización">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].tipoRelocalizacion.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Centro Origen">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].origenesDetalle")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Región Origen">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].regionOrigenes")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Comuna Origen">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].comunaOrigenes")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Centro Destino">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Región Destino">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].concesionDestino.region.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Comuna Destino">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].concesionDestino.DescripcionComuna")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            
                            <asp:TemplateField HeaderText="Toponimio Destino">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].concesionDestino.DescripcionToponimios")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.sectores[0].concesionDestino.estadoActual.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="120px">
                                <ItemTemplate>

                                    <asp:HiddenField ID="gNumPert" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "numPert") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="gColumnas" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "columnas") %>'></asp:HiddenField>                                        
                                    <asp:HiddenField ID="gDespliegaRedefinicion" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "despliegaRedefinicion") %>'></asp:HiddenField>                                        
                                    <asp:HiddenField ID="gDespliegaErrores" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "despliegaErrores") %>'></asp:HiddenField>                                        
                                    <asp:HiddenField ID="gDespliegaAlertas" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "despliegaAlertas") %>'></asp:HiddenField>                                        
                                    <asp:HiddenField ID="gDespliegaModificacion" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "despliegaModificacion") %>'></asp:HiddenField>                                        
                                    <asp:HiddenField ID="gEstadoSSP" runat="server" Visible="true" Value='<%#DataBinder.Eval(Container.DataItem, "sectores[0].resultadoResolucion.id") %>'></asp:HiddenField>

                                    <asp:ImageButton ID="gVer" Visible="true" runat="server" CausesValidation="false" CommandName="VerTramite" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idTramiteRel") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver Trámite" ToolTip="Ver Trámite" />
                                    
                                    <asp:ImageButton ID="gErroresTramite" Visible="false" runat="server" CausesValidation="false" CommandName="VerErrores" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idTramiteRel") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/exclamacion.png" Height="20px" AlternateText="Ver Errores Trámite" ToolTip="Ver Errores Trámite" />
                                    
                                    <asp:ImageButton ID="gErroresSector" Visible="false" runat="server" CausesValidation="false" CommandName="erroresSector" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idTramiteRel") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/info.png" Height="20px" AlternateText="Ver Errores Solicitud" ToolTip="Ver Errores Solicitud" />
                                    
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
                   <asp:Button ID="ExportarGrilla3" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla3_Click" Visible="false" />

                     
                   
            </asp:Panel>                  
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla3" />
        </Triggers>
    </asp:UpdatePanel>


    <br />

    </fieldset>


     <!-- JAVASCRIPT !-->
     <script type="text/javascript">
        invoca_calendarios("administrarSolicitudRelocalizacion");
    </script></asp:Content>