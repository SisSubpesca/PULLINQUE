<%@ Page    Language="C#" MasterPageFile="~/Solicitudes/SitioCatastroUnidades.Master" AutoEventWireup="true"  CodeBehind="informesResoluciones.aspx.cs" 
            Inherits="SubPesca.Solicitudes.Registrar.informesResoluciones" Theme="admin_style" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>



<%@ Register src="~/Solicitudes/Registrar/checkCertificadoOperacion.ascx"                      tagname="checkCertificadoOperacion"     tagprefix="uc1" %>

<%@ Register src="informeDeCartografia.ascx"            tagname="informeDeCartografia"                          tagprefix="uc30" %>
<%@ Register src="~/Solicitudes/Modificacion/unidadDeDependencia.ascx"         tagname="unidadDeDependencia"    tagprefix="uc31" %>
<%@ Register src="informeDeCartografiaObservacion.ascx" tagname="informeDeCartografiaObservacion"               tagprefix="uc29" %>

<%@ Register src="inspeccionTerreno.ascx"               tagname="inspeccionTerreno"                             tagprefix="uc28" %>
<%@ Register src="inspeccionTerrenoCoordenadas.ascx"    tagname="inspeccionTerrenoCoordenadas"                  tagprefix="uc32" %>
<%@ Register src="inspeccionTerrenoObservacion.ascx"    tagname="inspeccionTerrenoObservacion"                  tagprefix="uc27" %>

<%@ Register src="bancoNatural.ascx"                    tagname="bancoNatural"                                  tagprefix="uc26" %>
<%@ Register src="bancoNaturalObservacion.ascx"         tagname="bancoNaturalObservacion"                       tagprefix="uc25" %>

<%@ Register src="difusionBancoNatural.ascx"            tagname="difusionBancoNatural"                          tagprefix="uc24" %>
<%@ Register src="difusionBancoNaturalObservacion.ascx" tagname="difusionBancoNaturalObservacion"               tagprefix="uc23" %>

<%@ Register src="difrol.ascx"                          tagname="difrol"                                        tagprefix="uc22" %>
<%@ Register src="difrolObservacion.ascx"               tagname="difrolObservacion"                             tagprefix="uc21" %>


<%@ Register src="informeSEACartaTitular.ascx"              tagname="informeSEACartaTitular"                    tagprefix="uc19" %>
<%@ Register src="informeSEARCA.ascx"                       tagname="informeSEARCA"                             tagprefix="uc18" %>
<%@ Register src="informeSEAConsultaSEA.ascx"               tagname="informeSEAConsultaSEA"                     tagprefix="uc17" %>
<%@ Register src="informeSEACartaAmbiental.ascx"            tagname="informeSEACartaAmbiental"                  tagprefix="uc16" %>
<%@ Register src="informeSEAInformeUnidadAmbiental.ascx"    tagname="informeSEAInformeUnidadAmbiental"          tagprefix="uc15" %>
<%@ Register src="informeSEAObservacion.ascx"               tagname="informeSEAObservacion"                     tagprefix="uc14" %>

<%@ Register src="antecedentesComplementarios.ascx"                 tagname="antecedentesComplementarios"               tagprefix="uc13" %>
<%@ Register src="certificadoOperacion.ascx"                        tagname="certificadoOperacion"                      tagprefix="uc33" %>
<%@ Register src="antecedentesComplementariosObservacion.ascx"      tagname="antecedentesComplementariosObservacion"    tagprefix="uc12" %>

<%@ Register src="planos.ascx"                          tagname="planos"                        tagprefix="uc11" %>
<%@ Register src="planosInformeTecnicoUOT.ascx"         tagname="planosInformeTecnicoUOT"       tagprefix="uc10" %>
<%@ Register src="planosObservacion.ascx"               tagname="planosObservacion"             tagprefix="uc9" %>

<%@ Register src="informeDAC.ascx"                      tagname="informeDAC"                    tagprefix="uc8" %>
<%@ Register src="informeDACDevolucionJuridica.ascx"    tagname="informeDACDevolucionJuridica"  tagprefix="uc7" %>
<%@ Register src="informeDACObservacion.ascx"           tagname="informeDACObservacion"         tagprefix="uc6" %>

<%@ Register src="resolucionSSP.ascx"                   tagname="resolucionSSP"                 tagprefix="uc5" %>
<%@ Register src="resolucionSSPDevolucionSSFFAA.ascx"   tagname="resolucionSSPDevolucionSSFFAA" tagprefix="uc4" %>
<%@ Register src="resolucionSSPObservacion.ascx"        tagname="resolucionSSPObservacion"      tagprefix="uc3" %>

<%@ Register src="resolucionSSFFAA.ascx"                tagname="resolucionSSFFAA"              tagprefix="uc2" %>
<%@ Register src="resolucionSSFFAAObservacion.ascx"     tagname="resolucionSSFFAAObservacion"   tagprefix="uc1" %>


<%@ Register src="informacionSolicitud.ascx"                   tagname="informacionSolicitud"                 tagprefix="uc0" %>          

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">


    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"></asp:ToolkitScriptManager>

    
        <asp:UpdatePanel ID="UpdatePanelInformacionSolicitud" UpdateMode="Conditional" runat="server">
            <ContentTemplate> 
                <asp:Panel ID="PanelInformacionSolicitud"  Visible="true" runat="server">
                    <uc0:informacionSolicitud ID="informacionSolicitud" runat="server" />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

     <fieldset>

        <legend>Informes y Resoluciones</legend>
        <br />
        <table cellpadding="0px" cellspacing="0px">
        <tr>
            <td>
                <a class="tab1_selected" id="ctl00_rightbody_lnk_InfCartografia" onclick="javacript:mostrarPestanas(1);">IT U.O.T.</a>
            </td>

            <td>
                <a class="tab2" id="ctl00_rightbody_lnk_InspTerreno" onclick="javacript:mostrarPestanas(2);">Inspeccion en terreno</a>
            </td>

            <td>
                <a class="tab3" id="ctl00_rightbody_lnk_BancoNatural" onclick="javacript:mostrarPestanas(3);">Banco Natural</a>
            </td>

            <td>
                <a class="tab4" id="ctl00_rightbody_lnk_DifusionBancoNatural" onclick="javacript:mostrarPestanas(4);">Difusión Banco Natural</a>
            </td>

            <td>
                <asp:Panel  ID="TabDifrol" Visible="false" runat="server">
                    <a class="tab5" id="ctl00_rightbody_lnk_Difrol" onclick="javacript:mostrarPestanas(5);">DIFROL</a>
                </asp:Panel>
            </td>

            <td>
               <a class="tab6" id="ctl00_rightbody_lnk_InformeSEA" onclick="javacript:mostrarPestanas(6);">Informe Ambiental</a>
            </td>

            <td>
               <a class="tab7" id="ctl00_rightbody_lnk_AntecedentesComplementarios" onclick="javacript:mostrarPestanas(7);">Antecedentes Complementarios</a>
            </td>

            <td>
                <a class="tab8" id="ctl00_rightbody_lnk_Planos" onclick="javacript:mostrarPestanas(8);">Planos</a>
            </td>


            <td>
                <a  class="tab9" id="ctl00_rightbody_lnk_InformeDAC" onclick="javacript:mostrarPestanas(9);">Informe DAC</a>
            </td>

            <td>
                <a  class="tab10" id="ctl00_rightbody_lnk_ResolucionSSP" onclick="javacript:mostrarPestanas(10);">Resolución SSP</a>
            </td>


            <td>
                <a  class="tab11" id="ctl00_rightbody_lnk_ResolucionSSFFAA" onclick="javacript:mostrarPestanas(11);">Resolución SSFFAA</a>
            </td>

           
        </tr>
        </table>

        <div id="seccion1" style="display:none;">

            <asp:Panel ID="PanelInforCart"  Visible="true" runat="server">
                <uc30:informeDeCartografia ID="informeDeCartografia" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelEvaluacionOrdenamiento"  Visible="true" runat="server">
                <uc50:evaluacionOrdenamientoTerritorial ID="evaluacionOrdenamientoTerritorial" runat="server" />
            </asp:Panel>
            <!--
            <asp:Panel ID="PanelunidadDeDependencia"  Visible="true" runat="server">
                <uc31:unidadDeDependencia ID="unidadDeDependencia" runat="server" />
            </asp:Panel>
            -->
            <asp:Panel ID="PanelInforCartObservacion"  Visible="true" runat="server">
                <uc29:informeDeCartografiaObservacion ID="informeDeCartografiaObservacion" runat="server" />
            </asp:Panel>

        </div>


        <div id="seccion2" style="display:none;">

            <asp:Panel ID="PanelInspeccionTerreno"  Visible="true" runat="server">
                <uc28:inspeccionTerreno ID="inspeccionTerreno" runat="server" />
            </asp:Panel>

            <asp:Panel ID="PanelInspeccionTerrenoCoordenadas"  Visible="true" runat="server">
                <uc32:inspeccionTerrenoCoordenadas ID="inspeccionTerrenoCoordenadas" runat="server" />
            </asp:Panel>

            <asp:Panel ID="PanelInspeccionTerrenoObservacion"  Visible="true" runat="server">
                <uc27:inspeccionTerrenoObservacion ID="inspeccionTerrenoObservacion" runat="server" />
            </asp:Panel>
        
        </div>

        
        <div id="seccion3" style="display:none;">

            <asp:Panel ID="PanelBancoNatural"  Visible="true" runat="server">
                <uc26:bancoNatural ID="bancoNatural" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelBancoNaturalObservacion"  Visible="true" runat="server">
                <uc25:bancoNaturalObservacion ID="bancoNaturalObservacion" runat="server" />
            </asp:Panel>

        </div>
        

        <div id="seccion4" style="display:none;">

            <asp:Panel ID="PanelDifusionBancoNatural"  Visible="true" runat="server">
                <uc24:difusionBancoNatural   ID="difusionBancoNatural" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelDifusionBancoNaturalObservacion"  Visible="true" runat="server">
                <uc23:difusionBancoNaturalObservacion ID="difusionBancoNaturalObservacion" runat="server" />
            </asp:Panel>

        </div>


        
         <div id="seccion5" style="display:none;">

            <asp:Panel ID="PanelDifrol"  Visible="true" runat="server">
                <uc22:difrol ID="difrol" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelDifrolObservacion"  Visible="true" runat="server">
                <uc21:difrolObservacion ID="difrolObservacion" runat="server" />
            </asp:Panel>
            
        </div>

    
        <div id="seccion6" style="display:none;">


            <asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
               <ContentTemplate>    
                   <asp:panel ID="Panel1" runat="server">
                        <asp:ValidationSummary ID="ValidationSummaryErrores" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="SometimientoSEA"  />
                    </asp:panel>
                </ContentTemplate>
            </asp:UpdatePanel>

      

            <asp:UpdatePanel ID="UpdatePanelSometimientoSEA" UpdateMode="Conditional" runat="server">
                <ContentTemplate>   
                    <asp:Panel ID="PanelSometimientoSEA" CssClass="Content_msgGrilla" Visible="false" runat="server">
                        <div class="msgGrilla_div2">
                            <asp:Label ID="msSometimientoSEA" runat="server"></asp:Label>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>


            <fieldset>
                <legend>Informe SEA</legend>
                <br />

                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Ingresa SEA</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            <asp:DropDownList ID="FlujoSEA"  runat="server"  ValidationGroup="SometimientoSEA"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1"><span class="item"></span></td>
                        <td class="col2"><span class="item"></span></td>
                        <td class="col3"><asp:Button ID="Guardar" runat="server" Text="Guardar"  CausesValidation="true" onclick="Guardar_Click" ValidationGroup="SometimientoSEA" /></td>
                    </tr>
                    </table>
            </fieldset>


            <!-- check debe evaluarse ambientalmente -->
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate> 

                    <asp:Panel ID="Panel2"  Visible="true" runat="server">
                     
                    </asp:Panel>

            </ContentTemplate>
            </asp:UpdatePanel>

                   
            <asp:UpdatePanel ID="UpdatePanelInformeSEA" runat="server"  UpdateMode="Conditional">
                <ContentTemplate>

                    <asp:Panel ID="PanelInformeSEACartaTitular"  Visible="false" runat="server">
                        <uc19:informeSEACartaTitular ID="informeSEACartaTitularUC" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="PanelInformeSEARCA"  Visible="false" runat="server">
                        <uc18:informeSEARCA ID="informeSEARCA" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="PanelInformeSEAConsultaSEA"  Visible="false" runat="server">
                        <uc17:informeSEAConsultaSEA ID="informeSEAConsultaSEA" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="PanelInformeSEACartaAmbiental"  Visible="false" runat="server">
                        <uc16:informeSEACartaAmbiental ID="informeSEACartaAmbiental" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="PanelInformeSEAInformeUnidadAmbiental"  Visible="false" runat="server">
                        <uc15:informeSEAInformeUnidadAmbiental ID="informeSEAInformeUnidadAmbiental" runat="server" />
                    </asp:Panel>

                </ContentTemplate>
            </asp:UpdatePanel>

              

            <asp:Panel ID="PanelInformeSEAObservacion"  Visible="true" runat="server">
                <uc14:informeSEAObservacion ID="informeSEAObservacion" runat="server" />
            </asp:Panel>

        </div>

        
            
        <div id="seccion7" style="display:none;">
                     
            <asp:Panel ID="PanelAntecedentesComplementarios"  Visible="true" runat="server">
                <uc13:antecedentesComplementarios ID="antecedentesComplementarios" runat="server" />
            </asp:Panel>

            <asp:Panel ID="PanelCertificadoOperacion"  Visible="false" runat="server">
                
            </asp:Panel>

            <asp:Panel ID="PanelAntecedentesComplementariosObservacion"  Visible="true" runat="server">
                <uc12:antecedentesComplementariosObservacion ID="antecedentesComplementariosObservacion" runat="server" />
            </asp:Panel>

        </div>



        <div id="seccion8" style="display:none;">            

            <asp:Panel ID="PanelPlanos"  Visible="true" runat="server">
                <uc11:planos ID="planos" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelPlanosInformeTecnicoUOT"  Visible="true" runat="server">
                <uc10:planosInformeTecnicoUOT ID="planosInformeTecnicoUOT" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelPlanosObservacion"  Visible="true" runat="server">
                <uc9:planosObservacion ID="planosObservacion" runat="server" />
            </asp:Panel>

        </div>
    

        <div id="seccion9" style="display:none;">                      

            <!-- check certificado operacion -->
            <asp:UpdatePanel ID="UpdatePanelCheck" UpdateMode="Conditional" runat="server">
            <ContentTemplate> 

                    <asp:Panel ID="PanelCheck"  Visible="true" runat="server">
                        
                    </asp:Panel>

            </ContentTemplate>
            </asp:UpdatePanel>

            <asp:Panel ID="PanelInformeDAC"  Visible="true" runat="server">
                <uc8:informeDAC ID="informeDAC" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelInformeDACDevolucionJuridica"  Visible="true" runat="server">
                <uc7:informeDACDevolucionJuridica ID="informeDACDevolucionJuridica" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelInformeDACObservacion"  Visible="true" runat="server">
                <uc6:informeDACObservacion ID="informeDACObservacion" runat="server" />
            </asp:Panel>

        </div>



        <div id="seccion10" style="display:none;">  
                              
            <asp:Panel ID="PanelResolucionSSP"  Visible="true" runat="server">
                <uc5:resolucionSSP ID="resolucionSSP" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelResolucionSSPDevolucionSSFFAA"  Visible="true" runat="server">
                <uc4:resolucionSSPDevolucionSSFFAA ID="resolucionSSPDevolucionSSFFAA" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelResolucionSSPObservacion"  Visible="true" runat="server">
                <uc3:resolucionSSPObservacion ID="resolucionSSPObservacion" runat="server" />
            </asp:Panel>
            
        </div>


        <div id="seccion11" style="display:none;">  
            
            <asp:Panel ID="PanelResolucionSSFFAA"  Visible="true"  runat="server">
                <uc2:resolucionSSFFAA ID="resolucionSSFFAA" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelResolucionSSFFAAObservacion"  Visible="true" runat="server">
                <uc1:resolucionSSFFAAObservacion ID="resolucionSSFFAAObservacion" runat="server"/>
            </asp:Panel>

        </div>
     
    </fieldset>

 

</asp:Content>