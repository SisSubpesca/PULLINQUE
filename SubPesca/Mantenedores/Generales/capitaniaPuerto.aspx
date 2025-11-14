<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="capitaniaPuerto.aspx.cs" Inherits="SubPesca.Mantenedores.Generales.capitaniaPuerto" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

    <asp:Content ID="FormularioAdministracionTitulares" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Mantenedor de Capitanía de Puerto</span>
            </td>
            <td align="right" valign="middle">
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

    <fieldset>
    <legend>Datos de la Capitanía de Puerto</legend>
    
        <asp:ValidationSummary ID="ValidationSummaryFormularioAgregar" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
        
        <br />

        <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Panel ID="Content_Panel" Visible="false" runat="server">
        <table class="form" cellpadding="0" cellspacing="0">
            <tr>
                <td class="style1"><span class="item">Código</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    <asp:TextBox ID="codigo" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorCodigo" runat="server" ControlToValidate="codigo"  ValidationGroup="grupo1" ErrorMessage="Código" Display="Static">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidatorCodigo" runat="server" Type="Integer" MinimumValue="1" MaximumValue="1999999999" ControlToValidate="codigo" ForeColor="Red" ErrorMessage="Código rango no permitido" ValidationGroup="grupo1" />
                    <asp:CompareValidator ID="CompareValidatorCodigo" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="codigo" ForeColor="Red" ErrorMessage="Código Ingrese valores numéricos" ValidationGroup="grupo1" />
                </td>
                
            </tr>
            <tr>
                <td class="style1"><span class="item">Capitanía de Puerto</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td>
                    
                    <asp:TextBox ID="nombre" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorNombre" runat="server" ControlToValidate="nombre" ValidationGroup="grupo1" ErrorMessage="Capitanía de Puerto" Display="Static">*</asp:RequiredFieldValidator>    
                </td>
            </tr>
            <tr>
                <td class="style1"><span class="item">Aplica Distancia Colector</span></td>
                <td style="width:10px; text-align:center;"><span class="item">:</span></td>
                <td>
                    <asp:DropDownList ID="AplicaDistanciaColector" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorAplicaDistanciaColector" runat="server" ControlToValidate="AplicaDistanciaColector" ValidationGroup="grupo1" ErrorMessage="Aplica Distancia Colector" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>    
                </td>
                
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td style="width:10px; text-align:center;">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Agregar" runat="server" CausesValidation="true" 
                        OnClick="Agregar_Click" OnClientClick="javascript:doIframe();" Text="Agregar" 
                        ValidationGroup="grupo1" />
                    <asp:Button ID="Buscar" runat="server" onclick="Buscar_Click" Text="Buscar" />
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
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                DataKeyNames="idCapitaDePuerto"
                AllowPaging="True" PageSize="20" OnPageIndexChanging="GridView1_PageIndexChanged"
                AllowSorting="true" OnSorting="GridView1_Sorting"
                OnRowDataBound="GridView1_RowDataBound"
                OnRowCommand="GridView1_RowCommand"
                OnRowEditing="GridView1_RowEditing"
                OnRowUpdating="GridView1_RowUpdating"
                OnRowCancelingEdit="GridView1_RowCancelingEdit"
                CssClass="mGrid_dialog"
                PagerStyle-CssClass="pgr" 
                Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="ID" SortExpression="idCapitaDePuerto" DataField="idCapitaDePuerto" ReadOnly="true" ItemStyle-Width="60px" />
                    <asp:TemplateField HeaderText="Codigo" SortExpression="codigo" ItemStyle-Width="458px">
                         <ItemTemplate>
                            <asp:Label ID="gCodigo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "codigo") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="geCodigo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "codigo") %>' MaxLength="30" Width="99%"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorgeCodigo" runat="server" ControlToValidate="geCodigo"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Código" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidatorgeCodigo" runat="server" Type="Integer" MinimumValue="1" MaximumValue="1999999999" ControlToValidate="geCodigo" ForeColor="Red" ErrorMessage="Código rango no permitido" ValidationGroup="grupo2" />
                            <asp:CompareValidator ID="CompareValidatorgeCodigo" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="geCodigo" ForeColor="Red" ErrorMessage="Código Ingrese valores numéricos" ValidationGroup="grupo2" />

                        </EditItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Capitania de Puerto" SortExpression="region" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gNombre" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("nombreCapitaniaDePuerto")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="geNombre" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nombreCapitaniaDePuerto") %>' MaxLength="30" Width="99%"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidatorgeNombre" runat="server" ControlToValidate="geNombre"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Capitanía de Puerto" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>

                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Aplica Distancia Colector" SortExpression="requiereCertString" ItemStyle-Width="458px">
                        <ItemTemplate>
                            <asp:Label ID="gAplicaDistanciaColector" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("requiereCertString")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                                <asp:DropDownList ID="ddleditCountry" runat="server" AutoPostBack="true" />
                                <asp:RequiredFieldValidator id="RequiredFieldValidatorDdleditCountry" runat="server" ControlToValidate="ddleditCountry"  ValidationGroup="grupo2" ErrorMessage="Ingrese valor para Aplica Distancia Colector" ForeColor="Red" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>

                                <asp:HiddenField ID="hdnCountry" runat="server" Value='<%#Eval("requiereCertString") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>         

                    <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:ImageButton ID="gModificar" Visible="false" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idCapitaDePuerto") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                            <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idCapitaDePuerto") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="gActualizar" runat="server" CausesValidation="true" ValidationGroup="grupo2" CommandName="Update"
                                 ImageUrl="~/App_Themes/admin_style/images/guardar2.png"  Height="20px" AlternateText="Actualizar" ToolTip="Actualizar" />
                            <asp:ImageButton ID="gCancelar" runat="server" CausesValidation="false" CommandName="Cancel" 
                                 ImageUrl="~/App_Themes/admin_style/images/cancelar.png"  Height="20px" AlternateText="Cancelar" ToolTip="Cancelar" />                            
                        </EditItemTemplate>
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
            <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" Visible="false" /> 
                      
      </ContentTemplate> 
      <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
            <asp:PostBackTrigger ControlID="ExportarGrilla" />

      </Triggers>
      </asp:UpdatePanel>

    </asp:Content>

<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            width: 172px;
        }
    </style>
</asp:Content>

