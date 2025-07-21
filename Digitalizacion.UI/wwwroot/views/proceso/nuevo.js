$(document).ready(function () {

    $("#GuardarBtn").click(function () {
        let proceso = {
            IdResponsable: $("#IdResponsableCbo").val(),
            IdDepartamento: $("#IdDepartamentoCbo").val(),
            IdEquipo: $("#IdEquipoCbo").val(),
            FechaInicio: $("#FechaInicioTxt").val(),
            FechaFin: $("#FechaFinTxt").val(),
            Estado: $("#EstadoTxt").val(),
            Prioridad: $("#PrioridadTxt").val()
        };

        if (!proceso.IdResponsable || !proceso.IdDepartamento || !proceso.IdEquipo ||
            !proceso.Estado || !proceso.Prioridad) {
            alert("Por favor complete todos los campos obligatorios.");
            return;
        }

        $.ajax({
            url: "/mantenimiento/proceso/insert",
            type: "POST",
            data: { proceso: JSON.stringify(proceso) },
            success: function () {
                alert("Proceso registrado correctamente.");
                window.location.href = "/mantenimiento/proceso";
            },
            error: function (xhr) {
                console.error(xhr.responseText);
                alert("Ocurrió un error al registrar el proceso.");
            }
        });
    });

});
