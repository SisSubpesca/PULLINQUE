<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="agregarArchivoAdjunto.ascx.cs" Inherits="SubPesca.Mantenedores.Titulares.agregarArchivoAdjunto1" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

                        <fieldset>
                        <legend>Archivos Adjuntos</legend>
                        <br /> 
                    
                        <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
                        <div class="msgGrilla_div1">
                            <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
                        </div>
                        <div class="msgGrilla_div2">
                            <asp:Label ID="msgGrilla" runat="server"></asp:Label>
                        </div>
                        </asp:Panel>

                        <asp:ValidationSummary ID="ValidationSummaryArchivo" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupoArchivo" />

                        <table class="form" cellpadding="0px" cellspacing="0px">
                    
                       <tr>
                       <td class="col1"><span class="item">Tipo Archivo</span></td>
                       <td class="col2"><span class="item">:</span></td>
                       <td class="col3">
                   
                       <asp:DropDownList ID="TipoArchivo" AutoPostBack="false" runat="server"></asp:DropDownList>
                       <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoArchivoAntTerreno" runat="server" ControlToValidate="TipoArchivo" ValidationGroup="grupoArchivo" 
                       ErrorMessage="Tipo Archivo" InitialValue="-1" Display="Static" >*</asp:RequiredFieldValidator>

                       </td>
                        </tr>
                        <tr>
                           <td class="col1"><span class="item">Nombre Archivo</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3">

                           <asp:TextBox ID="NombreArchivo" MaxLength="40" Width="200px" runat="server"></asp:TextBox>
                           <asp:RequiredFieldValidator id="RequiredFieldValidatorNombreArchivoAntTerreno" runat="server" ControlToValidate="NombreArchivo" ValidationGroup="grupoArchivo" 
                           ErrorMessage="Nombre Archivo" Display="Static" >*</asp:RequiredFieldValidator>

                           </td>
                        </tr>

                        <tr>
                            <td class="col1"><span class="item">N° Control Ingreso</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3" colspan="4">
            
                                <asp:TextBox ID="NroControlIngresoArchivoAdj" runat="server"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidatorNumeroControlIngresoArchivoAdj" runat="server" Type="Integer" MinimumValue="1" MaximumValue="1999999999" ForeColor="Red" ControlToValidate="NroControlIngresoArchivoAdj" ErrorMessage="Nº Control Ingreso en rango no permitido" ValidationGroup="grupoArchivo" />
                                <asp:CompareValidator ID="CompareValidatorNumeroControlIngresoArchivoAdj" runat="server" Operator="DataTypeCheck" Type="Integer" ForeColor="Red" ControlToValidate="NroControlIngresoArchivoAdj" ErrorMessage="Nº Control Ingreso Ingrese valores numéricos" ValidationGroup="grupoArchivo" />
        
                            </td>
                        </tr>
                        <tr>
                            <td class="col1"><span class="item">Fecha Control Ingreso</span></td>
                            <td class="col2"><span class="item">:</span></td>
                            <td class="col3" colspan="4">
            
                                <asp:UpdatePanel ID="UpdatePanel_FechaRecepcionArchAdj" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div class="calendario">
                                        <div class="calendario_textbox">               
                                            <asp:TextBox ID="FechaTextRecepcionArchAdj" Width="120px" runat="server" ></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtenderArchivoAdj" runat="server"  Enabled="True"
                                            Format="dd'/'MM'/'yyyy'"  TargetControlID="FechaTextRecepcionArchAdj" PopupButtonID="endCal1"  CssClass="cal_Theme1" FirstDayOfWeek="Monday"/>
                                            <img runat="server" id="endCal1" alt ="CalendarioRecepcion" src="../../App_Themes/admin_style/images/calendar.png" style="cursor: hand;" />
                                            <asp:RegularExpressionValidator 
                                             ID="RegularExpressionValidatorArchivoAdj" 
                                             runat="server"
                                             ControlToValidate="FechaTextRecepcionArchAdj"
                                             ForeColor="Red"
                                             ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"  
                                             ErrorMessage="Ingrese formato válido"
                                             ValidationGroup="grupoArchivo">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                </asp:UpdatePanel>
            
                            </td>
                        </tr>
                        
                        <tr>
                           <td class="col1"><span class="item">Archivo Adjunto</span></td>
                           <td class="col2"><span class="item">:</span></td>
                           <td class="col3">
                           
                           <asp:UpdatePanel ID="UpdatePanelArchivoAdjunto" UpdateMode="Conditional" runat="server">
                           <ContentTemplate>

                           <asp:FileUpload ID="ArchivoAdjunto" MaxLength="40" Width="200px" runat="server" />

                           <asp:RequiredFieldValidator id="RequiredFieldValidatorArchivoAdjunto" runat="server" ControlToValidate="ArchivoAdjunto" ValidationGroup="grupoArchivo" 
                           ErrorMessage="Archivo Adjunto" Display="Static" InitialValue="0">*</asp:RequiredFieldValidator>

                           <asp:RegularExpressionValidator ID="RegularExpressionValidatorArchivoAdjunto" runat="server" ErrorMessage="Formato Archivo Incorrecto" ControlToValidate="ArchivoAdjunto" ValidationExpression= "(.*).(.doc|.DOC|.pdf|.PDF|.docx|.DOCX|.xls|.xlsx|.XLS|.XLSX|.dwg|.DWG|.dxf|.DXF|.jpg|.JPG|.jpeg|.JPEG)$" ValidationGroup="grupoArchivo" />

                           </ContentTemplate>
                           <Triggers>

                                <asp:PostBackTrigger ControlID="GuardarArchivoAdjunto" />

                           </Triggers>
                           </asp:UpdatePanel>

                           </td>
                        </tr>

                        <tr>
                            <td class="col1"></td>
                            <td class="col2"></td>
                            <td class="col3">
                                <asp:ImageButton ID="GuardarArchivoAdjunto" runat="server" 
                                    ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                    AlternateText="Guardar Archivo Adjunto" ToolTip="Guardar Archivo Adjunto" 
                                    CausesValidation="true" ValidationGroup="grupoArchivo" 
                                    onclick="GuardarArchivoAdjunto_Click"/>
                                <span class="item">Guardar Archivo Adjunto</span>
                            </td>
                        </tr>
                        </table>

                        <asp:UpdatePanel ID="UpdatePanelArchivoAdjuntoGrilla" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                        
                        <asp:Panel ID="PanelArchivoAdj"  Visible="true" runat="server">
                        <asp:GridView 
                            ID="GridArchivoAdjunto" 
                            runat="server" 
                            AutoGenerateColumns="False"
                            DataKeyNames="idArchivo"  
                            CellPadding="4" 
                            ForeColor="#333333"
                            GridLines="None"
                            AllowPaging="False"
                            AllowSorting="false" 
                            CssClass="mGrid"
                            PagerStyle-CssClass="pgr" 
                            width="100%" 
                            PageIndex= "1"
                            OnRowDataBound="GridArchivoAdjunto_RowDataBound"
                            OnRowCommand="GridArchivoAdjunto_RowCommand"
                            OnRowCreated="GridArchivoAdjunto_RowCreated"
                            >
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                               <asp:TemplateField HeaderText="Tipo Archivo">
                                    <ItemTemplate>

                                    <asp:HiddenField ID="gAccion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.accion") %>' />
                                    <%# DataBinder.Eval(Container, "DataItem.tipoDocumento.descripcion")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="Nombre Archivo">
                                    <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.archivoBinario.nombreArchivo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Numero CI">
                                    <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.numCIStringMetodo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha CI">
                                    <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.fechaCIStringMetodo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Vigencia">
                                    <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.estadoVigencia.descripcion")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Archivo Adjunto">
                                    <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.archivoBinario.nombreFisico")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Opciones" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="gDesasociar" Visible="false" runat="server" CausesValidation="false" CommandName="Desasociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivo") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                            ImageUrl="../../App_Themes/admin_style/images/unauth.png" Height="20px" AlternateText="Desasociar" ToolTip="Desasociar" />
                    
                                        <asp:ImageButton ID="gAsociar" Visible="false" runat="server" CausesValidation="false" CommandName="Asociar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivo") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                            ImageUrl="../../App_Themes/admin_style/images/realizado.png" Height="20px" AlternateText="Asociar" ToolTip="Asociar" />

                                        <asp:ImageButton ID="gDescargar" Visible="true" runat="server" CausesValidation="false" CommandName="Descargar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivo") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
                                        ImageUrl="../../App_Themes/admin_style/images/descargar.png" Height="20px" AlternateText="Descargar" ToolTip="Descargar" OnPreRender="ImgAdd_PreRender" />

                                        <asp:ImageButton ID="gBorrar" Visible="true" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idArchivo") + ";" + DataBinder.Eval(Container.DataItem, "index") %>'
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
                        </asp:Panel>

                        </ContentTemplate>
                        </asp:UpdatePanel>

                        </fieldset>
