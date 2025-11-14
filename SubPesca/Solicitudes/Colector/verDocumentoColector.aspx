<%@ Page Language="C#"  MasterPageFile="~/Solicitudes/SitioSolicitudesColector.Master" AutoEventWireup="true" 
CodeBehind="verDocumentoColector.aspx.cs"  Inherits="SubPesca.Solicitudes.Colector.verDocumentoColector"  Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="../Registrar/verDocumentoComponente.ascx"            tagname="verDocumentoComponente"                      tagprefix="uc01" %>




<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>


        
            
                <asp:Panel ID="PanelVerDocumento"  Visible="true" runat="server">
                    <uc01:verDocumentoComponente ID="verDocumentoComponente" runat="server" />
                </asp:Panel>
   

    
</asp:Content>