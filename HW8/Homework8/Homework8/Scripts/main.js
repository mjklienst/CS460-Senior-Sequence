//Showing athlete info
function myFunction(num) {

    var teamName = document.getElementById("Team" + num).innerText;
    //window function declares a global variable so we can access this outside the current function
    //keeping track of this button click
    window.numClickOne = num;

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Home/Athletes",
        data: { 'TeamName': teamName},
        success: teamAthletes,
        error: errorOnAjax
    });
}

//Showing event info
function myFunction2(num) {
    var athleteName = document.getElementById("Athlete" + num).innerText;
    var teamName = document.getElementById("Team" + window.numClickOne).innerText;
    //window function declares a global variable so we can access this outside the current function
    //keeping track of this button click
    window.numClickTwo = num;

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Home/Events",
        data: { 'AthleteName': athleteName, 'TeamName': teamName },
        success: events,
        error: errorOnAjax
    });
}

//Showing graph of dates and racetimes
function myFunction3(num) {
    var Event = document.getElementById("Event" + num).innerText;
    var athleteName = document.getElementById("Athlete" + numClickTwo).innerText;

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Home/GraphMath",
        data: { 'Event': Event, 'AthleteName': athleteName },
        success: plotGraph,
        error: errorOnAjax
    });
}

//Showing team data
$.ajax({
    type: "GET",
    dataType: "json",
    url: "/Home/TeamInfo",
    success: teamData,
    error: errorOnAjax
});

function errorOnAjax() {
    console.log("ERROR in ajax request.");
}

function teamData(data) {
    var teamCount = 0; //Counter for element id
    for (var i = 0; i < data.length; ++i) {
        $('#teamList').append($('<button id="Team' + teamCount + '" onclick="myFunction(' + teamCount + ')">' + data[i].TeamName + '</button>'));
        teamCount++;
    }
}

function teamAthletes(data) {
    document.getElementById("athleteList").remove();
    $('#athleteListOutside').append($('<div id="athleteList"></div> '));
    var athCount = 0; //Counter for element id
    for (var i = 0; i < data.length; ++i) {
        $('#athleteList').append($('<button id="Athlete' + athCount + '" onclick="myFunction2(' + athCount + ')">' + data[i].AthleteName + '</button>'));
        athCount++;
    }
}

function events(data) {
    document.getElementById("eventList").remove();
    $('#eventListOutside').append($('<div id="eventList"></div> '));
    var eventCount = 0; //Counter for element id
    for (var i = 0; i < data.length; ++i) {
        $('#eventList').append($('<button id="Event' + eventCount + '" onclick="myFunction3(' + eventCount + ')">' + data[i].Event1 + '</button>'));
        eventCount++;
    }
}


function plotGraph(data) {
    //Getting all the MeetDate info
    var dateArray = [];
    for (var i = 0; i < data.length; ++i) {
        dateArray[i] = data[i].MeetDate;
    }
    //Getting all the RaceTime info
    var timeArray = [];
    for (var i = 0; i < data.length; ++i) {
        timeArray[i] = data[i].RaceTime;
    }
    var trace = {
        x: dateArray,
        y: timeArray,
        mode: 'lines',
        type: 'scatter',
    };
    var plotData = [trace];
    var layout = {};
    Plotly.newPlot('theplot', plotData, layout);
}