<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menuprincV.ascx.cs" Inherits="SubPesca.Administrador.includes.menuprinc_v" %>

<script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.js")); %>" type="text/javascript"></script>
<script src="<% Response.Write(ResolveClientUrl("~/js/sb-admin-2.js")); %>" type="text/javascript"></script>
<script src="<% Response.Write(ResolveClientUrl("~/js/metisMenu.js")); %>" type="text/javascript"></script>
<script src="<% Response.Write(ResolveClientUrl("~/js/jquery/bootstrap.js")); %>" type="text/javascript"></script>
<link href="//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet"></link>


<div>
   
   <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
   
   <div class="navbar-default sidebar" role="navigation">
   
   <div class="sidebar-nav navbar-collapse">
                   
        <ul class="nav" id="side-menu">
            <li>
              <a href="#"><i class="fa-fw"></i> Unidades Espaciales<span class="fa arrow"></span></a>
                 
                 <ul class="nav nav-second-level">
                    
                    <li ID="PanelConcesionAcuicultura" Visible="true" runat="server">
                       <a href="<% Response.Write(ResolveClientUrl("~/Unidades/Concesion/administrarConcesiones.aspx")); %>">Concesión de Acuicultura</a>
                    </li>
                    <li ID="PanelExperimentalConcesion" Visible="true" runat="server">
                       <a href="<% Response.Write(ResolveClientUrl("~/Unidades/ExperimentalesConcesion/administrarCentroExperimentalesConcesion.aspx")); %>">Actividades Experimentales de Concesión</a>
                    </li>  
                    <li ID="PanelAcuiculturaAmerb" Visible="true" runat="server">
                       <a href="<% Response.Write(ResolveClientUrl("~/Unidades/Amerb/administrarCentroAmerb.aspx")); %>">Actividades de Acuicultura en AMERB</a>
                    </li>
                     <li ID="PanelExperimentalAmerb" Visible="true" runat="server">
                       <a href="<% Response.Write(ResolveClientUrl("~/Unidades/ExperimentalesAmerb/administrarCentroExperimentalesAmerb.aspx")); %>">Actividades Experimental en AMERB</a>
                    </li>
                    <li ID="PanelAcuiculturaECMPO" Visible="true" runat="server">
                       <a href="<% Response.Write(ResolveClientUrl("~/Unidades/ECMPO/administrarCentroECMPO.aspx")); %>">Actividades de Acuicultura ECMPO</a>
                    </li>
                    <li ID="PanelCentroFaenamiento" Visible="true" class="nav nav-second-level" runat="server">
                       <a href="<% Response.Write(ResolveClientUrl("~/Unidades/Faenamiento/administrarCentroFaenamiento.aspx")); %>">Centro de Faenamiento</a>
                    </li>
                    <li ID="PanelCentroAcopio" Visible="true" runat="server">
                       <a href="<% Response.Write(ResolveClientUrl("~/Unidades/Acopio/administrarCentroAcopio.aspx")); %>">Centro de Acopio</a>
                    </li>
					<li ID="PanelColectorSemillas" Visible="true" class="nav nav-second-level" runat="server">
                       <a href="<% Response.Write(ResolveClientUrl("~/Unidades/Colector/administrarColectorSemillas.aspx")); %>">Centro Colector de Semillas</a>
                    </li>

                    <li ID="PanelReportesUE" Visible="false" runat="server">
                        <a href="#">Reportes<span class="fa arrow"></span></a>
                        <ul class="nav nav-third-level">
                            
                            <li ID="PanelReporteConcesionAcuicultura" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/Administrador/ReportesP3/reportesConcesion.aspx")); %>">Reportes Concesión de Acuicultura</a>
                            </li>

                            <li ID="PanelReporteExperimentalConcesion" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/Administrador/ReportesP3/ReportesExperimentalConcesion.aspx")); %>">Reportes Actividades Experimentales de Concesión</a>
                            </li>

                            <li ID="PanelReporteAmerb" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/Administrador/ReportesP3/reportesAcuiculturaAmerb.aspx")); %>">Reportes Actividades de Acuicultura Amerb</a>
                            </li>

                            <li ID="PanelReporteExperimentalAmerb" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/Administrador/ReportesP3/ReportesExperimentalAmerb.aspx")); %>">Reportes Actividades Experimentales en Amerb</a>
                            </li>
                            
                            <li ID="PanelReporteECMPO" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/Administrador/ReportesP3/reportesECMPO.aspx")); %>">Reportes Acuicultura ECMPO</a>
                            </li>
                            
                            <li ID="PanelReporteCentroFaenamiento" Visible="false" class="nav nav-second-level" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/Administrador/ReportesP3/reportesCentroFaenamiento.aspx")); %>">Reportes Centro de Faenamiento</a>
                            </li>

                            <li ID="PanelReporteAcopio" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/Administrador/ReportesP3/reportesCentroAcopio.aspx")); %>">Reportes Centro de Acopio</a>
                            </li>

                            <li ID="PanelReporteSemilla" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/Administrador/ReportesP3/reportesColectoresSemillas.aspx")); %>">Reportes Colector de Semillas</a>
                            </li>
                            
                        </ul>
                    </li>
                 </ul>
            </li>
            <li>
              <a href="#"><i class="fa-fw"></i> Solicitudes de Unidades Espaciales<span class="fa arrow"></span></a>
                 <ul class="nav nav-second-level">
                            
                      <li ID="PanelSolicitudConcesion" Visible="false" runat="server">
                        <a href="#">Solicitudes de Concesión de Acuicultura<span class="fa arrow"></span></a>
                        
                        <ul class="nav nav-third-level">
                           
                            <li id="PanelIngresarSolConcesion" Visible="false" runat="server" >
                                <a class="nav nav-second-level" href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Registrar/inicioSolicitudConcesion.aspx")); %>">Ingresar Solicitudes</a>
                            </li>
                          
                            <li ID="PanelAdministrarSolConcesion" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Registrar/administrarSolicitudConcesion.aspx")); %>">Administrar Solicitudes</a>
						    </li>
                          
						    <li  ID="PanelCierreConcesion" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/GenerarCierreForzado.aspx")); %>">Generar Cierre Forzado</a>
						    </li>

                            <li ID="PanelAdmCierreConcesion" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/administrarCierreForzado.aspx")); %>">Administrar Cierre Forzado</a>
							</li>

                            <li ID="PanelIndicadoresConcesion" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadores.aspx")); %>">Resumen de Indicadores</a>
							</li>
                        </ul>
                    </li>
            
   
                    <li ID="PanelSolicitudesExpConcesion" Visible="false" class="nav nav-second-level" runat="server">
                        <a href="#">Solicitudes de Experimentales Concesión<span class="fa arrow"></span></a>
                        <ul class="nav nav-third-level">
                           <li ID="PanelIngresarExpConcesion"  Visible="false" runat="server">
                                <a class="nav nav-second-level" href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ExperimentalesConcesion/inicioSolicitudExperimentalesConcesion.aspx")); %>">Ingresar Solicitudes</a>
                           </li>

                           <li ID="PanelAdministrarExpConcesion" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ExperimentalesConcesion/administrarSolicitudExperimentalesConcesion.aspx")); %>">Administrar Solicitudes</a>
						   </li>

						   <li ID="PanelCierreExpConcesion" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/GenerarCierreForzadoExperimentalesConcesion.aspx")); %>">Generar Cierre Forzado</a>
						   </li>

						    <li ID="PanelAdmCierreExpConcesion" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/administrarCierreForzadoExperimentalesConcesion.aspx")); %>">Administrar Cierre Forzado</a>
							</li>

                            <li ID="PanelResumenIndicadoresExpConcesion" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresExpConcesion.aspx")); %>">Resumen de Indicadores</a>
							</li>

						</ul>
                    </li>

				    <li ID="PanelModificacionConcesion" Visible="false" runat="server">
                        <a href="#">Solicitudes de Modificación Concesión<span class="fa arrow"></span></a>
                        <ul class="nav nav-third-level">

                        <li ID="PanelIngresarModConcesion" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Modificacion/ingresarSolicitudModificacion.aspx")); %>">Ingresar Solicitud Modificación</a>
						</li>

						<li ID="PanelAdministrarModConcesion" Visible="false" runat="server">
                          	<a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Modificacion/administrarSolicitudModificacion.aspx")); %>">Administrar Solicitud de Modificación</a>
						</li>
                            
                        <li  ID="PanelCierreModificacionConcesion" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/GenerarCierreForzadoModificacionConcesion.aspx")); %>">Generar Cierre Forzado</a>
						</li>
						<li ID="PanelAdmCierreModificacionConcesion" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/administrarCierreForzadoModificacionConcesion.aspx")); %>">Administrar Cierre Forzado</a>
						</li>
                            
						<li  ID="PanelIndicadoresModConcesion" Visible="false" runat="server">
						    <a href="#">Resumen de Indicadores<span class="fa arrow"></span></a>
						    <ul class="nav nav-fourth-level">
                            <li ID="PanelIndicadorAmpliacion" Visible="false" runat="server">
							    <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresModAmpliacion.aspx")); %>">Resumen de Indicadores Ampliación</a>
							</li>
							<li ID="PanelIndicadorReduccion" Visible="false" runat="server">
							    <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresModReduccion.aspx")); %>">Resumen de Indicadores Reducción</a>
							</li>
							<li ID="PanelIndicadorEspecie" Visible="false" runat="server">
							    <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresModEspecie.aspx")); %>">Resumen de Indicadores Especie</a>
							</li>
							<li  ID="PanelIndicadorProyecto" Visible="false" runat="server">
							    <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresModProyTecnico.aspx")); %>">Resumen de Indicadores Proyecto Técnico</a>
							</li>
							<li ID="PanelIndicadorRegulariza" Visible="false" runat="server">
								<a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresModRegularizacion.aspx")); %>">Resumen de Indicadores Regularización</a>
							</li>
							</ul>
					    </li>

                        </ul>
                   </li>
                 
					<li ID="PanelRelocalizacionLey" Visible="false" runat="server">
					<a href="#"><i class="fa-fw"></i> Solicitudes de Relocalización por Ley<span class="fa arrow"></span></a>
					<ul class="nav nav-third-level">
                    <li ID="PanelIngresarRelocalizacion" Visible="false" runat="server">
                        <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Relocalizacion/preIngresarSolicitudRelocalizacion.aspx")); %>">Ingresar Relocalización</a>
                    </li>
                    <li ID="PanelAdmRelocalizacion" Visible="false" runat="server">
                        <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Relocalizacion/administrarSolicitudRelocalizacion.aspx")); %>">Administrar Relocalización</a>
                    </li>
                    
                     <li  ID="PanelCierreForzadoRelocalizacionLey" Visible="false" runat="server">
                        <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/GenerarCierreForzadoRelocalizacionLey.aspx")); %>">Generar Cierre Forzado</a>
					</li>
					<li ID="PanelAdmCierreForzadoRelocalizacionLey" Visible="false" runat="server">
                        <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/administrarCierreForzadoRelocalizacionLey.aspx")); %>">Administrar Cierre Forzado</a>
					</li>

					<li ID="PanelIndicadoresRelocalizacion" Visible="false" runat="server">
                    <a href="#">Resumen de Indicadores <span class="fa arrow"></span></a>
					<ul class="nav nav-fourth-level">
						<li ID="PanelIndicadorCero" Visible="false" runat="server">
						    <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresLeyCero.aspx")); %>">Resumen de Indicadores Sector 0 </a>
						</li>
						<li ID="PanelIndicadorCrea" Visible="false" runat="server">
						    <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresLeyCrea.aspx")); %>">Resumen de Indicadores Crea </a>
						</li>
                        <li ID="PanelIndicadorFusion" Visible="false" runat="server">
						    <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresLeyFusion.aspx")); %>">Resumen de Indicadores Fusión </a>
						</li>


					</ul>
                    </li>
                    </ul>
		         </li>
				 
                 
                 <li ID="PanelRESA" class="nav nav-second-level" Visible="false" runat="server">
				    <a href="#"><i class="fa-fw"></i> Solicitudes Relocalización RESA <span class="fa arrow"></span></a>
					<ul class="nav nav-third-level">

                    <li ID="PanelIngresarRelocalizacionRESA" Visible="false" runat="server">
                        <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/RelocalizacionRESA/preIngresarSolicitudRelocalizacionRESA.aspx")); %>">Ingresar Relocalización</a>
                    </li>
                    <li ID="PanelAdmRelocalizacionRESA" Visible="false" runat="server">
                        <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/RelocalizacionRESA/administrarSolicitudRelocalizacionRESA.aspx")); %>">Administrar Relocalización</a>
                    </li>
                    
                    <li ID="PanelIngresarRESA" class="nav nav-second-level" Visible="false" runat="server">
                        <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/RelocalizacionRESA/ingresoInformeRESA.aspx")); %>">Ingresar Informe Relocalización RESA</a>
                    </li>
                    <li ID="PanelAdministrarRESA" class="nav nav-second-level" Visible="false" runat="server">
                        <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/RelocalizacionRESA/administrarInformesRESA.aspx")); %>">Administrar Informes Relocalización RESA</a> 
                    </li>

                    <li  ID="PanelCierreForzadoRelocalizacionRESA" Visible="false" runat="server">
                        <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/GenerarCierreForzadoRelocalizacionRESA.aspx")); %>">Generar Cierre Forzado</a>
					</li>
					<li ID="administrarCierreForzadoRelocalizacionRESA" Visible="false" runat="server">
                        <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/administrarCierreForzadoRelocalizacionRESA.aspx")); %>">Administrar Cierre Forzado</a>
					</li>

					<li ID="PanelIndicadoresRESA" class="nav nav-second-level" Visible="false" runat="server">
                        <a href="#">Resumen de Indicadores <span class="fa arrow"></span></a>
					    <ul class="nav nav-fourth-level">
                            <li ID="PanelIndicadorRESACrea" class="nav nav-second-level" Visible="false" runat="server">
        					    <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresRESACrea.aspx")); %>">Resumen de Indicadores Crea </a>
		    				</li>
			    			<li ID="PanelIndicadorRESACero" Visible="false" runat="server">
				    		    <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresRESACero.aspx")); %>">Resumen de Indicadores Sector 0 </a>
					    	</li>
                            <li ID="PanelIndicadorRESAFusion" Visible="false" runat="server">
				    		    <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresRESAFusion.aspx")); %>">Resumen de Indicadores Fusión </a>
					    	</li>
						</ul>
                    </li>
                  </ul>
			     </li>

                 <li ID="PanelSolFaenamiento" Visible="false" class="nav nav-second-level" runat="server">
                    <a href="#">Solicitudes de Centro de Faenamiento<span class="fa arrow"></span></a>
                    <ul class="nav nav-third-level">
                        <li ID="PanelIngresarSolFaenamiento"  Visible="false" class="nav nav-second-level" runat="server">
                            <a class="nav nav-second-level" href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Faenamiento/inicioSolicitudFaenamiento.aspx")); %>">Ingresar Solicitudes</a>
                        </li>
                        <li ID="PanelAdministrarSolFaenamiento" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Faenamiento/administrarSolicitudFaenamiento.aspx")); %>">Administrar Solicitudes</a>
                        </li>
				        <li ID="PanelCierreFaenamiento" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/GenerarCierreForzadoFaenamiento.aspx")); %>">Generar Cierre Forzado</a>
				        </li>
					    <li ID="PanelAdministrarCierreFaenamiento" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/administrarCierreForzadoFaenamiento.aspx")); %>">Administrar Cierre Forzado</a>
					    </li>
                        <li ID="PanelIndicadoresFaenamiento" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresFaenamiento.aspx")); %>">Resumen de Indicadores</a>
					    </li>
                    </ul>
                 </li>  
                 <li ID="PanelSolModificacionFaenamiento" Visible="false" class="nav nav-second-level" runat="server">
                 <a href="#">Solicitudes de Modificación de Centros de Faenamiento<span class="fa arrow"></span></a>
                    <ul class="nav nav-third-level">
                        <li ID="PanelIngresarModificacionFaenamiento"  Visible="false" runat="server">
                            <a class="nav nav-second-level" href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/ingresarSolicitudModificacionCentroFaenamiento.aspx")); %>">Ingresar Solicitudes</a>
                        </li>
                        <li ID="PanelAdministrarModificacionFaenamiento" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroFaenamiento/administrarSolicitudModificacionCentroFaenamiento.aspx")); %>">Administrar Solicitudes</a>
						</li>
                        <li ID="PanelCierreForzadoModificacionCentroFaenamiento" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/GenerarCierreForzadoModificacionCentroFaenamiento.aspx")); %>">Generar Cierre Forzado</a>
				        </li>
					    <li ID="PanelAdministrarCierreForzadoModificacionCentroFaenamiento" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/administrarCierreForzadoModificacionCentroFaenamiento.aspx")); %>">Administrar Cierre Forzado</a>
					    </li>

                        
                         <li ID="PanelIndicadoresModFaenamiento" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="#">Resumen de Indicadores <span class="fa arrow"></span></a>
					        <ul class="nav nav-fourth-level">
                                <li ID="PanelIndicadorFaenamientoAmpliacion" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresFaenamientoModAmpliacion.aspx")); %>">Resumen de Indicadores Ampliación</a>
							    </li>
							    <li ID="PanelIndicadorFaenamientoReduccion" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresFaenamientoModReduccion.aspx")); %>">Resumen de Indicadores Reducción</a>
							    </li>
							    <li ID="PanelIndicadorFaenamientoEspecie" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresFaenamientoModEspecie.aspx")); %>">Resumen de Indicadores Renovación</a>
							    </li>
							    <li  ID="PanelIndicadorFaenamientoProyectoTecnico" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresFaenamientoModProyTecnico.aspx")); %>">Resumen de Indicadores Proyecto Técnico/Especie</a>
							    </li>
							    <li ID="PanelIndicadorFaenamientoRegularizacion" Visible="false" runat="server">
								    <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresFaenamientoModRegularizacion.aspx")); %>">Resumen de Indicadores Regularización</a>
							    </li>
						    </ul>
                        </li>

                   </ul>
                 </li>   

                 <li ID="PanelSolicitudAcopio" Visible="false" class="nav nav-second-level" runat="server">
                 <a href="#">Solicitudes Centros de Acopio<span class="fa arrow"></span></a>
                    <ul class="nav nav-third-level">
                        <li ID="PanelIngresarAcopio"  Visible="false" class="nav nav-second-level" runat="server">
                            <a class="nav nav-second-level" href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Acopio/inicioSolicitudAcopio.aspx")); %>">Ingresar Solicitudes</a>
                        </li>
                        <li ID="PanelAdministrarAcopio" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Acopio/administrarSolicitudAcopio.aspx")); %>">Administrar Solicitudes</a>
						</li>
                        <li ID="PanelCierreAcopio" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/GenerarCierreForzadoAcopio.aspx")); %>">Generar Cierre Forzado</a>
						</li>
                        <li ID="PanelAdmCierreAcopio" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/administrarCierreForzadoAcopio.aspx")); %>">Administrar Cierre Forzado</a>
						</li>
                        <li  ID="PanelIndicadoresAcopio" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresAcopio.aspx")); %>">Resumen de Indicadores</a>
						</li>
                    </ul>
                </li>
                
                <li ID="PanelSolModificacionAcopio" Visible="false" class="nav nav-second-level" runat="server">
                    <a href="#">Solicitudes de Modificación de Centros de Acopio<span class="fa arrow"></span></a>
                    <ul class="nav nav-third-level">
                        <li ID="PanelIngresarModAcopio"  Visible="false" class="nav nav-second-level" runat="server">
                            <a class="nav nav-second-level" href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/ingresarSolicitudModificacionCentroAcopio.aspx")); %>">Ingresar Solicitudes</a>
                        </li>
                        <li ID="PanelAdministrarModAcopio" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionCentroAcopio/administrarSolicitudModificacionCentroAcopio.aspx")); %>">Administrar Solicitudes</a>
						</li>
                        <li ID="PanelCierreForzadoModificacionCentroAcopio" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/GenerarCierreForzadoModificacionCentroAcopio.aspx")); %>">Generar Cierre Forzado</a>
				        </li>
					    <li ID="PanelAdministrarCierreForzadoModificacionCentroAcopio" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/administrarCierreForzadoModificacionCentroAcopio.aspx")); %>">Administrar Cierre Forzado</a>
					    </li>

                         <li ID="PanelIndicadoresModAcopio" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="#">Resumen de Indicadores <span class="fa arrow"></span></a>
					        <ul class="nav nav-fourth-level">
                                <li ID="PanelIndicadorAcopioAmpliacion" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresAcopioModAmpliacion.aspx")); %>">Resumen de Indicadores Ampliación</a>
							    </li>
							    <li ID="PanelIndicadorAcopioReduccion" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresAcopioModReduccion.aspx")); %>">Resumen de Indicadores Reducción</a>
							    </li>
							    <li ID="PanelIndicadorAcopioEspecie" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresAcopioModEspecie.aspx")); %>">Resumen de Indicadores Renovación</a>
							    </li>
							    <li  ID="PanelIndicadorAcopioProyectoTecnico" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresAcopioModProyTecnico.aspx")); %>">Resumen de Indicadores Proyecto Técnico/Especie</a>
							    </li>
							    <li ID="PanelIndicadorAcopioRegularizacion" Visible="false" runat="server">
								    <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresAcopioModRegularizacion.aspx")); %>">Resumen de Indicadores Regularización</a>
							    </li>
						    </ul>
                        </li>

                    </ul>
                </li>

                 <li ID="PanelECMPO" Visible="false" class="nav nav-second-level" runat="server">
                    <a href="#">Solicitudes de Acuicultura en ECMPO<span class="fa arrow"></span></a>
                    <ul class="nav nav-third-level">
                         <li ID="PanelIngresarECMPO"  Visible="false" class="nav nav-second-level" runat="server">
                            <a class="nav nav-second-level" href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ECMPO/inicioSolicitudECMPO.aspx")); %>">Ingresar Solicitudes</a>
                         </li>
                         <li ID="PanelAdmECMPO" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ECMPO/administrarSolicitudECMPO.aspx")); %>">Administrar Solicitudes</a>
						 </li>
						 <li ID="PanelCierreECMPO" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/GenerarCierreForzadoECMPO.aspx")); %>">Generar Cierre Forzado</a>
						 </li>
						 <li ID="PanelAdmCierreECMPO" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/administrarCierreForzadoECMPO.aspx")); %>">Administrar Cierre Forzado</a>
						 </li>
                         <li ID="PanelIndicadoresECMPO" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresECMPO.aspx")); %>">Resumen de Indicadores</a>
						 </li>
                         
                    </ul>
                </li>
                <li ID="PanelModificacionECMPO" Visible="false" class="nav nav-second-level" runat="server">
                    <a href="#">Solicitudes de Modificación de ECMPO<span class="fa arrow"></span></a>
                    <ul class="nav nav-third-level">
                        <li ID="PanelIngresarModECMPO"  Visible="true" class="nav nav-second-level" runat="server">
                            <a class="nav nav-second-level" href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionECMPO/ingresarSolicitudModificacionECMPO.aspx")); %>">Ingresar Solicitudes</a>
                        </li>
                        <li ID="PanelAdministrarModECMPO" class="nav nav-second-level" Visible="true" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionECMPO/administrarSolicitudModificacionECMPO.aspx")); %>">Administrar Solicitudes</a>
                        </li>
                        <li ID="PanelCierreForzadoModificacionECMPO" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/GenerarCierreForzadoModificacionECMPO.aspx")); %>">Generar Cierre Forzado</a>
						</li>
                        <li ID="PanelAdministracionCierreForzadoModificacionECMPO" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/administrarCierreForzadoModificacionECMPO.aspx")); %>">Administrar Cierre Forzado</a>
                        </li>

                        <li ID="PanelIndicadoresModECMPO" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="#">Resumen de Indicadores <span class="fa arrow"></span></a>
					        <ul class="nav nav-fourth-level">
                                <li ID="PanelIndicadorECMPOAmpliacion" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresECMPOModAmpliacion.aspx")); %>">Resumen de Indicadores Ampliación</a>
							    </li>
							    <li ID="PanelIndicadorECMPOReduccion" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresECMPOModReduccion.aspx")); %>">Resumen de Indicadores Reducción</a>
							    </li>
							    <li ID="PanelIndicadorECMPOEspecie" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresECMPOModEspecie.aspx")); %>">Resumen de Indicadores Especie</a>
							    </li>
							    <li  ID="PanelIndicadorECMPOProyecto" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresECMPOModProyTecnico.aspx")); %>">Resumen de Indicadores Proyecto Técnico</a>
							    </li>
							    <li ID="PanelIndicadorECMPORegulariza" Visible="false" runat="server">
								    <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresECMPOModRegularizacion.aspx")); %>">Resumen de Indicadores Regularización</a>
							    </li>
						    </ul>
                        </li>


                    </ul>
                 </li>
                 <li ID="PanelSolicitudesAcuiculturaAmerb" Visible="false" class="nav nav-second-level" runat="server">
                    <a href="#">Solicitudes de Acuicultura en AMERB<span class="fa arrow"></span></a>
                    <ul class="nav nav-third-level">
                        <li ID="PanelIngresarAmerb"  Visible="false" class="nav nav-second-level" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Amerb/inicioSolicitudAmerb.aspx")); %>">Ingresar Solicitudes</a>
                        </li>
                        <li ID="PanelAdministrarAmerb" class="nav nav-second-level" Visible="false" runat="server">
                             <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Amerb/administrarSolicitudAmerb.aspx")); %>">Administrar Solicitudes</a>
						</li>
						<li ID="PanelCierreAmerb" class="nav nav-second-level" Visible="false" runat="server">
                             <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/GenerarCierreForzadoAmerb.aspx")); %>">Generar Cierre Forzado</a>
						</li>
						<li ID="PanelAdmCierreAmerb" class="nav nav-second-level" Visible="false" runat="server">
                             <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/administrarCierreForzadoAmerb.aspx")); %>">Administrar Cierre Forzado</a>
						</li>
                        <li ID="PanelIndicadoresSolAmerb" class="nav nav-second-level" Visible="false" runat="server">
                             <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresAmerb.aspx")); %>">Resumen de Indicadores</a>
						</li>
                    </ul>
                  </li>
                     <li ID="PanelExperimentalesAmerb" Visible="false" class="nav nav-second-level" runat="server">
                    <a href="#">Solicitudes de Experimentales en AMERB<span class="fa arrow"></span></a>
                    <ul class="nav nav-third-level">
                        <li ID="PanelIngresaSolExperimentalAmerb"  Visible="false" class="nav nav-second-level" runat="server">
                            <a class="nav nav-second-level" href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ExperimentalesAmerb/inicioSolicitudExperimentalesAmerb.aspx")); %>">Ingresar Solicitudes</a>
                        </li>
                        <li ID="PanelAdministrarSolExperimentalAmerb" class="nav nav-second-level" Visible="false" runat="server">
                             <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ExperimentalesAmerb/administrarSolicitudExperimentalesAmerb.aspx")); %>">Administrar Solicitudes</a>
						</li>
						<li ID="PanelCierreExperimentalAmerb" class="nav nav-second-level" Visible="false" runat="server">
                             <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/GenerarCierreForzadoExperimentalesAmerb.aspx")); %>">Generar Cierre Forzado</a>
						</li>
						<li ID="PanelAdmiCierreExperimentalAmerb" class="nav nav-second-level" Visible="false" runat="server">
                             <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/administrarCierreForzadoExperimentalesAmerb.aspx")); %>">Administrar Cierre Forzado</a>
						</li>
                        <li ID="PanelIndicadoresExperimentalAmerb" class="nav nav-second-level" Visible="false" runat="server">
                             <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresExperimentalAmerb.aspx")); %>">Resumen de Indicadores</a>
						</li>

                    </ul>
                  </li>
                  <li ID="PanelModAmerb" Visible="true" class="nav nav-second-level" runat="server">
                    <a href="#">Solicitudes de Modificación de AMERB<span class="fa arrow"></span></a>
                    <ul class="nav nav-third-level">
                        <li ID="PanelIngresarModAmerb"  Visible="true" class="nav nav-second-level" runat="server">
                            <a class="nav nav-second-level" href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/ingresarSolicitudModificacionAmerb.aspx")); %>">Ingresar Solicitudes</a>
                        </li>
						<li ID="PanelAdministrarModAmerb" class="nav nav-second-level" Visible="true" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/ModificacionAmerb/administrarSolicitudModificacionAmerb.aspx")); %>">Administrar Solicitudes</a>
						</li>
                        <li ID="PanelCierreForzadoModificacionAmerb" class="nav nav-second-level" Visible="false" runat="server">
                             <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/GenerarCierreForzadoModificacionAmerb.aspx")); %>">Generar Cierre Forzado</a>
						</li>
						<li ID="PanelAdministracionCierreForzadoModificacionAmerb" class="nav nav-second-level" Visible="false" runat="server">
                             <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/administrarCierreForzadoModificacionAmerb.aspx")); %>">Administrar Cierre Forzado</a>
						</li>


                         <li ID="PanelIndicadoresModAMERB" class="nav nav-second-level" Visible="false" runat="server">
	                        <a href="#">Resumen de Indicadores <span class="fa arrow"></span></a>
	                        <ul class="nav nav-fourth-level">
        	                        <li ID="PanelIndicadorAMERBAmpliacion" Visible="false" runat="server">
			                        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresAMERBModAmpliacion.aspx")); %>">Resumen de Indicadores Ampliación</a>
		                        </li>
		                        <li ID="PanelIndicadorAMERBReduccion" Visible="false" runat="server">
			                        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresAMERBModReduccion.aspx")); %>">Resumen de Indicadores Reducción</a>
		                        </li>
		                        <li ID="PanelIndicadorAMERBEspecie" Visible="false" runat="server">
			                        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresAMERBModEspecie.aspx")); %>">Resumen de Indicadores Especie</a>
		                        </li>
		                        <li  ID="PanelIndicadorAMERBProyecto" Visible="false" runat="server">
			                        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresAMERBModProyTecnico.aspx")); %>">Resumen de Indicadores Proyecto Técnico</a>
		                        </li>
		                        <li ID="PanelIndicadorAMERBRegulariza" Visible="false" runat="server">
			                        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresAMERBModRegularizacion.aspx")); %>">Resumen de Indicadores Regularización</a>
		                        </li>
	                        </ul>
                        </li>

                    </ul>
                  </li>
                  
                  <li ID="PanelSolColectorSemilla" Visible="false" class="nav nav-second-level" runat="server">
                    <a href="#">Solicitudes de Colectores de semilla<span class="fa arrow"></span></a>
                    <ul class="nav nav-third-level">
                        <li ID="PanelIngresarColector"  Visible="false" class="nav nav-second-level" runat="server">
                            <a class="nav nav-second-level" href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/inicioSolicitudColector.aspx")); %>">Ingresar Solicitudes</a>
                        </li>
                        <li ID="PanelAdminColector" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Colector/administrarSolicitudColector.aspx")); %>">Administrar Solicitudes</a>
						</li>
                        <li ID="PanelCierreForzadoColector" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/GenerarCierreForzadoColectores.aspx")); %>">Generar Cierre Forzado</a>
						</li>
                        <li ID="PanelAdminCierreForzado" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/CierreForzado/administrarCierreForzadoColectores.aspx")); %>">Administrar Cierre Forzado</a>
						</li>
                        <li ID="PanelIndicadoresColector" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Administrador/IndicadoresP3/resumenIndicadoresSemillas.aspx")); %>">Resumen de indicadores</a>
	                    </li>
                       
                      </ul>
                  </li>
                    
                <li ID="PanelCajadeHerramientas" class="nav nav-second-level" visible="false" runat="server">
                    <a href="#"><i class="fa-fx"></i> Caja de Herramientas<span class="fa arrow"></span></a>
                    <ul class="nav nav-third-level">
                            
                    <li ID="PanelReporte" class="nav nav-third-level" Visible="false" runat="server">
				        <a href="#"><i class="fa-fw"></i> Reportes<span class="fa arrow"></span></a>
				        <ul class="nav nav-fourth-level">
						    <li ID="PanelReporteSolicitudesAcuicultura" class="nav nav-five-level" Visible="false" runat="server">
						        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/ReportesP3/reportesSolicitudesAcuicultura.aspx")); %>">Reportes Solicitudes de Concesión Acuicultura</a> 
						    </li>
                            <li ID="PanelReporteSolicitudesFaenamiento" class="nav nav-five-level" Visible="false" runat="server">
						        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/ReportesP3/reportesSolicitudesFaenamiento.aspx")); %>">Reportes Solicitudes Centro de Faenamiento</a> 
					        </li>
						    <li ID="PanelReporteCentroAcopio" class="nav nav-five-level" Visible="false" runat="server">
						        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/ReportesP3/reportesSolicitudesAcopio.aspx")); %>">Reportes Solicitudes Centro de Acopio</a>
						    </li>
                            <li ID="PanelReporteCentroSemilla" class="nav nav-five-level" Visible="false" runat="server">
						        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/ReportesP3/reportesSolicitudesSemillas.aspx")); %>">Reportes Solicitudes Colectores de Semillas</a>
						    </li>
                            <li ID="PanelReporteAcuiculturaECMPO" class="nav nav-five-level" Visible="false" runat="server">
						        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/ReportesP3/reportesSolicitudesECMPO.aspx")); %>">Reportes Solicitudes ECMPO</a>
						    </li>
                            <li ID="PanelReporteSolicitudesAmerb" class="nav nav-five-level" Visible="false" runat="server">
						        <a href="<% Response.Write(ResolveClientUrl("~/Administrador/ReportesP3/reportesSolicitudesAMERB.aspx")); %>">Reportes Solicitudes Acuicultura de Amerb</a>
						    </li>
                        </ul>
				    </li>

                    <li ID="PanelGenerador_Administrar" class="nav nav-third-level" Visible="false" runat="server">
                        <a href="#"><i class="fa-fw"></i> Generador Documental<span class="fa arrow"></span></a>
                        <ul class="nav nav-fourth-level">
                            <li ID="PanelGeneradorDocumental" class="nav nav-five-level" Visible="false" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/GeneradorDocumental/GeneradorDocumental.aspx")); %>">Generador de Documentos<span></span></a>
                            </li>
                            <li ID="PanelAdministradorDocumental" class="nav nav-five-level" Visible="false" runat="server">
                               <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/GeneradorDocumental/AdministradorDocumental.aspx")); %>">Administrador de Documentos<span></span></a>
                            </li>    
                        </ul> 
                    </li>    
                    <li ID="PanelPoligonosMasivos" class="nav nav-third-level" Visible="false" runat="server">
                        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/PoligonosMasivos/PoligonosMasivos.aspx")); %>">Poligonos Masivos<span></span></a>
                    </li>

                    <li ID="PanelResumenEstado" class="nav nav-third-level" Visible="false" runat="server">
                        <a href="#"><i class="fa-fw"></i> Resumen de Estados<span class="fa arrow"></span></a>
                        <ul class="nav nav-fourth-level">
                            <li ID="PanelResumenEstado2" class="nav nav-five-level" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/Administrador/Reportes/resumenEstados.aspx")); %>">Resumen de Estados</a>
                            </li>
                        </ul>
	                </li>
                    <li ID="PanelRequerimientosPlazos" class="nav nav-third-level" Visible="false" runat="server">
                        <a href="#"><i class="fa-fw"></i> Plazos controlados en el sistema<span class="fa arrow"></span></a>
                        <ul class="nav nav-fourth-level">
			                <li ID="PanelRequerimientosPlazos2" class="nav nav-five-level" Visible="false" runat="server">
                                <a href="<% Response.Write(ResolveClientUrl("~/Administrador/Reportes/plazosRequerimientos.aspx")); %>">Requerimientos con Plazos</a>
				            </li>
                        </ul>
		            </li>

                    <li ID="PanelVisacionesMasivas" class="nav nav-third-level" Visible="false" runat="server">
				        <a href="#"><i class="fa-fw"></i>Visaciones Masivas<span class="fa arrow"></span></a>
				        <ul class="nav nav-fourth-level">
						    <li ID="PanelInicioVisacionMasiva" class="nav nav-five-level" Visible="false" runat="server">
						        <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Visaciones/InicioVisacion.aspx")); %>">Iniciar Visación Masiva</a> 
						    </li>
                            <li ID="PanelVisacionMasiva" class="nav nav-five-level" Visible="false" runat="server">
						        <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Visaciones/VisarFirmar.aspx")); %>">Visar Masiva</a> 
					        </li>
                            <li ID="PanelFirmaMasiva" class="nav nav-five-level" Visible="false" runat="server">
						        <a href="<% Response.Write(ResolveClientUrl("~/Solicitudes/Visaciones/FirmaMasiva.aspx")); %>">Firmar Masiva</a> 
					        </li>
                        </ul>
				    </li>

                    <li ID="PanelGrupoSuspendido" class="nav nav-third-level" Visible="false" runat="server">
				        <a href="#"><i class="fa-fw"></i> Grupo Suspendido<span class="fa arrow"></span></a>
				        <ul class="nav nav-fourth-level">
						    <li ID="CrearGrupoSuspendido" class="nav nav-five-level" Visible="false" runat="server">
						        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/GrupoSuspendido/CrearGrupoSuspendido.aspx")); %>">Crear Grupo Suspendido</a> 
						    </li>
                            <li ID="AdministrarGrupoSuspendido" class="nav nav-five-level" Visible="false" runat="server">
						        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/GrupoSuspendido/AdministradorGrupoSuspendido.aspx")); %>">Administrador de Grupos Suspendidos</a> 
					        </li>
                        </ul>
				    </li >

                    <li ID="PanelBuscadorDeTramites" class="nav nav-third-level" Visible="true" runat="server">
                        <a href="<% Response.Write(ResolveClientUrl("~/BuscadorTramites/buscadorDeTramites.aspx")); %>">Buscador de Tramite<span></span></a>
                    </li>
                    </ul>
                </li>
            </ul>
        </li>
                
		 

         

         <li ID="PanelResolucion" Visible="false" runat="server">
            <a href="#"><i class="fa-fw"></i>Resoluciones<span class="fa arrow"></span></a>
            <ul class="nav nav-second-level">
                <li ID="PanelIngresarResolucion" class="nav nav-second-level" Visible="false" runat="server">
                  <a href="<% Response.Write(ResolveClientUrl("~/Resoluciones/ingresarResoluciones.aspx")); %>">Ingresar Resoluciones</a>
                </li>
                <li  ID="PanelAdministrarResolucion" class="nav nav-second-level" Visible="false" runat="server">
                  <a href="<% Response.Write(ResolveClientUrl("~/Resoluciones/administrarResoluciones.aspx")); %>">Administrar Resoluciones</a>
                  <a href="<% Response.Write(ResolveClientUrl("~/Resoluciones/busquedaResoluciones.aspx")); %>">B&uacutesqueda de Resoluciones</a>
                </li>
                <li ID="PanelMaterias" class="nav nav-third-level" Visible="false" runat="server">
                    <a href="#">Mantenedores<span class="fa arrow"></span></a>
                    <ul class="nav nav-second-level">
                        <li ID="PanelMateria" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/materia.aspx")); %>">Materia</a>
                        </li>
                        <li ID="PanelMateriaSubrequerimiento" class="nav nav-fourth-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/materiaSubrequerimiento.aspx")); %>">Materia SubRequerimiento</a>
                        </li>
                    </ul>
                </li>
            </ul>
         </li>

         <li ID="PanelTitulares" Visible="false" runat="server">
            <a href="#"><i class="fa-fw"></i> Titulares<span class="fa arrow"></span></a>
            <ul class="nav nav-second-level">
                
                <!-- mantenedor de titulares, representantes legales y operadores -->

                <li ID="PanelAdministrarTitular" class="nav nav-second-level" Visible="false" runat="server">
                    <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Titulares/administrarTitulares.aspx")); %>">Administrar Titular</a>
                </li>
                <li ID="Pane1AdministrarRepresentante" class="nav nav-second-level" Visible="false" runat="server">
				    <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Titulares/administrarRepresentantesLegales.aspx")); %>">Administrar Representante Legal</a>
                </li>
				<li ID="PanelAdministrarOperadores" class="nav nav-second-level" Visible="false" runat="server">
                    <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Titulares/administrarOperadores.aspx")); %>">Administrar Operador</a>
                </li>
                
                <li ID="PanelMantenedorTitulares" class="nav nav-third-level" Visible="false" runat="server">
                    <a href="#">Mantenedores<span class="fa arrow"></span></a>
                    <ul class="nav nav-second-level">

                    <li ID="PanelTipoOrganizacion" class="nav nav-second-level" Visible="false" runat="server">
				        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoOrganizacion.aspx")); %>">Tipo Organización</a>
				    </li>

                    <li ID="PanelAdministrarHolding" class="nav nav-second-level" Visible="false" runat="server">
				        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/holding.aspx")); %>">Holding</a> 
                    </li>
                    
                    <li  ID="PanelTipoContacto" class="nav nav-second-level" Visible="false" runat="server"> 
                        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoContacto.aspx?bp=1")); %>">Tipo Contacto</a>
                    </li>

                    <li ID="PanelArchivosTitulares" class="nav nav-second-level" Visible="false" runat="server">
                        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoArchivoTitular.aspx?bp=1")); %>">Tipo Archivos Titulares</a>
                    </li>

                    <li ID="PanelTitularesPendientes" class="nav nav-second-level" Visible="false" runat="server">
                        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/TitularesPendientes/TitularesPendientes.aspx.")); %>">Titulares Pendientes de Creación</a>
                    </li>

                    </ul>
                </li>
           </ul>
         </li>
         <li ID="PanelUsuarios" Visible="false" runat="server">
            <a href="#"><i class="fa-fw"></i> Usuarios<span class="fa arrow"></span></a>
            <ul class="nav nav-second-level">
                <li ID="PanelAdminUsuarios" class="nav nav-second-level" Visible="false" runat="server">
                    <a href="<% Response.Write(ResolveClientUrl("~/Administrador/Usuarios/listUsuarios.aspx")); %>">Administración de Usuarios</a>
                </li>
                <li  ID="PanelAdminRoles" class="nav nav-second-level" Visible="false" runat="server">
                    <a href="<% Response.Write(ResolveClientUrl("~/Administrador/Usuarios/adminRolesPrivAplicacion.aspx")); %>">Administración de Roles</a>
                </li>

            </ul>
         </li>
         <li ID="PanelMantenedores" Visible="false" runat="server">
		    <a href="#"><i class="fa-fw"></i> Administrador de Mantenedores <span class="fa arrow"></span></a>
			<ul class="nav nav-second-level">
                <li ID="PanelMantenedorTransversal" class="nav nav-second-level" Visible="false" runat="server">
                    <a href="#">Mantenedor Documental<span class="fa arrow"></span></a>
                    <ul class="nav nav-third-level">
                        <li ID="PanelTipoDocumento" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/tipoDocumento.aspx")); %>">Tipo Documento</a>
                        </li>
                        <li ID="PanelSubrequerimiento" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/temaSubrequerimiento.aspx")); %>">Tema Subrequerimiento</a>
                        </li>
                        <li  ID="PanelAsocSubTipo" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/asocSubrequerimientoTipo.aspx")); %>">Asociación Subrequerimiento - Tipo Documento</a>
                        </li>
                        <li ID="PanelAsocSubResultado" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/AsocSubrequerimientoResultado.aspx")); %>">Asociación Subrequerimiento - Resultado</a>
                        </li>
                        <li ID="PanelResultado" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/resultado.aspx")); %>">Tipo Resultado</a>
                        </li>
                        <li ID="PanelPreferenciaRelocalizacion" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/preferenciaRelocalizacion.aspx")); %>">Preferencia de Relocalización</a>
                        </li>
                        <li ID="PanelPlazoDocumentos" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/plazosDocumentos.aspx")); %>">Plazos Documentos</a>
                        </li>
                        <li ID="PaneltipoSupeditado" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/tipoSupeditado.aspx")); %>">Tipo Supeditado</a>
                        </li>
                        <li ID="PanelEstadosUOT" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/estadosUOT.aspx")); %>">Estados UOT</a>
                        </li>
                        <li ID="PanelEquivalenciaEstadoUOT" class="nav nav-second-level" Visible="false" runat="server">
                            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/equivalenciaEstadoUOT.aspx")); %>">Equivalencia Estado UOT</a>
                        </li>
                    </ul>
                </li>

                

				<li ID="PanelMantenedorGeneral" class="nav nav-second-level" Visible="false" runat="server" >
                    <a href="#">Mantenedor General <span class="fa arrow"></span></a>
				    <ul class="nav nav-third-level">
                        
                        <!-- Antecedentes del Sector -->
                        <li ID="PanelAdministracionTerritorial" class="nav nav-third-level" Visible="true" runat="server">
                            <a href="#">Administracion Territorial<span class="fa arrow"></span></a>
                            <ul class="nav nav-fourth-level">
                                <li ID="PanelRegion" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/region.aspx")); %>">Región</a>
					            </li>
                                <li ID="PanelProvidencia" class="nav nav-fourth-level" Visible="false" runat="server">
					                <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/provincia.aspx")); %>">Provincia </a>
						        </li>
						        <li ID="PanelComuna" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/comuna.aspx")); %>">Comuna</a>
						        </li>
                                <li ID="PanelCapitania" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/capitaniaPuerto.aspx")); %>">Capitanía de Puerto</a>
						        </li>
                                <li ID="PanelTipoCuerpoAgua" class="nav nav-fourth-level" Visible="false" runat="server">
    					        	<a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoCuerpoAgua.aspx")); %>">Tipo Cuerpo Agua</a>
						        </li>
    					        <li ID="PanelCuerpoAgua" class="nav nav-fourth-level" Visible="false" runat="server">
						        	<a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/cuerpoDeAgua.aspx")); %>">Cuerpo Agua</a>
						        </li>
                            </ul>
                       </li>

                       <li ID="PanelAgrupaciondeconcesiones" class="nav nav-third-level" Visible="true" runat="server">
                            <a href="#">Agrupacion Concesiones<span class="fa arrow"></span></a>
                            <ul class="nav nav-fourth-level">
                                <li ID="PanelMacrozona" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/macrozona.aspx")); %>">Macrozona</a>
						        </li>
                                <li ID="PanelDescanso" class="nav nav-fourth-level" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/descasoACS.aspx")); %>">Descansos ACS</a>
						        </li>
                                <li ID="PanelBarrio" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/barrio.aspx")); %>">Barrio</a>
						        </li>
                                 <li ID="PanelTipoBarrio" class="nav nav-fourth-level" Visible="false" runat="server">
                                    <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoBarrio.aspx?bp=1")); %>">Tipo de Barrio</a>
						        </li>
                            </ul>
                       </li>
                        
                        <li ID="PanelCartografia" class="nav nav-third-level" Visible="true" runat="server">
                        <a href="#">Cartografía<span class="fa arrow"></span></a>
                            <ul class="nav nav-fourth-level">
                               <li  ID="PanelCarta" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/carta.aspx")); %>">Carta SHOA - IGM Plano</a>
						        </li> 
                                <li ID="PanelDatum" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/datum.aspx")); %>">Datum</a>
						        </li>
                                <li ID="PanelTipoHuso" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/huso.aspx")); %>">Huso</a>
						        </li>
                                <li ID="PanelVertice" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoVertice.aspx?bp=1")); %>">Nombre Vértice</a>
						        </li>
                                <li ID="PanelTipoUso" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoUso.aspx?bp=1")); %>">Tipo Uso del poligono</a>
						        </li>
                                <li ID="PanelCoodenadasOriginales" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoArchivoAntEspaciales.aspx")); %>">Tipo Archivo Coordenadas Originales</a>
						        </li>
                                <li ID="PanelArchivoAntedentes" class="nav nav-fourth-level" Visible="false" runat="server">
						        	<a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoArchivoAntTerreno.aspx")); %>">Tipo Archivo Coordenadas 14 Ter</a>
						        </li>
                                <li ID="PanelArchivoRegularizacion" class="nav nav-fourth-level" Visible="false" runat="server">
						        	<a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoArchivoRegularizacion.aspx")); %>">Tipo Archivo Regularización</a>
						        </li>
                                
                            </ul>
                        </li>

                        <li ID="PanelEspecie" class="nav nav-third-level" Visible="true" runat="server">
                        <a href="#">Especies<span class="fa arrow"></span></a>
                            <ul class="nav nav-fourth-level">
                                <li ID="PanelEspecieCultivo" class="nav nav-fourth-level" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/especie.aspx")); %>">Especie de Cultivo</a>
						        </li>
                                <li ID="PanelEspecieAlimento" class="nav nav-fourth-level" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/especieTipoAlimento.aspx")); %>">Asociacíon Especie - Alimento</a>
						        </li>
                                <li ID="PanelTipoCultivo" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoCultivo.aspx?bp=1")); %>">Tipo Cultivo</a>
						        </li>
                                <li ID="PanelUnidadEjemplar" class="nav nav-fourth-level" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/unidadEjemplar.aspx?bp=1")); %>">Unidad Ejemplar</a>
						        </li>
                                <li ID="PanelEtapaCultivo" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/etapaDeDesarrollo.aspx")); %>">Etapa de Cultivo</a>
						        </li>
                                <li ID="PanelPesoEjemplar" class="nav nav-fourth-level" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/rangoPesoEjemplar.aspx?bp=1")); %>">Rango Peso Ejemplar</a>
						        </li>
                                <li ID="PanelTipoAlimento" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoAlimento.aspx?bp=1")); %>">Tipo Alimento</a>
						        </li>
                                <li ID="PanelGrupoEspecie" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/grupoEspecie.aspx")); %>">Grupo Especie</a>
						        </li>
                            </ul>
                        </li>

                        <li ID="PanelEstructurasCultivo" class="nav nav-third-level" Visible="true" runat="server">
                        <a href="#">Estructuras de Cultivo<span class="fa arrow"></span></a>
                            <ul class="nav nav-fourth-level">
                                <li ID="PanelEstructuraTecnica" class="nav nav-fourth-level" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/estructuraTecnica.aspx")); %>">Tipo de Estructura</a>
						        </li>
                                <li ID="PanelFormaEstructura" class="nav nav-fourth-level" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/formaEstructura.aspx")); %>">Forma Estructura</a>
						        </li>
                                <li ID="PanelUnidadEstructura" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/unidadMedidaEstructuraTecnica.aspx")); %>">Unidad de Medida Estructura</a>
						        </li>
                                <li ID="PanelVolumenUnidadMedidaEstructura" class="nav nav-fourth-level" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/volumenUnidadMedida.aspx")); %>">Volumen Unidad de Medida Estructura</a>
						        </li>
                                 <li ID="PanelAnio" class="nav nav-fourth-level" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/anio.aspx?bp=1")); %>">Años</a>
						        </li>
                                <li ID="PanelCultivoAlgas" class="nav nav-fourth-level" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/metodoCultivo.aspx?bp=1")); %>">Método Cultivo de Algas</a>
						        </li>

                            </ul>
                        </li>

                        <li ID="PanelOtros" class="nav nav-third-level" Visible="true" runat="server">
                        <a href="#">Otros<span class="fa arrow"></span></a>
                            <ul class="nav nav-fourth-level">
                                <li ID="PanelTipoConcesion" class="nav nav-fourth-level" Visible="false" runat="server">
					                <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoConcesion.aspx")); %>">Tipo Concesión</a>
						        </li>
                                <li ID="PanelTipoCentroAcopio" class="nav nav-fourth-level" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoCentroAcopio.aspx")); %>">Tipo Centro Acopio</a>
						        </li>
                                <li ID="PanelTipoCentroFaenamiento" class="nav nav-fourth-level" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoCentroFaenamiento.aspx")); %>">Tipo Centro Faenamiento</a>
						        </li>
                                <li ID="PanelPlazoNominal" class="nav nav-fourth-level" Visible="false" runat="server">
							        <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/plazoNominal.aspx")); %>">Plazo Nominal</a>
						        </li>
                                <li ID="PanelOficinaZonal" class="nav nav-fourth-level" Visible="false" runat="server">
                                    <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/oficinaZonal.aspx")); %>">Direccion Zonal</a>
                                </li>
                                <li ID="PanelDireccionZonal" class="nav nav-fourth-level" Visible="false" runat="server">
                                    <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/direccionZonalRegion.aspx")); %>">Direccion Zonal Region</a>
                                </li>
                                <li ID="PanelConsultores" class="nav nav-fourth-level" Visible="false" runat="server">
                                    <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/consultores.aspx")); %>">Consultores</a>
                                </li>
                                <li ID="PanelEntidadesDeAnalisis" class="nav nav-fourth-level" Visible="false" runat="server">
                                    <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/entidadesDeAnalisis.aspx")); %>">Entidades De Analisis</a>
                                </li>
                                <li ID="PanelEntidadesDeMuestreo" class="nav nav-fourth-level" Visible="false" runat="server">
                                    <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/entidadesDeMuestreo.aspx")); %>">Entidades De Muestreo</a>
                                </li>
                                <li ID="PanelBarrioAsociaciones" class="nav nav-fourth-level" Visible="false" runat="server">
						            <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/barrioAsociaciones.aspx")); %>">Administrar Unidad Espacial o Solicitud en Barrio</a>
						        </li>
                                <li ID="Panelferiado" class="nav nav-fourth-level" Visible="false" runat="server">
                                    <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Transversales/feriado.aspx")); %>">Feriado</a>
                                </li>
                            </ul>
                        </li>

                        <!--Nuevos Mantenedores-->
                        
                    </ul>
				 </li>

                 <li ID="Bitacoracambios" class="nav nav-second-level" Visible="false" runat="server">
                     <a href="<% Response.Write(ResolveClientUrl("~/Administrador/BitacoraCambios/BitacoraCambios.aspx")); %>">Bitacora de Cambios</a>
                 </li>

                 <li ID="AdministradorCorreos" class="nav nav-second-level" Visible="false" runat="server">
                     <a href="<% Response.Write(ResolveClientUrl("~/AdministradorCorreos/administradorDeCorreos.aspx")); %>">Administrador de Correos</a>
                 </li>
                 <%--<li id="PanelTipoFondo" class="nav nav-second-level" visible="false" runat="server">
                    <a href="<% Response.Write(ResolveClientUrl("~/Mantenedores/Generales/tipoFondo.aspx")); %>"></a>
                 </li>--%>


                </ul>
			  </li>
            </ul>
         </div>
      </div>
    </nav>
 </div>
        
