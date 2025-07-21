"use strict";
$(document).ready(function () {
    let currentPage = 1;
    let pageSize = 5;
    let totalPages = 0;
    let currentTexto = "";

    cargarDocumentos();

    $("#FiltroTxt").on("input", function () {
        currentTexto = $(this).val().toLowerCase();
        currentPage = 1;
        cargarDocumentos();
    });

    $("#prevPage").on("click", function () {
        if (currentPage > 1) {
            currentPage--;
            cargarDocumentos();
        }
    });

    $("#nextPage").on("click", function () {
        if (currentPage < totalPages) {
            currentPage++;
            cargarDocumentos();
        }
    });

    function cargarDocumentos() {
        $.ajax({
            type: 'GET',
            url: `https://localhost:7240/api/documento/pagination?texto=${encodeURIComponent(currentTexto)}&pageSize=${pageSize}&currentPage=${currentPage}&orderBy=NombreDocumento&sortOrder=true`,
            success: function (data) {
                if (Array.isArray(data) && data.length > 0) {
                    let totalRegistros = data[0].totalRegistros || 0;
                    totalPages = Math.ceil(totalRegistros / pageSize);
                    $("#DocumentoBody").empty();

                    data.forEach(doc => {
                        $("#DocumentoBody").append(`
                            <tr>
                                <td>${doc.nombreDocumento}</td>
                                <td>${doc.tipoDocumento}</td>
                                <td>${doc.formatoDocumento}</td>
                                <td>${doc.estado_Documento}</td>
                                <td>${doc.fechaDigitalizacion?.split('T')[0]}</td>
                                <td>
                                    <button class="btn btn-warning btn-sm me-1 EditarBtn" data-id="${doc.idDocumento}">
                                        <i class="fa-solid fa-pen-to-square"></i>
                                    </button>
                                    <button class="btn btn-danger btn-sm EliminarBtn" data-id="${doc.idDocumento}">
                                        <i class="fa-solid fa-trash"></i>
                                    </button>
                                </td>
                            </tr>
                        `);
                    });

                    $("#pageInfo").text(`Página ${currentPage} de ${totalPages}`);

                    $(".EditarBtn").on("click", function () {
                        const id = $(this).data("id");
                        window.location.href = `/mantenimiento/documento/editar/${id}`;
                    });

                    $(".EliminarBtn").on("click", function () {
                        const id = $(this).data("id");
                        if (confirm("¿Deseas eliminar este documento?")) {
                            eliminarDocumento(id);
                        }
                    });

                } else {
                    $("#DocumentoBody").html(`<tr><td colspan="6" class="text-center">No se encontraron documentos.</td></tr>`);
                    $("#pageInfo").text("Página 0 de 0");
                }
            },
            error: function () {
                alert("Error al cargar los documentos.");
            }
        });
    }

    function eliminarDocumento(id) {
        $.ajax({
            type: 'DELETE',
            url: `https://localhost:7240/api/documento/delete/${id}`,
            success: function () {
                alert("Documento eliminado correctamente.");
                cargarDocumentos();
            },
            error: function () {
                alert("Error al eliminar el documento.");
            }
        });
    }
});
