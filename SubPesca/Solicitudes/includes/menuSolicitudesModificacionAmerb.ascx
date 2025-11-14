<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menuSolicitudesModificacionAmerb.ascx.cs" Inherits="SubPesca.Solicitudes.includes.menuSolicitudesModificacionAmerb" %>

<!--Se agrega link-->
<script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.js")); %>" type="text/javascript"></script>
<script src="<% Response.Write(ResolveClientUrl("~/js/sb-admin-2.js")); %>" type="text/javascript"></script>
<script src="<% Response.Write(ResolveClientUrl("~/js/metisMenu.js")); %>" type="text/javascript"></script>
<script src="<% Response.Write(ResolveClientUrl("~/js/jquery/bootstrap.js")); %>" type="text/javascript"></script>
<link href="//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet"></link>


<asp:Panel ID="Content_MenuV" runat="server" Visible="false"></asp:Panel>
<asp:Panel ID="Content_MenuV_PublicLink"  style="border-top-width:0px;" runat="server">
    <div>
    <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
         <div class="navbar-default sidebar" role="navigation">
            <div class="sidebar-nav">
                <ul class="nav" id="Ul1">

                   <li>
                        <a href="#"><i class="fa-fw"></i>Solicitud de Modificación de Amerb<span></span></a>
                        <ul class="nav nav-third-level">
                            <li ID="Li7" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/datosModificacionAmerb.aspx")); %>" >Datos del Trámite de Modificación</a>
                           </li>
                           <li ID="Li1" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/identificacionTitularModificacionAmerb.aspx")); %>" >Identificación del Titular</a>
                           </li>
                           <li ID="Li2" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/generalModificacionAmerb.aspx")); %>">Referencia Global</a>
                           </li>
                           <li ID="Li3" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/antecedDelSectorModificacionAmerb.aspx")); %>">Antecedentes del Sector</a>
                           </li>
                           <li ID="Li4" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/proyTecnicoModificacionAmerb.aspx")); %>">Proyecto Técnico</a>
                           </li>
                           <li ID="Li6" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/unidadesEspacialesModificacionAmerb.aspx")); %>">Modificación de Amerb</a>
                           </li>
                        </ul>
                   </li>
                </ul>
            </div>
                   <div class="navbar-collapse">
                        <ul class="nav" id="side-menu">
                    <li>
                        <a href="#"><i class="fa-fw"></i> Informes y Resoluciones<span class="fa arrow"></span></a>
                            
                        <ul class="nav nav-five-level">
                              <li ID="SuficienciaFormal" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/pestanaSuficienciaFormalModificacionAmerb.aspx")); %>">Suficiencia Formal</a>
                              </li>
                              <li ID="EvaluacionURB" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/pestanaEvaluacionURBModificacionAmerb.aspx")); %>">Evaluación URB</a>
                              </li>
                              <li ID="ITUOT" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/pestanaInformeDeCartografiaModificacionAmerb.aspx")); %>">IT U.O.T</a>
                              </li>
                              <li ID="Inspeccionterreno" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/pestanaInspeccionTerrenoModificacionAmerb.aspx")); %>">Inspeccion en terreno</a>
                              </li>
                              <li ID="Banconatural" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/pestanaBancoNaturalModificacionAmerb.aspx")); %>">Banco Natural</a>
                              </li>
                              <li ID="Difusionbn" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/pestanaDifusionBancoNaturalModificacionAmerb.aspx")); %>">Difusión Banco Natural</a>
                              </li>
                              <li ID="Difrol" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/pestanaDifrolModificacionAmerb.aspx")); %>">DIFROL</a>
                              </li>
                              <li ID="Informeambiental" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/pestanaInformeAmbientalModificacionAmerb.aspx")); %>">Informe Ambiental</a>
                              </li>
                              <li ID="Antecedentescomplementarios" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/pestanaAntecedentesComplementariosModificacionAmerb.aspx")); %>">Antecedentes Complementarios</a>
                              </li>
                              <li ID="Planos" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/pestanaPlanosModificacionAmerb.aspx")); %>">Planos</a>
                              </li>
                              <li ID="Informedac" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/pestanaInformeDACModificacionAmerb.aspx")); %>">Informe DAC</a>
                              </li>
                              <li ID="Resolucionssp" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/pestanaResolucionSSPModificacionAmerb.aspx")); %>">Resolución SSP</a>
                              </li>
                              <li ID="Resolucionssffaa" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/pestanaResolucionSSFFAAModificacionAmerb.aspx")); %>">Resolución SSFFAA</a>
                              </li>            
                        </ul>
                    </li>
                </ul>
            </div>
            <div class="sidebar-nav">
                <ul class="nav" id="Ul2">
                    <li>
                        <a href="#"><i class="fa-fw"></i> Administrador de Documentos<span></span></a>   
                        <ul class="nav nav-third-level">
                            <li>
                                <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/ingresarDocumentoModificacionAmerb.aspx")); %>">Administrador de Documentos</a>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
            <div class="sidebar-nav">
               
            </div>
        </div>
    </nav>
</div>
</asp:Panel>