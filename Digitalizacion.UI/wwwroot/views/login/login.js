$("#LoginBtn").on("click", function () {
    const data = {
        usuario: $("#UsuarioTxt").val(),
        contrasenia: $("#ContraseniaTxt").val()
    };

    $.ajax({
        type: 'POST',
        url: '/login',
        data: data,
        success: function (response) {
            if (response.success) {
                window.location.href = "/home/index";
            } else {
                alert(response.message);
            }
        },
        error: function () {
            alert("Error de red. Intente nuevamente.");
        }
    });
});
