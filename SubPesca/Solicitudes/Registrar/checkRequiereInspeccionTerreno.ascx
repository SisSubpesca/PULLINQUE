<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="checkRequiereInspeccionTerreno.ascx.cs" Inherits="SubPesca.Solicitudes.Registrar.checkRequiereInspeccionTerreno" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>


<asp:UpdatePanel ID="UpdatePanelCheck" UpdateMode="Conditional" runat="server">
    <ContentTemplate>              

        <asp:Panel ID="PanelCheck" Visible="false" runat="server">

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
                <legend>¿Omitir Inspección en Terreno?</legend>
                <br />


                <table class="form" cellpadding="0px" cellspacing="0px" style="display:none;">
                <tr>
                    <td>&nbsp;IdSolicitud: <asp:TextBox ID="IdSolicitud" runat="server"></asp:TextBox></td>
                </tr>
                </table>


        
                <table class="form" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="col1"><span class="item">Omitir Inspección en Terreno</span></td>
                    <td class="col2"><span class="item">:</span></td>
                    <td class="col3" style="margin-left: 40px">
                        <span class="item">
                            <asp:DropDownList ID="CheckCombo" AutoPostBack="true" runat="server">  
                                <asp:ListItem Value="66">No</asp:ListItem>
                                <asp:ListItem Value="65">Si</asp:ListItem>
                            </asp:DropDownList> 
                        </span>
                    </td>
                </tr>
                <tr>
                    <td class="col1">&nbsp;</td>
                    <td class="col2">&nbsp;</td>
                    <td class="col3" style="margin-left: 40px">
                        <asp:Panel id="Panel1" runat="server"> 
                        <asp:ImageButton ID="ImageButton1" runat="server" 
                            ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                            AlternateText="Guardar" ToolTip="Guardar" 
                            CausesValidation="true" onclick="Check_Guardar_Click" />
                        <span class="item">Guardar</span>
                        </asp:Panel>
                    </td>
                </tr>
                </table>
            </fieldset>

        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
