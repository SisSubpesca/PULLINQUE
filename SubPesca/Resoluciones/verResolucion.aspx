<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="verResolucion.aspx.cs"
Inherits="SubPesca.Resoluciones.verResolucion" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
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
        
        <asp:HiddenField ID="idResolucion"   runat="server" />
        


        <asp:UpdatePanel ID="UpdatePanelTipoDocumento" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelTipoDocumento"  Visible="true" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="TipoDocumentoLiteral" runat="server" Text="<%$Resources:spanish.language,tipoDocumento%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="TipoDocumento" AutoPostBack="true" runat="server" OnSelectedIndexChanged="TipoDocumento_OnSelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanelTipoIngresoResolucion" UpdateMode="Conditional" runat="server">
            <ContentTemplate>              
                <asp:Panel ID="PanelTipoIngresoResolucion" Visible="true" runat="server">

                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Tipo Ingreso</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="TipoIngresoResolucion" AutoPostBack="true" runat="server"></asp:DropDownList></td>
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
                        <td class="col1"><span class="item"><asp:Literal ID="Literal3" runat="server" Text="Tipo de Relación del Documento"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="TipoRelacionDocumento" AutoPostBack="true" runat="server" OnSelectedIndexChanged="TipoRelacionDocumento_OnSelectedIndexChanged"></asp:DropDownList></td>
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
                        <td class="col3"><asp:DropDownList ID="Origen" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Origen_OnSelectedIndexChanged"></asp:DropDownList> *</td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelMateria" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelMateria"  Visible="true" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Materia</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="Materia" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Materia_OnSelectedIndexChanged"></asp:DropDownList> *</td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
                                                                       
                                    
        <asp:UpdatePanel ID="UpdatePanelResultado" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelResultado"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="ResultadoLiteral" runat="server" Text="<%$Resources:spanish.language,resultado%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td><td class="col3"><asp:DropDownList ID="Resultado" runat="server" AutoPostBack="false"></asp:DropDownList> *</td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        

        <asp:UpdatePanel ID="UpdatePanelDocumentoPrincipal"  UpdateMode="Conditional" runat="server">
            <ContentTemplate>       
                <asp:Panel ID="PanelDocumentoPrincipal"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="Literal1" runat="server" Text="Documento Principal"/></span></td>
                        <td class="col2"><span class="item">:</span></td><td class="col3"><asp:DropDownList ID="DocumentoPrincipal" runat="server" AutoPostBack="false"></asp:DropDownList> *</td>
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
                        <td class="col3"><asp:TextBox ID="Numero" autocomplete="tel-extension" runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="8"></asp:TextBox>&nbsp;<asp:Label ID="RequeridoNumero" runat="server"></asp:Label> * </td>
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
                        <td class="col3"><asp:TextBox ID="NumeroCI"  autocomplete="tel-extension" runat="server"  onKeyUp="return onlyNumeric(this)"  MaxLength="8"  AutoPostBack="true"></asp:TextBox>&nbsp;<asp:Label ID="RequeridoNumeroCI" runat="server"></asp:Label>&nbsp;<asp:Literal runat="server" ID="NumeroCIMensaje" Text=""></asp:Literal></td>
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
                                    <asp:TextBox ID="FechaCI" Columns="8" Width="80px" runat="server" ReadOnly="true" d></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="FechaCI"
                                        Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                    </asp:MaskedEditExtender>

                                </div>

                              

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


        
    
    </fieldset> 
    
    <br />


    <fieldset>
   

    <legend>Otros Antecedentes</legend><br />
        

    <asp:UpdatePanel ID="UpdatePanelDiarioOficinal" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="Panel3"  Visible="true" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Nº Diario Oficial</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:TextBox ID="NroDiarioOficial" runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="8"></asp:TextBox>&nbsp;<asp:Label ID="RequeridoNroDiarioOficial" runat="server"></asp:Label></td>
                </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
                    

    <asp:UpdatePanel ID="UpdatePanelFechaDiarioOficial" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelFechaDiarioOficial"  Visible="true" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Fecha de Diario Oficial</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">

                        <div class="calendario">
                            
                            <div class="calendario_textbox">               
                                <asp:TextBox ID="FechaDiarioOficial" Columns="8" Width="80px" runat="server"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="FechaDiarioOficial"
                                Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                </asp:MaskedEditExtender>
                            </div>

                            <asp:Panel ID="PanelCalendarioFechaDiarioOficial" Visible="true" runat="server">
                                <div class="calendario_icono">
                                    <asp:Image src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaDiarioOficial" alt="Calendario" runat="server" style="vertical-align: middle" />&nbsp;<asp:Label ID="Label1" runat="server"></asp:Label>
                                </div>
                            </asp:Panel>

                             <asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator2" 
                                runat="server"
                                ControlToValidate="FechaDiarioOficial"
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
                            
                            
    <asp:UpdatePanel ID="UpdatePanelVigencia" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelVigencia"  Visible="true" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Vigencia</span></td><td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:DropDownList ID="Vigencia" AutoPostBack="true" runat="server"></asp:DropDownList></td>
                </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanelFechaInicioPlazo" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelFechaInicioPlazo"  Visible="true" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Fecha Inicio Plazo</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                        <div class="calendario">
                            <div class="calendario_textbox">               
                                <asp:TextBox ID="FechaInicioPlazo" Columns="8" Width="80px" runat="server"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="FechaInicioPlazo"
                                    Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                    CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                </asp:MaskedEditExtender>

                            </div>

                            <asp:Panel ID="PanelCalendarioFechaInicio" Visible="true" runat="server">
                                <div class="calendario_icono">
                                    <asp:Image src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaInicioPlazo" alt="Calendario" runat="server" style="vertical-align: middle" />
                                </div>
                            </asp:Panel>

                             <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator3" 
                                    runat="server"
                                    ControlToValidate="FechaInicioPlazo"
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
                            

    <asp:UpdatePanel ID="UpdatePanelFechaVencimiento" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelFechaVencimiento"  Visible="true" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Fecha Vencimiento</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">

                        <div class="calendario">
                            <div class="calendario_textbox">               
                                <asp:TextBox ID="FechaVencimiento" Columns="8" Width="80px" runat="server"></asp:TextBox>
                                
                                <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="FechaVencimiento"
                                    Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                    CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                </asp:MaskedEditExtender>

                            </div>

                            <asp:Panel ID="PanelCalendarioFechaVencimiento" Visible="true" runat="server">
                                <div class="calendario_icono">
                                    <asp:Image src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaVencimiento" alt="Calendario" runat="server" style="vertical-align: middle" />
                                </div>
                            </asp:Panel>

                            <asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator4" 
                                runat="server"
                                ControlToValidate="FechaVencimiento"
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
    




    <asp:UpdatePanel ID="UpdatePanelNuevaFecha" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelNuevaFecha"  Visible="false" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item"><asp:Literal ID="NuevaFechaLiteral" runat="server" Text="<%$Resources:spanish.language,nuevafecha%>"/></span></td>
                    <td class="col2"><span class="item">:</span></td><td class="col3">

                        <div class="calendario">
                            <div class="calendario_textbox">               
                                <asp:TextBox ID="NuevaFecha" Columns="8" Width="80px" runat="server"></asp:TextBox>
                                
                                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="NuevaFecha"
                                    Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                    CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                </asp:MaskedEditExtender>

                            </div>

                            <asp:Panel ID="PanelCalendarioNuevaFecha" Visible="true" runat="server">
                                <div class="calendario_icono">
                                    <asp:Image src="../../App_Themes/admin_style/images/calendar.png" id="imgNuevaFecha" alt="Calendario" runat="server" style="vertical-align: middle" />
                                </div>
                            </asp:Panel>

                              <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator5" 
                                    runat="server"
                                    ControlToValidate="NuevaFecha"
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


    </fieldset>  



     <asp:UpdatePanel ID="UpdatePanelResolucionPrincipal" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <asp:Panel ID="PanelResolucionPrincipal" runat="server" Visible="false">
    
        <fieldset>
   

        <legend>Resolucion/Decreto Principal</legend><br />


   
        <asp:UpdatePanel ID="UpdatePanelTipoDocumentoPrincipal" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelTipoDocumentoPrincipal"  Visible="true" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="Literal2" runat="server" Text="<%$Resources:spanish.language,tipoDocumento%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="TipoDocumentoPrincipal" AutoPostBack="true" runat="server" OnSelectedIndexChanged="TipoDocumentoPrincipal_OnSelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>



        <asp:UpdatePanel ID="UpdatePanelOrigenPrincipal" UpdateMode="Conditional" runat="server">
            <ContentTemplate>              
                <asp:Panel ID="PanelOrigenPrincipal" Visible="true" runat="server">

                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Origen</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="OrigenPrincipal" AutoPostBack="true" runat="server"></asp:DropDownList></td>
                    </tr>
                    </table>

                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelNumeroPrincipal" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelNumeroPrincipal"  Visible="true" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Número</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="NumeroPrincipal" AutoComplete="nope"  runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="9"></asp:TextBox>&nbsp;<asp:Label ID="Label7" runat="server"></asp:Label></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
                
                    
        <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="Panel7"  Visible="true" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Fecha</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">

                            <div class="calendario">
                                    <div class="calendario_textbox">               
                                    <asp:TextBox ID="FechaPrincipal" Columns="8" Width="80px" runat="server"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender8" runat="server" TargetControlID="FechaPrincipal"
                                        Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                    </asp:MaskedEditExtender>

                                </div>
                               

                                <asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator7" 
                                runat="server"
                                ControlToValidate="FechaPrincipal"
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
                            
        </fieldset> 



        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanelDocumentosComplementan" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <asp:Panel ID="PanelDocumentosComplementan" runat="server" Visible="false">
    
   
        <fieldset>
            <legend>Resolucion/Decreto que complementan este documento</legend>
            <asp:UpdatePanel ID="upd1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   

                <asp:Panel ID="Panel5" CssClass="Content_msgGrilla" Visible="false" runat="server">
                    <div class="msgGrilla_div1">
                        <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
                    </div>
                    <div class="msgGrilla_div2">
                        <asp:Label ID="Label10" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

                <asp:GridView 
                    ID="GridViewComplementan" 
                    runat="server" 
                    AutoGenerateColumns="true" 
                    CellPadding="4" 
                    ForeColor="#333333" 
                    GridLines="None"
                    AllowPaging="false" 
                    AllowSorting="false" 
                    CssClass="mGrid"
                    PagerStyle-CssClass="pgr">
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#5794EF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            
            </ContentTemplate> 
            </asp:UpdatePanel>
        </fieldset>



        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>


    <br />


    <asp:UpdatePanel ID="UpdatePanelGridViewReferencian" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <asp:Panel ID="PanelGridViewReferencian" runat="server" Visible="false">
    
   
        <fieldset>
            <legend>Resoluciones/Decretos que referencian este documento</legend>
            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   


                  
                <asp:GridView 
                ID="GridViewReferencian" 
                runat="server" 
                AutoGenerateColumns="False" 
                CellPadding="4" 
                ForeColor="#333333"
                TabIndex="1"
                GridLines="None" 
                CssClass="mGrid"
                PagerStyle-CssClass="pgr"
                >
                        
                <Columns>
    
                    <asp:BoundField HeaderText="Tipo Documento"          DataField="tipoDocumento.descripcion"  ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Tipo Ingreso"            DataField="tipoIngreso.descripcion"  ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />

                    <asp:BoundField HeaderText="Nº"            DataField="numero"  ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Fecha"         DataField="fecha"  ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />

              
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
        </fieldset>


        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>


    </br>


    <fieldset>

        <table class="form" cellpadding="0px" cellspacing="0px" width="100%">
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td align="center"><asp:RadioButton ID="SinReferencia"      runat="server" AutoPostBack="true" GroupName="RadReferenciaUnidadEspecial" Text="Sin Referencias"   OnCheckedChanged="RadGrupoEspecieChecked" Checked="true" /></td>
            <td align="center"><asp:RadioButton ID="ConReferencia"      runat="server" AutoPostBack="true" GroupName="RadReferenciaUnidadEspecial" Text="Con Referencias"   OnCheckedChanged="RadGrupoEspecieChecked"/></td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>

        </table>

    </fieldset>

    <br />

    

    <asp:UpdatePanel ID="UpdatePanelFormularioUnidadEspacial" runat="server" UpdateMode="Conditional">
    <ContentTemplate>                        
    <asp:Panel ID="PanelFormularioUnidadEspacial" Visible="false" runat="server">


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
   

    <legend>Vinculación Unidad Espacial</legend><br />
   

    <asp:UpdatePanel ID="UpdatePanelUE" UpdateMode="Conditional" runat="server">
        <ContentTemplate>              


    <asp:UpdatePanel ID="UpdatePanelSolicitudUE" UpdateMode="Conditional" runat="server">
        <ContentTemplate>              
            <asp:Panel ID="PanelSolicitudUE" Visible="true" runat="server">

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Tipo</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:DropDownList ID="Tipo" AutoPostBack="true" runat="server" onselectedindexchanged="Tipo_OnSelectedIndexChanged"></asp:DropDownList></td>
                </tr>
                </table>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

           
    <asp:UpdatePanel ID="UpdatePanelTipoUnidadEspacial" UpdateMode="Conditional" runat="server">
        <ContentTemplate>              
            <asp:Panel ID="PanelTipoUnidadEspacial" Visible="false" runat="server">

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Tipo Unidad Espacial</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:DropDownList ID="TipoUnidadEspacial" AutoPostBack="true" runat="server" onselectedindexchanged="TipoUnidadEspacial_SelectedIndexChanged"></asp:DropDownList></td>
                </tr>
                </table>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanelTipoSolicitud" UpdateMode="Conditional" runat="server">
        <ContentTemplate>              
            <asp:Panel ID="PanelTipoSolicitud" Visible="false" runat="server">

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Tipo Solicitud</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:DropDownList ID="TipoSolicitud" AutoPostBack="true" runat="server" onselectedindexchanged="TipoSolicitud_SelectedIndexChanged"></asp:DropDownList></td>
                </tr>
                </table>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanelCodigoCentro" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelCodigoCentro"  Visible="false" runat="server">
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
                

    <asp:UpdatePanel ID="UpdatePanelNumeroPert" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelNumeroPert"  Visible="false" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Pert</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:TextBox ID="NumeroPert" runat="server" onKeyUp="return onlyNumeric(this)" MaxLength="10" AutoPostBack="true" OnTextChanged="Identificador_TextChanged"></asp:TextBox>&nbsp;<asp:Label ID="Label4" runat="server"></asp:Label></td>
                </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanelNumeroSector" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelNumeroSector"  Visible="false" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Número Sector</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:TextBox ID="NumeroSector" runat="server" onKeyUp="return onlyNumeric(this)" MaxLength="8" AutoPostBack="true" OnTextChanged="Identificador_TextChanged"></asp:TextBox>&nbsp;<asp:Label ID="Label6" runat="server"></asp:Label></td>
                </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
                
                    
    <asp:UpdatePanel ID="UpdatePanelNumeroIdentificador" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelNumeroIdentificador"  Visible="false" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Número Identificador</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:TextBox ID="NumeroIdentificador" runat="server" onKeyUp="return onlyNumeric(this)" MaxLength="10" AutoPostBack="true" OnTextChanged="Identificador_TextChanged"></asp:TextBox>&nbsp;<asp:Label ID="Label5" runat="server"></asp:Label></td>
                </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
                

  <asp:UpdatePanel ID="UpdatePanelDatosCentro" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelDatosCentro"  Visible="false" runat="server">
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
                OnRowCommand="GridUnidadEspacial_RowCommand"
                OnRowCreated="GridUnidadEspacial_RowCreated">
                        
                <Columns>

        
                    <asp:TemplateField HeaderText="Tipo">
                        <ItemTemplate>
                                <asp:HiddenField ID="gAccion"       runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                <asp:HiddenField ID="gIdVigencia"   runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.estadoVigencia.id") %>' />
                                <asp:HiddenField ID="gIdReferencia" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.idReferencia") %>' />
                                <%# DataBinder.Eval(Container, "DataItem.tipo.descripcion")%>
                        </ItemTemplate>
                    </asp:TemplateField> 
                 

                    <asp:TemplateField HeaderText="Tipo Solicitud / Tipo de Unidad Espacial">
                        <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.tipoUnidadEspacial.descripcion")%> <%# DataBinder.Eval(Container, "DataItem.tipoSolicitud.descripcion")%>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    
                    <asp:BoundField HeaderText="Código de Centro"           DataField="codigoCentro"                        ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Pert"                       DataField="numeroPert"                          ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Número Identificador"       DataField="numeroIdentificador"                 ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField HeaderText="Sector"                     DataField="DescripcionNumSector"                ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                    
                    <asp:TemplateField HeaderText="Código Centro Relocalizado" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                             <%# DataBinder.Eval(Container, "DataItem.codigoCentroRegularizado")%>
                        </ItemTemplate>
                    </asp:TemplateField> 

                     <asp:TemplateField HeaderText="Nueva Fecha Vencimiento" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                             <%# DataBinder.Eval(Container, "DataItem.fechaNuevoVencimientoString")%>
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
    
    <br />

    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanelFormularioReferencia" runat="server" UpdateMode="Conditional">
    <ContentTemplate>                        
    <asp:Panel ID="PanelFormularioReferencia" Visible="false" runat="server">


        
    <asp:UpdatePanel ID="UpdatePanelErroresReferenciaDocumentos" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelErroresReferenciaDocumentos" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="ErroresReferenciaDocumentos" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    

    <fieldset>
   

    <legend>Referencia a Documentos</legend><br />
   
    <asp:UpdatePanel ID="UpdatePanelDocumentos" UpdateMode="Conditional" runat="server">
    <ContentTemplate>              

    
           
    <asp:UpdatePanel ID="UpdatePanelOrigenReferencia" UpdateMode="Conditional" runat="server">
        <ContentTemplate>              
            <asp:Panel ID="PanelOrigenReferencia" Visible="true" runat="server">

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Origen</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:DropDownList ID="OrigenReferencia" AutoPostBack="true" runat="server"></asp:DropDownList></td>
                </tr>
                </table>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanelNumeroReferencia" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelNumeroReferencia"  Visible="true" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Número</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:TextBox ID="NumeroReferencia" runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="8"></asp:TextBox>&nbsp;<asp:Label ID="RequeridoNumeroReferencia" runat="server"></asp:Label></td>
                </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
                
                    
    <asp:UpdatePanel ID="UpdatePanelFechaReferencia" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelFechaReferencia"  Visible="true" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Fecha</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">

                        <div class="calendario">
                                <div class="calendario_textbox">               
                                <asp:TextBox ID="FechaReferencia" Columns="8" Width="80px" runat="server"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender7" runat="server" TargetControlID="FechaReferencia"
                                    Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                    CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                </asp:MaskedEditExtender>

                            </div>
                            <asp:Panel ID="PanelCalendarioFechaReferencia" Visible="true" runat="server">
                                <div class="calendario_icono">
                                    <asp:Image src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaReferencia" alt="Calendario" runat="server" style="vertical-align: middle" />&nbsp;<asp:Label ID="Label2" runat="server"></asp:Label>
                                </div>
                            </asp:Panel>

                            <asp:RegularExpressionValidator 
                            ID="RegularExpressionValidator6" 
                            runat="server"
                            ControlToValidate="FechaReferencia"
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
     
    </ContentTemplate>
    </asp:UpdatePanel>                       
    
    <table class="form" cellpadding="0px" cellspacing="0px">   
    <tr>
        <td class="col1"></td>
        <td class="col2"></td>
        <td class="col3">
            <asp:Button ID="GuardarReferencia" runat="server" Text="<%$Resources:spanish.language,guardar%>"  CausesValidation="true" onclick="GridReferencia_Agregar" style="height: 26px" />
        </td>
    </tr>
    </table>


    <br /> 


    <asp:UpdatePanel ID="UpdatePanelGridReferencia" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelGridReferencia"  Visible="true" runat="server">
                
                <asp:GridView 
                ID="GridReferencia" 
                runat="server" 
                AutoGenerateColumns="False" 
                CellPadding="4" 
                ForeColor="#333333"
                TabIndex="1"
                GridLines="None" 
                CssClass="mGrid"
                OnRowDataBound="GridReferencia_RowDataBound"
                PagerStyle-CssClass="pgr"
                OnRowCommand="GridReferencia_RowCommand"
                OnRowCreated="GridReferencia_RowCreated">
                        
                <Columns>

                    <asp:TemplateField HeaderText="Origen" ItemStyle-Width="50px">
                        <ItemTemplate>
                                <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                <%# DataBinder.Eval(Container, "DataItem.origenReferencia.descripcion")%>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    
                    <asp:BoundField HeaderText="Tipo Documento"          DataField="tipoDocResol.descripcion"  ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Tipo Ingreso"            DataField="tipoIngresoResol.descripcion"  ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />

                    <asp:BoundField HeaderText="Nº"            DataField="numeroReferencia"  ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Fecha"         DataField="fechaReferencia"  ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                    


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
    
    <br />

    
    <fieldset>



    <asp:UpdatePanel ID="UpdatePanelErroresUbicacion" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelErroresUbicacion" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="ErroresUbicacion" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>




   

    <legend>Ubicación</legend><br />
   
   <asp:UpdatePanel ID="UpdatePanelUbicacion" UpdateMode="Conditional" runat="server">
   <ContentTemplate>
   
   
           
    <asp:UpdatePanel ID="UpdatePanelRegion" UpdateMode="Conditional" runat="server">
        <ContentTemplate>              
            <asp:Panel ID="PanelRegion" Visible="true" runat="server">

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Región</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:DropDownList ID="Region" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Region_OnSelectedIndexChanged"></asp:DropDownList></td>
                </tr>
                </table>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanelComuna" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelComuna"  Visible="true" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Comuna</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:DropDownList ID="Comuna" AutoPostBack="true" runat="server"></asp:DropDownList></td>
                </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
                
                    
    <asp:UpdatePanel ID="UpdatePanelSector" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelSector"  Visible="true" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Sector</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:TextBox ID="Sector" AutoPostBack="true" runat="server" Columns="70" MaxLength="200"></asp:TextBox></td>
                </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    </ContentTemplate>
    </asp:UpdatePanel>

    <table class="form" cellpadding="0px" cellspacing="0px">   
    <tr>
        <td class="col1"></td>
        <td class="col2"></td>
        <td class="col3">
            <asp:Button ID="Button2" runat="server" Text="<%$Resources:spanish.language,guardar%>"  CausesValidation="true" onclick="GridUbicacion_Agregar" style="height: 26px" />
        </td>
    </tr>
    </table>


    <br /> 


    <asp:UpdatePanel ID="UpdatePanelGridUbicacion" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelGridUbicacion"  Visible="true" runat="server">
                
                <asp:GridView 
                ID="GridUbicacion" 
                runat="server" 
                AutoGenerateColumns="False" 
                CellPadding="4" 
                ForeColor="#333333"
                TabIndex="1"
                GridLines="None" 
                CssClass="mGrid"
                OnRowDataBound="GridUbicacion_RowDataBound"
                PagerStyle-CssClass="pgr"
                OnRowCommand="GridUbicacion_RowCommand"
                OnRowCreated="GridUbicacion_RowCreated">
                        
                <Columns>

                    <asp:TemplateField HeaderText="Region" ItemStyle-Width="50px">
                        <ItemTemplate>
                                <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                <%# DataBinder.Eval(Container, "DataItem.region.descripcion")%>
                        </ItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Comuna" ItemStyle-Width="50px">
                        <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.comuna.descripcion")%>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    
                    <asp:BoundField HeaderText="Sector"     DataField="sector"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />

                   

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
    
    <br />

  
    <fieldset>


    
    <asp:UpdatePanel ID="UpdatePanelErroresEspecie" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelErroresEspecie" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="ErroresEspecie" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>




   

    <legend>Especie de Documento</legend><br />
   

           
    <asp:UpdatePanel ID="UpdatePanelEspecie" UpdateMode="Conditional" runat="server">
        <ContentTemplate>              
            <asp:Panel ID="PanelEspecie" Visible="true" runat="server">

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Especie</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:DropDownList ID="Especie" AutoPostBack="true" runat="server"></asp:DropDownList></td>
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
            <asp:Button ID="Button3" runat="server" Text="<%$Resources:spanish.language,guardar%>"  CausesValidation="true" onclick="GridEspecie_Agregar" style="height: 26px" />
        </td>
    </tr>
    </table>


    <br /> 


    <asp:UpdatePanel ID="UpdatePanelGridEspecie" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelGridEspecie"  Visible="true" runat="server">
                
                <asp:GridView 
                ID="GridEspecie" 
                runat="server" 
                AutoGenerateColumns="False" 
                CellPadding="4" 
                ForeColor="#333333"
                TabIndex="1"
                GridLines="None" 
                CssClass="mGrid"
                OnRowDataBound="GridEspecie_RowDataBound"
                PagerStyle-CssClass="pgr"
                OnRowCommand="GridEspecie_RowCommand"
                OnRowCreated="GridEspecie_RowCreated">
                        
                <Columns>

                    <asp:TemplateField HeaderText="Especie" ItemStyle-Width="50px">
                        <ItemTemplate>
                                <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                <%# DataBinder.Eval(Container, "DataItem.especie.descripcion")%>
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
    

    <br />


    

    <asp:UpdatePanel ID="UpdatePanelErroresTitular" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelErroresTitular" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="ErroresTitular" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    
    <fieldset>
   

    <legend>Titular de Documento</legend><br />
   

           
    <asp:UpdatePanel ID="UpdatePanelTitular" UpdateMode="Conditional" runat="server">
        <ContentTemplate>              

            <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla" runat="server"></asp:Label>
                </div>
            </asp:Panel>

            
          


            
            <asp:Panel ID="PanelTitular" Visible="true" runat="server">

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Rut Persona</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                        
                    <asp:TextBox ID="RutPersona" MaxLength="10" Width="100px" runat="server" OnTextChanged="CambiaTipoPersona_Click" ></asp:TextBox>
                    <asp:Button ID="BuscarSolicitante" runat="server" Text="Buscar" onclick="BuscarSolicitante_Click" CausesValidation="true" ValidationGroup="grupo1" />
                        12345678-9

                    <asp:CustomValidator ID="ccNumCustVal" ControlToValidate="RutPersona" ErrorMessage="Rut Persona sin formato válido" ForeColor="Red" ClientValidationFunction="validaRUT" Display="Static" Font-Size="10" runat="server" ValidationGroup="grupo1"></asp:CustomValidator>

                    </td>
                </tr>
                </table>

            </asp:Panel>



            <asp:Panel ID="PanelDatosPersonaNatural" Visible="false" runat="server">

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1" nowrap><span class="item">Nombre del Solicitante</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                        <asp:HiddenField ID="TipoPersonaNatural" runat="server" Value="12"></asp:HiddenField>
                        <asp:TextBox ID="NombreSolicitanteNatural" MaxLength="40" Width="200px" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="col1"><span class="item">Género</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:TextBox ID="Genero" MaxLength="15" Width="80px" Readonly="true" runat="server" ></asp:TextBox></td>
                </tr>
                </table>

            </asp:Panel>
                
            <asp:Panel ID="PanelPersonaJuridica"  Visible="false" runat="server">

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Tipo</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                        <asp:HiddenField ID="TipoPersonaJuridica" runat="server" Value="13"></asp:HiddenField>
                        <asp:TextBox ID="SubtipoPersonaJuridica" MaxLength="40" Width="200px" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="col1" nowrap><span class="item">Nombre del Solicitante</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:TextBox ID="NombreSolicitanteJuridico" MaxLength="40" Width="200px" ReadOnly="true" runat="server"></asp:TextBox>&nbsp;</td>
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
            <asp:Button ID="Button4" runat="server" Text="<%$Resources:spanish.language,guardar%>"  CausesValidation="true" onclick="GridTitular_Agregar" style="height: 26px" />
        </td>
    </tr>
    </table>


    <br /> 


    <asp:UpdatePanel ID="UpdatePanelGridTitular" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="GridPanelTitular"  Visible="true" runat="server">
                
                <asp:GridView 
                ID="GridTitular" 
                runat="server" 
                AutoGenerateColumns="False" 
                CellPadding="4" 
                ForeColor="#333333"
                TabIndex="1"
                GridLines="None" 
                CssClass="mGrid"
                OnRowDataBound="GridTitular_RowDataBound"
                PagerStyle-CssClass="pgr"
                OnRowCommand="GridTitular_RowCommand"
                OnRowCreated="GridTitular_RowCreated">
                        
                <Columns>

                    <asp:TemplateField HeaderText="Rut" ItemStyle-Width="50px">
                        <ItemTemplate>
                                <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                    <%# DataBinder.Eval(Container, "DataItem.titular.rut")%> - <%# DataBinder.Eval(Container, "DataItem.titular.dv")%>
                        </ItemTemplate>
                    </asp:TemplateField> 

                    
                    <asp:TemplateField HeaderText="Nombre" ItemStyle-Width="50px">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container, "DataItem.titular.nombreSolicitante")%> 
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
    



    <br />


    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>


    <fieldset>

        <legend>Observaciones</legend>
  
        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Observaciones:</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:TextBox  ID="Observaciones" TextMode="multiline" Columns="50" Rows="5" runat="server" AutoPostBack="false"></asp:TextBox></td>
        </tr>
        </table>
    
                        
    </fieldset>           


    <br />        



    <fieldset>

     


        <legend>Archivo</legend><br />
   

        <asp:UpdatePanel ID="UpdatePanelArchivo" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelArchivo"  Visible="true" runat="server">

                    <asp:HiddenField ID="idArchivo"      runat="server" />

                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="ArchivoAdjuntoLiteral" runat="server" Text="<%$Resources:spanish.language,archivoAdjunto%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:Label ID="NombreArchivo" runat="server"></asp:Label></td>
                    </tr>
                    <tr runat="server" id="trDescargarArchivo" visible="false">
                        <td class="col1"></td>
                        <td class="col2"></td>
                        <td class="col3"><asp:ImageButton ID="gDescargar" runat="server" CausesValidation="false" ImageUrl="~/App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" onclick="DescargarArchivo_Click"  /></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="Guardar"/>
                <asp:PostBackTrigger ControlID="gDescargar" />
            </Triggers>
        </asp:UpdatePanel>
        
    
    
    </fieldset>


    <br />

    
    <table class="form" cellpadding="0px" cellspacing="0px">   
    <tr>
        <td class="col1"></td>
        <td class="col2"></td>
        <td class="col3">
            <asp:Button ID="Guardar" runat="server" Text="Guardar"  CausesValidation="true" ValidationGroup="grupo1" OnClick="Guardar_Click" Visible="false"  />
        </td>
    </tr>
    </table>



    <asp:UpdatePanel ID="UpdatePanelErroresInferior" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelErroresInferior" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="ErroresInferior" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    


    
    <!-- JAVASCRIPT !-->
    <script type="text/javascript">
        invoca_calendarios("ingresarResoluciones");
    </script>



</asp:Content>