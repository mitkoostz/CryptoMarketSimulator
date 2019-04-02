

function ValidateBuy(btc, currencyprice,UserBtcBalance) {

    var value = parseFloat($('#buyfield').val());

    var btcCost = (value * currencyprice) / btc;
    var usdCost = (value * currencyprice);
    var userBalance = UserBtcBalance;
 

    if (userBalance > btcCost && btcCost > 0) {
        // $("#modalinfo").text("SUCCESSFULLY BOUGHT " + value + " " + currencyname)
        //$("#myModal").modal('show');
        return true;
    }

    if (userBalance < btcCost) {
        $("#buyAlert").text("BALANCE IS NOT ENOUGH!");
        return false;
    }

    if (isNaN(value)) {
        $("#buyAlert").text("INCORRECT INPUT!");
        return false;
    }

    if (value < 0) {
        $("#buyAlert").text("INCORRECT INPUT!");
        return false;
    }
    if (value == 0) {
        $("#buyAlert").text("INCORRECT INPUT!");
        return false;
    }
      
    
    
}
function ValidateSell(btcprice,currencyprice,UserBalance,CurrencyBalance) {

    var btc = btcprice;
    var currency = currencyprice;
    var userBalance = UserBalance;
    var currencyBalance = CurrencyBalance;


    var value = parseFloat($('#sellfield').val());
    var btcCost = (value * currency) / btc;
    var usdCost = value * currency;



    if (currencyBalance >= value && btcCost > 0 && value != 0) {
        return true;
    }

    if (currencyBalance < value) {
        $("#sellAlert").text("BALANCE IS NOT ENOUGH!");
        return false;
    }

    if (isNaN(value)) {
        $("#sellAlert").text("INCORRECT INPUT!");
        return false;
    }

    if (value < 0) {
        $("#sellAlert").text("INCORRECT INPUT!");
        return false;
    }

    if (value == 0) {
        $("#sellAlert").text("INCORRECT INPUT!");
        return false;
    }



}

$("#buybutton").click(function () {

    var currencyname = $("#currencyname").text();

         $.ajax({
        type: "POST",
        url: "/Trade/Validate",
             data: { currencyname: currencyname },            
        success: function (json) {

            var data = $.parseJSON(json);

            var btc = parseFloat(data.BitcoinPrice);
            var currencyprice = parseFloat(data.CurrencyPrice);
            var userBalance = parseFloat(data.UserBtcBalance);

           

            if (ValidateBuy(btc, currencyprice, userBalance)) {


                var amount = $("#buyfield").val();
                var name = $("#currencyname").text();
                $("#buyloader").show();

                $.ajax({
                    type: "POST",
                    url: "/Trade/Buy",
                    data: { amount: amount, name: name },
                    success: function (json) {

                        var data = $.parseJSON(json);

                        $("#buysuccess").text("Bought " + amount + " " + name);


                        if (data.CurrencyCurrentPrice < 0.50) {
                            $("#CurrencyPrice").text(data.CurrencyCurrentPrice.toFixed(6) + "$");

                        } else {
                            $("#CurrencyPrice").text(data.CurrencyCurrentPrice.toFixed(2) + "$");

                        }
                        $("#CurrencyBalance").text(data.CurrencyBalance.toFixed(8));

                        $("#userb").text(data.UserBtcBalance.toFixed(8));
                        $("#userusd").text(data.UserUsdBalance.toFixed(2) + "$");

                        $("#buyloader").hide();



                        // $("#TradeB").html(response);
                        //$("#TradeB").load(window.location.href, function () {
                        //    
                        //});
                    }
                });
            }
        }
    });

   
});

$("#sellbutton").click(function () {

    var currencyname = $("#currencyname").text();

    $.ajax({
        type: "POST",
        url: "/Trade/Validate",
        data: { currencyname: currencyname },
        success: function (json) {

            var data = $.parseJSON(json);

            var btc = parseFloat(data.BitcoinPrice);
            var currencyprice = parseFloat(data.CurrencyPrice);
            var userBalance = parseFloat(data.UserBtcBalance);
            var currencyBalance = parseFloat(data.UserCurrencyBalance);

            if (ValidateSell(btc, currencyprice, userBalance, currencyBalance)) {

                var amount = $("#sellfield").val();
                var name = $("#currencyname").text();
                $("#sellloader").show();

                $.ajax({
                    type: "POST",
                    url: "/Trade/Sell",
                    data: { amount: amount, name: name },
                    success: function (json) {

                        $("#sellsuccess").text("Sold " + amount + " " + name);

                        var data = $.parseJSON(json);

                        if (data.CurrencyCurrentPrice < 0.50)
                        {
                            $("#CurrencyPrice").text(data.CurrencyCurrentPrice.toFixed(6) + "$");

                        } else {
                            $("#CurrencyPrice").text(data.CurrencyCurrentPrice.toFixed(2) + "$");

                        }
                        $("#CurrencyBalance").text(data.CurrencyBalance.toFixed(8));

                        $("#userb").text(data.UserBtcBalance.toFixed(8));
                        $("#userusd").text(data.UserUsdBalance.toFixed(2) + "$");

                        $("#sellloader").hide();

                    }
                });
            }
        }
    });


   
});

