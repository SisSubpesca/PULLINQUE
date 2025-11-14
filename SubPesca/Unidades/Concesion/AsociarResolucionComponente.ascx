<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AsociarResolucionComponente.ascx.cs" Inherits="SubPesca.Unidades.Concesion.AsociarResolucionComponente" %>
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



        <table class="form" cellpadding="0px" cellspacing="0px" style="display:none;">
        <tr>
            <td>&nbsp;IdSolicitud: <asp:TextBox ID="IdSolicitud" runat="server"></asp:TextBox></td>
        </tr>
        </table>


  <asp:Panel  Visible="false" runat="server">

        <fieldset>

    
            <legend>Asociar Resolucion Manualmente</legend>
            <br />

                
        <asp:UpdatePanel ID="UpdatePanelFormularioIngreso" UpdateMode="Conditional" runat="server">
            <ContentTemplate>              
                <asp:Panel ID="PanelFormularioIngreso" Visible="true" runat="server">
        

                <asp:UpdatePanel ID="UpdatePanelFlujoDocumental" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>              
                        <table class="form" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td class="col1"><span class="item">Resolucion</td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3"><asp:DropDownList ID="Resolucion" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Resolucion_OnSelectedIndexChanged"></asp:DropDownList> *</td>
                        </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>

            
                <table class="form" cellpadding="0px" cellspacing="0px">   
                <tr>
                    <td class="col1"></td>
                    <td class="col2"></td>
                    <td class="col3"><asp:Button ID="Guardar" runat="server" Text="<%$Resources:spanish.language,guardar%>"  CausesValidation="true" onclick="Guardar_Click" style="height: 26px" /></td>
                </tr>
                </table>



            </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>



    



           <asp:UpdatePanel ID="UpdatePanelGridResolucionesManuales" UpdateMode="Conditional" runat="server">
            <ContentTemplate>                        
                <asp:Panel ID="PanelGridResolucionesManuales"  Visible="true" runat="server">
                
                    <asp:GridView 
                    ID="GridResolucionesManuales" 
                    runat="server" 
                    AutoGenerateColumns="False" 
                    CellPadding="4" 
                    ForeColor="#333333"
                    TabIndex="1"
                    GridLines="None" 
                    CssClass="mGrid"
                    OnRowDataBound="GridResolucionesManuales_RowDataBound"
                    PagerStyle-CssClass="pgr"
                    OnRowCommand="GridResolucionesManuales_RowCommand"
                    OnRowCreated="GridResolucionesManuales_RowCreated">
                        
                    <Columns>

                        <asp:BoundField HeaderText="Resolucion"              DataField="cadena"      ItemStyle-HorizontalAlign="Center" />
                    
                        <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="200px">
                            <ItemTemplate>
                                <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="Ver" CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "resolucion.idResolucion")  + ";" + DataBinder.Eval(Container.DataItem, "solicitud.idSolConcesion") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver" ToolTip="Ver" />


                                <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar"  CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "resolucion.idResolucion")  + ";" + DataBinder.Eval(Container.DataItem, "solicitud.idSolConcesion") %>'
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
            </ContentTemplate>
        </asp:UpdatePanel>


    </fieldset>

    <br />


</asp:Panel>


    <fieldset>

    
    <legend>Resoluciones y Decretos</legend>
    <br />



    <asp:UpdatePanel ID="UpdatePanelErroresInferior" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelErroresInferior" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="ErroresInferior" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>



    
      <asp:UpdatePanel ID="UpdatePanelGridRequerimiento" UpdateMode="Conditional" runat="server">
        <ContentTemplate>                        
            <asp:Panel ID="PanelGridRequerimiento"  Visible="true" runat="server" CssClass="Content_Grilla">
                
                <asp:GridView 
                ID="GridRequerimiento" 
                runat="server" 
                AutoGenerateColumns="False" 
                CellPadding="4" 
                ForeColor="#333333"
                TabIndex="1"
                GridLines="None" 
                CssClass="mGrid"
                OnRowDataBound="GridRequerimiento_RowDataBound"
                OnRowCommand="GridRequerimiento_RowCommand"
                PagerStyle-CssClass="pgr"
                OnRowCreated="GridRequerimiento_RowCreated">
                        
                <Columns>


                    <asp:TemplateField Visible="true">
                        <ItemTemplate>
                            <asp:Label HeaderText="idRefUEDocGeneral" ID="hiddenIdRefUEDocGeneral" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idRefUEDocGeneral") %>'></asp:Label>
                            <asp:Label HeaderText="validaConforme" ID="validaConforme" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "validaConforme") %>'></asp:Label>
                            <asp:Label HeaderText="idEstadoVigencia" ID="hidden3" runat="server" Visible="true" Text='<%# (DataBinder.Eval(Container.DataItem, "idEstadoVigRequerimiento") == DBNull.Value? DataBinder.Eval(Container.DataItem, "idEstadoVigencia"): DataBinder.Eval(Container.DataItem, "idEstadoVigRequerimiento")) %>'></asp:Label>
                            <asp:Label HeaderText="idDocGeneral" ID="hidden1" runat="server" Visible="true" Text='<%# (DataBinder.Eval(Container.DataItem, "idDocGeneral") == DBNull.Value? DataBinder.Eval(Container.DataItem, "idDocGeneralResp") : DataBinder.Eval(Container.DataItem, "idDocGeneral")) %>'></asp:Label>
                            <asp:Label HeaderText="rowspan" ID="hidden4" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "columnas") %>'></asp:Label>
                            <asp:Label HeaderText="idArchivo" ID="gIDArchivo" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "idArchivoBinSC") %>'></asp:Label>
                            <asp:Label HeaderText="idResolucion" ID="gIDResolucion" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "idResolucion") %>'></asp:Label>
                            <asp:Label HeaderText="observacion" ID="gObservacion" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "observaciones") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="true">
                        <ItemTemplate>
                            <asp:Label HeaderText="idPestana" ID="hidden2" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem, "idPestana") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    
                    <asp:BoundField HeaderText="Ámbito"         DataField="nombrePestana" SortExpression="nombrePestana" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    
                    
                    <%--
                    <asp:BoundField HeaderText="Tema"           DataField="nombreSubRequerimiento" SortExpression="nombreSubRequerimiento" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    --%>
                    
                    <asp:TemplateField ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                     <ItemTemplate>
                        <asp:Label HeaderText="Materia" runat="server" Visible="true" Text='<%# (DataBinder.Eval(Container.DataItem, "nombreMateria") == DBNull.Value? DataBinder.Eval(Container.DataItem, "nombreSubRequerimiento") : DataBinder.Eval(Container.DataItem, "nombreMateria")) %>'></asp:Label>
                     </ItemTemplate>
                    </asp:TemplateField>
                            
                    <asp:BoundField HeaderText="Tipo Documento" DataField="nombreTipoDocReq"  SortExpression="nombreTipoDocReq"      ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Nº"             DataField="numeroReq" SortExpression="numeroReq"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Fecha"          DataField="fechaReq" SortExpression="fechaReq" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField HeaderText="Destino"        DataField="nombreTipoDestinatario" SortExpression="nombreTipoDestinatario" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            

                    
                    <asp:BoundField HeaderText="Tipo Documento" DataField="nombreTipoDocResp" SortExpression="nombreTipoDocResp" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Origen"             DataField="nombreTipoOrigen"     SortExpression="nombreTipoOrigen"     ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Nº"                 DataField="numeroResp"   SortExpression="numeroResp"    ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Fecha"              DataField="fechaResp"    SortExpression="fechaResp"    ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField HeaderText="Nº C.I"             DataField="numeroRespCI" SortExpression="numeroRespCI"    ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Fecha C.I"          DataField="fechaRespCI"  SortExpression="fechaRespCI"    ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField HeaderText="Resultado"          DataField="nombreResultadoResp" SortExpression="nombreResultadoResp"    ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Vigencia"           DataField="nombreEstadoVigencia" SortExpression="nombreEstadoVigencia"   ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    
                    
                    <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="200px">
                        <ItemTemplate>

                           <asp:Image ID="ImageButton1" Visible="true" runat="server"
                                ImageUrl="~/App_Themes/admin_style/images/list.png" Height="20px" AlternateText="Ver Observación" ToolTip='<%# (DataBinder.Eval(Container.DataItem, "observaciones")) %>' />


                            <asp:ImageButton ID="gVer" Visible="false" runat="server" CausesValidation="false" CommandName="VerResolucion" CommandArgument='<%# (DataBinder.Eval(Container.DataItem, "idDocGeneral") == DBNull.Value? DataBinder.Eval(Container.DataItem, "idDocGeneralResp"): DataBinder.Eval(Container.DataItem, "idDocGeneral")) + ";" + DataBinder.Eval(Container.DataItem, "idPestana") + ";" + DataBinder.Eval(Container.DataItem, "idResolucion") %>'
                                ImageUrl="~/App_Themes/admin_style/images/ver.png" Height="20px" AlternateText="Ver Resolución" ToolTip="Ver Resolución" />

                            <asp:ImageButton ID="gBorrar" Visible="false" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# (DataBinder.Eval(Container.DataItem, "idDocGeneral") == DBNull.Value? DataBinder.Eval(Container.DataItem, "idDocGeneralResp"): DataBinder.Eval(Container.DataItem, "idDocGeneral")) + ";" + DataBinder.Eval(Container.DataItem, "idPestana") + ";" + DataBinder.Eval(Container.DataItem, "idResolucion") %>'
                                ImageUrl="../../App_Themes/admin_style/images/delete.png" Height="20px" AlternateText="Eliminar" ToolTip="Eliminar" />

                            <asp:ImageButton ID="gDescargar" Visible="false" runat="server" CausesValidation="false" CommandName="DescargarArchivo" CommandArgument='<%# (DataBinder.Eval(Container.DataItem, "idDocGeneral") == DBNull.Value? DataBinder.Eval(Container.DataItem, "idDocGeneralResp"): DataBinder.Eval(Container.DataItem, "idDocGeneral")) + ";" + DataBinder.Eval(Container.DataItem, "idPestana") + ";" + DataBinder.Eval(Container.DataItem, "idResolucion") %>'
                                ImageUrl="~/App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" OnPreRender="ImgAdd_PreRender" />

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


     

    </fieldset>