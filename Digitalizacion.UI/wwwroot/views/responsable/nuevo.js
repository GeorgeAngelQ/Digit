"use strict";
function fnResponsableInsert(dtoResponsable) {
    let strResponsable = JSON.stringify(dtoResponsable);
    $.ajax({
        type: 'POST',
        cache: false,
        async: true,
        dataType: "text",
        url: "https://localhost:7179/mantenimiento/responsable/insert",
        data: { responsable: strResponsable },
        success: function () {
            alert("Se ingresó con éxito el responsable: " + dtoResponsable.NombreResponsable + " " + dtoResponsable.ApellidoResponsable);
            window.location.href = "/mantenimiento/responsable/index";
        },
        error(param1, param2, param3) {
            console.error("param1", param1);
            console.error("param2", param2);
            console.error("param3", param3);
            alert("Error al ingresar el responsable. Por favor, intente nuevamente.");
        }
    });
}
$("#GuardarBtn").on("click", function () {
    let dtoResponsable = {
        NombreResponsable: $("#NombreResponsableTxt").val(),
        ApellidoResponsable: $('#ApellidoResponsableTxt').val(),
        CorreoResponsable: $("#CorreoResponsableTxt").val(),
        TelefonoResponsable: $("#TelefonoResponsableTxt").val(),
        CargoResponsable: $("#CargoResponsableTxt").val()
    };
    fnResponsableInsert(dtoResponsable);
    console.log(dtoResponsable);
})