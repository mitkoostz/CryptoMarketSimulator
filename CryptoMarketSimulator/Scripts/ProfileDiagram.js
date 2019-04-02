

$(document).ready(function () {
 

    $.post("/Profile/UserTop5",
        {
            name: "Donald Duck",
            city: "Duckburg"
        },
        function (data, status) {

            var transactions = document.getElementById("transactions").getContext('2d');
            var myLineChart = new Chart(transactions, {
                type: 'line',
                data: {
                    labels: data.map(d => d.Currency),
                    datasets: [
                        {
                            label: "Transactions",
                            backgroundColor: "rgba(26, 83, 255,0.1)",
                            borderWidth: 2,
                            borderColor: "rgba(173, 173, 133,1)",
                            pointBackgroundColor: "rgba(0, 0, 204,1))",
                            pointBorderColor: "#fff",
                            pointBorderWidth: 1,
                            pointRadius: 4,
                            pointHoverBackgroundColor: "#6699ff",
                            pointHoverBorderColor: "rgba(220,220,220,1)",
                            data: data.map(d=> d.Transactions)
                        },                      
                    ]
                },
                options: {
                    responsive: true
                }
            });

            var usdcost = document.getElementById("usdcost").getContext('2d');
            var UsdCostChart = new Chart(usdcost, {
                type: 'line',
                data: {
                    labels: data.map(d => d.Currency),
                    datasets: [
                        {
                            label: "$ spent",
                            backgroundColor: "rgba(26, 83, 255,0.1)",
                            borderWidth: 2,
                            borderColor: "rgba(173, 173, 133,1)",
                            pointBackgroundColor: "rgba(0, 0, 204,1))",
                            pointBorderColor: "#fff",
                            pointBorderWidth: 1,
                            pointRadius: 4,
                            pointHoverBackgroundColor: "#6699ff",
                            pointHoverBorderColor: "rgba(220,220,220,1)",
                            data: data.map(d => d.DollarsSpend.toFixed(2))
                        },
                    ]
                },
                options: {
                    responsive: true
                }
            });



        });


});
