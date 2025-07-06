"use strict";
$(document).ready(function () {
    $("#FiltrarBtn").on("click", function () {
        let filtro = $("#IdUsuarioTxt").val();
        if (filtro) {
            fnUsuarioSistemaFiltrar(filtro);
        }
        else {
            alert("Por favor ingrese un ID para buscar")
        }
    });

    function fnUsuarioSistemaFiltrar(idUsuario) {
        $.ajax({
            type: 'GET',
            cache: false,
            async: true,
            dataType: "json",
            url: "https://localhost:7179/mantenimiento/usuariosistema/select-by-id/" + idUsuario,
            success: function (data) {
                if (data) {
                    console.log("Usuario encontrado:", data);
                    $("#ResultadosDiv").html(`
                        <div class="card p-3 mt-3">
                            <h5>Usuario: ${data.usuario}</h5>
                            <p>Contrasenia: ${data.contrasenia}</p>
                            <p>Rol: ${data.rol}</p>
                        </div>
                    `);
                }
                else {
                    alert("Usuario no encontrado")
                }
            },
            error(param1, param2, param3) {
                console.error("param1", param1);
                console.error("param2", param2);
                console.error("param3", param3);
                alert("Error al buscar el usuario. Por favor, intente nuevamente.");
            }

        });
    }
    function fnUsuarioSistemaDelete(idUsuario) {
        $.ajax({
            type: 'DELETE',
            cache: false,
            async: true,
            dataType: "text",
            url: "https://localhost:7179/mantenimiento/usuariosistema/delete/" + idUsuario,
            success: function () {
                alert("Usuario eliminado exitosamente: " + idUsuario);
                window.location.href = "/mantenimiento/usuariosistema/index"
            },
            error(param1, param2, param3) {
                console.error("param1", param1);
                console.error("param2", param2);
                console.error("param3", param3);
                alert("Error al eliminar el usuario. Por favor, intente nuevamente.");
            }
        });
    }
    $("#EditarBtn").on("click", function () {
        let idUsuario = $("#IdUsuarioTxt").val();

        if (idUsuario) {
            window.location.href = "https://localhost:7179/mantenimiento/usuariosistema/editar/" + idUsuario;
        }
        else {
            alert("Por favor ingrese un ID para editar");
        }
    });
    $("#EliminarBtn").on("click", function () {
        let idUsuario = $("#IdUsuarioTxt").val();
        if (idUsuario) {
            if (confirm("¿Estás seguro que deseas eliminar ese usuario?")) {
                fnUsuarioSistemaDelete(idUsuario);
            }
        } else {
            alert("Por favor ingrese un ID de usuario para editar.");
        }
    });
});