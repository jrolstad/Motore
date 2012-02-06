var submitCustomerEmailOptions = {
    iframe: false,
    resetForm: true,
    clearForm: true,
    beforeSubmit: function (arr, $form, options) {
        if (!$form.valid()) {
            return false;
        }
    },
    success: function (json) {
        if (json.success) {
            alert('success!');
        } else {
            alert('Error: ' + json.exception);
        }
    },
    error: function (xhr, textStatus, errorThrown) {
        alert(xhr.responseText + "\n" + textStatus + "\n" + errorThrown);
    }
};

$(function () {
    var $form = $("#SubmitCustomerEmailForm");
    $form.ajaxForm(submitCustomerEmailOptions);

    $("#SubmitCustomerEmailButton").button();
});