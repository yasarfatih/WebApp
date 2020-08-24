$().ready(function () {
    //gönder butonuna tıklandığında
    $("#send-form").click(function () {
        //form içerisindeki tüm verileri serileştir.
        var formData = $("#contact-form").serialize();
        console.log(formData);
        //form bilgilerini sunucuya gönder.
        $.ajax({
            url: '/Contact/SendMessage',
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json',
            data: formData,
            success: function (result) {
                if (result.Status === "error") {
                    $("#errormessage").html(result.Message);
                    $("#errormessage").show(600);
                    $("#sendmessage").hide();
                }
                else {
                    $("#sendmessage").html(result.Message);
                    $("#sendmessage").show(600);
                    $("#errormessage").hide();
                    $("#contact-form").remove();
                }
            }
        });
    });

    
});