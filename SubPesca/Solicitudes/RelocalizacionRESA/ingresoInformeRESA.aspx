<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="ingresoInformeRESA.aspx.cs"
 Inherits="SubPesca.Solicitudes.RelocalizacionRESA.ingresoInformeRESA" Theme="admin_style" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnableScriptGlobalization="True"></asp:ToolkitScriptManager>
   


   
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

  

    <fieldset>
   

        <legend><asp:Label ID="Titulo" runat="server"></asp:Label></legend>
        <br />


        
        <asp:HiddenField ID="IdInforme"   runat="server" />
        
        <asp:UpdatePanel ID="UpdatePanelFlujoDocumental" UpdateMode="Conditional" runat="server">
            <ContentTemplate>    
                <asp:Panel ID="PanelFlujoDocumental"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px>
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="FlujoDocumentalLiteral" runat="server" Text="<%$Resources:spanish.language,flujoDocumental%>" /></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="FlujoDocumental" AutoPostBack="true" runat="server"></asp:DropDownList> *</td>
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
                        <td class="col3"><asp:DropDownList ID="TipoEntrada" AutoPostBack="true" runat="server"></asp:DropDownList> *</td>
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

            
        <asp:UpdatePanel ID="UpdatePanelOrigen" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelOrigen"  Visible="true" runat="server">
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

        
        <asp:UpdatePanel ID="UpdatePanelSubRequerimiento" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelSubRequerimiento"  Visible="true" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Materia</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="Materia" AutoPostBack="true" runat="server"></asp:DropDownList> *</td>
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
                        <td class="col3"><asp:TextBox ID="Numero" autocomplete="tel-extension" runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="8" Columns="10"></asp:TextBox> * </td>
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


        <asp:UpdatePanel ID="UpdatePanelArchivo" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelArchivo"  Visible="true" runat="server">

                    <asp:HiddenField ID="idArchivo"      runat="server" />

                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="ArchivoAdjuntoLiteral" runat="server" Text="<%$Resources:spanish.language,archivoAdjunto%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:FileUpload ID="ArchivoAdjunto" MaxLength="40" Width="200px" runat="server"></asp:FileUpload>&nbsp;<asp:Label ID="RequeridoArchivoAdjunto" runat="server"></asp:Label><asp:RegularExpressionValidator ID="REGEXFileUploadLogo" runat="server" ErrorMessage="Formato Archivo Incorrecto" ControlToValidate="ArchivoAdjunto" ValidationExpression= "(.*).(.doc|.DOC|.pdf|.PDF|.docx|.DOCX|.xls|.xlsx|.XLS|.XLSX|.dwg|.DWG|.dxf|.DXF|.jpg|.JPG|.jpeg|.JPEG)$" /> <asp:Label ID="NombreArchivo" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1"><span class="item"></td>
                        <td class="col2"><span class="item"></td>
                        <td class="col3"><asp:ImageButton  ID="LimpiarArchivo" runat="server" AlternateText="Limpiar Archivo" Height="20px"  ImageUrl="~/App_Themes/admin_style/images/clean.png"  onclick="LimpiarArchivo_Click" ToolTip="Limpiar Archivo" />Borrar Archivo</td>
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

            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1"><span class="item">Observaciones</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3"><asp:TextBox  ID="Observaciones" TextMode="multiline" Columns="50" Rows="5" runat="server" AutoPostBack="false"></asp:TextBox></td>
            </tr>
            </table>
            </ContentTemplate>
        </asp:UpdatePanel>

    
    
    
    </fieldset> 
    
    <br />



     

    <asp:UpdatePanel ID="UpdatePanelFormularioUnidadEspacial" runat="server" UpdateMode="Conditional">
    <ContentTemplate>                        
    <asp:Panel ID="PanelFormularioUnidadEspacial" Visible="true" runat="server">


        <asp:UpdatePanel ID="UpdatePanelErroresUnidadEspacial" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   
                <asp:Panel ID="PanelErroresUnidadEspacial" CssClass="Content_msgGrilla" Visible="false" runat="server">
                    <div class="msgGrilla_div2">
                        <asp:Label ID="ErroresUnidadEspacial" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>



    <fieldset>
   

    <legend>Unidad Espaciales</legend><br />
   


           
    <asp:UpdatePanel ID="UpdatePanelCodigoCentro" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelCodigoCentro"  Visible="true" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Código Centro</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:TextBox ID="CodigoCentro" runat="server" onKeyUp="return onlyNumeric(this)"  MaxLength="8" AutoPostBack="true" OnTextChanged="Identificador_TextChanged"></asp:TextBox>&nbsp;<asp:Label ID="Label3" runat="server"></asp:Label></td>
                </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
                


  <asp:UpdatePanel ID="UpdatePanelDatosCentro" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelDatosCentro"  Visible="true" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Titulares</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:label id="titularesCAD" runat="server" /></td>
                </tr>
                <tr>
                    <td class="col1"><span class="item">Región</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:label id="regionCad" runat="server" /></td>
                </tr>
                <tr>
                    <td class="col1"><span class="item">Toponimio</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:label id="toponimioCad" runat="server" /></td>
                </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
                
                    
                    
    
    <table class="form" cellpadding="0px" cellspacing="0px">   
    <tr>
        <td class="col1"></td>
        <td class="col2"></td>
        <td class="col3">
            <asp:Button ID="Button1" runat="server" Text="<%$Resources:spanish.language,guardar%>"  CausesValidation="true" onclick="GridUnidadEspacial_Agregar" style="height: 26px" />
        </td>
    </tr>
    </table>


    <br /> 



    <asp:UpdatePanel ID="UpdatePanelErroresGrillaUnidadEspacial" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelErroresGrillaUnidadEspacial" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="ErroresGrillaUnidadEspacial" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanelUnidadEspacial" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelUnidadEspacial"  Visible="true" runat="server">
                
                <asp:GridView 
                ID="GridUnidadEspacial" 
                runat="server" 
                AutoGenerateColumns="False" 
                CellPadding="4" 
                ForeColor="#333333"
                TabIndex="1"
                GridLines="None" 
                CssClass="mGrid"
                OnRowDataBound="GridUnidadEspacial_RowDataBound"
                PagerStyle-CssClass="pgr"
                OnRowCommand="GridUnidadEspacial_RowCommand">
                        
                <Columns>

        
                    <asp:TemplateField HeaderText="Código de Centro">   
                        <ItemTemplate>
                                <asp:HiddenField ID="gAccion"       runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                <asp:HiddenField ID="gIdVigencia"   runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.estadoAsoc.id") %>' />
                                <asp:HiddenField ID="gIdReferencia" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.idSolConcesion") %>' />
                                <%# DataBinder.Eval(Container, "DataItem.codigoCentro")%>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    
                    <asp:BoundField HeaderText="Titulares"                  DataField="titulares"                  ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Comunas"                    DataField="comunas"                        ItemStyle-HorizontalAlign="Center" />

                    <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="200px">
                        <ItemTemplate>

                            <asp:ImageButton ID="gNoVigente" Visible="false" runat="server" CausesValidation="false" CommandName="NoVigente" CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                    ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Pasar a No Vigente" ToolTip="Pasar a No Vigente" />

                            <asp:ImageButton ID="gVigente" Visible="false" runat="server" CausesValidation="false" CommandName="Vigente" CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                    ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Pasar a Vigente" ToolTip="Pasar a Vigente" />

                            <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
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



    <asp:UpdatePanel ID="UpdatePanelErroresInferior" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelErroresInferior" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="ErroresInferior" runat="server"></asp:Label>
                </div>
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
                <asp:Button ID="Limpiar" runat="server" Text="<%$Resources:spanish.language,limpiar%>"  CausesValidation="false" onclick="Limpiar_Click" />
                <asp:Button ID="Guardar" runat="server" Text="<%$Resources:spanish.language,guardar%>"  CausesValidation="true"  onclick="Guardar_Click" style="height: 26px" /></td>
        </tr>
        </table>
                         

    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>


        
    <!-- JAVASCRIPT !-->
    <script type="text/javascript">
        invoca_calendarios("ingresoInformeRESA");
    </script>


 
    
</asp:Content>


