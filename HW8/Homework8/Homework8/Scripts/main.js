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
    var athCount = 0; //Counter for element id
    for (var i = 0; i < data.length; ++i) {
        $('#athleteList').append($('<button id="Athlete' + athCount + '" onclick="myFunction2(' + athCount + ')">' + data[i].AthleteName + '</button>'));
        athCount++;
    }
}

function events(data) {
    var eventCount = 0; //Counter for element id
    for (var i = 0; i < data.length; ++i) {
        $('#eventList').append($('<button id="Event' + eventCount + '" onclick="myFunction3(' + eventCount + ')">' + data[i].Event1 + '</button>'));
        eventCount++;
    }
}

function plotGraph(data) {
    var trace = {
        x: [data[0].MeetDate, data[1].MeetDate, data[2].MeetDate, data[3].MeetDate],
        y: [data[0].RaceTime,data[1].RaceTime,data[2].RaceTime,data[3].RaceTime],
        mode: 'lines',
        type: 'scatter',
    };
    var plotData = [trace];
    var layout = {};
    Plotly.newPlot('theplot', plotData, layout);
}