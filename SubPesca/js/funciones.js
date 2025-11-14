

function validatePre(grupo) {
    if (Page_ClientValidate(grupo)) {
        muestra_loading('cargando');
        return true;
    }
    return false;
}


function closeAutocomplete() {
    $(".ac_results").hide();
    // $("#ctl00_rightbody_antecedDelSectorComponente_NombreCartaAntecedentesEspaciales").siblings().addBack().addClass("ui-screen-hidden");
    //$(".ui-autocomplete").css({ "display": "none" });
}

/* CONTROL DE EVENTOS ----------------------------------------------------------------------------------------------------------------------------------------------------- */

function Cancelar()
{
    var confirmacion = confirm('Si continúa perderá los cambios no guardados\n¿Está seguro de cancelar?');
    return confirmacion;
};

/* ACCIONES --------------------------------------------------------------------------------------------------------------------------------------------------------------- */

function ocultarObjeto(id, masterpage)
{
    if (masterpage == 1)
    {
        document.getElementById("ctl00_rightbody_" + id).style.display = "none";
    }
    else
    {
        document.getElementById(id).style.display = "none";
    };
};

/* DIÁLOGOS Y MENSAJES EMERGENTES ----------------------------------------------------------------------------------------------------------------------------------------- */

function open_message(id)
{
    $("div#" + id + ".message").show("slow");
}

function close_message(id) {
    $("div#" + id + ".message").hide("slow");
};

function abre_dialogo2(id_div, id_objeto, id_objeto2) {
    var pagina = "";
    switch (id_div) {
        case "verNombres":
            pagina = "verNombres.aspx?rutPersona=" + id_objeto + "&bp=" + id_objeto2;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
    }
}

function abre_dialogo(id_div, id_objeto)
{
    var pagina = "";
    switch(id_div)
    {
        case "verNombres":
            pagina = "verNombres.aspx?rutPersona=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "verContactoRepresentanteLegal":
            pagina = "verContactoRepresentanteLegal.aspx?rutPersona=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "verContactoOperador":
            pagina = "verContactoOperador.aspx?rutPersona=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "agregarComuna":
            pagina = "agregarComuna.aspx";
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "agregarTipoConcesionUnidEspacial":
            pagina = "agregarTipoConcesion.aspx";
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "agregarTipoConcesionAntTerreno":
            pagina = "agregarTipoConcesion.aspx";
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "agregarTipoConcesionRegularizacion":
            pagina = "agregarTipoConcesion.aspx";
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "adminGrupos":
            pagina = "adminGrupos.aspx";
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "adminRoles":
            pagina = "adminRoles.aspx";
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "adminPert":
            var idUsuario = document.getElementById("ctl00_rightbody_Id_Usuario").value;
            pagina = "adminPert.aspx?id_usuario=" + idUsuario;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "alcanceRegional":
            pagina = "adminUsuarioPrivRegionales.aspx?id_usuario=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "historial":
            pagina = "historialSolicitudes.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "historialCambioVigenciaConcecion":
            pagina = "historialCambioVigenciaConcecion.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "historialCambioVigenciaExperimentalConcecion":
            pagina = "historialCambioVigenciaExperimentalesConcesion.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "historialCambioVigenciaAcopio":
            pagina = "historialCambioVigenciaAcopio.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "historialCambioVigenciaAmerb":
            pagina = "historialCambioVigenciaAmerb.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "historialCambioVigenciaExperimentalAmerb":
            pagina = "historialCambioVigenciaExperimentalesAmerb.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "historialCambioVigenciaColector":
            pagina = "historialCambioVigenciaColector.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "historialCambioVigenciaECMPO":
            pagina = "historialCambioVigenciaECMPO.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "historialCambioVigenciaExperimentalesAmerb":
            pagina = "historialCambioVigenciaExperimentalesAmerb.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "historialCambioVigenciaExperimentalesConcesion":
            pagina = "historialCambioVigenciaExperimentalesConcesion.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "historialCambioVigenciaFaenamiento":
            pagina = "historialCambioVigenciaFaenamiento.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "cambiarVigenciaConcesion":
            pagina = "cambiarVigenciaConcesion.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "cambiarVigenciaAcopio":
            pagina = "cambiarVigenciaAcopio.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "cambiarVigenciaColector":
            pagina = "cambiarVigenciaColector.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "cambiarVigenciaAmerb":
            pagina = "cambiarVigenciaAmerb.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "cambiarVigenciaECMPO":
            pagina = "cambiarVigenciaECMPO.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "cambiarVigenciaExperimentalesAmerb":
            pagina = "cambiarVigenciaExperimentalesAmerb.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "cambiarVigenciaExperimentalesConsecion":
            pagina = "cambiarVigenciaExperimentalesConsecion.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "cambiarVigenciaFaenamiento":
            pagina = "cambiarVigenciaFaenamiento.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "extenderPlazoVigenciaConcesion":
            pagina = "extenderPlazoVigenciaConcesion.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "extenderPlazoVigenciaExperimentalConcesion":
            pagina = "extenderVigenciaExperimentalConcesion.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "extenderPlazoVigenciaAcopio":
            pagina = "extenderPlazoVigenciaAcopio.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "extenderPlazoVigenciaAmerb":
            pagina = "extenderPlazoVigenciaAmerb.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "extenderPlazoVigenciaExperimentalAmerb":
            pagina = "extemderPlazoVigenciaExperimentalesAmerb.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "extenderPlazoVigenciaColector":
            pagina = "extenderPlazoVigenciaColector.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "extenderVigenciaFaenamiento":
            pagina = "extenderVigenciaFaenamiento.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "extenderPlazoVigenciaECMPO":
            pagina = "extenderPlazoVigenciaECMPO.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;      
        case "actaEntregaConcesion":
            pagina = "agregarActaEntregaConcesion.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "agregarActaEntregaFaenamiento":
            pagina = "agregarActaEntregaFaenamiento.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "actaEntregaColector":
            pagina = "agregarActaEntregaColector.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "actaEntregaAmerb":
            pagina = "agregarActaEntregaAmerb.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "actaEntregaAcopio":
            pagina = "agregarActaEntregaAcopio.aspx?idSolConcesion=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;
        case "cerradoespecial":
            document.getElementById("msg1").style.visibility = "visible";
            document.getElementById("msg2").style.visibility = "hidden";
            document.getElementById("msg3").style.visibility = "hidden";
            open_message(id_div);
            break;
        case "aperturaespecial":
            var id_solicitud = document.getElementById("ctl00_rightbody_IdSolicitud").value;
            document.getElementById("msg_confirm_apertura").innerHTML = "Está seguro de abrir nuevamente la solicitud y posicionarla en su estado correspondiente?";
            document.getElementById("msg4").style.visibility = "visible";
            document.getElementById("msg5").style.visibility = "hidden";
            open_message(id_div);
            break;

        case "confirmarEliminar":
            document.getElementById("msg1").style.visibility = "visible";
            document.getElementById("msg2").style.visibility = "hidden";
            document.getElementById("msg3").style.visibility = "hidden";
            open_message(id_div);

        case "agregarGrupoSusp":
            pagina = "../../Mantenedores/GrupoSuspendido/agregarGrupoSuspendido.aspx?paginaOrigen=" + id_objeto;
            document.getElementById("iframe_" + id_div).src = pagina;
            open_dialog(id_div);
            break;      
            
              
    };
};

function open_dialog(id)
{
    $("div#" + id + ".dialog").show("slow");
	window.location.hash = "#" + id;
};

function close_dialog(id) {
    $("div#" + id + ".dialog").hide("slow");
};

function muestra_loading(id) {
    muestra_div(id);
};

function muestra_div(id) {
    document.getElementById(id).style.display = "block";
};

function oculta_loading(id) {
    oculta_div(id);
};

function oculta_div(id) {
    document.getElementById(id).style.display = "none";
};


/*  OTROS  */

function validaRut(rutCompleto)
{
    var retorno = false;
    if (/^[0-9]+-[0-9kK]{1}$/.test( rutCompleto )) 
    {
        var tmp    = rutCompleto.split('-');
        var digv   = tmp[1];
        var rut    = tmp[0];
        if ( digv == 'k' ) 
        {
            digv = 'K' ;
        };
        var dv = calculaDV(rut);
        if (dv == digv )
        {
            retorno = true;
        };
    };
    
    return retorno;
};
 
function calculaDV(rut)
{
    var M=0,S=1;
    for(;rut;rut=Math.floor(rut/10)) 
    {
        S=(S+rut%10*(9-M++%6))%11;
    }
    return S?S-1:'K';
};






var dtCh = "/";
var minYear = 1900;
var maxYear = 2100;


function isInteger(s) {
    var i;
    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    return true;
}

function stripCharsInBag(s, bag) {
    var i;
    var returnString = "";
    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary(year) {
    return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
}

function DaysArray(n) {
    for (var i = 1; i <= n; i++) {
        this[i] = 31
        if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30 }
        if (i == 2) { this[i] = 29 }
    }
    return this
}


function validaFechaDDMMAAAA(source, arguments) {

    dtStr = arguments.Value;

    var daysInMonth = DaysArray(12)
    var pos1 = dtStr.indexOf(dtCh)
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
    var strDay = dtStr.substring(0, pos1)
    var strMonth = dtStr.substring(pos1 + 1, pos2)
    var strYear = dtStr.substring(pos2 + 1)
    strYr = strYear
    if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
    if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
    for (var i = 1; i <= 3; i++) {
        if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
    }
    month = parseInt(strMonth)
    day = parseInt(strDay)
    year = parseInt(strYr)

    if (pos1 == -1 || pos2 == -1) {
        arguments.IsValid = false;
    }
    if (strMonth.length < 1 || month < 1 || month > 12) {
        arguments.IsValid = false;
    }
    if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
        arguments.IsValid = false;
    }
    if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
        arguments.IsValid = false;
    }
    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
        arguments.IsValid = false;
    }
    return;
}

/*  ----------------------------------------------------------------------------------------------------------------------------------------- */

function onlyNumeric(obj) {
    var valor = obj.value;
    obj.value = valor.replace(/[^0-9]/g, "");
}
function onlyNumericoComa(obj) {
    var valor = obj.value;
    obj.value = valor.replace(/[^0-9\,]/g, "");

}

/*************************************************************************************************/

function calculaLatitud(pestana) {

    if (pestana == 'AntecedentesEspaciales') {

        var latitudHora = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LatitudHoraAntecedentesEspeciales").value;
        var latitudMinuto = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LatitudMinutoAntecedentesEspeciales").value;
        var latitudSegundo = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LatitudSegundoAntecedentesEspeciales").value;

        var result = parseFloat(latitudHora + (latitudMinuto / 60) + (latitudSegundo / 3600));
        var resultString = result.toString().replace('.',',');

        document.getElementById('ctl00_rightbody_antecedDelSectorComponente_LatitudAntecedentesEspeciales').value = resultString;
        document.getElementById('ctl00_rightbody_antecedDelSectorComponente_LatitudAntecedentesEspecialesReadOnly').value = resultString;
         
    } else if (pestana == 'AntecedentesTerreno') {

        var latitudHora = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LatitudHoraAntecedentesTerreno").value;
        var latitudMinuto = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LatitudMinutoAntecedentesTerreno").value;
        var latitudSegundo = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LatitudSegundoAntecedentesTerreno").value;

        var result = parseFloat(latitudHora + (latitudMinuto / 60) + (latitudSegundo / 3600));
        var resultString = result.toString().replace('.', ',');

        document.getElementById('ctl00_rightbody_antecedDelSectorComponente_LatitudDecimalAntecedentesTerreno').value = resultString;
        document.getElementById('ctl00_rightbody_antecedDelSectorComponente_LatitudDecimalAntecedentesTerrenoReadOnly').value = resultString;
        
    } else if (pestana == 'Regularizacion') {

        var latitudHora = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LatitudHoraRegularizacion").value;
        var latitudMinuto = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LatitudMinutoRegularizacion").value;
        var latitudSegundo = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LatitudSegundoRegularizacion").value;

        var result = parseFloat(latitudHora + (latitudMinuto / 60) + (latitudSegundo / 3600));
        var resultString = result.toString().replace('.', ',');

        document.getElementById('ctl00_rightbody_antecedDelSectorComponente_LatitudDecimalRegularizacion').value = resultString;
        document.getElementById('ctl00_rightbody_antecedDelSectorComponente_LatitudDecimalRegularizacionReadOnly').value = resultString;
    }
}


function calculaLongitud(pestana) {

    if (pestana == 'AntecedentesEspaciales') {

        var longitudHora = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LongitudHoraAntecedentesEspeciales").value;
        var longitudMinuto = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LongitudMinutoAntecedentesEspeciales").value;
        var longitudSegundo = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LongitudSegundoAntecedentesEspeciales").value;

        var result = parseFloat(longitudHora + (longitudMinuto / 60) + (longitudSegundo / 3600));
        var resultString = result.toString().replace('.', ',');

        document.getElementById('ctl00_rightbody_antecedDelSectorComponente_LongitudAntecedentesEspeciales').value = resultString;
        document.getElementById('ctl00_rightbody_antecedDelSectorComponente_LongitudAntecedentesEspecialesReadOnly').value = resultString;

    } else if (pestana == 'AntecedentesTerreno') {

        var longitudHora = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LongitudHoraAntecedentesTerreno").value;
        var longitudMinuto = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LongitudMinutoAntecedentesTerreno").value;
        var longitudSegundo = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LongitudSegundoAntecedentesTerreno").value;

        var result = parseFloat(longitudHora + (longitudMinuto / 60) + (longitudSegundo / 3600));
        var resultString = result.toString().replace('.', ',');

        document.getElementById('ctl00_rightbody_antecedDelSectorComponente_LongitudDecimalAntecedentesTerreno').value = resultString;
        document.getElementById('ctl00_rightbody_antecedDelSectorComponente_LongitudDecimalAntecedentesTerrenoReadOnly').value = resultString;

    } else if (pestana == 'Regularizacion') {

        var longitudHora = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LongitudHoraRegularizacion").value;
        var longitudMinuto = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LongitudMinutoRegularizacion").value;
        var longitudSegundo = document.getElementById("ctl00_rightbody_antecedDelSectorComponente_LongitudSegundoRegularizacion").value;

        var result = parseFloat(longitudHora + (longitudMinuto / 60) + (longitudSegundo / 3600));
        var resultString = result.toString().replace('.', ',');

        document.getElementById('ctl00_rightbody_antecedDelSectorComponente_LongitudDecimalRegularizacion').value = resultString;
        document.getElementById('ctl00_rightbody_antecedDelSectorComponente_LongitudDecimalRegularizacionReadOnly').value = resultString;
    }
}

