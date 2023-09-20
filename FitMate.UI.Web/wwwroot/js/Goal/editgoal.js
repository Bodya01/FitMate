function typeRadio_Changed() {
    if ($("#WeightliftingRadio:checked").length === 1) {
        $("#WeightliftingGroup").removeClass("d-none");
        $("#TimedGroup").removeClass("d-none"); // Remove the "d-none" class here
    } else {
        $("#WeightliftingGroup").addClass("d-none");
        $("#TimedGroup").addClass("d-none");
    }
}

$(document).ready(function () {
    typeRadio_Changed();
});