$.ajax({
    type: "GET",
    dataType: "json",
    url: "/Home/user",
    success: displayUser,
    error: errorOnAjax
});

function errorOnAjax() {
    console.log("ERROR in ajax request.");
}

function displayUser(data) {
    $('#name').append(data[5]);
    $('#pic').append($('<img />').attr('src', data[6]));


    for (var i = 0; i < data.length-3; ++i) {
            $('#userStuff').append($('<li>' + data[i] + '</li>'));
    }
}