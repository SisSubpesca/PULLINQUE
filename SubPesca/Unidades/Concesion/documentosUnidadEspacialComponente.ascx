<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="documentosUnidadEspacialComponente.ascx.cs" Inherits="SubPesca.Unidades.Concesion.documentosUnidadEspacialComponente" %>


<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

    
    <asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>
   
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
                    <asp:Label ID="ErroresSuperior" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanelDocumentos" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        <asp:Panel ID="PanelDocumentos" runat="server" Visible="true">

        
            <fieldset>
   

            <legend>Documentos de <asp:Label ID="NombreUnidadEspacialResoluciones" runat="server"></asp:Label></legend>
            <br />
                
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
                                <td class="col3"><asp:DropDownList ID="Origen" AutoPostBack="true" runat="server"></asp:DropDownList> *</td>
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
                                <td class="col3"><asp:DropDownList ID="Destinatario" AutoPostBack="true" runat="server"></asp:DropDownList> *</td>
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
                                <td class="col3"><asp:DropDownList ID="TipoDocumento" AutoPostBack="true" runat="server"></asp:DropDownList> *</td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

         
                <asp:UpdatePanel ID="UpdatePanelTema" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelTema"  Visible="true" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item"><asp:Literal ID="TipoLiteral" runat="server" Text="<%$Resources:spanish.language,tipo%>"/></span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3"><asp:TextBox ID="Tema"  runat="server" MaxLength="200" Columns="30" /> *</td>
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
                                <td class="col3"><asp:TextBox ID="Numero" autocomplete="tel-extension" runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="12"></asp:TextBox>&nbsp;<asp:Label ID="RequeridoNumero" runat="server"></asp:Label></td>
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
                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="Fecha"
                                        Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                    </asp:MaskedEditExtender>
                                </div>

                                <asp:Panel ID="PanelCalendarioFecha" Visible="true" runat="server">
                                    <div class="calendario_icono">
                                        <img  src="../../App_Themes/admin_style/images/calendar.png" id="imgFecha" alt="Calendario"  runat="server"  style="vertical-align: middle" />&nbsp;<asp:Label ID="RequeridoFecha" runat="server"></asp:Label>
                                    </div>
                                </asp:Panel>

                                <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidatorFecha" 
                                    runat="server"
                                    ControlToValidate="Fecha"
                                    ForeColor="Red"
                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" 
                                    ErrorMessage="Ingrese formato válido"
                                    ValidationGroup="grupo1">
                                    </asp:RegularExpressionValidator>
                                  
                                </div>

                                </td>
                            </tr>
                            </table>

                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                                     
                                   
                                            
                <asp:UpdatePanel ID="UpdatePanelNumeroCI" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelNumeroCI"  Visible="true" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item"><asp:Literal ID="NumeroCILiteral" runat="server" Text="<%$Resources:spanish.language,numeroCI%>"/></span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3"><asp:TextBox ID="NumeroCI"  autocomplete="tel-extension" runat="server"  onKeyUp="return onlyNumeric(this)"  MaxLength="8"  AutoPostBack="true">
                                </asp:TextBox>&nbsp;<asp:Label ID="RequeridoNumeroCI" runat="server"></asp:Label>&nbsp;<asp:Literal runat="server" ID="NumeroCIMensaje" Text="">
                                </asp:Literal>
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                                
                                
                                
                <asp:UpdatePanel ID="UpdatePanelFechaCI" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelFechaCI"  Visible="true" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item"><asp:Literal ID="FechaCILiteral" runat="server" Text="<%$Resources:spanish.language,fechaCI%>"/></span></td>
                                <td class="col2"><span class="item">:</span></td><td class="col3">

                                <div class="calendario">
                                    <div class="calendario_textbox">               
                                    <asp:TextBox ID="FechaCI" Columns="8" Width="80px" runat="server"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="FechaCI"
                                        Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                    </asp:MaskedEditExtender>

                                </div>

                                <asp:Panel ID="PanelCalendarioFechaCI" Visible="true" runat="server">
                                    <div class="calendario_icono">
                                        <asp:Image src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaCI" alt="Calendario" runat="server" style="vertical-align: middle" />&nbsp;<asp:Label ID="RequeridoFechaCI" runat="server"></asp:Label>
                                    </div>
                                </asp:Panel>

                                   <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator1" 
                                    runat="server"
                                    ControlToValidate="FechaCI"
                                    ForeColor="Red"
                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" 
                                    ErrorMessage="Ingrese formato válido"
                                    ValidationGroup="grupo1">
                                    </asp:RegularExpressionValidator>
                                  

                                    </div>
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

                 <asp:UpdatePanel ID="UpdatePanelObservaciones" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>   
                        <asp:Panel ID="PanelObservaciones"  Visible="true" runat="server">

                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item">Observaciones</span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3"><asp:TextBox  ID="Observaciones" TextMode="multiline" Columns="50" Rows="5" runat="server" AutoPostBack="false"></asp:TextBox></td>
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
                        <asp:Button ID="Guardar" runat="server" Text="<%$Resources:spanish.language,guardar%>"  CausesValidation="true" onclick="Guardar_Click" />
                    </td>
                </tr>
                </table>


                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>



            <asp:UpdatePanel ID="UpdatePanelErroresInferior" UpdateMode="Conditional" runat="server">
                <ContentTemplate>   
                    <asp:Panel ID="PanelErroresInferior" CssClass="Content_msgGrilla" Visible="false" runat="server">
                        <div class="msgGrilla_div2">
                            <asp:Label ID="ErroresInferior" runat="server"></asp:Label></div></asp:Panel></ContentTemplate></asp:UpdatePanel>
                            
                            
            <br />
            
            
            <asp:UpdatePanel ID="UpdatePanelGridDocumento" UpdateMode="Conditional" runat="server">
                <ContentTemplate>                        
                    <asp:Panel ID="PanelGridDocumento"  Visible="true" runat="server">
                
                        <asp:GridView 
                        ID="GridDocumento" 
                        runat="server" 
                        AutoGenerateColumns="False" 
                        CellPadding="4" 
                        ForeColor="#333333"
                        TabIndex="1"
                        GridLines="None" 
                        CssClass="mGrid"
                        OnRowDataBound="GridDocumento_RowDataBound"
                        PagerStyle-CssClass="pgr"
                        OnRowCommand="GridDocumento_RowCommand"
                        OnRowCreated="GridDocumento_RowCreated">
                        
                        <Columns>


                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label HeaderText="Vigencia" ID="gIdEstadoVigencia" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "estadoVigencia.id") %>'></asp:Label>
                                    <asp:Label HeaderText="Vigencia" ID="gIdArchivo" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "estadoVigencia.id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                       
                            <asp:BoundField HeaderText="Flujo Documental"   DataField="flujoDocumental.descripcion"     SortExpression="flujoDocumental.descripcion"        ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />


                            <asp:TemplateField Visible="true" HeaderText="Origen/Destinatario">
                                <ItemTemplate>
                                    <asp:Label ID="OrigenColum"         runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "origen.descripcion") %>'></asp:Label>
                                    <asp:Label ID="DestinatarioColum"   runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "destinatario.descripcion") %>'></asp:Label>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            


                            <asp:BoundField HeaderText="Tipo Documento"     DataField="tipoDocumento.descripcion"       SortExpression="tipoDocumento.descripcion"          ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Tema"               DataField="nombreTema"                      SortExpression="nombreTema"                                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Numero"             DataField="numero"                          SortExpression="numero"                             ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Fecha"              DataField="fecha"                           SortExpression="fecha"                              ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField HeaderText="Numero C.I."        DataField="numeroCI"                        SortExpression="numeroCI"                           ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Fecha C.I."         DataField="fechaCI"                         SortExpression="fechaCI"                            ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField HeaderText="Estado"             DataField="estadoVigencia.descripcion"      SortExpression="estadoVigencia.descripcion"         ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Observaciones"      DataField="observaciones"                   SortExpression="observaciones"                      ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            

                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                <ItemTemplate>

                                    <asp:ImageButton ID="gNoVigente" Visible="false" runat="server" CausesValidation="false" CommandName="NoVigente" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocConcesion") %>'
                                            ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Pasar a No Vigente" ToolTip="Pasar a No Vigente" />

                                    <asp:ImageButton ID="gVigente" Visible="false" runat="server" CausesValidation="false" CommandName="Vigente" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocConcesion")  %>'
                                            ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Pasar a Vigente" ToolTip="Pasar a Vigente" />

                                    <asp:ImageButton ID="gDescargar" Visible="false" runat="server" CausesValidation="false" CommandName="Descargar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocConcesion") %>' 
                                        ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" />

                                    <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocConcesion")  %>'
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
        

            </asp:Panel>
            </ContentTemplate>
            </asp:UpdatePanel>
            
            
        <br />



    <script type="text/javascript">
         invoca_calendarios("documentosUnidadEspacial");
    </script>
