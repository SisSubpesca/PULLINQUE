<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="agregarTitular.aspx.cs" Theme="admin_style" Inherits="SubPesca.Mantenedores.Titulares.agregarTitular" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="agregarArchivoAdjunto.ascx"            tagname="agregarArchivoAdjunto"                          tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


    

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_usuarios.js")); %>" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
    </style>
    </asp:Content>

    <asp:Content ID="FormularioAdministracionTitulares" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManageAdministracionTitulares" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>

    <asp:HiddenField ID="RegistroCentralizado" runat="server"></asp:HiddenField>

    
            <div id="divOculto" style="display:none">

                <asp:TextBox ID="RutPersona2" MaxLength="10" Width="100px" runat="server"></asp:TextBox>
                
                <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="RutPersona2"  ValidationGroup="grupo1"
                 ErrorMessage="Rut Persona" Display="Static"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" ControlToValidate="RutPersona2" ErrorMessage="Rut Persona sin formato válido" ForeColor="Red" ClientValidationFunction="validaRUT" Display="Static" Font-Size="10" runat="server" ValidationGroup="grupo1"></asp:CustomValidator>

            </div>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo"><asp:Label ID="LabelAccion" runat="server"></asp:Label> Titular</span>
            </td>
            <td align="right" valign="middle">
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

    <fieldset>
    <legend>Datos del Titular</legend>

    <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
        <div class="msgGrilla_div1">
            <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
        </div>
        <div class="msgGrilla_div2">
            <asp:Label ID="msgGrilla" runat="server"></asp:Label>
        </div>
    </asp:Panel>

    <asp:ValidationSummary ID="ValidationSummaryIngresoSolicitante" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
    <asp:ValidationSummary ID="ValidationSummaryBuscarSolicitante" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo2" />

    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"><span class="item">Rut Persona</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            
            <asp:TextBox ID="RutPersona" MaxLength="10" Width="100px" runat="server" AutoPostBack="true"
                ontextchanged="RutPersona_TextChanged"></asp:TextBox>
            
            <asp:Button ID="BuscarSolicitante" runat="server" Text="Buscar" onclick="ButtonRutPersona_Click" CausesValidation="true" ValidationGroup="grupo2" Visible = "false" />
            
            <asp:Label ID="EjemploRut" runat="server" Text="12345678-K" Visible = "false"></asp:Label>

            <asp:RequiredFieldValidator id="RequiredFieldValidatorRutPersona" runat="server" ControlToValidate="RutPersona"  ValidationGroup="grupo2"
            ErrorMessage="Rut Persona" Display="Static">*</asp:RequiredFieldValidator>

            <asp:CustomValidator ID="ccNumCustVal" ControlToValidate="RutPersona" ErrorMessage="Rut Persona sin formato válido" ForeColor="Red" ClientValidationFunction="validaRUT" Display="Static" Font-Size="10" runat="server" ValidationGroup="grupo2"></asp:CustomValidator>

        </td>
    </tr>
    </table>

    <asp:UpdatePanel ID="UpdatePanelDatosPersona" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 

    <asp:Panel ID="PanelPersonaNatural" Visible="false" runat="server">
    <table class="form" cellpadding="0px" cellspacing="0px">

    <asp:HiddenField ID="nombreTitular" runat="server" />

    <tr>
        <td class="col1"><span class="item">Nombre Persona Natural</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
            <asp:TextBox ID="NombrePersonaNatural" runat="server" Width="293px" 
                ontextchanged="NombrePersonaNatural_TextChanged" AutoPostBack="true"></asp:TextBox>

            <asp:RequiredFieldValidator id="RequiredFieldValidatorNombrePersonaNatural" runat="server" ControlToValidate="NombrePersonaNatural" ValidationGroup="grupo1" ErrorMessage="Nombre Persona Natural" Display="Static">*</asp:RequiredFieldValidator>
        
            <asp:ImageButton ID="cambiarNombrePersonaNatural" 
                                runat="server" ImageUrl="~/App_Themes/admin_style/images/editar_nombre.jpg" 
                                AlternateText="Cambiar Nombre Persona Natural" ToolTip="Cambiar Nombre Persona Natural" 
                                onclick="ImageButtonCambiarNombre_Click" style="width: 20px" Height="20px" Visible = "false"  />
            <asp:Label ID="EjemploNombrePN" runat="server" Text="Ejemplo: APELLIDO 1 APELLIDO 2, NOMBRE"></asp:Label>

        </td>
    </tr>
    <tr>
        <td class="col1"></td>
        <td class="col2"></td>
        <td class="col3">
            <asp:LinkButton ID="VerNombresPersonaNatural" 
                OnClientClick="javascript:abre_dialogo2('verNombres',document.getElementById('ctl00_rightbody_RutPersona').value,1)" 
                runat="server" Visible="false" >[Ver Nombres]</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="col1"><span class="item">Género</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
            <asp:DropDownList ID="Genero" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator id="RequiredFieldValidatorGenero" runat="server" ControlToValidate="Genero"  ValidationGroup="grupo1" ErrorMessage="Género" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>

        </td>
    </tr>
    </table>
    </asp:Panel>

    <asp:Panel ID="PanelPersonaJuridica" Visible="false" runat="server">
    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"><span class="item">Tipo</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
            <asp:DropDownList ID="TipoPersonaJuridica" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoPersonaJuridica" runat="server" ControlToValidate="TipoPersonaJuridica"  ValidationGroup="grupo1" ErrorMessage="Tipo" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
            
        </td>
    </tr>
    <tr>
        <td class="col1"><span class="item">Nombre Persona Jurídica</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
            <asp:TextBox ID="NombrePersonaJuridica" runat="server" Width="293px" 
                ontextchanged="NombrePersonaJuridica_TextChanged" AutoPostBack="true"></asp:TextBox>
            <asp:RequiredFieldValidator id="RequiredFieldValidatorNombrePersonaJuridica" runat="server" ControlToValidate="NombrePersonaJuridica" ValidationGroup="grupo1" ErrorMessage="Nombre Persona Jurídica" Display="Static">*</asp:RequiredFieldValidator>

            <asp:ImageButton ID="cambiarPersonaJuridica" 
                                runat="server" ImageUrl="~/App_Themes/admin_style/images/editar_nombre.jpg" 
                                AlternateText="Cambiar Nombre Persona Jurídica" ToolTip="Cambiar Nombre Persona Jurídica" 
                                onclick="ImageButtonCambiarNombre_Click" style="width: 20px" Height="20px" Visible="false" />
        </td>
    </tr>
    <tr>
        <td class="col1"></td>
        <td class="col2"></td>
        <td class="col3">
            <asp:LinkButton ID="VerNombrePersonaJuridica" 
                OnClientClick="javascript:abre_dialogo2('verNombres',document.getElementById('ctl00_rightbody_RutPersona').value,1)" 
                runat="server" Visible="false" >[Ver Nombres]</asp:LinkButton>
        </td>
    </tr>
    </table>
    </asp:Panel>

    <asp:Panel ID="PanelSernapesca" Visible="false" runat="server">
    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"><span class="item">N° Registro Subpesca</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
            <asp:TextBox ID="NumeroRegistroSubpesca" runat="server" BackColor="#ddddee" ReadOnly ></asp:TextBox>
            
        </td>
    </tr>
    <tr>
        <td class="col1"><span class="item">Fecha Registro Subpesca</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
            <asp:TextBox ID="FechaRegistroSubpesca" Width="120px" runat="server" BackColor="#ddddee" ReadOnly></asp:TextBox>
            
        </td>
    </tr>
    </table>
    </asp:Panel>

    <asp:Panel ID="PanelHolding" Visible="false" runat="server">
    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"><span class="item">Holding</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
            <asp:DropDownList ID="Holding" runat="server"></asp:DropDownList>
            
        </td>
    </tr>
    </table>
    </asp:Panel>

    <asp:Panel ID="PanelAPE" Visible="false" runat="server">
    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"><span class="item">APE</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
            <asp:DropDownList ID="APE" runat="server"></asp:DropDownList>
            
        </td>
    </tr>
    </table>
    </asp:Panel>

    <asp:Panel ID="PanelRPA" Visible="false" runat="server">
    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"><span class="item">RPA</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:TextBox ID="RPA" runat="server" BackColor="#ddddee" ReadOnly ></asp:TextBox>
        </td>
    </tr>
    </table>
    </asp:Panel>

    </ContentTemplate>
    </asp:UpdatePanel>
    
        <!-- Listado de Direcciones -->
        <asp:Panel ID="PanelGridContactoMatrizSucursal"  Visible="true" runat="server" CssClass="Content_Grilla">                                          
    <asp:GridView 
           ID="GridContactoMatrizSucursales"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%"
           OnRowCreated="GridContactoMatrizSucursales_RowCreated"
           OnRowDataBound="GridContactoMatrizSucursales_RowDataBound"
           OnRowCommand="GridContactoMatrizSucursales_RowCommand"
           AllowPaging="false">
           
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
               <asp:TemplateField HeaderText="Dirección" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>

                    <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                    <%# DataBinder.Eval(Container, "DataItem.direccion")%>

               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Región" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.region.region")%>
               </ItemTemplate>
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Comuna" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.region.comuna.comuna")%>
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                
                    <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idMatrizSuc") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
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

        <table class="form" cellpadding="0px" cellspacing="0px" width="100%">
                    <tr>
                        <td class="col1" colspan="6">
                            <asp:ImageButton ID="ImageButtonMatrizSucursal" 
                                runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                AlternateText="Agregar Matriz/Sucursal" ToolTip="Agregar Matriz/Sucursal" 
                                onclick="ImageButtonMatrizSucursal_Click" style="width: 20px" CausesValidation="true" ValidationGroup="grupo2"/>
                            <span class="item">Agregar Dirección</span></td>
                    </tr>
        </table>
        
        <br /><br />

        <!-- Listado de Contactos -->
        <asp:Panel ID="PanelContacto"  Visible="true" runat="server" CssClass="Content_Grilla">                                          
        <asp:GridView 
               ID="GridViewContacto"
               runat="server"
               AutoGenerateColumns="False" 
               CellPadding="4" 
               ForeColor="#333333" 
               GridLines="None"
               CssClass="mGrid"
               PagerStyle-CssClass="pgr"
               Width="100%"
               OnRowCreated="GridViewContacto_RowCreated"
               OnRowDataBound="GridViewContacto_RowDataBound"
               OnRowCommand="GridViewContacto_RowCommand"
               AllowPaging="false">
           
               <RowStyle BackColor="#EFF3FB" />
               <Columns>
                   <asp:TemplateField HeaderText="Tipo Contacto" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>

                        <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                        <%# DataBinder.Eval(Container, "DataItem.tipoContacto.descripcion")%>

                   </ItemTemplate>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Valor Contacto" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.valorContacto")%>
                   </ItemTemplate>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Detalle" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.detalle")%>
                   </ItemTemplate>
                   </asp:TemplateField>
               
                   <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>

                        <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idContacto") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
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

        <table class="form" cellpadding="0px" cellspacing="0px" width="100%">
                        <tr>
                            <td class="col1" colspan="6">
                                <asp:ImageButton ID="ImageButtonContacto" 
                                    runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                    AlternateText="Agregar Contacto" ToolTip="Agregar Contacto" 
                                    style="width: 20px" CausesValidation="true" ValidationGroup="grupo2" 
                                    onclick="ImageButtonContacto_Click"/>
                                <span class="item">Agregar Contacto</span></td>
                        </tr>
            </table>
        
        <br /><br />

        <!-- Listado de Representantes Legales (sólo para personas jurídicas)-->
        <asp:UpdatePanel ID="UpdatePanelPersJuridica" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 

        <asp:Panel ID="PanelPersJuridica" Visible="false" runat="server">

        <asp:GridView 
           ID="GridRepresentantesLegales"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%"
           OnRowCreated="GridRepresentantesLegales_RowCreated"
           OnRowDataBound="GridRepresentantesLegales_RowDataBound"
           OnRowCommand="GridRepresentantesLegales_RowCommand"
           AllowPaging="false">
                      
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
               <asp:TemplateField HeaderText="Rut Representante Legal">
                <ItemTemplate>
                    
                    <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                    <asp:HiddenField ID="gArchivoBinario" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.idArchivoBinario") %>' />

                    <%# DataBinder.Eval(Container, "DataItem.representanteLegal.rut")%>-<%# DataBinder.Eval(Container, "DataItem.representanteLegal.dv")%>

                </ItemTemplate>
               </asp:TemplateField>
                
               <asp:TemplateField HeaderText="Nombre Representante Legal">
                <ItemTemplate>
                     <%# DataBinder.Eval(Container, "DataItem.representanteLegal.nombreSolicitante")%>
                </ItemTemplate>
               </asp:TemplateField> 

               <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                
                    <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idRepLegal") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                    ImageUrl="../../App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar"  />
                                
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

        <table class="form" cellpadding="0px" cellspacing="0px" width="100%">
                    <tr>
                        <td class="col1" colspan="6"><asp:ImageButton ID="ImageRepresentanteLegal" 
                                runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                AlternateText="Asociar Representante Legal" 
                                ToolTip="Asociar Representante Legal" onclick="ImageRepresentanteLegal_Click" CausesValidation="true" ValidationGroup="grupo2" />
                            <span class="item">Asociar Representante Legal</span></td>
                    </tr>
        </table>

        </asp:Panel>

        <br /><br />

        </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Panel ID="PanelPersJuridica2" Visible="false" runat="server">

        <!-- Listado de Operadores -->
        <asp:GridView 
           ID="GridViewOperadores"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%"
           OnRowCreated="GridViewOperadores_RowCreated"
           OnRowDataBound="GridViewOperadores_RowDataBound"
           OnRowCommand="GridViewOperadores_RowCommand"
           AllowPaging="false">
                      
           <RowStyle BackColor="#EFF3FB" />
           <Columns>

               <asp:TemplateField HeaderText="Rut Operador">
                <ItemTemplate>

                    <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                    
                    <%# DataBinder.Eval(Container, "DataItem.operador.rut")%>-<%# DataBinder.Eval(Container, "DataItem.operador.dv")%>

                </ItemTemplate>
               </asp:TemplateField>
                
               <asp:TemplateField HeaderText="Nombre Operador">
                <ItemTemplate>
                     <%# DataBinder.Eval(Container, "DataItem.operador.nombreSolicitante")%>
                </ItemTemplate>
               </asp:TemplateField> 
               
               <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                     <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idOperador") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                     ImageUrl="../../App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" OnPreRender="ImgAdd_PreRender" />
                                
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

       
       <table class="form" cellpadding="0px" cellspacing="0px" width="100%">
                    <tr>
                        <td class="col1" colspan="6"><asp:ImageButton ID="ImageOperador" runat="server" 
                                ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                AlternateText="Asociar Operador" ToolTip="Asociar Operador" 
                                onclick="ImageOperador_Click" CausesValidation="true" ValidationGroup="grupo2" />
                            <span class="item">Asociar Operador</span></td>
                    </tr>
        </table>
        </asp:Panel>
       


    </fieldset>
   
    <asp:Panel ID="PanelArchivoAdjunto"  Visible="true" runat="server">
        <uc1:agregarArchivoAdjunto ID="agregarArchivoAdjunto" runat="server" />
    </asp:Panel>
    
    <br />
    
    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1">
        
        <asp:Button ID="GuardarTitular" runat="server" Text="Crear Titular" 
                onclick="GuardarTitular_Click" CausesValidation="true" ValidationGroup="grupo1" 
                Visible="false" style="height: 26px" />

        <asp:Button ID="ModificarTitular" runat="server" Text="Modificar Titular" 
                CausesValidation="true" ValidationGroup="grupo1" Visible="false" 
                onclick="ModificarTitular_Click" />
                
        </td>
        <td class="col2" colspan="5">
            <asp:Button ID="Cancelar" runat="server" Text="Cancelar" onclick="Cancelar_Click" />
        </td>
        
    </tr>
    </table>
        
         <!-- DIALOGOS -->
    
        <div id="verNombres" class="dialog">
        <div class="background"></div>
        <div class="content_dialog">
            <div class="top">
                <asp:LinkButton ID="cerrar_verNombres" CssClass="cerrar" OnClientClick="javascript:close_dialog('verNombres');" CausesValidation="false" runat="server"></asp:LinkButton>
            </div>
            <div class="body">
                <fieldset>
                    <legend>Ver Nombres</legend>
                    <iframe id="iframe_verNombres" src="" width="100%" height="249px" frameborder="0" scrolling="no"></iframe>
                </fieldset>
            </div>
        </div>
        </div>

    </asp:Content>

   