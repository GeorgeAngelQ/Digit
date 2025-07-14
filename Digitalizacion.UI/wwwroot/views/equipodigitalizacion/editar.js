"use strict";
function fnEquipoDigitalizacionUpdate(dtoEquipoDigitalizacion) {
    let strEquipoDigitalizacion = JSON.stringify(dtoEquipoDigitalizacion);
    $.ajax({
        type: 'PUT',
        cache: false,
        async: true,
        dataType: "text",
        url: "https://localhost:7179/mantenimiento/equipodigitalizacion/update/" + dtoEquipoDigitalizacion.IdEquipo,
        data: { EquipoDigitalizacion: strEquipoDigitalizacion },
        success: function () {
            alert("Se actualizó con éxito el equipo: " + dtoEquipoDigitalizacion.MarcaEquipo + " " + dtoEquipoDigitalizacion.ModeloEquipo);
            window.location.href = "/mantenimiento/equipodigitalizacion/index";
        },
        error(param1, param2, param3) {
            console.error("param1", param1);
            console.error("param2", param2);
            console.error("param3", param3);
            alert("Error al actualizar el equipo. Por favor, intente nuevamente.");
        }
    });
}
$("#GuardarBtn").on("click", function () {
    let dtoEquipoDigitalizacion = {
        IdEquipo: $("#IdEquipoTxt").val(),
        TipoEquipo: $("#TipoEquipoTxt").val(),
        MarcaEquipo: $('#MarcaEquipoTxt').val(),
        ModeloEquipo: $("#ModeloEquipoTxt").val(),
        EstadoEquipo: $("#EstadoEquipoTxt").val(),
        UbicacionEquipo: $("#UbicacionEquipoTxt").val()
    };
    fnEquipoDigitalizacionUpdate(dtoEquipoDigitalizacion);
    console.log(dtoEquipoDigitalizacion);
});
