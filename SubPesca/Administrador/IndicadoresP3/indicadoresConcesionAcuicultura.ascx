<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresConcesionAcuicultura.ascx.cs" Inherits="SubPesca.Administrador.IndicadoresP3.indicadoresConcesionAcuicultura" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>


    <asp:HiddenField ID="IdIndicador" runat="server" />
    <asp:HiddenField ID="IdTipoTramite" runat="server" />
    <asp:HiddenField ID="IdSubTipo" runat="server" />


    <asp:UpdatePanel ID="UpdatePanelMensajesValidaciones" UpdateMode="Conditional" runat="server">
       <ContentTemplate>    
           <asp:panel ID="Panel1" runat="server">
                <asp:ValidationSummary ID="ValidationSummaryErrores" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList"  ValidationGroup="ValidationSummaryIndicador" />
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


    <asp:UpdatePanel ID="UpdatePanelFormulario" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelFormulario" Visible="true" runat="server">


        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="numeroIndicador" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="nombreIndicador" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Fórmula</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><asp:Image ID="Formula" runat="server" /></td>
        </tr>          
        <tr>
            <td class="col1"><span class="item">Consideraciones</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="consideraciones" runat="server" Text=""></asp:Label></span></td>
        </tr>          

        <tr id="trNumeradorDesde" runat="server" visible="false">
            <td class="col1"><span class="item">Fecha Numerador desde</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <div class="calendario">
                    <div class="calendario_textbox">                    
                        <asp:TextBox ID="FechaNumeradorDesde" Columns="8" Width="80px" runat="server" ValidationGroup="ValidationSummaryIndicador"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="FechaNumeradorDesde"
                                        Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                                        CultureDateFormat="DMY" CultureDatePlaceholder="/">
                        </asp:MaskedEditExtender>
                    </div>
                    <div class="calendario_icono">
                        <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaNumeradorDesde" alt="Calendario" style="vertical-align: middle" runat="server" />
                    </div>

                    <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidatorFecha" 
                    runat="server"
                    ControlToValidate="FechaNumeradorDesde"
                    ForeColor="Red"
                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" 
                    ErrorMessage="Ingrese formato válido"
                    ValidationGroup="grupo1">
                    </asp:RegularExpressionValidator>

                </div>
            </td>
        </tr>

        <tr id="trNumeradorHasta" runat="server" visible="false">
            <td class="col1"><span class="item">Fecha Numerador hasta</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <div class="calendario">
                    <div class="calendario_textbox">
                        <asp:TextBox ID="FechaNumeradorHasta" Columns="8" Width="80px" runat="server" ValidationGroup="ValidationSummaryIndicador"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender8" runat="server" TargetControlID="FechaNumeradorHasta"
                            Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                            CultureDateFormat="DMY" CultureDatePlaceholder="/">
                        </asp:MaskedEditExtender>
                    </div>
                    <div class="calendario_icono">
                        <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaNumeradorHasta" alt="Calendario" style="vertical-align: middle" runat="server" />
                    </div>

                    <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator1" 
                    runat="server"
                    ControlToValidate="FechaNumeradorHasta"
                    ForeColor="Red"
                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" 
                    ErrorMessage="Ingrese formato válido"
                    ValidationGroup="grupo1">
                    </asp:RegularExpressionValidator>

                </div>
            </td>
        </tr>

        <tr id="trDenominadorDesde" runat="server" visible="false">
            <td class="col1"><span class="item">Fecha Denominador desde</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <div class="calendario">
                    <div class="calendario_textbox">                    
                        <asp:TextBox ID="FechaDenominadorDesde" Columns="8" Width="80px" runat="server" ValidationGroup="ValidationSummaryIndicador"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="FechaDenominadorDesde"
                            Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                            CultureDateFormat="DMY" CultureDatePlaceholder="/">
                        </asp:MaskedEditExtender>
                    </div>
                    <div class="calendario_icono">
                        <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaDenominadorDesde" alt="Calendario" style="vertical-align: middle" runat="server" />
                    </div>

                    <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator2" 
                    runat="server"
                    ControlToValidate="FechaDenominadorDesde"
                    ForeColor="Red"
                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" 
                    ErrorMessage="Ingrese formato válido"
                    ValidationGroup="grupo1">
                    </asp:RegularExpressionValidator>

                </div>
            </td>
        </tr>
        
        <tr id="trDenominadorHasta" runat="server" visible="false">
            <td class="col1"><span class="item">Fecha Denominador hasta</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <div class="calendario">
                    <div class="calendario_textbox">
                        <asp:TextBox ID="FechaDenominadorHasta" Columns="8" Width="80px" runat="server" ValidationGroup="ValidationSummaryIndicador"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="FechaDenominadorHasta"
                            Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                            CultureDateFormat="DMY" CultureDatePlaceholder="/">
                        </asp:MaskedEditExtender>
                    </div>
                    <div class="calendario_icono">
                        <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaDenominadorHasta" alt="Calendario" style="vertical-align: middle" runat="server" />
                    </div>

                    <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator3" 
                    runat="server"
                    ControlToValidate="FechaDenominadorHasta"
                    ForeColor="Red"
                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" 
                    ErrorMessage="Ingrese formato válido"
                    ValidationGroup="grupo1">
                    </asp:RegularExpressionValidator>

                </div>
            </td>
        </tr>


        <tr id="trFechaConsulta" runat="server" visible="false">
            <td class="col1"><span class="item">Fecha Consulta</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <div class="calendario">
                    <div class="calendario_textbox">
                        <asp:TextBox ID="FechaConsulta" Columns="8" Width="80px" runat="server" ValidationGroup="ValidationSummaryIndicador"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="FechaConsulta"
                            Mask="99/99/9999" MaskType="Date" CultureName="es-CL" CultureAMPMPlaceholder=""
                            CultureDateFormat="DMY" CultureDatePlaceholder="/">
                        </asp:MaskedEditExtender>
                    </div>
                    <div class="calendario_icono">
                        <img src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaConsulta" alt="Calendario" style="vertical-align: middle" runat="server" />
                    </div>

                    <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator4" 
                    runat="server"
                    ControlToValidate="FechaConsulta"
                    ForeColor="Red"
                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" 
                    ErrorMessage="Ingrese formato válido"
                    ValidationGroup="grupo1">
                    </asp:RegularExpressionValidator>

                </div>
            </td>
        </tr>

        <tr id="trRegiones" runat="server" visible="false">
            <td class="col1"><span class="item">Regiones(s)</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel13" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:ListBox ID="Regiones" SelectionMode="Multiple" Width="300px" runat="server" ValidationGroup="ValidationSummaryIndicador"></asp:ListBox>
                    <br />
                    <span class="item">máximo de selección: 5 regiones</span>
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        
        <tr>
            <td colspan="2"></td>
            <td class="col3">
                    <asp:Button ID="Limpiar" runat="server" Text="Limpiar"  CausesValidation="true" onclick="Limpiar_Click" style="height: 26px"  />
                    &nbsp;
                    <asp:Button ID="Calcular" runat="server" Text="Calcular"  CausesValidation="true" onclick="Calcular_Click" style="height: 26px"  ValidationGroup="ValidationSummaryIndicador" /></td>
        </tr>
        </table>


        <table id="indicador_detalle" class="indicadores_detalle" cellpadding="0" cellspacing="0" visible="false" runat="server">
        <tr id="fila_numerador" visible="true" runat="server">
            <td class="col1"><span class="item">Numerador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Numerador" runat="server" Text="0"></asp:Label></span></td>
        </tr>
        <tr id="fila_denominador" visible="true" runat="server">
            <td class="col1"><span class="item">Denominador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Denominador" runat="server" Text="0"></asp:Label></span></td>
        </tr>
        <tr id="Tr1" visible="true" runat="server">
            <td class="col1"><span class="item"><b>Resultado</b></span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><b><asp:Label ID="Resultado" runat="server" Text="0"></asp:Label></span></b></td>
        </tr>
        </table>


        <asp:UpdatePanel ID="UpdatePanelErroresInferior" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <asp:Panel ID="PanelErroresInferior" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="ErroresInferior" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>

    

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
        
    <br />
    
    <asp:UpdatePanel ID="UpdatePanelGrillaResultado" UpdateMode="Conditional" runat="server">
    <ContentTemplate>   

    <asp:Panel ID="GrillaResultado" runat="server" Visible="false">

        <fieldset>
            <legend>Solicitudes relacionadas al Indicador</legend>
            <asp:UpdatePanel ID="upd1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   

                <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
                    <div class="msgGrilla_div1">
                        <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
                    </div>
                    <div class="msgGrilla_div2">
                        <asp:Label ID="msgGrilla" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

                <asp:GridView ID="GridView1" runat="server" 
                    AutoGenerateColumns="true" 
                    CellPadding="4" 
                    ForeColor="#333333" 
                    GridLines="None"
                    AllowPaging="True" PageSize="10" 
                    OnPageIndexChanging="GridView1_PageIndexChanged"
                    AllowSorting="true" 
                    OnRowDataBound="GridView1_RowDataBound"
                    CssClass="mGrid"
                    PagerStyle-CssClass="pgr" >
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#5794EF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            
                <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" /> 
            </ContentTemplate> 
            <Triggers>
                <asp:PostBackTrigger ControlID="ExportarGrilla" />
            </Triggers>
            </asp:UpdatePanel>
        </fieldset>
    

    </asp:Panel>


    </ContentTemplate>
    </asp:UpdatePanel>
