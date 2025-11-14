<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="evaluarDocumentoComponente.ascx.cs" Inherits="SubPesca.Solicitudes.Registrar.evaluarDocumentoComponente" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="verDocumentoComponente.ascx"            tagname="verDocumentoComponente"                      tagprefix="uc01" %>



 

        <table class="form" cellpadding="0px" cellspacing="0px" style="display:none;">
        <tr>
            <td>&nbsp;IdSolicitud: <asp:TextBox ID="IdSolicitud" runat="server"></asp:TextBox></td>
            <td>&nbsp;IdRequerimiento: <asp:TextBox ID="IdRequerimiento" runat="server"></asp:TextBox></td>
        </tr>
        </table>


        <asp:UpdatePanel ID="UpdatePanelVerDocumento" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
            <ContentTemplate>                        
                <asp:Panel ID="PanelVerDocumento"  Visible="true" runat="server">
                    <uc01:verDocumentoComponente ID="verDocumentoComponente" runat="server" />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <br />
        
        <table class="form" cellpadding="0px" cellspacing="0px">   
        <tr>
            <td colspan="3">

                 <asp:UpdatePanel ID="UpdatePanelMensajes" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>   
                        <div class="msgGrilla_div2">
                            <asp:Label ID="Mensajes" runat="server"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            
            </td>
        </tr>

        <tr>
            <td colspan="3">&nbsp;</td>
        </tr>

        <tr>
            <td class="col1">
                
                <asp:UpdatePanel ID="UpdatePanelConforme" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelConforme"  Visible="true" runat="server">
                            &nbsp;<asp:Button ID="Conforme"   runat="server" Text="Conforme" CausesValidation="false" onclick="Conforme_Click"  />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            
            </td>
            <td class="col2">
                
               <asp:UpdatePanel ID="UpdatePanelNoConfome" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
                    <ContentTemplate>                        
                        <asp:Panel ID="PanelNoConfome"  Visible="true" runat="server">
                            &nbsp;<asp:Button ID="NoConfome"  runat="server" Text="No Conforme" CausesValidation="false" onclick="NoConfome_Click"  />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            
            </td>
            <td class="col3">
                &nbsp;<asp:Button ID="Cancelar"   runat="server" Text="Volver" CausesValidation="false" onclick="Cancelar_Click"  />
            </td>
        </tr>
        </table>