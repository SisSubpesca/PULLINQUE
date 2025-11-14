<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="cambiarVigenciaConcesion.aspx.cs" 
Inherits="SubPesca.Unidades.Concesion.cambiarVigenciaConcesion" Theme="admin_style" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
      
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></asp:ToolkitScriptManager>
      <asp:ValidationSummary ID="ValidationSummaryCambiarVigenciaConcesion" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
                       
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
                        
                        

<fieldset>
        <legend>Cambiar Vigencia Unidad Espacial</legend>
        <table class="form" cellpadding="5px" cellspacing="5px">
        <tr>
        <td class="col1"><span class="item">Código de Centro</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:Label ID="CodigoCentro" runat="server"></asp:Label>
        </td>
        
    </tr>
    <tr>
        <td class="col1"><span class="item">Titulares</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:Label ID="Titulares" runat="server"></asp:Label>          
        </td>
    </tr>
    <tr>
        <td class="col1"><span class="item">Vigencia Actual</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:Label ID="VigenciaActual" runat="server"></asp:Label>
        </td>
    </tr>
    </table>
    </fieldset>



<fieldset>
      <asp:UpdatePanel ID="updPanel" UpdateMode="Conditional" runat="server">
      <ContentTemplate>   
                                                                 
        



<table class="form" cellpadding="5px" cellspacing="5px">
        <tr>
        <td class="col1"><span class="item">Tipo Documento</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
<asp:DropDownList ID="TipoDocumento" runat="server" OnSelectedIndexChanged="TipoDocumento_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

<asp:RequiredFieldValidator id="RequiredFieldValidatorTipoDocumento" runat="server" ControlToValidate="TipoDocumento" ValidationGroup="grupo1" ErrorMessage="TipoDocumento" Display="Static">*</asp:RequiredFieldValidator>    

        </td>
        
    </tr>
</table>











<asp:UpdatePanel ID="UpdatePanelResol" UpdateMode="Conditional" runat="server">
<ContentTemplate>
<asp:Panel ID="PanelResol"  Visible="false" runat="server">

<table class="form" cellpadding="5px" cellspacing="5px">
        <tr>
        <td class="col1"><span class="item">Resolución</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
<asp:DropDownList ID="Resolucion" runat="server" CausesValidation="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorResolucion" 
                        runat="server" ControlToValidate="Resolucion" Display="Static" 
                        ErrorMessage="Resolucion" ForeColor="Red" 
                        ValidationGroup="grupo1">* Debe ingresar la resolución a través del Adm. de Resoluciones.</asp:RequiredFieldValidator>


        </td>
        
    </tr>
</table>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelOficio" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
        <asp:Panel ID="PanelOficio"  Visible="false" runat="server">
<table class="form" cellpadding="5px" cellspacing="5px">
        <tr>
        <td class="col1"><span class="item">Oficio</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
<asp:DropDownList ID="Oficio" runat="server" CausesValidation="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorOficio" 
                        runat="server" ControlToValidate="Oficio" Display="Static" 
                        ErrorMessage="Oficio" ForeColor="Red" 
                        ValidationGroup="grupo1">* Debe ingresar el Oficio a través del Adminimistrador de Documentos de UE.</asp:RequiredFieldValidator>



        </td>
        
    </tr>
</table>
</asp:Panel>
 </ContentTemplate>
        </asp:UpdatePanel>



<table class="form" cellpadding="5px" cellspacing="5px">
        <tr>
        <td class="col1"><span class="item">Observaciones</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
<asp:TextBox ID="Observaciones" TextMode="multiline" Columns="50" Rows="5" MaxLength="2000" runat="server" CausesValidation="true"></asp:TextBox>
                 <asp:RequiredFieldValidator id="RequiredFieldValidatorobservaciones" runat="server" ControlToValidate="observaciones"  ValidationGroup="grupo1"
                         ErrorMessage="Observaciones" Display="Static" ForeColor="Red">*</asp:RequiredFieldValidator>

        </td>
        
    </tr>
</table>


<table class="form" cellpadding="5px" cellspacing="5px">
        <tr>
        <td class="col1"></td>
        <td class="col2"></td>
        <td class="col3">
            <asp:Button ID="CambiarVigencia" runat="server" Text="Cambiar Vigencia" CausesValidation="true" 
                    ValidationGroup="grupo1" onclick="Agregar_Click"  />
                    <asp:Button ID="Volver" runat="server" Text="Volver" onclick="Cancelar_Click"  />
        </td>
        
    </tr>
</table>








                                                     
          
                       
      </ContentTemplate> 
      </asp:UpdatePanel>

      <asp:HiddenField ID="IdSolConcesion" runat="server" />
      <asp:HiddenField ID="EstadoSolicitud" runat="server" />
          </fieldset>
      </asp:Content>
