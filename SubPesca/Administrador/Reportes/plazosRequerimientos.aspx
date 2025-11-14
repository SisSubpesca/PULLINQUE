<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="plazosRequerimientos.aspx.cs" 
         Inherits="SubPesca.Administrador.Reportes.plazosRequerimientos" Theme="admin_style" %>

         
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
            <td class="col1"><span class="item">Tipo de solicitud de Unidad Espacial</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="TiposSolicitud" runat="server"></asp:DropDownList> *
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
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
            <td class="col1"><span class="item">Destinatario</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="TiposDestinatario" runat="server"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr>
            <td class="col1"><span class="item"></span></td>
            <td class="col2"><span class="item"></span></td>
            <td class="col3">

                <asp:Panel ID="Panel2" CssClass="Content_msgGrilla" Visible="true" runat="server">
                    <div class="msgGrilla_div2">
                        No se consideran requerimientos de solicitudes con Resolución SSP o Resolución SSFFAA Vigente.
                    </div>
                </asp:Panel>
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
                <asp:LinkButton ID="lnk_PorVencer" OnClick="cambiaGrilla_Click" CssClass="tab1_selected" runat="server">Requerimientos por Vencer</asp:LinkButton>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnk_Vencidos" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>
            </td>
            <td>
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
            <ContentTemplate> 
                <asp:LinkButton ID="lnk_Vencidos" OnClick="cambiaGrilla_Click" CssClass="tab2" runat="server">Requerimientos Vencidos</asp:LinkButton>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnk_PorVencer" EventName="Click" />
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

                                                      
            <asp:GridView ID="GridView1" runat="server" 
                AutoGenerateColumns="False" 
                CellPadding="4" 
                ForeColor="#333333" 
                GridLines="None"
                DataKeyNames="nombrePestana"
                AllowPaging="false"
                AllowSorting="true" 
                OnRowCommand="GridView_RowCommand"
                OnRowDataBound="GridView_RowDataBound"
                OnSorting="GridView_Sorting"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr" >
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="Pestaña" DataField="nombrePestana" SortExpression="nombrePestana" ItemStyle-Width="57px"  ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Sección" DataField="nombreSeccion" SortExpression="nombreSeccion" ItemStyle-Width="57px" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Requerimiento" DataField="nombreSubRequerimiento" SortExpression="nombreSubRequerimiento" ItemStyle-HorizontalAlign="Left" />
                    <asp:TemplateField HeaderText="Total Solicitudes" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:LinkButton ID="gTotal_Solicitudes" CommandName="commandPorVencer" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Numero") %>' 
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
            
            <asp:GridView ID="GridView2" runat="server" 
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                DataKeyNames="Numero"
                AllowPaging="False"
                AllowSorting="true"
                OnRowCommand="GridView_RowCommand"
                OnRowDataBound="GridView_RowDataBound"
                OnSorting="GridView2_Sorting"
                CssClass="mGrid"
                PagerStyle-CssClass="pgr" >
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="Pestaña" DataField="nombrePestana" SortExpression="nombrePestana" ItemStyle-Width="57px"  ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Sección" DataField="nombreSeccion" SortExpression="nombreSeccion" ItemStyle-Width="57px" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Requerimiento" DataField="nombreSubRequerimiento" SortExpression="nombreSubRequerimiento"  ItemStyle-HorizontalAlign="Left" />
                    <asp:TemplateField HeaderText="Total Solicitudes" SortExpression="total_solicitudes" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:LinkButton ID="gTotal_Solicitudes" CommandName="commandVencidos" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Numero") %>' 
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
            <asp:AsyncPostBackTrigger ControlID="lnk_PorVencer" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnk_Vencidos" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Filtrar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
        </Triggers>
        </asp:UpdatePanel>
        
    </fieldset>

</asp:Content>
