<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesModificacionECMPO.Master" AutoEventWireup="true" 
CodeBehind="verDocumentoModificacionECMPO.aspx.cs" Inherits="SubPesca.Solicitudes.ModificacionECMPO.verDocumentoModificacionECMPO" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="~/Solicitudes/Registrar/verDocumentoComponente.ascx"            tagname="verDocumentoComponente"                      tagprefix="uc01" %>




<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

        
            
                <asp:Panel ID="PanelVerDocumento"  Visible="true" runat="server">
                    <uc01:verDocumentoComponente ID="verDocumentoComponente" runat="server" />
                </asp:Panel>
   

    
</asp:Content>

