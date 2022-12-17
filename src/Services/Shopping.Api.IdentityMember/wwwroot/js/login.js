$(function () {
    
    $(".qrLogin").click(function () {
        console.log("qrLogin");
        $(this).addClass('active');
        $(".qrcontent").show();

        $(".accountLogin").removeClass('active');
        $(".accountcontent").hide();

        let qrvalue = $("#qrcodeValue").val();
        let qrcodeValuePrefix = $("#qrcodeValuePrefix").val();
        let qrcode = new QRCode(document.getElementById("qrcode"), {
            text: qrcodeValuePrefix+qrvalue,
            width: 150,
            height: 150,
            colorDark: "#000000",
            colorLight: "#ffffff",
            correctLevel: QRCode.CorrectLevel.H
        });
    });
    $(".accountLogin").click(function () {
        console.log("accountLogin");
        $(this).addClass('active');
        $(".accountcontent").show();

        $(".qrLogin").removeClass('active');
        $(".qrcontent").hide();
    });
    
    setInterval(() => {
        if ($(".qrLogin").hasClass("active")) {
            let qrvalue = $("#qrcodeValue").val();
            $.ajax({
                url: "/Account/QRCodeCheck?qrcode=" + qrvalue,
                type: "get",
                dataType: "json",
                success: function (resp) {
                    console.log("success", resp);
                    if (resp.status) {
                        $("#qrcodeStatus").val("1");
                        $("#submit").click();
                    }
                },
                error: function (err,m) {
                    console.log("error", err,m);
                }
            });
        }

    }, 5000);
    
})