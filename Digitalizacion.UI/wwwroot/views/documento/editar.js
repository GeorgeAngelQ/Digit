$(document).ready(function () {
    $("#ActualizarBtn").click(function () {
        let documento = {
            IdDocumento: $("#IdDocumento").val(),
            IdProceso: $("#IdProcesoCbo").val(),
            NombreDocumento: $("#NombreDocumentoTxt").val(),
            TipoDocumento: $("#TipoDocumentoTxt").val(),
            FormatoDocumento: $("#FormatoDocumentoTxt").val(),
            FechaDigitalizacion: $("#FechaDigitalizacionTxt").val(),
            Estado_Documento: $("#EstadoDocumentoTxt").val()
        };

        if (!documento.IdProceso || !documento.NombreDocumento || !documento.TipoDocumento ||
            !documento.FormatoDocumento || !documento.FechaDigitalizacion || !documento.Estado_Documento) {
            alert("Por favor complete todos los campos obligatorios.");
            return;
        }

        $.ajax({
            url: "/mantenimiento/documento/update/" + documento.IdDocumento,
            type: "PUT",
            data: { documento: JSON.stringify(documento) },
            success: function () {
                alert("Documento actualizado correctamente.");
                window.location.href = "/mantenimiento/documento";
            },
            error: function () {
                alert("Error al actualizar el documento.");
            }
        });
    });
});
