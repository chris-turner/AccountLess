$('#registerFormMessage').click(function () {
    $('#registerForm').animate({ height: "hide", opacity: "hide" }, "slow");
    $('#loginForm').animate({ height: "show", opacity: "show" }, "slow");
});

$('#loginFormMessage').click(function () {
    $('#loginForm').animate({ height: "hide", opacity: "hide" }, "slow");
    $('#registerForm').animate({ height: "show", opacity: "show" }, "slow");
});

$('#registerFormWithErrorMessage').click(function () {
    $('#registerFormWithError').animate({ height: "hide", opacity: "hide" }, "slow");
    $('#loginFormRegError').animate({ height: "show", opacity: "show" }, "slow");
});

$('#loginFormRegErrorMessage').click(function () {
    $('#loginFormRegError').animate({ height: "hide", opacity: "hide" }, "slow");
    $('#registerFormWithError').animate({ height: "show", opacity: "show" }, "slow");
});

