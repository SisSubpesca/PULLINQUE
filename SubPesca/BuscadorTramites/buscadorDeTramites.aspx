<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="buscadorDeTramites.aspx.cs" 
Inherits="SubPesca.BuscadorTramites.buscadorDeTramites" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>


<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"></asp:ToolkitScriptManager>

<fieldset>

        <legend>Buscar Trámite</legend>

        <asp:ValidationSummary ID="ValidationSummaryFormularioBuscar" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
        
        <br />
        
        <asp:UpdatePanel ID="UpdatePanelNumero" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelNumero"  Visible="true" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1" colspan ="3"><span class="item">Ingrese número pert, identificador de solicitud o código de centro para realizar su búsqueda:</span></td>
                        
                    </tr>
                    <tr>
                        <td class="col1"><span class="item">Identificador</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            <asp:TextBox ID="Identificador" runat="server" />
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorIdentificador" runat="server" ControlToValidate="Identificador"  ValidationGroup="grupo1" ErrorMessage="Identificador" Display="Static">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <br />
             
        <table class="form" cellpadding="0px" cellspacing="0px">   
        <tr>
            <td class="col1"></td>
            <td class="col2"></td>
            <td class="col3">
                <asp:Button ID="Buscar" runat="server" Text="Buscar"  CausesValidation="true" ValidationGroup="grupo1"  onclick="Buscar_Click" style="height: 26px" />
            </td>
        </tr>
        </table>

    <asp:UpdatePanel ID="UpdatePanelErroresGrilla" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelErroresGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="ErroresGrilla" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

     <asp:UpdatePanel ID="UpdatePanelUE" UpdateMode="Conditional" runat="server">
     <ContentTemplate>
     
            <asp:Panel ID="PanelUE"  Visible="true" runat="server">

                <asp:GridView ID="GridUE"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" 
                       PageSize="30" 
                       OnPageIndexChanging="GridUE_PageIndexChanged"
                       CssClass="mGrid"
                       PagerStyle-CssClass="pgr"
                       Width="100%">

                        <Columns>
                        
                            <asp:TemplateField HeaderText="Identificador">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.numPert")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Numero Pert">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.numPert")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Codigo de Centro">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.unidadEspacial.centrosDeCultivo.codigoCentro")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo Unidad Espacial">
                                <ItemTemplate>
                                    <%# System.Web.HttpUtility.HtmlEncode(DataBinder.Eval(Container, "DataItem.tipoUnidadEspacial.descripcion")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo de Tramite">
                                <ItemTemplate>
                                    <%# System.Web.HttpUtility.HtmlEncode(DataBinder.Eval(Container, "DataItem.tipoTramite.descripcion"))%> - <%# System.Web.HttpUtility.HtmlEncode(DataBinder.Eval(Container, "DataItem.tipoModSolicitud"))%> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Titulares">
                                <ItemTemplate>
                                    <%# System.Web.HttpUtility.HtmlEncode(DataBinder.Eval(Container, "DataItem.titularesCad"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                    <%# System.Web.HttpUtility.HtmlEncode(DataBinder.Eval(Container, "DataItem.region.descripcion"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comuna">
                                <ItemTemplate>
                                    <%# System.Web.HttpUtility.HtmlEncode(DataBinder.Eval(Container, "DataItem.comunasCad"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo">
                                <ItemTemplate>
                                    <%# System.Web.HttpUtility.HtmlEncode(DataBinder.Eval(Container, "DataItem.tipoSolicitudCad"))%>
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
                   <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click"  Visible="false" />  
                   
            </asp:Panel>                  

        </ContentTemplate> 
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
        </Triggers>   
        </asp:UpdatePanel>
        
</fieldset>




</asp:Content>