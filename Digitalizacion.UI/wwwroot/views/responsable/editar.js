"use strict";
function fnResponsableUpdate(dtoResponsable) {
    let strResponsable = JSON.stringify(dtoResponsable);
    $.ajax({
        type: 'PUT',
        cache: false,
        async: true,
        dataType: "text",
        url: "https://localhost:7179/mantenimiento/responsable/update/" + dtoResponsable.IdResponsable,
        data: { responsable: strResponsable },
        success: function () {
            alert("Se actualizó con éxito el responsable: " + dtoResponsable.NombreResponsable + " " + dtoResponsable.ApellidoResponsable);
            window.location.href = "/mantenimiento/responsable/index";
        },
        error(param1, param2, param3) {
            console.error("param1", param1);
            console.error("param2", param2);
            console.error("param3", param3);
            alert("Error al actualizar el responsable. Por favor, intente nuevamente.");
        }
    });
}
$("#GuardarBtn").on("click", function () {
    let dtoResponsable = {
        IdResponsable: $("#IdResponsableTxt").val(),
        NombreResponsable: $("#NombreResponsableTxt").val(),
        ApellidoResponsable: $('#ApellidoResponsableTxt').val(),
        CorreoResponsable: $("#CorreoResponsableTxt").val(),
        TelefonoResponsable: $("#TelefonoResponsableTxt").val(),
        CargoResponsable: $("#CargoResponsableTxt").val()
    };
    fnResponsableUpdate(dtoResponsable);
    console.log(dtoResponsable);
});
