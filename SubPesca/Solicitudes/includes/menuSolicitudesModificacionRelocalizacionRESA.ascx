<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menuSolicitudesModificacionRelocalizacionRESA.ascx.cs" Inherits="SubPesca.Solicitudes.includes.menuSolicitudesModificacionRelocalizacionRESA" %>

<asp:Panel ID="Content_MenuV"  runat="server" Visible="false"></asp:Panel>
<asp:Panel ID="Content_MenuV_PublicLink"  style="border-top-width:0px;" runat="server">
   <div>
    <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
         <div class="navbar-default sidebar" role="navigation">
            <div class="sidebar-nav">
                <ul class="nav" id="Ul1">

                   <li>
                        <a href="#"><i class="fa-fw"></i>Solicitud de Modificación de Relocalización RESA<span></span></a>
                        <ul class="nav nav-third-level">
                           <li ID="Li5" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Registrar/identificacionSolicitante.aspx")); %>" >Datos del Trámite de Relocalización</a>
                           </li>
                           <li ID="Li1" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/identificacionTitularCentroFaenamiento.aspx")); %>" >Identificación del Titular</a>
                           </li>
                           <li ID="Li2" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/generalModificacionCentroFaenamiento.aspx")); %>">Referencia Global</a>
                           </li>
                           <li ID="Li3" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/antecedDelSectorModificacionCentroFaenamiento.aspx")); %>">Antecedentes del Sector</a>
                           </li>
                           <li ID="Li4" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/proyTecnicoModificacionCentroFaenamiento.aspx")); %>">Proyecto Técnico</a>
                           </li>
                           <li ID="Li6" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/unidadesEspacialesCentroFaenamiento.aspx")); %>">Modificación de Centro de Faenamiento</a>
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
                              <li ID="ITUOT" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/pestanaInformeDeCartografiaModificacionCentroFaenamiento.aspx")); %>">IT U.O.T</a>
                              </li>
                              <li ID="Inspeccionterreno" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/pestanaInspeccionTerrenoModificacionCentroFaenamiento.aspx")); %>">Inspeccion en terreno</a>
                              </li>
                              <li ID="Banconatural" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/pestanaBancoNaturalModificacionCentroFaenamiento.aspx")); %>">Banco Natural</a>
                              </li>
                              <li ID="Difusionbn" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/pestanaDifusionBancoNaturalModificacionCentroFaenamiento.aspx")); %>">Difusión Banco Natural</a>
                              </li>
                              <li ID="Difrol" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/pestanaDifrolModificacionCentroFaenamiento.aspx")); %>">DIFROL</a>
                              </li>
                              <li ID="Informeambiental" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/pestanaInformeAmbientalModificacionCentroFaenamiento.aspx")); %>">Informe Ambiental</a>
                              </li>
                              <li ID="Antecedentescomplementarios" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/pestanaAntecedentesComplementariosModificacionCentroFaenamiento.aspx")); %>">Antecedentes Complementarios</a>
                              </li>
                              <li ID="Planos" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/pestanaPlanosModificacionCentroFaenamiento.aspx")); %>">Planos</a>
                              </li>
                              <li ID="Informedac" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/pestanaInformeDACModificacionCentroFaenamiento.aspx")); %>">Informe DAC</a>
                              </li>
                              <li ID="Resolucionssp" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/pestanaResolucionSSPModificacionCentroFaenamiento.aspx")); %>">Resolución SSP</a>
                              </li>
                              <li ID="Resolucionssffaa" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/pestanaResolucionSSFFAAModificacionCentroFaenamiento.aspx")); %>">Resolución SSFFAA</a>
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
                                <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/ingresarDocumentoModificacionCentroFaenamiento.aspx")); %>">Administrador de Documentos</a>
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