
$.ShowConfirm = function (title, content, okButtonText,
    okButtonFunc, cancelButtonText, cancelButtonFunc) {

    $.confirm({
        title: title,
        content: content,
        type: 'orange',
        typeAnimated: true,
        useBootstrap: false,
        buttons: {
            yes: {
                text: okButtonText,
                btnClass: 'btn-red',
                action: okButtonFunc
            },
            cancel: {
                text: cancelButtonText,
                btnClass: 'btn-primary',
                action: cancelButtonFunc
            }
        }
    });

};


$.ShowError = function (title, content, okButtonText,
    okButtonFunc) {

    $.confirm({
        title: title,
        content: content,
        type: 'red',
        typeAnimated: true,
        useBootstrap: false,
        buttons: {
            yes: {
                text: okButtonText,
                btnClass: 'btn-danger',
                action: okButtonFunc
            }
        }
    });

};

$.ShowInfo = function (title, content, okButtonText,
    okButtonFunc) {

    $.confirm({
        title: title,
        content: content,
        type: 'blue',
        typeAnimated: true,
        useBootstrap: false,
        buttons: {
            yes: {
                text: okButtonText,
                btnClass: 'btn-primary',
                action: okButtonFunc
            }
        }
    });
};

$.ShowSuccess = function (title, content, okButtonText,
    okButtonFunc) {

    $.confirm({
        title: title,
        content: content,
        type: 'green',
        typeAnimated: true,
        useBootstrap: false,
        buttons: {
            yes: {
                text: okButtonText,
                btnClass: 'btn-success',
                action: okButtonFunc
            }
        }
    });
};


$.Delete = function (url, recId, successFunc) {

    //silme onaylandı

    var obj = new Object();
    obj.id = recId;

    //alert(JSON.stringify(obj))

    //Category/Delete metodunu çağır
    $.ajax({
        url: url,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json", //sen bana ne göndereceksin
        contentType: "application/json;charset=utf-8", //ben sana ne gönderiyorum
        success: successFunc
    });
};


$.CK = function (controlName,finderUrl) {
    $().ready(function () {
        var editor = CKEDITOR.instances[controlName];
        if (editor) { editor.destroy(true); }
        CKEDITOR.replace(controlName, {
            enterMode: CKEDITOR.ENTER_BR
        });
        CKFinder.setupCKEditor(null, finderUrl);
    });
};

$.DataTable = function (tableId) {
    $(document).ready(function () {
        $('#' + tableId).DataTable({
            "language": {
                "sDecimal": ",",
                "sEmptyTable": "Tabloda herhangi bir veri mevcut değil",
                "sInfo": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
                "sInfoEmpty": "Kayıt yok",
                "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
                "sInfoPostFix": "",
                "sInfoThousands": ".",
                "sLengthMenu": "Sayfada _MENU_ kayıt göster",
                "sLoadingRecords": "Yükleniyor...",
                "sProcessing": "İşleniyor...",
                "sSearch": "Ara:",
                "sZeroRecords": "Eşleşen kayıt bulunamadı",
                "oPaginate": {
                    "sFirst": "İlk",
                    "sLast": "Son",
                    "sNext": "Sonraki",
                    "sPrevious": "Önceki"
                },
                "oAria": {
                    "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                    "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
                },
                "select": {
                    "rows": {
                        "_": "%d kayıt seçildi",
                        "0": "",
                        "1": "1 kayıt seçildi"
                    }
                }
            }
        });
    });
};

$.CropImage = function (uploadControlId, aspectRatio = NaN) {

    $().ready(function () {

        var cropperCreated = false;
        var cropper;
        var ratio;
        
        //Kırp yazan buton
        $("#crop-start").click(function () {
            //kırpma yapılacak resim <img> kontrolünün adresi
            $image = $("#crop-preview");
            //resmin genişlik ve yüksekliğini hesaplayalım.
            var imgWidth = $image.get(0).width;
            var imgHeight = $image.get(0).height;
            var browserWidth = $(window).width();
            var browserHeight = $(window).height();

            //resmin ekranın yarısı kadar olması için genişlik ve yüksekliği kaça bölmeliyim.
            ratio = imgWidth / (browserWidth / 2);

            //eğer resim küçük bir resimse ve ekranın yarısına sığıyorsa bu resmi boyutlandırmaya gerek yok.
            if (imgWidth < browserWidth / 2) {
                ratio = 1;
            }

            //resmin yeni genişliği ve yüksekliğini hesaplayalım.
            var newWidth = imgWidth / ratio;
            var newHeigth = imgHeight / ratio;

            //modal kontrolünün genişliğini kırpılacak resim kadar yapalım.
            $(".modal-body").css('padding', '10px');
            if (imgWidth < browserWidth / 2) {
                $(".modal-dialog").css('max-width', browserWidth / 2);
                $(".modal-dialog").css('width', browserWidth / 2);
            }
            else {
                $(".modal-dialog").css('max-width', newWidth + 40);
                $(".modal-dialog").css('width', newWidth + 40);
            }

            if (cropperCreated)
                $image.cropper('destroy');

            //cropper kontrolünü hazırlamam gerekiyor.
            $image.cropper({
                scalable: false,
                zoomable: false,
                minContainerWidth: newWidth,
                minContainerHeight: newHeigth,
                aspectRatio: aspectRatio
            });
            cropper = $image.data('cropper');
            cropperCreated = true;
            //kırpma ekranını açalım.
            $("#crop-modal").modal('show');
        });

        //kullanıcının bilgisayarından seçilen resmi kırpma ekranına yükleyelim.
        $("#" + uploadControlId).on("change", function () {
            var file = $(this).get(0).files[0];
            var fileName = file.name;
            //dosyanın adını input'a yazdır.
            $(this).next().addClass("selected").html(fileName);
            //resmi javascript kullanarak okuyalım
            var reader = new FileReader();
            //okuma işlemini başlatalım.
            reader.readAsDataURL(file);
            //okuma işlemi bittiğinde çalışacak fonsiyonu bağlayalım
            reader.onload = function (e) {
                $("#crop-preview").attr('src', e.target.result);
            };
        });

        $("#crop-complate").click(function () {
            var cropArea = cropper.getCropBoxData();
            //kırpma alanına ait bilgileri modal içerisinde yer alan hidden inputlara atıyorum.
            $("#left").val(cropArea.left);
            $("#top").val(cropArea.top);
            $("#width").val(cropArea.width);
            $("#height").val(cropArea.height);
            $("#ratio").val(ratio);
            $("#crop-modal").modal('hide');
        });
    });

};

$.Select = function (id, placeholder) {
    $().ready(function () {
        $("#" + id).select2({
            placeholder: placeholder
        });
    });        
};


$.UploadFile = function (uploadControlId) {

    $().ready(function () {

        //kullanıcının bilgisayarından seçilen resmi kırpma ekranına yükleyelim.
        $("#" + uploadControlId).on("change", function () {
            var file = $(this).get(0).files[0];
            var fileName = file.name;
            //dosyanın adını input'a yazdır.
            $(this).next().addClass("selected").html(fileName);            
        });

    });

};

$.Mask = function (controlId, mask) {
    $(document).ready(function () {
        $("#" + controlId).inputmask({ "mask": mask });
    });
};

$.Menu = function (controller, action) {
    $().ready(function () {
        var hrefValue = '/Admin/' + controller + '/' + action;
        var clickedA = $("a[href='" + hrefValue + "']");

        //menü açılır menü değil. tekli bir menü
        //açılır menülerde parent().parent() bize <ul class="sub"> verir.
        if (!clickedA.parent().parent().hasClass('sub')) {
            clickedA.trigger('click');
        }
        else { //menu açılır menü
            //tıklanan menüyü taşıyan üst menüye ulaşalım.
            //Örneğin Kategori Ekle'yi taşıyan üst menü Kategori İşlemleri'dir.
            var menuParentA = clickedA.parent().parent().prev();
            //Tıklanan menüyü etkin hale getirmek için ative class uygulanıyor.
            clickedA.parent().addClass('active');
            menuParentA.trigger('click');            
        }

        //eğer yukarıdaki adresle href değeri eşleşen bir a etiketi yoksa
        //şu an görüntülenen sayfanın menü karşılığı yoktur.
        if (clickedA.length === 0) {
            //son açılan menünün id değerini sessionStorage içerisinde saklamıştık.
            var lastMenuId = sessionStorage.getItem('id');
            //şu an adres satırında gördüğüm adrese karşılık gelen bir menü
            //olmadığından bir önceki açılır menüyü açık hale getireyim.
            $("a[data-id='" + lastMenuId + "']").trigger('click');
        }
        //herhangi bir açılır menü açıldığında bu menüye ait data-id değerini
        //sessionStorage içerisinde saklayalım.
        $("a[data-id]").click(function () {
            sessionStorage.setItem('id', $(this).data('id'));
        });

    });
};