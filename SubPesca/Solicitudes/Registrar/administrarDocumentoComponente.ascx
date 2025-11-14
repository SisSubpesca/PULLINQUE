<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="administrarDocumentoComponente.ascx.cs" Inherits="SubPesca.Solicitudes.Registrar.administrarDocumentoComponente" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register TagPrefix="uc0" src="informacionSolicitud.ascx"  tagname="informacionSolicitud"  %>          


<asp:UpdatePanel ID="UpdatePanelInformacionSolicitud" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 
        <asp:Panel ID="PanelInformacionSolicitud"  Visible="true" runat="server">
            <uc0:informacionSolicitud ID="informacionSolicitud" runat="server" />
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>


    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="right" valign="middle">
            <asp:ImageButton ID="Volver" ImageUrl="~/App_Themes/admin_style/images/volver.png" OnClick="Cancelar_Click" Height="30px" ToolTip="Volver" runat="server" />
        </td>
    </tr>
    </table>


<asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
    <ContentTemplate>    
        <asp:panel ID="Panel1" runat="server">
            <asp:ValidationSummary ID="ValidationSummaryFlujo" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList"  />
            <asp:ValidationSummary ID="ValidationSummaryEntrada" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" />
            <asp:ValidationSummary ID="ValidationSummarySalida" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" />

            <asp:ValidationSummary ID="ValidationSummaryGrupoRespuestaRequerimiento" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList"  />
            <asp:ValidationSummary ID="ValidationSummaryGrupoIngresoSinRequerimiento" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList"  />

            <asp:ValidationSummary ID="ValidationSummaryGrupoRequerimientoConRespuesta" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" />
            <asp:ValidationSummary ID="ValidationSummaryGrupoInformativo" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" />

            <asp:ValidationSummary ID="ValidationSummaryDocumentoAsociado" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList"  />
            <asp:ValidationSummary ID="ValidationSummaryGrupoSubmitIngreso" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ha ocurrido los siguientes errores:" DisplayMode="BulletList"  />

            <asp:ValidationSummary ID="ValidationSummaryErrores" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList"  ValidationGroup="grupo1" />
        </asp:panel>
    </ContentTemplate>
</asp:UpdatePanel>



<asp:UpdatePanel ID="UpdatePanelFormulario" UpdateMode="Conditional" runat="server">
    <ContentTemplate>   
        <asp:Panel ID="PanelFormulario" Visible="false" runat="server">

        

    <fieldset>


        <asp:UpdatePanel ID="UpdatePanelErroresSuperior" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   
            <asp:Panel ID="PanelErroresSuperior" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="ErroresSuperior" runat="server"></asp:Label>
                </div>
            </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        
         <table class="form" cellpadding="0px" cellspacing="0px" style="display:none;">
         <tr>
            <td>&nbsp;IdSolicitud: <asp:TextBox ID="IdSolicitud" runat="server"></asp:TextBox></td>
            <td>&nbsp;IdRequerimiento Mod: <asp:TextBox ID="idDocGeneral" runat="server"></asp:TextBox></td>
            <td>&nbsp;IdAchivo Mod: <asp:TextBox ID="idArchivo" runat="server"></asp:TextBox></td>
         </tr>
         </table>


      

   

        <legend>Modificar Documento</legend>
        <br />
        

   
        
    <asp:UpdatePanel ID="UpdatePanelFormularioIngreso" UpdateMode="Conditional" runat="server">
    <ContentTemplate>              
    <asp:Panel ID="PanelFormularioIngreso" Visible="false" runat="server">
        


        <asp:UpdatePanel ID="UpdatePanelFlujoDocumental" UpdateMode="Conditional" runat="server">
           <ContentTemplate>              
            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1"><span class="item">Flujo Documental</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                            
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="FlujoDocumental" AutoPostBack="true" runat="server" OnSelectedIndexChanged="FlujoDocumental_change"></asp:DropDownList>
                        <asp:RequiredFieldValidator id="RequiredFieldFlujoDocumentalGrupoDocumento" runat="server" ControlToValidate="FlujoDocumental"
                        ErrorMessage="Flujo Documental" Display="Static" InitialValue="0">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator id="RequiredFieldFlujoDocumental" runat="server" ControlToValidate="FlujoDocumental"  
                        ErrorMessage="Flujo Documental" Display="Static" InitialValue="0">*</asp:RequiredFieldValidator>
                    </ContentTemplate>
                    
                    </asp:UpdatePanel>

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
                        <td class="col1"><span class="item">Tipo Salida</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            
                            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="TipoSalida" AutoPostBack="true" runat="server" OnSelectedIndexChanged="TipoSalida_change"></asp:DropDownList>
                                <asp:RequiredFieldValidator id="RequiredFieldTipoSalidaGrupoDocumento" runat="server" ControlToValidate="TipoSalida"
                                ErrorMessage="Tipo Salida" Display="Static" InitialValue="0">*</asp:RequiredFieldValidator>

                                <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoSalida" runat="server" ControlToValidate="TipoSalida"  
                                ErrorMessage="Tipo Salida" Display="Static" InitialValue="0">*</asp:RequiredFieldValidator>

                            </ContentTemplate>
                          
                            </asp:UpdatePanel>

                        </td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental" EventName="SelectedIndexChanged" />
                
            </Triggers>

        </asp:UpdatePanel>


        
        <asp:UpdatePanel ID="UpdatePanelTipoEntrada" UpdateMode="Conditional" runat="server">

            <ContentTemplate>                        

                <asp:Panel ID="PanelTipoEntrada"  Visible="false" runat="server">

                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Tipo Entrada</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            
                            <asp:UpdatePanel ID="UpdatePanel17" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="TipoEntrada" AutoPostBack="true" runat="server" OnSelectedIndexChanged="TipoEntrada_change"></asp:DropDownList>
                                
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoEntrada" runat="server" ControlToValidate="TipoEntrada" 
                                ErrorMessage="Tipo Entrada" Display="Static" InitialValue="0">*</asp:RequiredFieldValidator>

                            </ContentTemplate>
                           
                            </asp:UpdatePanel>

                        </td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoSalida" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental" EventName="SelectedIndexChanged" />
               
            </Triggers>

        </asp:UpdatePanel>



            
      <asp:UpdatePanel ID="UpdatePanelOrigen" UpdateMode="Conditional" runat="server">

            <ContentTemplate>                        

                <asp:Panel ID="PanelOrigen"  Visible="false" runat="server">

                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="col1"><span class="item">Origen</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3">
                            
                                <asp:UpdatePanel ID="UpdatePanel20" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="Origen" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Origen_Change"></asp:DropDownList>
                                </ContentTemplate>

                              

                                </asp:UpdatePanel>

                            </td>
                        </tr>
                        </table>

                    
                </asp:Panel>
            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoSalida" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental" EventName="SelectedIndexChanged" />
              
            </Triggers>

        </asp:UpdatePanel>


         <asp:UpdatePanel ID="UpdatePanelDestinatario" UpdateMode="Conditional" runat="server">

            <ContentTemplate>                        

                <asp:Panel ID="PanelDestinatario"  Visible="false" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Destinatario</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                            
                        <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="Destinatario" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Destinatario_Change"></asp:DropDownList>
                        </ContentTemplate>
                       
                        </asp:UpdatePanel>

                    </td>
                </tr>
               </table>

            </asp:Panel>
            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoSalida" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental" EventName="SelectedIndexChanged" />
                
            </Triggers>

        </asp:UpdatePanel>





        <asp:UpdatePanel ID="UpdatePanelTipoDocumento" UpdateMode="Conditional" runat="server">

            <ContentTemplate>                        

                <asp:Panel ID="PanelTipoDocumento"  Visible="false" runat="server">

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Tipo Documento</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                            
                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="TipoDocumento" AutoPostBack="true" runat="server" OnSelectedIndexChanged="TipoDocumento_change"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Ambito" EventName="SelectedIndexChanged" />
                           
                        </Triggers>
                        </asp:UpdatePanel>

                    </td>
                </tr>
                </table>
                </asp:Panel>
            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoSalida" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Destinatario" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Origen" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental" EventName="SelectedIndexChanged" />
            
            </Triggers>

        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelDocumentoPrincipal" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelDocumentoPrincipal"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="LiteralDocumentoPrincipal" runat="server" /></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:DropDownList ID="DocumentoPrincipal" AutoPostBack="false" runat="server"></asp:DropDownList></td>
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
                            <td class="col1"><span class="item">Nº Requerimiento</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3">
                            
                                <asp:UpdatePanel ID="UpdatePanel27" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="NRequerimiento" AutoPostBack="true" runat="server" OnSelectedIndexChanged="NRequerimiento_change"></asp:DropDownList>
                                </ContentTemplate>

                              
                                
                               </asp:UpdatePanel>

                            </td>
                        </tr>
                        </table>

                    
                </asp:Panel>
            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoSalida" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoDocumento" EventName="SelectedIndexChanged" />
  
            </Triggers>

        </asp:UpdatePanel>




        <asp:UpdatePanel ID="UpdatePanel28" UpdateMode="Conditional" runat="server">

            <ContentTemplate>  
            
              
                <asp:Panel ID="PanelListaRequerimientos"  Visible="false" runat="server">
                     
                     <br />
               
                     <asp:ListView 
                            ID="ListViewEntradaRespuestaRequerimiento" 
                            runat="server" 
                            DataKeyNames="idDocPestana"
                            OnItemDataBound="ListViewEntradaRespuestaRequerimiento_ItemDataBound"
                            >
                            <LayoutTemplate>
                                    <table runat="server" id="table1" border="0" cellpadding="2" cellspacing="2" width="100%" style="border: 1px solid Silver; width: 100%; border-collapse: collapse;">
                                    <tr style="background-color: SkyBlue; height: 30px;">
                                        <td align="center" style="width: 100px;"><b>Seleccione</b></td>
                                        <td align="center" style="width: 200px;"><b>Ambito</b></td>
                                        <td align="center"><b>Tema</b></td>
                                        <td align="center" style="width: 150px;"><b>Resultado</b></td>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                 </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr2" runat="server">
                                    <td id="Td1" runat="server" align="center">
                                        <asp:HiddenField runat="server" ID="HiddenIdDocGeneralResp" Value='<%#Eval("idDocGeneralResp") %>' />
                                        <asp:CheckBox ID="chkSeleccionado" runat="server" AutoPostBack="true" />            
                                    </td>

                                    <td id="Td2" runat="server" align="center">
                                        <asp:HiddenField runat="server" ID="HiddenIdDocPestana" Value='<%#Eval("idDocPestana") %>' />
                                        <asp:Label ID="Label2" runat="server"  Text='<%#Eval("ambito.descripcion") %>' />
                                    </td>

                                    <td id="Td3" runat="server" align="center">
                                        <asp:HiddenField runat="server" ID="HiddenAmbitoId" Value='<%#Eval("ambito.id") %>' />
                                        <asp:HiddenField runat="server" ID="HiddenTipoId" Value='<%#Eval("tipo.id") %>' />
                                        <asp:HiddenField runat="server" ID="HiddenSeccionId" Value='<%#Eval("seccion.id") %>' />
                                        <asp:Label ID="Label3" runat="server"  Text='<%#Eval("tipo.descripcion") %>' />
                                    </td>

                                    <td id="Td4" runat="server" align="center">
                                        <asp:HiddenField runat="server" ID="HiddenIdResultado" Value='<%#Eval("estadoResultadoResp.id") %>' />
                                        <asp:DropDownList ID="AmbitoTipoResultado" AutoPostBack="true" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>

                            </ItemTemplate>
                        </asp:ListView>

                    </asp:Panel>
             

            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="NRequerimiento"    EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoSalida"        EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada"       EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental"   EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoDocumento" EventName="SelectedIndexChanged" />

            </Triggers>


        </asp:UpdatePanel>




       

        
         
         <asp:UpdatePanel ID="UpdatePanelAmbito" UpdateMode="Conditional" runat="server">

            <ContentTemplate>                        

                <asp:Panel ID="PanelAmbito"  Visible="false" runat="server">

                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="col1"><span class="item">Ámbito</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3">
                            
                                <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="Ambito" AutoPostBack="true" runat="server"  OnSelectedIndexChanged="Ambito_change"></asp:DropDownList>
                                    
                                    <asp:RequiredFieldValidator id="RequiredFieldAmbitoGrupoDocumento" runat="server" ControlToValidate="Ambito" 
                                    ErrorMessage="Ambito" Display="Static" InitialValue="0">*</asp:RequiredFieldValidator>
                                    
                                </ContentTemplate>

                         

                               </asp:UpdatePanel>

                            </td>
                        </tr>
                        </table>

                    
                </asp:Panel>
            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoSalida" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoDocumento" EventName="SelectedIndexChanged" />
                
            </Triggers>

        </asp:UpdatePanel>


         
         <asp:UpdatePanel ID="UpdatePanelTipo" UpdateMode="Conditional" runat="server">

            <ContentTemplate>                        

                <asp:Panel ID="PanelTipo"  Visible="false" runat="server">

                    
                        <table class="form" cellpadding="0px" cellspacing="0px">
                         <tr>
                            <td class="col1"><span class="item">Tema</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3">
                            
                                <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="Tipo" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Tipo_change"></asp:DropDownList>
                                    
                                    
                                    <asp:RequiredFieldValidator id="RequiredFieldTipoGrupoDocumento" runat="server" ControlToValidate="Tipo" 
                                    ErrorMessage="Tema" Display="Static" InitialValue="0">*</asp:RequiredFieldValidator>

                                </ContentTemplate>

                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Ambito" EventName="SelectedIndexChanged" />
                                   
                                </Triggers>

                               </asp:UpdatePanel>

                            </td>
                        </tr>
                        </table>
                </asp:Panel>
            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoSalida" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoDocumento" EventName="SelectedIndexChanged" />
               
            </Triggers>


        </asp:UpdatePanel>



          
         <asp:UpdatePanel ID="UpdatePanelDocumentosAmbito" UpdateMode="Conditional" runat="server">

            <ContentTemplate>                        

                <asp:Panel ID="PanelDocumentosAmbito"  Visible="false" runat="server">

                    
                        <table class="form" cellpadding="0px" cellspacing="0px">
                         <tr>
                            <td class="col1"><span class="item"></span></td>
                            <td class="col2"><span class="item"></span></td>
                            <td class="col3"><span class="item">Agregar Documento Asociado </span> <asp:ImageButton ID="btnAgregarDocumentoAsociado" runat="server" src="../../App_Themes/admin_style/images/add.png" onclick="GridViewSalidaDocumentoAsociado_Guardar" CausesValidation="true" /></td>
                        </tr>
                        </table>


                        

                        <asp:GridView 
                        ID="GridViewSalidaDocumentoAsociado" 
                        runat="server" 
                        AutoGenerateColumns="False" 
                        CellPadding="4" 
                        ForeColor="#333333"
                        GridLines="None" 
                        CssClass="mGrid"
                        OnRowDataBound="GridViewSalidaDocumentoAsociado_RowDataBound"
                        PagerStyle-CssClass="pgr"
                        OnRowCommand="GridViewSalidaDocumentoAsociado_RowCommand"
                        >
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="Ambito">
                                <ItemTemplate>
                                <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                <%# DataBinder.Eval(Container, "DataItem.ambito.descripcion") %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="Tema">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.tipo.descripcion") %>
                                </ItemTemplate>
                            </asp:TemplateField> 

                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "index") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
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

                


            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoSalida"            EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada"           EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental"       EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoDocumento"         EventName="SelectedIndexChanged" />
                
            </Triggers>


        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelMensajePlanos14TER" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:Panel ID="PanelMensajePlanos14TER"  Visible="false" runat="server">
                 <table class="form" cellpadding="0px" cellspacing="0px">
                 <tr>
                 <td class="col1"><span class="item"></span></td>
                 <td class="col2"><span class="item"></span></td>
                 <td class="col3">
                 <div class="msgGrilla_div2">
                    Al visar una carta ambiental se debe incorporar la visación de planos 14 TER.
                 </div>
                 </td>
                 </tr>
                 </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanelNumero" UpdateMode="Conditional" runat="server">

            <ContentTemplate>                        

                <asp:Panel ID="PanelNumero"  Visible="false" runat="server">

                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Número</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            
                            <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                               <asp:TextBox ID="Numero" autocomplete="tel-extension" AutoPostBack="true" runat="server" MaxLength="12"></asp:TextBox>&nbsp;<asp:Label ID="RequeridoNumero" runat="server"></asp:Label>
                            </ContentTemplate>
                            </asp:UpdatePanel>

                        </td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoSalida"        EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada"       EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental"   EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoDocumento"     EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Tipo"              EventName="SelectedIndexChanged" />

               
            </Triggers>

        </asp:UpdatePanel>



        
        <asp:UpdatePanel ID="UpdatePanelFecha" UpdateMode="Conditional" runat="server">

            <ContentTemplate>                        

                <asp:Panel ID="PanelFecha"  Visible="false" runat="server">

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Fecha</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">

                        <asp:UpdatePanel ID="UpdatePanel29" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>

                            <div class="calendario">
                                <div class="calendario_textbox">               
                                    <asp:TextBox ID="Fecha" Columns="8" Width="80px" runat="server"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="Fecha"
                                        Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                    </asp:MaskedEditExtender>

                                </div>
                                <div class="calendario_icono">
                                    <asp:Image src="../../App_Themes/admin_style/images/calendar.png" id="fechaImgDinamica" alt="Calendario" runat="server" style="vertical-align: middle" />&nbsp;<asp:Label ID="RequeridoFecha" runat="server"></asp:Label>
                                </div>

                                <asp:CustomValidator ID="ccFecha" ControlToValidate="Fecha" ErrorMessage="Fecha" ClientValidationFunction="validaFechaDDMMAAAA" 
                                    Display="static" Font-Size="10" runat="server">Fecha no válida</asp:CustomValidator>

                            </div>


                        </ContentTemplate>
                       

                        </asp:UpdatePanel>

                        
                    </td>
                </tr>
                </table>

                </asp:Panel>
            </ContentTemplate>


            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoSalida"        EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada"       EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental"   EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoDocumento"     EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Tipo"              EventName="SelectedIndexChanged" />

               
            </Triggers>

        </asp:UpdatePanel>



        <asp:UpdatePanel ID="UpdatePanelNuevaFecha" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelNuevaFecha"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="NuevaFechaLiteral" runat="server" Text="<%$Resources:spanish.language,nuevafecha%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">

                            <div class="calendario">
                                <div class="calendario_textbox">               
                                    <asp:TextBox ID="NuevaFecha" Columns="8" Width="80px" runat="server"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="NuevaFecha"
                                        Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                    </asp:MaskedEditExtender>

                                </div>
                                <div class="calendario_icono">
                                    <asp:Image src="../../App_Themes/admin_style/images/calendar.png" id="nuevaFechaImgDinamica" alt="Calendario" runat="server" style="vertical-align: middle" />
                                </div>

                                <asp:CustomValidator ID="ccNuevaFecha" ControlToValidate="NuevaFecha" ErrorMessage="Fecha" ClientValidationFunction="validaFechaDDMMAAAA" 
                                    Display="static" Font-Size="10" runat="server"><asp:Literal ID="NuevaFechaNoValidaLiteral" runat="server" Text="<%$Resources:spanish.language,fechaNoValida%>"/></asp:CustomValidator></div></td></tr></table></asp:Panel></ContentTemplate><Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoSalida"        EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada"       EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental"   EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoDocumento"     EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Tipo"              EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>



        <asp:UpdatePanel ID="UpdatePanelNumeroCI" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelNumeroCI"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="NumeroCILiteral" runat="server" Text="<%$Resources:spanish.language,numeroCI%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td><td class="col3"><asp:TextBox ID="NumeroCI"  autocomplete="tel-extension" runat="server"  onKeyUp="return onlyNumeric(this)"  MaxLength="8"  AutoPostBack="true" ontextchanged="NumeroCI_TextChanged"></asp:TextBox>&nbsp;<asp:Label ID="RequeridoNumeroCI" runat="server"></asp:Label>&nbsp;<asp:Literal runat="server" ID="NumeroCIMensaje" Text=""></asp:Literal></td></tr></table></asp:Panel></ContentTemplate><Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoSalida"        EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada"       EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental"   EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoDocumento"     EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Tipo"              EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        




        <asp:UpdatePanel ID="UpdatePanelFechaCI" UpdateMode="Conditional" runat="server">

            <ContentTemplate>                        

                <asp:Panel ID="PanelFechaCI"  Visible="false" runat="server">

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Fecha C.I.</span></td><td class="col2"><span class="item">:</span></td><td class="col3">

                    
                        <asp:UpdatePanel ID="UpdatePanel30" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div class="calendario">
                                    <div class="calendario_textbox">               
                                    <asp:TextBox ID="FechaCI" Columns="8" Width="80px" runat="server"></asp:TextBox><asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="FechaCI"
                                        Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                    </asp:MaskedEditExtender></div><div class="calendario_icono">
                                    <asp:Image src="../../App_Themes/admin_style/images/calendar.png" id="fechaCIImgDinamica" alt="Calendario" runat="server" style="vertical-align: middle" />&nbsp;<asp:Label ID="RequeridoFechaCI" runat="server"></asp:Label></div><asp:CustomValidator ID="cvFechaCI" ControlToValidate="FechaCI" ErrorMessage="Fecha" ClientValidationFunction="validaFechaDDMMAAAA" 
                                    Display="static" Font-Size="10" runat="server">Fecha no válida</asp:CustomValidator></div></ContentTemplate></asp:UpdatePanel></td></tr></table></asp:Panel></ContentTemplate><Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoSalida"        EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada"       EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental"   EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoDocumento"     EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Tipo"              EventName="SelectedIndexChanged" />

               
            </Triggers>

        </asp:UpdatePanel>



         <asp:UpdatePanel ID="UpdatePanelResultado" UpdateMode="Conditional" runat="server">

            <ContentTemplate>                        

                <asp:Panel ID="PanelResultado"  Visible="false" runat="server">

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Resultado</span></td><td class="col2"><span class="item">:</span></td><td class="col3">
                            
                        <asp:UpdatePanel ID="UpdatePanel25" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="Resultado" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Resultado_change"></asp:DropDownList> *</ContentTemplate></asp:UpdatePanel></td></tr></table></asp:Panel></ContentTemplate><Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoSalida"        EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada"       EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental"   EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoDocumento"     EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Tipo"              EventName="SelectedIndexChanged" />
              
            </Triggers>

        </asp:UpdatePanel>
                
        <asp:UpdatePanel ID="UpdatePanelMensajeAsociarPendiente" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   
            <asp:Panel ID="PanelMensajeAsociarPendiente" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="MensajeAsociarPendiente" runat="server"></asp:Label></div></asp:Panel></ContentTemplate></asp:UpdatePanel><asp:UpdatePanel ID="UpdatePanelPendiente" UpdateMode="Conditional" runat="server">
           <ContentTemplate>   
            
            <asp:Panel ID="PanelPendiente" Visible="false" runat="server">
            
            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1"><span class="item">Tipo Pendiente</span></td><td class="col2"><span class="item">:</span></td><td class="col3">
                    
                    
                    <asp:UpdatePanel ID="UpdatePanelTipoPendiente" UpdateMode="Conditional" runat="server">
                    <ContentTemplate> 
                        
                        <asp:Panel ID="PanelTipoPendiente"  Visible="true" runat="server">
                            <asp:DropDownList ID="TipoPendiente" runat="server"></asp:DropDownList>
                        </asp:Panel>
                    
                    </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:ImageButton ID="agregarGrupoSuspendido" 
                                runat="server" ImageUrl="~/App_Themes/admin_style/images/editar_nombre.jpg" 
                                AlternateText="Agregar Grupo Suspendido" ToolTip="Agregar Grupo Suspendido" 
                                style="width: 20px" Height="20px" Visible = "true" 
                        OnClientClick="javascript:abre_dialogo('agregarGrupoSusp','4')"  />
                </td>
                
            </tr>
            <tr>
                <td></td>
                <td colspan="2">
                    <asp:ImageButton ID="GuardarPendiente" Visible="true" runat="server" 
                        ImageUrl="../../App_Themes/admin_style/images/add.png" Height="20px" 
                        AlternateText="Agregar Unidad de Dependencia Pendiente" 
                        ToolTip="Agregar Unidad de Dependencia Pendiente" onclick="GuardarPendiente_Click" CausesValidation="true"
                     /><span class="item"> Guardar Tipo Pendiente</span> </td></tr></table><asp:UpdatePanel ID="UpdatePanelGrillaPendientes" UpdateMode="Conditional" runat="server">
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
               OnRowDataBound="GridPendientes_RowDataBound"
               OnRowCommand="GridPendientes_RowCommand"
               PagerStyle-CssClass="pgr"
               Width="100%">
               <RowStyle BackColor="#EFF3FB" />
               <Columns>
               
               <asp:TemplateField HeaderText="Tipo Pendiente" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                        <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                        <%# DataBinder.Eval(Container, "DataItem.grupoSuspendido.nombreGrupoSuspend")%>
               </ItemTemplate>
               </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Estado" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                    <asp:HiddenField ID="idEstadoVigencia" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.estadoVigencia.id") %>' />
                    <%# DataBinder.Eval(Container, "DataItem.estadoVigencia.descripcion")%>

                </ItemTemplate>
               </asp:TemplateField>
                             
               <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "index") %>'
                     ImageUrl="../../App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />

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

           </asp:Panel>
           
           </ContentTemplate>
           </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanelMensajeAsociarSup" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   
            <asp:Panel ID="PanelMensajeAsociarSup" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="MensajeAsociarSup" runat="server"></asp:Label></div></asp:Panel></ContentTemplate></asp:UpdatePanel><asp:UpdatePanel ID="UpdatePanelSupeditado" UpdateMode="Conditional" runat="server">
           <ContentTemplate>       
            <asp:Panel ID="PanelSupeditado" Visible="false" runat="server">

            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1"><span class="item">Tipo de Supeditado</span></td><td class="col2"><span class="item">:</span></td><td class="col3">
                    <asp:DropDownList ID="TipoSupeditado" runat="server" 
                        onselectedindexchanged="TipoSupeditado_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                   <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoSupeditado" runat="server" ControlToValidate="TipoSupeditado"  ValidationGroup="grupo1"
                        ErrorMessage="Tipo de Supeditado" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator></td></tr><asp:Panel ID="PanelPert" Visible="false" runat="server">
            <tr>
                <td class="col1"><span class="item">Pert/Identificador de la Solicitud</span></td><td class="col2"><span class="item">:</span></td><td class="col3">
                    <asp:TextBox ID="Pert" runat="server"></asp:TextBox></td></tr></asp:Panel><asp:Panel ID="PanelObservaciones" Visible="false" runat="server">
            <tr>
                <td class="col1"><span class="item">Observaciones</span></td><td class="col2"><span class="item">:</span></td><td class="col3">
                    <asp:TextBox  ID="Observaciones" TextMode="multiline" Columns="50" Rows="5" runat="server" AutoPostBack="false"></asp:TextBox></td></tr></asp:Panel><tr>
                <td></td>
                <td colspan="2">
                    <asp:ImageButton ID="GuardarSupeditado" Visible="true" runat="server" 
                        ImageUrl="../../App_Themes/admin_style/images/add.png" Height="20px" 
                        AlternateText="Guardar Supeditado" ToolTip="Guardar Supeditado" 
                        onclick="GuardarSupeditado_Click" CausesValidation="true"/><span class="item"> Guardar Tipo Supeditado</span> </td></tr></table><asp:UpdatePanel ID="UpdatePanelGrillaSupeditados" UpdateMode="Conditional" runat="server">
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
               OnRowDataBound="GridSupeditados_RowDataBound"
               OnRowCommand="GridSupeditados_RowCommand"
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
                        <%# DataBinder.Eval(Container, "DataItem.solicitudConcesionDep.itc")%>
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Resultado SSP" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.solicitudConcesionDep.resolucionSSP")%>
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Observaciones" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.observaciones")%>
               </ItemTemplate>
               </asp:TemplateField>
                             
               <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "index") %>'
                     ImageUrl="../../App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />

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

           </asp:Panel>
           </ContentTemplate>
           </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelArchivo" UpdateMode="Conditional" runat="server">


            <ContentTemplate>                        
            <asp:Panel ID="PanelArchivo"  Visible="false" runat="server">

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Archivo Adjunto</span></td><td class="col2"><span class="item">:</span></td><td class="col3">
                        <asp:UpdatePanel ID="UpdatePanel15" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:FileUpload ID="ArchivoAdjunto" MaxLength="40" Width="200px" runat="server"></asp:FileUpload>&nbsp;<asp:Label ID="RequeridoArchivoAdjunto" runat="server"></asp:Label><asp:RegularExpressionValidator ID="REGEXFileUploadLogo" runat="server" ErrorMessage="Formato Archivo Incorrecto" ControlToValidate="ArchivoAdjunto" ValidationExpression= "(.*).(.doc|.DOC|.pdf|.PDF|.docx|.DOCX|.xls|.xlsx|.XLS|.XLSX|.dwg|.DWG|.dxf|.DXF|.jpg|.JPG|.jpeg|.JPEG)$" />

                            <asp:Label ID="NombreArchivo" runat="server"></asp:Label></ContentTemplate><Triggers>
                            <asp:PostBackTrigger ControlID="Guardar"/>
                        </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                </table>
            </asp:Panel>
            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TipoSalida"        EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoEntrada"       EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlujoDocumental"   EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="TipoDocumento"     EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Tipo"              EventName="SelectedIndexChanged" />

                
                <asp:PostBackTrigger ControlID="Guardar"/>
                
            </Triggers>

        </asp:UpdatePanel>




               
        <table class="form" cellpadding="0px" cellspacing="0px">   
        <tr>
            <td class="col1"></td>
            <td class="col2"></td>
            <td class="col3"><asp:Button ID="Guardar" runat="server" Text="Modificar"  CausesValidation="true" onclick="Guardar_Click" /></td>
        </tr>
        </table>


        <br />





    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>


    </fieldset>


                                              </asp:Panel></ContentTemplate></asp:UpdatePanel><asp:UpdatePanel ID="UpdatePanelMensajeResolSSFFAA" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <asp:Panel ID="PanelMensajeResolSSFFAA"  Visible="false" runat="server">
                 <table class="form" cellpadding="0px" cellspacing="0px">
                 <tr>
                 <td class="col1"><span class="item"></span></td>
                 <td class="col2"><span class="item"></span></td>
                 <td class="col3">
                 <div class="msgGrilla_div2">
                    Se ha ingresado la resolución de SSFFAA aprueba, siendo que no  posee resol SSP que aprueba. </div></td></tr></table></asp:Panel></ContentTemplate></asp:UpdatePanel><asp:UpdatePanel ID="UpdatePanelMensajeITDACAprueba" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:Panel ID="PanelMensajePlanosITDACAprueba"  Visible="false" runat="server">
                 <table class="form" cellpadding="0px" cellspacing="0px">
                 <tr>
                 <td class="col1"><span class="item"></span></td>
                 <td class="col2"><span class="item"></span></td>
                 <td class="col3">
                 <div class="msgGrilla_div2">
                    Se ha ingresado IT DAC aprueba, siendo que no tiene documento, con el resultado aprueba anteriores para el ingreso de este documento. </div></td></tr></table></asp:Panel></ContentTemplate></asp:UpdatePanel><asp:UpdatePanel ID="UpdatePanelMensajeResolSSP" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:Panel ID="PanelMensajeResolSSP"  Visible="false" runat="server">
                 <table class="form" cellpadding="0px" cellspacing="0px">
                 <tr>
                 <td class="col1"><span class="item"></span></td>
                 <td class="col2"><span class="item"></span></td>
                 <td class="col3">
                 <div class="msgGrilla_div2">
                    Se ha ingresado la resolución de SSP aprueba, siendo que no  posee IT DAC que aprueba. </div></td></tr></table></asp:Panel></ContentTemplate></asp:UpdatePanel><asp:UpdatePanel ID="UpdatePanelErroresSalida" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   
            <asp:Panel ID="Content_msgErroresGrillaSalida" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgErroresGrillaSalida" runat="server"></asp:Label></div></asp:Panel></ContentTemplate></asp:UpdatePanel><asp:UpdatePanel ID="UpdatePanelErroresInferior" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   
            <asp:Panel ID="PanelErroresInferior" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="ErroresInferior" runat="server"></asp:Label></div></asp:Panel></ContentTemplate></asp:UpdatePanel><fieldset>

            <asp:Panel ID="PanelGridSalida"  Visible="true" runat="server">
                

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
                                <asp:Label HeaderText="idDocGeneral" ID="hidden1" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idDocGeneral") %>'></asp:Label><asp:Label HeaderText="rowspan" ID="hidden4" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "columnas") %>'></asp:Label><asp:Label HeaderText="idEstadoVigencia" ID="hidden3" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idEstadoReqSalida")%>'></asp:Label><asp:Label HeaderText="VisacionMasiva" ID="hiddenVisacionMasiva" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "visacionMasiva")%>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField Visible="true">
                            <ItemTemplate>
                                <asp:Label HeaderText="idPestana" ID="hidden2" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idPestana") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="Ámbito"         DataField="nombrePestana" SortExpression="nombrePestana" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Tema"           DataField="nombreSubRequerimiento" SortExpression="nombreSubRequerimiento" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            
                        <asp:BoundField HeaderText="Tipo Documento" DataField="nombreTipoDoc" SortExpression="nombreTipoDoc" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Nº"             DataField="numero" SortExpression="numero"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Fecha"          DataField="fecha" SortExpression="fecha" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                        <asp:BoundField HeaderText="Destino"        DataField="nombreTipoTray" SortExpression="nombreTipoTray"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Archivo"        DataField="nombreArchivo" SortExpression="nombreArchivo" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />

                            

                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                            <ItemTemplate>

                                <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocGeneral")  + ";" + DataBinder.Eval(Container.DataItem, "idPestana")  + ";" + DataBinder.Eval(Container.DataItem, "idArchivoBinSC") %>' 
                                        ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />

                                <asp:ImageButton ID="gDescargar" Visible="false" runat="server" CausesValidation="false" CommandName="Descargar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocGeneral")  + ";" + DataBinder.Eval(Container.DataItem, "idPestana")  + ";" + DataBinder.Eval(Container.DataItem, "idArchivoBinSC") %>' 
                                        ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" />

                                <asp:ImageButton ID="gNoVigente" Visible="false" runat="server" CausesValidation="false" CommandName="NoVigente" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocGeneral")  + ";" + DataBinder.Eval(Container.DataItem, "idPestana")  + ";" + DataBinder.Eval(Container.DataItem, "idArchivoBinSC") %>' 
                                    ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Pasar a No Vigente" ToolTip="Pasar a No Vigente" />

                                <asp:ImageButton ID="gVigente" Visible="false" runat="server" CausesValidation="false" CommandName="Vigente" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocGeneral")  + ";" + DataBinder.Eval(Container.DataItem, "idPestana")  + ";" + DataBinder.Eval(Container.DataItem, "idArchivoBinSC") %>' 
                                        ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Pasar a Vigente" ToolTip="Pasar a Vigente" />

                                <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocGeneral")  + ";" + DataBinder.Eval(Container.DataItem, "idPestana")  + ";" + DataBinder.Eval(Container.DataItem, "idArchivoBinSC") %>' 
                                        ImageUrl="../../App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />


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



        <asp:UpdatePanel ID="UpdatePanelErroresEntrada" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   
            <asp:Panel ID="Content_msgErroresGrillaEntrada" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgErroresGrillaEntrada" runat="server"></asp:Label></div></asp:Panel></ContentTemplate></asp:UpdatePanel><asp:Panel ID="PanelGridEntrada"  Visible="true" runat="server">
                

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
                            <asp:Label HeaderText="idDocGeneral" ID="hiddenEntradaIdDocGeneral" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idDocGeneral") %>'></asp:Label><asp:Label HeaderText="idDocGeneralResp" ID="hiddenEntradaIdDocGeneralResp" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idDocGeneralResp") %>'></asp:Label><asp:Label HeaderText="rowspan" ID="hiddenEntradaRowspan" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "columnas") %>'></asp:Label><asp:Label HeaderText="idEstadoVigencia" ID="hidden3" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idEstadoVigencia")%>'></asp:Label><asp:Label HeaderText="VisacionMasiva" ID="hiddenVisacionMasiva" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "visacionMasiva")%>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField Visible="true">
                        <ItemTemplate>
                            <asp:Label HeaderText="idPestana" ID="hiddenIdPestana" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idPestana") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="Ámbito"         DataField="nombrePestana" SortExpression="nombrePestana" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Tema"           DataField="nombreSubRequerimiento" SortExpression="nombreSubRequerimiento" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Origen"         DataField="nombreTipoTray" SortExpression="nombreTipoTray" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            
                    <asp:BoundField HeaderText="Tipo Documento" DataField="nombreTipoDoc" SortExpression="nombreTipoDoc" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Nº"             DataField="numero" SortExpression="numero"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Fecha"          DataField="fecha" SortExpression="fecha" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:d}" />

                    <asp:BoundField HeaderText="Nº C.I."        DataField="numeroCI" SortExpression="numeroCI"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Fecha C.I."     DataField="fechaCI" SortExpression="fechaCI" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                            
                    <asp:BoundField HeaderText="Archivo"        DataField="nombreArchivo" SortExpression="nombreArchivo" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />

                    <asp:BoundField HeaderText="Resultado"      DataField="nombreEstadoResultadoResp" SortExpression="nombreEstadoResultadoResp" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Estado"         DataField="nombreEstado" SortExpression="nombreEstado" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />

                            

                    <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                        <ItemTemplate>

                            <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocGeneralResp")  + ";" + DataBinder.Eval(Container.DataItem, "idPestana")  + ";" + DataBinder.Eval(Container.DataItem, "idArchivoBinSC") %>' 
                                    ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />

                            <asp:ImageButton ID="gDescargar" Visible="false" runat="server" CausesValidation="false" CommandName="Descargar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocGeneralResp")  + ";" + DataBinder.Eval(Container.DataItem, "idPestana")  + ";" + DataBinder.Eval(Container.DataItem, "idArchivoBinSC") %>' 
                                    ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" />

                            <asp:ImageButton ID="gNoVigente" Visible="false" runat="server" CausesValidation="false" CommandName="NoVigente" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocGeneralResp")  + ";" + DataBinder.Eval(Container.DataItem, "idPestana")  + ";" + DataBinder.Eval(Container.DataItem, "idArchivoBinSC") %>' 
                                ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Pasar a No Vigente" ToolTip="Pasar a No Vigente" />

                            <asp:ImageButton ID="gVigente" Visible="false" runat="server" CausesValidation="false" CommandName="Vigente" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocGeneralResp")  + ";" + DataBinder.Eval(Container.DataItem, "idPestana")  + ";" + DataBinder.Eval(Container.DataItem, "idArchivoBinSC") %>' 
                                    ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Pasar a Vigente" ToolTip="Pasar a Vigente" />

                            <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDocGeneralResp")  + ";" + DataBinder.Eval(Container.DataItem, "idPestana")  + ";" + DataBinder.Eval(Container.DataItem, "idArchivoBinSC") %>' 
                                    ImageUrl="../../App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />


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






                 <!-- Diálogo --><div id="agregarGrupoSusp" class="dialog">
       <div class="background"></div>
       <div class="content_dialog">
           <div class="top">
               <asp:LinkButton ID="cerrar_agregarGrupoSusp" CssClass="cerrar" OnClientClick="javascript:close_dialog('agregarGrupoSusp');" CausesValidation="false" runat="server"></asp:LinkButton></div><div class="body">
               <fieldset>
                   <legend>Agregar Grupo Suspendido</legend><iframe id="iframe_agregarGrupoSusp" src="" width="500px" height="300px" frameborder="0" scrolling="no"></iframe>            
               </fieldset>
              
           </div>
       </div>
       </div>