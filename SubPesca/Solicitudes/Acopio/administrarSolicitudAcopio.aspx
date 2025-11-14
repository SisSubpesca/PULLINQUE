<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administrador/SitioAdmin.Master" CodeBehind="administrarSolicitudAcopio.aspx.cs" 
Inherits="SubPesca.Solicitudes.Acopio.administrarSolicitudAcopio" Theme="admin_style" %>

<%@ Reference Page="inicioSolicitudAcopio.aspx" %>
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
            <span id="titulo_modulo">Administrar Tr&aacute;mites de Solicitudes de Centro de Acopio</span>
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


    <legend>B&uacute;squeda de Tr&aacute;mites de Solicitudes de Centro de Acopio</legend>

             
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
        <td class="col3" colspan="4">
            <asp:UpdatePanel ID="UpdatePanelEstado" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Estado" AutoPostBack="true" runat="server"></asp:DropDownList>
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




     <asp:UpdatePanel ID="UpdatePanelMensajeGrilla" UpdateMode="Conditional" runat="server">
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

    <asp:UpdatePanel ID="UpdatePanelEnTramite" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
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
                            <asp:TemplateField HeaderText="Numero Pert">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.numPert")%>
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
                            <asp:TemplateField HeaderText="Titulares">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(((string)Eval("DescripcionTitulares")).Replace("<br>"," - ")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estado">
                                 <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("estadoActual.descripcion")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
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
                   <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click"  Visible="false" />  

            </asp:Panel>
        </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
        </Triggers>
    </asp:UpdatePanel>



    <br />

    </fieldset>


     <!-- JAVASCRIPT !--><script type="text/javascript">
                             invoca_calendarios("administrarSolicitudRelocalizacion");
    </script></asp:Content>