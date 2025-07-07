$(document).ready(function () {
    $("#LoginFrm").on("submit", function (e) {
        e.preventDefault(); 

        const usuario = $("#UsuarioTxt").val();
        const contrasenia = $("#ContraseniaTxt").val();

        $.ajax({
            type: "POST",
            url: "/login", 
            data: {
                usuario: usuario,
                contrasenia: contrasenia
            },
            success: function (response) {
                if (response.success) {
                    window.location.href = "/mantenimiento/usuariosistema/index";
                } else {
                    alert(response.message || "Credenciales incorrectas");
                }
            },
            error: function () {
                alert("Error al procesar el login");
            }
        });
    });
    $("#toggle").on("click", function () {
        const input = $("#ContraseniaTxt");
        input.attr("type", input.attr("type") === "password" ? "text" : "password");
    });
});
