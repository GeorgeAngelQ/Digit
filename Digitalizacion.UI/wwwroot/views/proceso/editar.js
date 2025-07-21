$(document).ready(function () {
    $("#GuardarBtn").click(function () {
        let proceso = {
            IdProceso: $("#IdProcesoTxt").val(),
            IdResponsable: $("#IdResponsableCbo").val(),
            IdDepartamento: $("#IdDepartamentoCbo").val(),
            IdEquipo: $("#IdEquipoCbo").val(),
            FechaInicio: $("#FechaInicioTxt").val(),
            FechaFin: $("#FechaFinTxt").val(),
            Estado: $("#EstadoTxt").val(),
            Prioridad: $("#PrioridadTxt").val()
        };

        if (!proceso.IdResponsable || !proceso.IdDepartamento || !proceso.IdEquipo ||
            !proceso.FechaInicio || !proceso.Estado || !proceso.Prioridad) {
            alert("Por favor complete todos los campos obligatorios.");
            return;
        }

        $.ajax({
            url: "/mantenimiento/proceso/update/" + proceso.IdProceso,
            type: "PUT",
            data: { proceso: JSON.stringify(proceso) },
            success: function () {
                alert("Proceso actualizado correctamente.");
                window.location.href = "/mantenimiento/proceso";
            },
            error: function (xhr) {
                console.error(xhr.responseText);
                alert("Ocurrió un error al actualizar el proceso.");
            }
        });
    });
});
