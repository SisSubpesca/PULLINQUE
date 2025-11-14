<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Administrador/SitioAdmin.Master"  CodeBehind="InicioVisacion.aspx.cs" 
Inherits="SubPesca.Solicitudes.Visaciones.InicioVisacion" Theme="admin_style" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></asp:ToolkitScriptManager>


    
    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Iniciar Visación Masiva</span>
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />

    <fieldset>
        <legend>Búsqueda de Solicitudes</legend>


    <asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
        <ContentTemplate>    
            <asp:panel ID="PanelMensajes" runat="server">
                <asp:ValidationSummary ID="ValidationSummaryErrores" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="FormErrores"  />
            </asp:panel>
        </ContentTemplate>
    </asp:UpdatePanel>

     <asp:Panel ID="FormularioBusqueda" runat="server" Visible="true">
             
        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Tipo de Trámite</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanelTiposSolicitud" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="TiposSolicitud" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TiposSolicitud_OnSelectedIndexChanged"></asp:DropDownList> *
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        </table>

      


        <asp:UpdatePanel ID="UpdatePanelRegion" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1"><span class="item">Región</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3"><asp:DropDownList ID="Region" AutoPostBack="true" runat="server"  OnSelectedIndexChanged="Region_OnSelectedIndexChanged"></asp:DropDownList></td>
            </tr>
            </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>


        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Provincia</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanelProvincia" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Provincia" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Provincias_OnSelectedIndexChanged"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Region" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>

            </td>
        </tr>
        </table>

        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Comuna</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanelComuna" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Comuna" AutoPostBack="true" runat="server"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Region" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Provincia" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
           </td>
        </tr>
        </table>

        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">N° Pert/ Identificador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanelPert" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="Pert" TextMode="multiline" Columns="50" Rows="3" AutoPostBack="true" runat="server"></asp:TextBox>
                    Separados por coma (,)
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
           </td>
        </tr>
        </table>

        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item"></span></td>
            <td class="col2"><span class="item"></span></td>
            <td class="col3">
                
                <asp:UpdatePanel ID="UpdatePanelPanelRelocalizaciones" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <asp:Panel ID="PanelRelocalizaciones" Visible="false" runat="server">
                    <div class="msgGrilla_div4">
                        <asp:ImageButton ID="gAlert" Visible="true" runat="server" CausesValidation="false" ImageUrl="../../App_Themes/admin_style/images/info.gif" Height="20px" /> Los pert de relocalizaciones deben incluir un guión seguido del número de sector: Ej: 210110096-1
                    </div>
                </asp:Panel>
                </ContentTemplate>
                 <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>

                <asp:Panel ID="Panel2" Visible="true"  CssClass="Content_msgGrilla_s" runat="server">
                        Información: Debe tener asignada las solicitudes para iniciar visaciones.
                </asp:Panel>
            </td>
        </tr>
        </table>

        <fieldset>
        <legend>Tipo de Visación</legend>

        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Tipo de Visación</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanelTipoVisacion" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                            <asp:DropDownList ID="TipoVisacion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TipoVisacion_OnSelectedIndexChanged"></asp:DropDownList> *
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="TiposSolicitud" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        </table>

        <asp:UpdatePanel ID="UpdatePanelResultado" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            <asp:Panel id="PanelResultado" Visible="false" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Resultado</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:DropDownList ID="Resultado" runat="server"  MaxLength="8"></asp:DropDownList> *</td>
                </tr>
                </table>
            </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        </fieldset>


        <table class="form" cellpadding="0px" cellspacing="0px">   
        <tr>
            <td class="col1"></td>
            <td class="col2"></td>
            <td class="col3">
                <asp:Button ID="Limpiar" runat="server" Text="Limpiar"  CausesValidation="false" OnClick="Limpiar_Click"  />
                <asp:Button ID="Buscar" runat="server" Text="Buscar"  CausesValidation="true"  OnClick="Buscar_Click" />
            </td>
        </tr>
        </table>

        </asp:Panel>

        <br />


        
        <asp:UpdatePanel ID="UpdatePanelMensajeBusqueda" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   
                <asp:Panel ID="PanelMensajeBusqueda" CssClass="Content_msgGrilla" Visible="false" runat="server">
                    <div class="msgGrilla_div2">
                        <asp:Label ID="MensajeBusqueda" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelGrilla" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla" runat="server"></asp:Label>
                </div>
            </asp:Panel>
                                                      
            <asp:GridView ID="GridTramite" runat="server" 
                AutoGenerateColumns="False" 
                CellPadding="4" 
                ForeColor="#333333" 
                GridLines="None"
                DataKeyNames="idSolConcesion"
                AllowPaging="true"
                PageSize="30"
                OnPageIndexChanging="GridTramite_PageIndexChanged"
                OnRowCommand="GridTramite_RowCommand"
                OnRowDataBound="GridTramite_RowDataBound"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>

                <asp:TemplateField ItemStyle-Width="40px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkboxSelectAll2" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                        <asp:HiddenField runat="server" ID="HiddenIdSolConcesion" Value='<%#Eval("idSolConcesion") %>'  />
                        <asp:HiddenField runat="server" ID="HiddenIdTipoVisacion" Value='<%#Eval("tipoVisacion.id") %>' />
                        <asp:HiddenField runat="server" ID="HiddenPert" Value='<%#Eval("pert") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tema Visación">
                        <ItemTemplate>
                              <%# System.Web.HttpUtility.HtmlEncode((string)Eval("tipoVisacion.descripcion"))%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tipo Trámite">
                        <ItemTemplate>
                            <%# System.Web.HttpUtility.HtmlEncode((string)Eval("tipoTramite.descripcion"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                     

                    <asp:TemplateField HeaderText="Sub tipo de Trámite">
                        <ItemTemplate>
                            <%# System.Web.HttpUtility.HtmlEncode((string)Eval("subTipoTramite"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    

                    <asp:TemplateField HeaderText="N°Pert/Identificador">
                        <ItemTemplate>
                            <%# System.Web.HttpUtility.HtmlEncode((string)Eval("pert"))%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado de la solicitud">
                        <ItemTemplate>
                            <%# System.Web.HttpUtility.HtmlEncode((string)Eval("estado.descripcion"))%>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Región">
                        <ItemTemplate>
                            <%# System.Web.HttpUtility.HtmlEncode((string)Eval("region.descripcion"))%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Provincia">
                        <ItemTemplate>
                            <%# System.Web.HttpUtility.HtmlEncode((string)Eval("provincia.descripcion"))%>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Comuna">
                        <ItemTemplate>
                            <%# System.Web.HttpUtility.HtmlEncode((string)Eval("comunas"))%>
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
            





            <asp:UpdatePanel ID="UpdatePanelPanelBotones" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   
            
            <asp:Panel ID="PanelBotones" runat="server" Visible="false">

                <asp:Button ID="ButtonExportar" Visible="false" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrillaReposicion_Click" /> 

                <asp:HiddenField ID="TipoTramiteHidden" runat="server" />
                <asp:HiddenField ID="TipoTramiteDescripcionHidden" runat="server" />
                <asp:HiddenField ID="TipoVisacionHidden" runat="server" />
                <asp:HiddenField ID="TipoVisacionDescripcionHidden" runat="server" />
                <asp:HiddenField ID="ResultadoVisacionHidden" runat="server" />
                <asp:HiddenField ID="ResultadoVisacionDescripcionHidden" runat="server" />



                <br />
                
                <asp:UpdatePanel ID="UpdatePanelMensajeGuardar" UpdateMode="Conditional" runat="server">
                   <ContentTemplate>    
                       <asp:panel ID="PanelMensajeGuardar" runat="server">
                            <asp:ValidationSummary ID="ValidationSummary1" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="GrupoBotonGuardar" />
                        </asp:panel>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <br />

                <table class="form" cellpadding="0px" cellspacing="0px">   
                <tr>
                    <td class="col1"></td>
                    <td class="col2"></td>
                    <td class="col3">
                        <asp:Button ID="Guardar" runat="server" Text="Guardar"  CausesValidation="true"  OnClick="Guardar_Click" ValidationGroup="GrupoBotonGuardar" OnClientClick="javascript:muestra_loading('cargando');" />
                    </td>
                </tr>
                </table>

            </asp:Panel>

            </ContentTemplate>
            </asp:UpdatePanel>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Buscar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            <asp:PostBackTrigger ControlID="ButtonExportar" />
        </Triggers>

        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelLabelMensajeGuardado" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   
                <asp:Panel ID="PanelLabelMensajeGuardado" CssClass="Content_msgGrilla" Visible="false" runat="server">
                    <div class="msgGrilla_div2">
                        <asp:Label ID="LabelMensajeGuardado" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        
    </fieldset>

<div id="cargando" class="message">
        <div class="background"></div>
        <div style="width:100%; text-align:center; margin-top:350px;">
            <asp:Image ID="Image1" ImageUrl="~/App_Themes/admin_style/images/loading.gif" Width="100px" runat="server" />
        </div>
</div>


</asp:Content>


