
function myFunction(num) {
    var user = document.getElementById("repoOwner" + num).innerText;
    var repo = document.getElementById("repoName" + num).innerText;
    //alert("repo owner: " + user + " repo name: " + repo);

    $.ajax({
        type: "GET",
        dataType: "json",
        //url: "/Home/commits?user={user}&repo={repo}",
        url: "/Home/commits?user=" + user + "&repo=" + repo,
        data: {'userName': user, 'repoName': repo},
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

    for (var i = data.length-1; i >= 0; --i) {
        console.log("i: " + i + " : " + data[i].WhenCommitted);
        var rowCount = 1;
        var table = document.getElementById("tablePlacement");
        var row = table.insertRow(rowCount);
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);
        var cell4 = row.insertCell(3);
        cell1.innerHTML = data[i].Sha;
        cell2.innerHTML = data[i].Committer;
        cell3.innerHTML = data[i].WhenCommitted;
        cell4.innerHTML = data[i].CommitMessage;
        rowCount++;
    }
}

function displayUser(data) {
    var a = document.getElementById('name');
    a.href = data[4];
    $('#name').append(data[5]);
    $('#pic').append($('<img />').attr('src', data[6]));


    for (var i = 0; i < data.length-3; ++i) {
            $('#userStuff').append($('<li>' + data[i] + '</li>'));
    }
}

function displayRepos(data) {
    var repNum = 1;
    var a;
    for (var i = 0; i < data.length; ++i) {
        $('#repoName'+repNum).append(data[i].Name);
        $('#repoOwner' + repNum).append(data[i].OwnerLogin);
        $('#lastUpdated' + repNum).append(data[i].UpdatedAt);
        tagName = 'repoName' + repNum;
        console.log("owner: " + document.getElementById(tagName).innerText + " tag Name: " + tagName + " url: " + data[i].HtmlUrl);
        a = document.getElementById(tagName);
        a.href = data[i].HtmlUrl;
        $('#pic' + repNum).append($('<img />').attr('src', data[i].OwnerAvatarUrl));
        repNum++;
    }
}

