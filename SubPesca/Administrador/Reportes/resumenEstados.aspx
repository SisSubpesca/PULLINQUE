<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="resumenEstados.aspx.cs" 
         Inherits="SubPesca.Administrador.Reportes.resumenEstados" Theme="admin_style" %>
         
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <fieldset>
        <legend>Filtro de búsqueda</legend>
        <br />

        <asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
           <ContentTemplate>    
               <asp:panel ID="Panel1" runat="server">
                    <asp:ValidationSummary ID="ValidationSummaryErrores" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="FormErrores"  />
                </asp:panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1"><span class="item">Tipo de Solicitud</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="TiposSolicitud" runat="server" OnSelectedIndexChanged="TiposTramite_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList> *
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr>
            <td class="col1"><span class="item">Estado:</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Estado" runat="server"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>


        <tr>
            <td class="col1"><span class="item">N° Pert/Identificador Solicitud:</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="Pert" runat="server" MaxLength="20"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>

        <asp:Panel ID="PanelSectorialista" runat="server" visible="false">
            <tr>
                <td class="col1"><span class="item">Sectorialista</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="Sectorialistas" runat="server"></asp:DropDownList> 
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </asp:Panel>


        <tr>
            <td class="col1"><span class="item">Región</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="upd1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Regiones" AutoPostBack="true" OnSelectedIndexChanged="Regiones_OnSelectedIndexChanged" runat="server"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Provincia</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="upd2" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Provincias" AutoPostBack="true" OnSelectedIndexChanged="Provincias_OnSelectedIndexChanged" runat="server"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Regiones" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>                    
            </td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Comuna</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="upd3" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Comunas" runat="server"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Regiones" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Provincias" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr>
            <td class="col1"><span class="item">¿Mostrar solo Islas?</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:CheckBox ID="checkIslas" runat="server"></asp:CheckBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr>
            <td class="col1"><span class="item">¿Mostrar solo solicitudes con Check marcado IT UOT Aprueba?</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:CheckBox ID="checkAvanzaAprueba" runat="server"></asp:CheckBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
      
        <tr>
            <td class="col1"></td>
            <td class="col2"></td>
            <td class="col3">
                <asp:Button ID="Limpiar" runat="server" OnClick="Limpiar_Click" Text="Limpiar" />
                <asp:Button ID="Filtrar" runat="server" OnClick="Filtrar_Click" Text="Filtrar" />
            </td>
        </tr>
        </table>
    </fieldset>
    
   
    <fieldset>
        <legend>Resumen de estados de solicitudes</legend>
        
        <table cellpadding="0px" cellspacing="0px">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 
                    <asp:LinkButton ID="lnk_Tramite" OnClick="cambiaGrilla_Click" CssClass="tab1_selected" runat="server">Solicitudes En Trámite</asp:LinkButton>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnk_Rechazados" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 
                    <asp:LinkButton ID="lnk_Rechazados" OnClick="cambiaGrilla_Click" CssClass="tab2" runat="server">Solicitudes Rechazadas</asp:LinkButton>
                </ContentTemplate>
                 <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnk_Tramite" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        </table>

        <asp:UpdatePanel ID="upd4" UpdateMode="Conditional" runat="server">
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
                DataKeyNames="IdEstado"
                AllowPaging="False"
                AllowSorting="true"
                OnSorting="GridTramite_Sorting"
                OnRowCommand="GridTramite_RowCommand"
                OnRowDataBound="GridTramite_RowDataBound"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="Nº Estado" DataField="Numero" SortExpression="Numero" ItemStyle-Width="57px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Estado" DataField="Estado" SortExpression="Estado"  ItemStyle-Width="560px" />
                    <asp:BoundField HeaderText="Unidad"  DataField="unidadResponsable" SortExpression="unidadResponsable"/>
                    <asp:BoundField HeaderText="Requerimientos Pendientes"  DataField="docReq"/>
                    <asp:TemplateField HeaderText="Total Solicitudes" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:LinkButton ID="gTotal_Solicitudes" CommandName="Total" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdEstado") %>' 
                                 Text='<%# DataBinder.Eval(Container.DataItem, "total_solicitudes") %>' runat="server"></asp:LinkButton>
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
            <asp:HiddenField ID="KeySort1" runat="server" /> 
            

          <asp:GridView ID="GridRechazadas" runat="server" 
                AutoGenerateColumns="False" 
                CellPadding="4" 
                ForeColor="#333333" 
                GridLines="None"
                DataKeyNames="IdEstado"
                AllowPaging="False"
                AllowSorting="true"
                OnSorting="GridRechazadas_Sorting"
                OnRowCommand="GridRechazadas_RowCommand"
                OnRowDataBound="GridRechazadas_RowDataBound"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="Nº Estado" DataField="Numero" SortExpression="Numero" ItemStyle-Width="57px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Estado" DataField="Estado" SortExpression="Estado" ItemStyle-Width="560px" />
                    <asp:BoundField HeaderText="Unidad"  DataField="unidadResponsable" SortExpression="unidadResponsable"/>
                    <asp:BoundField HeaderText="Requerimientos Pendientes"  DataField="docReq"/>
                    <asp:TemplateField HeaderText="Total Solicitudes" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:LinkButton ID="gTotal_Solicitudes" CommandName="Total" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdEstado") %>' 
                                 Text='<%# DataBinder.Eval(Container.DataItem, "total_solicitudes") %>' runat="server"></asp:LinkButton>
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
            <asp:HiddenField ID="KeySort2" runat="server" /> 
            
            <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" /> 
        </ContentTemplate> 
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnk_Tramite"       EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnk_Rechazados"    EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Filtrar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
        </Triggers>
        </asp:UpdatePanel>


    </fieldset>


    <fieldset>
        <legend>Recurso de Reposición</legend>

        <table cellpadding="0px" cellspacing="0px">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 
                    <asp:LinkButton ID="LinkButton1" CssClass="tab1_selected" runat="server">Solicitudes en Recurso de Reposición o a la espera de Reconstitución de Expediente</asp:LinkButton>
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        </table>


        <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="Content_msgGrillaReposicion" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrillaReposicion" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrillaReposicion" runat="server"></asp:Label>
                </div>
            </asp:Panel>
                                                      
            <asp:GridView ID="GridReposicion" runat="server" 
                AutoGenerateColumns="False" 
                CellPadding="4" 
                ForeColor="#333333" 
                GridLines="None"
                DataKeyNames="IdEstado"
                AllowPaging="False"
                AllowSorting="true"
                OnSorting="GridReposicion_Sorting"
                OnRowCommand="GridReposicion_RowCommand"
                OnRowDataBound="GridReposicion_RowDataBound"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="Nº Estado" DataField="Numero"  SortExpression="Numero" ItemStyle-Width="57px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Estado" DataField="Estado" SortExpression="Estado" ItemStyle-Width="560px" />
                    <asp:BoundField HeaderText="Unidad"  DataField="unidadResponsable"  SortExpression="unidadResponsable"/>
                    <asp:BoundField HeaderText="Requerimientos Pendientes"  DataField="docReq"/>
                    <asp:TemplateField HeaderText="Total Solicitudes" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:LinkButton ID="gTotal_Solicitudes" CommandName="Total" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdEstado") %>' 
                                 Text='<%# DataBinder.Eval(Container.DataItem, "total_solicitudes") %>' runat="server"></asp:LinkButton>
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
            <asp:HiddenField ID="KeySort3" runat="server" /> 
        
            <asp:Button ID="ButtonExportarReposicion" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrillaReposicion_Click" Visible="false" /> 
        </ContentTemplate> 
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Filtrar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            <asp:PostBackTrigger ControlID="ButtonExportarReposicion" />
        </Triggers>
        </asp:UpdatePanel>
        
    </fieldset>

</asp:Content>
