function getData(ParentSelector, DataName) {
    return $(ParentSelector + " div").map(function (index, element) {
        return $(element).data(DataName);
    });
}

function setupWeekGraph() {
    $.get("/Nutrition/GetNutritionData", { PreviousDays: 7 }, function (data) {
        let dates = data.map(function (record) {
            let date = new Date(record.date);
            let day = date.getDate();
            let monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            let month = monthNames[date.getMonth()];
            return `${day} ${month}`;
        });

        let calories = data.map(x => x.calories);
        let carbs = data.map(x => x.carbs);
        let protein = data.map(x => x.protein);
        let fat = data.map(x => x.fat);


        let weekCaloriesContext = $("#WeekCaloriesGraph")[0].getContext("2d");
        let weekCaloriesChart = new Chart(weekCaloriesContext, {
            type: "line",
            data: {
                labels: $.makeArray(dates).reverse(),
                datasets: [{
                    label: "Calories",
                    data: $.makeArray(calories).reverse(),
                    backgroundColor: 'rgba(0,0,0,0)',
                    borderColor: 'rgba(0,0,255,1)',
                    borderWidth: 2,
                    lineTension: 0
                }]
            }
        });

        let weekMacroContext = $("#WeekMacroGraph")[0].getContext("2d");
        let weekMacroChart = new Chart(weekMacroContext, {
            type: "line",
            data: {
                labels: $.makeArray(dates).reverse(),
                datasets: [{
                    label: "Carbs",
                    data: $.makeArray(carbs).reverse(),
                    backgroundColor: 'rgba(0,0,0,0)',
                    borderColor: 'rgba(0, 199, 0, 1)',
                    borderWidth: 2,
                    lineTension: 0
                },
                {
                    label: "Protein",
                    data: $.makeArray(protein).reverse(),
                    backgroundColor: 'rgba(0,0,0,0)',
                    borderColor: 'rgba(240, 220, 0, 1)',
                    borderWidth: 2,
                    lineTension: 0
                },
                {
                    label: "Fat",
                    data: $.makeArray(fat).reverse(),
                    backgroundColor: 'rgba(0,0,0,0)',
                    borderColor: 'rgba(240, 0, 0, 1)',
                    borderWidth: 2,
                    lineTension: 0
                }]
            }
        });
    });


}

function setupMonthGraph() {
    $.get("/Nutrition/GetNutritionData", { PreviousDays: 28 }, function (data) {
        let dates = data.map(function (record) {
            let date = new Date(record.date);
            let day = date.getDate();
            let monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            let month = monthNames[date.getMonth()];
            return `${day} ${month}`;
        });
        let calories = data.map(x => x.calories);
        let carbs = data.map(x => x.carbs);
        let protein = data.map(x => x.protein);
        let fat = data.map(x => x.fat);


        let MonthCaloriesContext = $("#MonthCaloriesGraph")[0].getContext("2d");
        let MonthCaloriesChart = new Chart(MonthCaloriesContext, {
            type: "line",
            data: {
                labels: $.makeArray(dates).reverse(),
                datasets: [{
                    label: "Calories",
                    data: $.makeArray(calories).reverse(),
                    backgroundColor: 'rgba(0,0,0,0)',
                    borderColor: 'rgba(0,0,255,1)',
                    borderWidth: 2,
                    lineTension: 0
                }]
            },
            options: {
                plugins: {
                    customCanvasBackgroundColor: {
                        backgroundColor: '#fff',
                    }
                }
            }
        });

        let MonthMacroContext = $("#MonthMacroGraph")[0].getContext("2d");
        let MonthMacroChart = new Chart(MonthMacroContext, {
            type: "line",
            data: {
                labels: $.makeArray(dates).reverse(),
                datasets: [{
                    label: "Carbs",
                    data: $.makeArray(carbs).reverse(),
                    backgroundColor: 'rgba(0,0,0,0)',
                    borderColor: 'rgba(0, 199, 0, 1)',
                    borderWidth: 2,
                    lineTension: 0
                },
                {
                    label: "Protein",
                    data: $.makeArray(protein).reverse(),
                    backgroundColor: 'rgba(0,0,0,0)',
                    borderColor: 'rgba(240, 220, 0, 1)',
                    borderWidth: 2,
                    lineTension: 0
                },
                {
                    label: "Fat",
                    data: $.makeArray(fat).reverse(),
                    backgroundColor: 'rgba(0,0,0,0)',
                    borderColor: 'rgba(240, 0, 0, 1)',
                    borderWidth: 2,
                    lineTension: 0
                }]
            }
        });

    });
}

$(document).ready(function () {
    setupWeekGraph();
    setupMonthGraph();
});