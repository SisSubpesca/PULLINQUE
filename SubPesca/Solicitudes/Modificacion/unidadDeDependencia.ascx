<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="unidadDeDependencia.ascx.cs" Inherits="SubPesca.Solicitudes.Modificacion.unidadDeDependencia" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%--
<fieldset>

    <legend>Unidades de Dependencia</legend>
    <br />

    <table class="form" cellpadding="0px" cellspacing="0px" style="display:none;">
    <tr>
        <td>&nbsp;IdSolicitud: <asp:TextBox ID="IdSolicitud" runat="server"></asp:TextBox></td>
    </tr>
    </table>


        <asp:Panel ID="PanelFormularioIngreso" Visible="true" runat="server">

            <asp:UpdatePanel ID="UpdatePanelUnidDepend" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla" runat="server"></asp:Label>
                </div>
                </asp:Panel>
                

            <asp:ValidationSummary ID="ValidationSummaryIngresoSolicitante" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupoUnidDependencia" />
                
            </ContentTemplate>
            </asp:UpdatePanel>
            
            <asp:UpdatePanel ID="UpdatePanelUnidDependencia" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            <asp:Panel ID="PanelUnidDependencia"  Visible="true" runat="server">
            <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Unidad de Dependencia</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                        <asp:DropDownList ID="UnidadDeDependencia"  runat="server" onselectedindexchanged="unidadDeDependencia_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>
                        </td>
                    </tr>

            </table>
            </asp:Panel>
            </ContentTemplate>
            </asp:UpdatePanel>
                
       <asp:UpdatePanel ID="UpdatePanelUnidadDependencia" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            <asp:Panel ID="PanelUnidadDependencia"  Visible="false" runat="server">
                 
            <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Tipo de Unidad de Dependencia</span></td>   
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3" colspan="2">
                        <asp:DropDownList ID="TipoUnidadDependencia"  runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoUnidadDependencia" runat="server" ControlToValidate="TipoUnidadDependencia"  ValidationGroup="grupoUnidDependencia"
                                ErrorMessage="Tipo de Unidad de Dependencia" Display="Static" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1"><span class="item">Detalle de Unidad de Dependencia</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3" colspan ="2">
                        <asp:UpdatePanel ID="UpdatePanelDetalleUnidadDependencia" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="DetalleUnidadDependencia" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorDetalleUnidadDependencia" runat="server" ControlToValidate="DetalleUnidadDependencia"  ValidationGroup="grupoUnidDependencia"
                                ErrorMessage="Detalle de Unidad de Dependencia" Display="Static" InitialValue="0">*</asp:RequiredFieldValidator>

                            </ContentTemplate>
                        </asp:UpdatePanel> 
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">&nbsp;</td>
                        <td class="col2">&nbsp;</td>
                        <td class="col3">
                            <asp:ImageButton ID="GuardarUnindadDependencia" runat="server" 
                                ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                AlternateText="Guardar Unidad de Dependencia" 
                                ToolTip="Guardar Unidad de Dependencia" CausesValidation="true" 
                                ValidationGroup="grupoUnidDependencia" onclick="GuardarUnindadDependencia_Click" />
                            Guardar Detalle
                        <td class="col3">&nbsp;</td>
                    </tr>
            </table>

        </asp:Panel>
        </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="UnidadDeDependencia" EventName="SelectedIndexChanged" />
        </Triggers>
        </asp:UpdatePanel>

        </asp:Panel>

            
        <asp:UpdatePanel ID="UpdatePanelGrillaUnidadDependencia" UpdateMode="Conditional" runat="server">
              <ContentTemplate>
              <asp:Panel ID="PanelGrillaUnidadDependencia"  Visible="true" runat="server">

              <asp:GridView 
               ID="GridUnidadDependencia"
               runat="server"
               AutoGenerateColumns="False" 
               CellPadding="4" 
               ForeColor="#333333" 
               GridLines="None"
               AllowSorting="True" 
               CssClass="mGrid"
               OnRowDataBound="GridUnidadDependencia_RowDataBound"
               OnRowCommand="GridUnidadDependencia_RowCommand"
               PagerStyle-CssClass="pgr"
               Width="100%">
               <RowStyle BackColor="#EFF3FB" />
               <Columns>
               <asp:TemplateField HeaderText="Orden de Dependencia" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
               
               
                    <asp:HiddenField ID="gUnidadDependencia" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.idUnidadDependencia") %>' />
                    <%# DataBinder.Eval(Container, "DataItem.index")%>       
               </ItemTemplate>
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Tipo Unidad de Dependencia" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.tipoUnidDependencia.descripcion")%>     
               </ItemTemplate>
               </asp:TemplateField>
                          
                <asp:TemplateField HeaderText="Detalle de Unidad de Dependencia" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.claveUnidDependencia")%>       
                </ItemTemplate>
               </asp:TemplateField>

                <asp:TemplateField HeaderText="Estado Actual" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.estadoSolicitud.descripcion")%> 
                </ItemTemplate>
               </asp:TemplateField>
                             
               <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idUnidadDependencia") %>'
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


        <asp:Panel ID="PanelBotonGuardar" Visible="true" runat="server">

            <asp:UpdatePanel ID="UpdatePanelBotonUnidadDependencia" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="PanelBotonUnidadDependencia"  Visible="true" runat="server">
                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="col1"><asp:Button ID="ButtonUnidadDependencia" runat="server" Text="Guardar" onclick="ButtonUnidadDependencia_Click"
                                /></td>   
                        </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

        </asp:Panel>

    

</fieldset>
--%>