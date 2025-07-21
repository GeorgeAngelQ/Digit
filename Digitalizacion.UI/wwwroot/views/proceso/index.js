"use strict";
$(document).ready(function () {
    let currentPage = 1;
    let pageSize = 5;
    let totalPages = 0;
    let currentTexto = "";

    cargarProcesos();

    $("#FiltroTxt").on("input", function () {
        currentTexto = $(this).val().toLowerCase();
        currentPage = 1;
        cargarProcesos();
    });

    $("#prevPage").on("click", function () {
        if (currentPage > 1) {
            currentPage--;
            cargarProcesos();
        }
    });

    $("#nextPage").on("click", function () {
        if (currentPage < totalPages) {
            currentPage++;
            cargarProcesos();
        }
    });

    function cargarProcesos() {
        $.ajax({
            type: 'GET',
            url: `https://localhost:7240/api/proceso/pagination?texto=${encodeURIComponent(currentTexto)}&pageSize=${pageSize}&currentPage=${currentPage}&orderBy=Estado&sortOrder=true`,
            success: function (data) {
                if (Array.isArray(data) && data.length > 0) {
                    let totalRegistros = data[0].totalRegistros || 0;
                    totalPages = Math.ceil(totalRegistros / pageSize);
                    $("#ProcesoBody").empty();
                    data.forEach(proceso => {
                        $("#ProcesoBody").append(`
                            <tr>
                                <td>${proceso.nombreResponsable}</td>
                                <td>${proceso.nombreDepartamento}</td>
                                <td>${proceso.marcaEquipo} ${proceso.modeloEquipo}</td>
                                <td>${proceso.fechaInicio?.split('T')[0]}</td>
                                <td>${proceso.fechaFin ? proceso.fechaFin.split('T')[0] : ''}</td>
                                <td>${proceso.estado}</td>
                                <td>${proceso.prioridad}</td>
                                <td>
                                    <button class="btn btn-warning btn-sm me-1 EditarBtn" data-id="${proceso.idProceso}">
                                        <i class="fa-solid fa-pen-to-square"></i>
                                    </button>
                                    <button class="btn btn-danger btn-sm EliminarBtn" data-id="${proceso.idProceso}">
                                        <i class="fa-solid fa-trash"></i>
                                    </button>
                                </td>
                            </tr>
                        `);
                    });

                    $("#pageInfo").text(`Página ${currentPage} de ${totalPages}`);

                    $(".EditarBtn").on("click", function () {
                        const id = $(this).data("id");
                        window.location.href = `/mantenimiento/proceso/editar/${id}`;
                    });

                    $(".EliminarBtn").on("click", function () {
                        const id = $(this).data("id");
                        if (confirm("¿Deseas eliminar este proceso?")) {
                            eliminarProceso(id);
                        }
                    });

                } else {
                    $("#ProcesoBody").html(`<tr><td colspan="8" class="text-center">No se encontraron procesos.</td></tr>`);
                    $("#pageInfo").text("Página 0 de 0");
                }
            },
            error: function (xhr) {
                console.error("Error al cargar los procesos:", xhr);
                alert("Error al cargar los procesos.");
            }
        });
    }

    function eliminarProceso(idProceso) {
        $.ajax({
            type: 'DELETE',
            url: `https://localhost:7240/api/proceso/delete/${idProceso}`,
            success: function () {
                alert("Proceso eliminado correctamente.");
                cargarProcesos();
            },
            error: function () {
                alert("Error al eliminar el proceso. Puede que esté en uso.");
            }
        });
    }
});
