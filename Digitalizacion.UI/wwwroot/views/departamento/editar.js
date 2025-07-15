"use strict";
function fnDepartamentoUpdate(dtoDepartamento) {
    let strDepartamento = JSON.stringify(dtoDepartamento);
    $.ajax({
        type: 'PUT',
        cache: false,
        async: true,
        dataType: "text",
        url: "https://localhost:7179/mantenimiento/departamento/update/" + dtoDepartamento.IdDepartamento,
        data: { departamento: strDepartamento },
        success: function () {
            alert("Se actualizó con éxito el departamento: " + dtoDepartamento.NombreDepartamento);
            window.location.href = "/mantenimiento/departamento/index";
        },
        error(param1, param2, param3) {
            console.error("param1", param1);
            console.error("param2", param2);
            console.error("param3", param3);
            alert("Error al actualizar el departamento. Por favor, intente nuevamente.");
        }
    });
}
$("#GuardarBtn").on("click", function () {
    let dtoDepartamento = {
        IdDepartamento: $("#IdDepartamentoTxt").val(),
        NombreDepartamento: $("#NombreDepartamentoTxt").val(),
        UbicacionDepartamento: $('#UbicacionDepartamentoTxt').val(),
        ExtensionDepartamento: $("#ExtensionDepartamentoTxt").val()
    };
    fnDepartamentoUpdate(dtoDepartamento);
    console.log(dtoDepartamento);
});
