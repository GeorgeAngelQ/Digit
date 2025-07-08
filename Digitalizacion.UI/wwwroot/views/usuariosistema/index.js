"use strict";
$(document).ready(function () {
    fnCargarUsuarios();
    $("#FiltroTxt").on("input", function () {
        const texto = $(this).val().toLowerCase();
        $("#UsuarioSistemaBody tr").each(function () {
            const usuario = $(this).find("td:first").text().toLowerCase();
            $(this).toggle(usuario.includes(texto));
        });
    });
    function fnCargarUsuarios() {
        $.ajax({
            type: 'GET',
            url: "https://localhost:7240/api/usuariosistema/list",
            success: function (data) {
                if (data && Array.isArray(data)) {
                    $("#UsuarioSistemaBody").empty();
                    data.forEach(usuario => {
                        $("#UsuarioSistemaBody").append(`
                            <tr>
                                <td>${usuario.usuario}</td>
                                <td>••••••••</td>
                                <td>${usuario.nombreCompleto || ''}</td>
                                <td>${usuario.correoElectronico || ''}</td>
                                <td>${usuario.rol || ''}</td>
                                <td>
                                    <button class="btn btn-warning btn-sm me-1 EditarBtn" data-id="${usuario.idUsuario}">
                                        <i class="fa-solid fa-pen-to-square"></i>
                                    </button>
                                    <button class="btn btn-danger btn-sm EliminarBtn" data-id="${usuario.idUsuario}">
                                        <i class="fa-solid fa-trash"></i>
                                    </button>
                                </td>
                            </tr>
                        `);
                    });
                    $(".EditarBtn").on("click", function () {
                        const id = $(this).data("id");
                        window.location.href = "/mantenimiento/usuariosistema/editar/" + id;
                    });

                    $("EliminarBtn").on("click", function () {
                        const id = $(this).data("id");
                        if (confirm("¿Deseas eliminar este usuario?")) {
                            fnEliminarUsuario(id);
                        }
                    });
                } else {
                    alert("No se encontraron usuarios.");
                }
            },
            error: function () {
                alert("Error al cargar los usuarios.");
            }
        });
    }

    function fnEliminarUsuario(idUsuario) {
        $.ajax({
            type: 'DELETE',
            url: "https://localhost:7179/mantenimiento/usuariosistema/delete/" + idUsuario,
            success: function () {
                alert("Usuario eliminado correctamente.");
                fnCargarUsuarios();
            },
            error: function () {
                alert("Error al eliminar el usuario.");
            }
        });
    }
});
