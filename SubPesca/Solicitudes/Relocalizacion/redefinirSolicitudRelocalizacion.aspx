<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="redefinirSolicitudRelocalizacion.aspx.cs" 
Inherits="SubPesca.Solicitudes.Relocalizacion.redefinirSolicitudRelocalizacion" Theme="admin_style" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>


    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Redefinir Trámite de Relocalización Nº PERT: <asp:Label ID="numeroPert" runat="server"></asp:Label></span>
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />



<asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
   <ContentTemplate>    
       <asp:panel ID="Panel1" runat="server">
            <asp:ValidationSummary ID="ValidationSummaryInicioSolicitud" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="GrupoSector" />
        </asp:panel>
    </ContentTemplate>
</asp:UpdatePanel>

    <fieldset>
        <legend>Trámite de Relocalización</legend>
        <br />
        

        <asp:UpdatePanel ID="UpdatePanelCheck" UpdateMode="Conditional" runat="server">
            <ContentTemplate>

                <asp:HiddenField ID="indexSector" runat="server"  />

                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">¿Es un Sector 0?</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">

                        <asp:RadioButton ID="esSectorCeroOpcion1" runat="server" GroupName="grupoSector0" Text="Si" AutoPostBack="true" OnCheckedChanged="esSectorCeroOpcionChecked"/>
                        <asp:RadioButton ID="esSectorCeroOpcion2" runat="server" GroupName="grupoSector0" Text="No" AutoPostBack="true" OnCheckedChanged="esSectorCeroOpcionChecked"/>
                       
                    </td>
                </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
       


        <asp:UpdatePanel ID="UpdatePanelSectorCero" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:Panel ID="PanelSectorCero"  Visible="false" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Código del Centro</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">

                                <asp:TextBox  ID="CodigoSiepSectorCero"   runat="server" MaxLength="40" Columns="40" AutoPostBack="true" OnTextChanged="CodigoSiepSectorCero_OnTextChanged"></asp:TextBox>
                                  <asp:AutoCompleteExtender 
                                    runat="server" 
                                    ID="AutoCompleteExtender1" 
                                    TargetControlID="CodigoSiepSectorCero"
                                    ServicePath="../CentroAcuicolaWS.asmx"
                                    ServiceMethod="BuscarCentros"
                                    MinimumPrefixLength="2" 
                                    CompletionInterval="1000"
                                    EnableCaching="true"
                                    CompletionListCssClass="autocompletionList"
                                    CompletionListHighlightedItemCssClass="autoitemHighlighted"
                                    CompletionListItemCssClass="autolistItem"
                                    >
                                </asp:AutoCompleteExtender>
                        
                        </td>
                    </tr>
                    </table>

                    <asp:UpdatePanel ID="UpdatePanelNombreTitularSectorCero" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="PanelNombreTitularSectorCero"  Visible="true" runat="server">
                                <table class="form" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td class="col1"><span class="item">Nombre del Titular</span></td>
                                    <td class="col2"><span class="item">:</span></td>
                                    <td class="col3"><asp:TextBox  ID="nombreTitularSectorCero" runat="server" Columns="40" ReadOnly="true"></asp:TextBox></td>
                                </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Hectáreas Sector 0</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox  ID="HectareasSectorCero" MaxLength="10" Width="100px"  runat="server"></asp:TextBox>
                        
                          <asp:RegularExpressionValidator ID="RegularExpressionValidatorHectareasSectorCero"
                            ControlToValidate="HectareasSectorCero" 
                            ForeColor="Red"  
                            ValidationGroup="GrupoSector" 
                            runat="server" 
                            ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,9})?$" 
                            ErrorMessage="Ingrese formato válido en campo Hectáreas Sector 0. Ej: 12,3"
                            Display="None">
                            </asp:RegularExpressionValidator>
                        
                        </td>
                    </tr>
                    </table>

                    <br />

                    <asp:UpdatePanel ID="UpdatePanelDatosCentroSectorCero" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>   
                            <asp:Panel ID="PanelDatosCentroSectorCero" Visible="false" runat="server">

                                <table class="form" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td class="col1"><span class="item">Sector</span></td>
                                    <td class="col2"><span class="item">:</span></td>
                                    <td class="col3"><span class="item"><asp:Label ID="sectorSectorCero" runat="server"></asp:Label></span></td>
                                    <td class="col4"><span class="item">Comuna</span></td>
                                    <td class="col5"><span class="item">:</span></td>
                                    <td class="col6"><span class="item"><asp:Label ID="comunaSectorCero" runat="server"></asp:Label></span></td>
                                </tr>
                                <tr>
                                    <td class="col1"><span class="item">Región</span></td>
                                    <td class="col2"><span class="item">:</span></td>
                                    <td class="col3"><span class="item"><asp:Label ID="regionSectorCero" runat="server"></asp:Label></span></td>
                                    <td class="col4"><span class="item">AC</span></td>
                                    <td class="col5"><span class="item">:</span></td>
                                    <td class="col6"><span class="item"><asp:Label ID="acSectorCero" runat="server"></asp:Label></span></td>
                                </tr>
                                <tr>
                                    <td class="col1"><span class="item">Tipo de Centro</span></td>
                                    <td class="col2"><span class="item">:</span></td>
                                    <td class="col3"><span class="item"><asp:Label ID="tipoCentroSectorCero" runat="server"></asp:Label></span></td>
                                    <td class="col4"><span class="item">Superficie Total Centro</span></td>
                                    <td class="col5"><span class="item">:</span></td>
                                    <td class="col6"><span class="item"><asp:Label ID="superficieTotalCentroSectorCero" runat="server"></asp:Label></span></td>
                                </tr>

                                 <tr>
                                    <td class="col1"><span class="item">Superficie Total Centro (Cultivo)</span></td>
                                    <td class="col2"><span class="item">:</span></td>
                                    <td class="col3" colspan="4"><span class="item"><asp:Label ID="superficieCultivoTotalCentroSectorCero" runat="server"></asp:Label></span></td>
                                    
                                </tr>
                                </table>

                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

      
    </fieldset>

        
        <asp:UpdatePanel ID="UpdatePanelOrigen" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:Panel ID="PanelOrigen"  Visible="false" runat="server">

                    <fieldset>
                    <legend>Origenes</legend>
                    <br />
                    <asp:ValidationSummary ID="ValidationSummary1" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupoOrigen" />

                        <asp:HiddenField ID="indexOrigen" runat="server"  />

                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="col1"><span class="item">Código del Centro</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3">
                            
                                <asp:TextBox  ID="CodigoSiepOrigen" runat="server" ValidationGroup="grupoOrigen"  MaxLength="40" Columns="40" OnTextChanged="CodigoSiepOrigen_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                            
                                <asp:requiredfieldvalidator id="RequiredFieldValidator2"
                                  controltovalidate="CodigoSiepOrigen"
                                  validationgroup="grupoOrigen"
                                  errormessage="Ingrese codigo del centro"
                                  runat="Server">
                                </asp:requiredfieldvalidator>

                                <asp:AutoCompleteExtender 
                                    runat="server" 
                                    ID="autoComplete1" 
                                    TargetControlID="CodigoSiepOrigen"
                                    ServicePath="../CentroAcuicolaWS.asmx"
                                    ServiceMethod="BuscarCentros"
                                    MinimumPrefixLength="2" 
                                    CompletionInterval="1000"
                                    EnableCaching="true"
                                    CompletionListCssClass="autocompletionList"
                                    CompletionListHighlightedItemCssClass="autoitemHighlighted"
                                    CompletionListItemCssClass="autolistItem"
                                    >
                                </asp:AutoCompleteExtender>

                            </td>
                        </tr>
                        </table>

                           
                        <asp:UpdatePanel ID="UpdatePanelNombreTitularOrigen" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="PanelNombreTitularOrigen"  Visible="true" runat="server">
                                    <table class="form" cellpadding="0px" cellspacing="0px">
                                    <tr>
                                        <td class="col1"><span class="item">Nombre del Titular</span></td>
                                        <td class="col2"><span class="item">:</span></td>
                                        <td class="col3"><asp:TextBox  ID="nombreTitularOrigen" runat="server" Columns="40" ReadOnly="true"></asp:TextBox></td>
                                    </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>



                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="col1"><span class="item">Superficie Relocalización</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3"><asp:TextBox  ID="superficieRelocalizadaOrigen" MaxLength="10" Width="100px"  runat="server"   ValidationGroup="grupoOrigen"></asp:TextBox>

                                <asp:requiredfieldvalidator id="RequiredFieldValidator1"
                                  controltovalidate="superficieRelocalizadaOrigen"
                                  validationgroup="grupoOrigen"
                                  errormessage="Ingrese Superficie Relocalización"
                                  runat="Server">
                                </asp:requiredfieldvalidator>
                            

                              <asp:RegularExpressionValidator ID="RegularExpressionValidatorSuperficieRelocalizadaOrigen"
                                ControlToValidate="superficieRelocalizadaOrigen" 
                                ForeColor="Red"  
                                ValidationGroup="grupoOrigen" 
                                runat="server" 
                                ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,9})?$" 
                                ErrorMessage="Ingrese formato válido en campo Superficie Relocalización. Ej: 12,3"
                                Display="None">
                                </asp:RegularExpressionValidator>
                            
                            </td>
                        </tr>
                        <tr>
                            <td class="col1"><span class="item">Preferencia de Relocalización</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3">
                                <asp:ListBox ID="preferencias" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </td>
                        </tr>
                        </table>

                         <br />

                        <asp:UpdatePanel ID="UpdatePanelDatosCentroOrigen" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>   
                                <asp:Panel ID="PanelDatosCentroOrigen" Visible="false" runat="server">

                                    <table class="form" cellpadding="0px" cellspacing="0px">
                                    <tr>
                                        <td class="col1"><span class="item">Sector</span></td>
                                        <td class="col2"><span class="item">:</span></td>
                                        <td class="col3"><span class="item"><asp:Label ID="sectorOrigen" runat="server"></asp:Label></span></td>
                                        <td class="col4"><span class="item">Comuna</span></td>
                                        <td class="col5"><span class="item">:</span></td>
                                        <td class="col6"><span class="item"><asp:Label ID="comunaOrigen" runat="server"></asp:Label></span></td>
                                    </tr>
                                    <tr>
                                        <td class="col1"><span class="item">Región</span></td>
                                        <td class="col2"><span class="item">:</span></td>
                                        <td class="col3"><span class="item"><asp:Label ID="regionOrigen" runat="server"></asp:Label></span></td>
                                        <td class="col4"><span class="item">AC</span></td>
                                        <td class="col5"><span class="item">:</span></td>
                                        <td class="col6"><span class="item"><asp:Label ID="acOrigen" runat="server"></asp:Label></span></td>
                                    </tr>
                                    <tr>
                                        <td class="col1"><span class="item">Tipo de Centro</span></td>
                                        <td class="col2"><span class="item">:</span></td>
                                        <td class="col3"><span class="item"><asp:Label ID="tipoCentroOrigen" runat="server"></asp:Label></span></td>
                                        <td class="col4"><span class="item">Superficie Total Centro</span></td>
                                        <td class="col5"><span class="item">:</span></td>
                                        <td class="col6"><span class="item"><asp:Label ID="superficieTotalCentroOrigen" runat="server"></asp:Label></span></td>
                                    </tr>

                                    <tr>
                                        <td class="col1"><span class="item">Superficie Total Centro (Cultivo)</span></td>
                                        <td class="col2"><span class="item">:</span></td>
                                        <td class="col3" colspan="4"><span class="item"><asp:Label ID="superficieCultivoTotalCentroOrigen" runat="server"></asp:Label></span></td>
                                    </tr>
                                    </table>

                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                        


                        <asp:UpdatePanel ID="UpdatePanelBotonOrigen" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>                        
                                <asp:Panel ID="PanelBotonOrigen"  Visible="true" runat="server">
                                    <table class="form" cellpadding="0px" cellspacing="0px" width="100%" align="center" />
                                    <tr>
                                        <td class="col1"><span class="item"></span></td>
                                        <td class="col2"><span class="item"></span></td>
                                        <td class="col3" align="center">
                                            <asp:ImageButton ID="GuardarOrigen" runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                                AlternateText="Guardar Origen" ToolTip="Guardar Origen" onclick="GuardarOrigen_Click" ValidationGroup="grupoOrigen" CausesValidation="true" />
                                            <span class="item"><asp:Label id="botonGuardarOrigen" Text="Guardar Origen" runat="server"></asp:Label></span>
                                        </td>
                                    </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
        
                        <br />

                        <asp:UpdatePanel ID="UpdatePanelErroresGridOrigen" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>   
                                <asp:Panel ID="PanelErroresGridOrigen" CssClass="Content_msgGrilla" Visible="false" runat="server">
                                    <div class="msgGrilla_div2">
                                        <asp:Label ID="ErroresGridOrigen" runat="server"></asp:Label>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>


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
                                    OnRowDataBound="GridOrigen_RowDataBound"
                                    PagerStyle-CssClass="pgr"
                                    OnRowCommand="GridOrigen_RowCommand">
                        
                                    <Columns>

                                        <asp:TemplateField HeaderText="Centro de Origen">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                                <%# DataBinder.Eval(Container, "DataItem.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro")%>
                                            </ItemTemplate>
                                        </asp:TemplateField> 

                                        <asp:TemplateField HeaderText="Nombre del Titular">
                                            <ItemTemplate>
                                                 <%# DataBinder.Eval(Container, "DataItem.concesionOrigen.DescripcionTitulares")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sector">
                                            <ItemTemplate>
                                                
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
                            
                                        
                                        <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                            <ItemTemplate>

                                                <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "index") %>' 
                                                ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />
                                            

                                                <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "index") %>' 
                                                ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                                            
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
                    <asp:ValidationSummary ID="ValidationSummary2" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupoDestino" />

                        
                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="col1"><span class="item">Tipo de Relocalizaci&oacute;n</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3">
                                <asp:RadioButton ID="opcionCrea" runat="server" GroupName="grupoTipoRelocalizacion" Text="Crea"  AutoPostBack="true" OnCheckedChanged="tipoRelocalizacionOpcionChecked"/>
                                <asp:RadioButton ID="opcionFusiona" runat="server" GroupName="grupoTipoRelocalizacion" Text="Fusiona"    AutoPostBack="true" OnCheckedChanged="tipoRelocalizacionOpcionChecked"/>
                            </td>
                        </tr>
                        </table>
                        
       

                        <asp:UpdatePanel ID="UpdatePanelCentroDestino" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="PanelCentroDestino"  Visible="false" runat="server" >

                                    <table class="form" cellpadding="0px" cellspacing="0px">
                                    <tr>
                                        <td class="col1"><span class="item">Código del Centro Destino</span></td>
                                        <td class="col2"><span class="item">:</span></td>
                                        <td class="col3"><asp:TextBox  ID="codigoSiepCentroDestino"   runat="server" MaxLength="40" Columns="40" OnTextChanged="CodigoSiepDestino_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                        
                                            <asp:AutoCompleteExtender 
                                                runat="server" 
                                                ID="AutoCompleteExtender2" 
                                                TargetControlID="codigoSiepCentroDestino"
                                                ServicePath="../CentroAcuicolaWS.asmx"
                                                ServiceMethod="BuscarCentros"
                                                MinimumPrefixLength="2" 
                                                CompletionInterval="1000"
                                                EnableCaching="true"
                                                CompletionListCssClass="autocompletionList"
                                                CompletionListHighlightedItemCssClass="autoitemHighlighted"
                                                CompletionListItemCssClass="autolistItem"
                                                >
                                            </asp:AutoCompleteExtender>

                                        
                                        </td>
                                    </tr>
                                    </table>


                                    <asp:UpdatePanel ID="UpdatePanelNombreTitularDestino" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="PanelNombreTitularDestino"  Visible="true" runat="server">
                                                <table class="form" cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td class="col1"><span class="item">Nombre del Titular</span></td>
                                                    <td class="col2"><span class="item">:</span></td>
                                                    <td class="col3"><asp:TextBox  ID="nombreTitularDestino" runat="server" Columns="40" ReadOnly="true"></asp:TextBox></td>
                                                </tr>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>


                                    <br />


                                    <asp:UpdatePanel ID="UpdatePanelDatosCentroDestino" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>   
                                            <asp:Panel ID="PanelDatosCentroDestino" Visible="false" runat="server">

                                                <table class="form" cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td class="col1"><span class="item">Sector</span></td>
                                                    <td class="col2"><span class="item">:</span></td>
                                                    <td class="col3"><span class="item"><asp:Label ID="sectorDestino" runat="server"></asp:Label></span></td>
                                                    <td class="col4"><span class="item">Comuna</span></td>
                                                    <td class="col5"><span class="item">:</span></td>
                                                    <td class="col6"><span class="item"><asp:Label ID="comunaDestino" runat="server"></asp:Label></span></td>
                                                </tr>
                                                <tr>
                                                    <td class="col1"><span class="item">Región</span></td>
                                                    <td class="col2"><span class="item">:</span></td>
                                                    <td class="col3"><span class="item"><asp:Label ID="regionDestino" runat="server"></asp:Label></span></td>
                                                    <td class="col4"><span class="item">AC</span></td>
                                                    <td class="col5"><span class="item">:</span></td>
                                                    <td class="col6"><span class="item"><asp:Label ID="acDestino" runat="server"></asp:Label></span></td>
                                                </tr>
                                                <tr>
                                                    <td class="col1"><span class="item">Tipo de Centro</span></td>
                                                    <td class="col2"><span class="item">:</span></td>
                                                    <td class="col3"><span class="item"><asp:Label ID="tipoCentroDestino" runat="server"></asp:Label></span></td>
                                                    <td class="col4"><span class="item">Superficie Total Centro</span></td>
                                                    <td class="col5"><span class="item">:</span></td>
                                                    <td class="col6"><span class="item"><asp:Label ID="superficieTotalCentroDestino" runat="server"></asp:Label></span></td>
                                                </tr>
                                                </table>

                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>



                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>



                      

                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <br />


        <asp:UpdatePanel ID="UpdatePanelBotonSector" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelBotonSector"  Visible="true" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px" width="100%">
                    <tr>
                        <td class="col1" align="center">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                AlternateText="Guardar Sector" ToolTip="Guardar Sector" onclick="GuardarSector_Click"  CausesValidation="true" ValidationGroup="GrupoSector" />
                            <span class="item"><asp:Label id="botonGuardarSector" Text="Guardar Sector" runat="server"></asp:Label></span>
                        </td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        


        <br />


        <asp:UpdatePanel ID="UpdatePanelErroresGridSectores" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   
                <asp:Panel ID="PanelErroresGridSectores" CssClass="Content_msgGrilla" Visible="false" runat="server">
                    <div class="msgGrilla_div2">
                        <asp:Label ID="ErroresGridSectores" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>



    <fieldset>

        <asp:UpdatePanel ID="UpdatePanelGridSectores" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelGridSectores"  Visible="true" runat="server">
                
                    <asp:GridView 
                    ID="GridSectores" 
                    runat="server" 
                    AutoGenerateColumns="False" 
                    CellPadding="4" 
                    ForeColor="#333333"
                    TabIndex="1"
                    GridLines="None" 
                    CssClass="mGrid"
                    OnRowDataBound="GridSectores_RowDataBound"
                    PagerStyle-CssClass="pgr"
                    OnRowCommand="GridSectores_RowCommand"
                    >
                        
                    <Columns>

                        <asp:TemplateField HeaderText="Nº PERT">
                            <ItemTemplate>
                                <asp:HiddenField ID="gSSP" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.contieneSSp") %>' />
                                <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                  <%# DataBinder.Eval(Container, "DataItem.tramiteRel.numPert")%>              
                            </ItemTemplate>
                        </asp:TemplateField> 

                        <asp:TemplateField HeaderText="Nº Sector">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.numSector")%>                                                
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tipo de Relocalización">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.tipoRelocalizacion.descripcion")%>                                                             
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Centro de Origen">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.origenesDetalle")%>                                                                       
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Centro de Destino">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro")%> 
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nombre Titular">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.titularesOrigenesDetalle")%>       
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Sector">
                            <ItemTemplate>
                                    
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Región">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.concesionDestino.region.descripcion")%>           
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Comuna">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.concesionDestino.DescripcionComuna")%>                
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="AC">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.concesionDestino.barrio.barrio")%>                                
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                        <asp:TemplateField HeaderText="Tipo de Centro">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.concesionDestino.tipoUnidadEspacial.descripcion")%>                            
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Superficie Relocalizable [ha.]">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.superficieSector")%>                                                                       
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Superficie Total Centro [ha.]">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.concesionDestino.superficieCalculada")%>                                                                           
                            </ItemTemplate>
                        </asp:TemplateField>
                            
                                        
                        <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                            <ItemTemplate>


                                <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "index") %>' 
                                                ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                                <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "index") %>' 
                                                ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                                            
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


                    <br />


            


                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>



        <br />



        <table class="form" cellpadding="0px" cellspacing="0px" width="100%">
        <tr>
            <td class="col1" align="center">
                <asp:Button ID="ButtonLimpiarTramite" runat="server" Text="Limpiar" CausesValidation="false" onclick="LimpiarFormulario"  />
                <asp:Button ID="ButtonGuardarTramite" runat="server" Text="Redefinir" CausesValidation="false" onclick="GuardarTramite_Click"  />
            </td>
        </tr>
        </table>

    </fieldset>
              
        
    
</asp:Content>

