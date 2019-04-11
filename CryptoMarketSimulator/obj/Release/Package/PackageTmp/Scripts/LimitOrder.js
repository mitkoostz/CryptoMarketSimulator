

function ValidateLimitBuy(currencybalance, btcbalance)
{
    var buyatprice = $("#BuyAtPrice").val();
    var buyamount = $("#BuyAmount").val();
    var totalBtcCost = parseFloat(buyatprice) * parseFloat(buyamount);

    if (btcbalance < totalBtcCost)
    {
        return false;
    } else
    {
        return true;
    }
}
function ValidateLimitSell(currencybalance, btcbalance) {

    var sellatprice = $("#SellAtPrice").val();
    var sellamount = $("#SellAmount").val();
    var totalBtcCost = parseFloat(sell) * parseFloat(sellamount);

    if (currencybalance < parseFloat(sellamount)) {
        return false;
    } else {
        return true;
    }
}

function AddOrder(OrderDate, OrderType,OrderCurrency, OrderPrice, OrderAmount) {

    var tablerow = "<tr >"
        + " <td id = \"OrderDate\" >" + OrderDate + "</td >"
        + " <td id = \"OrderDate\" >" + OrderType + "</td >"
        + " <td id = \"OrderDate\" >" + OrderCurrency + "</td >"
        + " <td id = \"OrderDate\" >" + OrderPrice + "</td >"
        + " <td id = \"OrderDate\" >" + parseFloat(OrderAmount).toFixed(8) + "</td >"
        + " <td id = \"OrderCancel\" >" + parseFloat(OrderAmount).toFixed(8) + "</td >"

        + "</tr>";

    $("#OrdersBody").append(tablerow);

}


$('#BuyAmount').on('input', function (e) {

        var buyatprice = $("#BuyAtPrice").val();
        var buyamount = $("#BuyAmount").val();
    var total = parseFloat(buyatprice) * parseFloat(buyamount);
    $("#Total").val(total.toFixed(8));

    $("#selllimitlabel").text("");
    $("#buylimitlabel").text("");


});
$('#BuyAtPrice').on('input', function (e) {

    var buyatprice = $("#BuyAtPrice").val();
    var buyamount = $("#BuyAmount").val();
    var total = parseFloat(buyatprice) * parseFloat(buyamount);
    $("#Total").val(total.toFixed(8));

    $("#selllimitlabel").text("");
    $("#buylimitlabel").text("");
});

$('#SellAmount').on('input', function (e) {

    var buyatprice = $("#SellAtPrice").val();
    var buyamount = $("#SellAmount").val();
    var total = parseFloat(buyatprice) * parseFloat(buyamount);
    $("#Total2").val(total.toFixed(8));

    $("#selllimitlabel").text("");
    $("#buylimitlabel").text("");


});
$('#SellAtPrice').on('input', function (e) {

    var buyatprice = $("#SellAtPrice").val();
    var buyamount = $("#SellAmount").val();
    var total = parseFloat(buyatprice) * parseFloat(buyamount);
    $("#Total2").val(total.toFixed(8));

    $("#selllimitlabel").text("");
    $("#buylimitlabel").text("");
});



function UpdateOrderTable(UserOrders) {

    $("#OrdersBody").empty();

    for (var i = 0; i < UserOrders.length; i++) {
        var OrderDate = UserOrders[i].OrderDate;
        var OrderType = UserOrders[i].OrderType;
        var OrderCurrency = UserOrders[i].Currency;
        var OrderPrice = UserOrders[i].AtPrice;
        var OrderAmount = UserOrders[i].Amount;
        var OrderCancel = UserOrders[i].Order;
        var OrderId = UserOrders[i].Id;


        var CancelOrderOnclick = "DeleteOrder(\"" + OrderCancel + "\"," + OrderId + ");";

        var tablerow = "<tr id=\"" + OrderId + "\" class=\"OrdersRow\">"
            + " <td id = \"OrderDate\">" + OrderDate + "</td>"
            + " <td id = \"OrderType\">" + OrderType + "</td>"
            + " <td id = \"OrderCurrency\">" + OrderCurrency + "</td>"
            + " <td id = \"OrderPrice\">" + OrderPrice + "</td>"
            + " <td id = \"OrderAmount\">" + parseFloat(OrderAmount).toFixed(8) + "</td>"
            + " <td id = \"OrderCancel\" ><span onclick=" + CancelOrderOnclick + " class=\"glyphicon glyphicon-remove\"" + "></td >"
            + "</tr>";

        //console.log(tablerow.toString());

        $("#OrdersBody").append(tablerow.toString());

    }

}

function GetUserOrders() {

    $.ajax({
        type: "POST",
        url: "/Trade/GetUserOrders",
        data: {},
        success: function (json) {
            var data = $.parseJSON(json);


            UpdateOrderTable(data.UserOrders);

        }


    });
}


$("#buylimitbutton").click(function () {

    var currencyname = $("#currencyname").text();
    var buyatprice = $("#BuyAtPrice").val();
    var buyamount = $("#BuyAmount").val();

    $.ajax({
        type: "POST",
        url: "/Trade/LimitOrderValidate",
        data: {
            currencyname: currencyname
           
        },

        success: function (json) {

            var data = $.parseJSON(json);
            var UserCurrencyBalance = data.UserCurrencyBalance;
            var UserBtcBalance = data.UserBtcBalance;
            if (ValidateLimitBuy(UserCurrencyBalance, UserBtcBalance)) {
                $.ajax({
                    type: "POST",
                    url: "/Trade/BuyLimitOrder",
                    data: {
                        currencyname: currencyname,
                        buyatprice: buyatprice,
                        buyamount: buyamount
                    },

                    success: function (json) {
                        var data = $.parseJSON(json);
                        var userb = parseFloat(data.UserBtc);
                        var userusd = parseFloat(data.UserUsd);

                        $("#userb").text(userb.toFixed(8));
                        $("#userusd").text(userusd.toFixed(2) +"$");

                        $("#buylimitlabel").attr("class", "text-success");
                        $("#buylimitlabel").text("Order successfuly placed.");

                        GetUserOrders();
                        //AddOrder(Date.now, "Buy", currencyname, buyatprice, buyamount);
                        
                    }

                });
            } else {
                //console.log("no balance");
                $("#buylimitlabel").attr("class", "text-danger");
                $("#buylimitlabel").text("Balance is not enough.");

            }

        }

    });

    

});

$("#selllimitbutton").click(function () {

    var currencyname = $("#currencyname").text();
    var sellatprice = $("#SellAtPrice").val();
    var sellamount = $("#SellAmount").val();

    $.ajax({
        type: "POST",
        url: "/Trade/LimitOrderValidate",
        data: {
            currencyname: currencyname

        },

        success: function (json) {

            var data = $.parseJSON(json);
            var UserCurrencyBalance = data.UserCurrencyBalance;
            var UserBtcBalance = data.UserBtcBalance;
            if (ValidateLimitSell(UserCurrencyBalance, UserBtcBalance)) {
                $.ajax({
                    type: "POST",
                    url: "/Trade/SellLimitOrder",
                    data: {
                        currencyname: currencyname,
                        sellatprice: sellatprice,
                        sellamount: sellamount
                    },

                    success: function (json) {
                        var data = $.parseJSON(json);
                        var usercurrency = parseFloat(data.UserCurrency);

                        $("#CurrencyBalance").text(usercurrency.toFixed(8));

                        $("#selllimitlabel").attr("class", "text-success");
                        $("#selllimitlabel").text("Order successfuly placed.");
                        GetUserOrders();
                    }

                });
            } else {
                //console.log("no balance");
                $("#selllimitlabel").attr("class", "text-danger");
                $("#selllimitlabel").text("Balance is not enough.");

            }

        }

    });



});