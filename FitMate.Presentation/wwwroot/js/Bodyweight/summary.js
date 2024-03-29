﻿function setupGraph(Days, TargetSelector) {
    $.get("/Bodyweight/GetBodyweightData", { PreviousDays: Days }, function (data) {
        let dates = data.map(x => x.date);
        let weights = data.map(x => x.weight)
        let goalValue = $("#TargetWeight").data("target");
        let goals = Array(dates.length).fill(goalValue);

        let minValue = Math.min(Math.min(...weights), goalValue) * 0.99;
        let maxValue = Math.max(Math.max(...weights), goalValue) * 1.01;

        let context = $(TargetSelector)[0].getContext("2d");
        let chart = new Chart(context, {
            type: "line",
            data: {
                labels: dates,
                datasets: [{
                    label: "Weight (kg)",
                    data: weights,
                    backgroundColor: 'rgba(0,0,0,0)',
                    borderColor: ["#0089dc"],
                    borderWidth: 2,
                    lineTension: 0
                },
                {
                    label: "Goal (kg)",
                    data: goals,
                    backgroundColor: 'rgba(0,0,0,0)',
                    borderColor: ["#0089dc"],
                    borderWidth: 2,
                    borderDash: [5, 5]
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

$(document).ready(function () {
    setupGraph(7, "#WeekGraph");
    setupGraph(28, "#MonthGraph");
});