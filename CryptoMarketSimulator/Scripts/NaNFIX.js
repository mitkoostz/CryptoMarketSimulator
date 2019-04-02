$('#buyfield').on('input', function (e) {

    var value = parseFloat($(this).val());

    if (isNaN(value)) {

        $("#pricebtc").text("0.00000000 BTC");
        $("#priceusd").text("0.00 USD");
    }
    $("#buyAlert").text("");

});


$('#sellfield').on('input', function (e) {

    var value = parseFloat($(this).val());

    if (isNaN(value)) {

        $("#pricebtc2").text("0.00000000 BTC");
        $("#priceusd2").text("0.00 USD");
    }
    $("#sellAlert").text("");

});

