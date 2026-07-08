// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
        $("#bookingDate").datepicker({
            dateFormat: "yy-mm-dd"
        });
    $(".showBookingDetails").on('click', function (e) {
        e.preventDefault();
        $('.BookingDetails').show();
    })
})
