<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="entidadesDeAnalisis.aspx.cs" Inherits="SubPesca.Mantenedores.Transversales.entidadesDeAnalisis" Theme="admin_style" %>

    <%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 158px;
        }
    </style>
</asp:Content>



    <asp:Content ID="FormularioConsultorAmbiental" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnableScriptGlobalization="True"></asp:ToolkitScriptManager>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Mantenedor de Entidades de Análisis</span>
            </td>
            <td align="right" valign="middle">
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

    <fieldset>
    <legend>Datos de Entidades de Análisis</legend>
    
        <asp:ValidationSummary ID="ValidationSummaryFormularioAgregar" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
        
        <br />

        <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Panel ID="Content_Panel" Visible="false" runat="server">
        <table class="form" cellpadding="0" cellspacing="0">
            <tr>
                <td class="col1"><span class="item">Número Entidad de Análisis</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="numEntidadAnalisis" runat="server" MaxLength="20" Width="100px"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatornumEntidadAnalisis" runat="server" 
                        ControlToValidate="numEntidadAnalisis"  ValidationGroup="grupo1" ErrorMessage="Número Entidad de Análisis" 
                        Display="Static">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidatornumEntidadAnalisis" runat="server" Type="Integer" 
                        MinimumValue="1" MaximumValue="1999999999" ControlToValidate="numEntidadAnalisis" 
                        ErrorMessage="Número Entidad de Análisis con rango no permitido" ValidationGroup="grupo1" ForeColor="Red" />
                    <asp:CompareValidator ID="CompareValidatornumEntidadAnalisis" runat="server" Operator="DataTypeCheck" Type="Integer" 
                        ControlToValidate="numEntidadAnalisis" ErrorMessage="Número Entidad de Análisis Ingrese valores numéricos" ValidationGroup="grupo1" ForeColor="Red" />
                </td>
                
            </tr>
            <tr>
                <td class="col1"><span class="item">Razón Social</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="nombre" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorNombre" runat="server" 
                        ControlToValidate="nombre"  ValidationGroup="grupo1" ErrorMessage="Razón Social" 
                        Display="Static">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td class="col1"><span class="item">Representante Legal</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="RepresentanteLegal" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorRepresentanteLegal" runat="server" 
                        ControlToValidate="RepresentanteLegal"  ValidationGroup="grupo1" ErrorMessage="RepresentanteLegal" 
                        Display="Static">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td class="col1"><span class="item">Domicilio</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="Domicilio" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorDomicilio" runat="server" 
                        ControlToValidate="Domicilio"  ValidationGroup="grupo1" ErrorMessage="Domicilio" 
                        Display="Static">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td class="col1"><span class="item">Categoría</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="Categoria" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorCategoría" runat="server" 
                        ControlToValidate="Categoria"  ValidationGroup="grupo1" ErrorMessage="Categoría" 
                        Display="Static">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td class="col1"><span class="item">Número</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                     <asp:TextBox ID="Numero" runat="server" MaxLength="20" Width="100px"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorNumero" runat="server" 
                        ControlToValidate="Numero"  ValidationGroup="grupo1" ErrorMessage="Número" 
                        Display="Static">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidatorNumero" runat="server" Type="Integer" 
                        MinimumValue="1" MaximumValue="1999999999" ControlToValidate="Numero" 
                        ErrorMessage="Número con rango no permitido" ValidationGroup="grupo1" ForeColor="Red" />
                    <asp:CompareValidator ID="CompareValidatorNumero" runat="server" Operator="DataTypeCheck" Type="Integer" 
                        ControlToValidate="Numero" ErrorMessage="Número Ingrese valores numéricos" ValidationGroup="grupo1" ForeColor="Red" />
                </td>
                
            </tr>
            <tr>
                <td class="col1"><span class="item">Fecha</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                   <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                   <ContentTemplate>
                   <div class="calendario">
                   <div class="calendario_textbox">               
                        <asp:TextBox ID="Fecha" Width="120px" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtenderFecha" runat="server"  Enabled="True"
                        Format="dd'/'MM'/'yyyy'"  TargetControlID="Fecha" PopupButtonID="endCal1"  CssClass="cal_Theme1" FirstDayOfWeek="Monday"/>
                        <img runat="server" id="endCal1" alt ="Fecha" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                       
                        <asp:RegularExpressionValidator 
                         ID="RegularExpressionValidatorFecha" 
                         runat="server"
                         ControlToValidate="Fecha"
                         ForeColor="Red"
                         ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" 
                         ErrorMessage="Ingrese formato válido"
                         ValidationGroup="grupo1"> </asp:RegularExpressionValidator>
                    </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>  
                </td> 
            </tr>
            <tr>
                <td class="col1"><span class="item">Inicio Vigencia</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                   <asp:UpdatePanel ID="UpdatePanel_FechaRecepcion" UpdateMode="Conditional" runat="server">
                   <ContentTemplate>
                   <div class="calendario">
                   <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaTextRecepcion" Width="120px" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Enabled="True"
                        Format="dd'/'MM'/'yyyy'"  TargetControlID="FechaTextRecepcion" PopupButtonID="endCal2"  CssClass="cal_Theme1" FirstDayOfWeek="Monday"/>
                        <img runat="server" id="endCal2" alt ="CalendarioRecepcion" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                       
                        <asp:RegularExpressionValidator 
                         ID="RegularExpressionValidatorFechaTextRecepcion" 
                         runat="server"
                         ControlToValidate="FechaTextRecepcion"
                         ForeColor="Red"
                         ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" 
                         ErrorMessage="Ingrese formato válido"
                         ValidationGroup="grupo1"> </asp:RegularExpressionValidator>
                    </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>  
                </td> 
            </tr>
             <tr>
                <td class="col1"><span class="item">Fin Vigencia</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                   <asp:UpdatePanel ID="UpdatePanelFechaTerminoVigencia" UpdateMode="Conditional" runat="server">
                   <ContentTemplate>
                   <div class="calendario">
                   <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaTerminoVigencia" Width="120px" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtenderFechaTerminoVigencia" runat="server"  Enabled="True"
                        Format="dd'/'MM'/'yyyy'"  TargetControlID="FechaTerminoVigencia" PopupButtonID="endCal3"  CssClass="cal_Theme1" FirstDayOfWeek="Monday"/>
                        <img runat="server" id="endCal3" alt ="Fecha Termino Vigencia" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                       
                        <asp:RegularExpressionValidator 
                         ID="RegularExpressionValidatorFechaTerminoVigencia" 
                         runat="server"
                         ControlToValidate="FechaTerminoVigencia"
                         ForeColor="Red"
                         ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" 
                         ErrorMessage="Ingrese formato válido"
                         ValidationGroup="grupo1"> </asp:RegularExpressionValidator>
                    </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>   
                </td>
                
            </tr>
            <tr>
                <td class="col1"><span class="item">Condición de Vigencia</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="CondicionVigencia" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorCondicionVigencia" runat="server" 
                        ControlToValidate="CondicionVigencia"  ValidationGroup="grupo1" ErrorMessage="Condicion de Vigencia" 
                        Display="Static">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td class="col1"><span class="item">Correo Electrónico</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="CorreoElectronico" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                    
                </td>
                
            </tr>
            <tr>
                <td class="col1"><span class="item">Teléfono</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="Telefono" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidatorTelefono" runat="server" Type="Integer" 
                        MinimumValue="1" MaximumValue="1999999999" ControlToValidate="Telefono" 
                        ErrorMessage="Teléfono con rango no permitido" ValidationGroup="grupo1" ForeColor="Red" />
                    <asp:CompareValidator ID="CompareValidatorTelefono" runat="server" Operator="DataTypeCheck" Type="Integer" 
                        ControlToValidate="Telefono" ErrorMessage="Teléfono Ingrese valores numéricos" ValidationGroup="grupo1" ForeColor="Red" />
                </td>
                
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td style="width:10px; text-align:center;">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Agregar" runat="server" OnClick="Agregar_Click" 
                        OnClientClick="javascript:doIframe();" Text="Agregar" CausesValidation="true" ValidationGroup="grupo1" />
                    <asp:Button ID="Buscar" runat="server" OnClientClick="javascript:doIframe();" 
                        Text="Buscar" onclick="Buscar_Click" />
                </td>
            </tr>
        </table>
        </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
                       
        </fieldset>

        <asp:UpdatePanel ID="upd2" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <div style="height:30px;">
            <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla_dialog" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla" runat="server"></asp:Label>
                </div>
                <div class="msgGrilla_div3">
                    <a onclick="ocultarObjeto('Content_msgGrilla', 0)"><img src="../../App_Themes/admin_style/images/cerrar.jpg" height="20px" alt="borrar" /></a>
                </div>
            </asp:Panel>
            </div>     
            
            <asp:Panel ID="PanelGridEntidadAnalisis"  Visible="true" runat="server" CssClass="Content_Grilla">                                        
                <asp:GridView ID="GridView1" runat="server"
                RowStyle-VerticalAlign="top" AlternatingRowStyle-VerticalAlign="top"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                DataKeyNames="numEntidad"
                AllowPaging="True" PageSize="20" OnPageIndexChanging="GridView1_PageIndexChanged"
                AllowSorting="true" OnSorting="GridView1_Sorting"
                OnRowDataBound="GridView1_RowDataBound"
                OnRowCommand="GridView1_RowCommand"
                OnRowEditing="GridView1_RowEditing"
                OnRowUpdating="GridView1_RowUpdating"
                OnRowCancelingEdit="GridView1_RowCancelingEdit"
                CssClass="mGrid_dialog"
                PagerStyle-CssClass="pgr" 
                Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="Numero Entidad Analisis" DataField="numEntidad" ReadOnly="true" ItemStyle-Width="60px" />
                    <asp:TemplateField HeaderText="Razon Social" SortExpression="razonSocial" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gRazonSocial" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("razonSocial")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                                <asp:TextBox ID="geRazonSocial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "razonSocial") %>' MaxLength="30" Width="99%"></asp:TextBox>
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorgeRazonSocial" runat="server" ControlToValidate="geRazonSocial"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Razon Social" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>
                            
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Representante Legal" SortExpression="repLegal" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gRepresentanteLegal" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("repLegal")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="geRepresentanteLegal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "repLegal") %>' MaxLength="30" Width="99%"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorgeRepresentanteLegal" runat="server" ControlToValidate="geRepresentanteLegal"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Representante Legal" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>

                        </EditItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Domicilio" SortExpression="domicilio" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gDomicilio" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("domicilio")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="geDomicilio" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "domicilio") %>' MaxLength="30" Width="99%"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorgeDomicilio" runat="server" ControlToValidate="geDomicilio"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Domicilio" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>

                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Categoria" SortExpression="categoria" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gCategoria" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("categoria")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="geCategoria" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "categoria") %>' MaxLength="30" Width="99%"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorgeCategoria" runat="server" ControlToValidate="geCategoria"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Categoria" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>

                        </EditItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Número" SortExpression="numero" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gNumero" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "numero") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="geNumero" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "numero") %>' MaxLength="30" Width="99%"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorgeNumero" runat="server" ControlToValidate="geNumero"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Número" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidatorgeNumero" runat="server" Type="Integer" MinimumValue="1" MaximumValue="1999999999" ControlToValidate="geNumero" 
                             ErrorMessage="Número con rango no permitido" ValidationGroup="grupo2" />
                            <asp:CompareValidator ID="CompareValidatorgeNumero" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="geNumero" ErrorMessage="Número Ingrese valores numéricos" ValidationGroup="grupo2" />

                        </EditItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Fecha" SortExpression="fechaString">
                         <ItemTemplate>
                            <asp:Label ID="gFecha" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fechaString") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                            <asp:HiddenField ID="hdnFecha" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "fechaString") %>' />
                            <asp:TextBox ID="geFecha" Width="120px" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "fechaString") %>'></asp:TextBox>

                            <cc1:CalendarExtender ID="CalendarFecha" runat="server"  Enabled="True"
                            Format="dd'/'MM'/'yyyy'"  TargetControlID="geFecha" PopupButtonID="endCal1"  CssClass="cal_Theme1" FirstDayOfWeek="Monday"/>
                            <img runat="server" id="endCal1" alt ="Calendario Fecha" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                           
                            <asp:RegularExpressionValidator 
                             ID="RegularExpressionValidator1" 
                             runat="server"
                             ControlToValidate="geFecha"
                             ForeColor="Red"
                             ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"
                             ErrorMessage="Ingrese formato válido"
                             ValidationGroup="grupo2"> </asp:RegularExpressionValidator>

                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha Inicio Vigencia" SortExpression="inicioVigenciaString">
                         <ItemTemplate>
                            <asp:Label ID="gFechaIniVigencia" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "inicioVigenciaString") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                            <asp:HiddenField ID="hdnFechaIniVigencia" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "inicioVigenciaString") %>' />
                            <asp:TextBox ID="geFechaIniVigencia" Width="120px" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "inicioVigenciaString") %>'></asp:TextBox>

                            <cc1:CalendarExtender ID="CalendarExtenderFechaIniVigencia" runat="server"  Enabled="True"
                            Format="dd'/'MM'/'yyyy'"  TargetControlID="geFechaIniVigencia" PopupButtonID="endCal2"  CssClass="cal_Theme1" FirstDayOfWeek="Monday"/>
                            <img runat="server" id="endCal2" alt ="Calendario Fecha Inicio Vigencia" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                           
                            <asp:RegularExpressionValidator 
                             ID="RegularExpressionValidator2" 
                             runat="server"
                             ControlToValidate="geFechaIniVigencia"
                             ForeColor="Red"
                             ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"
                             ErrorMessage="Ingrese formato válido"
                             ValidationGroup="grupo2"> </asp:RegularExpressionValidator>

                        </EditItemTemplate>
                    </asp:TemplateField>
                    
                   <asp:TemplateField HeaderText="Fecha Término Vigencia" SortExpression="finVigenciaString">
                         <ItemTemplate>
                            
                            <asp:Label ID="gFechaTerminoVigencia" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "finVigenciaString") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                            <asp:HiddenField ID="hdnFechaTerminoVigencia" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "finVigenciaString") %>' />
                            <asp:TextBox ID="geFechaTerminoVigencia" Width="120px" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "finVigenciaString") %>'></asp:TextBox>

                            <cc1:CalendarExtender ID="CalendarExtenderFechaTerminoVigencia" runat="server"  Enabled="True"
                            Format="dd'/'MM'/'yyyy'"  TargetControlID="geFechaTerminoVigencia" PopupButtonID="endCal3"  CssClass="cal_Theme1" FirstDayOfWeek="Monday"/>
                            <img runat="server" id="endCal3" alt ="Calendario Fecha Término Vigencia" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                           
                            <asp:RegularExpressionValidator 
                             ID="RegularExpressionValidatorFechaTerminoVigencia" 
                             runat="server"
                             ControlToValidate="geFechaTerminoVigencia"
                             ForeColor="Red"
                             ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"
                             ErrorMessage="Ingrese formato válido"
                             ValidationGroup="grupo2"> </asp:RegularExpressionValidator>

                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Condición de Vigencia" SortExpression="condVigencia" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gCondVigencia" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("condVigencia")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="geCondVigencia" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "condVigencia") %>' MaxLength="30" Width="99%"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorgeCondVigencia" runat="server" ControlToValidate="geCondVigencia"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Condición de Vigencia" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>

                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Correo Electrónico" SortExpression="correo" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gCorreoElectronico" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("correo")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="geCorreoElectronico" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "correo") %>' MaxLength="30" Width="99%"></asp:TextBox>
                            
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Teléfono" SortExpression="fono" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gTelefono" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fono") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="geTelefono" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fono") %>' MaxLength="30" Width="99%"></asp:TextBox>
                            <asp:RangeValidator ID="RangeValidatorTelefono" runat="server" Type="Integer" MinimumValue="1" MaximumValue="1999999999" ControlToValidate="geTelefono" 
                             ErrorMessage="Teléfono con rango no permitido" ValidationGroup="grupo2" />
                            <asp:CompareValidator ID="CompareValidatorTelefono" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="geTelefono" ErrorMessage="Teléfono Ingrese valores numéricos" ValidationGroup="grupo2" />

                        </EditItemTemplate>
                    </asp:TemplateField>                      
                    <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:ImageButton ID="gModificar" Visible="false" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "numEntidad") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Cambiar nombre" ToolTip="Cambiar nombre" />
                            <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "numEntidad") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="gActualizar" runat="server" CausesValidation="true" ValidationGroup="grupo2" CommandName="Update"
                                 ImageUrl="~/App_Themes/admin_style/images/guardar2.png"  Height="20px" AlternateText="Actualizar" ToolTip="Actualizar" />
                            <asp:ImageButton ID="gCancelar" runat="server" CausesValidation="false" CommandName="Cancel" 
                                 ImageUrl="~/App_Themes/admin_style/images/cancelar.png"  Height="20px" AlternateText="Cancelar" ToolTip="Cancelar" />                            
                        </EditItemTemplate>
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

            <asp:HiddenField ID="KeySort" runat="server" />
            <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" Visible="false" />
                       
      </ContentTemplate> 
      <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
            <asp:PostBackTrigger ControlID="ExportarGrilla" />

      </Triggers>
      </asp:UpdatePanel>

    <script type="text/javascript">
        invoca_calendarios("entidadAnalisis");
    </script>

    </asp:Content>

