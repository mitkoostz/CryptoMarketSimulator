
$(document).ready(function () {


    $.post("/Home/CurrentPrices",
        {
            name: "Donald Duck",
            city: "Duckburg"
        },
        function (data, status) {
           

            //line
            var ctxL = document.getElementById("lineChart").getContext('2d');
            var myLineChart = new Chart(ctxL, {
                type: 'line',
                data: {
                    labels: data.map(d=>d.Name),
                    datasets: [
                        {
                            label: "My First dataset",
                            fillColor: "rgba(220,220,220,0.2)",
                            strokeColor: "rgba(220,220,220,1)",
                            pointColor: "rgba(220,220,220,1)",
                            pointStrokeColor: "#fff",
                            pointHighlightFill: "#fff",
                            pointHighlightStroke: "rgba(220,220,220,1)",
                            data: data.map(d => d.Price)
                        },
                    ]
                },
                options: {
                    responsive: true
                }
            });

        });


});

            