<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesRelocalizacionRESA.Master" AutoEventWireup="true" 
CodeBehind="datosTramiteRelocalizacionRESA.aspx.cs"  Inherits="SubPesca.Solicitudes.RelocalizacionRESA.datosTramiteRelocalizacionRESA" Theme="admin_style" %>



<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register src="~/Solicitudes/Registrar/informacionSolicitud.ascx"                   tagname="informacionSolicitud"                 tagprefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_usuarios.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="FormularioIngresoSolicitante" ContentPlaceHolderID="rightbody" runat="server">

    <asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>

    <asp:ToolkitScriptManager ID="ToolkitScriptManagerSolicitante" runat="server" EnablePartialRendering="false"></asp:ToolkitScriptManager>

    <asp:UpdatePanel ID="UpdatePanelInformacionSolicitud" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
                    <asp:Panel ID="PanelInformacionSolicitud"  Visible="true" runat="server">
                        <uc2:informacionSolicitud ID="informacionSolicitud" runat="server" />
                    </asp:Panel>
            </ContentTemplate>
    </asp:UpdatePanel>
        
    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Datos del Trámite de Relocalización RESA</span>
            </td>
        </tr>
    </table>

    <hr style="width:100%;" />

    <asp:UpdatePanel ID="UpdatePanelDatosGeneral" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 
    <asp:Panel ID="PanelDatosGeneral"  Visible="true" runat="server">
    
    <fieldset>
        
        <legend>Datos Generales</legend>
        <br />


            <asp:UpdatePanel ID="UpdatePanelDatosInicio" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <asp:Panel ID="PanelDatosInicio" Visible="true" runat="server">
                    <table>
                    <tr>
                        <td class="col1"><span class="item">Nº PERT</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:Label ID="numPertSector" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1"><span class="item">Identificador del Sector</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:Label ID="identificadorDelSector" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1"><span class="item">Fecha Recepción</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:Label ID="fechaRecepcionTramiteSector" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1"><span class="item">Fecha de Ingreso a Trámite</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:Label ID="fechaIngresoTramiteSector" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1"><span class="item">Tipo de Relocalización</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:Label ID="tipoRelocalizacion" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1"><span class="item">Superficie Sector [ha.]</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:Label ID="superficieSector" runat="server"></asp:Label></td>
                    </tr>
                    </table>
                </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        
    </fieldset>
    <br />
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>

    


     <asp:UpdatePanel ID="UpdatePanelOrigen" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:Panel ID="PanelOrigen"  Visible="true" runat="server">

                    <fieldset>
                    <legend>Origenes</legend>
                    <br />
                  
                     
                        <asp:UpdatePanel ID="UpdatePanelGridOrigen" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>                        
                                <asp:Panel ID="PanelGridOrigen"  Visible="true" runat="server">
                
                                    <asp:GridView 
                                    ID="GridOrigen" 
                                    runat="server" 
                                    AutoGenerateColumns="False" 
                                    CellPadding="4" 
                                    ForeColor="#333333"
                                    TabIndex="1"
                                    GridLines="None" 
                                    CssClass="mGrid"
                                    PagerStyle-CssClass="pgr">
                        
                                    <Columns>

                                        <asp:TemplateField HeaderText="Centro de Origen">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                                <%# DataBinder.Eval(Container, "DataItem.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro")%>
                                                <%# DataBinder.Eval(Container, "DataItem.concesionOrigen.unidadEspacial.centrosDeCultivo.nombreCentro")%>
                                            </ItemTemplate>
                                        </asp:TemplateField> 

                                        <asp:TemplateField HeaderText="Nombre del Titular">
                                            <ItemTemplate>
                                                 <%# DataBinder.Eval(Container, "DataItem.concesionOrigen.DescripcionTitulares")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sector">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.concesionOrigen.DescripcionToponimios")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Región">
                                            <ItemTemplate>
                                                  <%# DataBinder.Eval(Container, "DataItem.concesionOrigen.region.descripcion")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Comuna">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.concesionOrigen.DescripcionComuna")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AC">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.concesionOrigen.barrio.barrio")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tipo de Centro">
                                            <ItemTemplate>
                                                  <%# DataBinder.Eval(Container, "DataItem.concesionOrigen.tipoUnidadEspacial.descripcion")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Superficie Total Centro [ha.]">
                                            <ItemTemplate>
                                                  <%# DataBinder.Eval(Container, "DataItem.concesionOrigen.superficieCalculada")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Superficie de Relocalización [ha.]">
                                            <ItemTemplate>
                                                  <%# DataBinder.Eval(Container, "DataItem.superficieRelocalizada")%>
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
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <br />



          <asp:UpdatePanel ID="UpdatePanelDestino" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:Panel ID="PanelDestino"  Visible="false" runat="server">

                    <fieldset>
                    <legend>Destino</legend>
                    <br />

       
                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="col1"><span class="item">Código del Centro Destino</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3"><asp:TextBox  ID="codigoSiepCentroDestino"   runat="server" MaxLength="40" Columns="40" ReadOnly="true"></asp:TextBox></td>
                        </tr>
                        </table>


                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="col1"><span class="item">Nombre del Titular</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3"><asp:TextBox  ID="nombreTitularDestino" runat="server" Columns="40" ReadOnly="true"></asp:TextBox></td>
                        </tr>
                        </table>

                        <br />

                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="col1"><span class="item">Sector</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3"><span class="item"><asp:Label ID="sectorDestino" runat="server" ReadOnly="true"></asp:Label></span></td>
                            <td class="col4"><span class="item">Comuna</span></td>
                            <td class="col5"><span class="item">:</span></td>
                            <td class="col6"><span class="item"><asp:Label ID="comunaDestino" runat="server" ReadOnly="true"></asp:Label></span></td>
                        </tr>
                        <tr>
                            <td class="col1"><span class="item">Región</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3"><span class="item"><asp:Label ID="regionDestino" runat="server" ReadOnly="true"></asp:Label></span></td>
                            <td class="col4"><span class="item">AC</span></td>
                            <td class="col5"><span class="item">:</span></td>
                            <td class="col6"><span class="item"><asp:Label ID="acDestino" runat="server" ReadOnly="true"></asp:Label></span></td>
                        </tr>
                        <tr>
                            <td class="col1"><span class="item">Tipo de Centro</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3"><span class="item"><asp:Label ID="tipoCentroDestino" runat="server" ReadOnly="true"></asp:Label></span></td>
                            <td class="col4"><span class="item">Superficie Total Centro</span></td>
                            <td class="col5"><span class="item">:</span></td>
                            <td class="col6"><span class="item"><asp:Label ID="superficieTotalCentroDestino" runat="server" ReadOnly="true"></asp:Label></span></td>
                        </tr>
                        </table>


                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <br />
  


   <asp:UpdatePanel ID="UpdatePanelTramitesPendientes" UpdateMode="Conditional" runat="server">
   <ContentTemplate> 
   <asp:Panel ID="PanelTramitesPendientes" Visible="false" runat="server">

   <fieldset>
        
       <legend>Trámites de Modificación Pendientes del mismo Código Origen</legend>
       <br />
       <asp:UpdatePanel ID="UpdatePanelSolicitudesPendientes" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
        
        <asp:Panel ID="PanelSolicitudesPendientes" Visible="true" runat="server">

        <asp:GridView 
           ID="GridViewSolicitudesPendientes" 
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           AllowPaging="True" PageSize="10" 
           AllowSorting="True"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%">
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
               <asp:TemplateField HeaderText="Tipo Modificación" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.DescripcionTipoModificacion")%>
                </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Nº Pert" SortExpression="numPert" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.numPert")%>
                </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Fecha Recepción" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.fechaRecepcion")%>
                </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Fecha Ingreso a Trámite" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.fechaIngresoTramite")%>
                </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Estado Tramitación" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                   <%# DataBinder.Eval(Container, "DataItem.estadoActual.descripcion")%>
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

   </asp:Panel>
   </ContentTemplate>
   </asp:UpdatePanel>

</asp:Content>
