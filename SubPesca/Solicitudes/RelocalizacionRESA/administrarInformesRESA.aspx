<%@ Page Language="C#"  MasterPageFile="~/Administrador/SitioAdmin.Master"  AutoEventWireup="true" CodeBehind="administrarInformesRESA.aspx.cs" 
Inherits="SubPesca.Solicitudes.RelocalizacionRESA.administrarInformesRESA" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Reference Page="ingresoInformeRESA.aspx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"></asp:ToolkitScriptManager>



<asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
   <ContentTemplate>    
       <asp:panel ID="Panel1" runat="server">
            <asp:ValidationSummary ID="ValidationSummaryErrores" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList"  />
        </asp:panel>
    </ContentTemplate>
</asp:UpdatePanel>


<asp:UpdatePanel ID="UpdatePanelMensajeSuperior" UpdateMode="Conditional" runat="server">
    <ContentTemplate>   
        <asp:Panel ID="PanelMensajeSuperior" CssClass="Content_msgGrilla" Visible="false" runat="server">
            <div class="msgGrilla_div2">
                <asp:Label ID="MensajeSuperior" runat="server"></asp:Label>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>



<asp:UpdatePanel ID="UpdatePanelErroresSuperior" UpdateMode="Conditional" runat="server">
    <ContentTemplate>   
        <asp:Panel ID="PanelErroresSuperior" CssClass="Content_msgGrilla" Visible="false" runat="server">
            <div class="msgGrilla_div2">
                <asp:Label ID="ErroresSuperior" runat="server"></asp:Label>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

  


<fieldset>
   

        <legend>Administrador de Informes RESA</legend>
        <br />
        

       
        <asp:UpdatePanel ID="UpdatePanelNumero" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelNumero"  Visible="true" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Número</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="Numero" autocomplete="tel-extension" runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="8"/></td>
                    </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelCodigoCentro" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelCodigoCentro"  Visible="true" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Código de Centro</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox ID="codigoCentro" runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="8"/></td>
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
                <asp:Button ID="Limpiar" runat="server" Text="<%$Resources:spanish.language,limpiar%>"      onclick="Limpiar_Click"  CausesValidation="false"     />
                <asp:Button ID="Buscar" runat="server" Text="Buscar"  CausesValidation="false"              onclick="Buscar_Click" style="height: 26px" />
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

        
    <asp:UpdatePanel ID="UpdatePanelInformes" UpdateMode="Conditional" runat="server">
        <ContentTemplate>      
            <asp:Panel ID="PanelInformes"  Visible="true" runat="server">

                <asp:GridView ID="GridInformes"  
                       runat="server"
                       AutoGenerateColumns="False" 
                       CellPadding="4" 
                       ForeColor="#333333" 
                       GridLines="None"
                       AllowPaging="True" 
                       PageSize="50" 
                       OnPageIndexChanging="GridInformes_PageIndexChanged"
                       CssClass="mGrid"
                       OnRowDataBound="GridInformes_RowDataBound"
                       OnRowCommand="GridInformes_RowCommand"
                       PagerStyle-CssClass="pgr"
                       Width="100%">

                        <Columns>

                          

                            <asp:TemplateField HeaderText="Número">
                                <ItemTemplate>
                                    <asp:Label HeaderText="idEstadoVigencia" ID="gEstadoVigencia" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "estadoVigencia.id") %>'></asp:Label>
                                    <%# DataBinder.Eval(Container, "DataItem.numero")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField HeaderText="Fecha" DataField="fecha" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField HeaderText="Codigos de Centro" DataField="codCentros" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />

                            

                          

                            <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="120px">
                                <ItemTemplate>
                                    
                                    <asp:ImageButton ID="gVer" Visible="true" runat="server" CausesValidation="false" CommandName="VerInforme" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idInformeRel") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver Informe" ToolTip="Ver Informe" />


                                    <asp:ImageButton ID="gNoVigente" Visible="false" runat="server" CausesValidation="false" CommandName="NoVigente" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idInformeRel") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Pasar a No Vigente" ToolTip="Pasar a No Vigente" />

                                    <asp:ImageButton ID="gVigente" Visible="false" runat="server" CausesValidation="false" CommandName="Vigente" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idInformeRel") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Pasar a Vigente" ToolTip="Pasar a Vigente" />


                                    <asp:ImageButton ID="gModificar" Visible="true" runat="server" CausesValidation="false" CommandName="ModificarInforme" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idInformeRel") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Ver Trámite" ToolTip="Modificar Informe" />
                                    
                                    <asp:ImageButton ID="gEliminar" Visible="true" runat="server" CausesValidation="false" CommandName="EliminarInforme" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idInformeRel") %>'
                                         ImageUrl="../../App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Ver Trámite" ToolTip="Eliminar Informe" />
                                    
                                    
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


</asp:Content>