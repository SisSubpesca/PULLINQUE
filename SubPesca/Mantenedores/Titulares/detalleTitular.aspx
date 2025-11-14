<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detalleTitular.aspx.cs" 
Inherits="SubPesca.Mantenedores.Titulares.detalleTitular" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="FormularioIngresoSolicitante" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManagerSolicitante" runat="server"></asp:ToolkitScriptManager>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Ver Titular</span>
            </td>
            <td align="right" valign="middle">
            <asp:ImageButton ID="Volver" ImageUrl="~/App_Themes/admin_style/images/volver.png" OnClick="Volver_Click" Height="30px" ToolTip="Volver" runat="server" />
        </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

    <fieldset>
        
        <legend>Datos del Titular</legend>
        <br />

        <asp:UpdatePanel ID="UpdatePanelDatosTitular" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
        
        <asp:Panel ID="PanelDatosTitular" Visible="true" runat="server">
                        
            <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Tipo Persona</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                        
                            <asp:TextBox ID="TipoPersona" runat="server" Width="100px" BackColor="#ddddee" ReadOnly></asp:TextBox>
                                                                         
                        </td>
                    </tr>
                    <tr>
                        <td class="col1"><span class="item">Rut Persona</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            <asp:TextBox ID="RutPersona" MaxLength="8" Width="100px" runat="server" BackColor="#ddddee" ReadOnly></asp:TextBox> - <asp:TextBox ID="DVPersona" BackColor="#ddddee" ReadOnly MaxLength="1" Width="20px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1" nowrap><span class="item">Nombre del Solicitante</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            <asp:TextBox ID="NombreSolicitante" MaxLength="40" Width="200px" BackColor="#ddddee" ReadOnly runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1"></td>
                        <td class="col2"></td>
                        <td class="col3">
                            <asp:LinkButton ID="VerNombres" OnClientClick="javascript:abre_dialogo2('verNombres',document.getElementById('ctl00_rightbody_RutPersona').value,1)" runat="server">[Ver Nombres]</asp:LinkButton>
                       </td>
                    </tr>
                    <tr>
                        <td class="col1" nowrap><span class="item">Holding</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            <asp:TextBox ID="Holding" MaxLength="40" Width="400px" BackColor="#ddddee" ReadOnly runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1" nowrap><span class="item">APE</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            <asp:TextBox ID="APE" MaxLength="40" Width="400px" BackColor="#ddddee" ReadOnly runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    
                     <asp:Panel ID="PanelRPA" Visible="false" runat="server">
                     <tr>
                        <td class="col1"><span class="item">RPA</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
            
                            <asp:TextBox ID="RPA" runat="server" BackColor="#ddddee" ReadOnly ></asp:TextBox>
            
                        </td>
                    </tr>
                    </asp:Panel>

                    <asp:Panel ID="PanelEspecialPersonaJuridica" Visible="false" runat="server">
                        <tr>
                            <td class="col1">
                                <span class="item">Nº Reg. Subpesca</span></td>
                            <td class="col2">
                                <span class="item">:</span></td>
                            <td class="col3">
                                <asp:TextBox ID="NumeroRegistroSubpesca" runat="server" Width="100px" BackColor="#ddddee" ReadOnly></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1"><span class="item">Fecha Reg. Subpesca</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3">
                        
                                <asp:TextBox ID="FechaRegistroSubpesca" runat="server" Width="140px" BackColor="#ddddee" ReadOnly></asp:TextBox>
                                                                         
                            </td>
                        </tr>
                    </asp:Panel>
                    
            </table>

        <!-- Listado de Direcciones -->
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
           AllowPaging="True" PageSize="5" OnPageIndexChanging="GridContactoMatrizSucursales_PageIndexChanged"
           >
           
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

            </Columns>

            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#5794EF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
         
         <!-- Listado de Contactos -->
         <asp:GridView 
           ID="GridContacto"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%"
           OnRowCreated="GridContacto_RowCreated"
           AllowPaging="True" PageSize="5" OnPageIndexChanging="GridContacto_PageIndexChanged"
           >
           
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

            </Columns>

            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#5794EF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>

        <!-- Listado de Representantes Legales (sólo para personas jurídicas)-->
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
           AllowPaging="True" PageSize="5" OnPageIndexChanging="GridRepresentantesLegales_PageIndexChanged">
                      
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
               
                <asp:TemplateField HeaderText="Rut Representante Legal" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.rutPersonaRepLegal")%> - <%# DataBinder.Eval(Container, "DataItem.dvRepresentante")%>
               </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Nombre Representante Legal" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.nombreRepresentante")%>
               </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
     
                     <asp:ImageButton ID="gVer" Visible="true" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "rutPersonaRepLegal") %>'
                     ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

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
           AllowPaging="True" PageSize="5">
                      
           <RowStyle BackColor="#EFF3FB" />
           <Columns>

               <asp:TemplateField HeaderText="Rut Operador">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.rutOperador")%>-<%# DataBinder.Eval(Container, "DataItem.dvOperador")%>
                </ItemTemplate>
               </asp:TemplateField>
                
               <asp:TemplateField HeaderText="Nombre Operador">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.nombreOperador")%>
                </ItemTemplate>
               </asp:TemplateField> 
               
               <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                     
                     <asp:ImageButton ID="gVer" Visible="true" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "rutOperador") %>'
                    ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

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

       <!-- Listado de Archivos Adjuntos (para todas las personas)-->
        <asp:GridView 
           ID="GridArchivosAdjuntos"
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%"
           OnRowCreated="GridArchivosAdjuntos_RowCreated"
           OnRowCommand="GridArchivosAdjuntos_RowCommand"
           OnRowDataBound="GridArchivosAdjuntos_RowDataBound"
           AllowPaging="False" OnPageIndexChanging="GridArchivosAdjuntos_PageIndexChanged">
                      
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
               <asp:BoundField HeaderText="Tipo Archivo"   DataField="nombreTipo" SortExpression="nombreTipo" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
               <asp:BoundField HeaderText="Nombre Archivo" DataField="nombreArchivo" SortExpression="nombreArchivo" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />

               <asp:BoundField HeaderText="Número CI" DataField="numeroCI" SortExpression="numeroCI" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
               <asp:BoundField HeaderText="Fecha CI" DataField="fechaCI" SortExpression="fechaCI" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
               
               <asp:BoundField HeaderText="Vigencia" DataField="nombreEstadoVig" SortExpression="nombreEstadoVig" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />

               <asp:BoundField HeaderText="Archivo Adjunto" DataField="nombreFisico" SortExpression="nombreFisico" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                             
               <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>

                    <asp:ImageButton ID="gDescargar" Visible="false" runat="server" CausesValidation="false" CommandName="Descargar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivoBinSC") %>'
                     ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" OnPreRender="ImgAdd_PreRender" />

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

       <asp:HiddenField ID="BackPage" runat="server" />

        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>

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


    </fieldset>
</asp:Content>