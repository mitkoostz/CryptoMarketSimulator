$.post("/Trade/GetCurrencyCharts",
    {
        currencyname: "Bitcoin"

    },
    function (json, status) {

        var data = $.parseJSON(json);

        //for (var i = 0; i < data.length; i++) {

        //    var date = data[i].substring(5, 7);
        //    data[i].Date = date;

        //}
        var currencychart = document.getElementById("BtcCharts").getContext('2d');
        var myLineChart = new Chart(currencychart, {
            type: 'line',
            data: {
                labels: data.map(d => d.Date.substring(5, 16)),
                datasets: [
                    {
                        label: "USD",
                        backgroundColor: "rgba(237, 143, 11,0.6)",
                        borderWidth: 1,
                        borderColor: "rgba(237, 143, 11,0.6)",
                        pointBackgroundColor: "rgba(237, 143, 11,0.6)",
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
                responsive: true,
                
                
            }
        });
    });