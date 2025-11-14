<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerGrupoSuspendido.aspx.cs" Inherits="SubPesca.Mantenedores.GrupoSuspendido.VerGrupoSuspendido" 
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
                <asp:DropDownList ID="TipoAgrupacion" runat="server"></asp:DropDownList> *

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
            <asp:TextBox ID="CodigoCentro" runat="server"></asp:TextBox>
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

    <asp:Panel ID="PanelArchivoAdjuntoOculto"  Visible="true" runat="server">
    <fieldset>
    <legend>Archivo Adjunto</legend>

    <asp:Panel ID="PanelArchivoAdjuntoGrupo" CssClass="Content_msgGrilla" Visible="false" runat="server">
        <div class="msgGrilla_div2">
            <asp:Label ID="LabelArchivoAdjuntoGrupo" runat="server"></asp:Label>
        </div>
    </asp:Panel>

    <asp:ValidationSummary ID="ValidationSummaryArchivoAdjuntoGrupo" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo2" />
    
  

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
                                 <asp:TemplateField HeaderText="Vigencia">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.estadoVigencia.descripcion")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="¿Es Documento Final?">
                                    <ItemTemplate>
                                        <%# Eval("docFinal").ToString().ToLower() == "true" ? "Sí" : "No"%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="gDescargar" Visible="true" runat="server" CausesValidation="false" CommandName="Descargar" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.idArchivo") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" OnPreRender="ImgAdd_PreRender" />
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

    <asp:Panel ID="PanelAsociacionUE"  Visible="true" runat="server">
    <fieldset>
    <legend>Asociación con Solicitudes UE</legend>

    <asp:Panel ID="PanelAsociacionSolUE" CssClass="Content_msgGrilla" Visible="false" runat="server">
        <div class="msgGrilla_div2">
            <asp:Label ID="LabelAsociacionSolUE" runat="server"></asp:Label>
        </div>
    </asp:Panel>

    <asp:ValidationSummary ID="ValidationSummaryAsociacionSolUE" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo3" />
    
    

    <asp:UpdatePanel ID="UpdatePanelAsociacionSolUE" UpdateMode="Conditional" runat="server">
    <ContentTemplate>

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
                            >
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
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

    <br />

    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1" colspan="6">
           
        </td>
    </tr>
    </table>
</asp:Content>