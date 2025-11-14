<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="busquedaResoluciones.aspx.cs" 
Inherits="SubPesca.Resoluciones.busquedaResoluciones" Theme="admin_style" %>


<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"></asp:ToolkitScriptManager>



<asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
   <ContentTemplate>    
       <asp:panel ID="Panel1" runat="server">
            <asp:ValidationSummary ID="ValidationSummaryErrores" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList"  />
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



<asp:UpdatePanel ID="UpdatePanelErroresSuperior" UpdateMode="Conditional" runat="server">
    <ContentTemplate>   
        <asp:Panel ID="PanelErroresSuperior" CssClass="Content_msgGrilla" Visible="false" runat="server">
            <div class="msgGrilla_div2">
                <asp:Label ID="ErroresSuperior" runat="server"></asp:Label>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

  


<fieldset>
   

        <legend>B&uacute;squeda de Resoluciones</legend>
        <br />
        

        
        <asp:UpdatePanel ID="UpdatePanelOrigen" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelOrigen"  Visible="true" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="OrigenLiteral" runat="server" Text="<%$Resources:spanish.language,origen%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="Origen" AutoPostBack="true" runat="server"></asp:DropDownList></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


      

        <asp:UpdatePanel ID="UpdatePanelNumero" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelNumero"  Visible="true" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Número</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="Numero" autocomplete="tel-extension" runat="server"  MaxLength="20"/></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


             
    

        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
        <asp:Panel ID="Panel2"  Visible="true" runat="server">
            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1"><span class="item">Fecha Desde</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:UpdatePanel ID="UpdatePanelFechaDesde" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="calendario">
                            <div class="calendario_textbox">               
                                <asp:TextBox ID="FechaDesde" Columns="8" Width="80px" runat="server"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="FechaDesde"
                                    Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                    CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                </asp:MaskedEditExtender>
                            </div>
                            <div class="calendario_icono">
                                <asp:Image src="../App_Themes/admin_style/images/calendar.png" id="imgFechaDesde" alt="Calendario"  runat="server"  style="vertical-align: middle" />
                            </div>

                             &nbsp;&nbsp;&nbsp;<asp:CustomValidator ID="ccFecha" ControlToValidate="FechaDesde"  ClientValidationFunction="validaFechaDDMMAAAA" 
                                            Display="static" Font-Size="10" runat="server"><asp:Literal ID="FechaNoValidaLiteral" runat="server" Text="<%$Resources:spanish.language,fechaNoValida%>"/></asp:CustomValidator></div></ContentTemplate><Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                    </Triggers>
                    </asp:UpdatePanel>

                </td>
            </tr>
            </table>
        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>
                

        <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
        <asp:Panel ID="Panel3"  Visible="true" runat="server">
            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1"><span class="item">Fecha Hasta</span></td><td class="col2"><span class="item">:</span></td><td class="col3">
                    <asp:UpdatePanel ID="UpdatePanelFechaHasta" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="calendario">
                            <div class="calendario_textbox">               
                                <asp:TextBox ID="FechaHasta" Columns="8" Width="80px" runat="server"></asp:TextBox><asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="FechaHasta"
                                    Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                    CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                </asp:MaskedEditExtender>
                            </div>
                            <div class="calendario_icono">
                                <asp:Image src="../App_Themes/admin_style/images/calendar.png" id="imgFechaHasta" alt="Calendario"  runat="server"  style="vertical-align: middle" />
                            </div>

                            &nbsp;&nbsp;&nbsp;<asp:CustomValidator ID="CustomValidator1" ControlToValidate="FechaHasta"  ClientValidationFunction="validaFechaDDMMAAAA" 
                                            Display="static" Font-Size="10" runat="server"><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:spanish.language,fechaNoValida%>"/></asp:CustomValidator></div></ContentTemplate><Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                    </Triggers>
                    </asp:UpdatePanel>

                </td>
                </tr>
            </table>
        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>
                

       

        <br />


            
        <table class="form" cellpadding="0px" cellspacing="0px">   
        <tr>
            <td class="col1"></td>
            <td class="col2"></td>
            <td class="col3">
                <asp:Button ID="Limpiar" runat="server" Text="<%$Resources:spanish.language,limpiar%>"      onclick="Limpiar_Click"  CausesValidation="false"     />
                <asp:Button ID="Buscar" runat="server" Text="Buscar"  CausesValidation="true"              onclick="Buscar_Click" style="height: 26px" />
            </td>
        </tr>
        </table>




    <asp:UpdatePanel ID="UpdatePanelErroresGrilla" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelErroresGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="ErroresGrilla" runat="server"></asp:Label></div></asp:Panel></ContentTemplate></asp:UpdatePanel><asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
     <ContentTemplate>
     
     
            <asp:Panel ID="PanelResoluciones"  Visible="true" runat="server">

                <asp:GridView ID="GridResoluciones"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" 
                       PageSize="30" 
                       OnPageIndexChanging="GridResoluciones_PageIndexChanged"
                       CssClass="mGrid"
                       OnRowDataBound="GridResoluciones_RowDataBound"
                       OnRowCommand="GridResoluciones_RowCommand"
                       PagerStyle-CssClass="pgr"
                       Width="100%">

                        <Columns>

                            
                            <asp:TemplateField HeaderText="Encontrado en">
                                <ItemTemplate>
                                     <asp:Label HeaderText="idArchivo" ID="gIDArchivo" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "archivoAdjunto.idArchivo") %>'></asp:Label>
                                     <asp:Label HeaderText="idEstadoVigencia" ID="gEstadoVigencia" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "vigencia.id") %>'></asp:Label>
                                     
                                     <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("origenRegistro.descripcion")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tipo de Documento">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("tipoDocumento.descripcion")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Origen">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("origen.descripcion")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Numero">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.numero")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField HeaderText="Fecha" DataField="fechaString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />

                                                     
                            <asp:TemplateField HeaderText="Resultado">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("resultado.descripcion")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            
                            <asp:TemplateField HeaderText="Vigencia">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("vigencia.descripcion")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                    

                            <asp:TemplateField HeaderText="Materia">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("materia.descripcion")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Referencias">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.descripcionDatosRef")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            


                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="120px">
                                <ItemTemplate>
                                    
                                    <asp:ImageButton ID="gVer" 
                                    Visible="true" 
                                    runat="server" 
                                    CausesValidation="false" 
                                    CommandName="VerResolucion"  
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "origenRegistro.id") + ";" + DataBinder.Eval(Container.DataItem, "idResolucion")  %>'
                                    ImageUrl="~/App_Themes/admin_style/images/ver.png" 
                                    Height="20px" 
                                    AlternateText="Ver Resolución" 
                                    ToolTip="Ver Resolución" />
                                    
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




 </asp:Content>