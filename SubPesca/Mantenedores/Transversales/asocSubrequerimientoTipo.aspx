<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="asocSubrequerimientoTipo.aspx.cs" Inherits="SubPesca.Mantenedores.Transversales.asocDocumentoTipo" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>


    <asp:Content ID="FormularioAdministracionTitulares" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
   
    <asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
       <ContentTemplate>    
           <asp:panel ID="Panel1" runat="server">
                <asp:ValidationSummary ID="ValidationSummaryErrores" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList"  />
            </asp:panel>
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

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Mantenedor de Subrequerimiento - Tipo Documento</span>
            </td>
            <td align="right" valign="middle">
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />



    <fieldset>

    <legend>Asociación Subrequerimiento - Tipo Documento</legend>

    <br />
        
        
    <table class="form" cellpadding="0px" cellspacing="0px" style="display:none;">
    <tr>
        <td>&nbsp;IdValidacionDocumentacion: <asp:TextBox ID="IdValidacionDocumentacion" runat="server"></asp:TextBox></td>
    </tr>
    </table>
        
        
    <asp:UpdatePanel ID="UpdatePanelFormularioIngreso" UpdateMode="Conditional" runat="server">
    <ContentTemplate>   
                   
    <asp:Panel ID="PanelFormularioIngreso" Visible="false" runat="server">

            
        <asp:UpdatePanel ID="UpdatePanelErroresSuperior" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   
                <asp:Panel ID="PanelErroresSuperior" CssClass="Content_msgGrilla" Visible="false" runat="server">
                    <div class="msgGrilla_div2">
                        <asp:Label ID="ErroresSuperior" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        
        <asp:UpdatePanel ID="UpdatePanelFlujoDocumental" UpdateMode="Conditional" runat="server">
            <ContentTemplate>              
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item"><asp:Literal ID="FlujoDocumentalLiteral" runat="server" Text="<%$Resources:spanish.language,flujoDocumental%>" /></span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                            <asp:DropDownList ID="FlujoDocumental" AutoPostBack="true" runat="server" OnSelectedIndexChanged="FlujoDocumental_change"></asp:DropDownList>
                           
                    </td>
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
                        <td class="col3">
                                <asp:DropDownList ID="TipoSalida" AutoPostBack="true" runat="server" OnSelectedIndexChanged="TipoSalida_change"></asp:DropDownList>
                               
                        </td>
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
                        <td class="col3">
                                <asp:DropDownList ID="TipoEntrada" AutoPostBack="true" runat="server" OnSelectedIndexChanged="TipoEntrada_change"></asp:DropDownList>
                               
                        </td>
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
                        <td class="col3"><asp:DropDownList ID="Origen" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Origen_Change"></asp:DropDownList></td>
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
                        <td class="col3"><asp:DropDownList ID="Destinatario" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Destinatario_Change"></asp:DropDownList></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanelAmbito" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelAmbito"  Visible="false" runat="server">
                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="col1"><span class="item">Ámbito</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3"><asp:DropDownList ID="Ambito" AutoPostBack="true" runat="server"  OnSelectedIndexChanged="Ambito_change"></asp:DropDownList></td>
                        </tr>
                        </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanelSeccion" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelSeccion"  Visible="false" runat="server">
                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="col1"><span class="item">Sección</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3"><asp:DropDownList ID="Seccion" AutoPostBack="true" runat="server" 
                                    onselectedindexchanged="Seccion_change"></asp:DropDownList></td>
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
                        <td class="col3"><asp:DropDownList ID="Tipo" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Tipo_change"></asp:DropDownList></td>
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
                        <td class="col3"><asp:DropDownList ID="TipoDocumento" AutoPostBack="true" 
                                runat="server" onselectedindexchanged="TipoDocumento_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanelAplica" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
        <asp:Panel ID="PanelAplica"  Visible="false" runat="server">

        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Aplica Número</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:DropDownList ID="AplicaNumero" runat="server">
                </asp:DropDownList>
            </td>  
            <td><span class="item">Aplica Fecha</span></td>  
            <td><span class="item">:</span></td>  
            <td>
                <asp:DropDownList ID="AplicaFecha" runat="server">
                </asp:DropDownList>
            </td>    
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica N° CI</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:DropDownList ID="AplicaNumeroCI" runat="server">
                </asp:DropDownList>
            </td>  
            <td><span class="item">Aplica Fecha CI</span></td>  
            <td><span class="item">:</span></td>  
            <td>
                <asp:DropDownList ID="AplicaFechaCI" runat="server">
                </asp:DropDownList>
            </td>      
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Archivo Adjunto</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:DropDownList ID="AplicaArchivoBinario" runat="server">
                </asp:DropDownList>
            </td>  
            <td></td>  
            <td></td>  
            <td></td>      
        </tr>
            <tr>
                <td class="col1">
                    <span class="item">Verifica Conforme</span></td>
                <td class="col2">
                    <span class="item">:</span></td>
                <td class="col3">
                    
                    <asp:DropDownList ID="VerificaConforme" runat="server"></asp:DropDownList>

                </td>
                <td>
                    <span class="item">Extensión Plazo</span></td>
                <td>
                    <span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="ExtensionPlazo" runat="server"></asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td class="col1">
                    <span class="item">Aplica Concesión</span></td>
                <td class="col2">
                    <span class="item">:</span></td>
                <td class="col3">
                    

                    <asp:DropDownList ID="AplicaConcesion" runat="server"></asp:DropDownList>

                </td>
                <td>
                    <span class="item">Aplica Modificación - Ampliación</span></td>
                <td>
                    <span class="item">:</span></td>
                <td>
                    
                    <asp:DropDownList ID="AplicaModificacionAmpliacion" runat="server"></asp:DropDownList>

                </td>
            </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Modificación - Reducción</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaModificacionReduccion" runat="server"></asp:DropDownList>

            </td>  
            <td><span class="item">Aplica Modificación - Especie PT</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="AplicaModificacionEspeciePT" runat="server"></asp:DropDownList>

            </td>     
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Modificación - Regularización</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaModificacionRegularizacion" runat="server"></asp:DropDownList>

            </td>  
            <td><span class="item">Aplica Relocalización</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="AplicaRelocalizacion" runat="server"></asp:DropDownList>
            
            </td>      
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Amerb</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaAmerb" runat="server"></asp:DropDownList>
            
            </td>  
            <td><span class="item">Aplica Faenamiento</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="AplicaFaenamiento" runat="server"></asp:DropDownList>
                        
            </td>     
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Acopio</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaAcopio" runat="server"></asp:DropDownList>

            </td>  
            <td><span class="item">Aplica Colectores</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="AplicaColectores" runat="server"></asp:DropDownList>
            
            </td>   
        </tr>
        <tr>
            <td class="col1"><span class="item">Verifica Ampliación de Plazo</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="VerificaAmpliacionPlazo" runat="server"></asp:DropDownList>

            </td>  
            <td><span class="item">Verifica Ampliación Extensión</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="VerficiaAmpliacionExtension" runat="server"></asp:DropDownList>
            
            </td>   
        </tr>
                <tr>
            <td class="col1"><span class="item">Aplica Experimental Amerb</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaExpAmerb" runat="server"></asp:DropDownList>
            
            </td>  
            <td><span class="item">Aplica Experimental Concesión</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="AplicaExpConcesion" runat="server"></asp:DropDownList>
            
            </td>   
        </tr>
         <tr>
            <td class="col1"><span class="item">Aplica ECMPO</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaECMPO" runat="server"></asp:DropDownList>
            
            </td>  
            <td><span class="item">Aplica Mod ECMPO - Ampliación</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="AplicaModECMPOAmpliacion" runat="server"></asp:DropDownList>
            
            </td>   
        </tr>
         <tr>
            <td class="col1"><span class="item">Aplica Mod ECMPO - Reducción</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaModECMPOReduccion" runat="server"></asp:DropDownList>
            
            </td>  
            <td><span class="item">Aplica Mod ECMPO - Especie</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="AplicaModECMPOEspecie" runat="server"></asp:DropDownList>
            
            </td>   
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Mod ECMPO - PT</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaModECMPO_PT" runat="server"></asp:DropDownList>
            
            </td>  
            <td><span class="item">Aplica Mod ECMPO - Regularización</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="AplicaModECMPORegularizacion" runat="server"></asp:DropDownList>
            
            </td>   
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Mod Acopio - Ampliación</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaModAcopioAmpliacion" runat="server"></asp:DropDownList>
            
            </td>  
            <td><span class="item">Aplica Mod Acopio - Reducción</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="AplicaModAcopioReduccion" runat="server"></asp:DropDownList>
            
            </td>   
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Mod Acopio - Especie</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaModAcopioEspecie" runat="server"></asp:DropDownList>
            
            </td>  
            <td><span class="item">Aplica Mod Acopio - PT</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="AplicaModAcopioPT" runat="server"></asp:DropDownList>
            
            </td>   
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Mod Acopio - Regularización</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaModAcopioRegularizacion" runat="server"></asp:DropDownList>
            
            </td>  
            <td><span class="item">Aplica Mod Faenamiento - Ampliación</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="AplicaModFaenamientoAmpliacion" runat="server"></asp:DropDownList>
            
            </td>   
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Mod Faenamiento - Reducción</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaModFaenamientoReduccion" runat="server"></asp:DropDownList>
            
            </td>  
            <td><span class="item">Aplica Mod Faenamiento - Especie</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="AplicaModFaenamientoEspecie" runat="server"></asp:DropDownList>
            
            </td>   
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Mod Faenamiento - PT</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaModFaenamientoPT" runat="server"></asp:DropDownList>
            
            </td>  
            <td><span class="item">Aplica Mod Faenamiento - Regularización</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="AplicaModFaenamientoRegularizacion" runat="server"></asp:DropDownList>
            
            </td>   
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Mod Amerb - Ampliación</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaModAmerbAmpliacion" runat="server"></asp:DropDownList>
            
            </td>  
            <td><span class="item">Aplica Mod Amerb - Reducción</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="AplicaModAmerbReduccion" runat="server"></asp:DropDownList>
            
            </td>   
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Mod Amerb - Especie</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaModAmerbEspecie" runat="server"></asp:DropDownList>
            
            </td>  
            <td><span class="item">Aplica Mod Amerb - PT</span></td>  
            <td><span class="item">:</span></td>  
            <td>
            
            <asp:DropDownList ID="AplicaModAmerbPT" runat="server"></asp:DropDownList>
            
            </td>   
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Mod Amerb - Regularización</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:DropDownList ID="AplicaModAmerbRegularizacion" runat="server"></asp:DropDownList>
            
            </td>  
            <td>&nbsp;</td>  
            <td>&nbsp;</td>  
            <td>&nbsp;</td>   
        </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>

        <table class="form" cellpadding="0px" cellspacing="0px">   
        <tr>
            <td class="col1"><asp:Button ID="Guardar" runat="server" Text="<%$Resources:spanish.language,guardar%>"  CausesValidation="true" onclick="Guardar_Click" /></td>              
        </tr>
        </table>

        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>

        <br />

        
        <table class="form" cellpadding="0px" cellspacing="0px">   
        <tr>
            <td class="col1"><asp:Button ID="Button2" runat="server" Text="Buscar" onclick="Buscar_Click" /></td>              
        </tr>
        </table>

    </asp:Panel>

    </ContentTemplate>
    </asp:UpdatePanel>
                        
    <asp:UpdatePanel ID="UpdatePanelGridRequerimiento" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelGridRequerimiento"  Visible="true" runat="server" CssClass="Content_Grilla">

                      <asp:GridView 
                        ID="GridRequerimiento" 
                        DataKeyNames="idValDocumentacion"
                        runat="server"
                        RowStyle-VerticalAlign="top" AlternatingRowStyle-VerticalAlign="top" 
                        AutoGenerateColumns="False" 
                        CellPadding="4" 
                        ForeColor="#333333"
                        TabIndex="1"
                        GridLines="None" 
                        CssClass="mGrid"
                        OnRowDataBound="GridRequerimiento_RowDataBound"
                        PagerStyle-CssClass="pgr"
                        OnRowCommand="GridRequerimiento_RowCommand"
                        OnRowEditing="GridRequerimiento_RowEditing"
                        OnRowUpdating="GridRequerimiento_RowUpdating"
                        OnRowCancelingEdit="GridRequerimiento_RowCancelingEdit"
                        AllowPaging="True" PageSize="20" OnPageIndexChanging="GridRequerimiento_PageIndexChanged">
                        
                        <Columns>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label HeaderText="idPestana" ID="hidden2" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idPestana") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Id" DataField="idValDocumentacion" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                            <asp:BoundField HeaderText="Flujo Documental" DataField="flujoDocumentalString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                            <asp:BoundField HeaderText="Tipo Entrada" DataField="nombreTipoIO" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                            <asp:BoundField HeaderText="Tipo Salida" DataField="nombreTipoIO" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                            <asp:BoundField HeaderText="Origen" DataField="nombreTipoOrigen"     SortExpression="nombreTipoOrigen"     ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                            <asp:BoundField HeaderText="Destino" DataField="nombreTipoDestinatario" SortExpression="nombreTipoDestinatario" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                            <asp:BoundField HeaderText="Ambito" DataField="nombrePestana" SortExpression="nombrePestana" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                            <asp:BoundField HeaderText="Seccion" DataField="seccionString" SortExpression="nombrePestana" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                            <asp:BoundField HeaderText="Tema" DataField="nombreSubRequerimiento" SortExpression="nombreSubRequerimiento" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                            <asp:BoundField HeaderText="Tipo Documento" DataField="nombreTipoDocResp"  SortExpression="nombreTipoDocResp" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                                                        
                            <asp:TemplateField HeaderText="Numero" SortExpression="numeroString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="gNumero" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("numeroString")) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>

                                <asp:DropDownList ID="ddleditCountry" runat="server" AutoPostBack="true" />
                            
                                <asp:HiddenField ID="hdnCountry" runat="server" Value='<%#Eval("numeroString") %>' />
                        
                            </EditItemTemplate>
                            </asp:TemplateField> 
                            
                            <asp:TemplateField HeaderText="Fecha" SortExpression="fechaString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="gFecha" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("fechaString")) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>

                                <asp:DropDownList ID="ddleditCountry2" runat="server" AutoPostBack="true" />
                            
                                <asp:HiddenField ID="hdnCountry2" runat="server" Value='<%#Eval("fechaString") %>' />
                        
                            </EditItemTemplate>
                            </asp:TemplateField> 
                            
                            <asp:TemplateField HeaderText="Numero CI" SortExpression="numeroCIString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="gNumeroCI" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("numeroCIString")) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>

                                <asp:DropDownList ID="ddleditCountry3" runat="server" AutoPostBack="true" />
                            
                                <asp:HiddenField ID="hdnCountry3" runat="server" Value='<%#Eval("numeroCIString") %>' />
                        
                            </EditItemTemplate>
                            </asp:TemplateField> 
                            
                            <asp:TemplateField HeaderText="Fecha CI" SortExpression="fechaCIString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="gFechaCI" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("fechaCIString")) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>

                                <asp:DropDownList ID="ddleditCountry4" runat="server" AutoPostBack="true" />
                            
                                <asp:HiddenField ID="hdnCountry4" runat="server" Value='<%#Eval("fechaCIString") %>' />
                        
                            </EditItemTemplate>
                            </asp:TemplateField> 
                            
                            <asp:TemplateField HeaderText="Archivo Binario" SortExpression="archivoString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="gArchivoBinario" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("archivoString")) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>

                                <asp:DropDownList ID="ddleditCountry5" runat="server" AutoPostBack="true" />
                            
                                <asp:HiddenField ID="hdnCountry5" runat="server" Value='<%#Eval("archivoString") %>' />
                        
                            </EditItemTemplate>
                            </asp:TemplateField> 

                            <asp:TemplateField HeaderText="Verifica Conforme" SortExpression="verificaConformeString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="gVerificaConforme" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "verificaConformeString") %>'  ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                           
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Extension Plazo" SortExpression="nuevaFechaString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="gNuevaFecha" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nuevaFechaString") %>'  ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                           
                            </asp:TemplateField>              

                            <asp:TemplateField HeaderText="Concesion" SortExpression="aplicaConcesionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="gConcesion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaConcesionString") %>'  ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                           
                            </asp:TemplateField>  

                            <asp:TemplateField HeaderText="Mod. Ampliacion" SortExpression="aplicaModAmpliacionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gModAmpliacion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModAmpliacionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                           
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mod. Reduccion" SortExpression="aplicaModReduccionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gModReduccion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModReduccionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                           
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mod. Especie PT" SortExpression="aplicaModEspeciePTString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gModEspeciePT" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModEspeciePTString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                           
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Mod. Regularizacion" SortExpression="aplicaModRegularizacionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gModRegularizacion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModRegularizacionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                           
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Relocalizacion" SortExpression="aplicaRelocalizacionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gRelocalizacion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaRelocalizacionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                           
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Amerb" SortExpression="aplicaAmerbString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAmerb" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaAmerbString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                           
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Faenamiento" SortExpression="aplicaFaenamientoString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gFaenamiento" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaFaenamientoString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                           
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Acopio" SortExpression="aplicaAcopioString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAcopio" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaAcopioString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                           
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Colector" SortExpression="aplicaColectoresString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gColector" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaColectoresString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                           
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Verifica Ampliacion de Plazo" SortExpression="verificaAmpPlazoString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gVerificaAmpPlazo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "verificaAmpPlazoString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Verifica Ampliacion Extension" SortExpression="verificaAmpExtensionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gVerificaAmpExtension" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "verificaAmpExtensionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Experimental Amerb" SortExpression="aplicaExpAmerbString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaExpAmerb" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaExpAmerbString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Aplica Experimental Concesion" SortExpression="aplicaExpConcesionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaExpConcesion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaExpConcesionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica ECMPO" SortExpression="aplicaECMPOString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaECMPO" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaECMPOString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod ECMPO - Ampliacion" SortExpression="aplicaModECMPOAmpliacionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModECMPOAmpliacion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModECMPOAmpliacionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod ECMPO - Reduccion" SortExpression="aplicaModECMPOReduccionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModECMPOReduccion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModECMPOReduccionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod ECMPO - Especie" SortExpression="aplicaModECMPOEspecieString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModECMPOEspecie" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModECMPOEspecieString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod ECMPO - PT" SortExpression="aplicaModECMPOPTString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModECMPOPT" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModECMPOPTString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod ECMPO - Regularizacion" SortExpression="aplicaModECMPORegularizacionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModECMPORegularizacion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModECMPORegularizacionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod Acopio - Ampliacion" SortExpression="aplicaModAcopioAmpliacionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModAcopioAmpliacion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModAcopioAmpliacionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod Acopio - Reduccion" SortExpression="aplicaModAcopioReduccionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModAcopioReduccion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModAcopioReduccionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod Acopio - Especie" SortExpression="aplicaModAcopioEspecieString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModAcopioEspecie" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModAcopioEspecieString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod Acopio - PT" SortExpression="aplicaModAcopioPTString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModAcopioPT" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModAcopioPTString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod Acopio - Regularizacion" SortExpression="aplicaModAcopioRegularizacionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModAcopioRegularizacion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModAcopioRegularizacionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod Faenamiento - Ampliacion" SortExpression="aplicaModFaenamientoAmpliacionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModFaenamientoAmpliacion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModFaenamientoAmpliacionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Aplica Mod Faenamiento - Reduccion" SortExpression="aplicaModFaenamientoReduccionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModFaenamientoReduccion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModFaenamientoReduccionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod Faenamiento - Especie" SortExpression="aplicaModFaenamientoEspecieString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModFaenamientoEspecie" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModFaenamientoEspecieString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod Faenamiento - PT" SortExpression="aplicaModFaenamientoPTString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModFaenamientoPT" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModFaenamientoPTString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod Faenamiento - Regularizacion" SortExpression="aplicaModFaenamientoRegularizacionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModFaenamientoRegularizacion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModFaenamientoRegularizacionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod Amerb - Ampliacion" SortExpression="aplicaModAmerbAmpliacionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModAmerbAmpliacion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModAmerbAmpliacionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod Amerb - Reduccion" SortExpression="aplicaModAmerbReduccionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModAmerbReduccion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModAmerbReduccionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod Amerb - Especie" SortExpression="aplicaModAmerbEspecieString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModAmerbEspecie" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModAmerbEspecieString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod Amerb - PT" SortExpression="aplicaModAmerbPTString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModAmerbPT" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModAmerbPTString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Aplica Mod Amerb - Regularizacion" SortExpression="aplicaModAmerbRegularizacionString" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="gAplicaModAmerbRegularizacion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaModAmerbRegularizacionString") %>' ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                            <ItemTemplate>
                            <asp:ImageButton ID="gModificar" Visible="false" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idValDocumentacion") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                            <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idValDocumentacion") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:ImageButton ID="gActualizar" runat="server" CommandName="Update" ImageUrl="~/App_Themes/admin_style/images/guardar2.png"  Height="20px" AlternateText="Actualizar" ToolTip="Actualizar" />
                            <asp:ImageButton ID="gCancelar" runat="server" CommandName="Cancel" ImageUrl="~/App_Themes/admin_style/images/cancelar.png"  Height="20px" AlternateText="Cancelar" ToolTip="Cancelar" />                            
                            </EditItemTemplate>
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
    </asp:Content>