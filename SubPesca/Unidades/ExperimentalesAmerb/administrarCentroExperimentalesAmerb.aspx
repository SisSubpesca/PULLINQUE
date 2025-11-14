<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administrador/SitioAdmin.Master" CodeBehind="administrarCentroExperimentalesAmerb.aspx.cs" 
Inherits="SubPesca.Unidades.ExperimentalesAmerb.administrarCentroExperimentalesAmerb" Theme="admin_style" %>

<%@ Reference Page="~/Solicitudes/ExperimentalesAmerb/inicioSolicitudExperimentalesAmerb.aspx" %>
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
            <span id="titulo_modulo">Administrar Experimentales en Amerb</span>
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


    <legend>B&uacute;squeda de Experimentales en Amerb</legend>

             
    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"><span class="item">Código de Centro de Experimentales en Amerb</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            <asp:UpdatePanel ID="UpdatePanelCodigoCentro" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="CodigoSiep" Width="80px" AutoPostBack="true" runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="10"></asp:TextBox>
                </ContentTemplate>
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
                    <asp:TextBox ID="TitularNombre" Width="300px" AutoPostBack="true" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender 
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
                    </asp:AutoCompleteExtender>

                </ContentTemplate>
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
            </asp:UpdatePanel>

        </td>
   
        <td class="col1"><span class="item">Provincia</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanelProvincia" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Provincia" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Provincia_OnSelectedIndexChanged"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
       </td>
    </tr>

    
    <tr>
        <td class="col1"><span class="item">Comuna</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            <asp:UpdatePanel ID="UpdatePanelComuna" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Comuna" AutoPostBack="true" runat="server"></asp:DropDownList>
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
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
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





    <asp:UpdatePanel ID="UpdatePanelEnTramite" UpdateMode="Conditional" runat="server">
        <ContentTemplate>  
        
            <asp:Panel ID="PanelSolicitudesMsg"  Visible="false" runat="server" >
                     <div class="msgGrilla_div1">
                        <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
                    </div>
                    <div class="msgGrilla_Solicitud">
                        <asp:Label ID="msgGrilla_Sol" runat="server"></asp:Label>
                    </div>
            </asp:Panel>
                                          
            <asp:Panel ID="PanelEnTramite"  Visible="true" runat="server">
           
                    <asp:GridView ID="GridSolicitudesAdm"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" PageSize="10" 
                       OnPageIndexChanging="GridSolicitudesAdm_PageIndexChanged"
                       CssClass="mGrid"
                       OnRowDataBound="GridSolicitudesAdm_RowDataBound"
                       OnRowCommand="GridSolicitudesAdm_RowCommand"
                       PagerStyle-CssClass="pgr"
                       Width="100%">
                        <Columns>


                            <asp:TemplateField HeaderText="Código de Centro">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.unidadEspacial.centrosDeCultivo.codigoCentro")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="N° Pert">
                                <ItemTemplate>
                                  <asp:Label ID="Label1" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("numPert")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.region.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Provincia">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.provincia.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comuna">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.DescripcionComuna")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Ingreso Trámite">
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
                                    <%# DataBinder.Eval(Container, "DataItem.DescripcionTitulares")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            

                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label HeaderText="idEstadoVigencia" ID="hidden3" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "idEstadoUnidadEspacial")%>'></asp:Label>
                                
                                </ItemTemplate>
                            </asp:TemplateField>
                            

                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="170px">
                                <ItemTemplate>

                                         <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="VerConcesion" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver Experimentales Amerb" ToolTip="Ver Experimentales Amerb" />
                                         
                                         <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="ModificarConcesion" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar Experimentales Amerb" ToolTip="Modificar Experimentales Amerb" />
                                         
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



    <br />

    </fieldset>


      <!-- JAVASCRIPT !-->
    <script type="text/javascript">
        invoca_calendarios("administrarSolicitudConcesion");
    </script>
    
    <div id="historialCambioVigenciaExperimentalAmerb" class="dialog">
        <div class="background"></div>
        <div class="content_dialog">
            <div class="top">
                <asp:LinkButton ID="cerrar_historialCambioVigenciaExperimentalAmerb" CssClass="cerrar" OnClientClick="javascript:close_dialog('historialCambioVigenciaExperimentalAmerb');" CausesValidation="false" runat="server"></asp:LinkButton></div><div class="body">
            </div>
            <div class="body">
                <fieldset>
                <legend>Historial que Modifican Vigencia</legend>
                <iframe id="iframe_historialCambioVigenciaExperimentalAmerb" src="" width="550" height="400" scrolling="auto" frameborder="0"></iframe>
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
        
    <div id="extenderPlazoVigenciaExperimentalAmerb" class="dialog">
        <div class="background"></div>
        <div class="content_dialog">
            <div class="top">
                <asp:LinkButton ID="cerrar_extenderPlazoVigenciaAmerb" CssClass="cerrar" OnClientClick="javascript:close_dialog('extenderPlazoVigenciaExperimentalAmerb');" CausesValidation="false" runat="server"></asp:LinkButton>
            </div>
            <div class="body">
                <fieldset>
                <legend>Extender Plazo Vigencia</legend>
                <iframe id="iframe_extenderPlazoVigenciaExperimentalAmerb" src="" width="550" height="400" scrolling="auto" frameborder="0"></iframe>
                </fieldset> 
            </div>
        </div>
    </div> 
    
    </asp:Content>

