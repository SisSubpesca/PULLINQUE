<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="carta.aspx.cs" Inherits="SubPesca.Mantenedores.Generales.carta" Theme="admin_style" %>
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

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

    <asp:Content ID="Formulario" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManagerCarta" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Mantenedor de Carta</span>
            </td>
            <td align="right" valign="middle">
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

    <fieldset>
    <legend>Datos de Carta</legend>
    
        <asp:ValidationSummary ID="ValidationSummaryFormularioAgregar" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
        
        <br />

        <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Panel ID="Content_Panel" Visible="false" runat="server">
        <table class="form" cellpadding="0" cellspacing="0">
            <tr>
                <td class="style1"><span class="item">Región</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="Region" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorRegion" runat="server" 
                        ControlToValidate="Region" Display="Static" ErrorMessage="Región" 
                        ValidationGroup="grupo1" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td class="style1"><span class="item">Nombre Carta</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="TipoCarta" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTipoCarta" runat="server" 
                        ControlToValidate="TipoCarta" Display="Static" ErrorMessage="Nombre Carta" 
                        ValidationGroup="grupo1" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td class="style1"><span class="item">Estado</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td><asp:DropDownList ID="Estado" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEstado" runat="server" 
                        ControlToValidate="Estado" Display="Static" ErrorMessage="Estado" 
                        ValidationGroup="grupo1" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td class="style1">
                    <span class="item">Datum</span></td>
                <td class="col2">
                    <span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="Datum" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDatum" runat="server" 
                        ControlToValidate="Datum" Display="Static" ErrorMessage="Datum" 
                        InitialValue="-1" ValidationGroup="grupo1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style1"><span class="item">Tipo Huso</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="TipoHuso" runat="server"></asp:DropDownList>
            
                </td>
                
            </tr>
            <tr>
                <td class="col1"><span class="item">Número Carta</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    
                    <asp:TextBox ID="nombre" runat="server" MaxLength="25"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorNombre" runat="server" ControlToValidate="nombre" ValidationGroup="grupo1" ErrorMessage="Número Carta" Display="Static" >*</asp:RequiredFieldValidator>
                    
                    
                        
                </td>
            </tr>
                        <tr>
                            <td class="col1">
                                <span class="item">Número Edición</span></td>
                            <td class="col2">
                                <span class="item">:</span></td>
                            <td>
                                <asp:TextBox ID="NumeroEdicion" runat="server" Height="22px"></asp:TextBox>
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorNumeroEdicion" runat="server" ControlToValidate="NumeroEdicion" ValidationGroup="grupo1" ErrorMessage="Número Edición" Display="Static">*</asp:RequiredFieldValidator>    
                                <asp:RangeValidator ID="RangeValidatorNumeroEdicion" runat="server" Type="Integer" MinimumValue="1" MaximumValue="255" ControlToValidate="NumeroEdicion" ForeColor="Red" ErrorMessage="Número Edición rango no permitido" ValidationGroup="grupo1" />
                                <asp:CompareValidator ID="CompareValidatorNumeroEdicion" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="NumeroEdicion" ForeColor="Red" ErrorMessage="Número Edición ingrese valores numéricos" ValidationGroup="grupo1" />
                     
                            </td>
            </tr>
            <tr>
                <td class="col1">
                    <span class="item">Año Edición</span></td>
                <td class="col2">
                    <span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="AnioEdicion" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorAnioEdicion" runat="server" ControlToValidate="AnioEdicion" ValidationGroup="grupo1" ErrorMessage="Año Edición" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>    
                </td>
            </tr>
                        <tr>
                <td class="col1"><span class="item">Escala</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="Escala" runat="server" Width="276px"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorEscala" runat="server" ControlToValidate="Escala" ValidationGroup="grupo1" ErrorMessage="Escala" Display="Static">*</asp:RequiredFieldValidator>    
                </td>
                
            </tr>
            <tr>
                <td class="col1">
                    <span class="item">A_A_A</span></td>
                <td class="col2">
                    <span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="AAA" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorAAA" runat="server" ControlToValidate="AAA" ValidationGroup="grupo1" ErrorMessage="AAA" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>    

                </td>
            </tr>
            <tr>
                <td class="col1">
                    <span class="item">Reemplazo Carta</span></td>
                <td class="col2">
                    <span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="ReemplazoCarta" runat="server"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td class="col1">
                    <span class="item">Fecha Reemplazo</span></td>
                <td class="col2">
                    <span class="item">:</span></td>
                <td>
                <asp:UpdatePanel ID="UpdatePanel_FechaRecepcion" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaTextRecepcion" Width="120px" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Enabled="True"
                        Format="dd'/'MM'/'yyyy HH':'mm'"  TargetControlID="FechaTextRecepcion" PopupButtonID="endCal1"  CssClass="cal_Theme1" FirstDayOfWeek="Monday"/>
                        <img runat="server" id="endCal1" alt ="CalendarioRecepcion" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                       
                        <asp:RegularExpressionValidator 
                         ID="RegularExpressionValidator1" 
                         runat="server"
                         ControlToValidate="FechaTextRecepcion"
                         ForeColor="Red"
                         ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d\s(([0-1][0-9])|([2][0-3])):[0-5][0-9]$" 
                         ErrorMessage="Ingrese formato válido"
                         ValidationGroup="grupo1"> </asp:RegularExpressionValidator>
                    </div>
                </div>
            </ContentTemplate>
            </asp:UpdatePanel></td>
            </tr>
            <tr>
                <td class="col1">
                    <span class="item">Observaciones</span></td>
                <td style="width:10px; text-align:center;">
                    <span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="observaciones" runat="server" Columns="50" MaxLength="2000" 
                        Rows="5" TextMode="multiline"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td style="width: 10px; text-align: center;">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Agregar" runat="server" CausesValidation="true" OnClick="Agregar_Click" OnClientClick="javascript:doIframe();" Text="Agregar" ValidationGroup="grupo1" />
                    <asp:Button ID="Buscar" runat="server" OnClientClick="javascript:doIframe();" Text="Buscar" onclick="Buscar_Click" />
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
            
            <asp:Panel ID="PanelGridCarta"  Visible="true" runat="server" CssClass="Content_Grilla">                                          
            <asp:GridView ID="GridView1" runat="server"
                RowStyle-VerticalAlign="top" AlternatingRowStyle-VerticalAlign="top"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                DataKeyNames="idCarta"
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
                    <asp:BoundField HeaderText="ID" DataField="idCarta" SortExpression="idCarta" ReadOnly="true"/>
                    <asp:TemplateField HeaderText="Region" SortExpression="regionString">
                         <ItemTemplate>
                            <asp:Label ID="gRegion" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("regionString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                            <asp:DropDownList ID="ddleditCountry" runat="server" AutoPostBack="true" />
                            <asp:HiddenField ID="hdnCountry" runat="server" Value='<%#Eval("regionString") %>' />
                    
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorddleditCountry" runat="server" ControlToValidate="ddleditCountry"  ValidationGroup="grupo2" ErrorMessage="Seleccione valor para Región" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                        </EditItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Nombre Carta" SortExpression="tipoCartaString">
                        <ItemTemplate>
                            <asp:Label ID="gTipoCarta" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("tipoCartaString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddleditCountry2" runat="server" AutoPostBack="true" />
                            <asp:HiddenField ID="hdnCountry2" runat="server" Value='<%#Eval("tipoCartaString") %>' />
                    
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorddleditCountry2" runat="server" ControlToValidate="ddleditCountry2"  ValidationGroup="grupo2" ErrorMessage="Seleccione valor para Tipo Carta" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                        </EditItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Estado" SortExpression="estadoString">
                        <ItemTemplate>
                            <asp:Label ID="gEstado" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("estadoString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddleditCountry10" runat="server" AutoPostBack="true" />
                            <asp:HiddenField ID="hdnCountry10" runat="server" Value='<%#Eval("estadoString") %>' />
                    
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorddleditCountry10" runat="server" ControlToValidate="ddleditCountry10"  ValidationGroup="grupo2" ErrorMessage="Seleccione valor para Estado" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Datum" SortExpression="datumString">
                         <ItemTemplate>
                            <asp:Label ID="gDatum" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "datumString") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                            <asp:DropDownList ID="ddleditCountry3" runat="server" AutoPostBack="true" />
                            <asp:HiddenField ID="hdnCountry3" runat="server" Value='<%#Eval("datumString") %>' />
                    
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorddleditCountry3" runat="server" ControlToValidate="ddleditCountry3"  ValidationGroup="grupo2" ErrorMessage="Seleccione valor para Datum" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tipo Huso" SortExpression="tipoHusoString">
                         <ItemTemplate>
                            <asp:Label ID="gTipoHuso" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tipoHusoString") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                            <asp:DropDownList ID="ddleditCountry4" runat="server" AutoPostBack="true" />
                            <asp:HiddenField ID="hdnCountry4" runat="server" Value='<%#Eval("tipoHusoString") %>' />
                    
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Numero Carta" SortExpression="numeroCarta">
                         <ItemTemplate>
                            <asp:Label ID="gNumeroCarta" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "numeroCarta") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                            <asp:HiddenField ID="hdnNumeroCarta" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "numeroCarta") %>' />
                            <asp:TextBox ID="geNumeroCarta" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "numeroCarta") %>'></asp:TextBox>
                            
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorgeNumeroCarta" runat="server" ControlToValidate="geNumeroCarta"  ValidationGroup="grupo2" ErrorMessage="Seleccione valor para Número Carta" ForeColor="Red" Display="Static" InitialValue= ""></asp:RequiredFieldValidator>
                            
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Numero Edicion" SortExpression="numeroEdicion">
                         <ItemTemplate>
                            <asp:Label ID="gNumeroEdicion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "numeroEdicion") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                            <asp:HiddenField ID="hdnNumeroEdicion" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "numeroEdicion") %>' />
                            <asp:TextBox ID="geNumeroEdicion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "numeroEdicion") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorgeNumeroEdicion" runat="server" ControlToValidate="geNumeroEdicion"  ValidationGroup="grupo2" ErrorMessage="Seleccione valor para Número Edición" ForeColor="Red" Display="Static" InitialValue= ""></asp:RequiredFieldValidator>
                            
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Año Edicion" SortExpression="anioEdicion">
                         <ItemTemplate>
                            <asp:Label ID="gAnioEdicion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "anioEdicion") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddleditCountry5" runat="server" AutoPostBack="true" />
                            <asp:HiddenField ID="hdnCountry5" runat="server" Value='<%#Eval("anioEdicion") %>' />
                    
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorddleditCountry5" runat="server" ControlToValidate="ddleditCountry5"  ValidationGroup="grupo2" ErrorMessage="Seleccione valor para Año Edición" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                        </EditItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Escala" SortExpression="escala">
                         <ItemTemplate>
                            <asp:Label ID="gEscala" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "escala") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                            <asp:HiddenField ID="hdnEscala" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "escala") %>' />
                            <asp:TextBox ID="geEscala" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "escala") %>'></asp:TextBox>
                            
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorgeEscala" runat="server" ControlToValidate="geEscala"  ValidationGroup="grupo2" ErrorMessage="Seleccione valor para Escala" ForeColor="Red" Display="Static" InitialValue= ""></asp:RequiredFieldValidator>
                            
                        </EditItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="A A A" SortExpression="aaaString" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gAAA" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("aaaString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                                <asp:DropDownList ID="ddleditCountry20" runat="server" AutoPostBack="true" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorDdleditCountry20" runat="server" ControlToValidate="ddleditCountry20"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para A A A" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                                <asp:HiddenField ID="hdnCountry20" runat="server" Value='<%#Eval("aaaString") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>        


                    <asp:TemplateField HeaderText="Reemplazo Carta" SortExpression="reemplazoCarta">
                         <ItemTemplate>
                            <asp:Label ID="gReemplazoCarta" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "reemplazoCarta") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>

                            <asp:HiddenField ID="hdnReemplazoCarta" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "reemplazoCarta") %>' />
                            <asp:TextBox ID="geReemplazoCarta" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "reemplazoCarta") %>' MaxLength="30" Width="99%"></asp:TextBox>
                            
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha Reemplazo" SortExpression="fechaReemplazo">
                         <ItemTemplate>
                            <asp:Label ID="gFechaReemplazo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fechaReemplazoString") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                            <asp:HiddenField ID="hdnFechaReemplazo" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "fechaReemplazo") %>' />
                            <asp:TextBox ID="geFechaReemplazo" Width="120px" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "fechaReemplazoString") %>'></asp:TextBox>

                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Enabled="True"
                            Format="dd'/'MM'/'yyyy HH':'mm'"  TargetControlID="geFechaReemplazo" PopupButtonID="endCal1"  CssClass="cal_Theme1" FirstDayOfWeek="Monday"/>
                            <img runat="server" id="endCal1" alt ="CalendarioRecepcion" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                           
                            <asp:RegularExpressionValidator 
                             ID="RegularExpressionValidator1" 
                             runat="server"
                             ControlToValidate="geFechaReemplazo"
                             ForeColor="Red"
                             ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d\s(([0-1][0-9])|([2][0-3])):[0-5][0-9]$" 
                             ErrorMessage="Ingrese formato válido"
                             ValidationGroup="grupo1"> </asp:RegularExpressionValidator>

                        </EditItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Observaciones" SortExpression="observaciones">
                        <ItemTemplate>
                            <asp:Label ID="gObservaciones" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("observaciones")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>

                            <asp:HiddenField ID="hdnObservaciones" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "observaciones") %>' />
                            <asp:TextBox ID="geObservaciones" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "observaciones") %>' MaxLength="30" Width="99%"></asp:TextBox>
                            
                        </EditItemTemplate>
                    </asp:TemplateField>
                                          
                    <asp:TemplateField HeaderText="Opciones">
                        <ItemTemplate>
                            <asp:ImageButton ID="gModificar" Visible="false" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idCarta") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                            <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idCarta") %>'
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
        invoca_calendarios("inicioSolicitud");
      </script>
    </asp:Content>