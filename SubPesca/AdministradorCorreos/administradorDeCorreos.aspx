<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="administradorDeCorreos.aspx.cs" 
Inherits="SubPesca.AdministradorCorreo.administradorDeCorreos" Theme="admin_style"  %>



    <%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
    
    <asp:Content ID="FormularioAdministracionCorreos" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Administrador de Correos Electrónicos</span>
            </td>
            <td align="right" valign="middle">
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

    <!-- formulario de modificación -->
    <fieldset>
    <legend>Datos de Correo Electrónico</legend>
    
        <asp:ValidationSummary ID="ValidationSummaryFormularioModificar" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
        
        <br />

        <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
        <asp:Panel ID="Content_PanelModificarCorreo" Visible="false" runat="server">
        
        <table class="form" cellpadding="0" cellspacing="0">
            <tr>
                <td class="style1"><span class="item">Clave Correo</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                     <asp:HiddenField ID="ClaveCorreo" runat="server" />

                     <asp:Label ID="ClaveTemplateAvisoDescr" runat="server" Width="276px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1"><span class="item">Descripción Correo</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="DescripcionCorreo" runat="server" Width="276px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="col1"><span class="item">Título</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="Subject" runat="server" Width="276px"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorSubject" runat="server" ControlToValidate="Subject" ValidationGroup="grupo1" ErrorMessage="Subject" Display="Static" >*</asp:RequiredFieldValidator> 
                </td>
            </tr>
            <tr>
                <td class="col1">
                    <span class="item">Cuerpo</span>
                </td>
                <td style="width:10px; text-align:center;">
                    <span class="item">:</span>
                </td>
                <td>
                    <asp:TextBox ID="Cuerpo" runat="server" Columns="50" MaxLength="2000" Rows="5" 
                        TextMode="multiline" ValidationGroup="{"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorCuerpo" runat="server" ControlToValidate="Cuerpo" ValidationGroup="grupo1" ErrorMessage="Cuerpo" Display="Static" >*</asp:RequiredFieldValidator> 
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <span class="item">Descripción de Etiquetas</span></td>
                <td class="col2">
                    <span class="item">:</span></td>
                <td>
                    <asp:Label ID="DescripcionCuerpo" runat="server" Width="276px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="col1">
                    <span class="item">Rol Usuario para envío</span></td>
                <td class="col2">
                    <span class="item">:</span></td>
                <td>
                    <asp:ListBox ID="RolUsuario" runat="server" Width="350px" SelectionMode="Multiple"></asp:ListBox>
                    
                </td>
            </tr>
            <tr>
                <td class="col1"><span class="item">Direcciones para envío</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="Direcciones" runat="server" MaxLength="50" Width="250px"></asp:TextBox> *  Ingresar direcciones de correos separadas por ,
 
                </td>
                
            </tr>
            <tr>
                <td class="col1">
                    <span class="item">Aplica Envío</span></td>
                <td class="col2">
                    <span class="item">:</span></td>
                <td>
                    <asp:CheckBox ID="AplicaEnvio" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td style="width: 10px; text-align: center;">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Modificar" runat="server" Text="Modificar" OnClick="Modificar_Click" />
                    <asp:Button ID="Cancelar" runat="server" Text="Cancelar" onclick="Cancelar_Click" />
                </td>
            </tr>
        </table>
        
        </asp:Panel>
        
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Modificar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Cancelar" EventName="Click" />
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
            </asp:Panel>
            </div>
                 
            <asp:Panel ID="PanelGrid"  Visible="true" runat="server" CssClass="Content_Grilla2">                                          
                <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                DataKeyNames="idDestTemplate"
                AllowPaging="True" PageSize="30" 
                OnPageIndexChanging="GridView1_PageIndexChanged"
                AllowSorting="true"
                OnRowDataBound="GridView1_RowDataBound"
                OnRowCommand="GridView1_RowCommand"
                CssClass="mGrid_dialog"
                PagerStyle-CssClass="pgr" 
                Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="Clave Correo" DataField="claveTemplateAvisoDescr" ReadOnly="true" ItemStyle-Width="30px" />
                    
                    <asp:TemplateField HeaderText="Descripción Correo">
                         <ItemTemplate>
                            <asp:Label ID="gDescCorreo" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("descrTemplateAviso")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Titulo">
                         <ItemTemplate>
                            <asp:Label ID="gSubject" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("templateSubject")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    
                    <asp:TemplateField HeaderText="Cuerpo">
                        <ItemTemplate>
                            <asp:Label ID="gCuerpo" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("templateCuerpo")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                    <asp:TemplateField HeaderText="Rol Usuario para envío">
                        <ItemTemplate>
                            <asp:Label ID="gRolUsuario" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("destinatariosRolTemplateComa")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    
                    <asp:TemplateField HeaderText="Direcciones para envío">
                        <ItemTemplate>
                            <asp:Label ID="gDirecciones" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("destinatariosDireccionTemplateComa")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>    
                    
                    <asp:TemplateField HeaderText="Aplica Envio">
                        <ItemTemplate>
                            <asp:Label ID="gAplicaEnvio" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicaEnvioString") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                        
                    <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:ImageButton ID="gVer" Visible="true" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "claveTemplateAviso") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                            <asp:ImageButton ID="gModificar" Visible="true" runat="server" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "claveTemplateAviso") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
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
                <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" Visible= "false"/>
            
            </asp:Panel>
      </ContentTemplate> 
      <Triggers>
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
      </Triggers>
      </asp:UpdatePanel>
    </asp:Content>
