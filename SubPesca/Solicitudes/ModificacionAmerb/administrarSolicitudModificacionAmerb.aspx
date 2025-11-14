<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" 
CodeBehind="administrarSolicitudModificacionAmerb.aspx.cs" Inherits="SubPesca.Solicitudes.ModificacionAmerb.administrarSolicitudModificacionAmerb" Theme="admin_style" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></asp:ToolkitScriptManager>

    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Administrar Solicitudes de Modificación de Amerb</span></td>
    </tr>
    </table>
    <hr style="width:100%;" />

    <fieldset>
        <legend>Búsqueda de Solicitudes</legend>

    <asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
       <ContentTemplate>    
           <asp:panel ID="Panel1" runat="server">
                <asp:ValidationSummary ID="ValidationSummaryErrores" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList"   ValidationGroup="FormBusquedaExperimentalesConcesion" />
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

             
    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"><span class="item">Número Identificador Solicitud</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            <asp:UpdatePanel ID="UpdatePanelNPert" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="NPert" Width="160px" AutoPostBack="true" runat="server" MaxLength="20"></asp:TextBox>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>

        </td>
    </tr>
    <tr>
        <td class="col1"><span class="item">Código Centro</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            <asp:UpdatePanel ID="UpdatePanelCentro" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="NombreCentro" AutoPostBack="true" runat="server"></asp:TextBox>
               
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
                <link href="../Modificacion/AutoCompleteStyle.css" rel="stylesheet" type="text/css" />
                <asp:TextBox ID="TitularNombre" Width="300px" AutoPostBack="true" runat="server"></asp:TextBox>
                <cc1:AutoCompleteExtender 
                    runat="server" 
                    ID="autoComplete1" 
                    TargetControlID="TitularNombre"
                    ServicePath="~/Solicitudes/Modificacion/adminSolicitudModificacion.asmx"
                    ServiceMethod="BuscarTitulares"
                    MinimumPrefixLength="2" 
                    CompletionInterval="1000"
                    EnableCaching="true"
                    CompletionListCssClass="completionList"
                    CompletionListHighlightedItemCssClass="itemHighlighted"
                    CompletionListItemCssClass="listItem"
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
        <td class="col1"><span class="item">Tipo Modificación</span></td>
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

    <tr>
        <td class="col1"><span class="item">Fecha Ingreso Trámite Desde</span></td>
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
    <td class="col1"><span class="item">Fecha Ingreso Trámite Hasta</span></td><td class="col2"><span class="item">:</span></td><td class="col3">
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

                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla" runat="server"></asp:Label></div></asp:Panel></ContentTemplate></asp:UpdatePanel><table cellpadding="0px" cellspacing="0px">
    <tr>
        <td>
            
            <asp:UpdatePanel ID="UpdatePanelPestanaEnTramite" UpdateMode="Conditional" runat="server">
            <ContentTemplate> 
                <asp:LinkButton ID="lnk_EnTramite"  CssClass="tab1_selected"  runat="server" onclick="cambiaPestania_Click" CausesValidation="false">En Trámite</asp:LinkButton></ContentTemplate></asp:UpdatePanel></td><td>
            
            <asp:UpdatePanel ID="UpdatePanelPestanaAprobada" UpdateMode="Conditional" runat="server">
            <ContentTemplate> 
                <asp:LinkButton ID="lnk_Aprobada"  CssClass="tab2" runat="server"  onclick="cambiaPestania_Click" CausesValidation="false">Aprobada</asp:LinkButton></ContentTemplate></asp:UpdatePanel></td><td>
            
            <asp:UpdatePanel ID="UpdatePanelPestanaRechazada" UpdateMode="Conditional" runat="server">
            <ContentTemplate> 
                <asp:LinkButton ID="lnk_Rechazada"  CssClass="tab3"  runat="server" onclick="cambiaPestania_Click" CausesValidation="false">Rechazada</asp:LinkButton></ContentTemplate></asp:UpdatePanel></td></tr></table><asp:UpdatePanel ID="UpdatePanelEnTramite" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelEnTramite"  Visible="true" runat="server">
                <div class="msgGrilla_Concesion">
                    <asp:Label ID="msgGrilla_Conc" runat="server"></asp:Label></div><asp:GridView ID="GridConcesAdm"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" PageSize="10" 
                       CssClass="mGrid"
                       OnRowDataBound="GridConcesAdm_RowDataBound"
                       OnRowCommand="GridConcesAdm_RowCommand"
                       OnPageIndexChanging="GridConcesAdm_PageIndexChanged"
                       PagerStyle-CssClass="pgr"
                       Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Numero Identificador Solicitud">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.numPert")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo Modificacion">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("DescripcionTipoModificacion")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Centro">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.unidadEspacial.centrosDeCultivo.codigoCentro")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Titulares">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(((string)Eval("DescripcionTitulares")).Replace("<br>"," - ")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                     <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("region.descripcion")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Provincia">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("provincia.descripcion")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Comuna">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(((string)Eval("DescripcionComuna")).Replace("<br>"," - ")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Fecha Recepcion">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.fechaRecepcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha de Ingreso Tramite">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.fechaIngresoTramite")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("estadoActual.descripcion")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                <ItemTemplate>

                                         <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                                         <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                                         
                                         <asp:ImageButton ID="gRedefinir" Visible="false" runat="server" CausesValidation="false" CommandName="Redefinir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/flechas.jpg" Height="20px" AlternateText="Redefinir" ToolTip="Redefinir" />

                                         <asp:ImageButton ID="gError" Visible="false" runat="server" CausesValidation="false" CommandName="ErroresSolicitud" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/info.png" Height="20px" AlternateText="Errores Solicitud" ToolTip="Errores Solicitud" />

                                         <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
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
                   <asp:Button ID="ExportarGrilla1" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla1_Click"  Visible="false" /> 


            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla1" />
        </Triggers>
    </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanelAprobada" UpdateMode="Conditional" runat="server">
        <ContentTemplate>      
                                    
                    <asp:Panel ID="PanelAprobada"  Visible="false" runat="server">
            
                    <asp:GridView ID="GridSolicitudesAdm"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" PageSize="10" OnPageIndexChanging="GridSolicitudesAdm_PageIndexChanged"
                       CssClass="mGrid"
                       OnRowDataBound="GridSolicitudesAdm_RowDataBound"
                       OnRowCommand="GridSolicitudesAdm_RowCommand"
                       PagerStyle-CssClass="pgr"
                       Width="100%">
                       <Columns>
                            <asp:TemplateField HeaderText="Numero Identificador Solicitud">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.numPert")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo Modificacion">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("DescripcionTipoModificacion")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Centro">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.unidadEspacial.centrosDeCultivo.codigoCentro")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Titulares">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(((string)Eval("DescripcionTitulares")).Replace("<br>"," - ")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                     <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("region.descripcion")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Provincia">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("provincia.descripcion")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Comuna">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(((string)Eval("DescripcionComuna")).Replace("<br>"," - ")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Fecha Recepcion">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.fechaRecepcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha de Ingreso Tramite">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.fechaIngresoTramite")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("estadoActual.descripcion")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                <ItemTemplate>
                                
                                         <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                                         <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                                        
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

    <asp:UpdatePanel ID="UpdatePanelRechazada" UpdateMode="Conditional" runat="server">
        
        <ContentTemplate>                        
            <asp:Panel ID="PanelRechazada"  Visible="false" runat="server">
                    
                    <asp:GridView ID="GridViewRechazada"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" PageSize="10" 
                       CssClass="mGrid"
                       OnRowDataBound="GridSolModRechazada_RowDataBound"
                       OnRowCommand="GridSolModRechazada_RowCommand"
                       OnPageIndexChanging="GridSolModRechazada_PageIndexChanged"
                       PagerStyle-CssClass="pgr"
                       Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Numero Identificador Solicitud">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.numPert")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo Modificacion">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("DescripcionTipoModificacion")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Centro">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.unidadEspacial.centrosDeCultivo.codigoCentro")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Titulares">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(((string)Eval("DescripcionTitulares")).Replace("<br>"," - ")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                     <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("region.descripcion")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Provincia">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("provincia.descripcion")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Comuna">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(((string)Eval("DescripcionComuna")).Replace("<br>"," - ")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Fecha Recepcion">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.fechaRecepcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha de Ingreso Tramite">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.fechaIngresoTramite")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("estadoActual.descripcion")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                <ItemTemplate>
                                
                                         <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                                         <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                                         
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

                    <asp:Button ID="ExportarGrilla3" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla3_Click"  Visible="false" />  


            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla3" />
        </Triggers>
    </asp:UpdatePanel>

    </fieldset>


              <!-- JAVASCRIPT !--><script type="text/javascript">
                                    invoca_calendarios("administrarSolicitudModificacion");
       </script></asp:Content>