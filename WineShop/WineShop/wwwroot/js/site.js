/*Saskira gehitzeko*/
$(document).ready(function () {
    $(".AddLink").click(function (e) {
        e.preventDefault();
        // Get the id from the link
        var recordToAdd = $(this).attr("data-id");
        var kantitatea = parseInt($(this).attr("data-kantitatea"));
        var salneurria = parseFloat($(this).attr("data-salneurria"));
        var guztira = parseFloat($("#cart-total").text());
        if (recordToAdd !== '') {
            // Perform the ajax post
            $.post("/Saskia/SaskiaGehituAjax", { "id": recordToAdd, "kantitatea": kantitatea, "salneurria": salneurria, "guztira": guztira },
                function (data) {
                    // Successful requests get here
                    // Update the page elements
                    $("#item-count-" + recordToAdd).text(data.kantitatea.toString());
                    $("#cart-total").text((data.guztira + data.salneurria).toString());
                    $("#update-message").text(data.mezua);
                });
        }
    });
})

