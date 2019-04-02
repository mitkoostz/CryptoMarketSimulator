var btc = parseFloat($("#bitcoinPrice").text());
var currency = parseFloat($("#currencyPrice").text());

$('#buyfield').on('input', function (e) {

    var value = parseFloat($(this).val());
    var btcCost = (value * currency) / btc;
    var usdCost = value * currency;


    $("#pricebtc").text(btcCost.toFixed(8) + " BTC");
    $("#priceusd").text(usdCost.toFixed(3) + " USD");

});

$('#sellfield').on('input', function (e) {

    var value = parseFloat($(this).val());
    var btcCost = (value * currency) / btc;
    var usdCost = value * currency;


    $("#pricebtc2").text(btcCost.toFixed(8) + " BTC");
    $("#priceusd2").text(usdCost.toFixed(3) + " USD");



});

