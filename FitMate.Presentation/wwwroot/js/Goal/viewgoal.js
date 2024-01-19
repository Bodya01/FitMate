$(document).ready(function () {
    var type = $("#ProgressType").data("goaltype");
    if (type == "weightlifting")
        LoadWeightliftingGraph();
});

function LoadWeightliftingGraph() {
    let id = $("#ProgressType").data("goalid");

    let progressData = $.get("/GoalProgress/GetWeightliftingProgress", { GoalId: id }, function (result) {
        let dates = result.map(function (record) { return record.date; });
        let weights = result.map(function (record) { return record.weight; });
        let goalWeight = Array(dates.length).fill($("#WeightliftingGoal").data("goal"));

        let minWeight = Math.min(...weights);
        let minValue = Math.min(minWeight, goalWeight[0]) - 5;

        let maxWeight = Math.max(...weights);
        let maxValue = Math.max(maxWeight, goalWeight[0]) + 5;

        let ctx = $("#ProgressChart")[0].getContext("2d");

        let chart = new Chart(ctx,
            {
                type: 'line',
                data: {
                    labels: dates,
                    datasets: [{
                        label: "Weight (kg)",
                        data: weights,
                        backgroundColor: 'rgba(0,0,0,0)',

                        borderColor: ["#0089dc"]
                    },
                    {
                        label: "Goal (kg)",
                        data: goalWeight,
                        borderDash: [5, 5],
                        backgroundColor: 'rgba(0,0,0,0)',

                        borderColor: ["#0089dc"]
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                suggestedMin: minValue,
                                suggestedMax: maxValue
                            }
                        }]
                    }
                }
            });
    });
}