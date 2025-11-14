<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesAcopio.Master"  AutoEventWireup="true" CodeBehind="datosCentroAcopio.aspx.cs" 
Inherits="SubPesca.Solicitudes.Acopio.datosCentroAcopio" Theme="admin_style"  %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="../Registrar/informacionSolicitud.ascx"                   tagname="informacionSolicitud"                 tagprefix="uc1" %>

<asp:Content ID="FormularioUnidadesEspeciales" ContentPlaceHolderID="rightbody" runat="server">

    <asp:HiddenField ID="IdSolicitud" runat="server"></asp:HiddenField>

    <asp:ToolkitScriptManager ID="ToolkitScriptManagerUnidadesEspeciales" runat="server"></asp:ToolkitScriptManager>

    <asp:UpdatePanel ID="UpdatePanelInformacionSolicitud" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 
                <asp:Panel ID="PanelInformacionSolicitud"  Visible="true" runat="server">
                    <uc1:informacionSolicitud ID="informacionSolicitud" runat="server" />
                </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Solicitud Centro de Acopio</span>
            </td>
        </tr>
    </table>

    <hr style="width:100%;" />

    <!-- Datos de la Concesión -->
    <fieldset>
        <legend>Solicitud Centro de Acopio</legend>

        <asp:HiddenField ID="IdUnidadEspacial" runat="server" Value="0"></asp:HiddenField>

        <!-- Crear Concesión -->
        
        <asp:ValidationSummary ID="valSum" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />

        <asp:UpdatePanel ID="UpdatePanelMensajesSuperior" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="Content_msgGrillaGral_1" CssClass="Content_msgGrilla" Visible="false" runat="server">
                    <div class="msgGrilla_div1">
                        <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
                    </div>
                    <div class="msgGrilla_div2">
                        <asp:Label ID="msgGrillaGral_1" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <br /> 
                <table class="form" cellpadding="0px" cellspacing="0px">
                
                    <asp:UpdatePanel ID="UpdatePanelNumPert" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PanelNumPert" runat="server" Visible="true">
                        
                            <tr>
                               <td class="col1"><span class="item">Nº PERT</span></td>
                               <td class="col2"><span class="item">:</span></td>
                               <td class="col3"><asp:TextBox ID="NumPert" MaxLength="15" Width="80px" runat="server" CausesValidation="false" ReadOnly="true" CssClass="campoDeshabilitado"></asp:TextBox></td>
                            </tr>

                        </asp:Panel>
                    </ContentTemplate>
                    </asp:UpdatePanel>


                    
                    <asp:UpdatePanel ID="UpdatePanelFechaRecepcion" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PanelFechaRecepcion" runat="server" Visible="true">
                        
                            <tr>
                               <td class="col1"><span class="item">Fecha de Recepción</span></td>
                               <td class="col2"><span class="item">:</span></td>
                               <td class="col3"><asp:TextBox ID="FechaRecepcion" MaxLength="15" Width="160px" runat="server" CausesValidation="false" ReadOnly="true" CssClass="campoDeshabilitado"></asp:TextBox></td>
                            </tr>

                        </asp:Panel>
                    </ContentTemplate>
                    </asp:UpdatePanel>



                    <asp:UpdatePanel ID="UpdatePanelFechaIngresoTramite" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PanelFechaIngresoTramite" runat="server" Visible="true">
                        
                            <tr>
                               <td class="col1"><span class="item">Fecha de Ingreso a Trámite</span></td>
                               <td class="col2"><span class="item">:</span></td>
                               <td class="col3"><asp:TextBox ID="FechaIngresoTramite" MaxLength="15" Width="160px" runat="server" CausesValidation="false" ReadOnly="true" CssClass="campoDeshabilitado"></asp:TextBox></td>
                            </tr>

                        </asp:Panel>
                    </ContentTemplate>
                    </asp:UpdatePanel>



                    <asp:UpdatePanel ID="UpdatePanelTipoCentroAcopio" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PanelTipoCentroAcopio" runat="server" Visible="true">
                        
                            <tr>
                               <td class="col1"><span class="item">Tipo Centro de Acopio</span></td>
                               <td class="col2"><span class="item">:</span></td>
                               <td class="col3"><asp:DropDownList ID="TipoCentroDeAcopio" runat="server"></asp:DropDownList></td>
                            </tr>

                        </asp:Panel>
                    </ContentTemplate>
                    </asp:UpdatePanel>


                    
                <asp:Panel id="PanelBotonGuardar" runat="server" Visible="true">
                    
                    <tr>
                        <td class="col1"><span class="item"></span></td>
                        <td class="col2"><span class="item"></span></td>
                        <td class="col3" align="left">
                            
                             <asp:ImageButton ID="GuardarDatos" runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                   AlternateText="Guardar Datos" ToolTip="Guardar Datos" onclick="GuardarDatos_Click" 
                                   CausesValidation="true" ValidationGroup="grupo1" />
                            <span class="item">Guardar Datos</span>
                                </td>
                    </tr>
                    
                </asp:Panel>

            
                </table>


                <br />
                <br />


        
    </fieldset>
  

</asp:Content>
