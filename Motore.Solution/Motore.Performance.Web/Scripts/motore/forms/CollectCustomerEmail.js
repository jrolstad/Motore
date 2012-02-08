$(function () {

    $("#SubmitCustomerEmailButton").button();
});

function showSubmitEmailResponse(response) {
    $("#customerEmailSubmitResult").html(response.message);
}