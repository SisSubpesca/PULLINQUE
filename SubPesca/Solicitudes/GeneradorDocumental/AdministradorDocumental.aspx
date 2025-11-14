<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" CodeBehind="AdministradorDocumental.aspx.cs" 
    AutoEventWireup="true" Theme="admin_style" Inherits="SubPesca.Solicitudes.GeneradorDocumental.AdministradorDocumental" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>


<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">

    <asp:Panel ID="Panel1"  Visible="true" runat="server">
        <fieldset>

        <legend>Administrador de Documentos</legend>

        <br />
            
           <asp:Panel ID="PanelNombrePlantilla" runat="server" Visible="true">
                <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Nombre Plantilla *</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox  ID="NombrePlantilla" runat="server" AutoPostBack="false"></asp:TextBox></td>
                    </tr>
                </table>
           </asp:Panel>

           <asp:Panel ID="PanelDescripcionPlantilla" runat="server" Visible="true">
                <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Descripcion Plantilla *</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox  ID="DescripcionPlantilla" runat="server" AutoPostBack="false"></asp:TextBox></td>
                    </tr>
                </table>
           </asp:Panel>
           <br />
           <asp:Panel ID="PanelTipoDocumento"  Visible="true" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Tipo de Tramite *</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:ListBox ID="TiposSolicitud" runat="server" SelectionMode="Multiple"></asp:ListBox></td>
                    </tr>
                </table>
           </asp:Panel>
           <br />
           <asp:Panel ID="PanelArchivo"  Visible="true" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="ArchivoAdjuntoLiteral" runat="server" Text="<%$Resources:spanish.language,archivoAdjunto%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td><td class="col3"><asp:FileUpload ID="ArchivoAdjunto" MaxLength="40" Width="200px" runat="server"></asp:FileUpload>&nbsp;<asp:Label ID="RequeridoArchivoAdjunto" runat="server"></asp:Label><asp:RegularExpressionValidator ID="REGEXFileUploadLogo" runat="server" ErrorMessage="Formato Archivo Incorrecto" ControlToValidate="ArchivoAdjunto" ValidationExpression= "(.*).(.docx|.DOCX)$" />*</td>
                    </tr>
                </table>
           </asp:Panel>

           <table class="form" cellpadding="0px" cellspacing="0px">   
                <tr>
                    <td class="col1"></td>
                    <td class="col2"></td>
                    <td class="col3">
                        <asp:Button ID="Limpiar" runat="server" Text="Limpiar"  CausesValidation="false" OnClick="Limpiar_Click"  />
                        <asp:Button ID="Guardar" runat="server" Text="Guardar"  CausesValidation="true"  OnClick="Guardar_Click" />
                    </td>
                </tr>
           </table>
           
            <!--<Triggers>
                <asp:PostBackTrigger ControlID="Guardar"/>
            </Triggers>-->
            <!--AllowPaging="True" PageSize="10" OnPageIndexChanging="GridPlanillas_PageIndexChanged"-->
            <!-- OnRowDataBound="GridPlanillas_RowDataBound"
        OnRowCommand="GridPlanillas_RowCommand"-->
        <asp:GridView ID="GridPlanillas" 
        runat="server"
        AutoGenerateColumns="False" 
        CellPadding="4" 
        ForeColor="#333333" 
        GridLines="None"
        AllowPaging="True" PageSize="10" OnPageIndexChanging="GridPlanillas_PageIndexChanged"
        CssClass="mGrid"
        OnRowDataBound="GridPlanillas_RowDataBound"
        OnRowCommand="GridPlanillas_RowCommand"
        PagerStyle-CssClass="pgr"
        Width="100%">
            <Columns>
                <asp:TemplateField Visible=false>
                    <ItemTemplate>
                        <asp:Label HeaderText="EstadoVigencia" ID="EstadoVigencia" runat="server" Visible="false" Text='<%# (DataBinder.Eval(Container, "DataItem.vigencia.id")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Id Doc. Planilla"> 
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.idDocPlanilla") %>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Nombre Documento">
                    <ItemTemplate>
                         <%# DataBinder.Eval(Container, "DataItem.NombreDocPlanilla") %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Fecha Inserción">
                   <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.fechaIngresoSistema")%>
                   </ItemTemplate> 
                </asp:TemplateField>

               <asp:TemplateField HeaderText="Tramites Asociados">
                   <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.tramiteCad")%>
                   </ItemTemplate> 
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="70px">
                    <ItemTemplate>
                        <asp:ImageButton ID="gDescargar" Visible="false" runat="server" CausesValidation="false" CommandName="Descargar"
                             ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar"
                             CommandArgument='<%# DataBinder.Eval(Container, "DataItem.idDocPlanilla") %>'/>
                        
                        
                        <asp:ImageButton ID="gNoVigente" Visible="false" runat="server" CausesValidation="false" CommandName="NoVigente" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.idDocPlanilla")  %>'
                             ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Pasar a No Vigente" ToolTip="Pasar a No Vigente" />

                        <asp:ImageButton ID="gVigente" Visible="false" runat="server" CausesValidation="false" CommandName="Vigente" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.idDocPlanilla")  %>'
                             ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Pasar a Vigente" ToolTip="Pasar a Vigente" />


                        <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" 
                             ImageUrl="../../App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" 
                             CommandArgument='<%# DataBinder.Eval(Container, "DataItem.idDocPlanilla") %>' />
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
        <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" Visible="true" /> 
        </fieldset>
            
    </asp:Panel>

</asp:Content>