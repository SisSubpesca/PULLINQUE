<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Administrador/SitioAdmin.Master" CodeBehind="BitacoraCambios.aspx.cs" 
Inherits="SubPesca.Administrador.BitacoraCambios.BitacoraCambios"  Theme="admin_style"%>

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
                <span id="titulo_modulo">Bit&aacute;cora de Cambios</span>
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

    <legend>B&uacute;squeda de Tr&aacute;mites</legend>

    <table class="form" cellpadding="0px" cellspacing="0px">
    
    <tr>
        <td class="col1"><span class="item">Tipo de Transacci&oacute;n</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePaneltramite" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:DropDownList ID="Tramite" runat="server"></asp:DropDownList> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click"/>
            </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>

    <tr>
        <td class="col1"><span class="item">Nombre Tabla</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:UpdatePanel ID="UpdatePanelNombreTabla" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:DropDownList ID="NombreTabla" runat="server" ></asp:DropDownList> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>

    <tr>
        <td class="col1"><span class="item">PERT / Identificador</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            <asp:UpdatePanel ID="UpdatePanelNPert" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="NPert" Width="180px" AutoPostBack="true" runat="server" MaxLength="20"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    
    <tr>
        <td class="col1"><span class="item">Fecha Desde</span></td>
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
                                        
                                        
        <td class="col1"><span class="item">Fecha Hasta</span></td>
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

    <asp:UpdatePanel ID="upd2" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
    <asp:Panel ID="PanelGridBitacora"  Visible="true" runat="server" CssClass="Content_Grilla"> 
    <asp:GridView ID="GridBitacora"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" PageSize="20"
                       CssClass="mGrid"
                       PagerStyle-CssClass="pgr"
                       OnPageIndexChanging="GridBitacora_OnPageIndexChanging"
                       Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Tipo de Transaccion">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.Column1")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre Tabla">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.tabla")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PERT/Identificador">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.identificador")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Clave Tabla">
                                <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(((string)Eval("pk")).Replace("<"," ").Replace(">"," ")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Campo">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.campo")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valor Original">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.valorOriginal")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valor Nuevo">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.valorNuevo")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.fechaTrn")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Usuario">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.Usuario")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre Usuario">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.nombresUsuario")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Observaciones">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.observaciones")%>
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

      <!-- JAVASCRIPT !--><script type="text/javascript">
                              invoca_calendarios("administrarSolicitudRelocalizacion");
    </script>

</asp:Content>

