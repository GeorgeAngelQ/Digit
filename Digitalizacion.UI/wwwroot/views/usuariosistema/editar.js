"use strict";
function fnUsuarioSistemaUpdate(dtoUsuarioSistema) {
    let strUsuarioSistema = JSON.stringify(dtoUsuarioSistema);
    $.ajax({
        type: 'PUT',
        cache: false,
        async: true,
        dataType: "text",
        url: "https://localhost:7179/mantenimiento/usuariosistema/update/" + dtoUsuarioSistema.IdUsuario,
        data: { usuarioSistema: strUsuarioSistema },
        success: function () {
            alert("Se actualizó con éxito el usuario: " + dtoUsuarioSistema.Usuario);
            window.location.href = "/mantenimiento/usuariosistema/index";
        },
        error(param1, param2, param3) {
            console.error("param1", param1);
            console.error("param2", param2);
            console.error("param3", param3);
            alert("Error al actualizar el usuario. Por favor, intente nuevamente.");
        }
    });
}
$("#GuardarBtn").on("click", function () {
    let dtoUsuarioSistema = {
        IdUsuario: $("#IdUsuarioTxt").val(),
        NombreCompleto: $("#NombreCompletoTxt").val(),
        CorreoElectronico: $('#CorreoElectronicoTxt').val(),
        Usuario: $("#UsuarioTxt").val(),
        Contrasenia: $("#ContraseniaTxt").val(),
        Rol: $("#RolTxt").val()
    };
    fnUsuarioSistemaUpdate(dtoUsuarioSistema);
    console.log(dtoUsuarioSistema);
});
