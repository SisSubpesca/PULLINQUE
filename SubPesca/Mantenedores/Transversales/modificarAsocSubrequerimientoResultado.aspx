<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administrador/SitioAdmin.Master" CodeBehind="modificarAsocSubrequerimientoResultado.aspx.cs" 
Inherits="SubPesca.Mantenedores.Transversales.modificarAsocSubrequerimientoResultado" Theme="admin_style" %>

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

    <fieldset>

    <legend>Modificar Asociación Subrequerimiento - Tipo Documento</legend>

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
                            <asp:DropDownList ID="FlujoDocumental" AutoPostBack="true" runat="server" 
                                OnSelectedIndexChanged="FlujoDocumental_change" Enabled ="false" 
                                ForeColor="Silver"></asp:DropDownList>
                           
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
                                <asp:DropDownList ID="TipoSalida" AutoPostBack="true" runat="server" 
                                    OnSelectedIndexChanged="TipoSalida_change" Enabled ="false" ForeColor="Silver"></asp:DropDownList>
                               
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
                                <asp:DropDownList ID="TipoEntrada" AutoPostBack="true" runat="server" 
                                    OnSelectedIndexChanged="TipoEntrada_change" Enabled ="false" ForeColor="Silver"></asp:DropDownList>
                               
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
                    <asp:CheckBox ID="VerificaConforme" runat="server" /></td>
                <td>
                    <span class="item">Extensión Plazo</span></td>
                <td>
                    <span class="item">:</span></td>
                <td>
                    <asp:CheckBox ID="ExtensionPlazo" runat="server" /></td>
            </tr>
            <tr>
                <td class="col1">
                    <span class="item">Aplica Concesión</span></td>
                <td class="col2">
                    <span class="item">:</span></td>
                <td class="col3">
                    <asp:CheckBox ID="AplicaConcesion" runat="server" />
                </td>
                <td>
                    <span class="item">Aplica Modificación - Ampliación</span></td>
                <td>
                    <span class="item">:</span></td>
                <td>
                    <asp:CheckBox ID="AplicaModificacionAmpliacion" runat="server" />
                </td>
            </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Modificación - Reducción</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:CheckBox ID="AplicaModificacionReduccion" runat="server" /></td>  
            <td><span class="item">Aplica Modificación - Especie PT</span></td>  
            <td><span class="item">:</span></td>  
            <td><asp:CheckBox ID="AplicaModificacionEspeciePT" runat="server" /></td>     
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Modificación - Regularización</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:CheckBox ID="AplicaModificacionRegularizacion" runat="server" /></td>  
            <td><span class="item">Aplica Relocalización</span></td>  
            <td><span class="item">:</span></td>  
            <td><asp:CheckBox ID="AplicaRelocalizacion" runat="server" /></td>      
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Amerb</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:CheckBox ID="AplicaAmerb" runat="server" /></td>  
            <td><span class="item">Aplica Faenamiento</span></td>  
            <td><span class="item">:</span></td>  
            <td><asp:CheckBox ID="AplicaFaenamiento" runat="server" /></td>     
        </tr>
        <tr>
            <td class="col1"><span class="item">Aplica Acopio</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:CheckBox ID="AplicaAcopio" runat="server" /></td>  
            <td><span class="item">Aplica Colectores</span></td>  
            <td><span class="item">:</span></td>  
            <td><asp:CheckBox ID="AplicaColectores" runat="server" /></td>   
        </tr>
        <tr>
            <td class="col1"><span class="item">Verifica Ampliación de Plazo</span></td>       
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:CheckBox ID="VerificaAmpliacionPlazo" runat="server" /></td>  
            <td><span class="item">Verifica Ampliación Extensión</span></td>  
            <td><span class="item">:</span></td>  
            <td><asp:CheckBox ID="VerficiaAmpliacionExtension" runat="server" /></td>   
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
            <td class="col1"><asp:Button ID="Guardar" runat="server" Text="Modificar"  CausesValidation="true" onclick="Guardar_Click" /></td>              
            <td class="col2"><asp:Button ID="Volver" runat="server" Text="Volver" 
                    onclick="Volver_Click"/></td>              
        </tr>
        </table>

        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>

        <br />

    </asp:Panel>

    </ContentTemplate>
    </asp:UpdatePanel>
             
    </fieldset>     

    </asp:Content>