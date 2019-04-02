
    function GetFileName() {
                       // Get your file input (by it's id)
                       var fileInput = document.getElementById('myFile');
                       // Use a regular expression to pull the file name only
                       var fileName = fileInput.value.split(/(\\|\/)/g).pop();
                       // Alert the file name (example)
                       alert(fileName);
};

$(function () {

    $("input:file").change(function () {

        var fileName = $(this).val();

        $('#uploadbtn').trigger('click');



    });

});

