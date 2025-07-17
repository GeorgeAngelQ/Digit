"use strict";

$(document).ready(function () {
    let currentPage = 1;
    const pageSize = 5;
    const orderBy = 'IdResponsable';
    const sortOrder = true;
    let totalPages = 0;
    let currentTexto = '';

    fnCargarResponsables();

    $("#FiltroTxt").on("input", function () {
        currentTexto = $(this).val();
        currentPage = 1;
        fnCargarResponsables();
    });

    $("#prevPage").on("click", function () {
        if (currentPage > 1) {
            currentPage--;
            fnCargarResponsables();
        }
    });

    $("#nextPage").on("click", function () {
        if (currentPage < totalPages) {
            currentPage++;
            fnCargarResponsables();
        }
    });

    function fnCargarResponsables() {
        $.ajax({
            type: 'GET',
            url: `https://localhost:7240/api/responsable/pagination?texto=${encodeURIComponent(currentTexto)}&pageSize=${pageSize}&currentPage=${currentPage}&orderBy=${orderBy}&sortOrder=${sortOrder}`,
            success: function (data) {
                $("#ResponsableBody").empty();

                if (Array.isArray(data) && data.length > 0) {
                    data.forEach(resp => {
                        $("#ResponsableBody").append(`
                            <tr>
                                <td>${(resp["NombreResponsable"] || '') + ' ' + (resp["ApellidoResponsable"] || '')}</td>
                                <td>${resp["CorreoResponsable"] || ''}</td>
                                <td>${resp["TelefonoResponsable"] || ''}</td>
                                <td>${resp["CargoResponsable"] || ''}</td>
                                <td>
                                    <button class="btn btn-warning btn-sm me-1 EditarBtn" data-id="${resp.IdResponsable}">
                                        <i class="fa-solid fa-pen-to-square"></i>
                                    </button>
                                    <button class="btn btn-danger btn-sm EliminarBtn" data-id="${resp.IdResponsable}">
                                        <i class="fa-solid fa-trash"></i>
                                    </button>
                                </td>
                            </tr>
                        `);
                    });

                    $(".EditarBtn").on("click", function () {
                        const idResponsable = $(this).data("id");
                        window.location.href = "/mantenimiento/responsable/editar/" + idResponsable;
                    });

                    $(".EliminarBtn").on("click", function () {
                        const idResponsable = $(this).data("id");
                        if (confirm("¿Deseas eliminar al responsable?")) {
                            fnEliminarResponsable(idResponsable);
                        }
                    });

                    const totalRegistros = data[0].TotalRegistros || 0;
                    totalPages = Math.ceil(totalRegistros / pageSize);
                    $("#pageInfo").text(`Página ${currentPage} de ${totalPages}`);
                } else {
                    $("#ResponsableBody").append(`
                        <tr><td colspan="6" class="text-center">No se encontraron responsables.</td></tr>
                    `);
                    totalPages = 0;
                    $("#pageInfo").text("Página 1 de 0");
                }
            },
            error: function (xhr) {
                console.error("Error al cargar los responsables:", xhr);
                alert("Error al cargar los responsables.");
            }
        });
    }

    function fnEliminarResponsable(idResponsable) {
        $.ajax({
            type: 'DELETE',
            url: "https://localhost:7179/mantenimiento/responsable/delete/" + idResponsable,
            success: function () {
                alert("Responsable eliminado correctamente.");
                fnCargarResponsables();
            },
            error: function () {
                alert("Error al eliminar el responsable.");
            }
        });
    }
});
