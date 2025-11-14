
function aceptar_cerrado(etapa) 
{
    var motivoscierre = document.getElementById("ctl00_rightbody_MotivosCierre");
    var id_solicitud  = document.getElementById("ctl00_rightbody_IdSolicitud").value;
    var id_estado     = motivoscierre.options[motivoscierre.selectedIndex].value;
    
    switch(etapa)
    {
        case 1:
            document.getElementById("msg1").style.visibility = "hidden";
            document.getElementById("msg2").style.visibility = "visible";
            document.getElementById("msg3").style.visibility = "hidden";
            document.getElementById("msg_confirm_cierre").innerHTML = "Está seguro de llevar a la solicitud con id " + id_solicitud + " a un estado especial de cierre?";
            break;
        case 2:
            var success = estadosEspecialesCierre.Solicitud_Cerrar(id_solicitud, id_estado);
            if (success)
            {
                document.getElementById("msg2").style.visibility = "hidden";
                document.getElementById("msg3").style.visibility = "visible";
            };
            break;
    };
};

function aceptar_apertura() 
{
    var id_solicitud  = document.getElementById("ctl00_rightbody_IdSolicitud").value;
    var success = estadosEspecialesCierre.Solicitud_Abrir(id_solicitud);
    if (success)
    {
        document.getElementById("msg4").style.visibility = "hidden";
        document.getElementById("msg5").style.visibility = "visible";
    };

};