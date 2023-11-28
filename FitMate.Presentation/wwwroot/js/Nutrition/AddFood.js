function addFoodRecord(addButton) {
    let selectedCard = $(addButton).parents(".card");

    let id = $(selectedCard).data("id");
    let name = $(selectedCard).data("name");
    let carbs = $(selectedCard).data("carbs");
    let protein = $(selectedCard).data("protein");
    let fat = $(selectedCard).data("fat");
    let calories = $(selectedCard).data("calories");

    let rowClone = $("#NewRowTemplate").clone();
    rowClone.removeAttr("id");
    rowClone.removeClass("d-none");

    rowClone.find(".recordID").val(id);
    rowClone.find(".recordName").val(name);
    rowClone.find(".recordCarbs").val(carbs);
    rowClone.find(".recordProtein").val(protein);
    rowClone.find(".recordFat").val(fat);
    rowClone.find(".recordCalories").val(calories);

    $("#RecordBody").append(rowClone);
    updateInputNames();
}

function setNewFoodFields(editButton) {
    let selectedCard = $(editButton).parents(".card");

    let id = $(selectedCard).data("id");
    let name = $(selectedCard).data("name");
    let carbs = $(selectedCard).data("carbs");
    let protein = $(selectedCard).data("protein");
    let fat = $(selectedCard).data("fat");
    let calories = $(selectedCard).data("calories");
    let size = $(selectedCard).data("servingsize");
    let unit = $(selectedCard).data("servingunit");

    $("#existingFoodID").val(id);
    $("#newFoodName").val(name);
    $("#newFoodSize").val(size);
    $("#newFoodUnit").val(unit);
    $("#newFoodCarbs").val(carbs);
    $("#newFoodProtein").val(protein);
    $("#newFoodFat").val(fat);
    $("#newFoodCalories").val(calories);

    newFoodIDChanged();

}

function newFoodIDChanged() {
    if ($("#existingFoodID").val() == 0) {
        $("#NewFoodHeader").text("Add New Food")
        $("#NewFoodCancel").addClass("d-none");
    }
    else {
        $("#NewFoodHeader").text("Edit Food");
        $("#NewFoodCancel").removeClass("d-none");
    }
}

function cancelEdit() {
    $("#existingFoodID").val(0);
    newFoodIDChanged();
}

function updateInputNames() {
    $("#RecordBody").find("tr").each(function (index, row) {
        $(row).find(".recordID").attr("name", "FoodIDs[" + String(index) + "]");
        $(row).find(".recordQuantity").attr("name", "Quantities[" + String(index) + "]");
    });
}

function removeRow(row) {
    $(row).parents("tr").remove();
    updateInputNames();
}

function updateCalories() {
    let carbs = $("#newFoodCarbs").val();
    let protein = $("#newFoodProtein").val();
    let fat = $("#newFoodFat").val();
    let calories = (carbs * 4) + (protein * 4) + (fat * 9);
    $("#newFoodCalories").val(calories);
}