<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="resumenConcesionComponente.ascx.cs" Inherits="SubPesca.Unidades.Concesion.resumenConcesionComponente" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>



   

    <style type="text/css">
        .style1
        {
            width: 199px;
        }
        .style2
        {
            width: 206px;
        }
        .style3
        {
            width: 198px;
        }
    </style>



   

    <asp:ToolkitScriptManager ID="ToolkitScriptManagerSolicitante" runat="server" EnablePartialRendering="false"></asp:ToolkitScriptManager>
    
    <asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>
   
    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
               <span id="titulo_modulo"> Resumen de <asp:Label ID="NombreUnidadEspacial" runat="server"></asp:Label></span>
            </td>
        </tr>
    </table>

    <hr style="width:100%;" />


 <fieldset>
 <legend>Datos Generales</legend>
   <asp:UpdatePanel ID="UpdatePanelDatosGeneral" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 
    <asp:Panel ID="PanelDatosGeneral"  Visible="true" runat="server">
    
        
        <br />
            
                <asp:UpdatePanel ID="UpdatePanelCodigoCentro" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 
                <asp:Panel ID="PanelCodigoCentro"  Visible="true" runat="server">
                    <table>
                    <tr>
                        <td class="style1"><span class="item">Código de Centro</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            <asp:Label ID="CodigoCentro" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    </table>
                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>


                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 
                <asp:Panel ID="Panel2"  Visible="true" runat="server">
                    <table>
                    <tr>
                        <td class="style1"><span class="item">PERT/Numero identificador solicitud</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3">
                            <asp:Label ID="Pert" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    </table>
                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>

                

                <asp:UpdatePanel ID="UpdatePanelNumeroDiarioOficial" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 
                <asp:Panel ID="PanelNumeroDiarioOficial"  Visible="true" runat="server">
                <table>
                <tr>
                    <td class="style1"><span class="item">N° Diario Oficial</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                        <asp:Label ID="NumeroDiarioOficial" runat="server"></asp:Label>
                    </td>
                </tr>
                </table>
                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="UpdatePanelFechaDiarioOficial" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 
                <asp:Panel ID="PanelFechaDiarioOficial"  Visible="true" runat="server">
                <table>
                <tr>
                    <td class="style1"><span class="item">Fecha de Diario Oficial</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3">
                        <asp:Label ID="FechaDiarioOficial" runat="server" ></asp:Label>
                    </td>
                </tr>
                </table>
                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="UpdatePanelNumeroActaEntrega" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 
                <asp:Panel ID="PanelNumeroActaEntrega"  Visible="true" runat="server">
                <table>
                    <tr>
                        <td class="style1">
                            <span class="item">N° de Acta de Entrega</span></td>
                        <td class="col2">
                            :</td>
                        <td class="col3">
                            <asp:Label ID="NumeroActaEntrega" runat="server" ></asp:Label>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="UpdatePanelFechaActaEntrega" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 
                <asp:Panel ID="PanelFechaActaEntrega"  Visible="true" runat="server">
                <table>
                    <tr>
                        <td class="style1">
                            <span class="item">Fecha de Entrega</span></td>
                        <td class="col2">
                            :</td>
                        <td class="col3">
                            <asp:Label ID="FechaActaEntrega" runat="server" ></asp:Label>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="UpdatePanelCapitaniaDePuerto" UpdateMode="Conditional" runat="server">
                <ContentTemplate> 
                <asp:Panel ID="PanelCapitaniaDePuerto"  Visible="true" runat="server">
                <table>
                    <tr>
                        <td class="style1">
                            <span class="item">Capitanía de Puerto</span></td>
                        <td class="col2">
                            :</td>
                        <td class="col3">
                            <asp:Label ID="CapitaniaPuerto" runat="server" ></asp:Label>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>

                <table>
                    <tr>
                        <td class="style1">
                            <span class="item">Actual Titular</span></td>
                        <td class="col2">
                            :</td>
                        <td class="col3">
                            <asp:Label ID="ActualTitular" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <span class="item">Especie</span></td>
                        <td class="col2">
                            :</td>
                        <td class="col3">
                            <asp:Label ID="Especie" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <span class="item">Grupo Informativo</span></td>
                        <td class="col2">
                            :</td>
                        <td class="col3">
                            <asp:Label ID="GrupoInformativo" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
        
    
    <br />
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
   
   <asp:UpdatePanel ID="UpdatePanelSuperficie" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
        
        <asp:Panel ID="PanelSuperficie" Visible="true" runat="server">

        <asp:GridView 
           ID="GridViewSuperficie" 
           runat="server"
           AutoGenerateColumns="False" 
           CellPadding="4" 
           ForeColor="#333333" 
           GridLines="None"
           AllowPaging="True" PageSize="10" 
           AllowSorting="True"
           CssClass="mGrid"
           PagerStyle-CssClass="pgr"
           Width="100%">
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
               <asp:TemplateField HeaderText="Tipo Uso" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.tipoUso.descripcion")%>
                </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Toponimio" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.toponimio")%>
                </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Superficie Calculada" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                   <%# DataBinder.Eval(Container, "DataItem.areaCalculada")%>
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

       <br />
        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>
   
   <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
   <ContentTemplate> 
   <asp:Panel ID="Panel1" Visible="true" runat="server">
   <table>
                <tr>
                    <td class="style3">
                        <span class="item">Región</span></td>
                    <td class="col2">
                        :</td>
                    <td class="col3">
                        <asp:Label ID="Region" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <span class="item">Comuna</span></td>
                    <td class="col2">
                        :</td>
                    <td class="col3">
                        <asp:Label ID="Comuna" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <span class="item">Provincia</span></td>
                    <td class="col2">
                        :</td>
                    <td class="col3">
                        <asp:Label ID="Provincia" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <span class="item">Plazo Nominal</span></td>
                    <td class="col2">
                        :</td>
                    <td class="col3">
                        <asp:Label ID="PlazoNominal" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <span class="item">Plazo de Inicio</span></td>
                    <td class="col2">
                        :</td>
                    <td class="col3">
                        <asp:Label ID="PlazoInicio" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <span class="item">Plazo de Vencimiento</span></td>
                    <td class="col2">
                        :</td>
                    <td class="col3">
                        <asp:Label ID="PlazoVencimiento" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <span class="item">Vigencia</span></td>
                    <td class="col2">
                        :</td>
                    <td class="col3">
                        <asp:Label ID="Vigencia" runat="server"></asp:Label>
                    </td>
                </tr>
                </table>
  
 </asp:Panel>
 </ContentTemplate>
 </asp:UpdatePanel>  
 </fieldset>