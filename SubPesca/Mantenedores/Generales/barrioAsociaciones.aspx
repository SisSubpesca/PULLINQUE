<%@ Page Language="C#"  MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="barrioAsociaciones.aspx.cs" 
Inherits="SubPesca.Mantenedores.Generales.barrioAsociaciones" Theme="admin_style"  %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

    <asp:Content ID="FormularioAdministracionTipoBarrioConBarrio" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Administrar Unidad Espacial o Solicitud en Barrio</span>
            </td>
            <td align="right" valign="middle">  
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

    <fieldset>
    <legend>Datos de la asociación</legend>


        <asp:UpdatePanel id="UpdatePanelMensaje" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="PanelMensaje" Visible="true" runat="server">
                <asp:ValidationSummary ID="ValidationSummaryFormularioAgregar" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
            </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>


        <br />

        <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Panel ID="Content_Panel" Visible="false" runat="server">

            <table class="form" cellpadding="0" cellspacing="0">
         
            
            <tr>
            <td colspan="3">

                <asp:UpdatePanel ID="UpdatePanelSolicitudUE" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>              
                        <asp:Panel ID="PanelSolicitudUE" Visible="true" runat="server">

                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item">Tipo</span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3"><asp:DropDownList ID="Tipo" AutoPostBack="true" runat="server" onselectedindexchanged="Tipo_OnSelectedIndexChanged"></asp:DropDownList></td>
                            </tr>
                            </table>

                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

           
                <asp:UpdatePanel ID="UpdatePanelTipoUnidadEspacial" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>              
                        <asp:Panel ID="PanelTipoUnidadEspacial" Visible="false" runat="server">

                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item">Tipo Unidad Espacial</span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3"><asp:DropDownList ID="TipoUnidadEspacial" AutoPostBack="true" runat="server" onselectedindexchanged="TipoUnidadEspacial_SelectedIndexChanged"></asp:DropDownList></td>
                            </tr>
                            </table>

                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <asp:UpdatePanel ID="UpdatePanelTipoSolicitud" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>              
                        <asp:Panel ID="PanelTipoSolicitud" Visible="false" runat="server">

                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item">Tipo Trámite</span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3"><asp:DropDownList ID="TipoSolicitud" AutoPostBack="true" runat="server" onselectedindexchanged="TipoSolicitud_SelectedIndexChanged"></asp:DropDownList></td>
                            </tr>
                            </table>

                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <asp:UpdatePanel ID="UpdatePanelCodigoCentro" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelCodigoCentro"  Visible="false" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item">Código Centro</span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3"><asp:TextBox ID="CodigoCentro" runat="server" onKeyUp="return onlyNumeric(this)"  MaxLength="9" AutoPostBack="true"></asp:TextBox>&nbsp;<asp:Label ID="Label3" runat="server"></asp:Label></td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                

                <asp:UpdatePanel ID="UpdatePanelNumeroPert" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelNumeroPert"  Visible="false" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item">Pert</span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3"><asp:TextBox ID="NumeroPert" runat="server"  MaxLength="10" AutoPostBack="true"></asp:TextBox>&nbsp;<asp:Label ID="Label4" runat="server"></asp:Label></td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <asp:UpdatePanel ID="UpdatePanelNumeroSector" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelNumeroSector"  Visible="false" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item">Número Sector</span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3"><asp:TextBox ID="NumeroSector" runat="server" onKeyUp="return onlyNumeric(this)" MaxLength="8" AutoPostBack="true"></asp:TextBox>&nbsp;<asp:Label ID="Label6" runat="server"></asp:Label></td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                    
                <asp:UpdatePanel ID="UpdatePanelNumeroIdentificador" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelNumeroIdentificador"  Visible="false" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item">Número Identificador</span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3"><asp:TextBox ID="NumeroIdentificador" runat="server" onKeyUp="return onlyNumeric(this)" MaxLength="10" AutoPostBack="true"></asp:TextBox>&nbsp;<asp:Label ID="Label5" runat="server"></asp:Label></td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="Panel1"  Visible="true" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item">Barrio ACS</span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3"><asp:DropDownList ID="BarrioACS" runat="server"></asp:DropDownList></td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>                        
                        <asp:Panel ID="Panel2"  Visible="true" runat="server">
                            <table class="form" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td class="col1"><span class="item">Barrio ACM</span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3"><asp:DropDownList ID="BarrioACM" runat="server"></asp:DropDownList></td>
                            </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>


            </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>


            <tr>
                <td style="width:100px">&nbsp;</td>
                <td style="width:10px; text-align:center;">&nbsp;</td>
                <td><asp:Button ID="Buscar" runat="server" OnClick="Buscar_Click" Text="Buscar" CausesValidation="false" />&nbsp;<asp:Button ID="Agregar" runat="server" OnClick="Agregar_Click" OnClientClick="javascript:doIframe();" Text="Actualizar" CausesValidation="false" ValidationGroup="grupo1" /></td>
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
                AllowPaging="True" PageSize="25" OnPageIndexChanging="GridView1_PageIndexChanged"
                AllowSorting="true" OnSorting="GridView1_Sorting"
                OnRowDataBound="GridView1_RowDataBound"
                OnRowCommand="GridView1_RowCommand"
                OnRowEditing="GridView1_RowEditing"
                OnRowUpdating="GridView1_RowUpdating"
                OnRowCancelingEdit="GridView1_RowCancelingEdit"
                DataKeyNames="idSolConcesion"
                CssClass="mGrid_dialog"
                PagerStyle-CssClass="pgr" 
                Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>


                    <asp:TemplateField HeaderText="Tipo">
                         <ItemTemplate>
                            <asp:Label ID="gTipo" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("tipo.descripcion")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Tipo Trámite / Tipo Unidad Espacial">
                         <ItemTemplate>
                            <asp:Label ID="gTipoUE" runat="server"  Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("tipoUnidadEspacial.descripcion"))%>'></asp:Label>
                            <asp:Label ID="gTipoSol" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("tipoSolicitud.descripcion"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SubTipo Trámite">
                         <ItemTemplate>
                            <asp:Label ID="gSubTipoSol" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("DescripcionTipoModificacion"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Región">
                         <ItemTemplate>
                            <asp:Label ID="gRegion" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("region.descripcion"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Código de Centro">
                         <ItemTemplate>
                            <asp:Label ID="gCodigoCentro" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "codigoCentro") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Número Pert/Número Identificador">
                         <ItemTemplate>
                            <asp:Label ID="gPert" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "numPert") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Barrio ACS">
                         <ItemTemplate>
                            <asp:Label ID="gBarrioACS" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "barrioACS.descripcion") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddleditBarrioACS" runat="server" AutoPostBack="true" />
                            <asp:HiddenField ID="hdnBarrioACS" runat="server" Value='<%#Eval("barrioACS.id") %>' />
                            <asp:HiddenField ID="hdnIdSolConcesion" runat="server" Value='<%#Eval("idSolConcesion") %>' />
                            <asp:HiddenField ID="hdnIdRegion" runat="server" Value='<%#Eval("region.id") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Barrio ACM">
                         <ItemTemplate>
                            <asp:Label ID="gBarrioACM" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "barrioACM.descripcion") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddleditBarrioACM" runat="server" AutoPostBack="true" />
                            <asp:HiddenField ID="hdnBarrioACM" runat="server" Value='<%#Eval("barrioACM.id") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>   
                                    
                      <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:ImageButton ID="gModificar" Visible="false" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
                                 ImageUrl="~/App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                            <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idSolConcesion") %>'
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
            <asp:AsyncPostBackTrigger ControlID="Buscar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
            <asp:PostBackTrigger ControlID="ExportarGrilla" />
      </Triggers>
      </asp:UpdatePanel>

  

    </asp:Content>
