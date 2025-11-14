<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="examenPreliminar.ascx.cs" Inherits="SubPesca.Solicitudes.Registrar.examenPreliminar" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>


<asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
   <ContentTemplate>    
       <asp:panel ID="Panel1" runat="server">
            <asp:ValidationSummary ID="ValidationSummaryErrores" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList"  />
        </asp:panel>
    </ContentTemplate>
</asp:UpdatePanel>



<asp:UpdatePanel ID="UpdatePanelErroresSuperior" UpdateMode="Conditional" runat="server">
    <ContentTemplate>   
        <asp:Panel ID="PanelErroresSuperior" CssClass="Content_msgGrilla" Visible="false" runat="server">
            <div class="msgGrilla_div2">
                <asp:Label ID="ErroresSuperior" runat="server"></asp:Label>i
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

  

    <fieldset>
   

        <legend>Examen Preliminar</legend>
        <br />
        

        <table class="form" cellpadding="0px" cellspacing="0px" style="display:none;">
        <tr>
            <td>&nbsp;IdSolicitud: <asp:TextBox ID="IdSolicitud" runat="server"></asp:TextBox></td>
        </tr>
        </table>



        
        
<asp:UpdatePanel ID="UpdatePanelFormularioIngreso" UpdateMode="Conditional" runat="server">
    <ContentTemplate>              
        <asp:Panel ID="PanelFormularioIngreso" Visible="true" runat="server">
        

        <asp:UpdatePanel ID="UpdatePanelFlujoDocumental" UpdateMode="Conditional" runat="server">
            <ContentTemplate>              
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item"><asp:Literal ID="FlujoDocumentalLiteral" runat="server" Text="<%$Resources:spanish.language,flujoDocumental%>" /></span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:DropDownList ID="FlujoDocumental" AutoPostBack="true" runat="server" OnSelectedIndexChanged="FlujoDocumental_change"></asp:DropDownList> *</td>
                </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelTipoSalida" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelTipoSalida"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="TipoSalidaLiteral" runat="server" Text="<%$Resources:spanish.language,tipoSalida%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="TipoSalida" AutoPostBack="true" runat="server" OnSelectedIndexChanged="TipoSalida_change"></asp:DropDownList> *</td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        
        <asp:UpdatePanel ID="UpdatePanelTipoEntrada" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelTipoEntrada"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="TipoEntradaLiteral" runat="server" Text="<%$Resources:spanish.language,tipoEntrada%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="TipoEntrada" AutoPostBack="true" runat="server" OnSelectedIndexChanged="TipoEntrada_change"></asp:DropDownList> *</td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

            
        <asp:UpdatePanel ID="UpdatePanelOrigen" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelOrigen"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="OrigenLiteral" runat="server" Text="<%$Resources:spanish.language,origen%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="Origen" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Origen_Change"></asp:DropDownList> *</td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelDestinatario" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelDestinatario"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="DestinatarioLiteral" runat="server" Text="<%$Resources:spanish.language,destinatario%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="Destinatario" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Destinatario_Change"></asp:DropDownList> *</td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelTipoDocumento" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelTipoDocumento"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="TipoDocumentoLiteral" runat="server" Text="<%$Resources:spanish.language,tipoDocumento%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="TipoDocumento" AutoPostBack="true" runat="server" OnSelectedIndexChanged="TipoDocumento_change"></asp:DropDownList> *</td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelDocumentoPrincipal" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelDocumentoPrincipal"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="LiteralDocumentoPrincipal" runat="server" /></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="DocumentoPrincipal" AutoPostBack="false" runat="server"></asp:DropDownList> *</td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

       
        <asp:UpdatePanel ID="UpdatePanelNumeroRequerimiento" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelNumeroRequerimiento"  Visible="false" runat="server">
                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="col1"><span class="item"><asp:Literal ID="NRequerimientoLiteral" runat="server" Text="<%$Resources:spanish.language,nRequerimiento%>"/></span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3"><asp:DropDownList ID="NRequerimiento" AutoPostBack="true" runat="server" OnSelectedIndexChanged="NRequerimiento_change"></asp:DropDownList> *</td>
                        </tr>
                        </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelListaRequerimientos" UpdateMode="Conditional" runat="server">
            <ContentTemplate>  
                <asp:Panel ID="PanelListaRequerimientos"  Visible="false" runat="server">
                     <br />
               
                     <asp:ListView 
                            ID="ListViewEntradaRespuestaRequerimiento" 
                            runat="server" 
                            DataKeyNames="idDocPestana"
                            OnItemDataBound="ListViewEntradaRespuestaRequerimiento_ItemDataBound"
                            >
                            <LayoutTemplate>
                                    <table runat="server" id="table1" border="0" cellpadding="2" cellspacing="2" width="100%" style="border: 1px solid Silver; width: 100%; border-collapse: collapse;">
                                    <tr style="background-color: SkyBlue; height: 30px;">
                                        <td align="center" style="width: 100px;"><b><asp:Literal ID="SeleccioneDocumentoAsociadoLiteral" runat="server" Text="<%$Resources:spanish.language,seleccioneDocumentoAsociado%>"/></b></td>
                                        <td align="center" style="width: 200px;"><b><asp:Literal ID="AmbitoDocumentoAsociadoLiteral" runat="server" Text="<%$Resources:spanish.language,ambitoDocumentoAsociado%>"/></b></td>
                                        <td align="center"><b><asp:Literal ID="TipoDocumentoAsociadoLiteral" runat="server" Text="<%$Resources:spanish.language,tipoDocumentoAsociado%>"/></b></td>
                                        <td align="center" style="width: 150px;"><b><asp:Literal ID="ResultadoDocumentoAsociadoLiteral" runat="server" Text="<%$Resources:spanish.language,resultadoDocumentoAsociado%>"/></b></td>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                 </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr2" runat="server">
                                    <td id="Td1" runat="server" align="center">
                                        <asp:HiddenField runat="server" ID="HiddenIdDocGeneralResp" Value='<%#Eval("idDocGeneralResp") %>' />
                                        <asp:CheckBox ID="chkSeleccionado" runat="server" AutoPostBack="false" />            
                                    </td>

                                    <td id="Td2" runat="server" align="center">
                                        <asp:HiddenField runat="server" ID="HiddenIdDocPestana" Value='<%#Eval("idDocPestana") %>' />
                                        <asp:Label ID="Label2" runat="server"  Text='<%#Eval("ambito.descripcion") %>' />
                                    </td>

                                    <td id="Td3" runat="server" align="center">
                                        <asp:HiddenField runat="server" ID="HiddenTipoId" Value='<%#Eval("tipo.id") %>' />
                                        <asp:Label ID="Label3" runat="server"  Text='<%#Eval("tipo.descripcion") %>' />
                                    </td>

                                    <td id="Td4" runat="server" align="center">
                                        <asp:HiddenField runat="server" ID="HiddenIdResultado" Value='<%#Eval("estadoResultadoResp.id") %>' />
                                        <asp:DropDownList ID="AmbitoTipoResultado" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    </td>
                                </tr>

                            </ItemTemplate>
                        </asp:ListView>
                    </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

         
        <asp:UpdatePanel ID="UpdatePanelAmbito" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelAmbito"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="AmbitoLiteral" runat="server" Text="<%$Resources:spanish.language,ambito%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="Ambito" AutoPostBack="true" runat="server"  OnSelectedIndexChanged="Ambito_change"></asp:DropDownList> *</td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

         
        <asp:UpdatePanel ID="UpdatePanelTipo" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelTipo"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="TipoLiteral" runat="server" Text="<%$Resources:spanish.language,tipo%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="Tipo" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Tipo_change"></asp:DropDownList> *</td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

          
        <asp:UpdatePanel ID="UpdatePanelDocumentosAmbito" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelDocumentosAmbito"  Visible="false" runat="server">
    
                        <table class="form" cellpadding="0px" cellspacing="0px">
                         <tr>
                            <td class="col1"><span class="item"></span></td>
                            <td class="col2"><span class="item"></span></td>
                            <td class="col3"><span class="item"><asp:Literal ID="AgregarDocumentoAsociadoLiteral" runat="server" Text="<%$Resources:spanish.language,agregarDocumentoAsociado%>"/></span> <asp:ImageButton ID="btnAgregarDocumentoAsociado" runat="server" src="../../App_Themes/admin_style/images/add.png" onclick="GridViewSalidaDocumentoAsociado_Guardar"  /></td>
                        </tr>
                        </table>


                        <asp:GridView 
                        ID="GridViewSalidaDocumentoAsociado" 
                        runat="server" 
                        AutoGenerateColumns="False" 
                        CellPadding="4" 
                        ForeColor="#333333"
                        GridLines="None" 
                        CssClass="mGrid"
                        OnRowDataBound="GridViewSalidaDocumentoAsociado_RowDataBound"
                        PagerStyle-CssClass="pgr"
                        OnRowCommand="GridViewSalidaDocumentoAsociado_RowCommand"
                        >
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="Ambito">
                                <ItemTemplate>
                                <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                <%# DataBinder.Eval(Container, "DataItem.ambito.descripcion") %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="Tema">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.tipo.descripcion") %>
                                </ItemTemplate>
                            </asp:TemplateField> 

                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "index") %>'
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
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelNumero" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelNumero"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="NumeroLiteral" runat="server" Text="<%$Resources:spanish.language,numero%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="Numero" autocomplete="tel-extension" runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="8"></asp:TextBox>&nbsp;<asp:Label ID="RequeridoNumero" runat="server"></asp:Label></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        
        <asp:UpdatePanel ID="UpdatePanelFecha" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelFecha"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="FechaLiteral" runat="server" Text="<%$Resources:spanish.language,fecha%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">

                            <div class="calendario">
                                <div class="calendario_textbox">               
                                    <asp:TextBox ID="Fecha" Columns="8" Width="80px" runat="server"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="Fecha"
                                        Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                    </asp:MaskedEditExtender>

                                </div>
                                <div class="calendario_icono">
                                    <asp:Image src="../../App_Themes/admin_style/images/calendar.png" id="fechaImgDinamica" alt="Calendario" runat="server" style="vertical-align: middle" />&nbsp;<asp:Label ID="RequeridoFecha" runat="server"></asp:Label>
                                </div>
                                <asp:CustomValidator ID="ccFecha" ControlToValidate="Fecha" ErrorMessage="Fecha" ClientValidationFunction="validaFechaDDMMAAAA" 
                                    Display="static" Font-Size="10" runat="server"><asp:Literal ID="FechaNoValidaLiteral" runat="server" Text="<%$Resources:spanish.language,fechaNoValida%>"/></asp:CustomValidator></div></td></tr></table></asp:Panel></ContentTemplate></asp:UpdatePanel><asp:UpdatePanel ID="UpdatePanelNuevaFecha" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelNuevaFecha"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="NuevaFechaLiteral" runat="server" Text="<%$Resources:spanish.language,nuevafecha%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td><td class="col3">

                            <div class="calendario">
                                <div class="calendario_textbox">               
                                    <asp:TextBox ID="NuevaFecha" Columns="8" Width="80px" runat="server"></asp:TextBox><asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="NuevaFecha"
                                        Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                    </asp:MaskedEditExtender>

                                </div>
                                <div class="calendario_icono">
                                    <asp:Image src="../../App_Themes/admin_style/images/calendar.png" id="nuevaFechaImgDinamica" alt="Calendario" runat="server" style="vertical-align: middle" />
                                </div>

                                <asp:CustomValidator ID="ccNuevaFecha" ControlToValidate="NuevaFecha" ErrorMessage="Fecha" ClientValidationFunction="validaFechaDDMMAAAA" 
                                    Display="static" Font-Size="10" runat="server"><asp:Literal ID="NuevaFechaNoValidaLiteral" runat="server" Text="<%$Resources:spanish.language,fechaNoValida%>"/></asp:CustomValidator></div></td></tr></table></asp:Panel></ContentTemplate></asp:UpdatePanel><asp:UpdatePanel ID="UpdatePanelNumeroCI" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelNumeroCI"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="NumeroCILiteral" runat="server" Text="<%$Resources:spanish.language,numeroCI%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td><td class="col3"><asp:TextBox ID="NumeroCI"  autocomplete="tel-extension" runat="server"  onKeyUp="return onlyNumeric(this)"  MaxLength="8"  AutoPostBack="true" ontextchanged="NumeroCI_TextChanged"></asp:TextBox>&nbsp;<asp:Label ID="RequeridoNumeroCI" runat="server"></asp:Label>&nbsp;<asp:Literal runat="server" ID="NumeroCIMensaje" Text=""></asp:Literal></td></tr></table></asp:Panel></ContentTemplate></asp:UpdatePanel><asp:UpdatePanel ID="UpdatePanelFechaCI" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelFechaCI"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="FechaCILiteral" runat="server" Text="<%$Resources:spanish.language,fechaCI%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td><td class="col3">

                            <div class="calendario">
                                    <div class="calendario_textbox">               
                                    <asp:TextBox ID="FechaCI" Columns="8" Width="80px" runat="server"></asp:TextBox><asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="FechaCI"
                                        Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                    </asp:MaskedEditExtender>

                                </div>
                                <div class="calendario_icono">
                                    <asp:Image src="../../App_Themes/admin_style/images/calendar.png" id="fechaCIImgDinamica" alt="Calendario" runat="server" style="vertical-align: middle" />&nbsp;<asp:Label ID="RequeridoFechaCI" runat="server"></asp:Label></div><asp:CustomValidator ID="cvFechaCI" ControlToValidate="FechaCI" ErrorMessage="Fecha" ClientValidationFunction="validaFechaDDMMAAAA" 
                                    Display="static" Font-Size="10" runat="server"><asp:Literal ID="FechaCINoValidaLiteral" runat="server" Text="<%$Resources:spanish.language,fechaNoValida%>"/></asp:CustomValidator></div></td></tr></table></asp:Panel></ContentTemplate></asp:UpdatePanel><asp:UpdatePanel ID="UpdatePanelResultado" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelResultado"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="ResultadoLiteral" runat="server" Text="<%$Resources:spanish.language,resultado%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td><td class="col3"><asp:DropDownList ID="Resultado" runat="server" AutoPostBack="false"></asp:DropDownList> *</td></tr></table></asp:Panel></ContentTemplate></asp:UpdatePanel><asp:UpdatePanel ID="UpdatePanelArchivo" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelArchivo"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="ArchivoAdjuntoLiteral" runat="server" Text="<%$Resources:spanish.language,archivoAdjunto%>"/></span></td><td class="col2"><span class="item">:</span></td><td class="col3">
                                <asp:FileUpload ID="ArchivoAdjunto" MaxLength="40" Width="200px" runat="server"></asp:FileUpload>&nbsp;<asp:Label ID="RequeridoArchivoAdjunto" runat="server"></asp:Label><asp:RegularExpressionValidator ID="REGEXFileUploadLogo" runat="server" ErrorMessage="Formato Archivo Incorrecto" ControlToValidate="ArchivoAdjunto" ValidationExpression= "(.*).(.doc|.DOC|.pdf|.PDF|.docx|.DOCX|.xls|.xlsx|.XLS|.XLSX|.dwg|.DWG|.dxf|.DXF|.jpg|.JPG|.jpeg|.JPEG)$" />

                        </td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="Guardar"/>
            </Triggers>
        </asp:UpdatePanel>


        <table class="form" cellpadding="0px" cellspacing="0px">   
        <tr>
            <td class="col1"></td>
            <td class="col2"></td>
            <td class="col3">
                <asp:Button ID="Limpiar" runat="server" Text="<%$Resources:spanish.language,limpiar%>"  CausesValidation="false" onclick="Limpiar_Click" />
                <asp:Button 
                            ID="Guardar" runat="server" Text="<%$Resources:spanish.language,guardar%>"  
                            CausesValidation="true" onclick="Guardar_Click" style="height: 26px" /></td>
        </tr>
        </table>


        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanelErroresInferior" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelErroresInferior" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="ErroresInferior" runat="server"></asp:Label></div></asp:Panel></ContentTemplate></asp:UpdatePanel><br />        
                        
    <asp:UpdatePanel ID="UpdatePanelGridRequerimiento" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelGridRequerimiento"  Visible="true" runat="server" CssClass="Content_Grilla">
                
                <asp:GridView 
                ID="GridRequerimiento" 
                runat="server" 
                AutoGenerateColumns="False" 
                CellPadding="4" 
                ForeColor="#333333"
                TabIndex="1"
                GridLines="None" 
                CssClass="mGrid"
                OnRowDataBound="GridRequerimiento_RowDataBound"
                PagerStyle-CssClass="pgr"
                OnRowCommand="GridRequerimiento_RowCommand"
                OnRowCreated="GridRequerimiento_RowCreated">
                        
                <Columns>


                    <asp:TemplateField Visible="true">
                        <ItemTemplate>
                            <asp:Label HeaderText="validaConforme" ID="validaConforme" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "validaConforme") %>'></asp:Label><asp:Label HeaderText="idEstadoVigencia" ID="hidden3" runat="server" Visible="true" Text='<%# (DataBinder.Eval(Container.DataItem, "idEstadoVigRequerimiento") == DBNull.Value? DataBinder.Eval(Container.DataItem, "idEstadoVigencia"): DataBinder.Eval(Container.DataItem, "idEstadoVigRequerimiento")) %>'></asp:Label><asp:Label HeaderText="idDocGeneral" ID="hidden1" runat="server" Visible="true" Text='<%# (DataBinder.Eval(Container.DataItem, "idDocGeneral") == DBNull.Value? DataBinder.Eval(Container.DataItem, "idDocGeneralResp") : DataBinder.Eval(Container.DataItem, "idDocGeneral")) %>'></asp:Label><asp:Label HeaderText="rowspan" ID="hidden4" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "columnas") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField Visible="true">
                        <ItemTemplate>
                            <asp:Label HeaderText="idPestana" ID="hidden2" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idPestana") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="Ámbito"         DataField="nombrePestana" SortExpression="nombrePestana" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Tema"           DataField="nombreSubRequerimiento" SortExpression="nombreSubRequerimiento" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            
                    <asp:BoundField HeaderText="Tipo Documento" DataField="nombreTipoDocReq" SortExpression="nombreTipoDocReq" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Nº"             DataField="numeroReq" SortExpression="numeroReq"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Fecha"          DataField="fechaReq" SortExpression="fechaReq" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField HeaderText="Destino"        DataField="nombreTipoDestinatario" SortExpression="nombreTipoDestinatario" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            

                    <asp:BoundField HeaderText="Tipo Documento"     DataField="nombreTipoDocResp"  SortExpression="nombreTipoDocResp"      ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Origen"             DataField="nombreTipoOrigen"     SortExpression="nombreTipoOrigen"     ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Nº"                 DataField="numeroResp"   SortExpression="numeroResp"    ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Fecha"              DataField="fechaResp"    SortExpression="fechaResp"    ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField HeaderText="Nº C.I"             DataField="numeroRespCI" SortExpression="numeroRespCI"    ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Fecha C.I"          DataField="fechaRespCI"  SortExpression="fechaRespCI"    ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField HeaderText="Resultado"          DataField="nombreResultadoResp" SortExpression="nombreResultadoResp"    ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />

                    <asp:BoundField HeaderText="Estado Requerimientos" DataField="estadoCompletado"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Evaluación"           DataField="estadoFinal"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />


                    <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="200px">
                        <ItemTemplate>
                            <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# (DataBinder.Eval(Container.DataItem, "idDocGeneral") == DBNull.Value? DataBinder.Eval(Container.DataItem, "idDocGeneralResp") : DataBinder.Eval(Container.DataItem, "idDocGeneral"))  + ";" + DataBinder.Eval(Container.DataItem, "idPestana") %>'
                                    ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                            <asp:ImageButton ID="gAdministrar" Visible="false" runat="server" CausesValidation="false" CommandName="Administrar" CommandArgument='<%# (DataBinder.Eval(Container.DataItem, "idDocGeneral") == DBNull.Value? DataBinder.Eval(Container.DataItem, "idDocGeneralResp"): DataBinder.Eval(Container.DataItem, "idDocGeneral")) + ";" + DataBinder.Eval(Container.DataItem, "idPestana")  %>'
                                    ImageUrl="../../App_Themes/admin_style/images/tabs.png" Height="20px" AlternateText="Administrar Requerimiento" ToolTip="Administrar Requerimiento" />

                            <asp:ImageButton ID="gEvaluar" Visible="false" runat="server" CausesValidation="false" CommandName="Evaluar" CommandArgument='<%# (DataBinder.Eval(Container.DataItem, "idDocGeneral") == DBNull.Value? DataBinder.Eval(Container.DataItem, "idDocGeneralResp"): DataBinder.Eval(Container.DataItem, "idDocGeneral")) + ";" + DataBinder.Eval(Container.DataItem, "idPestana")   %>'
                                    ImageUrl="../../App_Themes/admin_style/images/evaluar.png" Height="20px" AlternateText="Evaluar Requerimiento" ToolTip="Evaluar Requerimiento" />

                            <asp:ImageButton ID="gNoVigente" Visible="false" runat="server" CausesValidation="false" CommandName="NoVigente" CommandArgument='<%# (DataBinder.Eval(Container.DataItem, "idDocGeneral") == DBNull.Value? DataBinder.Eval(Container.DataItem, "idDocGeneralResp"): DataBinder.Eval(Container.DataItem, "idDocGeneral")) + ";" + DataBinder.Eval(Container.DataItem, "idPestana")  %>'
                                    ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Pasar a No Vigente" ToolTip="Pasar a No Vigente" />

                            <asp:ImageButton ID="gVigente" Visible="false" runat="server" CausesValidation="false" CommandName="Vigente" CommandArgument='<%# (DataBinder.Eval(Container.DataItem, "idDocGeneral") == DBNull.Value? DataBinder.Eval(Container.DataItem, "idDocGeneralResp"): DataBinder.Eval(Container.DataItem, "idDocGeneral")) + ";" + DataBinder.Eval(Container.DataItem, "idPestana")  %>'
                                    ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Pasar a Vigente" ToolTip="Pasar a Vigente" />

                            <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# (DataBinder.Eval(Container.DataItem, "idDocGeneral") == DBNull.Value? DataBinder.Eval(Container.DataItem, "idDocGeneralResp"): DataBinder.Eval(Container.DataItem, "idDocGeneral")) + ";" + DataBinder.Eval(Container.DataItem, "idPestana")  %>'
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
    
             
    </fieldset>           
