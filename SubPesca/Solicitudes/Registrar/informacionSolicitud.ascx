<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="informacionSolicitud.ascx.cs" Inherits="SubPesca.Solicitudes.Registrar.informacionSolicitud" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>



<script type="text/jscript">
    function alertaMensajeIngresoDoc() {
        confirm("No se debe ingresar documentos cuando una solicitud se encuentre en estado supeditado o pendiente o en la condición de suspendida.");
    }

</script>


<asp:UpdatePanel ID="UpdatePanelInformacionSol" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="PanelInformacionSolicitud"  Visible="false" runat="server">
            <table width="100%" border="0">

                <asp:Panel ID="NPertPanel" Visible="true" runat="server">
                    <tr>
                        <td align="right"><span id="titulo_modulo">Nº PERT: <asp:Label ID="NumeroPert" runat="server"></asp:Label></span></td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="NumIdentificadorPanel" Visible="false" runat="server">
                    <tr>
                        <td align="right"><span id="titulo_modulo">Número Identificador Solicitud: <asp:Label ID="NumIdentificador" runat="server"></asp:Label></span></td>
                    </tr>
                </asp:Panel>

                <tr>
                    <td>
                        <table width="100%">
                        <tr>
                            <td align="right" nowrap="nowrap" width="77%"><div align="right"><asp:Label ID="DefinicionEstado" runat="server"></asp:Label></div></td>
                            <td align="right" nowrap="nowrap" width="11%"><div align="right"><span class="item"><b>Estado Actual:</b></span> <asp:Label ID="EstadoActualSolicitud" runat="server" ></asp:Label></div></td>
                                
                        </tr>
                        <tr>
                            <td align="right" nowrap="nowrap" width="77%"></td>
                            <td align="right" nowrap="nowrap" width="11%"><div align="right"><span class="item"><b>Estado Posterior:</b></span><asp:Label ID="EstadoPosteriorSolicitud" runat="server" ></asp:Label></div></td>
                        </tr>
                        </table>
                    </td>
                </tr>
                
                <asp:Panel ID="PanelInfoRequerimientos" Visible="false" runat="server">
                    <tr>
                        <td align="right"><span class="item"><b>Pendiente: </b></span><asp:Label ID="InfoRequerimientos" runat="server"></asp:Label></td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="PanelEstadoActualFlujoIsla" Visible="false" runat="server">
                    <tr>
                        <td>
                            <table width="100%">
                            <tr>
                                <td align="right" nowrap="nowrap" width="11%"><div align="right"><span class="item"><b>Estado Actual Flujo Isla:</b></span> <asp:Label ID="EstadoActualFlujoIsla" runat="server" ></asp:Label></div></td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="PanelMensajes" Visible="false" runat="server">
                    <tr>
                        <td>
                            <table>
                            <tr>
                                <td align="right" nowrap="nowrap" width="11%"><div align="right"><span class="mensaje"><b>Mensajes:</b><asp:Label ID="MensajesSolicitud" runat="server"></asp:Label></span></div></td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="PanelMensajesVarios" Visible="false" runat="server">
                    <tr>
                        <td>
                            <table>
                            <tr>
                                <td align="right" nowrap="nowrap" width="11%"><div align="right"><span class="mensaje"><b><asp:Label ID="MensajesVarios" runat="server"></asp:Label></b></span></div></td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="PanelGrupoSuspendido" Visible="false" runat="server">
                    <tr>
                        <td>
                            <table>
                            <tr>
                                <td align="right" nowrap="nowrap" width="11%"><div align="right"><span class="mensaje"><b><asp:Label ID="MensajeGrupoSuspendido" runat="server"></asp:Label></b></span></div></td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="PanelCheckSupeditaAvanza" Visible="false" runat="server">
                    <tr>
                        <td>
                            <table>
                            <tr>
                                <td align="right" nowrap="nowrap" width="11%"><div align="right"><span class="mensaje"><b><asp:Label ID="MensajeCheckSupeditaAvanza" runat="server"></asp:Label></b></span></div></td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="PanelCheckSuspendida" Visible="false" runat="server">
                    <tr>
                        <td>
                            <table>
                            <tr>
                                <td align="right" nowrap="nowrap" width="11%"><div align="right"><span class="mensaje"><b><asp:Label ID="MensajeCheckSuspendida" runat="server"></asp:Label></b></span></div></td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="PanelBotonVolverUE" Visible="false" runat="server">
                    <tr>
                        <td align="right">
                            <asp:Button ID="BotonVolverUE" runat="server" Text="Volver a Unidad Espacial" onclick="BotonVolverUE_Click" />
                        </td>
                    </tr>
                </asp:Panel>

            </table>

        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>


