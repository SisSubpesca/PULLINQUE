<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="asignarResoluciones.ascx.cs" Inherits="SubPesca.Resoluciones.asignarResoluciones" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>


<asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
   <ContentTemplate>    
       <asp:panel ID="Panel1" runat="server">
            <asp:ValidationSummary ID="ValidationSummaryErrores" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList"  />
        </asp:panel>
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
   

        <legend>Asociación de Resoluciones</legend>
        <br />


        
        <asp:UpdatePanel ID="UpdatePanelResoluciones" UpdateMode="Conditional" runat="server">
            <ContentTemplate>              
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Resolución</td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:DropDownList ID="Resolucion" AutoPostBack="true" runat="server"></asp:DropDownList> *</td>
                </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelSeccion" UpdateMode="Conditional" runat="server">
            <ContentTemplate>              
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Sección</td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3"><asp:DropDownList ID="Seccion" AutoPostBack="true" runat="server" OnSelectedIndexChanged="FlujoDocumental_change"></asp:DropDownList> *</td>
                </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

        
     
        <asp:UpdatePanel ID="UpdatePanelNuevaFecha" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                                    
                <asp:Panel ID="PanelNuevaFecha"  Visible="true" runat="server">
                    <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="NuevaFechaLiteral" runat="server" Text="<%$Resources:spanish.language,nuevafecha%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td><td class="col3">

                            <div class="calendario">
                                <div class="calendario_textbox">               
                                    <asp:TextBox ID="NuevaFecha" Columns="8" Width="80px" runat="server"></asp:TextBox><asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="NuevaFecha"
                                        Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                                    </asp:MaskedEditExtender>

                                </div>
                                <div class="calendario_icono">
                                    <asp:Image src="../../App_Themes/admin_style/images/calendar.png" id="nuevaFechaImgDinamica" alt="Calendario" runat="server" style="vertical-align: middle" />
                                </div>
                        </div>
                        </td>
                    </tr>
                    </table>
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>


            
        <table class="form" cellpadding="0px" cellspacing="0px">   
        <tr>
            <td class="col1"></td>
            <td class="col2"></td>
            <td class="col3">
                <asp:Button ID="Button1" runat="server" Text="<%$Resources:spanish.language,guardar%>"  CausesValidation="true" onclick="GridResolucion_Agregar" style="height: 26px" />
            </td>
        </tr>
        </table>



        
    <br /> 


    <asp:UpdatePanel ID="UpdatePanelResolucion" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelResolucion"  Visible="true" runat="server">
                
                <asp:GridView 
                ID="GridResolucion" 
                runat="server" 
                AutoGenerateColumns="False" 
                CellPadding="4" 
                ForeColor="#333333"
                TabIndex="1"
                GridLines="None" 
                CssClass="mGrid"
                OnRowDataBound="GridResolucion_RowDataBound"
                PagerStyle-CssClass="pgr"
                OnRowCommand="GridResolucion_RowCommand"
                OnRowCreated="GridResolucion_RowCreated">
                        
                <Columns>

                 

                    <asp:TemplateField HeaderText="Resolución">
                        <ItemTemplate>
                                <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                <%# DataBinder.Eval(Container, "DataItem.tipoUnidadEspacial.descripcion")%>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    
                    <asp:BoundField HeaderText="Seccion" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    

                    <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="200px">
                        <ItemTemplate>

                            <asp:ImageButton ID="gVigenciaLocal" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "index") %>'
                                ImageUrl="~/App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />


                            <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "index") %>'
                                    ImageUrl="~/App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />

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
    
    
    
    <asp:UpdatePanel ID="UpdatePanelErroresInferior" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelErroresInferior" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="ErroresInferior" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    </fieldset>

