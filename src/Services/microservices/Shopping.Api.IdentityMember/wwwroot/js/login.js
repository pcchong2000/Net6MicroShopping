
var qrcode;
$(function () {
    $(".qrLogin").click(function () {
        console.log("qrLogin");
        $(this).addClass('active');
        $(".qrcontent").show();

        $(".accountLogin").removeClass('active');
        $(".accountcontent").hide();

        DrawQRCode();
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
            QRCodeCheck()
        }

    }, 5000);

    function DrawQRCode() {
        let qrvalue = $("#qrcodeValue").val();
        let qrcodeValuePrefix = $("#qrcodeValuePrefix").val();
        //if (qrcode) {
        //    qrcode.clear();
        //    $("#qrcode").html("");
        //}
        qrcode = new QRCode(document.getElementById("qrcode"), {
            text: qrcodeValuePrefix + qrvalue,
            width: 150,
            height: 150,
            colorDark: "#000000",
            colorLight: "#ffffff",
            correctLevel: QRCode.CorrectLevel.H
        });
    }
    function QRCodeCheck() {
        let qrvalue = $("#qrcodeValue").val();
        $.ajax({
            url: "/Account/QRCodeCheck?qrcode=" + qrvalue,
            type: "get",
            dataType: "json",
            success: function (resp) {
                console.log("success", resp);
                if (resp.status == 1) {
                    $("#qrcodeStatus").val("1");
                    $("#submit").click();
                } else if (resp.status == -1) {
                    QRCodeReset();

                }
            },
            error: function (err, m) {
                console.log("error", err, m);
            }
        });
    }
    function QRCodeReset() {
        $.ajax({
            url: "/Account/QRCodeReset",
            type: "post",
            contentType:'application/json',
            data: JSON.stringify({
                ReturnUrl: $("#ReturnUrl").val()
            }),
            dataType: "json",
            success: function (resp) {
                console.log("success", resp);
                if (resp.qrCode) {
                    $("#qrcodeValue").val(resp.qrCode);
                    $("#qrcodeStatus").val("0");
                    let qrcodeValuePrefix = $("#qrcodeValuePrefix").val();
                    
                    if (qrcode) {
                        qrcode.clear(); // clear the code.
                        qrcode.makeCode(qrcodeValuePrefix+resp.qrCode);
                    }
                    
                }
            },
            error: function (err, m) {
                console.log("error", err, m);
            }
        });
    }
})