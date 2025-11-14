<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Administrador/SitioAdmin.Master" CodeBehind="administrarSolicitudConcesion.aspx.cs" 
Inherits="SubPesca.Solicitudes.Registrar.administrarSolicitudConcesion" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
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
            <span id="titulo_modulo">Administrar Solicitudes de Concesión</span>
        </td>
    </tr>

    

    </table>
    <hr style="width:100%;" />

    <asp:UpdatePanel ID="UpdatePanelMensajeSuperior" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelMensajeSuperior" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="MensajeSuperior" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <fieldset>
        <legend>Búsqueda de Solicitudes</legend>



             
    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"><span class="item">Nº PERT</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            <asp:UpdatePanel ID="UpdatePanelNPert" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="NPert" Width="80px" AutoPostBack="true" runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="10"></asp:TextBox>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>

        </td>
    </tr>
    <tr style="display:none;">
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
                <link href="AutoCompleteStyle.css" rel="stylesheet" type="text/css" />
                <asp:TextBox ID="TitularNombre" Width="300px" AutoPostBack="true" runat="server"></asp:TextBox>
                <cc1:AutoCompleteExtender 
                    runat="server" 
                    ID="autoComplete1" 
                    TargetControlID="TitularNombre"
                    ServicePath="adminSolicitudConcesion.asmx"
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



    <asp:UpdatePanel ID="UpdatePanelMensajeGrilla" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla" runat="server"></asp:Label></div></asp:Panel></ContentTemplate></asp:UpdatePanel><asp:UpdatePanel ID="UpdatePanelSolicitudes" UpdateMode="Conditional" runat="server">
        <ContentTemplate>      
            <asp:Panel ID="PanelSolicitudesMsg"  Visible="false" runat="server">
                     <div class="msgGrilla_div1">
                        <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
                    </div>
                    <div class="msgGrilla_Solicitud">
                        <asp:Label ID="msgGrilla_Sol" runat="server"></asp:Label></div></asp:Panel><asp:Panel ID="PanelSolicitudes"  Visible="false" runat="server">
                   

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
                            <asp:TemplateField HeaderText="Num. Pert">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.numPert")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                     <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("region.descripcion")) %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                     
                                <asp:TemplateField HeaderText="Provincia">
                                <ItemTemplate>
                                    
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("provincia.descripcion")) %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Comuna">
                                <ItemTemplate>
                                    
                                    
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(((string)Eval("DescripcionComuna")).Replace("<br>"," - ")) %>'></asp:Label>
                                    
                                    </ItemTemplate></asp:TemplateField>
                                    
                                <asp:TemplateField HeaderText="Fecha Ingreso Tramite">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.fechaIngresoTramite")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Titulares">
                                <ItemTemplate>
                                    
                                    
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(((string)Eval("DescripcionTitulares")).Replace("<br>"," - ")) %>'></asp:Label>
                                    
                                    
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Estado Actual">
                                <ItemTemplate>
                                    
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("estadoActual.descripcion")) %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Opciones" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    
                                    <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />
                                    
                                    <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />

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

                   <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" Visible="false" /> 

            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
      </Triggers>
    </asp:UpdatePanel>



    </fieldset>


             <!-- JAVASCRIPT !--><script type="text/javascript">
        invoca_calendarios("administrarSolicitudConcesion");
    </script></asp:Content>