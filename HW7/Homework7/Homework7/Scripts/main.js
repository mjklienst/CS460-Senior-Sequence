
//Function for whatever button is clicked, shows commits for that repo
function myFunction(num) {
    var user = document.getElementById("repoOwner" + num).innerText;
    var repo = document.getElementById("repoName" + num).innerText;

    $.ajax({
        type: "GET",
        dataType: "json",
        //url: "/Home/commits?user={user}&repo={repo}",
        url: "/Home/commits?user=" + user + "&repo=" + repo,
        data: { 'userName': user, 'repoName': repo },
        success: displayCommits,
        error: errorOnAjax
    });
}

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

//Putting commits into a table
function displayCommits(data) {
    document.getElementById("tablePlacement").remove();
    $('#bigTable').append($('<table id=\"tablePlacement\">'));
    $('#tablePlacement').append($('<tr id=\"tableTr\">'));
    $('#tableTr').append($('<td id=\"tdSha\"> <strong> SHA </td>'));
    $('#tableTr').append($('<td id=\"tdCommitter\"> <strong> Committer </td>'));
    $('#tableTr').append($('<td id=\"tdWhen\"> <strong> Timestamp </td>'));
    $('#tableTr').append($('<td id=\"tdMessage\"> <strong> Commit Message </td>'));
    $('#tablePlacement').append($('</tr>'));
    $('#bigTable').append($('</table>'));

    //Putting items in table starting from the last element to output in descending order (latest commits show at top)
    for (var i = data.length - 1; i >= 0; --i) {
        var rowCount = 1;
        var table = document.getElementById("tablePlacement");
        var row = table.insertRow(rowCount); //Insert at new row
        //Add all 4 cells at a time
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);
        var cell4 = row.insertCell(3);
        //Update all 4 cells data
        cell1.innerHTML = '<a href="' + data[i].Url + '">' + data[i].Sha + '</a>';
        cell2.innerHTML = data[i].Committer;
        cell3.innerHTML = data[i].WhenCommitted;
        cell4.innerHTML = data[i].CommitMessage;
        rowCount++; //Increment rowCount so we can add new row
    }
}

//Putting user data on home page
function displayUser(data) {
    var a = document.getElementById('name'); //Making name into a hyperlink
    a.href = data[4];
    $('#name').append(data[5]);

    $('#pic').append($('<img />').attr('src', data[6])); //Adding image

    //Show the rest of the user data
    for (var i = 0; i < data.length - 3; ++i) {
        $('#userStuff').append($('<li>' + data[i] + '</li>'));
    }
}

//Putting repo data on home page
function displayRepos(data) {
    var repNum = 1; //For the html tags
    var a; //For the hyperlink
    for (var i = 0; i < data.length; ++i) {
        $('#repoName' + repNum).append(data[i].Name);
        $('#repoOwner' + repNum).append(data[i].OwnerLogin);
        $('#lastUpdated' + repNum).append(data[i].UpdatedAt);
        tagName = 'repoName' + repNum;
        a = document.getElementById(tagName); //Setting the hyperlink up
        a.href = data[i].HtmlUrl;
        $('#pic' + repNum).append($('<img />').attr('src', data[i].OwnerAvatarUrl));
        repNum++; //Increment so we can match new html tag
    }
}

