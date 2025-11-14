<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="verDocumentoComponente.ascx.cs" Inherits="SubPesca.Solicitudes.Registrar.verDocumentoComponente" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="informacionSolicitud.ascx"                   tagname="informacionSolicitud"                 tagprefix="uc0" %>      


<asp:UpdatePanel ID="UpdatePanelInformacionSolicitud" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 
        <asp:Panel ID="PanelInformacionSolicitud"  Visible="true" runat="server">
            <uc0:informacionSolicitud ID="informacionSolicitud" runat="server" />
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>


<!-- Título de la página -->
<table class="formtop" cellpadding="0px" cellspacing="0px">
<tr>
<td align="left" valign="middle">
<span id="titulo_modulo">Ver Entrada/Salida</span>
</td>
<td align="right" valign="middle">
<asp:ImageButton ID="Volver" ImageUrl="~/App_Themes/admin_style/images/volver.png" OnClick="Cancelar_Click" Height="30px" ToolTip="Volver" runat="server" />
</td>
</tr>
</table>
<hr style="width:100%;" />

<asp:UpdatePanel ID="UpdatePanelFormulario" UpdateMode="Conditional" runat="server">
<ContentTemplate>    
    <asp:Panel ID="PanelFormulario"  Visible="false" runat="server">   


    <fieldset>
   

        <legend>Datos de Entrada/Salida</legend>
        <br />

              
        <asp:UpdatePanel ID="UpdatePanelFlujoDocumental" UpdateMode="Conditional" runat="server">
           <ContentTemplate>    
                <asp:Panel ID="PanelFlujoDocumental"  Visible="false" runat="server">          
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="FlujoDocumentalLiteral" runat="server" Text="<%$Resources:spanish.language,flujoDocumental%>" /></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="FlujoDocumental" AutoPostBack="true" runat="server" ReadOnly="true" Columns="40" /></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>




        <asp:UpdatePanel ID="UpdatePanelTipoSalida" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelTipoSalida"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="TipoSalidaLiteral" runat="server" Text="<%$Resources:spanish.language,tipoSalida%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="TipoSalida" AutoPostBack="true" runat="server" ReadOnly="true" Columns="40" /></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        
        <asp:UpdatePanel ID="UpdatePanelTipoEntrada" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelTipoEntrada" runat="server" Visible="false">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="TipoEntradaLiteral" runat="server" Text="<%$Resources:spanish.language,tipoEntrada%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="TipoEntrada" AutoPostBack="true" runat="server" ReadOnly="true"  Columns="40" /></td>
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
                        <td class="col3"><asp:TextBox ID="Origen" AutoPostBack="true" runat="server" ReadOnly="true" Columns="40" /></td>
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
                        <td class="col3"><asp:TextBox ID="Destinatario" AutoPostBack="true" runat="server" ReadOnly="true"  Columns="40" /></td>
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
                        <td class="col3"><asp:TextBox ID="TipoDocumento" AutoPostBack="true" runat="server" ReadOnly="true" Columns="40" /></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelDocumentoPrincipal" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelDocumentoPrincipal"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="LiteralDocumentoPrincipal" runat="server" /></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="DocumentoPrincipal" AutoPostBack="false" runat="server" ReadOnly="true" Columns="40" /></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>



       
        <asp:UpdatePanel ID="UpdatePanelNumeroRequerimiento" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelNumeroRequerimiento"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="NRequerimientoLiteral" runat="server" Text="<%$Resources:spanish.language,nRequerimiento%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="NRequerimiento" AutoPostBack="true" runat="server"  ReadOnly="true" Columns="40" /></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>




        <asp:UpdatePanel ID="UpdatePanelListaRequerimientos" UpdateMode="Conditional" runat="server">
            <ContentTemplate>  
                <asp:Panel ID="PanelListaRequerimientos"  Visible="false" runat="server">
                     
                     <br />
               
                     <asp:ListView 
                            ID="ListViewEntradaRespuestaRequerimiento" 
                            runat="server" 
                            DataKeyNames="idDocPestana"
                            >
                            <LayoutTemplate>
                                    <table runat="server" id="table1" border="0" cellpadding="2" cellspacing="2" width="100%" style="border: 1px solid Silver; width: 100%; border-collapse: collapse;">
                                    <tr style="background-color: SkyBlue; height: 30px;">
                                        <td align="center" style="width: 200px;"><b><asp:Literal ID="AmbitoDocumentoAsociadoLiteral" runat="server" Text="<%$Resources:spanish.language,ambitoDocumentoAsociado%>"/></b></td>
                                        <td align="center"><b><asp:Literal ID="TipoDocumentoAsociadoLiteral" runat="server" Text="<%$Resources:spanish.language,tipoDocumentoAsociado%>"/></b></td>
                                        <td align="center" style="width: 150px;"><b><asp:Literal ID="ResultadoDocumentoAsociadoLiteral" runat="server" Text="<%$Resources:spanish.language,resultadoDocumentoAsociado%>"/></b></td>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                 </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr2" runat="server">

                                    <td id="Td2" runat="server" align="center">
                                        <asp:Label ID="Label2" runat="server"  Text='<%#Eval("ambito.descripcion") %>' />
                                    </td>

                                    <td id="Td3" runat="server" align="center">
                                        <asp:HiddenField runat="server" ID="HiddenTipoId" Value='<%#Eval("tipo.id") %>' />
                                        <asp:Label ID="Label3" runat="server"  Text='<%#Eval("tipo.descripcion") %>' />
                                    </td>

                                    <td id="Td4" runat="server" align="center">
                                        <asp:Label ID="Label1" runat="server"  Text='<%#Eval("estadoResultadoResp.descripcion") %>' />
                                    </td>
                                </tr>

                            </ItemTemplate>
                        </asp:ListView>

                    </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        
         
        <asp:UpdatePanel ID="UpdatePanelAmbito" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelAmbito"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="AmbitoLiteral" runat="server" Text="<%$Resources:spanish.language,ambito%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="Ambito" AutoPostBack="true" runat="server"  ReadOnly="true" Columns="40" /></td>
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
                        <td class="col3"><asp:TextBox ID="Tipo" AutoPostBack="true" runat="server" ReadOnly="true" Columns="40" /></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>



          
        <asp:UpdatePanel ID="UpdatePanelDocumentosAmbito" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelDocumentosAmbito"  Visible="false" runat="server">


                        <asp:GridView 
                        ID="GridViewSalidaDocumentoAsociado" 
                        runat="server" 
                        AutoGenerateColumns="False" 
                        CellPadding="4" 
                        ForeColor="#333333"
                        GridLines="None" 
                        CssClass="mGrid"
                        PagerStyle-CssClass="pgr"
                        >
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="Ambito">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.ambito.descripcion") %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="Tema">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.tipo.descripcion") %>
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





        <asp:UpdatePanel ID="UpdatePanelNumero" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelNumero"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="NumeroLiteral" runat="server" Text="<%$Resources:spanish.language,numero%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="Numero" autocomplete="tel-extension" runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="8"></asp:TextBox></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>



        
        <asp:UpdatePanel ID="UpdatePanelFecha" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelFecha"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="FechaLiteral" runat="server" Text="<%$Resources:spanish.language,fecha%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            <asp:TextBox ID="Fecha" Columns="8"  runat="server" ReadOnly="true" />
                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="Fecha"
                                    Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                    CultureDateFormat="DMY" CultureDatePlaceholder="/">
                            </asp:MaskedEditExtender>
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
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            <asp:TextBox ID="NuevaFecha" Columns="8" Width="80px" runat="server"></asp:TextBox>
                            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="NuevaFecha"
                                Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                CultureDateFormat="DMY" CultureDatePlaceholder="/">
                            </asp:MaskedEditExtender>
                        </td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>



        <asp:UpdatePanel ID="UpdatePanelNumeroCI" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelNumeroCI"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="NumeroCILiteral" runat="server" Text="<%$Resources:spanish.language,numeroCI%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="NumeroCI"  autocomplete="tel-extension" runat="server"   MaxLength="8" ReadOnly="true" Columns="8" ></asp:TextBox></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        



        <asp:UpdatePanel ID="UpdatePanelFechaCI" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelFechaCI"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="FechaCILiteral" runat="server" Text="<%$Resources:spanish.language,fechaCI%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="FechaCI" Columns="8"  runat="server" ReadOnly="true" />
                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="FechaCI"
                                    Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                    CultureDateFormat="DMY" CultureDatePlaceholder="/">
                            </asp:MaskedEditExtender>
                        </td>
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
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="Resultado" AutoPostBack="true" runat="server" ReadOnly="true" Columns="40" /></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!--
         <asp:UpdatePanel ID="UpdatePanelResultadoSupeditado" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelResultadoSupeditado"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="ResultadoSupeditadoLiteral" runat="server" Text="<%$Resources:spanish.language,resultadoSupeditado%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="ResultadoSupeditado" AutoPostBack="true" runat="server" ReadOnly="true" Columns="40" /></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        -->

        <!-- Listado de Pendientes -->
        <asp:UpdatePanel ID="UpdatePanelGrillaPendientes" UpdateMode="Conditional" runat="server">
              <ContentTemplate>
              <asp:Panel ID="PanelGrillaPendientes"  Visible="true" runat="server">

              <asp:GridView 
               ID="GridPendientes"
               runat="server"
               AutoGenerateColumns="False" 
               CellPadding="4" 
               ForeColor="#333333" 
               GridLines="None"
               AllowSorting="True" 
               CssClass="mGrid"
               PagerStyle-CssClass="pgr"
               Width="100%">
               <RowStyle BackColor="#EFF3FB" />
               <Columns>
               
               <asp:TemplateField HeaderText="Tipo Pendiente" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.grupoSuspendido.nombreGrupoSuspend")%>
               </ItemTemplate>
               </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Estado" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.estadoVigencia.descripcion")%>
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

        <!-- Listado de Supeditados -->
        <asp:UpdatePanel ID="UpdatePanelGrillaSupeditados" UpdateMode="Conditional" runat="server">
              <ContentTemplate>
              <asp:Panel ID="PanelGrillaSupeditados"  Visible="true" runat="server">

              <asp:GridView 
               ID="GridSupeditados"
               runat="server"
               AutoGenerateColumns="False" 
               CellPadding="4" 
               ForeColor="#333333" 
               GridLines="None"
               AllowSorting="True" 
               CssClass="mGrid"
               PagerStyle-CssClass="pgr"
               Width="100%">
               <RowStyle BackColor="#EFF3FB" />
               <Columns>
               
               <asp:TemplateField HeaderText="Tipo Supeditado" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                        <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                        <%# DataBinder.Eval(Container, "DataItem.tipoSupeditado.descripcion")%>
               </ItemTemplate>
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Pert/Identificador de la Solicitud" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.solicitudConcesionDep.numPert")%>
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Resultado UOT" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                        
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Resultado SSP" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                        
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Observaciones" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.observaciones")%>
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


        <asp:UpdatePanel ID="UpdatePanelArchivoAdjunto" UpdateMode="Conditional" runat="server">
            <ContentTemplate>    
                <asp:Panel ID="PanelArchivo"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Archivo Adjunto</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="ArchivoAdjunto"  Columns="40" runat="server" ReadOnly="true"  /></td>
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


        
        <br />
    <fieldset>
                    
                  

                <asp:Panel ID="PanelGridRequerimiento"  Visible="true" runat="server">
                

                      <asp:GridView 
                        ID="GridSalida" 
                        runat="server" 
                        AutoGenerateColumns="False" 
                        CellPadding="4" 
                        ForeColor="#333333"
                        GridLines="None" 
                        CssClass="mGrid"
                        PageIndex="1"
                        OnRowDataBound="GridSalida_RowDataBound"
                        PagerStyle-CssClass="pgr"
                        OnRowCommand="GridSalida_RowCommand"
                        OnRowCreated="GridSalida_RowCreated" 
                        >
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>



                           <asp:TemplateField Visible="true">
                                <ItemTemplate>
                                    <asp:Label HeaderText="idDocGeneral" ID="hidden1" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idDocGeneral") %>'></asp:Label>
                                    <asp:Label HeaderText="rowspan" ID="hidden4" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "columnas") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField Visible="true">
                                <ItemTemplate>
                                    <asp:Label HeaderText="idPestana" ID="hidden2" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idPestana") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

         
                           
                            <asp:BoundField HeaderText="Ámbito"         DataField="nombrePestana" SortExpression="nombrePestana" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Tema"           DataField="nombreSubRequerimiento" SortExpression="nombreSubRequerimiento" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            
                            <asp:BoundField HeaderText="Tipo Documento" DataField="nombreTipoDoc" SortExpression="nombreTipoDoc" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Nº"             DataField="numero" SortExpression="numero"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Fecha"          DataField="fecha" SortExpression="fecha" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Destino"        DataField="nombreTipoTray" SortExpression="nombreTipoTray"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Archivo"        DataField="nombreArchivo" SortExpression="nombreArchivo" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />

                            

                             <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gVer" Visible="true" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocGeneral") %>' 
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                                    <asp:ImageButton ID="gDescargar" Visible="false" runat="server" CausesValidation="false" CommandName="Descargar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBinSC") %>' 
                                         ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" />


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
    

    </fieldset>
        <br />
        <br />
    <fieldset>
                    
                     

                <asp:Panel ID="Panel1"  Visible="true" runat="server">
                

                      <asp:GridView 
                        ID="GridEntrada" 
                        runat="server" 
                        AutoGenerateColumns="False" 
                        CellPadding="4" 
                        ForeColor="#333333"
                        GridLines="None" 
                        CssClass="mGrid"
                        PageIndex="1"
                        OnRowDataBound="GridEntrada_RowDataBound"
                        PagerStyle-CssClass="pgr"
                        OnRowCommand="GridEntrada_RowCommand"
                        OnRowCreated="GridEntrada_RowCreated" 
                        >
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>



                            <asp:TemplateField Visible="true">
                                <ItemTemplate>
                                    <asp:Label HeaderText="idDocGeneral" ID="hiddenEntradaIdDocGeneral" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idDocGeneral") %>'></asp:Label>
                                    <asp:Label HeaderText="idDocGeneralResp" ID="hiddenEntradaIdDocGeneralResp" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idDocGeneralResp") %>'></asp:Label>
                                    <asp:Label HeaderText="rowspan" ID="hiddenEntradaRowspan" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "columnas") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField Visible="true">
                                <ItemTemplate>
                                    <asp:Label HeaderText="idPestana" ID="hiddenIdPestana" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idPestana") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
         
                           
                            <asp:BoundField HeaderText="Ámbito"         DataField="nombrePestana" SortExpression="nombrePestana" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Tema"           DataField="nombreSubRequerimiento" SortExpression="nombreSubRequerimiento" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Origen"         DataField="nombreTipoTray" SortExpression="nombreTipoTray" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            
                            <asp:BoundField HeaderText="Tipo Documento" DataField="nombreTipoDoc" SortExpression="nombreTipoDoc" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Nº"             DataField="numero" SortExpression="numero"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Fecha"          DataField="fecha" SortExpression="fecha" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />

                            <asp:BoundField HeaderText="Nº C.I."        DataField="numeroCI" SortExpression="numeroCI"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Fecha C.I."     DataField="fechaCI" SortExpression="fechaCI" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                            
                            <asp:BoundField HeaderText="Archivo"        DataField="nombreArchivo" SortExpression="nombreArchivo" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />

                            <asp:BoundField HeaderText="Resultado"      DataField="nombreEstadoResultadoResp" SortExpression="nombreEstadoResultadoResp" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />

                            

                             <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gVer" Visible="true" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocGeneralResp") %>' 
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                                    <asp:ImageButton ID="gDescargar" Visible="false" runat="server" CausesValidation="false" CommandName="Descargar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBinSC") %>' 
                                         ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" />

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
 

    </fieldset>