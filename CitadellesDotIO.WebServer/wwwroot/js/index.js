const connection = new signalR.HubConnectionBuilder()
    .withUrl("/lobbieshub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

connection.onclose(async () => {
    await start();
});

// Start the connection.
start().then(async () => {
    $.get("/home/get");
    connection.on("getLobbies", (lobbies) => {
        console.log(lobbies);
        $("#lobbiesDiv").empty();
        lobbies.forEach((lobby) => {
            $("#lobbiesDiv").append("<p>" + lobby.name + "<p>");
        });
    });

    $("#createLobbyButton").click(() => {
        $.get("/home/CreateLobby", { newLobbyName: 'NewRandomLobbyName' });
    });
});
