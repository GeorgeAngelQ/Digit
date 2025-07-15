"use strict";

$(document).ready(function () {
    let currentPage = 1;
    const pageSize = 5;
    const orderBy = 'IdDepartamento';
    const sortOrder = true;
    let totalPages = 0;
    let currentTexto = '';

    fnCargarDepartamentos();

    $("#FiltroTxt").on("input", function () {
        currentTexto = $(this).val();
        currentPage = 1;
        fnCargarDepartamentos();
    });

    $("#prevPage").on("click", function () {
        if (currentPage > 1) {
            currentPage--;
            fnCargarDepartamentos();
        }
    });

    $("#nextPage").on("click", function () {
        if (currentPage < totalPages) {
            currentPage++;
            fnCargarDepartamentos();
        }
    });

    function fnCargarDepartamentos() {
        $.ajax({
            type: 'GET',
            url: `https://localhost:7240/api/departamento/pagination?texto=${encodeURIComponent(currentTexto)}&pageSize=${pageSize}&currentPage=${currentPage}&orderBy=${orderBy}&sortOrder=${sortOrder}`,
            success: function (data) {
                $("#DepartamentoBody").empty();

                if (Array.isArray(data) && data.length > 0) {
                    data.forEach(dep => {
                        $("#DepartamentoBody").append(`
                            <tr>
                                <td>${dep["NombreDepartamento"] || ''}</td>
                                <td>${dep["UbicacionDepartamento"] || ''}</td>
                                <td>${dep["ExtensionDepartamento"] || ''}</td>
                                <td>
                                    <button class="btn btn-warning btn-sm me-1 EditarBtn" data-id="${dep.IdDepartamento}">
                                        <i class="fa-solid fa-pen-to-square"></i>
                                    </button>
                                    <button class="btn btn-danger btn-sm EliminarBtn" data-id="${dep.IdDepartamento}">
                                        <i class="fa-solid fa-trash"></i>
                                    </button>
                                </td>
                            </tr>
                        `);
                    });

                    $(".EditarBtn").on("click", function () {
                        const idDepartamento = $(this).data("id");
                        window.location.href = "/mantenimiento/departamento/editar/" + idDepartamento;
                    });

                    $(".EliminarBtn").on("click", function () {
                        const idDepartamento = $(this).data("id");
                        if (confirm("¿Deseas eliminar este departamento?")) {
                            fnEliminarDepartamento(idDepartamento);
                        }
                    });

                    const totalRegistros = data[0].TotalRegistros || 0;
                    totalPages = Math.ceil(totalRegistros / pageSize);
                    $("#pageInfo").text(`Página ${currentPage} de ${totalPages}`);
                } else {
                    $("#DepartamentoBody").append(`
                        <tr><td colspan="6" class="text-center">No se encontraron departamentos.</td></tr>
                    `);
                    totalPages = 0;
                    $("#pageInfo").text("Página 1 de 0");
                }
            },
            error: function (xhr) {
                console.error("Error al cargar los departamentos:", xhr);
                alert("Error al cargar los departamentos.");
            }
        });
    }

    function fnEliminarDepartamento(idDepartamento) {
        $.ajax({
            type: 'DELETE',
            url: "https://localhost:7179/mantenimiento/departamento/delete/" + idDepartamento,
            success: function () {
                alert("Departamento eliminado correctamente.");
                fnCargarDepartamentos();
            },
            error: function () {
                alert("Error al eliminar el departamento.");
            }
        });
    }
});
