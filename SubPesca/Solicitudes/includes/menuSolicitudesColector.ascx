<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menuSolicitudesColector.ascx.cs" Inherits="SubPesca.Solicitudes.includes.menuSolicitudesColector" %>

<!--Se agrega link-->
<script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.js")); %>" type="text/javascript"></script>
<script src="<% Response.Write(ResolveClientUrl("~/js/sb-admin-2.js")); %>" type="text/javascript"></script>
<script src="<% Response.Write(ResolveClientUrl("~/js/metisMenu.js")); %>" type="text/javascript"></script>
<script src="<% Response.Write(ResolveClientUrl("~/js/jquery/bootstrap.js")); %>" type="text/javascript"></script>
<link href="//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet"></link>

<asp:Panel ID="Content_MenuV"  runat="server" Visible="false"></asp:Panel>
<asp:Panel ID="Content_MenuV_PublicLink" style="border-top-width:0px;" runat="server">
     <div>
    <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
         <div class="navbar-default sidebar" role="navigation">
            <div class="sidebar-nav">
                <ul class="nav" id="Ul1">

                   <li>
                        <a href="#"><i class="fa-fw"></i>Solicitud Colectores de Semillas<span></span></a>
                        <ul class="nav nav-third-level">
                           <li ID="Li7" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/datosCentroColector.aspx")); %>" >Datos del Trámite</a>
                           </li>
                           <li ID="Li1" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/identificacionSolicitanteColector.aspx")); %>" >Identificación del Solicitante</a>
                           </li>
                           <li ID="Li2" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/generalColector.aspx")); %>">Referencia Global</a>
                           </li>
                           <li ID="Li3" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/antecedDelSectorColector.aspx")); %>">Antecedentes del Sector</a>
                           </li>
                           <li ID="Li4" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/proyTecnicoColector.aspx")); %>">Proyecto Técnico</a>
                           </li>
                           <li ID="Li6" Visible="true" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/unidadEspacialColector.aspx")); %>">Creación de Colector de Semilla</a>
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
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/pestanaInformeDeCartografiaColector.aspx")); %>">IT U.O.T</a>
                              </li>
                              <li ID="Inspeccionterreno" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/pestanaInspeccionTerrenoColector.aspx")); %>">Inspeccion en terreno</a>
                              </li>
                              <li ID="Banconatural" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/pestanaBancoNaturalColector.aspx")); %>">Banco Natural</a>
                              </li>
                              <li ID="Difusionbn" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/pestanaDifusionBancoNaturalColector.aspx")); %>">Difusión Banco Natural</a>
                              </li>
                              <li ID="Difrol" Visible="false" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/pestanaDifrolColector.aspx")); %>">DIFROL</a>
                              </li>
                              <li ID="Informeambiental" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/pestanaInformeAmbientalColector.aspx")); %>">Informe Ambiental</a>
                              </li>
                              <li ID="Antecedentescomplementarios" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/pestanaAntecedentesComplementariosColector.aspx")); %>">Antecedentes Complementarios</a>
                              </li>
                              <li ID="Planos" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/pestanaPlanosColector.aspx")); %>">Planos</a>
                              </li>
                              <li ID="Informedac" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/pestanaInformeDACColector.aspx")); %>">Informe DAC</a>
                              </li>
                              <li ID="Resolucionssp" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/pestanaResolucionSSPColector.aspx")); %>">Resolución SSP</a>
                              </li>
                              <li ID="Resolucionssffaa" Visible="true" runat="server">
                                  <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/pestanaResolucionSSFFAAColector.aspx")); %>">Resolución SSFFAA</a>
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
                                <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/ingresarDocumentoColector.aspx")); %>">Administrador de Documentos</a>
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