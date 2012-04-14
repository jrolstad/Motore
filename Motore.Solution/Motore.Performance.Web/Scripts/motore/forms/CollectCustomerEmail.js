$(function () {
    $("#SubmitCustomerEmailButton").button();
});

function showSubmitEmailResponse(response) {
    $("#CustomerEmailSubmitResult").html(response.message);
    $("#CustomerEmailInput").hide();
}