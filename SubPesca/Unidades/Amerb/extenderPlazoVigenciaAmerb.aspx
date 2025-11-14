<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="extenderPlazoVigenciaAmerb.aspx.cs" Inherits="SubPesca.Unidades.Amerb.extenderPlazoVigenciaAmerb" Theme="admin_style" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            width: 795px;
        }
        </style>
</head>

<body>
    <form id="dialog_extenderPlazoVigenciaConcesion" runat="server">
      
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>
      <asp:ValidationSummary ID="ValidationSummaryExtenderPlazoVigencia" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
                        
                        <asp:UpdatePanel ID="UpdatePanelMsg" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                        <asp:Panel ID="PanelSolicitudesMsg"  Visible="false" runat="server">
                        <div class="msgGrilla_div1">
                            <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
                        </div>
                        <div class="msgGrilla_Solicitud">
                            <asp:Label ID="msgGrilla_Sol" runat="server">
                            </asp:Label>
                        </div>
                        </asp:Panel>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        
                        

      <asp:UpdatePanel ID="updPanel" UpdateMode="Conditional" runat="server">
      <ContentTemplate>   
                                                                 
        <table class="form" cellpadding="0px" cellspacing="0px" width="100%">
        <tr>
            <td class="style3" colspan="3">&nbsp;</td>
        </tr>
        
            <tr>
                <td class="col1"><span class="item">Resolución</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    
                            <asp:DropDownList ID="Resolucion" runat="server" CausesValidation="false">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorResolucion" 
                                runat="server" ControlToValidate="Resolucion" Display="Static" 
                                ErrorMessage="Resolución" ForeColor="Red" ValidationGroup="grupo1" InitialValue="0">*</asp:RequiredFieldValidator>
                </td>
            </tr>
       
            <tr>
                <td class="col1"><span class="item">Plazo Nominal</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                       
                    <asp:DropDownList ID="PlazoNominal"  AutoPostBack="true" runat="server" CausesValidation="false" OnSelectedIndexChanged="PlazoNominal_change" ValidationGroup="grupo1"></asp:DropDownList>                  
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorPlazoNominal" runat="server" ControlToValidate="PlazoNominal" ForeColor="Red" ValidationGroup="grupo1" ErrorMessage="Plazo Nominal" Display="Static" InitialValue="-1">*</asp:RequiredFieldValidator>

                </td>
            </tr>
                

                
            <tr>
                <td class="col1"><span class="item">Plazo de Inicio</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                       
                <asp:UpdatePanel ID="UpdatePanelPlazoInicio" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="PlazoInicio" Columns="8" Width="120px" runat="server" CausesValidation="false"></asp:TextBox>
                   
                    </div>

                    <asp:Panel ID="PanelPlazoInicio" Visible="true" runat="server">
                        <div class="calendario_icono">
                            <img  src="../../App_Themes/admin_style/images/calendar.png" id="imgPlazoInicio" alt="Calendario" style="vertical-align: middle" />
                        </div>
                    </asp:Panel>


                    <asp:RequiredFieldValidator id="RequiredFieldValidatorPlazoInicio" runat="server" ControlToValidate="PlazoInicio"  ValidationGroup="grupo1"
                        ErrorMessage="Plazo de Inicio" Display="Static" ForeColor="Red">*</asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidatorPlazoInicio" 
                        runat="server"
                        ControlToValidate="PlazoInicio"
                        ForeColor="Red"
                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d\s(([0-1][0-9])|([0-2][0-3])):[0-5][0-9]$" 
                        ErrorMessage="Ingrese formato válido"
                        ValidationGroup="grupo1">
                        </asp:RegularExpressionValidator>
                          
                     </div></ContentTemplate>
                     </asp:UpdatePanel>
                    </td>
            </tr>
                

            <asp:UpdatePanel ID="UpdatePanelNumeroPlazo" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            <asp:Panel ID="PanelNumeroPlazo" Visible="true" runat="server">
            <tr>
                <td class="col1"><span class="item">Nº Plazo</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                   
                <asp:TextBox ID="NumeroPlazo" MaxLength="3" Width="80px" runat="server" OnTextChanged="NumeroPlazo_TextChanged" AutoPostBack="true"></asp:TextBox> 
                <asp:CompareValidator ID="CompareValidatorNumeroPlazo" ControlToValidate="NumeroPlazo" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="El Nº Plazo debe ser Numérico." Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
                <asp:RequiredFieldValidator id="RequiredFieldValidatorNumeroPlazo" runat="server" ControlToValidate="NumeroPlazo"  ValidationGroup="grupo1"
                    ErrorMessage="Nº Plazo" Display="Static" ForeColor="Red">*</asp:RequiredFieldValidator>

                </td>
            </tr>
            </asp:Panel>
            </ContentTemplate>
            </asp:UpdatePanel>
               

            <asp:UpdatePanel ID="UpdatePanelPlazoVencimiento" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            <asp:Panel ID="PanelPlazoNominalVencimiento" Visible="true" runat="server">
            <tr>
                <td class="col1"><span class="item">Plazo de Vencimiento</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                       
                <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="PlazoVencimiento" Columns="8" Width="120px" runat="server" CausesValidation="false" ReadOnly="true" CssClass="campoDeshabilitado"></asp:TextBox>
                    </div>

                    <asp:Panel ID="PanelFechaVencimiento" Visible="false" runat="server">
                        <div class="calendario_icono">
                            <img  src="../../App_Themes/admin_style/images/calendar.png" id="imgPlazoVencimiento" alt="Calendario" style="vertical-align: middle" />
                        </div>
                    </asp:Panel>


                     <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="PlazoVencimiento"  ValidationGroup="grupo1"
                        ErrorMessage="Plazo de Vencimiento" Display="Static" ForeColor="Red">*</asp:RequiredFieldValidator>


                        <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator1" 
                        runat="server"
                        ControlToValidate="PlazoVencimiento"
                        ForeColor="Red"
                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d\s(([0-1][0-9])|([0-2][0-3])):[0-5][0-9]$" 
                        ErrorMessage="Ingrese formato válido"
                        ValidationGroup="grupo1">
                        </asp:RegularExpressionValidator>

                </div>
                </td>
            </tr>
            </asp:Panel>
            </ContentTemplate>
            </asp:UpdatePanel>
        
        
            <tr>
                <td class="col1"><span class="item">Observaciones</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
            
                    
                        <asp:TextBox ID="Observaciones" TextMode="multiline" Columns="50" Rows="5" MaxLength="2000" runat="server" CausesValidation="false"></asp:TextBox>


                        <asp:RequiredFieldValidator id="RequiredFieldValidatorobservaciones" runat="server" ControlToValidate="observaciones"  ValidationGroup="grupo1"
                                ErrorMessage="Observaciones" Display="Static" ForeColor="Red">*</asp:RequiredFieldValidator>
                     
            
                </td></tr><tr>
                <td class="style3"></td>
                <td class="col2"></td>
                <td class="style1">
                    <asp:Button ID="ExtenderVigencia" runat="server" Text="Extender Vigencia" CausesValidation="true" 
                        ValidationGroup="grupo1" onclick="ExtenderVigencia_Click" />
                </td>
            </tr>
        </table>                                                      
          
                       
      </ContentTemplate> 
      </asp:UpdatePanel>

      <asp:HiddenField ID="IdSolConcesion" runat="server" />
      
    <script type="text/javascript">
        invoca_calendarios("extensionPlazo");
    </script>

    </form>
</body>
</html>
