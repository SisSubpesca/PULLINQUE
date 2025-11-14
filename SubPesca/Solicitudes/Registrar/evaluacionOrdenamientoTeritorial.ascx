<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="evaluacionOrdenamientoTeritorial.ascx.cs" Inherits="SubPesca.Solicitudes.Registrar.evaluacionOrdenamientoTeritorial"  %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>


<asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
<ContentTemplate>    
<asp:panel ID="PanelMensajesValidaciones" Visible ="false" runat="server">
    
    <asp:ValidationSummary ID="ValidationSummaryTipoPendiente" CssClass="valSum" style="color:#772222;" runat="server" DisplayMode="BulletList"/>
    <asp:ValidationSummary ID="ValidationSummarySupeditado" CssClass="valSum" style="color:#772222;" runat="server" DisplayMode="BulletList"/>
    <asp:ValidationSummary ID="ValidationSummaryEvaluacionOrdenamientoTerr" CssClass="valSum" style="color:#772222;" runat="server" DisplayMode="BulletList" ValidationGroup="grupo1" />
    
</asp:panel>
</ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="UpdatePanelMensajeEvalUnidOrdenamTerr" UpdateMode="Conditional" runat="server">
    <ContentTemplate>   
        <asp:Panel ID="PanelMensajeEvalUnidOrdenamTerr" CssClass="Content_msgGrilla" Visible="false" runat="server">
            <div class="msgGrilla_div2">
                <asp:Label ID="MensajeEvalUnidOrdenamTerr" runat="server"></asp:Label>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

<fieldset>

           
           <table class="form" cellpadding="0px" cellspacing="0px" style="display:none;">
        <tr>
            <td>&nbsp;IdSolicitud: <asp:TextBox ID="IdSolicitud" runat="server"></asp:TextBox></td>
            <td>&nbsp;idEvaluacionUOT: <asp:TextBox ID="IdEvaluacionUOT" runat="server"></asp:TextBox></td>
        </tr>
        </table>
        
           <legend>Evaluación Unidad Ordenamiento Territorial para Colector</legend>
           
           <br />
        
           <asp:UpdatePanel ID="UpdatePanelEvalUnidOrdenamTerr" UpdateMode="Conditional" runat="server">
           <ContentTemplate> 

           <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1"><span class="item">Requiere IT U.O.T</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:DropDownList ID="RequiereIT_UOT" runat="server" 
                        onselectedindexchanged="RequiereIT_UOT_SelectedIndexChanged" AutoPostBack="true" CausesValidation="true"></asp:DropDownList>
                    
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorRequiereIT_UOT" runat="server" ControlToValidate="RequiereIT_UOT"  ValidationGroup="grupo1"
                        ErrorMessage="Requiere IT U.O.T" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>

            <asp:Panel ID="PanelResultado" Visible="false" runat="server">
            <tr>
                <td class="col1"><span class="item">Resultado</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:DropDownList ID="Resultado" runat="server" 
                        onselectedindexchanged="Resultado_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </td>
           </tr>
           </asp:Panel>

           </table>

           <br />

           <asp:Panel ID="PanelPendiente" Visible="false" runat="server">
            
            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1"><span class="item">Tipo Pendiente</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">

                    <asp:UpdatePanel ID="UpdatePanelTipoPendiente" UpdateMode="Conditional" runat="server">
                    <ContentTemplate> 
                        
                        <asp:Panel ID="PanelTipoPendiente"  Visible="true" runat="server">
                            <asp:DropDownList ID="TipoPendiente" runat="server"></asp:DropDownList>
                        </asp:Panel>
                    
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <asp:ImageButton ID="agregarGrupoSuspendido" 
                                runat="server" ImageUrl="~/App_Themes/admin_style/images/editar_nombre.jpg" 
                                AlternateText="Agregar Grupo Suspendido" ToolTip="Agregar Grupo Suspendido" 
                                style="width: 20px" Height="20px" Visible = "true" 
                                OnClientClick="javascript:abre_dialogo('agregarGrupoSusp','1')" />

                </td>
                
            </tr>
            <tr>
                <td></td>
                <td colspan="2">
                    <asp:ImageButton ID="GuardarPendiente" Visible="true" runat="server" 
                        ImageUrl="../../App_Themes/admin_style/images/add.png" Height="20px" 
                        AlternateText="Agregar Unidad de Dependencia Pendiente" 
                        ToolTip="Agregar Unidad de Dependencia Pendiente" 
                        onclick="GuardarPendiente_Click" style="width: 20px"
                     /><span class="item"> Guardar Tipo Pendiente</span>
                </td>
            </tr>
            </table>
            
           <asp:UpdatePanel ID="UpdatePanelGrillaPendientes" UpdateMode="Conditional" runat="server">
              <ContentTemplate>
              <asp:Panel ID="PanelGrillaPendientes"  Visible="true" runat="server">

              <asp:GridView 
               ID="GridPendientes"
               runat="server"
               AutoGenerateColumns="False" 
               CellPadding="4" 
               ForeColor="#333333" 
               GridLines="None"
               AllowSorting="True" 
               CssClass="mGrid"
               OnRowDataBound="GridPendientes_RowDataBound"
               OnRowCommand="GridPendientes_RowCommand"
               PagerStyle-CssClass="pgr"
               Width="100%">
               <RowStyle BackColor="#EFF3FB" />
               <Columns>
               
               <asp:TemplateField HeaderText="Tipo Pendiente" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                        <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                        <%# DataBinder.Eval(Container, "DataItem.grupoSuspendido.nombreGrupoSuspend")%>
               </ItemTemplate>
               </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Estado" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.estadoVigencia.descripcion")%>
                </ItemTemplate>
               </asp:TemplateField>
                             
               <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "index") %>'
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

           </asp:Panel>

           <asp:Panel ID="PanelSupeditado" Visible="false" runat="server">

            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1"><span class="item">Tipo de Supeditado</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:DropDownList ID="TipoSupeditado" runat="server" 
                        onselectedindexchanged="TipoSupeditado_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </td>
                
            </tr>

            <asp:Panel ID="PanelPert" Visible="false" runat="server">
            <tr>
                <td class="col1"><span class="item">Pert/Identificador de la Solicitud</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:TextBox ID="Pert" runat="server"></asp:TextBox>
                </td>
                
            </tr>
            </asp:Panel>

            <asp:Panel ID="PanelObservaciones" Visible="false" runat="server">
            <tr>
                <td class="col1"><span class="item">Observaciones</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:TextBox  ID="Observaciones" TextMode="multiline" Columns="50" Rows="5" runat="server" AutoPostBack="false"></asp:TextBox>
                </td>
                
            </tr>
            </asp:Panel>

            <tr>
                <td></td>
                <td colspan="2">
                    <asp:ImageButton ID="GuardarSupeditado" Visible="true" runat="server" 
                        ImageUrl="../../App_Themes/admin_style/images/add.png" Height="20px" 
                        AlternateText="Guardar Supeditado" ToolTip="Guardar Supeditado" 
                        onclick="GuardarSupeditado_Click"/><span class="item"> Guardar Tipo Supeditado</span>
                </td>
            </tr>
            </table>

            <asp:UpdatePanel ID="UpdatePanelGrillaSupeditados" UpdateMode="Conditional" runat="server">
              <ContentTemplate>
              <asp:Panel ID="PanelGrillaSupeditados"  Visible="true" runat="server">

              <asp:GridView 
               ID="GridSupeditados"
               runat="server"
               AutoGenerateColumns="False" 
               CellPadding="4" 
               ForeColor="#333333" 
               GridLines="None"
               AllowSorting="True" 
               CssClass="mGrid"
               OnRowDataBound="GridSupeditados_RowDataBound"
               OnRowCommand="GridSupeditados_RowCommand"
               PagerStyle-CssClass="pgr"
               Width="100%">
               <RowStyle BackColor="#EFF3FB" />
               <Columns>
               
               <asp:TemplateField HeaderText="Tipo Supeditado" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                        <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                        <%# DataBinder.Eval(Container, "DataItem.tipoSupeditado.descripcion")%>
               </ItemTemplate>
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Pert/Identificador de la Solicitud" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.solicitudConcesionDep.numPert")%>
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Resultado UOT" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                        
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Resultado SSP" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                        
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Observaciones" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.observaciones")%>
               </ItemTemplate>
               </asp:TemplateField>
                             
               <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                    <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "index") %>'
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

           </asp:Panel>
     
           <br />
                
           <table cellpadding="0px" cellspacing="0px" class="form">
                    <tr>
                        <td>
                            <asp:Button ID="GuardarEvaluacion" runat="server" Text="Guardar Evaluación" 
                            onclick="GuardarEvaluacion_Click" ToolTip="Guardar Evaluación" Visible="true" CausesValidation="true" ValidationGroup="grupo1"/>

                        </td>
                       
                    </tr>
           </table>

           </ContentTemplate>
           </asp:UpdatePanel>

</fieldset>


        
<!-- Diálogo -->
<div id="agregarGrupoSusp" class="dialog">
        <div class="background"></div>
        <div class="content_dialog">
            <div class="top">
                <asp:LinkButton ID="cerrar_agregarGrupoSusp" CssClass="cerrar" OnClientClick="javascript:close_dialog('agregarGrupoSusp');" CausesValidation="false" runat="server"></asp:LinkButton>
            </div>
            <div class="body">
                <fieldset>
                    <legend>Agregar Grupo Suspendido</legend>
                    <iframe id="iframe_agregarGrupoSusp" src="" width="500px" height="300px" frameborder="0" scrolling="no"></iframe>
                </fieldset>
            </div>
        </div>
        </div>
