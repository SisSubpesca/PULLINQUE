<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tramitesAsociadosConcesionComponente.ascx.cs" Inherits="SubPesca.Unidades.Concesion.tramitesAsociadosConcesionComponente" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

    <asp:ToolkitScriptManager ID="ToolkitScriptManagerSolicitante" runat="server" EnablePartialRendering="false"></asp:ToolkitScriptManager>
    
    <asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>
   
    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
               <span id="titulo_modulo"> Trámites Asociados 
                   <asp:Label ID="NombreUnidadEspacial" runat="server" Text="Label"></asp:Label></span>
            </td>
        </tr>
    </table>

    <hr style="width:100%;" />

    <fieldset>
    <asp:UpdatePanel ID="UpdatePanelDatosGeneral" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 
    <asp:Panel ID="PanelDatosGeneral"  Visible="true" runat="server">


    <table cellpadding="0px" cellspacing="0px">
    <tr>
        <td>
            
            <asp:UpdatePanel ID="UpdatePanelPestanaEnTramite" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 
                    <asp:LinkButton ID="lnk_EnTramite"  CssClass="tab1_selected"  runat="server" onclick="cambiaPestania_Click" CausesValidation="false">En Trámite</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td>
            
            <asp:UpdatePanel ID="UpdatePanelPestanaAprobada" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 
                    <asp:LinkButton ID="lnk_Aprobada"  CssClass="tab2" runat="server"  onclick="cambiaPestania_Click" CausesValidation="false">Aprobada</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        
        <td>
            <asp:UpdatePanel ID="UpdatePanelPestanaRechazada" UpdateMode="Conditional" runat="server">
            <ContentTemplate> 
                <asp:LinkButton ID="lnk_Rechazada"  CssClass="tab2" runat="server"  onclick="cambiaPestania_Click" CausesValidation="false">Rechazada</asp:LinkButton>
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    </table>

    <br />

    <asp:UpdatePanel ID="UpdatePanelEnTramite" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        
            <asp:Panel ID="PanelSolicitudesMsg"  Visible="false" runat="server">
                     <div class="msgGrilla_div1">
                        <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
                    </div>
                    <div class="msgGrilla_Solicitud">
                        <asp:Label ID="msgGrilla_Sol" runat="server"></asp:Label>
                    </div>
                   
            </asp:Panel>                        
            <asp:Panel ID="PanelEnTramite"  Visible="true" runat="server">
               
                
                <asp:GridView ID="GridEnTramite"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" 
                       PageSize="10" OnPageIndexChanging="GridEnTramite_PageIndexChanged"
                       CssClass="mGrid"
                       PagerStyle-CssClass="pgr"
                       OnRowDataBound="GridEnTramite_RowDataBound"
                       OnRowCommand="GridEnTramite_RowCommand"
                       Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Nº Pert">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.numPert")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fecha de Ingreso Trámite">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.fechaIngresoTramite")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Titulares">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.titularesCad")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tipo de Trámite">
                                <ItemTemplate>
                                      <%# DataBinder.Eval(Container, "DataItem.tipoTramite.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="120px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gVer" Visible="true" runat="server" CausesValidation="false" CommandName="VerTramite" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver Trámite" ToolTip="Ver Trámite" />
                                    
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


    <asp:UpdatePanel ID="UpdatePanelAprobada" UpdateMode="Conditional" runat="server">
        <ContentTemplate>      
            <asp:Panel ID="PanelAprobada"  Visible="false" runat="server">

                    <asp:GridView ID="GridAprobada"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" 
                       PageSize="10" OnPageIndexChanging="GridAprobada_PageIndexChanged"
                       CssClass="mGrid"
                       PagerStyle-CssClass="pgr"
                       OnRowDataBound="GridAprobada_RowDataBound"
                       OnRowCommand="GridAprobada_RowCommand"
                       Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Nº Pert">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.numPert")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fecha de Ingreso Trámite">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.fechaIngresoTramite")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Titulares">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.titularesCad")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tipo de Trámite">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.tipoTramite.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="120px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gVer" Visible="true" runat="server" CausesValidation="false" CommandName="VerTramite" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver Trámite" ToolTip="Ver Trámite" />
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


    <asp:UpdatePanel ID="UpdatePanelRechazada" UpdateMode="Conditional" runat="server">
        <ContentTemplate>      
            <asp:Panel ID="PanelRechazada"  Visible="false" runat="server">

                <asp:GridView ID="GridRechazada"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" 
                       PageSize="10" OnPageIndexChanging="GridRechazada_PageIndexChanged"
                       CssClass="mGrid"
                       PagerStyle-CssClass="pgr"
                       OnRowDataBound="GridRechazada_RowDataBound"
                       OnRowCommand="GridRechazada_RowCommand"
                       Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Nº Pert">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.numPert")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fecha de Ingreso Trámite">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.fechaIngresoTramite")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Titulares">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.titularesCad")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tipo de Trámite">
                                <ItemTemplate>
                                     <%# DataBinder.Eval(Container, "DataItem.tipoTramite.descripcion")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="120px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="gVer" Visible="true" runat="server" CausesValidation="false" CommandName="VerTramite" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver Trámite" ToolTip="Ver Trámite" />
                                    
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

    <br />

    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    </fieldset>