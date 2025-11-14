<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="antecedentesComplementariosObservacion.ascx.cs" Inherits="SubPesca.Solicitudes.Registrar.antecedentesComplementariosObservacion" %>


<fieldset>


        <table class="form" cellpadding="0px" cellspacing="0px" style="display:none;">
        <tr>
            <td>&nbsp;IdSolicitud: <asp:TextBox ID="IdSolicitud" runat="server"></asp:TextBox></td>
        </tr>
        </table>
        
   

        <legend>Observaciones</legend>
        <br />


        <asp:UpdatePanel ID="UpdatePanelMensajeObservaciones" UpdateMode="Conditional" runat="server">
            <ContentTemplate>   
            <asp:Panel ID="PanelMensajeObservaciones" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div2">
                    <asp:Label ID="MensajeObservaciones" runat="server"></asp:Label>
                </div>
            </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelObservaciones" UpdateMode="Conditional" runat="server">
            <ContentTemplate>              

            <asp:HiddenField ID="idObservacion" runat="server"/>

            <table class="form" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1"><span class="item">Observaciones</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3"><asp:TextBox  ID="Observaciones" TextMode="multiline" Columns="50" Rows="5" runat="server" AutoPostBack="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td><p>
                    <asp:Panel ID="PanelAgregarObservaciones"  Visible="true" runat="server">
                        &nbsp;<asp:ImageButton ID="GuardarObservacion" Visible="true" runat="server"   onclick="GuardarObservacion_Click" 
                        ImageUrl="../../App_Themes/admin_style/images/add.png" Height="20px" AlternateText="Guardar Observación" ToolTip="Guardar Observación"/>Guardar Observación
                    </asp:Panel>
                    </p>
                </td>
            </tr>
            </table>
            </ContentTemplate>
        </asp:UpdatePanel>


</fieldset>
