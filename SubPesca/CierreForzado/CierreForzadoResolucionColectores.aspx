<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administrador/SitioAdmin.Master" CodeBehind="CierreForzadoResolucionColectores.aspx.cs" 
Inherits="SubPesca.CierreForzado.CierreForzadoResolucionColectores" Theme="admin_style" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>




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
            <span id="titulo_modulo">Cierre Forzado Colectores de Semilla - Resumen</span>
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />

    <fieldset>
        <legend>Informe de Cierre</legend>

                <asp:HiddenField ID="idInformeTecnico" runat="server" />

                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>              
                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="col1"><span class="item">Informe Técnico</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3"><asp:Literal ID="NumInforme" runat="server"></asp:Literal></td>
                        </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>


    </fieldset>     

    <br />


    <fieldset>

    <asp:Panel ID="PanelInformeRechazo" runat="server">

        <asp:UpdatePanel ID="UpdatePanelSolicitudes" UpdateMode="Conditional" runat="server">
            <ContentTemplate>      

                <asp:GridView 
                    ID="GridVwHeaderChckbox" 
                    DataKeyNames="idSolConcesion"
                    runat="server" 
                    AutoGenerateColumns="False" 
                    AllowPaging="true"
                    PageSize="30"
                    OnPageIndexChanging="GridVwHeaderChckbox_PageIndexChanged"
                    AllowSorting="true"
                    CellPadding="4" 
                    ForeColor="#333333"
                    TabIndex="1"
                    GridLines="None" 
                    CssClass="mGrid"
                    OnRowDataBound="GridVwHeaderChckbox_RowDataBound"
                    PagerStyle-CssClass="pgr"
                    OnRowCommand="GridVwHeaderChckbox_RowCommand"
                    OnRowCreated="GridVwHeaderChckbox_RowCreated">
                        
                    <Columns>

                        <asp:TemplateField ItemStyle-Width="40px" > 
                            <HeaderTemplate>
                                ¿Cuenta con Resolución SSP de Cierre?
                                <br />
                                <%--<asp:CheckBox ID="chkboxSelectAll2" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" /> --%>
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server" Enabled="false"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nº Pert">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="HiddenIdSolConcesion"   Value='<%#Eval("idSolConcesion") %>' />
                                <asp:HiddenField runat="server" ID="HiddenIdDocITCierre"    Value='<%#Eval("solicitudCierre.docITCierre.idRequerimiento") %>' />
                                <asp:HiddenField runat="server" ID="HiddenTieneIT"          Value='<%#Eval("tieneIT") %>' />
                                <asp:HiddenField runat="server" ID="HiddenTieneSSP"         Value='<%#Eval("tieneSSP") %>' />
                                <%# DataBinder.Eval(Container, "DataItem.datosSolicitudUE.numIdentSolicitud")%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Región">
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

                        <asp:TemplateField HeaderText="Especie">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.DescripcionEspeciesComa")%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Existencia RCA">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.tieneRCA")%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tamaño de la Concesión">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.superficieCalculada")%>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Estado Solicitud">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.estadoActual.descripcion")%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                        <asp:TemplateField HeaderText="Tipo Trámite">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.tipoTramite.descripcion")%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        

                         <asp:TemplateField HeaderText="Estado Cierre Forzado">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.estadoCierreForzado.descripcion")%>
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

             
            </ContentTemplate>
        </asp:UpdatePanel>

    <br />
    <asp:Panel ID="Panel2" Visible="false" runat="server">
    
        <asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
           <ContentTemplate>    
               <asp:panel ID="Panel1" runat="server">
                    <asp:ValidationSummary ID="ValidationSummaryErrores" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="GrupoResolucionSSPRechazo" />
                </asp:panel>
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
        <legend>Resolución SSP de Cierre</legend>
        <br />
        
        
        <asp:UpdatePanel ID="UpdatePanelFormularioIngreso" UpdateMode="Conditional" runat="server">
            <ContentTemplate>              
            <asp:Panel ID="PanelFormularioIngreso" Visible="true" runat="server">



            
                <table cellpadding="0px" cellspacing="0px" width="100%">   
                <tr>
                    <td class="col1"></td>
                    <td class="col2"></td>
                    <td class="col3" align="right">
                        <asp:ImageButton  ID="LimpiarFechas" runat="server" AlternateText="Limpiar Fechas" Height="20px"  ImageUrl="~/App_Themes/admin_style/images/clean.png"  onclick="Limpiar_Click" ToolTip="Limpiar Fechas" />
                    </td>
                </tr>
                </table>


                
        

                <asp:UpdatePanel ID="UpdatePanelFlujoDocumental" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>              
                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="col1"><span class="item"><asp:Literal ID="FlujoDocumentalLiteral" runat="server" Text="<%$Resources:spanish.language,flujoDocumental%>" /></span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3">
                                <asp:DropDownList ID="FlujoDocumental" AutoPostBack="true" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="FlujoDocumental" ErrorMessage="Flujo Documental" Display="Static" InitialValue="0" ValidationGroup="GrupoResolucionSSPRechazo"> * </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>

        
                <asp:UpdatePanel ID="UpdatePanelTipoEntrada" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelTipoEntrada"  Visible="true" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item"><asp:Literal ID="TipoEntradaLiteral" runat="server" Text="<%$Resources:spanish.language,tipoEntrada%>"/></span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3">
                                    <asp:DropDownList ID="TipoEntrada" AutoPostBack="true" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="TipoEntrada" ErrorMessage="Tipo de Entrada" Display="Static" InitialValue="0" ValidationGroup="GrupoResolucionSSPRechazo"> * </asp:RequiredFieldValidator>                                        
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>



            
                <asp:UpdatePanel ID="UpdatePanelOrigen" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelOrigen"  Visible="true" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item"><asp:Literal ID="OrigenLiteral" runat="server" Text="<%$Resources:spanish.language,origen%>"/></span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3">
                                    <asp:DropDownList ID="Origen" AutoPostBack="true" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="Origen" ErrorMessage="Origen" Display="Static" InitialValue="0" ValidationGroup="GrupoResolucionSSPRechazo"> * </asp:RequiredFieldValidator>                                            
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <asp:UpdatePanel ID="UpdatePanelTipoDocumento" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelTipoDocumento"  Visible="true" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item"><asp:Literal ID="TipoDocumentoLiteral" runat="server" Text="<%$Resources:spanish.language,tipoDocumento%>"/></span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3">
                                    <asp:DropDownList ID="TipoDocumento" AutoPostBack="true" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ControlToValidate="TipoDocumento" ErrorMessage="Tipo Documento" Display="Static" InitialValue="0" ValidationGroup="GrupoResolucionSSPRechazo"> * </asp:RequiredFieldValidator>                                            
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>


         
                <asp:UpdatePanel ID="UpdatePanelTipo" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelTipo"  Visible="true" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item"><asp:Literal ID="TipoLiteral" runat="server" Text="<%$Resources:spanish.language,tipo%>"/></span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3">
                                    <asp:DropDownList ID="Tipo" AutoPostBack="true" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" ControlToValidate="Tipo" ErrorMessage="Tema" Display="Static" InitialValue="0" ValidationGroup="GrupoResolucionSSPRechazo"> * </asp:RequiredFieldValidator>                                                
                                </td>
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
                                <td class="col1"><span class="item"><asp:Literal ID="NumeroLiteral" runat="server" Text="<%$Resources:spanish.language,numero%>"/></span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3">
                                    <asp:TextBox ID="Numero" autocomplete="tel-extension" runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="8"></asp:TextBox>
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" ControlToValidate="Numero" ErrorMessage="Numero" Display="Static" InitialValue="" ValidationGroup="GrupoResolucionSSPRechazo"> * </asp:RequiredFieldValidator>                                                
                                    </td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

        
                <asp:UpdatePanel ID="UpdatePanelFecha" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelFecha"  Visible="true" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item"><asp:Literal ID="FechaLiteral" runat="server" Text="<%$Resources:spanish.language,fecha%>"/></span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3">

                                    <div class="calendario">
                                        <div class="calendario_textbox">               
                                            <asp:TextBox ID="Fecha" Columns="8" Width="80px" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" ControlToValidate="Fecha" ErrorMessage="Fecha" Display="Static" InitialValue="" ValidationGroup="GrupoResolucionSSPRechazo"> * </asp:RequiredFieldValidator>                                                 
                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="Fecha"
                                                Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                                CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                            </asp:MaskedEditExtender>

                                        </div>
                                        <div class="calendario_icono">
                                            <asp:Image src="../App_Themes/admin_style/images/calendar.png" id="fechaImgDinamica" alt="Calendario" runat="server" style="vertical-align: middle" />
                                        </div>
                                        <asp:CustomValidator ID="ccFecha" ControlToValidate="Fecha" ErrorMessage="Fecha" ClientValidationFunction="validaFechaDDMMAAAA" 
                                            Display="static" Font-Size="10" runat="server"><asp:Literal ID="FechaNoValidaLiteral" runat="server" Text="<%$Resources:spanish.language,fechaNoValida%>"/></asp:CustomValidator></div>
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <asp:UpdatePanel ID="UpdatePanelNumeroCI" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelNumeroCI"  Visible="false" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item"><asp:Literal ID="NumeroCILiteral" runat="server" Text="<%$Resources:spanish.language,numeroCI%>"/></span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3">
                                    <asp:TextBox ID="NumeroCI"  autocomplete="tel-extension" runat="server"  onKeyUp="return onlyNumeric(this)"  MaxLength="8"  AutoPostBack="true" ontextchanged="NumeroCI_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator11" runat="server" ControlToValidate="NumeroCI" ErrorMessage="Numero C.I." Display="Static" InitialValue="" ValidationGroup="GrupoResolucionSSPRechazo"> * </asp:RequiredFieldValidator>                                                
                                    &nbsp;<asp:Literal runat="server" ID="NumeroCIMensaje" Text=""></asp:Literal>
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
        

                <asp:UpdatePanel ID="UpdatePanelFechaCI" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelFechaCI"  Visible="false" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item"><asp:Literal ID="FechaCILiteral" runat="server" Text="<%$Resources:spanish.language,fechaCI%>"/></span></td>
                                <td class="col2"><span class="item">:</span></td><td class="col3">

                                    <div class="calendario">
                                            <div class="calendario_textbox">               
                                            <asp:TextBox ID="FechaCI" Columns="8" Width="80px" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator10" runat="server" ControlToValidate="FechaCI" ErrorMessage="Fecha C.I." Display="Static" InitialValue="" ValidationGroup="GrupoResolucionSSPRechazo"> * </asp:RequiredFieldValidator>                                                 
                                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="FechaCI"
                                                Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                                CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                            </asp:MaskedEditExtender>

                                        </div>
                                        <div class="calendario_icono">
                                            <asp:Image src="../../App_Themes/admin_style/images/calendar.png" id="fechaCIImgDinamica" alt="Calendario" runat="server" style="vertical-align: middle" />&nbsp;<asp:Label ID="RequeridoFechaCI" runat="server"></asp:Label>
                                        </div>

                                        <asp:CustomValidator ID="cvFechaCI" ControlToValidate="FechaCI" ErrorMessage="Fecha" ClientValidationFunction="validaFechaDDMMAAAA" 
                                            Display="static" Font-Size="10" runat="server"><asp:Literal ID="FechaCINoValidaLiteral" runat="server" Text="<%$Resources:spanish.language,fechaNoValida%>"/></asp:CustomValidator></div>
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                                    
                                    
                <asp:UpdatePanel ID="UpdatePanelResultado" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelResultado"  Visible="true" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item"><asp:Literal ID="ResultadoLiteral" runat="server" Text="<%$Resources:spanish.language,resultado%>"/></span></td>
                                <td class="col2"><span class="item">:</span></td><td class="col3">
                                    <asp:DropDownList ID="Resultado" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator8" runat="server" ControlToValidate="Resultado" ErrorMessage="Resultado" Display="Static" InitialValue="0" ValidationGroup="GrupoResolucionSSPRechazo"> * </asp:RequiredFieldValidator>                                                 
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <asp:UpdatePanel ID="UpdatePanelArchivo" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelArchivo"  Visible="true" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item"><asp:Literal ID="ArchivoAdjuntoLiteral" runat="server" Text="<%$Resources:spanish.language,archivoAdjunto%>"/></span></td><td class="col2"><span class="item">:</span></td><td class="col3">
                                        <asp:FileUpload ID="ArchivoAdjunto" MaxLength="40" Width="200px" runat="server"></asp:FileUpload> *
                                        <asp:RequiredFieldValidator id="RequiredFieldValidator9" runat="server" ControlToValidate="ArchivoAdjunto" ErrorMessage="Archivo Adjunto" Display="Static" InitialValue="" ValidationGroup="GrupoResolucionSSPRechazo"> * </asp:RequiredFieldValidator>                                                 
                                        <asp:RegularExpressionValidator ID="REGEXFileUploadLogo" runat="server" ErrorMessage="Formato Archivo Incorrecto" ControlToValidate="ArchivoAdjunto" ValidationExpression= "(.*).(.doc|.DOC|.pdf|.PDF|.docx|.DOCX|.xls|.xlsx|.XLS|.XLSX|.dwg|.DWG|.dxf|.DXF|.jpg|.JPG|.jpeg|.JPEG)$" />
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Guardar"/>
                    </Triggers>
                </asp:UpdatePanel>


                <br />


                <asp:UpdatePanel ID="UpdatePanelErroresInferior" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>   
                        <asp:Panel ID="PanelErroresInferior" CssClass="Content_msgGrilla" Visible="false" runat="server">
                            <div class="msgGrilla_div2">
                                <asp:Label ID="ErroresInferior" runat="server"></asp:Label>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
        
        

            </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
                        
        </fieldset>


        <br />

        <table class="form" cellpadding="0px" cellspacing="0px">   
        <tr>
            <td class="col1"></td>
            <td class="col2"></td>
            <td class="col3">
                <asp:Button ID="Guardar" runat="server" Text="<%$Resources:spanish.language,guardar%>"  CausesValidation="true" onclick="Guardar_Click" style="height: 26px" ValidationGroup="GrupoResolucionSSPRechazo" />
            </td>
        </tr>
        </table>
            

        </asp:Panel>
        </asp:Panel>


    </fieldset>     
    

    

</asp:Content>