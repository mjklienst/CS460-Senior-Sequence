$.ajax({
    type: "GET",
    dataType: "json",
    url: "/Home/repositories",
    success: displayRepos,
    error: errorOnAjax
});

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

//data.Name[i] ==
function displayRepos(data) {
   // $('#repoName').append(data[0].Name);

    //$('#pic').append($('<img />').attr('src', data[6]));

    var repNum = 1;
    for (var i = 0; i < data.length; ++i) {
       // $('#repoName').append($(data[0].Name));
        $('#repoName'+repNum).append(data[i].Name);
        $('#repoOwner'+repNum).append(data[i].OwnerLogin);
        $('#lastUpdated'+repNum).append(data[i].UpdatedAt);
        repNum++;
    }
}

document.getElementById("repoOwner5").onclick = function () {
    window.open("Google.com");
}