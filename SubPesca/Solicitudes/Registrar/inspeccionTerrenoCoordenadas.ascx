<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="inspeccionTerrenoCoordenadas.ascx.cs" Inherits="SubPesca.Solicitudes.Registrar.inspeccionTerrenoCoordenadas" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="asp" %>




<asp:UpdatePanel ID="UpdInspeccionTerrenoCoordenadas" UpdateMode="Conditional" runat="server">

<ContentTemplate>

<asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>
                    
<br />
                    
<asp:ValidationSummary ID="validacionesPoligono" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
<asp:ValidationSummary ID="validacionesVertice" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo2" />
                    
<asp:Panel ID="Content_msgGrillaGral_1" CssClass="Content_msgGrilla" Visible="false" runat="server">
    <div class="msgGrilla_div1">
        <asp:Image ID="IcoGral_1" CssClass="Ico_msgGrilla" runat="server" />
    </div>
    <div class="msgGrilla_div2">
        <asp:Label ID="msgGrillaGral_1" runat="server"></asp:Label>
    </div>
</asp:Panel>

<br />
                    
<asp:Panel ID="PanelCoordenadasGeograficaInspTerreno"  Visible="true" runat="server">
                    
<fieldset>

    <legend>Coordenadas Geográficas Inspección de Terreno</legend>
    <br />

    <asp:HiddenField ID="IdCoordenadaGeo" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="IdPoligono" runat="server" value="0" ></asp:HiddenField>

<asp:Panel ID="FormularioIngreso" runat="server" Visible="true">

<!-- Sección Polígono -->
<table class="form" cellpadding="0px" cellspacing="0px">

<tr>
    <td class="col1"><span class="item">DATUM</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3">
        <asp:UpdatePanel ID="UpdatePanelDATUMInspeccionTerrenoCoordenadas" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:DropDownList ID="DATUMinspeccionTerrenoCoordenadas" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDATUMinspeccionTerrenoCoordenadas" runat="server" ControlToValidate="DATUMinspeccionTerrenoCoordenadas" Display="Static" ErrorMessage="DATUM" InitialValue="-1" ValidationGroup="grupo2">*</asp:RequiredFieldValidator>
        </ContentTemplate>
        </asp:UpdatePanel>
    </td>
    <td class="col3">&nbsp;</td>
    <td class="col3">&nbsp;</td>
    <td class="col3">&nbsp;</td>                        
</tr>

<tr>
    <td class="col1"><span class="item">Huso Horario</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3">
        <asp:DropDownList ID="HusoHorarioinspeccionTerrenoCoordenadas" runat="server"></asp:DropDownList>
        
    </td>
    <td class="col3">&nbsp;</td>
    <td class="col3">&nbsp;</td>
    <td class="col3">&nbsp;</td>
</tr>

<tr>
    <td class="col1" nowrap>
    <span class="item">Coordenada Geográfica Ant. Sector</span></td>
    <td class="col2">
    <span class="item">:</span></td>
    <td class="col3">
    <asp:DropDownList ID="CoordenadaGeoinspeccionTerrenoCoordenadas" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator id="RequiredFieldValidatorCoordenadaGeoinspeccionTerrenoCoordenadas" runat="server" ControlToValidate="CoordenadaGeoinspeccionTerrenoCoordenadas"  ValidationGroup="grupo2" ErrorMessage="Coordenada Geográfica Ant. Sector" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
    </td>
    <td class="col3">&nbsp;</td>
    <td class="col3">&nbsp;</td>
    <td class="col3">&nbsp;</td>
</tr>

</table>

<!-- Sección de Vértice -->        
<asp:Panel ID="PanelVertice"  Visible="true" runat="server">
              
<br />

<fieldset>
<legend>Vértice</legend>
<br />
        
<asp:HiddenField ID="IdVertice" runat="server" value="0" ></asp:HiddenField>

<table class="form" cellpadding="0px" cellspacing="0px">

<tr>
    <td class="col1"><span class="item">Vértice</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3">
        <asp:DropDownList ID="VerticeInspeccionTerrenoCoordenadas" AutoPostBack="false" runat="server"></asp:DropDownList> 
        <asp:RequiredFieldValidator id="RequiredFieldValidatorVerticeInspeccionTerrenoCoordenadas" runat="server" ControlToValidate="VerticeInspeccionTerrenoCoordenadas"  ValidationGroup="grupo1"
        ErrorMessage="Vértice" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>
    </td>
    <td class="col3">&nbsp;</td>
    <td class="col3">&nbsp;</td>
</tr>

<tr>
    <td class="col1"><span class="item">Latitud</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3">

    <asp:TextBox ID="LatitudHoraInspeccionTerrenoCoordenadas" runat="server" MaxLength="2" Width="40px"></asp:TextBox> °
    <asp:RequiredFieldValidator id="RequiredFieldValidatorLatitudHoraAntecedentesEspeciales" runat="server" ControlToValidate="LatitudHoraInspeccionTerrenoCoordenadas"  ValidationGroup="grupo1" ErrorMessage="Latitud (Grado)" Display="Static">*</asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidatorLatitudHoraInspeccionTerrenoCoordenadas" ControlToValidate="LatitudHoraInspeccionTerrenoCoordenadas" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Latitud Grado debe ser entero y estar entre el rango 0 a 90." MinimumValue="0" MaximumValue="90" Type="Integer" ValidationGroup="AllValidator"></asp:RangeValidator>
                       
    <asp:TextBox ID="LatitudMinutoInspeccionTerrenoCoordenadas" runat="server" MaxLength="2" Width="40px" ></asp:TextBox> '
    <asp:RequiredFieldValidator id="RequiredFieldValidatorLatitudMinutoAntecedentesEspeciales" runat="server" ControlToValidate="LatitudMinutoInspeccionTerrenoCoordenadas"  ValidationGroup="grupo1"
    ErrorMessage="Latitud (Minuto)" Display="Static">*</asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidatorLatitudMinutoInspeccionTerrenoCoordenadas" ControlToValidate="LatitudMinutoInspeccionTerrenoCoordenadas" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Latitud Minuto debe ser entero y estar entre el rango 0 a 59." MinimumValue="0" MaximumValue="59" Type="Integer" ValidationGroup="AllValidator"></asp:RangeValidator>
                       
    <asp:TextBox ID="LatitudSegundoInspeccionTerrenoCoordenadas" runat="server" Width="70px" ></asp:TextBox> ''
    <asp:RequiredFieldValidator id="RequiredFieldValidatorLatitudInspeccionTerrenoCoordenadas" runat="server" ControlToValidate="LatitudSegundoInspeccionTerrenoCoordenadas"  ValidationGroup="grupo1"
    ErrorMessage="Latitud (Segundo)" Display="Static">*</asp:RequiredFieldValidator>
    
    <asp:RegularExpressionValidator ID="RegularExpressionValidatorLatitudSegundoInspeccionTerrenoCoordenadas" ControlToValidate="LatitudSegundoInspeccionTerrenoCoordenadas" ValidationGroup="grupo1" ForeColor="Red" runat="server" ValidationExpression="^[0-9]{1,9}(\,[0-9]{0,4})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>
                       
    </td>
    <td class="col3">&nbsp;</td>
    <td class="col3">&nbsp;</td>
</tr>

<tr>
    <td class="col1"><span class="item">Longitud</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3">
                       
    <asp:TextBox ID="LongitudHoraInspeccionTerrenoCoordenadas" runat="server" MaxLength="3" Width="40px" ></asp:TextBox> °
    <asp:RequiredFieldValidator id="RequiredFieldValidatorLongitudHoraInspeccionTerrenoCoordenadas" runat="server" ControlToValidate="LongitudHoraInspeccionTerrenoCoordenadas"  ValidationGroup="grupo1"
    ErrorMessage="Longitud (Grado)" Display="Static">*</asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidatorLongitudHoraInspeccionTerrenoCoordenadas" ControlToValidate="LongitudHoraInspeccionTerrenoCoordenadas" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Longitud Grado debe ser entero y estar entre el rango 0 a 360." MinimumValue="0" MaximumValue="360" Type="Integer" ValidationGroup="AllValidator"></asp:RangeValidator>
                       
    <asp:TextBox ID="LongitudMinutoInspeccionTerrenoCoordenadas" runat="server" MaxLength="2" Width="40px" ></asp:TextBox> '
    <asp:RequiredFieldValidator id="RequiredFieldValidatorLongitudMinutoInspeccionTerrenoCoordenadas" runat="server" ControlToValidate="LongitudMinutoInspeccionTerrenoCoordenadas"  ValidationGroup="grupo1"
    ErrorMessage="Longitud (Minuto)" Display="Static">*</asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidatorLongitudMinutoInspeccionTerrenoCoordenadas" ControlToValidate="LongitudMinutoInspeccionTerrenoCoordenadas" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Longitud Minuto debe ser entero y estar entre el rango 0 a 59." MinimumValue="0" MaximumValue="59" Type="Integer" ValidationGroup="AllValidator"></asp:RangeValidator>
                       
    <asp:TextBox ID="LongitudSegundoInspeccionTerrenoCoordenadas" runat="server" Width="70px" ></asp:TextBox> ''
    <asp:RequiredFieldValidator id="RequiredFieldValidatorLongitudSegundoInspeccionTerrenoCoordenadas" runat="server" ControlToValidate="LongitudSegundoInspeccionTerrenoCoordenadas"  ValidationGroup="grupo1"
    ErrorMessage="Longitud (Segundo)" Display="Static">*</asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidatorLongitudSegundoInspeccionTerrenoCoordenadas" ControlToValidate="LongitudSegundoInspeccionTerrenoCoordenadas" runat="server" ForeColor="Red" ValidationGroup="grupo1" ValidationExpression="^[0-9]{1,9}(\,[0-9]{1,4})?$" ErrorMessage="Ingrese formato válido Ej: 12,3"></asp:RegularExpressionValidator>
                       
    </td>
    <td class="col3">&nbsp;</td>
    <td class="col3">&nbsp;</td>
</tr>

<tr>
    <td class="col1"><span class="item">UTM E</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3">
    <asp:TextBox ID="UTMEInspeccionTerrenoCoordenadas" runat="server" Columns="10" Width="80px"></asp:TextBox>
    <asp:RequiredFieldValidator id="RequiredFieldValidatorUTMEInspeccionTerrenoCoordenadas" runat="server" ControlToValidate="UTMEInspeccionTerrenoCoordenadas"  ValidationGroup="grupo1"
    ErrorMessage="UTM E" Display="Static">*</asp:RequiredFieldValidator>
    
    <asp:RegularExpressionValidator ID="RegularExpressionValidatorUTMEInspeccionTerrenoCoordenadas" ControlToValidate="UTMEInspeccionTerrenoCoordenadas" ForeColor="Red"  ValidationGroup="grupo1" runat="server" ValidationExpression="^[0-9]{1,8}(\,[0-9]{1,4})?$" ErrorMessage="Ingrese formato válido Ej: 359824,2100"></asp:RegularExpressionValidator> 

    </td>
    <td class="col3" style="margin-left: 40px"></td>
    <td class="col3" style="margin-left: 40px">
    
    
    </td>
</tr>

    <tr>
        <td class="col1">
            <span class="item">UTM N</span></td>
        <td class="col2">
            <span class="item">:</span></td>
        <td class="col3">
            <asp:TextBox ID="UtmNInspeccionTerrenoCoordenadas" runat="server" Columns="10" Width="80px"></asp:TextBox>
        <asp:RequiredFieldValidator id="RequiredFieldValidatorUtmNInspeccionTerrenoCoordenadas" runat="server" ControlToValidate="UtmNInspeccionTerrenoCoordenadas"  ValidationGroup="grupo1"
    ErrorMessage="UTM N" Display="Static">*</asp:RequiredFieldValidator>
    
    <asp:RegularExpressionValidator ID="RegularExpressionValidatorUtmNInspeccionTerrenoCoordenadas" ControlToValidate="UtmNInspeccionTerrenoCoordenadas" ForeColor="Red"  ValidationGroup="grupo1" runat="server" ValidationExpression="^[0-9]{1,8}(\,[0-9]{1,4})?$" ErrorMessage="Ingrese formato válido Ej: 7951532,1200"></asp:RegularExpressionValidator> 

        </td>
        <td class="col3" style="margin-left: 40px">
            &nbsp;</td>
        <td class="col3" style="margin-left: 40px">
            &nbsp;</td>
    </tr>


                            <tr>
                            <td colspan="5" nowrap="nowrap">
                                <table width="100%" border="0">
                                <tr>
                                    <td align="center">
                                                <asp:Panel ID="PanelBotonesInspeccionTerrenoCoordenadas"  Visible="true" runat="server">
                                                <asp:ImageButton ID="ImageButton1" runat="server" 
                                                ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                                AlternateText="Guardar Vértice" ToolTip="Guardar Vértice" 
                                                CausesValidation="true" 
                                                ValidationGroup="grupo1" style="width: 20px" 
                                                onclick="GuardarVerticeInspeccionTerrenoCoordenadas_Click" />
                                                <span class="item">Guardar Vértice</span>
                                                &nbsp;&nbsp;
                                                <asp:ImageButton ID="LimpiarVertice" runat="server" 
                                                ImageUrl="~/App_Themes/admin_style/images/clean.png" Height="20px" 
                                                AlternateText="Limpiar Vértice" ToolTip="Limpiar Vértice" onclick="LimpiarVertice_Click" 
                                                />
                                            </asp:Panel>
                                    </td>
                                </tr>
                                </table>
                            </td>  
                            </tr>

                    <tr>
                    <td colspan="5">
                    <!-- Lista de Vértices -->
                    <asp:HiddenField ID="Accion" runat="server" Value="0"></asp:HiddenField>

                    <asp:GridView 
                    ID="GridVerticeInspeccionTerrenoCoordenadas"
                    runat="server"
                    AutoGenerateColumns="False" 
                    CellPadding="4" 
                    ForeColor="#333333" 
                    GridLines="None"
                    AllowPaging="True" PageSize="10"
                    CssClass="mGrid"
                    OnRowDataBound="GridVerticeInspeccionTerrenoCoordenadas_RowDataBound"
                    OnRowCommand="GridVerticeInspeccionTerrenoCoordenadas_RowCommand"
                    PagerStyle-CssClass="pgr"
                    
                    Width="100%">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                    <asp:TemplateField HeaderText="Vértice">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.vertice.descripcion") %>
                    </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Latitud">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.latitudHora") + "º" + DataBinder.Eval(Container, "DataItem.latitudMinuto") + "'" + DataBinder.Eval(Container, "DataItem.latitudSegundo") + "''"%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Longitud">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container, "DataItem.longitudHora") + "º" + DataBinder.Eval(Container, "DataItem.longitudMinuto") + "'" + DataBinder.Eval(Container, "DataItem.longitudSegundo") + "''"%>
                    </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="UTM E">
                        <ItemTemplate>
                        <%#Eval("utmE", "{0:f4}")%>
                        </ItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="UTM N">
                        <ItemTemplate>
                            <%#Eval("utmN", "{0:f4}")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                        <ItemTemplate>

                        <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idVertice") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                        ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                        <asp:ImageButton ID="gModificar" Visible="false" runat="server" CausesValidation="false" CommandName="Modificar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idVertice") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                        ImageUrl="../../App_Themes/admin_style/images/modificar.png" Height="20px" AlternateText="Modificar" ToolTip="Modificar" />
                        <asp:ImageButton ID="gEliminar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idVertice") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                        ImageUrl="../../App_Themes/admin_style/images/eliminar.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />
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
</td>
</tr>

</table>

</fieldset>
</asp:Panel>

<!-- Sección de boton guardar -->        
<table class="form" cellpadding="0px" cellspacing="0px">

<tr>
                            <td align="center">
                                <asp:Panel ID="PanelBotonesPoligonoOriginal" runat="server" Visible ="true">
                                    <asp:Panel ID="PanelBotonesPoligonoInspeccionTerreno"  Visible="true" runat="server">
                                        <asp:ImageButton ID="ImageButton2" runat="server" 
                                         AlternateText="Guardar Polígono" Height="20px" 
                                         ImageUrl="~/App_Themes/admin_style/images/add.png"
                                         CausesValidation="true" ValidationGroup="grupo2" 
                                         style="width: 20px" ToolTip="Guardar Polígono" 
                                         onclick="GuardarPoligonoInspeccionTerrenoCoordenadas_Click" />
                                        <span class="item">Guardar Polígono</span>
                                    </asp:Panel><asp:ImageButton ID="LimpiarPoligono" runat="server" 
                                            AlternateText="Limpiar Poligono" Height="20px" 
                                            ImageUrl="~/App_Themes/admin_style/images/clean.png" 
                                            onclick="LimpiarPoligono_Click" ToolTip="Limpiar Poligono" />
                                </asp:Panel>
                            </td>
                        </tr>

</table>

</asp:Panel>


<!-- Lista de Polígonos -->
<table class="form" cellpadding="0px" cellspacing="0px">
<tr>
    <td>
        
        <asp:GridView ID="GridPoligonosInspeccionTerrenoCoordenadas" runat="server" 
         AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" CssClass="mGrid" 
         ForeColor="#333333" GridLines="None"
         OnRowCommand="GridPoligonosInspeccionTerrenoCoordenadas_RowCommand" 
         OnRowCreated="GridPoligonosInspeccionTerrenoCoordenadas_RowCreated" 
         OnRowDataBound="GridPoligonosInspeccionTerrenoCoordenadas_RowDataBound"  
         PagerStyle-CssClass="pgr" PageSize="10" Width="100%">
         <RowStyle BackColor="#EFF3FB" />
             <Columns>
             <asp:TemplateField HeaderText="Datum">
             <ItemTemplate>
                
                <%# DataBinder.Eval(Container, "DataItem.poligonoInspTerreno.coordenadaPoligono.datum.descripcion")%>   
             </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Huso Horario">
             <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.poligonoInspTerreno.coordenadaPoligono.tipoHuso.descripcion")%>     
             </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Coordenada Geográfica Ant. Sector">
             <ItemTemplate>
                Nº Poligono:<%# DataBinder.Eval(Container, "DataItem.poligonoAntecSector.idPoligono")%>-<%# DataBinder.Eval(Container, "DataItem.poligonoAntecSector.tipoUso.descripcion")%>-<%# DataBinder.Eval(Container, "DataItem.poligonoAntecSector.toponimio")%>-<%# DataBinder.Eval(Container, "DataItem.tipoConcesAntecSector")%>   
            </ItemTemplate>
             </asp:TemplateField>
             <asp:BoundField HeaderText="Comparación Automática de Coordenadas" DataField="comparacionString"  ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
             <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="200px">
             <ItemTemplate>
                  
                  <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.poligonoInspTerreno.idPoligono") %>'
                  ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />

                  <asp:ImageButton ID="gDesasociar" Visible="false" runat="server" CausesValidation="false" CommandName="Desasociar" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.poligonoInspTerreno.idPoligono") %>'
                  ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Desasociar" ToolTip="Desasociar" />
                    
                  <asp:ImageButton ID="gAsociar" Visible="false" runat="server" CausesValidation="false" CommandName="Asociar" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.poligonoInspTerreno.idPoligono") %>'
                  ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Asociar" ToolTip="Asociar" />

                  <asp:ImageButton ID="gModificar" runat="server" AlternateText="Modificar" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.poligonoInspTerreno.idPoligono") %>' 
                  CommandName="Modificar" Height="20px" ImageUrl="../../App_Themes/admin_style/images/modificar.png" ToolTip="Modificar" Visible="false" />
                                    
                  <asp:ImageButton ID="gEliminar" runat="server" AlternateText="Eliminar" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.poligonoInspTerreno.idPoligono") %>' 
                  CommandName="Eliminar" Height="20px" ImageUrl="../../App_Themes/admin_style/images/eliminar.png" ToolTip="Eliminar" Visible="false" />

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
                
    </td>
</tr>
</table>


</fieldset>

</asp:Panel>

</ContentTemplate>
</asp:UpdatePanel>