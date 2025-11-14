<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="administrarConcesiones.aspx.cs" 
Inherits="SubPesca.Unidades.Concesion.administrarConcesiones" Theme="admin_style" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>


<%@ Reference Page="~/Solicitudes/Registrar/unidadEspacial.aspx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"></asp:ToolkitScriptManager>

    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Administrar Concesiones de Acuicultura</span>
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

   <legend>Búsqueda de Concesiones</legend>
   
    <table class="form" cellpadding="0px" cellspacing="0px" width="100%">
    <tr>
        <td class="col1"><span class="item">Código de Centro</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            <asp:UpdatePanel ID="UpdatePanelCodigoCentro" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="CodigoSiep" Width="80px" AutoPostBack="true" runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="10"></asp:TextBox>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>

        </td>
    </tr>

    <tr>
        <td class="col1"><span class="item">N° Pert</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            <asp:UpdatePanel ID="UpdatePanelPert" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="Pert" Width="160px" AutoPostBack="true" runat="server" MaxLength="20"></asp:TextBox>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>

        </td>
    </tr>
    
    <tr>
        <td class="col1"><span class="item">Titular</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            <asp:UpdatePanel ID="UpdatePanelTitular" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <link href="../../Solicitudes/Registrar/administrarDocumento.aspx" rel="stylesheet" type="text/css" />
                <asp:TextBox ID="TitularNombre" Width="300px" AutoPostBack="true" runat="server"></asp:TextBox>
                <cc1:AutoCompleteExtender 
                    runat="server" 
                    ID="autoComplete1" 
                    TargetControlID="TitularNombre"
                    ServicePath="~/Solicitudes/Registrar/adminSolicitudConcesion.asmx"
                    ServiceMethod="BuscarTitulares"
                    MinimumPrefixLength="2" 
                    CompletionInterval="1000"
                    EnableCaching="true"
                    CompletionListCssClass="autocompletionList"
                    CompletionListHighlightedItemCssClass="autoitemHighlighted"
                    CompletionListItemCssClass="autolistItem"
                    >
                </cc1:AutoCompleteExtender>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>

        </td>
    </tr>
    <tr>
        <td class="col1"><span class="item">Región</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanelRegion" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:DropDownList ID="Region" AutoPostBack="true" runat="server"  OnSelectedIndexChanged="Region_OnSelectedIndexChanged"></asp:DropDownList>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>

        </td>
    </tr>
    <tr>
        <td class="col1"><span class="item">Provincia</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanelProvincia" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:DropDownList ID="Provincia" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Provincias_OnSelectedIndexChanged"></asp:DropDownList>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Region" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>

        </td>
    </tr>
    <tr>
        <td class="col1"><span class="item">Comuna</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanelComuna" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:DropDownList ID="Comuna" AutoPostBack="true" runat="server"></asp:DropDownList>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Region" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Provincia" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
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
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>
       </td>
    </tr>
    
    <tr>
        <td class="col1"><span class="item">Fecha Desde</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanelFechaDesde" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaDesde" Columns="8" Width="80px" runat="server"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="FechaDesde"
                            Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                            CultureDateFormat="DMY" CultureDatePlaceholder="/">
                        </asp:MaskedEditExtender>
                    </div>
                    <div class="calendario_icono">
                        <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaDesde" alt="Calendario" style="vertical-align: middle" />
                    </div>

                     &nbsp;&nbsp;&nbsp;<asp:CustomValidator ID="ccFecha" ControlToValidate="FechaDesde"  ClientValidationFunction="validaFechaDDMMAAAA" 
                                    Display="static" Font-Size="10" runat="server"><asp:Literal ID="FechaNoValidaLiteral" runat="server" Text="<%$Resources:spanish.language,fechaNoValida%>"/></asp:CustomValidator></div></ContentTemplate><Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>

        </td>
    </tr>
    <tr>
        <td class="col1"><span class="item">Fecha Hasta</span></td><td class="col2"><span class="item">:</span></td><td class="col3">
            <asp:UpdatePanel ID="UpdatePanelFechaHasta" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaHasta" Columns="8" Width="80px" runat="server"></asp:TextBox><asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="FechaHasta"
                            Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                            CultureDateFormat="DMY" CultureDatePlaceholder="/">
                        </asp:MaskedEditExtender>
                    </div>
                    <div class="calendario_icono">
                        <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaHasta" alt="Calendario" style="vertical-align: middle" />
                    </div>

                    &nbsp;&nbsp;&nbsp;<asp:CustomValidator ID="CustomValidator1" ControlToValidate="FechaHasta"  ClientValidationFunction="validaFechaDDMMAAAA" 
                                    Display="static" Font-Size="10" runat="server"><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:spanish.language,fechaNoValida%>"/></asp:CustomValidator></div></ContentTemplate><Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>

        </td>
    </tr>
    </table>

    <table class="form" cellpadding="0px" cellspacing="0px">   
    <tr>
        <td class="col1"></td>
        <td class="col2"></td>
        <td class="col3">
            <asp:Button ID="Limpiar" runat="server" Text="Limpiar"  CausesValidation="false" OnClick="Limpiar_Click"  />
            <asp:Button ID="Buscar" runat="server" Text="Buscar"  CausesValidation="true"  OnClick="FiltrarCargaGrilla" />
        </td>
    </tr>
    </table>

               
    <br />

    <asp:UpdatePanel ID="UpdatePanelSolicitudes" UpdateMode="Conditional" runat="server">
        <ContentTemplate>      
            
            <asp:Panel ID="PanelSolicitudesMsg"  Visible="false" runat="server" >
                     <div class="msgGrilla_div1">
                        <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
                    </div>
                    <div class="msgGrilla_Solicitud">
                        <asp:Label ID="msgGrilla_Sol" runat="server"></asp:Label>
                    </div>
            </asp:Panel>


                    <asp:Panel ID="PanelSolicitudes"  Visible="false" runat="server" CssClass="Content_Grilla">

                    <asp:GridView ID="GridSolicitudesAdm"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" PageSize="20" OnPageIndexChanging="GridSolicitudesAdm_PageIndexChanged"
                       CssClass="mGrid"
                       OnRowDataBound="GridSolicitudesAdm_RowDataBound"
                       OnRowCommand="GridSolicitudesAdm_RowCommand"
                       PagerStyle-CssClass="pgr"
                       Width="100%">
                        <Columns>

                            <asp:TemplateField HeaderText="Codigo de Centro">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.unidadEspacial.centrosDeCultivo.codigoCentro")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="N° Pert">
                                <ItemTemplate>
                                  <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("numPert")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                     <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("region.descripcion")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Provincia">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("provincia.descripcion")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comuna">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(((string)Eval("DescripcionComuna")).Replace("<br>"," - ")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Ingreso Tramite">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.fechaIngresoTramite")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.estadoVigencia.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Titulares">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(((string)Eval("DescripcionTitulares")).Replace("<br>"," - ")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label HeaderText="idEstadoVigencia" ID="hidden3" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "idEstadoUnidadEspacial")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="200px">
                                <ItemTemplate>
                                          
                                         <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="VerConcesion" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver Concesión" ToolTip="Ver" />

                                         <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="ModificarConcesion" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar Concesión" ToolTip="Modificar Concesión" />
                                            
                                        <asp:ImageButton ID="gDesasociar" Visible="false" runat="server" CausesValidation="false" CommandName="Desasociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Pasar a Vigente" ToolTip="Pasar a Vigente" />
                    
                                         <asp:ImageButton ID="gAsociar" Visible="false" runat="server" CausesValidation="false" CommandName="Asociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Pasar a No Vigente" ToolTip="Pasar a No Vigente" />

                                         <asp:LinkButton ID="gAmpliarVigencia" CommandName="AmpliarVigencia" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>' Visible="false" runat="server">
                                         <asp:Image ID="ImageampliacionPlazo" runat="server" ImageUrl="~/App_Themes/admin_style/images/ampliacionPlazo.png" Height="20px" AlternateText="Ampliar Vigencia" ToolTip="Ampliar Vigencia" />
                                         </asp:LinkButton>
                                         
                                         <asp:LinkButton ID="gHistorial" CommandName="VerHistorial" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>' Visible="false" runat="server">
                                         <asp:Image ID="gImgHistorial" runat="server" ImageUrl="~/App_Themes/admin_style/images/icono_historial_proyectos.png" Height="20px" AlternateText="Ver Historial" ToolTip="Ver Historial" />
                                         </asp:LinkButton>

                                         <asp:LinkButton ID="gEliminarUE" CommandName="EliminarUE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>' Visible="false" runat="server">
                                         <asp:Image ID="gImgEliminarUE" runat="server" ImageUrl="~/App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Eliminar Unidad Espacial" ToolTip="Eliminar Unidad Espacial" />
                                         </asp:LinkButton>

                                         <asp:LinkButton ID="gTransformarUE" CommandName="TransformarUE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>' Visible="false" runat="server">
                                         <asp:Image ID="gImgTransformarUE" runat="server" ImageUrl="~/App_Themes/admin_style/images/transform.png" Height="20px" AlternateText="Transformar a Solicitud" ToolTip="Transformar a Solicitud" />
                                         </asp:LinkButton>
                                         
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

          <!-- JAVASCRIPT !--><script type="text/javascript">
        invoca_calendarios("administrarSolicitudConcesion");
    </script><div id="historialCambioVigenciaConcecion" class="dialog">
        <div class="background"></div>
        <div class="content_dialog">
            <div class="top">
                <asp:LinkButton ID="cerrar_historialCambioVigenciaConcecion" CssClass="cerrar" OnClientClick="javascript:close_dialog('historialCambioVigenciaConcecion');" CausesValidation="false" runat="server"></asp:LinkButton></div><div class="body">
            </div>
            <div class="body">
                <fieldset>
                <legend>Historial que Modifican Vigencia</legend>
                <iframe id="iframe_historialCambioVigenciaConcecion" src="" width="550" height="400" scrolling="auto" frameborder="0"></iframe>
                </fieldset> 
            </div>
        </div>
      </div>
                 
    <div id="cambiarVigenciaConcesion" class="dialog">
        <div class="background"></div>
        <div class="content_dialog">
            <div class="body">
                <fieldset>
                <legend>Cambiar Vigencia</legend>
                <iframe id="iframe_cambiarVigenciaConcesion" src="" width="600" height="400" scrolling="auto" frameborder="0"></iframe>
                </fieldset> 
            </div>
        </div>
      </div>
        
       <div id="extenderPlazoVigenciaConcesion" class="dialog">
        <div class="background"></div>
        <div class="content_dialog">
            <div class="top">
                <asp:LinkButton ID="cerrar_extenderPlazoVigenciaConcesion" CssClass="cerrar" OnClientClick="javascript:close_dialog('extenderPlazoVigenciaConcesion');" CausesValidation="false" runat="server"></asp:LinkButton>
            </div>
            <div class="body">
                <fieldset>
                <legend>Extender Plazo Vigencia</legend>
                <iframe id="iframe_extenderPlazoVigenciaConcesion" src="" width="550" height="400" scrolling="auto" frameborder="0"></iframe>
                </fieldset> 
            </div>
        </div>
      </div> 
        
      </asp:Content>