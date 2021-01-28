$(document).ready(function () {
  $(".signup-form").on("submit", function (e) {
    if (
      $("input[name='Username']").val() == "" ||
      $("input[name='Email']").val() == "" ||
      $("input[name='Password']").val() == "" ||
      $("input[name='PasswordConfirm']").val() == ""
    ) {
      e.preventDefault();
      toastr["error"]("Please fill in the required fields", "", {
        positionClass: "toast-top-right",
        closeButton: true,
        progressBar: true,
        preventDuplicates: true,
        newestOnTop: true,
        rtl: $("body").attr("dir") === "rtl" || $("html").attr("dir") === "rtl",
      });
    } else if (
      $("input[name='Password']").val() !=
      $("input[name='PasswordConfirm']").val()
    ) {
      e.preventDefault();
      toastr["error"]("Passwords must match.", "", {
        positionClass: "toast-top-right",
        closeButton: true,
        progressBar: true,
        preventDuplicates: true,
        newestOnTop: true,
        rtl: $("body").attr("dir") === "rtl" || $("html").attr("dir") === "rtl",
      });
    }
  });
});
