"use strict";
$(document).ready(function () {
    let currentPage = 1;
    const pageSize = 5;
    const orderBy = 'IdUsuario';
    const sortOrder = true;
    let totalPages = 0;
    let currentTexto = '';

    fnCargarUsuarios();

    $("#FiltroTxt").on("input", function () {
        currentTexto = $(this).val();
        currentPage = 1;
        fnCargarUsuarios();
    });

    $("#prevPage").on("click", function () {
        if (currentPage > 1) {
            currentPage--;
            fnCargarUsuarios();
        }
    });

    $("#nextPage").on("click", function () {
        if (currentPage < totalPages) {
            currentPage++;
            fnCargarUsuarios();
        }
    });

    function fnCargarUsuarios() {
        $.ajax({
            type: 'GET',
            url: `https://localhost:7240/api/usuariosistema/pagination?texto=${encodeURIComponent(currentTexto)}&pageSize=${pageSize}&currentPage=${currentPage}&orderBy=${orderBy}&sortOrder=${sortOrder}`,
            success: function (data) {
                $("#UsuarioSistemaBody").empty();

                if (Array.isArray(data) && data.length > 0) {
                    data.forEach(usuario => {
                        $("#UsuarioSistemaBody").append(`
                            <tr>
                                <td>${usuario["Usuario"] || ''}</td>
                                <td>••••••••</td>
                                <td>${usuario["NombreCompleto"] || ''}</td>
                                <td>${usuario["CorreoElectronico"] || ''}</td>
                                <td>${usuario["Rol"] || ''}</td>
                                <td>
                                    <button class="btn btn-warning btn-sm me-1 EditarBtn" data-id="${usuario.IdUsuario}">
                                        <i class="fa-solid fa-pen-to-square"></i>
                                    </button>
                                    <button class="btn btn-danger btn-sm EliminarBtn" data-id="${usuario.IdUsuario}">
                                        <i class="fa-solid fa-trash"></i>
                                    </button>
                                </td>
                            </tr>
                        `);
                    });

                    $(".EditarBtn").on("click", function () {
                        const idUsuario = $(this).data("id");
                        window.location.href = "/mantenimiento/usuariosistema/editar/" + idUsuario;
                    });

                    $(".EliminarBtn").on("click", function () {
                        const idUsuario = $(this).data("id");
                        if (confirm("¿Deseas eliminar este usuario?")) {
                            fnEliminarUsuario(idUsuario);
                        }
                    });

                    const totalRegistros = data[0].TotalRegistros || 0;
                    totalPages = Math.ceil(totalRegistros / pageSize);
                    $("#pageInfo").text(`Página ${currentPage} de ${totalPages}`);
                } else {
                    $("#UsuarioSistemaBody").append(`
                        <tr><td colspan="6" class="text-center">No se encontraron usuarios.</td></tr>
                    `);
                    totalPages = 0;
                    $("#pageInfo").text("Página 1 de 0");
                }
            },
            error: function (xhr) {
                console.error("Error al cargar los usuarios:", xhr);
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
