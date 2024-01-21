function typeRadio_Changed() {
    if ($("#WeightliftingRadio:checked").length === 1) {
        $("#WeightliftingGroup").removeClass("d-none");
        $("#TimedGroup").addClass("d-none");
        $("#goalForm").action = "/Goal/CreateWeightlifting"
    } else {
        $("#WeightliftingGroup").addClass("d-none");
        $("#TimedGroup").removeClass("d-none");
        $("#goalForm").action = "/Goal/CreateTimed"
    }
}

$(document).ready(function () {
    typeRadio_Changed();
});