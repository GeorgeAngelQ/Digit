$(document).ready(function () {
    $("#GuardarBtn").click(function () {
        const archivoInput = $("#ArchivoDocumento")[0].files[0];

        if (!archivoInput) {
            alert("Seleccione un archivo.");
            return;
        }

        const formData = new FormData();
        formData.append("Archivo", archivoInput);
        formData.append("IdProceso", $("#IdProcesoCbo").val());
        formData.append("NombreDocumento", $("#NombreDocumentoTxt").val());
        formData.append("TipoDocumento", $("#TipoDocumentoTxt").val());
        formData.append("FormatoDocumento", $("#FormatoDocumentoTxt").val());
        formData.append("FechaDigitalizacion", $("#FechaDigitalizacionTxt").val());
        formData.append("Estado_Documento", $("#EstadoDocumentoTxt").val());

        $.ajax({
            url: "/mantenimiento/documento/insert",
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function () {
                alert("Documento registrado y archivo guardado correctamente.");
                window.location.href = "/mantenimiento/documento";
            },
            error: function () {
                alert("Error al registrar el documento o guardar el archivo.");
            }
        });
    });
});
