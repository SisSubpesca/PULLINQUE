

function validaCorreo(source, arguments)
{
    var id_usuario = document.getElementById("ctl00_rightbody_Id_Usuario").value;
    var respuesta = formUsuario.validaDatosUsuario(id_usuario, "Correo", arguments.Value);
    arguments.IsValid = respuesta.value;
    
    // Mensaje de aviso de que el mail ingresado ya está asignado para otro usuario 
    if (arguments.IsValid)
    {
        document.getElementById("ctl00_rightbody_emailInUsedVal").style.display = "none";
    }
    else
    {
        document.getElementById("ctl00_rightbody_emailInUsedVal").style.display = "inline";
    };
    
    // Mensaje de formato incorrecto    
    if (document.getElementById("ctl00_rightbody_emailFormatoVal").style.visibility == "hidden")
    {
        document.getElementById("ctl00_rightbody_emailFormatoVal").style.display = "none";
    }
    else
    {
        document.getElementById("ctl00_rightbody_emailFormatoVal").style.display = "inline";
    };    
    
    return;
};

function validaRUT(source, arguments)
{
    arguments.IsValid = true;

    if (validaRut(arguments.Value) == true )
    {
        arguments.IsValid = true;
    }
    else
    {
        arguments.IsValid = false;
    };

    return;
};


function prueba(arg) {
    alert(arg);
}

function validaNickUsuario(source, arguments)
{
    var id_usuario = document.getElementById("ctl00_rightbody_Id_Usuario").value;
    var respuesta = formUsuario.validaDatosUsuario(id_usuario, "Usuario", arguments.Value);
    arguments.IsValid = respuesta.value;
    
    return;
};

function validaGrupoUsuario(source, arguments)
{
    if (arguments.Value != "-1" )
    {
        arguments.IsValid = true;
    }
    else
    {
        arguments.IsValid = false;
    };
    
    return;
};

/***********************************************************************/

function despliegaMensajeAlerta(mensajeAlerta) {
    alert(mensajeAlerta);
}


function despliegaMensajeAlerta2(mensajeAlerta,div) {
    alert(mensajeAlerta);
    oculta_loading(div);
}
