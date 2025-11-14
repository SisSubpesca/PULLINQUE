<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="footer.ascx.cs" Inherits="SubPesca.Administrador.includes.footer" %>

<div id="footer">
    <table id="footer" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"></td>
        <td class="col2">
            <asp:Label ID="direccion1" CssClass="direccion" runat="server" Text="Oficina Central: Bellavista 168, piso 16 - Fono: (56-32) 250 2700 Valparaíso, Chile "></asp:Label>
        </td>
        <td class="col3"></td>
    </tr>
    <tr>
        <td class="col1"></td>
        <td class="col2">
            <asp:Label ID="direccion2" CssClass="direccion" runat="server" Text="Oficina Coordinación: Teatinos 120 piso 9 - Fono (56-2)695 36 07 Santiago, Chile"></asp:Label>
        </td>
        <td class="col3"></td>
    </tr>
    </table>    
    <div id="act">
        <asp:Label ID="act" CssClass="act" runat="server" Text="Sistema desarrollado por ACT S.A."></asp:Label>
    </div>
</div>