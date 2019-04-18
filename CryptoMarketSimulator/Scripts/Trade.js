

function UpdateBuyField() {

    var value = parseFloat($("#buyfield").val());
    if (isNaN(value)) { return; }
    var btc = parseFloat($("#bitcoinPrice").text());
    var currency = parseFloat($("#currencyPrice").text());

    var btcCost = (value * currency) / btc;
    var usdCost = value * currency;


    $("#pricebtc").text(btcCost.toFixed(8) + " BTC");
    $("#priceusd").text(usdCost.toFixed(3) + " USD");

}
function UpdateSellField() {

    var value = parseFloat($("#sellfield").val());
    if (isNaN(value)) { return; }
    var btc = parseFloat($("#bitcoinPrice").text());
    var currency = parseFloat($("#currencyPrice").text());

    var btcCost = (value * currency) / btc;
    var usdCost = value * currency;


    $("#pricebtc2").text(btcCost.toFixed(8) + " BTC");
    $("#priceusd2").text(usdCost.toFixed(3) + " USD");

}

$('#buyfield').on('input', function (e) {

    if ($(this).val().length > 8) {

        var value = $(this).val();
        $(this).val(value.slice(0, -1));

        return;
    }

    UpdateBuyField();

});

$('#sellfield').on('input', function (e) {

    if ($(this).val().length > 8) {

        var value = $(this).val();
        $(this).val(value.slice(0, -1));

        return;
    }
    UpdateSellField();

});



function RefreshCurrency() {

    var currencyname = $("#currencyname").text();
    $.ajax({
        type: "POST",
        url: "/Trade/RefreshCurrency",
        data: { currencyname: currencyname },
        success: function (json)
        {
            var data = $.parseJSON(json);

            $("#CurrencyPrice").text(data.CurrencyUsdPrice + "$");
            $("#CurrencyBTCPrice").text((data.CurrencyUsdPrice / data.BitcoinPrice).toFixed(8) + " BTC");

            $("#bitcoinPrice").text(data.BitcoinPrice);
            $("#currencyPrice").text(data.CurrencyUsdPrice);

            UpdateBuyField();
            UpdateSellField();

            $("#userb").text(data.UserBtcBalance.toFixed(8));
            $("#userusd").text(data.UserUsdBalance.toFixed(2) + "$");
            $("#CurrencyBalance").text(data.UserCurrencyBalance.toFixed(8));

            $("#currencyname").text(currencyname);
            // UpdateOrderTable(data.UserOrders);
            GetUserOrders();

        },
        complete: function () {

            setTimeout(function () { RefreshCurrency(); }, 4500);
        }

    });

}
$(document).ready(function () {

    RefreshCurrency();
});


function DeleteOrder(order,id)
{

    $.ajax({
        type: "POST",
        url: "/Trade/DeleteOrder",
        data: { order: order },
        success: function (json) {

            var data = $.parseJSON(json);
            $("#" + id).remove();
        }

    });

}



$(document).ready(function () {

    var currencyname = $("#currencyname").text();

    $.post("/Trade/GetCurrencyCharts",
        {
            currencyname: currencyname
           
        },
        function (json, status) {

            var data = $.parseJSON(json);

            //for (var i = 0; i < data.length; i++) {

            //    var date = data[i].substring(5, 7);
            //    data[i].Date = date;

            //}
            var currencychart = document.getElementById("CurrencyChart").getContext('2d');
            var myLineChart = new Chart(currencychart, {
                type: 'line',
                data: {
                    labels: data.map(d => d.Date.substring(5,16)),
                    datasets: [
                        {
                            label: "USD",
                            backgroundColor: "rgba(26, 83, 255,0.4)",
                            borderWidth: 1,
                            borderColor: "rgba(173, 173, 133,1)",
                            pointBackgroundColor: "rgba(0, 0, 204,1))",
                            pointBorderColor: "#fff",
                            pointBorderWidth: 1,
                            pointRadius: 1,
                            pointHoverBackgroundColor: "#6699ff",
                            pointHoverBorderColor: "rgba(220,220,220,1)",
                            data: data.map(d => d.CurrencyUsdValue)
                        }
                      
                    ]
                },
                options: {
                    responsive: true
                }
            });
        });


});
