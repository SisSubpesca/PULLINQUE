<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="equivalenciaEstadoUOT.aspx.cs" Inherits="SubPesca.Mantenedores.Generales.equivalenciaEstadoUOT" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

    <asp:Content ID="FormularioAdministracionEquivalenciaEstadoUOT" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Mantenedor de Equivalencia Estado UOT</span>
            </td>
            <td align="right" valign="middle">  
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />  

    <fieldset>
    <legend>Datos de Equivalencia Estado UOT</legend>
    
        <asp:ValidationSummary ID="ValidationSummaryFormularioAgregar" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
        
        <br />

        <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Panel ID="Content_Panel" Visible="true" runat="server">
        <table class="form" cellpadding="0" cellspacing="0">
             <tr>
            <td class="col1"><span class="item">Tipo de Solicitud</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="TiposSolicitud" runat="server" OnSelectedIndexChanged="TiposTramite_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorTiposSolicitud" runat="server" ControlToValidate="TiposSolicitud" ValidationGroup="grupo1" ErrorMessage="TiposSolicitud" Display="Static">*</asp:RequiredFieldValidator>    
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr>
            <td class="col1"><span class="item">Estado Solicitud:</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Estado" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorEstado" runat="server" ControlToValidate="Estado" ValidationGroup="grupo1" ErrorMessage="Estado" Display="Static">*</asp:RequiredFieldValidator>    
                </ContentTemplate>
               
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr>
            <td class="col1"><span class="item">Estado UOT</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:DropDownList ID="EstadoUOT" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorEstadoUOT" runat="server" ControlToValidate="EstadoUOT" ValidationGroup="grupo1" ErrorMessage="EstadoUOT" Display="Static">*</asp:RequiredFieldValidator>    
            </td>
        </tr>
            <tr>
                <td style="width:100px">
                    &nbsp;</td>
                <td style="width:10px; text-align:center;">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Agregar" runat="server" OnClick="Agregar_Click" 
                        Text="Agregar" CausesValidation="true" ValidationGroup="grupo1" />

                    <asp:Button ID="Buscar" runat="server" OnClick="Buscar_Click" 
                        Text="Buscar" CausesValidation="true" ValidationGroup="grupo1"   />
            </td>
        </tr>

       
        </table>
        </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
                       
        </fieldset>

        <asp:UpdatePanel ID="upd2" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <div style="height:30px;">
            <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla_dialog" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla" runat="server"></asp:Label>
                </div>
                <div class="msgGrilla_div3">
                    <a onclick="ocultarObjeto('Content_msgGrilla', 0)"><img src="../../App_Themes/admin_style/images/cerrar.jpg" height="20px" alt="borrar" /></a>
                </div>
            </asp:Panel>
            </div>     
                                                      
            <asp:GridView ID="GridView1" runat="server"
                RowStyle-VerticalAlign="top" AlternatingRowStyle-VerticalAlign="top"
                DataKeyNames="idEstadoSolicitudUOT"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                AllowPaging="True" PageSize="30" OnPageIndexChanging="GridView1_PageIndexChanged"
                AllowSorting="true" OnSorting="GridView1_Sorting"
                OnRowDataBound="GridView1_RowDataBound"
                OnRowCommand="GridView1_RowCommand"
                CssClass="mGrid_dialog"
                PagerStyle-CssClass="pgr" 
                Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    
                    <asp:BoundField HeaderText="Tipo Unid. Espacial" DataField="nombreTipoUE" ReadOnly="true" ItemStyle-Width="60px" />

                    <asp:TemplateField HeaderText="Estado Solicitud" ItemStyle-Width="458px">
                         <ItemTemplate>
                            <asp:Label ID="gEstadoSolicitud" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("nombreEstadoSolicitud")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado Solicitud UOT" ItemStyle-Width="458px">
                         <ItemTemplate>
                            <asp:Label ID="gEstadoSolicitudUOT" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("nombreEstadoSolicitudUOT")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idEstadoSolicitudUOT") + ";" + DataBinder.Eval(Container.DataItem, "idEstadoSolicitud") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
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
            
            <asp:HiddenField ID="KeySort" runat="server" />
            <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" /> 
                       
      </ContentTemplate> 
      <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
      </Triggers>
      </asp:UpdatePanel>

    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td style="width:100px">
        <asp:Button ID="Volver" runat="server" Text="Volver" onclick="Volver_Click" />
        </td>
       
    </tr>
    </table>

    </asp:Content>

