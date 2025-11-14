<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesModificacionCentroAcopio.Master" AutoEventWireup="true" 
CodeBehind="datosModificacionCentroAcopio.aspx.cs" Inherits="SubPesca.Solicitudes.ModificacionCentroAcopio.datosModificacionCentroAcopio" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="~/Solicitudes/Registrar/checkTramitaUTS.ascx"                tagname="checkTramitaUTS"              tagprefix="uc0" %>
<%@ Register src="~/Solicitudes/Registrar/informacionSolicitud.ascx"             tagname="informacionSolicitud"           tagprefix="uc2" %>

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
                <span id="titulo_modulo">Datos del Trámite de Modificación de Centro de Acopio</span>
            </td>
        </tr>
    </table>

    <hr style="width:100%;" />

    <asp:UpdatePanel ID="UpdatePanelDatosGeneral" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 
    <asp:Panel ID="PanelDatosGeneral"  Visible="false" runat="server">
    
    <fieldset>
        
        <legend>Datos Generales</legend>
        <br />

            <table>
             <tr>
                <td class="col1"><span class="item">Tipo Modificación</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3"><asp:Label ID="TipoModificacion" runat="server"></asp:Label></td>
            </tr>
            </table>

            <asp:UpdatePanel ID="UpdatePanelDatosInicio" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            <asp:Panel ID="PanelDatosInicio" Visible="false" runat="server">
                <table>
                <tr>
                    <td class="col1"><span class="item">Nº PERT</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:Label ID="numPertConcesion" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="col1"><span class="item">Fecha Recepción</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:Label ID="fechaRecepcionTramiteConcesion" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="col1"><span class="item">Fecha de Ingreso a Trámite</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:Label ID="fechaIngresoTramiteConcesion" runat="server"></asp:Label></td>
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


    

    

    <asp:UpdatePanel ID="UpdatePanelDatosConcesion" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 
    <asp:Panel ID="PanelDatosConcesion"  Visible="false" runat="server">

    <fieldset>
        
        <legend>Datos del Centro de Acopio</legend>
        <br />

        <asp:Panel ID="Panel1" Visible="true" runat="server">
            <table>
            <tr>
                <td class="col1"><span class="item">Código de Centro</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3"><asp:Label ID="codigoCentroConcesion" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="col1"><span class="item">Superficie de la Concesión</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3"><asp:Label ID="SuperficieConcesion" runat="server"></asp:Label><span class="item">[Ha.]</span></td>
            </tr>
            </table>
        </asp:Panel>
    </fieldset>
   <br />
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    

   <asp:UpdatePanel ID="UpdatePanelDatosSol" UpdateMode="Conditional" runat="server">
   <ContentTemplate> 
   <asp:Panel ID="PanelDatosSol" Visible="false" runat="server">

    <fieldset>
        
        <legend>Datos de la Solicitud</legend>
        <br />
            
            <asp:ValidationSummary ID="ValidationSummaryInicioSolicitud" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />

            <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        
            <table>
            <tr>
                <td class="col1"><span class="item">Superficie Amp/Reduc Requerida</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:TextBox ID="SuperficieAmpReducRequerida" runat="server"></asp:TextBox><span class="item">[Ha.]</span>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorSuperficieAmpReducRequerida" runat="server" ControlToValidate="SuperficieAmpReducRequerida"  ValidationGroup="grupo1"
                         ErrorMessage="Superficie Amp/Reduc Requerida" Display="Static">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorSuperficieAmpReducRequerida" ControlToValidate="SuperficieAmpReducRequerida" ForeColor="Red"  ValidationGroup="grupo1" runat="server" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,9})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>

                </td>
            </tr>
            <tr>
                <td class="col1"><span class="item">Superficie Total Final</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:TextBox ID="SuperficieTotalFinal" runat="server"></asp:TextBox><span class="item">[Ha.]</span>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorSuperficieTotalFinal" runat="server" ControlToValidate="SuperficieTotalFinal"  ValidationGroup="grupo1"
                         ErrorMessage="Superficie Total Final" Display="Static">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorSuperficieTotalFinal" ControlToValidate="SuperficieTotalFinal" ForeColor="Red"  ValidationGroup="grupo1" runat="server" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,9})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>

                </td>
            </tr>
            <asp:Panel ID="PanelGuardarSuperficie"  Visible="false" runat="server">
            <tr>
                <td class="col3" colspan="3">
                    <asp:ImageButton ID="GuardarSuperficieSol" runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                    AlternateText="Guardar Superficie Solicitud" ToolTip="Guardar Superficie Solicitud" onclick="GuardarSuperficieSolicitud_Click" CausesValidation="true" ValidationGroup="grupo1"/>
                                <span class="item">Guardar Superficie Solicitud</span>
                </td>
            </tr>
            </asp:Panel>
            </table>
        
    </fieldset>
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>


        <!-- check interviene uts -->
    <asp:UpdatePanel ID="UpdatePanelCheck" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 

            <asp:Panel ID="PanelCheck"  Visible="true" runat="server">
                <uc0:checkTramitaUTS ID="checkTramitaUTS" runat="server" />
            </asp:Panel>

    </ContentTemplate>
    </asp:UpdatePanel>



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
           AllowPaging="True" PageSize="10" OnPageIndexChanging="GridViewSolicitudesPendientes_PageIndexChanged"
           AllowSorting="True"
           OnRowCreated="GridViewSolicitudesPendientes_RowCreated"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%" 
                onselectedindexchanged="GridViewSolicitudesPendientes_SelectedIndexChanged">
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
