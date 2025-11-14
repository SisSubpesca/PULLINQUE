<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesAmerb.Master" AutoEventWireup="true" CodeBehind="datosCentroAmerb.aspx.cs" 
Inherits="SubPesca.Solicitudes.Amerb.datosCentroAmerb" Theme="admin_style"  %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="../Registrar/informacionSolicitud.ascx"                   tagname="informacionSolicitud"                      tagprefix="uc1" %>


<asp:Content ID="FormularioUnidadesEspeciales" ContentPlaceHolderID="rightbody" runat="server">




<link href="../../css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
<script src="../../js/jquery/jquery.autocomplete.js" type="text/javascript"></script>

<script type="text/javascript">

    function nuevaFuncion() {
        $("#<%=Amerb.ClientID%>").autocomplete('../Amerb/SearchAmerb_CS.ashx');
    }

</script>

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
                <span id="titulo_modulo">Solicitud Acuicultura Amerb</span>
            </td>
        </tr>
    </table>

    <hr style="width:100%;" />

    <!-- Datos de la Concesión -->
    <fieldset>
        <legend>Solicitud Acuilcultura Amerb</legend>

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
                               <td class="col1"><span class="item">Número Identificador Solicitud</span></td>
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

                    <asp:UpdatePanel ID="UpdateNumeroCI" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PanelNumeroCI" runat="server" Visible="true">

                            <tr>
                                <td class="col1"><span class="item">Número CI</span></td>
                                <td class="col2"><span class="item">:</span></td>
                                <td class="col3"><asp:TextBox ID="NumeroCI" autocomplete="tel-extension" MaxLength="10" Width="100px"  runat="server" CausesValidation="false" ReadOnly="true" CssClass="campoDeshabilitado"></asp:TextBox></td>
                            </tr>
            
                        </asp:Panel>
                    </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="UpdatePanelFechaCI" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PanelFechaCI" runat="server" Visible="true">
                        
                            <tr>
                               <td class="col1"><span class="item">Fecha CI</span></td>
                               <td class="col2"><span class="item">:</span></td>
                               <td class="col3"><asp:TextBox ID="FechaCI" MaxLength="15" Width="160px" runat="server" CausesValidation="false" ReadOnly="true" CssClass="campoDeshabilitado"></asp:TextBox></td>
                            </tr>

                        </asp:Panel>
                    </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="UpdatePanelOficina" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PanelOficina" runat="server" Visible="true">
                        
                            <tr>
                               <td class="col1"><span class="item">Lugar de Ingreso</span></td>
                               <td class="col2"><span class="item">:</span></td>
                               <td class="col3"><asp:TextBox ID="Oficina" MaxLength="15" Width="160px" runat="server" CausesValidation="false" ReadOnly="true" CssClass="campoDeshabilitado"></asp:TextBox></td>
                            </tr>

                        </asp:Panel>
                    </ContentTemplate>
                    </asp:UpdatePanel>


                    <asp:UpdatePanel ID="UpdatePanelSuperficieSectorCultivo" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PanelSuperficieSectorCultivo" runat="server" Visible="true">
                        
                            <tr>
                               <td class="col1"><span class="item">Superficie sector cultivo</span></td>
                               <td class="col2"><span class="item">:</span></td>
                               <td class="col3"><asp:TextBox ID="SuperficieSectorCultivo" MaxLength="15" Width="80px" runat="server" CausesValidation="false"></asp:TextBox></td>
                            </tr>

                        </asp:Panel>
                    </ContentTemplate>
                    </asp:UpdatePanel>


                    <asp:UpdatePanel ID="UpdatePanelPorcentajeSectorCultivo" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PanelPorcentajeSectorCultivo" runat="server" Visible="true">
                        
                            <tr>
                               <td class="col1"><span class="item">% sector cultivo</span></td>
                               <td class="col2"><span class="item">:</span></td>
                               <td class="col3"><asp:TextBox ID="PorcentajeSectorCultivo" MaxLength="15" Width="80px" runat="server" CausesValidation="false"></asp:TextBox></td>
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

<fieldset>

<script type="text/javascript">
    Sys.Application.add_load(nuevaFuncion);
</script>

<legend>Datos Amerb</legend>
<table class="form" cellpadding="0px" cellspacing="0px"> 
<tr>
    <td class="col1"><span class="item">Amerb</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3">

                <asp:TextBox ID="Amerb" runat="server" Width="350px" AutoCompleteType="Disabled" ontextchanged="Amerb_TextChanged"></asp:TextBox>
    </td>
    <td class="col3">
        &nbsp;</td>
    <td class="col3">
        &nbsp;</td>
    <td class="col3">
        &nbsp;</td>
</tr>
<asp:UpdatePanel ID="UpdatePanelAmerb" UpdateMode="Conditional" runat="server">
<ContentTemplate>
<asp:Panel ID="PanelAmerb" runat="server" Visible="false">                 
<tr>
    <td class="col1"><span class="item">COD_SNP</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3"><asp:Label ID="COD_SNP" runat="server"></asp:Label></td>
    <td class="col1"><span class="item">Región</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3"><asp:Label ID="Region" runat="server"></asp:Label></td>
</tr>
<tr>
    <td class="col1"><span class="item">ESTADO</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3"><asp:Label ID="Estado" runat="server"></asp:Label></td>
    <td class="col1"><span class="item">CDU01</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3"><asp:Label ID="CDU01" runat="server"></asp:Label></td>
</tr>
<tr>
    <td class="col1"><span class="item">FCDU01</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3"><asp:Label ID="FCDU01" runat="server"></asp:Label></td>
    <td class="col1"><span class="item">CDU02</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3"><asp:Label ID="CDU02" runat="server"></asp:Label></td>
</tr>
<tr>
    <td class="col1"><span class="item">FCDU02</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3"><asp:Label ID="FCDU02" runat="server"></asp:Label></td>
    <td class="col1"><span class="item">CDU03</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3"><asp:Label ID="CDU03" runat="server"></asp:Label></td>
</tr>
<tr>
    <td class="col1"><span class="item">FCDU03</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3"><asp:Label ID="FCDU03" runat="server" ></asp:Label></td>
    <td class="col1"><span class="item">Superficie_Hectareas</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3"><asp:Label ID="Superficie_Hectareas" runat="server"></asp:Label></td>
</tr>
<tr>
    <td class="col1"><span class="item">UltimoPlazo</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3"><asp:Label ID="UltimoPLazo" runat="server"></asp:Label></td>
    <td class="col1"><span class="item">Informe</span></td>
    <td class="col2"><span class="item">:</span></td>
    <td class="col3"><asp:Label ID="Informe" runat="server"></asp:Label></td>
</tr>
</asp:Panel>

<asp:Panel ID="PanelBotonGuardarAmerb" runat="server" Visible="true"> 
<tr>
    <td class="col1"></td>
    <td class="col2"></td>
    <td class="col3"><asp:ImageButton ID="GuardarDatosAmerb" runat="server" ImageUrl="~/App_Themes/admin_style/images/add.png" Height="20px" 
                                   AlternateText="Guardar Datos" ToolTip="Guardar Datos" onclick="GuardarDatosAmerb_Click" 
                                   CausesValidation="true" ValidationGroup="grupo1" />
                            <span class="item">Guardar Datos</span></td>
    <td class="col1"></td>
    <td class="col2"></td>
    <td class="col3"></td>
</tr>
</asp:Panel>

</ContentTemplate>
</asp:UpdatePanel>
</table>
</fieldset>

<br />


        

  

</asp:Content>
