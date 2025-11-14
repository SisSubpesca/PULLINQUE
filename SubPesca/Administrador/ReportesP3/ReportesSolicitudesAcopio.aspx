<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="ReportesSolicitudesAcopio.aspx.cs" 
Inherits="SubPesca.Administrador.ReportesP3.ReportesSolicitudesAcopio" Theme="admin_style" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="400"></asp:ScriptManager>
    
    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Reportes Solicitudes Centro de Acopio</span>
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />
    <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" runat="server">
    <ContentTemplate> 
        <asp:Panel ID="Content_Errores" runat="server" CssClass="valSum" Visible="false"></asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Filtrar" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="TiposReportes" EventName="SelectedIndexChanged" />
    </Triggers>
    </asp:UpdatePanel>

    <fieldset>
        <legend>Reportes</legend>
        <br />
        <asp:UpdatePanel ID="UpdatePanel0" UpdateMode="Always" runat="server">
        <ContentTemplate>
        <table class="form" cellpadding="0px" cellspacing="0px">


        <tr id="tr_tipoTramite"  runat="server">
            <td class="col1"><span class="item">Tipo de Trámite</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="TipoTramite" AutoPostBack="true" OnSelectedIndexChanged="TiposTramite_OnSelectedIndexChanged" runat="server"></asp:DropDownList> *
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>


        <tr id="tr_tipoModificacion" runat="server">
            <td class="col1"><span class="item">Tipo de Modificación</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel14" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="TipoModificacion" AutoPostBack="true" OnSelectedIndexChanged="TipoModificacion_OnSelectedIndexChanged" runat="server"></asp:DropDownList> 
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>


        <tr id="tr_tipoRelocalizacion" runat="server">
            <td class="col1"><span class="item">Tipo de Relocalización</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel15" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="TipoRelocalizacion" AutoPostBack="true" OnSelectedIndexChanged="TipoRelocalizacion_OnSelectedIndexChanged" runat="server"></asp:DropDownList> 
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>

        
        <tr id="tr_tipoReporte" runat="server">
            <td class="col1"><span class="item">Tipo de Reporte</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="TiposReportes" AutoPostBack="true" OnSelectedIndexChanged="TiposReportes_OnSelectedIndexChanged" runat="server"></asp:DropDownList> *
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr id="tr_region" runat="server">
            <td class="col1"><span class="item">Región</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Regiones" AutoPostBack="true" OnSelectedIndexChanged="Regiones_OnSelectedIndexChanged" runat="server"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr id="tr_provincia" runat="server">
            <td class="col1"><span class="item">Provincia</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Provincias" AutoPostBack="true" OnSelectedIndexChanged="Provincias_OnSelectedIndexChanged" runat="server"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Regiones" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr id="tr_comuna" runat="server">
            <td class="col1"><span class="item">Comuna</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Comunas" runat="server"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Regiones" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Provincias" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        
        <tr id="tr_macrozona" runat="server">
            <td class="col1"><span class="item">Macrozona</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Macrozonas" AutoPostBack="true" OnSelectedIndexChanged="Macrozonas_OnSelectedIndexChanged" runat="server"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Regiones" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr id="tr_barrio" runat="server">
            <td class="col1"><span class="item">Barrio</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Barrios" runat="server"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Regiones" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Macrozonas" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
       
        <tr id="tr_codigocentro" runat="server">
            <td class="col1"><span class="item">Código de Centro de Cultivo</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel11" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="CodigoCentro" MaxLength="15" runat="server"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Filtrar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        
            
        <tr id="tr_pert" runat="server">
            <td class="col1"><span class="item">Pert</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="Pert" MaxLength="15" runat="server"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Filtrar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>  
        
 
        <tr id="tr_especies" runat="server">
            <td class="col1"><span class="item">Especie(s)</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel13" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:ListBox ID="Especies" SelectionMode="Multiple" Width="300px" runat="server"></asp:ListBox>
                    <br />
                    <span class="item">máximo de selección: 5 especies</span>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>

         <tr id="tr_grupoEspecie" runat="server">
            <td class="col1"><span class="item">Grupo Especie:</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="GrupoEspecie" runat="server"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>



        <tr id="tr_estado" runat="server">
            <td class="col1"><span class="item">Estado:</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3">
                <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="Estado" runat="server"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr>
            <td class="col1"></td>
            <td class="col2"></td>
            <td class="col3">
                <asp:Button ID="Limpiar" runat="server" OnClick="Limpiar_Click" Text="Limpiar" CausesValidation="false" />
                <asp:Button ID="Filtrar" runat="server" OnClick="Filtrar_Click" Text="Filtrar" OnClientClick="javascript:muestra_loading('cargando');" />
            </td>
        </tr>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>

    <div id="cargando" class="message">
        <div class="background"></div>
        <div style="width:100%; text-align:center; margin-top:350px;">
            <asp:Image ID="Image1" ImageUrl="~/App_Themes/admin_style/images/loading.gif" Width="100px" runat="server" />
        </div>
    </div>

    <asp:UpdatePanel ID="upd1" UpdateMode="Conditional" runat="server">
    <ContentTemplate>   

        <fieldset id="Content_Resultado" visible="false" runat="server">
            <legend>Resultado</legend>

            <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
                <div class="msgGrilla_div1">
                    <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
                </div>
                <div class="msgGrilla_div2">
                    <asp:Label ID="msgGrilla" runat="server"></asp:Label>
                </div>
            </asp:Panel>                          
                                                    
            <asp:Panel ID="Content_Grilla" CssClass="Content_Grilla" runat="server">
                <asp:GridView ID="GridView0" 
                    runat="server" 
                    AutoGenerateColumns="True" 
                    CellPadding="4" 
                    ForeColor="#333333" 
                    GridLines="None"
                    AllowPaging="True" 
                    PageSize="12" 
                    OnPageIndexChanging="GridView_PageIndexChanged"
                    OnRowDataBound="GridView_RowDataBound" 
                    ShowFooter="False" 
                    CssClass="mGrid"
                    PagerStyle-CssClass="pgr">
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#004080" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="#446699" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#5794EF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                
              
                <asp:Button ID="ExportarGrilla" runat="server" Text="Exportar a Excel" CssClass="exportar_grilla" OnClick="ExportarGrilla_Click" />  

            </asp:Panel>

            
        </fieldset>         
    </ContentTemplate> 
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Filtrar" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="Limpiar" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="TiposReportes" EventName="SelectedIndexChanged" />
        <asp:PostBackTrigger ControlID="ExportarGrilla" />
    </Triggers>
    </asp:UpdatePanel>
   
    <asp:HiddenField ID="IdTipoReporte" runat="server" />

</asp:Content>
