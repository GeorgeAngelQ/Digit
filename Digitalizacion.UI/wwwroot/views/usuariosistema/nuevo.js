"use strict";
function fnUsuarioSistemaInsert(dtoUsuarioSistema) {
    let strUsuarioSistema = JSON.stringify(dtoUsuarioSistema);
    $.ajax({
        type: 'POST',
        cache: false,
        async: true,
        dataType: "text",
        url: "https://localhost:7179/mantenimiento/usuariosistema/insert",
        data: { usuarioSistema: strUsuarioSistema },
        success: function () {
            alert("Se ingresó con éxito el usuario: " + dtoUsuarioSistema.usuario);
            window.location.href = "/mantenimiento/usuariosistema/index";
        },
        error(param1, param2, param3){
            console.error("param1", param1);
            console.error("param2", param2);
            console.error("param3", param3);
            alert("Error al ingresar el usuario. Por favor, intente nuevamente.");  
        }
    });
}
$("#GuardarBtn").on("click", function () {
    let dtoUsuarioSistema = {
        usuario: $("#UsuarioTxt").val(),
        contrasenia: $("#ContraseniaTxt").val(),
        rol: $("#RolTxt").val()
    };
    fnUsuarioSistemaInsert(dtoUsuarioSistema);
    console.log(dtoUsuarioSistema);
})