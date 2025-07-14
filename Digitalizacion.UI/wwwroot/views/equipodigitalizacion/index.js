"use strict";

$(document).ready(function () {
    let currentPage = 1;
    const pageSize = 5;
    const orderBy = 'IdEquipo';
    const sortOrder = true;
    let totalPages = 0;
    let currentTexto = '';

    fnCargarEquipos();

    $("#FiltroTxt").on("input", function () {
        currentTexto = $(this).val();
        currentPage = 1;
        fnCargarEquipos();
    });

    $("#prevPage").on("click", function () {
        if (currentPage > 1) {
            currentPage--;
            fnCargarEquipos();
        }
    });

    $("#nextPage").on("click", function () {
        if (currentPage < totalPages) {
            currentPage++;
            fnCargarEquipos();
        }
    });

    function fnCargarEquipos() {
        $.ajax({
            type: 'GET',
            url: `https://localhost:7240/api/equipodigitalizacion/pagination?texto=${encodeURIComponent(currentTexto)}&pageSize=${pageSize}&currentPage=${currentPage}&orderBy=${orderBy}&sortOrder=${sortOrder}`,
            success: function (data) {
                $("#EquipoDigitalizacionBody").empty();

                if (Array.isArray(data) && data.length > 0) {
                    data.forEach(equipo => {
                        $("#EquipoDigitalizacionBody").append(`
                            <tr>
                                <td>${equipo["MarcaEquipo"] || ''}</td>
                                <td>${equipo["ModeloEquipo"] || ''}</td>
                                <td>${equipo["EstadoEquipo"] || ''}</td>
                                <td>${equipo["UbicacionEquipo"] || ''}</td>
                                <td>${equipo["TipoEquipo"] || ''}</td>
                                <td>
                                    <button class="btn btn-warning btn-sm me-1 EditarBtn" data-id="${equipo.IdEquipo}">
                                        <i class="fa-solid fa-pen-to-square"></i>
                                    </button>
                                    <button class="btn btn-danger btn-sm EliminarBtn" data-id="${equipo.IdEquipo}">
                                        <i class="fa-solid fa-trash"></i>
                                    </button>
                                </td>
                            </tr>
                        `);
                    });

                    $(".EditarBtn").on("click", function () {
                        const idEquipo = $(this).data("id");
                        window.location.href = "/mantenimiento/equipodigitalizacion/editar/" + idEquipo;
                    });

                    $(".EliminarBtn").on("click", function () {
                        const idEquipo = $(this).data("id");
                        if (confirm("¿Deseas eliminar este equipo?")) {
                            fnEliminarEquipo(idEquipo);
                        }
                    });

                    const totalRegistros = data[0].TotalRegistros || 0;
                    totalPages = Math.ceil(totalRegistros / pageSize);
                    $("#pageInfo").text(`Página ${currentPage} de ${totalPages}`);
                } else {
                    $("#EquipoDigitalizacionBody").append(`
                        <tr><td colspan="6" class="text-center">No se encontraron equipos.</td></tr>
                    `);
                    totalPages = 0;
                    $("#pageInfo").text("Página 1 de 0");
                }
            },
            error: function (xhr) {
                console.error("Error al cargar los equipos:", xhr);
                alert("Error al cargar los equipos.");
            }
        });
    }

    function fnEliminarEquipo(idEquipoDigitalizacion) {
        $.ajax({
            type: 'DELETE',
            url: "https://localhost:7179/mantenimiento/equipodigitalizacion/delete/" + idEquipoDigitalizacion,
            success: function () {
                alert("Equipo eliminado correctamente.");
                fnCargarEquipos();
            },
            error: function () {
                alert("Error al eliminar el equipo.");
            }
        });
    }
});
