$(document).ready(function () {
    $("#tab-register").click(function () {
        $.ajax({
            type: 'GET',
            url: '/account/register', // Указываем URL представления
            success: function (data) {
                // Обработка успешного ответа (если необходимо)
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });
    });
});