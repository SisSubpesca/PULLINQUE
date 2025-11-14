<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menuSolicitudesModificacionCentroAcopio.ascx.cs" Inherits="SubPesca.Solicitudes.includes.menuSolicitudesModificacionCentroAcopio" %>


<!--Se agrega link-->
<script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.js")); %>" type="text/javascript"></script>
<script src="<% Response.Write(ResolveClientUrl("~/js/sb-admin-2.js")); %>" type="text/javascript"></script>
<script src="<% Response.Write(ResolveClientUrl("~/js/metisMenu.js")); %>" type="text/javascript"></script>
<script src="<% Response.Write(ResolveClientUrl("~/js/jquery/bootstrap.js")); %>" type="text/javascript"></script>
<link href="//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet"></link>

<!-- eliminar dos lineas mas abajo chocan con le nav bar nuevo -->
<asp:Panel ID="Content_MenuV"  runat="server" Visible ="false"></asp:Panel>
<asp:Panel ID="Content_MenuV_PublicLink"  style="border-top-width:0px;" runat="server">
    <div>
    <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
         <div class="navbar-default sidebar" role="navigation">
            <div class="sidebar-nav">
                <ul class="nav" id="Ul1">

                   <li>
                        <a href="#"><i class="fa-fw"></i>Solicitud Modificación Centro de Acopio<span></span></a>
                        <ul class="nav nav-third-level">
                           <li ID="Li5" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/datosModificacionCentroAcopio.aspx")); %>" >Datos del Trámite de Modificación</a>
                           </li>
                           <li ID="Li1" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/identificacionTitularModificacionCentroAcopio.aspx")); %>" >Identificación del Titular</a>
                           </li>
                           <li ID="Li2" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/generalModificacionCentroAcopio.aspx")); %>">Referencia Global</a>
                           </li>
                           <li ID="Li3" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/antecedDelSectorModificacionCentroAcopio.aspx")); %>">Antecedentes del Sector</a>
                           </li>
                           <li ID="Li4" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/proyTecnicoModificacionCentroAcopio.aspx")); %>">Proyecto Técnico</a>
                           </li>
                           <li ID="Li6" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/unidadesEspacialesModificacionCentroAcopio.aspx")); %>">Modificación de Centro de Acopio</a>
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
                              <li ID="ITUOT" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/pestanaInformeDeCartografiaModificacionCentroAcopio.aspx")); %>">IT U.O.T</a>
                              </li>
                              <li ID="Inspeccionterreno" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/pestanaInspeccionTerrenoModificacionCentroAcopio.aspx")); %>">Inspeccion en terreno</a>
                              </li>
                              <li ID="Banconatural" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/pestanaBancoNaturalModificacionCentroAcopio.aspx")); %>">Banco Natural</a>
                              </li>
                              <li ID="Difusionbn" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/pestanaDifusionBancoNaturalModificacionCentroAcopio.aspx")); %>">Difusión Banco Natural</a>
                              </li>
                              <li ID="Difrol" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/pestanaDifrolModificacionCentroAcopio.aspx")); %>">DIFROL</a>
                              </li>
                              <li ID="Informeambiental" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/pestanaInformeAmbientalModificacionCentroAcopio.aspx")); %>">Informe Ambiental</a>
                              </li>
                              <li ID="Antecedentescomplementarios" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/pestanaAntecedentesComplementariosModificacionCentroAcopio.aspx")); %>">Antecedentes Complementarios</a>
                              </li>
                              <li ID="Planos" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/pestanaPlanosModificacionCentroAcopio.aspx")); %>">Planos</a>
                              </li>
                              <li ID="Informedac" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/pestanaInformeDACModificacionCentroAcopio.aspx")); %>">Informe DAC</a>
                              </li>
                              <li ID="Resolucionssp" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/pestanaResolucionSSPModificacionCentroAcopio.aspx")); %>">Resolución SSP</a>
                              </li>
                              <li ID="Resolucionssffaa" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/pestanaResolucionSSFFAAModificacionCentroAcopio.aspx")); %>">Resolución SSFFAA</a>
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
                                <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/ingresarDocumentoModificacionCentroAcopio.aspx")); %>">Administrador de Documentos</a>
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