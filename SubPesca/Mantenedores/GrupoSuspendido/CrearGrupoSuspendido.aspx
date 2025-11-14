<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearGrupoSuspendido.aspx.cs" Inherits="SubPesca.Mantenedores.GrupoSuspendido.CrearGrupoSuspendido" 
MasterPageFile="~/Administrador/SitioAdmin.Master" Theme="admin_style" %>


<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManageAdministracionContactos" runat="server" EnablePartialRendering="true" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>

    <fieldset>
    
    <asp:UpdatePanel ID="UpdatePanelMensajeEvalUnidOrdenamTerr" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
            <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:ValidationSummary ID="ValidationSummaryGrupoSuspendido" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
    
    <table class="form" cellpadding="0px" cellspacing="0px">
    
    <tr>
        <td class="col1"><span class="item">Tipo Agrupación</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            
            <asp:UpdatePanel ID="UpdatePanelTipoAgrupacion" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:DropDownList ID="TipoAgrupacion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SeleccionTipoAgrupacion_Selected"></asp:DropDownList> *

                <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoAgrupacion" runat="server" ControlToValidate="TipoAgrupacion"  ValidationGroup="grupo1"
                                ErrorMessage="Tipo Agrupación" Display="None" InitialValue="-1">*</asp:RequiredFieldValidator>
             </ContentTemplate>
             </asp:UpdatePanel> 
            
        </td>
    </tr>
    
    <tr>
        <td class="col1"><span class="item">Código de Centro</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
        <asp:UpdatePanel ID="UpdatePanelCodigoCentro" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                    <asp:TextBox ID="CodigoCentro" runat="server" Enabled="false"></asp:TextBox>
            </ContentTemplate>
        </asp:UpdatePanel>
        </td>
        
    </tr>
    <tr>
        <td class="col1"><span class="item">Nombre Grupo Suspendido</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            <asp:TextBox ID="NombreGrupo" runat="server"></asp:TextBox>  *
        </td>
    </tr>
    </table>
    
    </fieldset>

    <asp:Panel ID="PanelArchivoAdjuntoOculto"  Visible="false" runat="server">
    <fieldset>
    <legend>Archivo Adjunto</legend>

    <asp:Panel ID="PanelArchivoAdjuntoGrupo" CssClass="Content_msgGrilla" Visible="false" runat="server">
        <div class="msgGrilla_div2">
            <asp:Label ID="LabelArchivoAdjuntoGrupo" runat="server"></asp:Label>
        </div>
    </asp:Panel>

    <asp:ValidationSummary ID="ValidationSummaryArchivoAdjuntoGrupo" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo2" />
    
    <table class="form" cellpadding="0px" cellspacing="0px">
    
    <tr>
        <td class="col1"><span class="item">Número</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            
            <asp:TextBox ID="NumeroCI" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator id="RequiredFieldValidatorNumero" runat="server" ControlToValidate="NumeroCI"  ValidationGroup="grupo2" ErrorMessage="Número" Display="none"></asp:RequiredFieldValidator>
            
        </td>
    </tr>
    
    <tr>
    <td class="col1"><span class="item">Fecha</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            
                                <asp:UpdatePanel ID="UpdatePanel_FechaRecepcionArchAdj" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div class="calendario">
                                        <div class="calendario_textbox">               
                                            <asp:TextBox ID="FechaTextRecepcionArchAdj" Width="120px" runat="server" ></asp:TextBox>  *
                                            <cc1:CalendarExtender ID="CalendarExtenderArchivoAdj" runat="server"  Enabled="True"
                                            Format="dd'/'MM'/'yyyy HH':'mm'"  TargetControlID="FechaTextRecepcionArchAdj" PopupButtonID="endCal1"  CssClass="cal_Theme1" FirstDayOfWeek="Monday"/>
                                            <img runat="server" id="endCal1" alt ="CalendarioRecepcion" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                                            <asp:RequiredFieldValidator id="RequiredFieldValidatorFechRecepArchivoAdj" runat="server" ControlToValidate="FechaTextRecepcionArchAdj"  ValidationGroup="grupo2"
                                             ErrorMessage="Fecha Control Ingreso" Display="Static">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator 
                                             ID="RegularExpressionValidatorArchivoAdj" 
                                             runat="server"
                                             ControlToValidate="FechaTextRecepcionArchAdj"
                                             ForeColor="Red"
                                             ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d\s(([0-1][0-9])|([2][0-3])):[0-5][0-9]$" 
                                             ErrorMessage="Ingrese formato válido"
                                             ValidationGroup="grupo2">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                </asp:UpdatePanel> 
        </td>   
    </tr>

    <tr>
        <td class="col1"><span class="item">Archivo Físico</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
        
                           <asp:UpdatePanel ID="UpdatePanelArchivoAdjunto" UpdateMode="Conditional" runat="server">
                           <ContentTemplate>

                           <asp:FileUpload ID="ArchivoAdjunto" MaxLength="40" Width="200px" runat="server" />  *

                           <asp:RequiredFieldValidator id="RequiredFieldValidatorArchivoAdjunto" runat="server" ControlToValidate="ArchivoAdjunto" ValidationGroup="grupo2" 
                           ErrorMessage="Archivo Adjunto" Display="Static" InitialValue="0">*</asp:RequiredFieldValidator>

                           <asp:RegularExpressionValidator ID="RegularExpressionValidatorArchivoAdjunto" runat="server" ErrorMessage="Formato Archivo Incorrecto" ControlToValidate="ArchivoAdjunto" ValidationExpression= "(.*).(.doc|.DOC|.pdf|.PDF|.docx|.DOCX|.xls|.xlsx|.XLS|.XLSX|.dwg|.DWG|.dxf|.DXF|.jpg|.JPG|.jpeg|.JPEG)$" ValidationGroup="grupo2" />

                           </ContentTemplate>
                           <Triggers>

                                <asp:PostBackTrigger ControlID="AgregarArchivoAdjunto" />

                           </Triggers>
                           </asp:UpdatePanel>                   
        
        </td>
    </tr>
       
    <tr>
        <td class="col1"><span class="item">¿Es Documento Final?</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4"><asp:DropDownList ID="EsDocumentoFinal" runat="server"></asp:DropDownList></td>
    </tr>

    <tr>
        <td class="col1" colspan="6"><asp:ImageButton ID="AgregarArchivoAdjunto" runat="server" 
                ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                AlternateText="Agregar Archivo Adjunto" ToolTip="Agregar Archivo Adjunto" 
                ValidationGroup="grupo2" CausesValidation="true" 
                onclick="AgregarArchivoAdjunto_Click"  />
            <span class="item">Agregar Archivo Adjunto</span></td>
    </tr>
       
    </table>

    <asp:UpdatePanel ID="UpdatePanelGrillaArchivoAdjunto" UpdateMode="Conditional" runat="server">
    <ContentTemplate>

    <asp:Panel ID="PanelArchivoAdjunto"  Visible="true" runat="server">
                        <asp:GridView 
                            ID="GridArchivoAdjunto" 
                            runat="server" 
                            AutoGenerateColumns="False"
                            CellPadding="4" 
                            ForeColor="#333333"
                            GridLines="None"
                            AllowPaging="False"
                            AllowSorting="false" 
                            CssClass="mGrid"
                            PagerStyle-CssClass="pgr" 
                            width="100%" 
                            PageIndex= "1" 
                            OnRowCreated="GridArchivoAdjunto_RowCreated"
                            OnRowDataBound="GridViewGridArchivoAdjunto_RowDataBound"
                            OnRowCommand="GridViewGridArchivoAdjunto_RowCommand"
                            >
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                               <asp:TemplateField HeaderText="Numero">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                       <%# DataBinder.Eval(Container, "DataItem.numero")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="Fecha">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.fecha")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Archivo Fisico">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.nombreFisico")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="¿Es Documento Final?">
                                    <ItemTemplate>
                                        <%# Eval("docFinal").ToString().ToLower() == "true" ? "Sí" : "No"%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Vigencia">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.estadoVigencia.descripcion")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="gDescargar" Visible="true" runat="server" CausesValidation="false" CommandName="Descargar" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.idArchivo") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" OnPreRender="ImgAdd_PreRender" />

                                        <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.idArchivo")  %>'
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
    </asp:UpdatePanel>
    </fieldset>
    </asp:Panel>

    <asp:Panel ID="PanelAsociacionUE"  Visible="false" runat="server">
    <fieldset>
    <legend>Asociación con Solicitudes UE</legend>

    <asp:Panel ID="PanelAsociacionSolUE" CssClass="Content_msgGrilla" Visible="false" runat="server">
        <div class="msgGrilla_div2">
            <asp:Label ID="LabelAsociacionSolUE" runat="server"></asp:Label>
        </div>
    </asp:Panel>

    <asp:ValidationSummary ID="ValidationSummaryAsociacionSolUE" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo3" />
    
    <table class="form" cellpadding="0px" cellspacing="0px">
    
        <tr>
            <td class="col1"><span class="item">Tipo Solicitud</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
                <asp:DropDownList ID="TipoSolicitud" runat="server"></asp:DropDownList> *
                <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoSolicitud" runat="server" ControlToValidate="TipoSolicitud"  ValidationGroup="grupo3" ErrorMessage="Tipo Solicitud" Display="none" InitialValue="-1"></asp:RequiredFieldValidator>
            
            </td>
        </tr>
    
        <tr>
            <td class="col1"><span class="item">Número Pert/Identificador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
            
            <asp:TextBox ID="NumeroPert" runat="server" OnTextChanged="Identificador_TextChanged" AutoPostBack="true"></asp:TextBox>
            <asp:RequiredFieldValidator id="RequiredFieldValidatorNumeroPert" runat="server" ControlToValidate="NumeroPert"  ValidationGroup="grupo3" ErrorMessage="Número Pert/Identificador" Display="none" InitialValue="-1"></asp:RequiredFieldValidator>

            </td>
        </tr>
            <asp:Panel ID="PanelCentro"  Visible="true" runat="server"> 
                <tr>
                    <td class="col1"><span class="item">Titulares</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3" colspan="4"><asp:Label ID="Titulares" runat="server"></asp:Label></td>
                </tr>
               
                <tr>
                    <td class="col1"><span class="item">Región</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3" colspan="4"><asp:Label ID="Region" runat="server"></asp:Label></td>
                </tr>

                <tr>
                    <td class="col1"><span class="item">Toponimio</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3" colspan="4"><asp:Label ID="Toponimio" runat="server"></asp:Label></td>
                </tr>
            </asp:Panel>
    <tr>
        <td class="col1" colspan="3"><asp:ImageButton ID="ImageButtonAsociacionSolUE" runat="server" 
                ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                AlternateText="Asociar con Solicitud UE" ToolTip="Asociar con Solicitud UE" 
                ValidationGroup="grupo3" CausesValidation="true" 
                onclick="ImageButtonAsociacionSolUE_Click"  />
            <span class="item">Asociar con Solicitud UE</span></td>
    </tr>
       
    </table>

    
    <asp:Panel ID="PanelGridViewAsociacionSolUE"  Visible="true" runat="server">
                        <asp:GridView 
                            ID="GridViewAsociacionSolUE" 
                            runat="server" 
                            AutoGenerateColumns="False"
                            CellPadding="4" 
                            ForeColor="#333333"
                            GridLines="None"
                            AllowPaging="False"
                            AllowSorting="false" 
                            CssClass="mGrid"
                            PagerStyle-CssClass="pgr" 
                            width="100%" 
                            PageIndex= "1" 
                            OnRowCreated="GridViewAsociacionSolUE_RowCreated"
                            OnRowDataBound="GridViewAsociacionSolUE_RowDataBound"
                            OnRowCommand="GridViewAsociacionSolUE_RowCommand"
                            >
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                               <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label HeaderText="EstadoVigencia" ID="EstadoVigencia" runat="server" Visible="false" Text='<%# (DataBinder.Eval(Container, "DataItem.estadoVigencia.id")) %>'></asp:Label>
                                    </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Tipo Solicitud">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                        <%# DataBinder.Eval(Container, "DataItem.solicitudConcesion.tipoTramite.descripcion")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="Pert">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.solicitudConcesion.numPert")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Número Identificador">
                                    <ItemTemplate>
                                         <%# DataBinder.Eval(Container, "DataItem.solicitudConcesion.idConcesion")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                         <%# DataBinder.Eval(Container, "DataItem.estadoVigencia.descripcion")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="gNoVigente" Visible="false" runat="server" CausesValidation="false" CommandName="NoVigente" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.solicitudConcesion.idConcesion")  %>'
                                        ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="NoVigente" ToolTip="NoVigente" />

                                        <asp:ImageButton ID="gVigente" Visible="false" runat="server" CausesValidation="false" CommandName="Vigente" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.solicitudConcesion.idConcesion")  %>'
                                        ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Vigente" ToolTip="Vigente" />
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
    </asp:Panel>

    <br />

    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1" colspan="6">
            <asp:Button ID="GuardarGrupoSuspendido" runat="server" 
                Text="Guardar Grupo Suspendido" CausesValidation="true" 
                ValidationGroup="grupo1" onclick="GuardarGrupoSuspendido_Click"/>
        </td>
    </tr>
    </table>
</asp:Content>