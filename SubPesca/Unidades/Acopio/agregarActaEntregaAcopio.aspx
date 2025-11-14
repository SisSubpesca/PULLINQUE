<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agregarActaEntregaAcopio.aspx.cs" Theme="admin_style" Inherits="SubPesca.Unidades.Acopio.agregarActaEntregaAcopio" %>


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
        .style2
        {
            width: 153px;
        }
    </style>
</head>
<body>
    <form id="dialog_actaEntregaAcopio" runat="server">
      
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>
      
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
             <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                 
        <table class="form" cellpadding="0px" cellspacing="0px" >
        <tr>
            <td class="style2"><span class="item">Nº Acta Entrega</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="style1">
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="NumeroActaEntrega" MaxLength="10" Width="100px"  runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidatorPert" runat="server" ControlToValidate="NumeroActaEntrega"  ValidationGroup="grupo1"
                         ErrorMessage="Nº Acta Entrega" Display="Static" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" Type="Integer" 
                        MinimumValue="1" MaximumValue="1999999999" ControlToValidate="NumeroActaEntrega" 
                        ErrorMessage="Nº Acta Entrega rango no permitido" ValidationGroup="grupo1" ForeColor="Red" />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="DataTypeCheck" Type="Integer" 
                        ControlToValidate="NumeroActaEntrega" ErrorMessage="Nº Acta Entrega Ingrese valores numéricos" ForeColor="Red" ValidationGroup="grupo1" />
                </ContentTemplate>
                
                </asp:UpdatePanel>
            </td>
        </tr>
        
         
         <tr>
         <td class="style2"><span class="item">Fecha Acta Entrega</span></td><td class="col2"><span class="item">:</span></td>
             <td class="style1">
            
            
            <asp:UpdatePanel ID="UpdatePanelFechaDesde" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <div class="calendario">
                    <div class="calendario_textbox">               
                        <asp:TextBox ID="FechaRecepcion" Columns="8" Width="120px" runat="server"></asp:TextBox>
                   
                    </div>

                    <asp:Panel ID="PanelCalendario" Visible="true" runat="server">
                        <div class="calendario_icono">
                            <img  src="../../App_Themes/admin_style/images/calendar.png" id="imgFechaRecepcion" alt="Calendario" style="vertical-align: middle" />
                        </div>
                    </asp:Panel>


                    <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="FechaRecepcion"  ValidationGroup="grupo1"
                        ErrorMessage="Fecha Acta de Entrega" Display="Static">*</asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator2" 
                        runat="server"
                        ControlToValidate="FechaRecepcion"
                        ForeColor="Red"
                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d\s(([0-1][0-9])|([0-2][0-3])):[0-5][0-9]$" 
                        ErrorMessage="Ingrese formato válido"
                        ValidationGroup="grupo1">
                        </asp:RegularExpressionValidator>
                          
                     </div></ContentTemplate>
                     </asp:UpdatePanel>
            
            
            </td></tr><tr>
            <td class="style2"></td>
            <td class="col2"></td>
            <td class="style1">
                <asp:Button ID="Agregar" runat="server" Text="Agregar" CausesValidation="true" 
                    ValidationGroup="grupo1" onclick="Agregar_Click" />
            </td>
        </tr>
        </table>                                                      
          
                       
      </ContentTemplate> 
      </asp:UpdatePanel>

      <asp:HiddenField ID="IdSolConcesion" runat="server" />
      
    <!-- JAVASCRIPT !-->
    <script type="text/javascript">
        invoca_calendarios("agregarActaEntrega");
    </script>

    </form>
</body>
</html>