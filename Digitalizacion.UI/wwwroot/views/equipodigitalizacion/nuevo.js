"use strict";
function fnEquipoDigitalizacionInsert(dtoEquipoDigitalizacion) {
    let strEquipoDigitalizacion = JSON.stringify(dtoEquipoDigitalizacion);
    $.ajax({
        type: 'POST',
        cache: false,
        async: true,
        dataType: "text",
        url: "https://localhost:7179/mantenimiento/equipodigitalizacion/insert",
        data: { equipoDigitalizacion: strEquipoDigitalizacion },
        success: function () {
            alert("Se ingresó con éxito el equipo: " + dtoEquipoDigitalizacion.MarcaEquipo + " " + dtoEquipoDigitalizacion.ModeloEquipo);
            window.location.href = "/mantenimiento/equipodigitalizacion/index";
        },
        error(param1, param2, param3) {
            console.error("param1", param1);
            console.error("param2", param2);
            console.error("param3", param3);
            alert("Error al ingresar el equipo. Por favor, intente nuevamente.");
        }
    });
}
$("#GuardarBtn").on("click", function () {
    let dtoEquipoDigitalizacion = {
        TipoEquipo: $("#TipoEquipoTxt").val(),
        MarcaEquipo: $('#MarcaEquipoTxt').val(),
        ModeloEquipo: $("#ModeloEquipoTxt").val(),
        EstadoEquipo: $("#EstadoEquipoTxt").val(),
        UbicacionEquipo: $("#UbicacionEquipoTxt").val()
    };
    fnEquipoDigitalizacionInsert(dtoEquipoDigitalizacion);
    console.log(dtoEquipoDigitalizacion);
})