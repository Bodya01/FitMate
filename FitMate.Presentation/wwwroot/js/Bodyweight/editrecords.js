function getDateString(DateObject) {
    let year = String(DateObject.getFullYear());
    let month = String(DateObject.getMonth() + 1);
    month = month.length === 1 ? "0".concat(month) : month;
    let day = String(DateObject.getDate());
    day = day.length === 1 ? "0".concat(day) : day;

    return year + "-" + month + "-" + day;
}

function updateInputNames() {
    $("table tbody tr").each(function (index, element) {
        $(element).find("input[type=number]").attr("name", "rw[" + String(index) + "]");
        $(element).find("input[type=date]").attr("name", "rd[" + String(index) + "]");
    });
}

function removeTemplateRow() {
    $("#NewRowTemplate").remove();
}

function removeRow(sender) {
    $(sender).parents("tr").remove();
}

function addNewRow(weight, date) {
    let rowClone = $("#NewRowTemplate").clone();
    rowClone.attr("id", null).removeClass("d-none");
    rowClone.find("input").eq(0).val(weight);
    rowClone.find("input").eq(1).val(date);

    $("table tbody").prepend(rowClone);
}

function validateNewDate() {
    let newDate = $("#NewDateInput").val();
    let result = true;

    $("table tbody tr td input[type=date]").each(function (index, element) {
        if ($(element).val() == newDate) {
            $("#NewDateInput").addClass("border-danger");
            result = false;
            return false;
        }
    });

    if (result === true)
        $("#NewDateInput").removeClass("border-danger");
    return result;
}

function validateAllDates() {
    let result = true;
    let elements = $("table tbody tr td input[type=date]")
    elements.removeClass("border-danger");
    elements.each(function (outerIndex, outerElement) {
        elements.each(function (innerIndex, innerElement) {
            if (outerIndex === innerIndex)
                return true;
            else {
                if ($(outerElement).val() === $(innerElement).val()) {
                    $(outerElement).addClass("border-danger");
                    $(innerElement).addClass("border-danger");
                    result = false;
                    return true;
                }
            }
        });
    });

    return result;
}


////////  Event Handlers  ////////
function formSubmit_Clicked() {
    if (validateAllDates() === false)
        return;
    removeTemplateRow();
    updateInputNames();
}

function addRowButton_Clicked() {
    
    let weightInput = $("#NewWeightInput");
    let dateInput = $("#NewDateInput");

    weightInput.removeClass("border-danger");
    dateInput.removeClass("border-danger");

    if (validateNewDate() === false)
        return;

    let weight = weightInput.val();
    if (weight == "") {
        weightInput.addClass("border-danger");
        return;
    }

    let date = dateInput.val();
    if (date == "") {
        dateInput.addClass("border-danger");
        return;
    }

    addNewRow(weight, date);
    weightInput.val("");
}

$(document).ready(function () {
    validateNewDate();
});